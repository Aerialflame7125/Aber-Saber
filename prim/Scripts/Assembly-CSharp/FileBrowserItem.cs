using UnityEngine;

public class FileBrowserItem
{
	public string displayName { get; private set; }

	public string fullPath { get; private set; }

	public Sprite icon => null;

	public FileBrowserItem(string displayName, string fullPath)
	{
		this.displayName = displayName;
		this.fullPath = fullPath;
	}
}
