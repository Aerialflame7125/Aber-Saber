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
		switch (difficulty)
		{
		case LevelDifficulty.Easy:
			return "Easy";
		case LevelDifficulty.Normal:
			return "Normal";
		case LevelDifficulty.Hard:
			return "Hard";
		case LevelDifficulty.Expert:
			return "Expert";
		case LevelDifficulty.ExpertPlus:
			return "Expert+";
		default:
			return "Unknown";
		}
	}

	public static int DefaultRating(this LevelDifficulty difficulty)
	{
		switch (difficulty)
		{
		case LevelDifficulty.Easy:
			return 1;
		case LevelDifficulty.Normal:
			return 3;
		case LevelDifficulty.Hard:
			return 5;
		case LevelDifficulty.Expert:
			return 7;
		case LevelDifficulty.ExpertPlus:
			return 9;
		default:
			return 5;
		}
	}

	public static float NoteJumpMovementSpeed(this LevelDifficulty difficulty)
	{
		switch (difficulty)
		{
		case LevelDifficulty.Easy:
			return 10f;
		case LevelDifficulty.Normal:
			return 10f;
		case LevelDifficulty.Hard:
			return 10f;
		case LevelDifficulty.Expert:
			return 12f;
		case LevelDifficulty.ExpertPlus:
			return 12f;
		default:
			return 5f;
		}
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
