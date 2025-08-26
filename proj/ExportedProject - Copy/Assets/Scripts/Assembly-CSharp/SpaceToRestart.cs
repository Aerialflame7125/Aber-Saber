using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceToRestart : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
