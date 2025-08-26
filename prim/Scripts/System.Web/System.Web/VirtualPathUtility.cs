using System.Text;
using System.Web.Configuration;
using System.Web.Util;
using Microsoft.Win32;

namespace System.Web;

/// <summary>Provides utility methods for common virtual path operations.  </summary>
public static class VirtualPathUtility
{
	private static bool monoSettingsVerifyCompatibility;

	private static bool runningOnWindows;

	private static char[] path_sep;

	private static readonly char[] invalidVirtualPathChars;

	private static readonly string aspNetVerificationKey;

	static VirtualPathUtility()
	{
		path_sep = new char[1] { '/' };
		invalidVirtualPathChars = new char[2] { ':', '*' };
		aspNetVerificationKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ASP.NET";
		try
		{
			runningOnWindows = RuntimeHelpers.RunningOnWindows;
			if (WebConfigurationManager.GetWebApplicationSection("system.web/monoSettings") is MonoSettingsSection monoSettingsSection)
			{
				monoSettingsVerifyCompatibility = monoSettingsSection.VerificationCompatibility != 1;
			}
		}
		catch
		{
		}
	}

	/// <summary>Appends the literal slash mark (/) to the end of the virtual path, if one does not already exist.</summary>
	/// <param name="virtualPath">The virtual path to append the slash mark to.</param>
	/// <returns>The modified virtual path.</returns>
	public static string AppendTrailingSlash(string virtualPath)
	{
		if (virtualPath == null)
		{
			return virtualPath;
		}
		int length = virtualPath.Length;
		if (length == 0 || virtualPath[length - 1] == '/')
		{
			return virtualPath;
		}
		return virtualPath + "/";
	}

	/// <summary>Combines a base path and a relative path.</summary>
	/// <param name="basePath">The base path.</param>
	/// <param name="relativePath">The relative path.</param>
	/// <returns>The combined <paramref name="basePath" /> and <paramref name="relativePath" />.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="relativePath" /> is a physical path.-or-
	///         <paramref name="relativePath" /> includes one or more colons.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="relativePath" /> is <see langword="null" /> or an empty string.-or-
	///         <paramref name="basePath" /> is <see langword="null" /> or an empty string.</exception>
	public static string Combine(string basePath, string relativePath)
	{
		basePath = Normalize(basePath);
		if (IsRooted(relativePath))
		{
			return Normalize(relativePath);
		}
		int length = basePath.Length;
		if (basePath[length - 1] != '/')
		{
			if (length > 1)
			{
				int num = basePath.LastIndexOf('/');
				if (num >= 0)
				{
					basePath = basePath.Substring(0, num + 1);
				}
			}
			else
			{
				basePath += "/";
			}
		}
		return Normalize(basePath + relativePath);
	}

	/// <summary>Returns the directory portion of a virtual path.</summary>
	/// <param name="virtualPath">The virtual path.</param>
	/// <returns>The directory referenced in the virtual path. </returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualPath" /> is not rooted. - or -
	///         <paramref name="virtualPath" /> is <see langword="null" /> or an empty string.</exception>
	public static string GetDirectory(string virtualPath)
	{
		return GetDirectory(virtualPath, normalize: true);
	}

	internal static string GetDirectory(string virtualPath, bool normalize)
	{
		if (normalize)
		{
			virtualPath = Normalize(virtualPath);
		}
		int length = virtualPath.Length;
		if (IsAppRelative(virtualPath) && length < 3)
		{
			virtualPath = ToAbsolute(virtualPath);
			length = virtualPath.Length;
		}
		if (length == 1 && virtualPath[0] == '/')
		{
			return null;
		}
		int num = virtualPath.LastIndexOf('/', length - 2, length - 2);
		if (num > 0)
		{
			return virtualPath.Substring(0, num + 1);
		}
		return "/";
	}

