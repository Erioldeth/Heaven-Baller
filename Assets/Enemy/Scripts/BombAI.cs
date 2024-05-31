using System.Collections;
using UnityEngine;

public class BombAI : EnemyAI
{
	[SerializeField] private float triggerRadius;
	[SerializeField] private float chargeTime;
	[SerializeField] private float blastRadius;
	[SerializeField] private float explosionForce;
	[SerializeField] private GameObject explosionPrefab;
	private bool canExplode;
	private float baseMoveForce;
	private Collider[] nearbyEntities;
	private GameObject explosion;

	private float DistanceToPlayer => Vector3.Distance(transform.position, player.transform.position);

	protected override void Start()
	{
		base.Start();
		canExplode = true;
		baseMoveForce = moveForce;
		nearbyEntities = new Collider[10];
		explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
	}

	protected override void Update()
	{
		base.Update();
		explosion.transform.position = transform.position;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Destroy(explosion, 3);
	}

	protected override void PerformAbility()
	{
		if(DistanceToPlayer < triggerRadius && canExplode)
		{
			StartCoroutine(Explode());
		}
	}

	private IEnumerator Explode()
	{
		canExplode = false;
		moveForce = 0;
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		particle.Play();
		yield return new WaitForSecondsRealtime(chargeTime);
		particle.Stop();

		if(DistanceToPlayer > triggerRadius)
		{
			canExplode = true;
			moveForce = baseMoveForce;
		}
		else
		{
			explosion.GetComponent<ParticleSystem>().Play();
			int entityMask = LayerMask.GetMask("Player", "Enemy");
			int entityCount = Physics.OverlapSphereNonAlloc(transform.position, blastRadius, nearbyEntities, entityMask);

			for(int i = 0; i < entityCount; ++i)
			{
				if(nearbyEntities[i].gameObject != gameObject)
				{
					Rigidbody entityBody = nearbyEntities[i].GetComponent<Rigidbody>();
					entityBody.AddExplosionForce(explosionForce, transform.position, blastRadius, 0, ForceMode.Impulse);
				}
			}

			removed = true;
		}
	}
}
