using System;
using System.IO;
using UnityEngine;

public class CustomLevelsModelSO : ScriptableObject
{
	public enum ImportLevelResult
	{
		ErrorCantOpenArchive,
		ErrorInvalidData,
		ErrorMissingSongFile,
		Success
	}

	public enum ExtractStandardLevelAndGetSongFilenameResult
	{
		Error,
		Success
	}

	public enum ImportStandardLevelResult
	{
		Error,
		Success
	}

	public static string kStandardLevelInfoFileName = "Info.dat";

	private const string kCustomLevelsDirectoryName = "CustomLevels";

	public static string customLevelsDirectoryPath
	{
		get
		{
			string dataPath = Application.dataPath;
			return Path.Combine(dataPath, "CustomLevels");
		}
	}

	public static string GetDefaultNameForCustomLevel(string songName, string songAuthorName, string levelAuthorName)
	{
		return songAuthorName + " - " + songName + " (" + levelAuthorName + ")";
	}

	public static string GetDefaultDirectoryPathForCustomLevel(string songName, string songAuthorName, string levelAuthorName)
	{
		string defaultNameForCustomLevel = GetDefaultNameForCustomLevel(songName, songAuthorName, levelAuthorName);
		string dirName = Path.Combine(customLevelsDirectoryPath, defaultNameForCustomLevel);
		return FileHelpers.GetUniqueDirectoryNameByAppendingNumber(dirName);
	}

	public void ImportCustomLevelFromDirectoryAsync(string levelDir, Action<string> callback)
	{
		Action<bool, string> callback2 = delegate(bool success, string newLevelDir)
		{
			if (!success)
			{
				callback(null);
			}
			else
			{
				CustomLevelInfoWrapper customLevelInfoWrapper = CustomLevelInfoWrapperLoader.LoadCustomLevelInfoWrapper(newLevelDir);
				if (customLevelInfoWrapper != null)
				{
					callback(customLevelInfoWrapper.levelID);
				}
				else
				{
					callback(null);
				}
			}
		};
		CustomLevelImporter.ImportSongFromDirectoryAsync(levelDir, customLevelsDirectoryPath, callback2);
	}

	public void LoadCustomLevelsAsync(Action<CustomLevelInfoWrapper[]> callback)
	{
		CustomLevelInfoWrapperLoader.LoadCustomLevelInfoWrappersAsync(customLevelsDirectoryPath, callback);
	}

	public static void ExtractStandardLevelAndGetSongFilename(string levelFilenamePath, string dstDirPath, Action<ExtractStandardLevelAndGetSongFilenameResult, string> finishCallback)
	{
		ExtractStandardLevelAndGetSongFilenameResult result = ExtractStandardLevelAndGetSongFilenameResult.Error;
		string songFilename = null;
		Action job = delegate
		{
			try
			{
				if (File.Exists(levelFilenamePath))
				{
					FileCompressionHelper.ExtractZipToDirectory(levelFilenamePath, dstDirPath);
					string path = Path.Combine(dstDirPath, kStandardLevelInfoFileName);
					if (File.Exists(path))
					{
						StandardLevelSaveData standardLevelSaveData = null;
						string stringData = File.ReadAllText(path);
						standardLevelSaveData = StandardLevelSaveData.DeserializeFromJSONString(stringData);
						if (standardLevelSaveData != null)
						{
							result = ExtractStandardLevelAndGetSongFilenameResult.Success;
							songFilename = standardLevelSaveData.songFilename;
						}
					}
				}
			}
			catch
			{
			}
		};
		Action finnish = delegate
		{
			if (finishCallback != null)
			{
				finishCallback(result, songFilename);
			}
		};
		HMTask hMTask = new HMTask(job, finnish);
		hMTask.Run();
	}

	public static void ImportStandardLevel(string scrDirPath, string songFilenamePath, Action<ImportStandardLevelResult> finishCallback)
	{
		ImportStandardLevelResult result = ImportStandardLevelResult.Error;
		StandardLevelSaveData standardLevelSaveData = null;
		string customLevelsDirectoryPath = CustomLevelsModelSO.customLevelsDirectoryPath;
		Action job = delegate
		{
			string text = null;
			try
			{
				string path = Path.Combine(scrDirPath, kStandardLevelInfoFileName);
				if (File.Exists(path))
				{
					string stringData = File.ReadAllText(path);
					standardLevelSaveData = StandardLevelSaveData.DeserializeFromJSONString(stringData);
					if (standardLevelSaveData != null)
					{
						string fileName = Path.GetFileName(songFilenamePath);
						if (fileName != standardLevelSaveData.songFilename)
						{
							standardLevelSaveData.SetSongFilename(fileName);
						}
						if (standardLevelSaveData.difficultyBeatmaps.Length != 0)
						{
							BeatmapSaveData[] array = new BeatmapSaveData[standardLevelSaveData.difficultyBeatmaps.Length];
							for (int i = 0; i < array.Length; i++)
							{
								StandardLevelSaveData.DifficultyBeatmap difficultyBeatmap = standardLevelSaveData.difficultyBeatmaps[i];
								string path2 = Path.Combine(scrDirPath, difficultyBeatmap.beatmapFilename);
								if (!File.Exists(path2))
								{
									return;
								}
								BeatmapSaveData beatmapSaveData = null;
								stringData = File.ReadAllText(path2);
								beatmapSaveData = BeatmapSaveData.DeserializeFromJSONString(stringData);
								if (beatmapSaveData == null)
								{
									return;
								}
								array[i] = beatmapSaveData;
							}
							string defaultNameForCustomLevel = GetDefaultNameForCustomLevel(standardLevelSaveData.songName, standardLevelSaveData.songAuthorName, standardLevelSaveData.levelAuthorName);
							text = Path.Combine(customLevelsDirectoryPath, defaultNameForCustomLevel);
							text = FileHelpers.GetUniqueDirectoryNameByAppendingNumber(text);
							Directory.CreateDirectory(text);
							string path3 = Path.Combine(text, kStandardLevelInfoFileName);
							stringData = standardLevelSaveData.SerializeToJSONString();
							File.WriteAllText(path3, stringData);
							for (int j = 0; j < array.Length; j++)
							{
								StandardLevelSaveData.DifficultyBeatmap difficultyBeatmap2 = standardLevelSaveData.difficultyBeatmaps[j];
								path3 = Path.Combine(text, difficultyBeatmap2.beatmapFilename);
								stringData = array[j].SerializeToJSONString();
								File.WriteAllText(path3, stringData);
							}
							string text2 = Path.Combine(scrDirPath, standardLevelSaveData.coverImageFilename);
							path3 = Path.Combine(text, standardLevelSaveData.coverImageFilename);
							if (File.Exists(text2))
							{
								File.Copy(text2, path3);
							}
							path3 = Path.Combine(text, standardLevelSaveData.songFilename);
							File.Copy(songFilenamePath, path3);
							result = ImportStandardLevelResult.Success;
						}
					}
				}
			}
			catch
			{
				if (text != null)
				{
					try
					{
						Directory.Delete(text);
						return;
					}
					catch
					{
						return;
					}
				}
			}
		};
		Action finnish = delegate
		{
			if (finishCallback != null)
			{
				finishCallback(result);
			}
		};
		HMTask hMTask = new HMTask(job, finnish);
		hMTask.Run();
	}
}
