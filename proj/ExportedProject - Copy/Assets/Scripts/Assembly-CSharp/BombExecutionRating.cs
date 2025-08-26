public class BombExecutionRating : BeatmapObjectExecutionRating
{
	public enum Rating
	{
		OK = 0,
		NotGood = 1
	}

	public Rating rating { get; set; }

	public BombExecutionRating(float time, Rating rating)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRatingType.Bomb;
		base.time = time;
		this.rating = rating;
	}
}
