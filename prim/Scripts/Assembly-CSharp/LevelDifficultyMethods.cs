public static class LevelDifficultyMethods
{
	private const string kDifficultyEasyName = "Easy";

	private const string kDifficultyNormalName = "Normal";

	private const string kDifficultyHardName = "Hard";

	private const string kDifficultyExpertName = "Expert";

	private const string kDifficultyExpertPlusName = "Expert+";

	private const string kDifficultyExpertPlusModersName = "ExpertPlus";

	private const string kDifficultyExpertUnknownName = "Unknown";

	public static string Name(this LevelDifficulty difficulty)
	{
		return difficulty switch
		{
			LevelDifficulty.Easy => "Easy", 
			LevelDifficulty.Normal => "Normal", 
			LevelDifficulty.Hard => "Hard", 
			LevelDifficulty.Expert => "Expert", 
			LevelDifficulty.ExpertPlus => "Expert+", 
			_ => "Unknown", 
		};
	}

	public static int DefaultRating(this LevelDifficulty difficulty)
	{
		return difficulty switch
		{
			LevelDifficulty.Easy => 1, 
			LevelDifficulty.Normal => 3, 
			LevelDifficulty.Hard => 5, 
			LevelDifficulty.Expert => 7, 
			LevelDifficulty.ExpertPlus => 9, 
			_ => 5, 
		};
	}

	public static float NoteJumpMovementSpeed(this LevelDifficulty difficulty)
	{
		return difficulty switch
		{
			LevelDifficulty.Easy => 10f, 
			LevelDifficulty.Normal => 10f, 
			LevelDifficulty.Hard => 10f, 
			LevelDifficulty.Expert => 12f, 
			LevelDifficulty.ExpertPlus => 12f, 
			_ => 5f, 
		};
	}

	public static bool LevelDifficultyFromName(this string name, out LevelDifficulty levelDifficulty)
	{
		switch (name)
		{
		case "Easy":
			levelDifficulty = LevelDifficulty.Easy;
			return true;
		case "Normal":
			levelDifficulty = LevelDifficulty.Normal;
			return true;
		case "Hard":
			levelDifficulty = LevelDifficulty.Hard;
			return true;
		case "Expert":
			levelDifficulty = LevelDifficulty.Expert;
			return true;
		case "Expert+":
		case "ExpertPlus":
			levelDifficulty = LevelDifficulty.ExpertPlus;
			return true;
		default:
			levelDifficulty = LevelDifficulty.Normal;
			return false;
		}
	}
}
