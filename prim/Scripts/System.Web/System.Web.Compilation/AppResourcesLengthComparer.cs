using System.Collections.Generic;

namespace System.Web.Compilation;

internal class AppResourcesLengthComparer<T> : IComparer<T>
{
	private int CompareStrings(string a, string b)
	{
		if (a == null || b == null)
		{
			return 0;
		}
		return b.Length - a.Length;
	}

	int IComparer<T>.Compare(T _a, T _b)
	{
		string text = null;
		string text2 = null;
		if (_a is string && _b is string)
		{
			text = _a as string;
			text2 = _b as string;
		}
		else if (_a is List<string> && _b is List<string>)
		{
			text = (_a as List<string>)[0];
			text2 = (_b as List<string>)[0];
		}
		else
		{
			if (!(_a is AppResourceFileInfo) || !(_b is AppResourceFileInfo))
			{
				return 0;
			}
			text = (_a as AppResourceFileInfo).Info.Name;
			text2 = (_b as AppResourceFileInfo).Info.Name;
		}
		return CompareStrings(text, text2);
	}
}
