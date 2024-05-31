using UnityEngine;

public class MinionAI : EnemyAI
{
	[SerializeField] private float swarmRadius;
	[SerializeField] private float forceBuff;
	private Collider[] nearbyAllies;

	protected override void Start()
	{
		base.Start();
		nearbyAllies = new Collider[10];
	}

	protected override void PerformAbility()
	{
		if(!particle.isPlaying)
		{
			particle.Play();
		}

		int allyMask = LayerMask.GetMask("Enemy");
		int allyCount = Physics.OverlapSphereNonAlloc(transform.position, swarmRadius, nearbyAllies, allyMask) - 1;
		body.AddForce(forceBuff * allyCount * moveForce * DirectionToPlayer, ForceMode.Acceleration);

		if(allyCount == 0)
		{
			particle.Stop();
		}
	}
}