using System.Reflection;

namespace System.Web.Util;

internal static class AssemblyUtil
{
	private const string _emptyFileVersion = "0.0.0.0";

	public static string GetAssemblyFileVersion(Assembly assembly)
	{
		AssemblyFileVersionAttribute[] array = (AssemblyFileVersionAttribute[])assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), inherit: false);
		string text;
		if (array.Length != 0)
		{
			text = array[0].Version;
			if (string.IsNullOrEmpty(text))
			{
				text = "0.0.0.0";
			}
		}
		else
		{
			text = "0.0.0.0";
		}
		return text;
	}
}
