using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] enemyPrefabs;
	[SerializeField] private int maxSpawn;
	private int currentSpawn = 1;
	private int count = 1;

	private void OnEnable()
	{
		EnemyAI.UpdateSpawner += OnEnemyDeath;
	}

	private void Start()
	{
		Invoke(nameof(SpawnWave), 1.5f);
	}

	private void Update()
	{
		if(GameManager.isGameOver)
		{
			return;
		}

		if(count == 0)
		{
			count = UpdateSpawn();
			SpawnWave();
		}
	}

	private void OnDisable()
	{
		EnemyAI.UpdateSpawner -= OnEnemyDeath;
	}

	private void OnEnemyDeath()
	{
		--count;
	}

	private int UpdateSpawn()
	{
		if(currentSpawn == maxSpawn)
			return maxSpawn;

		if(Random.Range(0f, currentSpawn) <= 1)
		{
			++currentSpawn;
		}

		return currentSpawn;
	}

	private void SpawnWave()
	{
		for(int i = 0; i < count; ++i)
		{
			GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
			Instantiate(prefab, GetRandomPosition(prefab.transform.position.y), prefab.transform.rotation);
		}
	}

	private Vector3 GetRandomPosition(float height)
	{
		return new(Random.Range(-9, 9), height, Random.Range(-9, 9));
	}
}