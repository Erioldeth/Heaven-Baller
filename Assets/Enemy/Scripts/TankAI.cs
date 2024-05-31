using System.Collections;
using UnityEngine;

public class TankAI : EnemyAI
{
	[SerializeField] private float initialCooldown;
	[SerializeField] private float castTime;
	[SerializeField] private float duration;
	[SerializeField] private float cooldown;
	[SerializeField] private float massMultiplier;
	[SerializeField] private float sizeMultipler;
	private bool canUseAbility;

	protected override void Start()
	{
		base.Start();
		Invoke(nameof(ActivateAbility), initialCooldown);
	}

	protected override void PerformAbility()
	{
		if(canUseAbility)
		{
			StartCoroutine(Overwhelm());
		}
	}

	private void ActivateAbility()
	{
		canUseAbility = true;
	}

	private IEnumerator Overwhelm()
	{
		canUseAbility = false;
		body.AddForce(moveForce * Vector3.up, ForceMode.VelocityChange);
		yield return Transform(castTime, massMultiplier, sizeMultipler);
		particle.Play();
		yield return new WaitForSecondsRealtime(duration);
		particle.Stop();
		yield return Transform(castTime, 1 / massMultiplier, 1 / sizeMultipler);
		yield return new WaitForSecondsRealtime(cooldown);
		canUseAbility = true;
	}

	private IEnumerator Transform(float castTime, float massMultiplier, float sizeMultiplier)
	{
		body.mass *= massMultiplier;
		Vector3 currentScale = transform.localScale;
		Vector3 newScale = sizeMultiplier * currentScale;
		Vector3 scaleDifference = newScale - currentScale;
		float elapsedTime = 0;
		while(elapsedTime < castTime)
		{
			transform.position += scaleDifference.y / 2 * Time.fixedDeltaTime * Vector3.up;
			transform.localScale = Vector3.Lerp(currentScale, newScale, elapsedTime / castTime);
			elapsedTime += Time.fixedDeltaTime;
			yield return null;
		}
		transform.localScale = newScale;
	}
}