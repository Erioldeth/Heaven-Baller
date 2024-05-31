using System.Collections;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] powerPrefabs;
	[SerializeField] private int maxSpawn;
	private bool canSpawn;
	private int count;

	private void OnEnable()
	{
		Power.UpdateSpawner += OnPowerActivated;
	}

	private void Start()
	{
		Invoke(nameof(BeginSpawning), 3);
	}

	private void Update()
	{
		if(GameManager.isGameOver)
		{
			return;
		}

		if(canSpawn && count < maxSpawn)
		{
			StartCoroutine(SpawnPower());
		}
	}

	private void OnDisable()
	{
		Power.UpdateSpawner -= OnPowerActivated;
	}

	private void OnPowerActivated()
	{
		--count;
	}

	private void BeginSpawning()
	{
		canSpawn = true;
	}

	private IEnumerator SpawnPower()
	{
		canSpawn = false;
		++count;
		GameObject prefab = powerPrefabs[Random.Range(0, powerPrefabs.Length)];
		Instantiate(prefab, GetRandomPosition(prefab.transform.position.y), prefab.transform.rotation);
		yield return new WaitForSecondsRealtime(Random.Range(5f, 10f));
		canSpawn = true;
	}

	private Vector3 GetRandomPosition(float height)
	{
		return new(Random.Range(-9, 9), height, Random.Range(-9, 9));
	}
}