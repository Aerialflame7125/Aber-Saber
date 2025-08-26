using System.Collections.Generic;
using System.IO;

public class CustomLevelFileBrowserModel : FileBrowserModel
{
	public class CustomLevelFileBrowserItem : FileBrowserItem
	{
		public CustomLevelInfoWrapper customLevelInfoWrapper { get; private set; }

		public CustomLevelFileBrowserItem(string name, string path, CustomLevelInfoWrapper customLevelInfoWrapper)
			: base(name, path)
		{
			this.customLevelInfoWrapper = customLevelInfoWrapper;
		}

		public static bool IsItemMember(FileBrowserItem item)
		{
			return item.GetType() == typeof(CustomLevelFileBrowserItem);
		}
	}

	public static CustomLevelFileBrowserModel instance = new CustomLevelFileBrowserModel();

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
			list.Add(new DirectoryBrowserItem(Path.GetFileName(path3), Path.GetFullPath(path3)));
		}
		string[] files = Directory.GetFiles(path);
		string[] array2 = files;
		foreach (string path4 in array2)
		{
			string text = Path.GetExtension(path4).ToLower();
			if (text == ".bslevel")
			{
				list.Add(new CustomLevelFileBrowserItem(Path.GetFileName(path4), Path.GetFullPath(path4), null));
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
