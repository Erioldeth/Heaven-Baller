using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReceiver : MonoBehaviour
{
	[SerializeField] private bool canReceiveAll;
	[SerializeField] private List<Power.Type> allowedPowerTypes;
	private long powerMask;
	private Dictionary<Power.Type, Coroutine> activePowers;

	private void Start()
	{
		powerMask = 0;
		allowedPowerTypes.ForEach(type => powerMask |= 1L << (int)type);
		activePowers = new();
	}

	public bool CanReceivePower(Power power)
	{
		return canReceiveAll || (powerMask & (1L << (int)power.PowerType)) != 0;
	}

	public void ActivatePower(Power power)
	{
		if(activePowers.TryGetValue(power.PowerType, out Coroutine activeCoroutine))
		{
			StopCoroutine(activeCoroutine);
			power.RemoveFrom(gameObject);
		}
		activePowers[power.PowerType] = StartCoroutine(UsePower(power));
	}

	private IEnumerator UsePower(Power power)
	{
		power.ApplyTo(gameObject);
		yield return new WaitForSecondsRealtime(power.Duration);
		power.RemoveFrom(gameObject);
		activePowers.Remove(power.PowerType);
	}
}