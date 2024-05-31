using System;
using UnityEngine;

abstract public class EnemyAI : EntityController
{
	public static event Action UpdateSpawner;

	[SerializeField] private GameObject auraPrefab;
	[SerializeField] private Vector3 auraOffset = Vector3.zero;
	private GameObject aura;
	protected GameObject player;
	protected ParticleSystem particle;

	protected Vector3 DirectionToPlayer => Vector3.Scale(player.transform.position - transform.position, new(1, 0, 1)).normalized;

	protected override void Start()
	{
		base.Start();
		player = GameObject.FindWithTag("Player");
		aura = Instantiate(auraPrefab, transform.position, auraPrefab.transform.rotation);
		particle = aura.GetComponent<ParticleSystem>();
	}

	protected override void Update()
	{
		if(GameManager.isGameOver)
		{
			return;
		}

		aura.transform.position = transform.position + auraOffset;
		if(!isStunned)
		{
			PerformAbility();
		}
		base.Update();
	}

	protected virtual void OnDisable()
	{
		Destroy(aura);
	}

	protected virtual void OnDestroy()
	{
		UpdateSpawner?.Invoke();
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject obj = collision.gameObject;
		if(obj != null && !obj.CompareTag("Ground"))
		{
			Vector3 impulse = collision.GetContact(0).impulse;
			obj.GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
			body.AddForce(-impulse, ForceMode.Impulse);
		}
	}

	protected override void Move()
	{
		body.AddForce(moveForce * DirectionToPlayer, ForceMode.Acceleration);
	}

	abstract protected void PerformAbility();
}