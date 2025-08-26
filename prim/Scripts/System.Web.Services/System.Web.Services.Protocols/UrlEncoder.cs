using System.Text;

namespace System.Web.Services.Protocols;

internal class UrlEncoder
{
	private const int Max16BitUtf8SequenceLength = 4;

	internal static readonly char[] HexUpperChars = new char[16]
	{
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'A', 'B', 'C', 'D', 'E', 'F'
	};

	private UrlEncoder()
	{
	}

	internal static string EscapeString(string s, Encoding e)
	{
		return EscapeStringInternal(s, (e == null) ? new ASCIIEncoding() : e, escapeUriStuff: false);
	}

	internal static string UrlEscapeString(string s, Encoding e)
	{
		return EscapeStringInternal(s, (e == null) ? new ASCIIEncoding() : e, escapeUriStuff: true);
	}

	private static string EscapeStringInternal(string s, Encoding e, bool escapeUriStuff)
	{
		if (s == null)
		{
			return null;
		}
		byte[] bytes = e.GetBytes(s);
		StringBuilder stringBuilder = new StringBuilder(bytes.Length);
		foreach (byte b in bytes)
		{
			char c = (char)b;
			if (b > 127 || b < 32 || c == '%' || (escapeUriStuff && !IsSafe(c)))
			{
				HexEscape8(stringBuilder, c);
			}
			else
			{
				stringBuilder.Append(c);
			}
		}
		return stringBuilder.ToString();
	}

	internal static string UrlEscapeStringUnicode(string s)
	{
		int length = s.Length;
		StringBuilder stringBuilder = new StringBuilder(length);
		for (int i = 0; i < length; i++)
		{
			char c = s[i];
			if (IsSafe(c))
			{
				stringBuilder.Append(c);
			}
			else if (c == ' ')
			{
				stringBuilder.Append('+');
			}
			else if ((c & 0xFF80) == 0)
			{
				HexEscape8(stringBuilder, c);
			}
			else
			{
				HexEscape16(stringBuilder, c);
			}
		}
		return stringBuilder.ToString();
	}

	private static void HexEscape8(StringBuilder sb, char c)
	{
		sb.Append('%');
		sb.Append(HexUpperChars[((int)c >> 4) & 0xF]);
		sb.Append(HexUpperChars[c & 0xF]);
	}

	private static void HexEscape16(StringBuilder sb, char c)
	{
		sb.Append("%u");
		sb.Append(HexUpperChars[((int)c >> 12) & 0xF]);
		sb.Append(HexUpperChars[((int)c >> 8) & 0xF]);
		sb.Append(HexUpperChars[((int)c >> 4) & 0xF]);
		sb.Append(HexUpperChars[c & 0xF]);
	}

	private static bool IsSafe(char ch)
	{
		if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
		{
			return true;
		}
		switch (ch)
		{
		case '!':
		case '\'':
		case '(':
		case ')':
		case '*':
		case '-':
		case '.':
		case '_':
			return true;
		default:
			return false;
		}
	}
}
