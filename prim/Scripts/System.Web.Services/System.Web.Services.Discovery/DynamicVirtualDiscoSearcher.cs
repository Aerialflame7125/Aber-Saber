using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Discovery;

internal class DynamicVirtualDiscoSearcher : DynamicDiscoSearcher
{
	private class AppSettings
	{
		internal readonly bool AccessRead;

		internal readonly string[] Bindings;

		internal readonly string VPath;

		internal AppSettings(DirectoryEntry entry)
		{
			string schemaClassName = entry.SchemaClassName;
			AccessRead = true;
			switch (schemaClassName)
			{
			case "IIsWebVirtualDir":
			case "IIsWebDirectory":
				if (!(bool)entry.Properties["AccessRead"][0])
				{
					AccessRead = false;
				}
				else if (schemaClassName == "IIsWebVirtualDir")
				{
					VPath = (string)entry.Properties["Path"][0];
				}
				break;
			case "IIsWebServer":
			{
				Bindings = new string[entry.Properties["ServerBindings"].Count];
				for (int i = 0; i < Bindings.Length; i++)
				{
					Bindings[i] = (string)entry.Properties["ServerBindings"][i];
				}
				break;
			}
			default:
				AccessRead = false;
				break;
			}
		}
	}

	private string rootPathAsdi;

	private string entryPathPrefix;

	private string startDir;

	private Hashtable webApps = new Hashtable();

	private Hashtable Adsi = new Hashtable();

	protected override bool IsVirtualSearch => true;

	internal DynamicVirtualDiscoSearcher(string startDir, string[] excludedUrls, string rootUrl)
		: base(excludedUrls)
	{
		origUrl = rootUrl;
		entryPathPrefix = GetWebServerForUrl(rootUrl) + "/ROOT";
		this.startDir = startDir;
		string text = new Uri(rootUrl).LocalPath;
		if (text.Equals("/"))
		{
			text = "";
		}
		rootPathAsdi = entryPathPrefix + text;
	}

	internal override void Search(string fileToSkipAtBegin)
	{
		SearchInit(fileToSkipAtBegin);
		ScanDirectory(rootPathAsdi);
		CleanupCache();
	}

	protected override void SearchSubDirectories(string nameAdsiDir)
	{
		_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
		DirectoryEntry directoryEntry = (DirectoryEntry)Adsi[nameAdsiDir];
		if (directoryEntry == null)
		{
			if (!DirectoryEntry.Exists(nameAdsiDir))
			{
				return;
			}
			directoryEntry = new DirectoryEntry(nameAdsiDir);
			Adsi[nameAdsiDir] = directoryEntry;
		}
		foreach (DirectoryEntry child in directoryEntry.Children)
		{
			DirectoryEntry directoryEntry3 = (DirectoryEntry)Adsi[child.Path];
			if (directoryEntry3 == null)
			{
				directoryEntry3 = child;
				Adsi[child.Path] = child;
			}
			else
			{
				child.Dispose();
			}
			if (GetAppSettings(directoryEntry3) != null)
			{
				ScanDirectory(directoryEntry3.Path);
			}
		}
	}

	protected override DirectoryInfo GetPhysicalDir(string dir)
	{
		DirectoryEntry directoryEntry = (DirectoryEntry)Adsi[dir];
		if (directoryEntry == null)
		{
			if (!DirectoryEntry.Exists(dir))
			{
				return null;
			}
			directoryEntry = new DirectoryEntry(dir);
			Adsi[dir] = directoryEntry;
		}
		try
		{
			DirectoryInfo directoryInfo = null;
			AppSettings appSettings = GetAppSettings(directoryEntry);
			if (appSettings == null)
			{
				return null;
			}
			if (appSettings.VPath == null)
			{
				if (!dir.StartsWith(rootPathAsdi, StringComparison.Ordinal))
				{
					throw new ArgumentException(Res.GetString("WebVirtualDisoRoot", dir, rootPathAsdi), "dir");
				}
				string text = dir.Substring(rootPathAsdi.Length);
				text = text.Replace('/', '\\');
				directoryInfo = new DirectoryInfo(startDir + text);
			}
			else
			{
				directoryInfo = new DirectoryInfo(appSettings.VPath);
			}
			if (directoryInfo.Exists)
			{
				return directoryInfo;
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "GetPhysicalDir", ex);
			}
			return null;
		}
		return null;
	}

	private string GetWebServerForUrl(string url)
	{
		Uri uri = new Uri(url);
		foreach (DirectoryEntry child in new DirectoryEntry("IIS://" + uri.Host + "/W3SVC").Children)
		{
			DirectoryEntry directoryEntry2 = (DirectoryEntry)Adsi[child.Path];
			if (directoryEntry2 == null)
			{
				directoryEntry2 = child;
				Adsi[child.Path] = child;
			}
			else
			{
				child.Dispose();
			}
			AppSettings appSettings = GetAppSettings(directoryEntry2);
			if (appSettings == null || appSettings.Bindings == null)
			{
				continue;
			}
			string[] bindings = appSettings.Bindings;
			foreach (string obj in bindings)
			{
				_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
				string[] array = obj.Split(':');
				string text = array[0];
				string value = array[1];
				string text2 = array[2];
				if (Convert.ToInt32(value, CultureInfo.InvariantCulture) != uri.Port)
				{
					continue;
				}
				if (uri.HostNameType == UriHostNameType.Dns)
				{
					if (text2.Length == 0 || string.Compare(text2, uri.Host, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return directoryEntry2.Path;
					}
				}
				else if (text.Length == 0 || string.Compare(text, uri.Host, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return directoryEntry2.Path;
				}
			}
		}
		return null;
	}

	protected override string MakeResultPath(string dirName, string fileName)
	{
		return origUrl + dirName.Substring(rootPathAsdi.Length, dirName.Length - rootPathAsdi.Length) + "/" + fileName;
	}

	protected override string MakeAbsExcludedPath(string pathRelativ)
	{
		return rootPathAsdi + "/" + pathRelativ.Replace('\\', '/');
	}

	private AppSettings GetAppSettings(DirectoryEntry entry)
	{
		string path = entry.Path;
		AppSettings appSettings = null;
		object obj = webApps[path];
		if (obj == null)
		{
			lock (webApps)
			{
				obj = webApps[path];
				if (obj == null)
				{
					appSettings = new AppSettings(entry);
					webApps[path] = appSettings;
				}
			}
		}
		else
		{
			appSettings = (AppSettings)obj;
		}
		if (!appSettings.AccessRead)
		{
			return null;
		}
		return appSettings;
	}

	private void CleanupCache()
	{
		foreach (DictionaryEntry item in Adsi)
		{
			((DirectoryEntry)item.Value).Dispose();
		}
		rootPathAsdi = null;
		entryPathPrefix = null;
		startDir = null;
		Adsi = null;
		webApps = null;
	}
}
