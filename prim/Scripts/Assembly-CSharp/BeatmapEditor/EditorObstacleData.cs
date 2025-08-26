namespace BeatmapEditor;

public class EditorObstacleData
{
	public ObstacleType type { get; private set; }

	public EditorObstacleData(ObstacleType type)
	{
		this.type = type;
	}
}
