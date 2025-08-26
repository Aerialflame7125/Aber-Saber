using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptableObjectTestUser : MonoBehaviour
{
	[SerializeField]
	private ScriptableObjectTest _scriptableObjectTest;

	public void SwitchScene(string sceneName)
	{
		_scriptableObjectTest.Foo();
		SceneManager.LoadScene(sceneName);
	}
}
