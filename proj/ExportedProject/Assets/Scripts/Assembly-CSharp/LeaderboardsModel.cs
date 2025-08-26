public class LeaderboardsModel
{
	public static string GetLeaderboardID(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode)
	{
		string text = "Unknown";
		switch (difficultyLevel.difficulty)
		{
		case LevelDifficulty.Easy:
			text = "Easy";
			break;
		case LevelDifficulty.Normal:
			text = "Normal";
			break;
		case LevelDifficulty.Hard:
			text = "Hard";
			break;
		case LevelDifficulty.Expert:
			text = "Expert";
			break;
		case LevelDifficulty.ExpertPlus:
			text = "ExpertPlus";
			break;
		}
		string text2 = "Unknown";
		switch (gameplayMode)
		{
		case GameplayMode.SoloStandard:
			text2 = "SoloStandard";
			break;
		case GameplayMode.SoloNoArrows:
			text2 = "SoloNoArrows";
			break;
		case GameplayMode.SoloOneSaber:
			text2 = "SoloOneSaber";
			break;
		case GameplayMode.PartyStandard:
			text2 = "PartyStandard";
			break;
		}
		return difficultyLevel.level.levelID + "_" + text + "_" + text2;
	}
}
