public class LongNoteData : BeatmapObjectData
{
	public NoteLineLayer noteLineLayer { get; private set; }

	public NoteType noteType { get; private set; }

	public float duration { get; private set; }

	public LongNoteData(int id, float time, int lineIndex, NoteLineLayer noteLineLayer, NoteType noteType, float duration)
	{
		base.beatmapObjectType = BeatmapObjectType.LongNote;
		base.id = id;
		base.time = time;
		this.duration = duration;
		base.lineIndex = lineIndex;
		this.noteLineLayer = noteLineLayer;
		this.noteType = noteType;
	}

	public void UpdateDuration(float duration)
	{
		this.duration = duration;
	}

	public override BeatmapObjectData GetCopy()
	{
		return new LongNoteData(base.id, base.time, base.lineIndex, noteLineLayer, noteType, duration);
	}
}
