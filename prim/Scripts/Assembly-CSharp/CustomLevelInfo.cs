using System;

[Serializable]
public class CustomLevelInfo
{
	[Serializable]
	public class DifficultyLevelInfo
	{
		public string difficulty;

		public int difficultyRank;

		public float noteJumpMovementSpeed;

		public string audioPath;

		public string jsonPath;
	}

	public string songName;

	public string songSubName;

	public string authorName;

	public float beatsPerMinute;

	public float previewStartTime;

	public float previewDuration;

	public string coverImagePath;

	public string environmentName;

	public DifficultyLevelInfo[] difficultyLevels;
}
