using UnityEngine;

class ItemAnimation : MonoBehaviour
{
	readonly private float spinSpeed = 25;
	readonly private float floatSpeed = 2;
	readonly private float floatHeight = 0.25f;
	float initialHeight;

	protected virtual void Start()
	{
		initialHeight = transform.position.y;
	}

	protected virtual void Update()
	{
		transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
		float newHeight = initialHeight + Mathf.Sin(floatSpeed * Time.time) * floatHeight;
		transform.Translate((newHeight - transform.position.y) * Vector3.up);
	}
}