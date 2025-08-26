using System.IO;
using System.Security.Permissions;

namespace System.Web.Util;

internal static class PathUtil
{
	private static string _system32Path = GetSystem32Path();

	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	private static string GetSystem32Path()
	{
		return Environment.GetFolderPath(Environment.SpecialFolder.System);
	}

	internal static string GetSystemDllFullPath(string filename)
	{
		return Path.Combine(_system32Path, filename);
	}
}
