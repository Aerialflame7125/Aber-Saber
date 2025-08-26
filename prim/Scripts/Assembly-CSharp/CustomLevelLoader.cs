using System.IO;

public static class CustomLevelLoader
{
	public static CustomLevelInfo LoadCustomLevelInfo(string path, string jsonFile = "info.json")
	{
		string filePath = Path.Combine(path, jsonFile);
		return FileHelpers.LoadFromJSONFile<CustomLevelInfo>(filePath);
	}
}
