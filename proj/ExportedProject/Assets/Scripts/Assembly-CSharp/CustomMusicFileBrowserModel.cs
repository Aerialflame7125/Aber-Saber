using System.Collections.Generic;
using System.IO;

public class CustomMusicFileBrowserModel : FileBrowserModel
{
	public class MusicFileBrowserItem : FileBrowserItem
	{
		public MusicFileBrowserItem(string name, string path)
			: base(name, path)
		{
		}

		public static bool IsItemMember(FileBrowserItem item)
		{
			return item.GetType() == typeof(MusicFileBrowserItem);
		}
	}

	public static CustomMusicFileBrowserModel instance = new CustomMusicFileBrowserModel();

	protected override FileBrowserItem[] GetContentOfDirectory(string path)
	{
		List<FileBrowserItem> list = new List<FileBrowserItem>();
		string path2 = path + "\\..";
		if (Path.GetFullPath(path2) != Path.GetFullPath(path))
		{
			list.Add(new DirectoryBrowserItem("..", Path.GetFullPath(path2)));
		}
		if (!CanOpenDirectory(path))
		{
			return list.ToArray();
		}
		path = Path.GetFullPath(path);
		string[] directories = Directory.GetDirectories(path);
		string[] array = directories;
		foreach (string path3 in array)
		{
			if (CanOpenDirectory(path3))
			{
				list.Add(new DirectoryBrowserItem(Path.GetFileName(path3), Path.GetFullPath(path3)));
			}
		}
		string[] files = Directory.GetFiles(path);
		string[] array2 = files;
		foreach (string path4 in array2)
		{
			switch (Path.GetExtension(path4).ToLower())
			{
			case ".ogg":
			case ".mp3":
			case ".wav":
				list.Add(new MusicFileBrowserItem(Path.GetFileName(path4), Path.GetFullPath(path4)));
				break;
			}
		}
		return list.ToArray();
	}

	private bool CanOpenDirectory(string path)
	{
		try
		{
			Directory.GetDirectories(path);
			return true;
		}
		catch
		{
			return false;
		}
	}
}
