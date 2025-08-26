using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebSockets;

internal static class SubProtocolUtil
{
	private static readonly char[] _lwsTrimChars = new char[2] { ' ', '\t' };

	private static readonly char[] _splitChars = new char[1] { ',' };

	public static bool IsValidSubProtocolName(string subprotocol)
	{
		if (!string.IsNullOrEmpty(subprotocol))
		{
			return subprotocol.All(IsValidSubProtocolChar);
		}
		return false;
	}

	private static bool IsValidSubProtocolChar(char c)
	{
		if ('!' <= c && c <= '~')
		{
			return !IsSeparatorChar(c);
		}
		return false;
	}

	private static bool IsSeparatorChar(char c)
	{
		switch (c)
		{
		case '\t':
		case ' ':
		case '"':
		case '(':
		case ')':
		case ',':
		case '/':
		case ':':
		case ';':
		case '<':
		case '=':
		case '>':
		case '?':
		case '@':
		case '[':
		case '\\':
		case ']':
		case '{':
		case '}':
			return true;
		default:
			return false;
		}
	}

	public static List<string> ParseHeader(string headerValue)
	{
		if (headerValue == null)
		{
			return null;
		}
		List<string> list = new List<string>();
		string[] array = headerValue.Split(_splitChars);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].Trim(_lwsTrimChars);
			if (text.Length != 0)
			{
				if (!IsValidSubProtocolName(text))
				{
					return null;
				}
				list.Add(text);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		if (list.Distinct(StringComparer.Ordinal).Count() != list.Count)
		{
			return null;
		}
		return list;
	}
}
