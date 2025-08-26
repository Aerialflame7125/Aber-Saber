public class ObstacleExecutionRating : BeatmapObjectExecutionRating
{
	public enum Rating
	{
		OK,
		NotGood
	}

	public Rating rating { get; set; }

	public ObstacleExecutionRating(float time, Rating rating)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRatingType.Obstacle;
		base.time = time;
		this.rating = rating;
	}
}
