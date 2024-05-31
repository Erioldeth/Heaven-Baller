using UnityEngine;

class ElectroBehavior : PowerBehavior
{
	public float stunTime;
	public GameObject hitPrefab;

	private void OnCollisionEnter(Collision collision)
	{
		if(enabled && collision.gameObject.TryGetComponent(out EntityController controller))
		{
			controller.Stun(stunTime);
			Destroy(Instantiate(hitPrefab, collision.GetContact(0).point, hitPrefab.transform.rotation), 1.5f);
		}
	}
}