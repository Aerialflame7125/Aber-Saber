using System.Text;

namespace System.Web.Util;

internal sealed class StrUtils
{
	private StrUtils()
	{
	}

	public static bool StartsWith(string str1, string str2)
	{
		return StartsWith(str1, str2, ignore_case: false);
	}

	public static bool StartsWith(string str1, string str2, bool ignore_case)
	{
		int length = str2.Length;
		if (length == 0)
		{
			return true;
		}
		int length2 = str1.Length;
		if (length > length2)
		{
			return false;
		}
		return string.Compare(str1, 0, str2, 0, length, ignore_case, Helpers.InvariantCulture) == 0;
	}

	public static bool EndsWith(string str1, string str2)
	{
		return EndsWith(str1, str2, ignore_case: false);
	}

	public static bool EndsWith(string str1, string str2, bool ignore_case)
	{
		int length = str2.Length;
		if (length == 0)
		{
			return true;
		}
		int length2 = str1.Length;
		if (length > length2)
		{
			return false;
		}
		return string.Compare(str1, length2 - length, str2, 0, length, ignore_case, Helpers.InvariantCulture) == 0;
	}

	public static string EscapeQuotesAndBackslashes(string attributeValue)
	{
		StringBuilder stringBuilder = null;
		for (int i = 0; i < attributeValue.Length; i++)
		{
			char c = attributeValue[i];
			if (c == '\'' || c == '"' || c == '\\')
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
					stringBuilder.Append(attributeValue.Substring(0, i));
				}
				stringBuilder.Append('\\');
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder?.Append(c);
			}
		}
		if (stringBuilder != null)
		{
			return stringBuilder.ToString();
		}
		return attributeValue;
	}

	public static bool IsNullOrEmpty(string value)
	{
		return string.IsNullOrEmpty(value);
	}

	public static string[] SplitRemoveEmptyEntries(string value, char[] separator)
	{
		return value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
	}
}
