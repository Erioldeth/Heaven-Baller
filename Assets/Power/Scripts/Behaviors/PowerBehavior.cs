using UnityEngine;

public class PowerBehavior : MonoBehaviour
{
	public GameObject auraPrefab;
	protected GameObject aura;

	protected virtual void OnEnable()
	{
		if(aura != null)
		{
			aura.GetComponent<ParticleSystem>().Play();
		}
	}

	protected virtual void Start()
	{
		aura = Instantiate(auraPrefab, transform.position, auraPrefab.transform.rotation);
		aura.transform.localScale = Vector3.Scale(aura.transform.localScale / 1.5f, transform.localScale);
	}

	protected virtual void Update()
	{
		aura.transform.position = transform.position;
	}

	protected virtual void OnDisable()
	{
		aura.GetComponent<ParticleSystem>().Stop();
	}

	protected virtual void OnDestroy()
	{
		Destroy(aura);
	}
}