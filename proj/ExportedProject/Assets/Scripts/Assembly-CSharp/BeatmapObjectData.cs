public abstract class BeatmapObjectData
{
	public BeatmapObjectType beatmapObjectType { get; protected set; }

	public float time { get; protected set; }

	public int lineIndex { get; protected set; }

	public int id { get; protected set; }

	public virtual void MirrorLineIndex(int lineCount)
	{
		lineIndex = lineCount - 1 - lineIndex;
	}

	public abstract BeatmapObjectData GetCopy();
}
