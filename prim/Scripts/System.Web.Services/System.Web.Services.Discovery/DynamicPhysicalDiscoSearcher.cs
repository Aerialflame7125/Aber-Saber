using System.IO;

namespace System.Web.Services.Discovery;

internal class DynamicPhysicalDiscoSearcher : DynamicDiscoSearcher
{
	private string startDir;

	protected override bool IsVirtualSearch => false;

	internal DynamicPhysicalDiscoSearcher(string searchDir, string[] excludedUrls, string startUrl)
		: base(excludedUrls)
	{
		startDir = searchDir;
		origUrl = startUrl;
	}

	internal override void Search(string fileToSkipAtBegin)
	{
		SearchInit(fileToSkipAtBegin);
		ScanDirectory(startDir);
	}

	protected override void SearchSubDirectories(string localDir)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(localDir);
		if (!directoryInfo.Exists)
		{
			return;
		}
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		foreach (DirectoryInfo directoryInfo2 in directories)
		{
			if (!(directoryInfo2.Name == ".") && !(directoryInfo2.Name == ".."))
			{
				ScanDirectory(localDir + "\\" + directoryInfo2.Name);
			}
		}
	}

	protected override DirectoryInfo GetPhysicalDir(string dir)
	{
		if (!Directory.Exists(dir))
		{
			return null;
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(dir);
		if (!directoryInfo.Exists)
		{
			return null;
		}
		if ((directoryInfo.Attributes & (FileAttributes.Hidden | FileAttributes.System | FileAttributes.Temporary)) != 0)
		{
			return null;
		}
		return directoryInfo;
	}

	protected override string MakeResultPath(string dirName, string fileName)
	{
		return origUrl + dirName.Substring(startDir.Length, dirName.Length - startDir.Length).Replace('\\', '/') + "/" + fileName;
	}

	protected override string MakeAbsExcludedPath(string pathRelativ)
	{
		return startDir + "\\" + pathRelativ.Replace('/', '\\');
	}
}
