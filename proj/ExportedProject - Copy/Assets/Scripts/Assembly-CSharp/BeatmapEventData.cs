public class BeatmapEventData
{
	public BeatmapEventType type { get; protected set; }

	public float time { get; protected set; }

	public int value { get; protected set; }

	public BeatmapEventData(float time, BeatmapEventType type, int value)
	{
		this.time = time;
		this.type = type;
		this.value = value;
	}

	public BeatmapEventData GetCopy()
	{
		return new BeatmapEventData(time, type, value);
	}
}
