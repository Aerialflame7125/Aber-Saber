using System.Text;

namespace System.Web.Util;

internal class UrlUtils
{
	private static char[] path_sep = new char[2] { '\\', '/' };

	public static string InsertSessionId(string id, string path)
	{
		string directory = GetDirectory(path);
		if (!directory.EndsWith("/"))
		{
			directory += "/";
		}
		string text = HttpRuntime.AppDomainAppVirtualPath;
		if (!text.EndsWith("/"))
		{
			text += "/";
		}
		if (path.StartsWith(text))
		{
			path = path.Substring(text.Length);
		}
		if (path.StartsWith("/"))
		{
			path = ((path.Length > 1) ? path.Substring(1) : "");
		}
		return Canonic(text + "(" + id + ")/" + path);
	}

	public static string GetSessionId(string path)
	{
		if (path == null)
		{
			return null;
		}
		int length = HttpRuntime.AppDomainAppVirtualPath.Length;
		if (path.Length <= length)
		{
			return null;
		}
		path = path.Substring(length);
		int num = path.Length;
		if (num == 0 || path[0] != '/')
		{
			path = "/" + path;
			num++;
		}
		if (num < 27 || path[1] != '(' || path[26] != ')')
		{
			return null;
		}
		return path.Substring(2, 24);
	}

	public static bool HasSessionId(string path)
	{
		if (path == null || path.Length < 5)
		{
			return false;
		}
		if (StrUtils.StartsWith(path, "/("))
		{
			return path.IndexOf(")/") > 2;
		}
		return false;
	}

	public static string RemoveSessionId(string base_path, string file_path)
	{
		int num = base_path.IndexOf("/(");
		string text = base_path.Substring(0, num + 1);
		if (!text.EndsWith("/"))
		{
			text += "/";
		}
		num = base_path.IndexOf(")/");
		if (num != -1 && base_path.Length > num + 2)
		{
			string text2 = base_path.Substring(num + 2);
			if (!text2.EndsWith("/"))
			{
				text2 += "/";
			}
			text += text2;
		}
		return Canonic(text + GetFile(file_path));
	}

	public static string Combine(string basePath, string relPath)
	{
		if (relPath == null)
		{
			throw new ArgumentNullException("relPath");
		}
		int length = relPath.Length;
		if (length == 0)
		{
			return "";
		}
		relPath = relPath.Replace('\\', '/');
		if (IsRooted(relPath))
		{
			return Canonic(relPath);
		}
		char c = relPath[0];
		if (length < 3 || c == '~' || c == '/' || c == '\\')
		{
			if (basePath == null || (basePath.Length == 1 && basePath[0] == '/'))
			{
				basePath = string.Empty;
			}
			string text = ((c == '/') ? "" : "/");
			if (c == '~')
			{
				if (length == 1)
				{
					relPath = "";
				}
				else if (length > 1 && relPath[1] == '/')
				{
					relPath = relPath.Substring(2);
					text = "/";
				}
				string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
				if (appDomainAppVirtualPath.EndsWith("/"))
				{
					text = "";
				}
				return Canonic(appDomainAppVirtualPath + text + relPath);
			}
			return Canonic(basePath + text + relPath);
		}
		if (basePath == null || basePath.Length == 0 || basePath[0] == '~')
		{
			basePath = HttpRuntime.AppDomainAppVirtualPath;
		}
		if (basePath.Length <= 1)
		{
			basePath = string.Empty;
		}
		return Canonic(basePath + "/" + relPath);
	}

	public static string Canonic(string path)
	{
		bool flag = IsRooted(path);
		bool flag2 = path.EndsWith("/");
		string[] array = path.Split(path_sep);
		int num = array.Length;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			string text = array[i];
			if (text.Length == 0 || text == ".")
			{
				continue;
			}
			if (text == "..")
			{
				num2--;
				continue;
			}
			if (num2 < 0)
			{
				if (!flag)
				{
					throw new HttpException("Invalid path.");
				}
				num2 = 0;
			}
			array[num2++] = text;
		}
		if (num2 < 0)
		{
			throw new HttpException("Invalid path.");
		}
		if (num2 == 0)
		{
			return "/";
		}
		string input = string.Join("/", array, 0, num2);
		input = RemoveDoubleSlashes(input);
		if (flag)
		{
			input = "/" + input;
		}
		if (flag2)
		{
			input += "/";
		}
		return input;
	}

	public static string GetDirectory(string url)
	{
		url = url.Replace('\\', '/');
		int num = url.LastIndexOf('/');
		if (num > 0)
		{
			if (num < url.Length)
			{
				num++;
			}
			return RemoveDoubleSlashes(url.Substring(0, num));
		}
		return "/";
	}

	public static string RemoveDoubleSlashes(string input)
	{
		int num = -1;
		for (int i = 1; i < input.Length; i++)
		{
			if (input[i] == '/' && input[i - 1] == '/')
			{
				num = i - 1;
				break;
			}
		}
		if (num == -1)
		{
			return input;
		}
		StringBuilder stringBuilder = new StringBuilder(input.Length);
		stringBuilder.Append(input, 0, num);
		for (int j = num; j < input.Length; j++)
		{
			if (input[j] == '/')
			{
				int num2 = j + 1;
				if (num2 >= input.Length || input[num2] != '/')
				{
					stringBuilder.Append('/');
				}
			}
			else
			{
				stringBuilder.Append(input[j]);
			}
		}
		return stringBuilder.ToString();
	}

	public static string GetFile(string url)
	{
		url = url.Replace('\\', '/');
		int num = url.LastIndexOf('/');
		if (num >= 0)
		{
			if (url.Length == 1)
			{
				return "";
			}
			return url.Substring(num + 1);
		}
		throw new ArgumentException($"GetFile: `{url}' does not contain a /");
	}

	public static bool IsRooted(string path)
	{
		if (path == null || path.Length == 0)
		{
			return true;
		}
		char c = path[0];
		if (c == '/' || c == '\\')
		{
			return true;
		}
		return false;
	}

	public static bool IsRelativeUrl(string path)
	{
		if (path[0] != '/')
		{
			return path.IndexOf(':') == -1;
		}
		return false;
	}

	public static string ResolveVirtualPathFromAppAbsolute(string path)
	{
		if (path[0] != '~')
		{
			return path;
		}
		if (path.Length == 1)
		{
			return HttpRuntime.AppDomainAppVirtualPath;
		}
		if (path[1] == '/' || path[1] == '\\')
		{
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			if (appDomainAppVirtualPath.Length > 1)
			{
				return appDomainAppVirtualPath + "/" + path.Substring(2);
			}
			return "/" + path.Substring(2);
		}
		return path;
	}

	public static string ResolvePhysicalPathFromAppAbsolute(string path)
	{
		if (path[0] != '~')
		{
			return path;
		}
		if (path.Length == 1)
		{
			return HttpRuntime.AppDomainAppPath;
		}
		if (path[1] == '/' || path[1] == '\\')
		{
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (appDomainAppPath.Length > 1)
			{
				return appDomainAppPath + "/" + path.Substring(2);
			}
			return "/" + path.Substring(2);
		}
		return path;
	}
}
