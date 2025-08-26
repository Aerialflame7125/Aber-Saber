using UnityEngine;

public class LevelCompletionResults
{
	public enum LevelEndStateType
	{
		Cleared,
		Failed
	}

	public enum Rank
	{
		E,
		D,
		C,
		B,
		A,
		S,
		SS,
		SSS
	}

	public static float kScoreMulForNoEnergyOrNoObstaclesGameplayOption = 0.7f;

	private int _maxPossibleScore;

	public BeatmapObjectExecutionRating[] beatmapObjectExecutionRatings { get; private set; }

	public MultiplierValuesRecorder.MultiplierValue[] multiplierValues { get; private set; }

	public int score { get; private set; }

	public int unmodifiedScore { get; private set; }

	public Rank rank { get; private set; }

	public bool fullCombo { get; private set; }

	public float[] saberActivityValues { get; private set; }

	public float leftSaberMovementDistance { get; private set; }

	public float rightSaberMovementDistance { get; private set; }

	public float[] handActivityValues { get; private set; }

	public float leftHandMovementDistance { get; private set; }

	public float rightHandMovementDistance { get; private set; }

	public float songDuration { get; private set; }

	public LevelEndStateType levelEndStateType { get; private set; }

	public float energy { get; private set; }

	public int goodCutsCount { get; private set; }

	public int badCutsCount { get; private set; }

	public int missedCount { get; private set; }

	public int notGoodCount { get; private set; }

	public int okCount { get; private set; }

	public int averageCutScore { get; private set; }

	public int maxCutScore { get; private set; }

	public int maxCombo { get; private set; }

	public float minDirDeviation { get; private set; }

	public float maxDirDeviation { get; private set; }

	public float averageDirDeviation { get; private set; }

	public float minTimeDeviation { get; private set; }

	public float maxTimeDeviation { get; private set; }

	public float averageTimeDeviation { get; private set; }

	public LevelCompletionResults(int levelNotesCount, BeatmapObjectExecutionRating[] beatmapObjectExecutionRatings, MultiplierValuesRecorder.MultiplierValue[] multiplierValues, int score, int maxCombo, float[] saberActivityValues, float leftSaberMovementDistance, float rightSaberMovementDistance, float[] handActivityValues, float leftHandMovementDistance, float rightHandMovementDistance, float songDuration, LevelEndStateType levelEndStateType, float energy)
	{
		this.beatmapObjectExecutionRatings = beatmapObjectExecutionRatings;
		this.multiplierValues = multiplierValues;
		this.score = score;
		unmodifiedScore = score;
		this.maxCombo = maxCombo;
		this.saberActivityValues = saberActivityValues;
		this.leftSaberMovementDistance = leftSaberMovementDistance;
		this.rightSaberMovementDistance = rightSaberMovementDistance;
		this.handActivityValues = handActivityValues;
		this.leftHandMovementDistance = leftHandMovementDistance;
		this.rightHandMovementDistance = rightHandMovementDistance;
		this.songDuration = songDuration;
		this.levelEndStateType = levelEndStateType;
		this.energy = energy;
		foreach (BeatmapObjectExecutionRating beatmapObjectExecutionRating in beatmapObjectExecutionRatings)
		{
			if (beatmapObjectExecutionRating.beatmapObjectRatingType == BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Note)
			{
				NoteExecutionRating noteExecutionRating = (NoteExecutionRating)beatmapObjectExecutionRating;
				if (noteExecutionRating.rating == NoteExecutionRating.Rating.GoodCut)
				{
					goodCutsCount++;
					float num = Mathf.Abs(noteExecutionRating.cutDirDeviation);
					float num2 = Mathf.Abs(noteExecutionRating.cutTimeDeviation);
					if (goodCutsCount == 1)
					{
						float num4 = (averageDirDeviation = num);
						num4 = (maxDirDeviation = num4);
						minDirDeviation = num4;
						num4 = (averageTimeDeviation = num2);
						num4 = (maxTimeDeviation = num4);
						minTimeDeviation = num4;
					}
					else
					{
						if (minDirDeviation > num)
						{
							minDirDeviation = num;
						}
						if (maxDirDeviation < num)
						{
							maxDirDeviation = num;
						}
						averageDirDeviation += num;
						if (minTimeDeviation > num2)
						{
							minTimeDeviation = num2;
						}
						if (maxTimeDeviation < num2)
						{
							maxTimeDeviation = num2;
						}
						averageTimeDeviation += num2;
					}
					if (maxCutScore < noteExecutionRating.cutScore)
					{
						maxCutScore = noteExecutionRating.cutScore;
					}
					averageCutScore += noteExecutionRating.cutScore;
				}
				else if (noteExecutionRating.rating == NoteExecutionRating.Rating.BadCut)
				{
					badCutsCount++;
				}
				else if (noteExecutionRating.rating == NoteExecutionRating.Rating.Missed)
				{
					missedCount++;
				}
			}
			else if (beatmapObjectExecutionRating.beatmapObjectRatingType == BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Bomb)
			{
				BombExecutionRating bombExecutionRating = (BombExecutionRating)beatmapObjectExecutionRating;
				if (bombExecutionRating.rating == BombExecutionRating.Rating.OK)
				{
					okCount++;
				}
				else
				{
					notGoodCount++;
				}
			}
			else if (beatmapObjectExecutionRating.beatmapObjectRatingType == BeatmapObjectExecutionRating.BeatmapObjectExecutionRatingType.Obstacle)
			{
				ObstacleExecutionRating obstacleExecutionRating = (ObstacleExecutionRating)beatmapObjectExecutionRating;
				if (obstacleExecutionRating.rating == ObstacleExecutionRating.Rating.OK)
				{
					okCount++;
				}
				else
				{
					notGoodCount++;
				}
			}
		}
		if (goodCutsCount > 0)
		{
			averageCutScore /= goodCutsCount;
			averageDirDeviation /= goodCutsCount;
			averageTimeDeviation /= goodCutsCount;
		}
		_maxPossibleScore = ScoreController.MaxScoreForNumberOfNotes(levelNotesCount);
		rank = GetRankForScore(score, _maxPossibleScore);
		fullCombo = goodCutsCount == levelNotesCount && badCutsCount == 0 && notGoodCount == 0;
	}

