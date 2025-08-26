public class BombExecutionRating : BeatmapObjectExecutionRating
{
	public enum Rating
	{
		OK,
		NotGood
	}

	public Rating rating { get; set; }

	public BombExecutionRating(float time, Rating rating)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRatingType.Bomb;
		base.time = time;
		this.rating = rating;
	}
}
