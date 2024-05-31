using UnityEngine;

public class RadiationBehavior : PowerBehavior
{
	public float blastRadius;
	public float explosionForce;
	public GameObject explosionPrefab;

	protected override void OnEnable()
	{
		base.OnEnable();
		if(aura != null)
		{
			Activate();
		}
	}

	protected override void Start()
	{
		base.Start();
		Activate();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(enabled && !collision.gameObject.CompareTag("Ground"))
		{
			Explode(collision.GetContact(0).point);
			BlowAway(collision.gameObject);
		}
	}

	private void Activate()
	{
		Explode(transform.position);
		int entityMask = LayerMask.GetMask("Player", "Enemy");
		foreach(Collider entity in Physics.OverlapSphere(transform.position, blastRadius, entityMask))
		{
			if(entity.gameObject != gameObject)
			{
				BlowAway(entity.gameObject);
			}
		}
	}

	private void Explode(Vector3 position)
	{
		Destroy(Instantiate(explosionPrefab, position, explosionPrefab.transform.rotation), 3);
	}

	private void BlowAway(GameObject entity)
	{
		Vector3 direction = (entity.transform.position - transform.position).normalized;
		entity.GetComponent<Rigidbody>().AddForce(explosionForce * direction, ForceMode.Impulse);
	}
}