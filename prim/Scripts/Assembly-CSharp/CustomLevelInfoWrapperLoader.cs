using System;
using System.Collections.Generic;
using System.IO;

public static class CustomLevelInfoWrapperLoader
{
	public static void LoadCustomLevelInfoWrappersAsync(string customLevelDir, Action<CustomLevelInfoWrapper[]> callback)
	{
		List<CustomLevelInfoWrapper> customLevels = new List<CustomLevelInfoWrapper>();
		DirectoryInfo directoryInfo = new DirectoryInfo(customLevelDir);
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		HMTask hMTask = new HMTask(delegate
		{
			DirectoryInfo[] array = directories;
			foreach (DirectoryInfo directoryInfo2 in array)
			{
				CustomLevelInfoWrapper customLevelInfoWrapper = LoadCustomLevelInfoWrapper(directoryInfo2.FullName);
				if (customLevelInfoWrapper != null)
				{
					customLevels.Add(customLevelInfoWrapper);
				}
			}
		}, delegate
		{
			callback(customLevels.ToArray());
		});
		hMTask.Run();
	}

	public static CustomLevelInfoWrapper LoadCustomLevelInfoWrapper(string path)
	{
		try
		{
			CustomLevelInfo customLevelInfo = CustomLevelLoader.LoadCustomLevelInfo(path);
			string fullPath = Path.GetFullPath(path);
			return new CustomLevelInfoWrapper(fullPath, customLevelInfo);
		}
		catch
		{
			return null;
		}
	}
}
