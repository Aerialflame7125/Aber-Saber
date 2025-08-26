using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileHelpers
{
	public static string GetEscapedURLForFilePath(string filePath)
	{
		return "file:///" + WWW.EscapeURL(filePath);
	}

	public static string GetUniqueDirectoryNameByAppendingNumber(string dirName)
	{
		int num = 0;
		string text = dirName;
		while (Directory.Exists(text))
		{
			num++;
			text = dirName + " " + num;
		}
		return text;
	}

	public static string[] GetFilePaths(string directoryPath, HashSet<string> extensions)
	{
		if (!Directory.Exists(directoryPath))
		{
			return null;
		}
		string[] files = Directory.GetFiles(directoryPath);
		List<string> list = new List<string>();
		string[] array = files;
		foreach (string text in array)
		{
			string item = Path.GetExtension(text).Replace(".", string.Empty);
			if (extensions.Contains(item))
			{
				list.Add(text);
			}
		}
		return list.ToArray();
	}

	public static string[] GetFileNamesFromFilePaths(string[] filePaths)
	{
		List<string> list = new List<string>();
		foreach (string path in filePaths)
		{
			list.Add(Path.GetFileName(path));
		}
		return list.ToArray();
	}

	public static void SaveToJSONFile(object obj, string filePath, string tempFilePath, string backupFilePath)
	{
		string contents = JsonUtility.ToJson(obj);
		if (File.Exists(filePath))
		{
			File.WriteAllText(tempFilePath, contents);
			File.Replace(tempFilePath, filePath, backupFilePath);
		}
		else
		{
			File.WriteAllText(filePath, contents);
		}
	}

	public static T LoadFromJSONFile<T>(string filePath, string backupFilePath = null) where T : class
	{
		T val = (T)null;
		string text = null;
		if (File.Exists(filePath))
		{
			text = File.ReadAllText(filePath);
			try
			{
				val = JsonUtility.FromJson<T>(text);
			}
			catch
			{
				val = (T)null;
			}
		}
		if (val == null && backupFilePath != null && File.Exists(backupFilePath))
		{
			text = File.ReadAllText(backupFilePath);
			try
			{
				val = JsonUtility.FromJson<T>(text);
			}
			catch
			{
				val = (T)null;
			}
		}
		return val;
	}
}
