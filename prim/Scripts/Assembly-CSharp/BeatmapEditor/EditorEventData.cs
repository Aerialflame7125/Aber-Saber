namespace BeatmapEditor;

public class EditorEventData
{
	public readonly int value;

	public readonly bool isPreviousValidValue;

	public EditorEventData(int value, bool isPreviousValidValue)
	{
		this.value = value;
		this.isPreviousValidValue = isPreviousValidValue;
	}
}
