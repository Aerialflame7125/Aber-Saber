public class ObstacleExecutionRating : BeatmapObjectExecutionRating
{
	public enum Rating
	{
		OK = 0,
		NotGood = 1
	}

	public Rating rating { get; set; }

	public ObstacleExecutionRating(float time, Rating rating)
	{
		base.beatmapObjectRatingType = BeatmapObjectExecutionRatingType.Obstacle;
		base.time = time;
		this.rating = rating;
	}
}
