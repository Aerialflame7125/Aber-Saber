using System;

public abstract class FileBrowserModel
{
	public void GetContentOfDirectory(string path, Action<FileBrowserItem[]> callback)
	{
		FileBrowserItem[] items = null;
		Action job = delegate
		{
			items = GetContentOfDirectory(path);
		};
		Action finnish = delegate
		{
			callback(items);
		};
		HMTask hMTask = new HMTask(job, finnish);
		hMTask.Run();
	}

	protected abstract FileBrowserItem[] GetContentOfDirectory(string path);
}