	private static Rank GetRankForScore(int score, int maxPossibleScore)
	{
		float num = (float)score / (float)maxPossibleScore;
		if (score == maxPossibleScore)
		{
			return Rank.SSS;
		}
		if (num > 0.9f)
		{
			return Rank.SS;
		}
		if (num > 0.8f)
		{
			return Rank.S;
		}
		if (num > 0.65f)
		{
			return Rank.A;
		}
		if (num > 0.5f)
		{
			return Rank.B;
		}
		if (num > 0.35f)
		{
			return Rank.C;
		}
		if (num > 0.2f)
		{
			return Rank.D;
		}
		return Rank.E;
	}

	public static string GetRankName(Rank rank)
	{
		return rank switch
		{
			Rank.E => "E", 
			Rank.D => "D", 
			Rank.C => "C", 
			Rank.B => "B", 
			Rank.A => "A", 
			Rank.S => "S", 
			Rank.SS => "SS", 
			Rank.SSS => "SSS", 
			_ => "XXX", 
		};
	}

	public static LevelCompletionResults TestData(int score, LevelEndStateType levelEndStateType)
	{
		return new LevelCompletionResults(500, new BeatmapObjectExecutionRating[3]
		{
			new NoteExecutionRating(0f, NoteExecutionRating.Rating.GoodCut, 123, 0f, 0f),
			new NoteExecutionRating(1f, NoteExecutionRating.Rating.GoodCut, 321, 0f, 0f),
			new NoteExecutionRating(2f, NoteExecutionRating.Rating.GoodCut, 213, 0f, 0f)
		}, new MultiplierValuesRecorder.MultiplierValue[3]
		{
			new MultiplierValuesRecorder.MultiplierValue(1, 0f),
			new MultiplierValuesRecorder.MultiplierValue(4, 1f),
			new MultiplierValuesRecorder.MultiplierValue(2, 2f)
		}, score, 1, new float[4] { 0f, 0f, 1f, 1f }, 123f, 456f, new float[4] { 0f, 1f, 1f, 0f }, 234f, 567f, 60f, levelEndStateType, 0.5f);
	}

	public void ModifyScore(int score)
	{
		this.score = score;
		rank = GetRankForScore(score, _maxPossibleScore);
	}
}
