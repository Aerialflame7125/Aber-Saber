public class DirectoryBrowserItem : FileBrowserItem
{
	public DirectoryBrowserItem(string displayName, string path)
		: base(displayName, path)
	{
	}

	public static bool IsItemMember(FileBrowserItem item)
	{
		return item.GetType() == typeof(DirectoryBrowserItem);
	}
}
