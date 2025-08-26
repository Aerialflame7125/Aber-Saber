public interface IStandardLevelDifficultyBeatmap
{
	IStandardLevel level { get; }

	LevelDifficulty difficulty { get; }

	int difficultyRank { get; }

	float noteJumpMovementSpeed { get; }

	BeatmapData beatmapData { get; }
}
