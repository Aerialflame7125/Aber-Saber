using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BookmarksFoldersModel : ScriptableObject
{
	[SerializeField]
	private string[] myFolders;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private FileBrowserItem[] _bookmarksFolders;

	public FileBrowserItem[] bookmarksFolders
	{
		get
		{
			List<FileBrowserItem> list = new List<FileBrowserItem>();
			if (_mainSettingsModel.lastImportedFile != null)
			{
				list.Add(new DirectoryBrowserItem(Path.GetDirectoryName(_mainSettingsModel.lastImportedFile), _mainSettingsModel.lastImportedFile));
			}
			string[] logicalDrives = Directory.GetLogicalDrives();
			foreach (string text in logicalDrives)
			{
				list.Add(new DirectoryBrowserItem(text, text));
			}
			string[] array = myFolders;
			foreach (string path in array)
			{
				list.Add(new DirectoryBrowserItem(Path.GetDirectoryName(path), path));
			}
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			list.Add(new DirectoryBrowserItem("Desktop", folderPath));
			string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			list.Add(new DirectoryBrowserItem("MyMusic", folderPath2));
			_bookmarksFolders = list.ToArray();
			return _bookmarksFolders;
		}
	}
}
