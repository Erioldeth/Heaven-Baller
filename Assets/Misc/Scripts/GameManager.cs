using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static bool isGameOver;
	public GameObject canvas;

	private void Start()
	{
		canvas.SetActive(false);
		isGameOver = false;
	}

	private void Update()
	{
		if(isGameOver)
		{
			canvas.SetActive(true);
		}
	}

	public void RestartGame()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
	}
}