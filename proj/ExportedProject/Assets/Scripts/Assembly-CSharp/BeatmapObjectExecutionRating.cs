public class BeatmapObjectExecutionRating
{
	public enum BeatmapObjectExecutionRatingType
	{
		Note = 0,
		Bomb = 1,
		Obstacle = 2
	}

	public BeatmapObjectExecutionRatingType beatmapObjectRatingType { get; set; }

	public float time { get; set; }
}
