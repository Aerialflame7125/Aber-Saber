using UnityEngine;

public class ResetPlayerPrefsOnButton : MonoBehaviour
{
	[SerializeField]
	private KeyCode _keyCode = KeyCode.F9;

	private void Update()
	{
		if (Input.GetKeyDown(_keyCode))
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
