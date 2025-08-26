namespace BeatmapEditor;

public class BeatData
{
	public EditorNoteData[] baseNotesData { get; private set; }

	public EditorNoteData[] upperNotesData { get; private set; }

	public EditorNoteData[] topNotesData { get; private set; }

	public EditorObstacleData[] obstaclesData { get; private set; }

	public EditorEventData[] eventsData { get; private set; }

	public BeatData()
	{
		Init();
	}

	public BeatData(BeatData other)
	{
		Init();
		for (int i = 0; i < baseNotesData.Length; i++)
		{
			baseNotesData[i] = other.baseNotesData[i];
		}
		for (int j = 0; j < upperNotesData.Length; j++)
		{
			upperNotesData[j] = other.upperNotesData[j];
		}
		for (int k = 0; k < topNotesData.Length; k++)
		{
			topNotesData[k] = other.topNotesData[k];
		}
		for (int l = 0; l < obstaclesData.Length; l++)
		{
			obstaclesData[l] = other.obstaclesData[l];
		}
		for (int m = 0; m < eventsData.Length; m++)
		{
			eventsData[m] = other.eventsData[m];
		}
	}

	private void Init()
	{
		baseNotesData = new EditorNoteData[4];
		upperNotesData = new EditorNoteData[4];
		topNotesData = new EditorNoteData[4];
		obstaclesData = new EditorObstacleData[4];
		eventsData = new EditorEventData[16];
	}

	public BeatData Clone()
	{
		return new BeatData(this);
	}

	public EditorNoteData[] NoteDataForLineLayer(NoteLineLayer noteLineLayer)
	{
		return noteLineLayer switch
		{
			NoteLineLayer.Base => baseNotesData, 
			NoteLineLayer.Upper => upperNotesData, 
			NoteLineLayer.Top => topNotesData, 
			_ => null, 
		};
	}
}
