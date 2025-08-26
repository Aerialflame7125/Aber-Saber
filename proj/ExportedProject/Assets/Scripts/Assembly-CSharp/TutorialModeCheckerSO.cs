using UnityEngine;

public class TutorialModeCheckerSO : ScriptableObject
{
	private const string kTutorialModeEnabledKey = "kTutorialModeEnabledKey";

	private const int kTutorialModeEnabledValue = 2;

	public bool tutorialModeEnabled
	{
		get
		{
			return PlayerPrefs.GetInt("kTutorialModeEnabledKey") == 2;
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("kTutorialModeEnabledKey", 2);
				EnableTutorialMode();
			}
			else
			{
				PlayerPrefs.DeleteKey("kTutorialModeEnabledKey");
				DisableTutorialMode();
			}
		}
	}

	private void EnableTutorialMode()
	{
	}

	private void DisableTutorialMode()
	{
	}

	private void OnEnable()
	{
		if (tutorialModeEnabled)
		{
			EnableTutorialMode();
		}
	}
}
