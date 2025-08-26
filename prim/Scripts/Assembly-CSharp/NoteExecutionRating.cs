public class NoteExecutionRating : BeatmapObjectExecutionRating
{
	public enum Rating
	{
		GoodCut,
		Missed,
		BadCut
	}

	public Rating rating { get; set; }

	public int cutScore { get; set; }

	public float cutTimeDeviation { get; set; }

	public float cutDirDeviation { get; set; }

	public NoteExecutionRating(float time, Rating rating, int cutScore, float cutTimeDeviation, float cutDirDeviation)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRatingType.Note;
		base.time = time;
		this.rating = rating;
		this.cutScore = cutScore;
		this.cutTimeDeviation = cutTimeDeviation;
		this.cutDirDeviation = cutDirDeviation;
	}
}
