using UnityEngine;

public class Electro : Power
{
	[SerializeField] private float stunTime;
	[SerializeField] private GameObject hitPrefab;

	public override void ApplyTo(GameObject receiver)
	{
		if(receiver.TryGetComponent(out ElectroBehavior script))
		{
			script.enabled = true;
		}
		else
		{
			script = receiver.AddComponent<ElectroBehavior>();
			script.stunTime = stunTime;
			script.auraPrefab = auraPrefab;
			script.hitPrefab = hitPrefab;
		}
	}

	public override void RemoveFrom(GameObject receiver)
	{
		receiver.GetComponent<ElectroBehavior>().enabled = false;
	}
}