	/// <summary>Retrieves the extension of the file that is referenced in the virtual path.</summary>
	/// <param name="virtualPath">The virtual path.</param>
	/// <returns>The file name extension string literal, including the period (.), <see langword="null" />, or an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualPath" /> contains one or more characters that are not valid, as defined in <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
	public static string GetExtension(string virtualPath)
	{
		if (StrUtils.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		virtualPath = Canonize(virtualPath);
		int num = virtualPath.LastIndexOf('.');
		if (num == -1 || num == virtualPath.Length - 1 || num < virtualPath.LastIndexOf('/'))
		{
			return string.Empty;
		}
		return virtualPath.Substring(num);
	}

	/// <summary>Retrieves the file name of the file that is referenced in the virtual path.</summary>
	/// <param name="virtualPath">The virtual path. </param>
	/// <returns>The file name literal after the last directory character in <paramref name="virtualPath" />; otherwise, the last directory name if the last character of <paramref name="virtualPath" /> is a directory or volume separator character.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualPath" /> contains one or more characters that are not valid, as defined in <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
	public static string GetFileName(string virtualPath)
	{
		virtualPath = Normalize(virtualPath);
		if (IsAppRelative(virtualPath) && virtualPath.Length < 3)
		{
			virtualPath = ToAbsolute(virtualPath);
		}
		if (virtualPath.Length == 1 && virtualPath[0] == '/')
		{
			return string.Empty;
		}
		virtualPath = RemoveTrailingSlash(virtualPath);
		int num = virtualPath.LastIndexOf('/');
		return virtualPath.Substring(num + 1);
	}

	internal static bool IsRooted(string virtualPath)
	{
		if (!IsAbsolute(virtualPath))
		{
			return IsAppRelative(virtualPath);
		}
		return true;
	}

	/// <summary>Returns a Boolean value indicating whether the specified virtual path is absolute; that is, it starts with a literal slash mark (/).</summary>
	/// <param name="virtualPath">The virtual path to check. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="virtualPath" /> is an absolute path and is not <see langword="null" /> or an empty string (""); otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is <see langword="null" />.</exception>
	public static bool IsAbsolute(string virtualPath)
	{
		if (StrUtils.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		if (virtualPath[0] != '/')
		{
			return virtualPath[0] == '\\';
		}
		return true;
	}

	/// <summary>Returns a Boolean value indicating whether the specified virtual path is relative to the application.</summary>
	/// <param name="virtualPath">The virtual path to check. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="virtualPath" /> is relative to the application; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is <see langword="null" />.</exception>
	public static bool IsAppRelative(string virtualPath)
	{
		if (StrUtils.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		if (virtualPath.Length == 1 && virtualPath[0] == '~')
		{
			return true;
		}
		if (virtualPath[0] == '~' && (virtualPath[1] == '/' || virtualPath[1] == '\\'))
		{
			return true;
		}
		return false;
	}

	/// <summary>Returns the relative virtual path from one virtual path containing the root operator (the tilde [~]) to another.</summary>
	/// <param name="fromPath">The starting virtual path to return the relative virtual path from.</param>
	/// <param name="toPath">The ending virtual path to return the relative virtual path to.</param>
	/// <returns>The relative virtual path from <paramref name="fromPath" /> to <paramref name="toPath" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="fromPath" /> is not rooted.- or -
	///         <paramref name="toPath" /> is not rooted.</exception>
	public static string MakeRelative(string fromPath, string toPath)
	{
		if (fromPath == null || toPath == null)
		{
			throw new NullReferenceException();
		}
		if (toPath == "")
		{
			return toPath;
		}
		toPath = ToAbsoluteInternal(toPath);
		fromPath = ToAbsoluteInternal(fromPath);
		if (string.CompareOrdinal(fromPath, toPath) == 0 && fromPath[fromPath.Length - 1] == '/')
		{
			return "./";
		}
		string[] array = toPath.Split('/');
		string[] array2 = fromPath.Split('/');
		int i;
		for (i = 1; array[i] == array2[i] && array.Length != i + 1 && array2.Length != i + 1; i++)
		{
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int j = 1; j < array2.Length - i; j++)
		{
			stringBuilder.Append("../");
		}
		for (int k = i; k < array.Length; k++)
		{
			stringBuilder.Append(array[k]);
			if (k < array.Length - 1)
			{
				stringBuilder.Append('/');
			}
		}
		return stringBuilder.ToString();
	}

	private static string ToAbsoluteInternal(string virtualPath)
	{
		if (IsAppRelative(virtualPath))
		{
			return ToAbsolute(virtualPath, HttpRuntime.AppDomainAppVirtualPath);
		}
		if (IsAbsolute(virtualPath))
		{
			return Normalize(virtualPath);
		}
		throw new ArgumentOutOfRangeException("Specified argument was out of the range of valid values.");
	}

	/// <summary>Removes a trailing slash mark (/) from a virtual path.</summary>
	/// <param name="virtualPath">The virtual path to remove any trailing slash mark from. </param>
	/// <returns>A virtual path without a trailing slash mark, if the virtual path is not already the root directory ("/"); otherwise, <see langword="null" />.</returns>
	public static string RemoveTrailingSlash(string virtualPath)
	{
		if (virtualPath == null || virtualPath == "")
		{
			return null;
		}
		int num = virtualPath.Length - 1;
		if (num == 0 || virtualPath[num] != '/')
		{
			return virtualPath;
		}
		return virtualPath.Substring(0, num);
	}

	/// <summary>Converts a virtual path to an application absolute path.</summary>
	/// <param name="virtualPath">The virtual path to convert to an application-relative path. </param>
	/// <returns>The absolute path representation of the specified virtual path. </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="virtualPath" /> is not rooted. </exception>
	/// <exception cref="T:System.Web.HttpException">A leading double period (..) is used to exit above the top directory.</exception>
	public static string ToAbsolute(string virtualPath)
	{
		return ToAbsolute(virtualPath, normalize: true);
	}

	internal static string ToAbsolute(string virtualPath, bool normalize)
	{
		if (IsAbsolute(virtualPath))
		{
			if (normalize)
			{
				return Normalize(virtualPath);
			}
			return virtualPath;
		}
		string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
		if (appDomainAppVirtualPath == null)
		{
			throw new HttpException("The path to the application is not known");
		}
		if (virtualPath.Length == 1 && virtualPath[0] == '~')
		{
			return appDomainAppVirtualPath;
		}
		return ToAbsolute(virtualPath, appDomainAppVirtualPath, normalize);
	}

	/// <summary>Converts a virtual path to an application absolute path using the specified application path.</summary>
	/// <param name="virtualPath">The virtual path to convert to an application-relative path.</param>
	/// <param name="applicationPath">The application path to use to convert <paramref name="virtualPath" /> to a relative path.</param>
	/// <returns>The absolute virtual path representation of <paramref name="virtualPath" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="applicationPath" /> is not rooted.</exception>
	/// <exception cref="T:System.Web.HttpException">A leading double period (..) is used in the application path to exit above the top directory.</exception>
	public static string ToAbsolute(string virtualPath, string applicationPath)
	{
		return ToAbsolute(virtualPath, applicationPath, normalize: true);
	}

	internal static string ToAbsolute(string virtualPath, string applicationPath, bool normalize)
	{
		if (StrUtils.IsNullOrEmpty(applicationPath))
		{
			throw new ArgumentNullException("applicationPath");
		}
		if (StrUtils.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		if (IsAppRelative(virtualPath))
		{
			if (applicationPath[0] != '/')
			{
				throw new ArgumentException("appPath is not rooted", "applicationPath");
			}
			string text = applicationPath + ((virtualPath.Length == 1) ? "/" : virtualPath.Substring(1));
			if (normalize)
			{
				return Normalize(text);
			}
			return text;
		}
		if (virtualPath[0] != '/')
		{
			throw new ArgumentException($"Relative path not allowed: '{virtualPath}'");
		}
		if (normalize)
		{
			return Normalize(virtualPath);
		}
		return virtualPath;
	}

	/// <summary>Converts a virtual path to an application-relative path using the application virtual path that is in the <see cref="P:System.Web.HttpRuntime.AppDomainAppVirtualPath" /> property. </summary>
	/// <param name="virtualPath">The virtual path to convert to an application-relative path. </param>
	/// <returns>The application-relative path representation of <paramref name="virtualPath" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualPath" /> is <see langword="null" />. </exception>
	public static string ToAppRelative(string virtualPath)
	{
		string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
		if (appDomainAppVirtualPath == null)
		{
			throw new HttpException("The path to the application is not known");
		}
		return ToAppRelative(virtualPath, appDomainAppVirtualPath);
	}

	/// <summary>Converts a virtual path to an application-relative path using a specified application path.</summary>
	/// <param name="virtualPath">The virtual path to convert to an application-relative path. </param>
	/// <param name="applicationPath">The application path to use to convert <paramref name="virtualPath" /> to a relative path. </param>
	/// <returns>The application-relative path representation of <paramref name="virtualPath" />.</returns>
	public static string ToAppRelative(string virtualPath, string applicationPath)
	{
		virtualPath = Normalize(virtualPath);
		if (IsAppRelative(virtualPath))
		{
			return virtualPath;
		}
		if (!IsAbsolute(applicationPath))
		{
			throw new ArgumentException("appPath is not absolute", "applicationPath");
		}
		applicationPath = Normalize(applicationPath);
		if (applicationPath.Length == 1)
		{
			return "~" + virtualPath;
		}
		int length = applicationPath.Length;
		if (string.CompareOrdinal(virtualPath, applicationPath) == 0)
		{
			return "~/";
		}
		if (string.CompareOrdinal(virtualPath, 0, applicationPath, 0, length) == 0)
		{
			return "~" + virtualPath.Substring(length);
		}
		return virtualPath;
	}

	internal static string Normalize(string path)
	{
		if (!IsRooted(path))
		{
			throw new ArgumentException($"The relative virtual path '{path}' is not allowed here.");
		}
		if (path.Length == 1)
		{
			return path;
		}
		path = Canonize(path);
		int num = path.IndexOf('.');
		while (num >= 0 && ++num != path.Length)
		{
			char c = path[num];
			if (c == '/' || c == '.')
			{
				break;
			}
			num = path.IndexOf('.', num);
		}
		if (num < 0)
		{
			return path;
		}
		bool flag = false;
		bool flag2 = false;
		string[] array = null;
		if (path[0] == '~')
		{
			if (path.Length == 2)
			{
				return "~/";
			}
			flag = true;
			path = path.Substring(1);
		}
		else if (path.Length == 1)
		{
			return "/";
		}
		if (path[path.Length - 1] == '/')
		{
			flag2 = true;
		}
		string[] array2 = StrUtils.SplitRemoveEmptyEntries(path, path_sep);
		int num2 = array2.Length;
		int num3 = 0;
		for (int i = 0; i < num2; i++)
		{
			string text = array2[i];
			if (text == ".")
			{
				continue;
			}
			if (text == "..")
			{
				num3--;
				if (num3 >= 0)
				{
					continue;
				}
				if (flag)
				{
					if (array == null)
					{
						array = StrUtils.SplitRemoveEmptyEntries(HttpRuntime.AppDomainAppVirtualPath, path_sep);
					}
					if (array.Length + num3 >= 0)
					{
						continue;
					}
				}
				throw new HttpException("Cannot use a leading .. to exit above the top directory.");
			}
			if (num3 >= 0)
			{
				array2[num3] = text;
			}
			else
			{
				array[array.Length + num3] = text;
			}
			num3++;
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (array != null)
		{
			flag = false;
			int num4 = array.Length;
			if (num3 < 0)
			{
				num4 += num3;
			}
			for (int j = 0; j < num4; j++)
			{
				stringBuilder.Append('/');
				stringBuilder.Append(array[j]);
			}
		}
		else if (flag)
		{
			stringBuilder.Append('~');
		}
		for (int k = 0; k < num3; k++)
		{
			stringBuilder.Append('/');
			stringBuilder.Append(array2[k]);
		}
		if (stringBuilder.Length > 0)
		{
			if (flag2)
			{
				stringBuilder.Append('/');
			}
			return stringBuilder.ToString();
		}
		return "/";
	}

	internal static string Canonize(string path)
	{
		int num = -1;
		for (int i = 0; i < path.Length; i++)
		{
			if (path[i] == '\\' || (path[i] == '/' && i + 1 < path.Length && (path[i + 1] == '/' || path[i + 1] == '\\')))
			{
				num = i;
				break;
			}
		}
		if (num < 0)
		{
			return path;
		}
		StringBuilder stringBuilder = new StringBuilder(path.Length);
		stringBuilder.Append(path, 0, num);
		for (int j = num; j < path.Length; j++)
		{
			if (path[j] == '\\' || path[j] == '/')
			{
				int num2 = j + 1;
				if (num2 >= path.Length || (path[num2] != '\\' && path[num2] != '/'))
				{
					stringBuilder.Append('/');
				}
			}
			else
			{
				stringBuilder.Append(path[j]);
			}
		}
		return stringBuilder.ToString();
	}

	internal static bool IsValidVirtualPath(string path)
	{
		if (path == null)
		{
			return false;
		}
		bool flag = true;
		if (runningOnWindows)
		{
			try
			{
				object value = Registry.GetValue(aspNetVerificationKey, "VerificationCompatibility", null);
				if (value != null && value is int)
				{
					flag = (int)value != 1;
				}
			}
			catch
			{
			}
		}
		if (flag)
		{
			flag = monoSettingsVerifyCompatibility;
		}
		if (!flag)
		{
			return true;
		}
		return path.IndexOfAny(invalidVirtualPathChars) == -1;
	}
}
