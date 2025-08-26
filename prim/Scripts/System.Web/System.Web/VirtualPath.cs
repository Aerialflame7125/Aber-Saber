using System.IO;
using System.Web.Util;

namespace System.Web;

internal class VirtualPath : IDisposable
{
	private string _absolute;

	private string _appRelative;

	private string _appRelativeNotRooted;

	private string _extension;

	private string _directory;

	private string _directoryNoNormalize;

	private string _currentRequestDirectory;

	private string _physicalPath;

	public bool IsAbsolute { get; private set; }

	public bool IsFake { get; private set; }

	public bool IsRooted { get; private set; }

	public bool IsAppRelative { get; private set; }

	public string Original { get; private set; }

	public string Absolute
	{
		get
		{
			if (IsAbsolute)
			{
				return Original;
			}
			if (_absolute == null)
			{
				string original = Original;
				if (!VirtualPathUtility.IsRooted(original))
				{
					_absolute = MakeRooted(original);
				}
				else
				{
					_absolute = original;
				}
				if (VirtualPathUtility.IsAppRelative(_absolute))
				{
					_absolute = VirtualPathUtility.ToAbsolute(_absolute);
				}
			}
			return _absolute;
		}
	}

	public string AppRelative
	{
		get
		{
			if (IsAppRelative)
			{
				return Original;
			}
			if (_appRelative == null)
			{
				string original = Original;
				if (!VirtualPathUtility.IsRooted(original))
				{
					_appRelative = MakeRooted(original);
				}
				else
				{
					_appRelative = original;
				}
				if (VirtualPathUtility.IsAbsolute(_appRelative))
				{
					_appRelative = VirtualPathUtility.ToAppRelative(_appRelative);
				}
			}
			return _appRelative;
		}
	}

	public string AppRelativeNotRooted
	{
		get
		{
			if (_appRelativeNotRooted == null)
			{
				_appRelativeNotRooted = AppRelative.Substring(2);
			}
			return _appRelativeNotRooted;
		}
	}

	public string Extension
	{
		get
		{
			if (_extension == null)
			{
				_extension = VirtualPathUtility.GetExtension(Original);
			}
			return _extension;
		}
	}

	public string Directory
	{
		get
		{
			if (_directory == null)
			{
				_directory = VirtualPathUtility.GetDirectory(Absolute);
			}
			return _directory;
		}
	}

	public string DirectoryNoNormalize
	{
		get
		{
			if (_directoryNoNormalize == null)
			{
				_directoryNoNormalize = VirtualPathUtility.GetDirectory(Absolute, normalize: false);
			}
			return _directoryNoNormalize;
		}
	}

	public string CurrentRequestDirectory
	{
		get
		{
			if (_currentRequestDirectory != null)
			{
				return _currentRequestDirectory;
			}
			HttpRequest httpRequest = HttpContext.Current?.Request;
			if (httpRequest != null)
			{
				return VirtualPathUtility.GetDirectory(httpRequest.CurrentExecutionFilePath);
			}
			return null;
		}
		set
		{
			_currentRequestDirectory = value;
		}
	}

	public string PhysicalPath
	{
		get
		{
			if (_physicalPath != null)
			{
				return _physicalPath;
			}
			HttpRequest httpRequest = HttpContext.Current?.Request;
			if (httpRequest != null)
			{
				_physicalPath = httpRequest.MapPath(Absolute);
				return _physicalPath;
			}
			return null;
		}
	}

	public VirtualPath(string vpath)
		: this(vpath, null, isFake: false)
	{
	}

	public VirtualPath(string vpath, string baseVirtualDir)
		: this(vpath, null, isFake: false)
	{
		CurrentRequestDirectory = baseVirtualDir;
	}

	public VirtualPath(string vpath, string physicalPath, bool isFake)
	{
		IsRooted = VirtualPathUtility.IsRooted(vpath);
		IsAbsolute = VirtualPathUtility.IsAbsolute(vpath);
		IsAppRelative = VirtualPathUtility.IsAppRelative(vpath);
		if (isFake)
		{
			if (string.IsNullOrEmpty(physicalPath))
			{
				throw new ArgumentException("physicalPath");
			}
			_physicalPath = physicalPath;
			Original = "~/" + Path.GetFileName(_physicalPath);
			IsFake = true;
		}
		else
		{
			Original = vpath;
			IsFake = false;
		}
	}

	public bool StartsWith(string s)
	{
		return StrUtils.StartsWith(Original, s);
	}

	private string MakeRooted(string original)
	{
		string currentRequestDirectory = CurrentRequestDirectory;
		if (!string.IsNullOrEmpty(currentRequestDirectory))
		{
			return VirtualPathUtility.Combine(currentRequestDirectory, original);
		}
		return VirtualPathUtility.Combine(HttpRuntime.AppDomainAppVirtualPath, original);
	}

	public void Dispose()
	{
		_absolute = null;
		_appRelative = null;
		_appRelativeNotRooted = null;
		_extension = null;
		_directory = null;
	}

	public override string ToString()
	{
		string text = Original;
		if (string.IsNullOrEmpty(text))
		{
			return GetType().ToString();
		}
		if (IsFake)
		{
			text = text + " [fake: " + PhysicalPath + "]";
		}
		return text;
	}

	public static VirtualPath PhysicalToVirtual(string physical_path)
	{
		if (string.IsNullOrEmpty(physical_path))
		{
			return null;
		}
		string appDomainAppPath = HttpRuntime.AppDomainAppPath;
		if (!StrUtils.StartsWith(physical_path, appDomainAppPath))
		{
			return null;
		}
		string text = physical_path.Substring(appDomainAppPath.Length - 1);
		if (text[0] != '/')
		{
			return null;
		}
		return new VirtualPath(text);
	}
}
