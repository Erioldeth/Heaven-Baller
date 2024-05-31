using System;
using UnityEngine;

public abstract class Power : MonoBehaviour
{
	public static event Action UpdateSpawner;

	public enum Type { RADIATION, ELECTRO }

	[SerializeField] protected GameObject auraPrefab;

	[field: SerializeField] public float Duration { get; private set; }
	[field: SerializeField] public Type PowerType { get; private set; }

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out PowerReceiver receiver) && receiver.CanReceivePower(this))
		{
			receiver.ActivatePower(this);
			UpdateSpawner?.Invoke();
			Destroy(gameObject);
		}
	}

	public abstract void ApplyTo(GameObject receiver);

	public abstract void RemoveFrom(GameObject receiver);
}