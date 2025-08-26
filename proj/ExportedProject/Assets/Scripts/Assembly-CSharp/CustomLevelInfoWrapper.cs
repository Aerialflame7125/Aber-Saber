using System.IO;

public class CustomLevelInfoWrapper
{
	public string levelID { get; private set; }

	public string path { get; private set; }

	public CustomLevelInfo customLevelInfo { get; private set; }

	public string coverImageFilePath { get; private set; }

	public string audioPath { get; private set; }

	public CustomLevelInfoWrapper(string path, CustomLevelInfo customLevelInfo)
	{
		this.path = Path.GetFullPath(path);
		levelID = ComputeLevelID();
		this.customLevelInfo = customLevelInfo;
		coverImageFilePath = Path.Combine(this.path, customLevelInfo._coverImageFilename);
		audioPath = GetAudioPreviewPath();
	}

	private string GetAudioPreviewPath()
	{
		if (customLevelInfo._difficultyBeatmaps.Length > 0)
		{
			return Path.Combine(path, customLevelInfo._difficultyBeatmaps[0]._songFilename);
		}
		return null;
	}

	private string ComputeLevelID()
	{
		return path;
	}
}
