using UnityEngine;

public interface IStandardLevel
{
	string levelID { get; }

	string songName { get; }

	string songSubName { get; }

	string songAuthorName { get; }

	AudioClip audioClip { get; }

	float beatsPerMinute { get; }

	float songTimeOffset { get; }

	float shuffle { get; }

	float shufflePeriod { get; }

	float previewStartTime { get; }

	float previewDuration { get; }

	Sprite coverImage { get; }

	IStandardLevelDifficultyBeatmap[] difficultyBeatmaps { get; }

	SceneInfo environmentSceneInfo { get; }

	IStandardLevelDifficultyBeatmap GetDifficultyLevel(LevelDifficulty difficulty);
}
