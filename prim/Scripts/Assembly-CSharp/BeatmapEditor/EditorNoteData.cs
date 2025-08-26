namespace BeatmapEditor;

public class EditorNoteData
{
	public NoteType type { get; private set; }

	public NoteCutDirection cutCirection { get; private set; }

	public EditorNoteData(NoteType type, NoteCutDirection cutDirection)
	{
		this.type = type;
		cutCirection = cutDirection;
	}
}
