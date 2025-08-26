using System.Collections.Generic;
using System.IO;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AppResourceFilesCollection
{
	private List<AppResourceFileInfo> files;

	private bool isGlobal;

	private string sourceDir;

	public string SourceDir => sourceDir;

	public bool HasFiles
	{
		get
		{
			if (string.IsNullOrEmpty(sourceDir))
			{
				return false;
			}
			return files.Count > 0;
		}
	}

	public List<AppResourceFileInfo> Files => files;

	public AppResourceFilesCollection(HttpContext context)
	{
		if (context == null)
		{
			throw new ArgumentNullException("context");
		}
		isGlobal = true;
		files = new List<AppResourceFileInfo>();
		string path = Path.Combine(HttpRuntime.AppDomainAppPath, "App_GlobalResources");
		if (Directory.Exists(path))
		{
			sourceDir = path;
		}
	}

	public AppResourceFilesCollection(string parserDir)
	{
		if (string.IsNullOrEmpty(parserDir))
		{
			throw new ArgumentException("parserDir cannot be empty");
		}
		isGlobal = true;
		files = new List<AppResourceFileInfo>();
		string path = Path.Combine(parserDir, "App_LocalResources");
		if (Directory.Exists(path))
		{
			sourceDir = path;
			HttpApplicationFactory.WatchLocationForRestart(sourceDir, "*");
		}
	}

	public void Collect()
	{
		if (string.IsNullOrEmpty(sourceDir))
		{
			return;
		}
		FileInfo[] array = new DirectoryInfo(sourceDir).GetFiles();
		if (array.Length == 0)
		{
			return;
		}
		FileInfo[] array2 = array;
		foreach (FileInfo fileInfo in array2)
		{
			string extension = fileInfo.Extension;
			if (Acceptable(extension, out var kind))
			{
				AppResourceFileInfo item = new AppResourceFileInfo(fileInfo, kind);
				files.Add(item);
			}
		}
		if (!isGlobal || files.Count != 0)
		{
			AppResourcesLengthComparer<AppResourceFileInfo> comparer = new AppResourcesLengthComparer<AppResourceFileInfo>();
			files.Sort(comparer);
		}
	}

	private bool Acceptable(string extension, out AppResourceFileKind kind)
	{
		string text = extension.ToLower(Helpers.InvariantCulture);
		if (!(text == ".resx"))
		{
			if (!(text == ".resource"))
			{
				kind = AppResourceFileKind.NotResource;
				return false;
			}
			kind = AppResourceFileKind.Resource;
			return true;
		}
		kind = AppResourceFileKind.ResX;
		return true;
	}
}
