using UnityEngine;

public class ActivateOnVisible : MonoBehaviour
{
	public GameObject[] _gameObjects;

	private void Awake()
	{
		for (int i = 0; i < _gameObjects.Length; i++)
		{
			_gameObjects[i].SetActive(value: false);
		}
	}

	private void OnBecameVisible()
	{
		for (int i = 0; i < _gameObjects.Length; i++)
		{
			_gameObjects[i].SetActive(value: true);
		}
	}

	private void OnBecameInvisible()
	{
		for (int i = 0; i < _gameObjects.Length; i++)
		{
			_gameObjects[i].SetActive(value: false);
		}
	}
}
