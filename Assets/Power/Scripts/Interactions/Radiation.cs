using UnityEngine;

public class Radiation : Power
{
	[SerializeField] private float massMultiplier;
	[SerializeField] private float blastRadius;
	[SerializeField] private float explosionForce;
	[SerializeField] private GameObject explosionPrefab;

	public override void ApplyTo(GameObject receiver)
	{
		receiver.GetComponent<Rigidbody>().mass *= massMultiplier;
		if(receiver.TryGetComponent(out RadiationBehavior script))
		{
			script.enabled = true;
		}
		else
		{
			script = receiver.AddComponent<RadiationBehavior>();
			script.blastRadius = blastRadius;
			script.explosionForce = explosionForce;
			script.auraPrefab = auraPrefab;
			script.explosionPrefab = explosionPrefab;
		}
	}

	public override void RemoveFrom(GameObject receiver)
	{
		receiver.GetComponent<Rigidbody>().mass /= massMultiplier;
		receiver.GetComponent<RadiationBehavior>().enabled = false;
	}
}