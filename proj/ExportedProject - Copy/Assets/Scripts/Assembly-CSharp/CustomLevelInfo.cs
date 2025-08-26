using System;

[Serializable]
public class CustomLevelInfo
{
	[Serializable]
	public class DifficultyLevelInfo
	{
		public string _difficulty;

		public int _difficultyRank;

		public float _noteJumpMovementSpeed;

		public string _songFilename;

		public string _beatmapFilename;
	}

	public string _songName;
	public string _songSubName;
	public string _songAuthorName;
	public float _beatsPerMinute;
	public float _previewStartTime;
	public float _previewDuration;
	public string _coverImageFilename;
	public string _environmentName;

	public DifficultyLevelInfo[] _difficultyBeatmaps;
}
