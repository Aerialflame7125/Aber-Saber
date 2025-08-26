using UnityEngine.Analytics;

public static class Analytics
{
	public static bool Enabled { get; set; }

	public static bool GameStart()
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	public static bool GameOver()
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	public static bool LevelStart(string levelID, LevelDifficulty difficulty, GameplayMode gameplayMode)
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	public static bool LevelEnd(string levelID, LevelCompletionResults levelCompletionResults, LevelDifficulty difficulty, GameplayMode gameplayMode)
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	public static bool TutorialStart()
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	public static bool TutorialComplete()
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	public static bool TutorialSkip()
	{
		if (!Enabled)
		{
			return false;
		}
		return false;
	}

	private static bool resultOK(AnalyticsResult result)
	{
		return result == AnalyticsResult.Ok;
	}
}
