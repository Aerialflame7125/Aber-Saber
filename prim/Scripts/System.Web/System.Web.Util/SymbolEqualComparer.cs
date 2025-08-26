using System.Collections;
using System.Globalization;

namespace System.Web.Util;

internal class SymbolEqualComparer : IComparer
{
	internal static readonly IComparer Default = new SymbolEqualComparer();

	internal SymbolEqualComparer()
	{
	}

	int IComparer.Compare(object keyLeft, object keyRight)
	{
		string text = keyLeft as string;
		string text2 = keyRight as string;
		if (text == null)
		{
			throw new ArgumentNullException("keyLeft");
		}
		if (text2 == null)
		{
			throw new ArgumentNullException("keyRight");
		}
		int length = text.Length;
		int length2 = text2.Length;
		if (length != length2)
		{
			return 1;
		}
		for (int i = 0; i < length; i++)
		{
			char c = text[i];
			char c2 = text2[i];
			if (c == c2)
			{
				continue;
			}
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			UnicodeCategory unicodeCategory2 = char.GetUnicodeCategory(c2);
			if (unicodeCategory == UnicodeCategory.UppercaseLetter && unicodeCategory2 == UnicodeCategory.LowercaseLetter)
			{
				if (char.ToLower(c, CultureInfo.InvariantCulture) == c2)
				{
					continue;
				}
			}
			else if (unicodeCategory2 == UnicodeCategory.UppercaseLetter && unicodeCategory == UnicodeCategory.LowercaseLetter && char.ToLower(c2, CultureInfo.InvariantCulture) == c)
			{
				continue;
			}
			return 1;
		}
		return 0;
	}
}
