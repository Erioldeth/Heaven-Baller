using System.Collections;
using UnityEngine;

public class FlashAI : EnemyAI
{
	[SerializeField] private float chargeTime;
	[SerializeField] private float dashTime;
	[SerializeField] private float recoveryTime;
	[SerializeField] private float cooldown;
	private float baseMoveForce;
	private bool canDash;

	protected override void Start()
	{
		base.Start();
		canDash = true;
		baseMoveForce = moveForce;
		moveForce = 0;
	}

	protected override void PerformAbility()
	{
		if(canDash)
		{
			StartCoroutine(Dash());
		}
	}

	private IEnumerator Dash()
	{
		canDash = false;
		particle.Play();
		yield return new WaitForSecondsRealtime(chargeTime);
		particle.Stop();
		moveForce = baseMoveForce;
		yield return new WaitForSecondsRealtime(dashTime);
		moveForce = 0;
		yield return new WaitForSecondsRealtime(recoveryTime);
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		yield return new WaitForSecondsRealtime(cooldown);
		canDash = true;
	}
}