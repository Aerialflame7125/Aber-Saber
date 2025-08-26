using System;
using System.IO;

public static class CustomLevelImporter
{
	public static void ImportSongFromDirectoryAsync(string sourceDir, string customLevelsDir, Action<bool, string> callback)
	{
		string targetPath = string.Empty;
		bool success = false;
		Action job = delegate
		{
			string name = new DirectoryInfo(sourceDir).Name;
			targetPath = Path.Combine(customLevelsDir, name);
			targetPath = FileHelpers.GetUniqueDirectoryNameByAppendingNumber(targetPath);
			success = CopyDir(sourceDir, targetPath);
		};
		Action finnish = delegate
		{
			success &= Directory.Exists(targetPath);
			callback(success, targetPath);
		};
		HMTask hMTask = new HMTask(job, finnish);
		hMTask.Run();
	}

	private static bool CopyDir(string sourcePath, string targetPath)
	{
		try
		{
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			if (Directory.Exists(sourcePath))
			{
				string[] files = Directory.GetFiles(sourcePath);
				string[] array = files;
				foreach (string text in array)
				{
					string fileName = Path.GetFileName(text);
					string destFileName = Path.Combine(targetPath, fileName);
					File.Copy(text, destFileName, true);
				}
			}
			return true;
		}
		catch
		{
			return false;
		}
	}
}
