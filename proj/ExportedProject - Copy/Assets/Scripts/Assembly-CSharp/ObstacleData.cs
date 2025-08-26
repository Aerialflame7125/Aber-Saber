public class ObstacleData : BeatmapObjectData
{
	public ObstacleType obstacleType { get; private set; }

	public float duration { get; private set; }

	public int width { get; private set; }

	public ObstacleData(int id, float time, int lineIndex, ObstacleType obstacleType, float duration, int width)
	{
		base.beatmapObjectType = BeatmapObjectType.Obstacle;
		base.id = id;
		base.time = time;
		base.lineIndex = lineIndex;
		this.obstacleType = obstacleType;
		this.duration = duration;
		this.width = width;
	}

	public void UpdateDuration(float duration)
	{
		this.duration = duration;
	}

	public override BeatmapObjectData GetCopy()
	{
		return new ObstacleData(base.id, base.time, base.lineIndex, obstacleType, duration, width);
	}

	public override void MirrorLineIndex(int lineCount)
	{
		base.lineIndex = lineCount - width - base.lineIndex;
	}
}
