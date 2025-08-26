using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Web.Services.Discovery;

internal abstract class DynamicDiscoSearcher
{
	protected string origUrl;

	protected string[] excludedUrls;

	protected string fileToSkipFirst;

	protected ArrayList filesFound;

	protected DiscoverySearchPattern[] primarySearchPatterns;

	protected DiscoverySearchPattern[] secondarySearchPatterns;

	protected DiscoveryDocument discoDoc = new DiscoveryDocument();

	protected Hashtable excludedUrlsTable;

	protected int subDirLevel;

	internal DiscoveryDocument DiscoveryDocument => discoDoc;

	internal DiscoverySearchPattern[] PrimarySearchPattern
	{
		get
		{
			if (primarySearchPatterns == null)
			{
				primarySearchPatterns = new DiscoverySearchPattern[1]
				{
					new DiscoveryDocumentSearchPattern()
				};
			}
			return primarySearchPatterns;
		}
	}

	internal DiscoverySearchPattern[] SecondarySearchPattern
	{
		get
		{
			if (secondarySearchPatterns == null)
			{
				secondarySearchPatterns = new DiscoverySearchPattern[2]
				{
					new ContractSearchPattern(),
					new DiscoveryDocumentLinksPattern()
				};
			}
			return secondarySearchPatterns;
		}
	}

	protected abstract bool IsVirtualSearch { get; }

	internal DynamicDiscoSearcher(string[] excludeUrlsList)
	{
		excludedUrls = excludeUrlsList;
		filesFound = new ArrayList();
	}

	internal virtual void SearchInit(string fileToSkipAtBegin)
	{
		subDirLevel = 0;
		fileToSkipFirst = fileToSkipAtBegin;
	}

	protected bool IsExcluded(string url)
	{
		if (excludedUrlsTable == null)
		{
			excludedUrlsTable = new Hashtable();
			string[] array = excludedUrls;
			foreach (string pathRelativ in array)
			{
				excludedUrlsTable.Add(MakeAbsExcludedPath(pathRelativ).ToLower(CultureInfo.InvariantCulture), null);
			}
		}
		return excludedUrlsTable.Contains(url.ToLower(CultureInfo.InvariantCulture));
	}

	protected void ScanDirectory(string directory)
	{
		_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
		if (!IsExcluded(directory) && !ScanDirByPattern(directory, IsPrimary: true, PrimarySearchPattern))
		{
			if (!IsVirtualSearch)
			{
				ScanDirByPattern(directory, IsPrimary: false, SecondarySearchPattern);
			}
			else if (subDirLevel != 0)
			{
				DiscoverySearchPattern[] patterns = new DiscoverySearchPattern[1]
				{
					new DiscoveryDocumentLinksPattern()
				};
				ScanDirByPattern(directory, IsPrimary: false, patterns);
			}
			if (!IsVirtualSearch || subDirLevel <= 0)
			{
				subDirLevel++;
				fileToSkipFirst = "";
				SearchSubDirectories(directory);
				subDirLevel--;
			}
		}
	}

	protected bool ScanDirByPattern(string dir, bool IsPrimary, DiscoverySearchPattern[] patterns)
	{
		DirectoryInfo physicalDir = GetPhysicalDir(dir);
		if (physicalDir == null)
		{
			return false;
		}
		_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
		bool flag = false;
		for (int i = 0; i < patterns.Length; i++)
		{
			FileInfo[] files = physicalDir.GetFiles(patterns[i].Pattern);
			foreach (FileInfo fileInfo in files)
			{
				if ((fileInfo.Attributes & FileAttributes.Directory) == 0)
				{
					_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
					if (string.Compare(fileInfo.Name, fileToSkipFirst, StringComparison.OrdinalIgnoreCase) != 0)
					{
						string text = MakeResultPath(dir, fileInfo.Name);
						filesFound.Add(text);
						discoDoc.References.Add(patterns[i].GetDiscoveryReference(text));
						flag = true;
					}
				}
			}
		}
		return IsPrimary && flag;
	}

	internal abstract void Search(string fileToSkipAtBegin);

	protected abstract DirectoryInfo GetPhysicalDir(string dir);

	protected abstract void SearchSubDirectories(string directory);

	protected abstract string MakeResultPath(string dirName, string fileName);

	protected abstract string MakeAbsExcludedPath(string pathRelativ);
}
