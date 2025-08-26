public class BeatmapObjectExecutionRating
{
	public enum BeatmapObjectExecutionRatingType
	{
		Note,
		Bomb,
		Obstacle
	}

	public BeatmapObjectExecutionRatingType beatmapObjectRatingType { get; set; }

	public float time { get; set; }
}
