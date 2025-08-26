namespace BeatmapEditor;

public interface IBeatmapEditorEventType
{
	EventDrawStyleSO.SubEventDrawStyle GetSelectedEventType(string eventId);
}
