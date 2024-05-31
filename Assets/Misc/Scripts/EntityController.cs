using System.Collections;
using UnityEngine;

abstract public class EntityController : MonoBehaviour
{
	[SerializeField] protected float moveForce;
	[SerializeField] protected float mass = 1;
	protected Rigidbody body;
	protected bool isStunned;
	protected bool removed;
	private Coroutine stunCoroutine;

	protected virtual void Start()
	{
		body = GetComponent<Rigidbody>();
		body.mass = mass;
	}

	protected virtual void Update()
	{
		if(GameManager.isGameOver)
		{
			body.velocity = Vector3.zero;
			body.angularVelocity = Vector3.zero;
			return;
		}

		if(!isStunned)
		{
			Move();
		}
		removed |= transform.position.y < -20;
	}

	protected virtual void LateUpdate()
	{
		if(removed)
		{
			gameObject.SetActive(false);
			Destroy(gameObject, 3);
		}
	}

	public void Stun(float duration)
	{
		if(stunCoroutine != null)
		{
			StopCoroutine(stunCoroutine);
		}
		stunCoroutine = StartCoroutine(StunCoroutine(duration));
	}

	private IEnumerator StunCoroutine(float duration)
	{
		isStunned = true;
		yield return new WaitForSeconds(duration);
		isStunned = false;
		stunCoroutine = null;
	}

	abstract protected void Move();
}