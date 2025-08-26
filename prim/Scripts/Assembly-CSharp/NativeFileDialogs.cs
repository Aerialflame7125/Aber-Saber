using System.IO;
using SFB;
using UnityEngine;

public class NativeFileDialogs
{
	private const string kLastUsedDirectoryPathKey = "NativeFileDialogs.LastUsedFilePath";

	private static string lastUsedDirectory => PlayerPrefs.GetString("NativeFileDialogs.LastUsedFilePath", null);

	private static void SetLastUsedDirectory(string path)
	{
		string text = null;
		if (text != null)
		{
			try
			{
				text = Path.GetDirectoryName(path);
			}
			catch
			{
			}
		}
		PlayerPrefs.SetString("NativeFileDialogs.LastUsedFilePath", text);
	}

	public static string SaveFileDialog(string title, string defaultName, string extension, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = lastUsedDirectory;
		}
		string text = StandaloneFileBrowser.SaveFilePanel(title, initialDirectory, defaultName, extension);
		if (text == null || text == string.Empty)
		{
			return null;
		}
		SetLastUsedDirectory(text);
		return text;
	}

	public static string[] OpenFileDialogMultiselect(string title, string extension, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = lastUsedDirectory;
		}
		string[] array = StandaloneFileBrowser.OpenFilePanel(title, initialDirectory, extension, multiselect: true);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		SetLastUsedDirectory(array[0]);
		return array;
	}

	public static string OpenFileDialog(string title, string extension, string initialDirectory)
	{
		ExtensionFilter[] extensions = (string.IsNullOrEmpty(extension) ? null : new ExtensionFilter[1]
		{
			new ExtensionFilter(string.Empty, extension)
		});
		return OpenFileDialog(title, extensions, initialDirectory);
	}

	public static string OpenFileDialog(string title, ExtensionFilter[] extensions, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = lastUsedDirectory;
		}
		string[] array = StandaloneFileBrowser.OpenFilePanel(title, initialDirectory, extensions, multiselect: false);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		SetLastUsedDirectory(array[0]);
		return array[0];
	}

	public static string OpenDirectoryDialog(string title, string initialDirectory)
	{
		if (initialDirectory == null)
		{
			initialDirectory = lastUsedDirectory;
		}
		string[] array = StandaloneFileBrowser.OpenFolderPanel(title, initialDirectory, multiselect: false);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		SetLastUsedDirectory(array[0]);
		return array[0];
	}
}
