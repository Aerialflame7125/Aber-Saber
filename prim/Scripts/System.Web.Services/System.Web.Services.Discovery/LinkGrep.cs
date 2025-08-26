using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;

namespace System.Web.Services.Discovery;

internal class LinkGrep
{
	private static readonly Regex tagRegex = new Regex("\\G<(?<prefix>[\\w:.-]+(?=:)|):?(?<tagname>[\\w.-]+)(?:\\s+(?<attrprefix>[\\w:.-]+(?=:)|):?(?<attrname>[\\w.-]+)\\s*=\\s*(?:\"(?<attrval>[^\"]*)\"|'(?<attrval>[^']*)'|(?<attrval>[a-zA-Z0-9\\-._:]+)))*\\s*(?<empty>/)?>");

	private static readonly Regex doctypeDirectiveRegex = new Regex("\\G<!doctype\\b(([\\s\\w]+)|(\".*\"))*>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);

	private static readonly Regex endtagRegex = new Regex("\\G</(?<prefix>[\\w:-]+(?=:)|):?(?<tagname>[\\w-]+)\\s*>");

	private static readonly Regex commentRegex = new Regex("\\G<!--(?>[^-]*-)+?->");

	private static readonly Regex whitespaceRegex = new Regex("\\G\\s+(?=<|\\Z)");

	private static readonly Regex textRegex = new Regex("\\G[^<]+");

	private LinkGrep()
	{
	}

	private static string ReadEntireStream(TextReader input)
	{
		char[] array = new char[4096];
		int num = 0;
		while (true)
		{
			int num2 = input.Read(array, num, array.Length - num);
			if (num2 == 0)
			{
				break;
			}
			num += num2;
			if (num == array.Length)
			{
				char[] array2 = new char[array.Length * 2];
				Array.Copy(array, 0, array2, 0, array.Length);
				array = array2;
			}
		}
		return new string(array, 0, num);
	}

	internal static string SearchForLink(Stream stream)
	{
		string text = null;
		text = ReadEntireStream(new StreamReader(stream));
		int num = 0;
		Match match;
		if ((match = doctypeDirectiveRegex.Match(text, num)).Success)
		{
			num += match.Length;
		}
		bool flag;
		do
		{
			flag = false;
			if ((match = whitespaceRegex.Match(text, num)).Success)
			{
				flag = true;
			}
			else if ((match = textRegex.Match(text, num)).Success)
			{
				flag = true;
			}
			num += match.Length;
			if (num == text.Length)
			{
				break;
			}
			if ((match = tagRegex.Match(text, num)).Success)
			{
				flag = true;
				string value = match.Groups["tagname"].Value;
				if (string.Compare(value, "link", StringComparison.OrdinalIgnoreCase) == 0)
				{
					CaptureCollection captures = match.Groups["attrname"].Captures;
					CaptureCollection captures2 = match.Groups["attrval"].Captures;
					int count = captures.Count;
					bool flag2 = false;
					bool flag3 = false;
					string text2 = null;
					for (int i = 0; i < count; i++)
					{
						string strA = captures[i].ToString();
						string text3 = captures2[i].ToString();
						if (string.Compare(strA, "type", StringComparison.OrdinalIgnoreCase) == 0 && ContentType.MatchesBase(text3, "text/xml"))
						{
							flag2 = true;
						}
						else if (string.Compare(strA, "rel", StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(text3, "alternate", StringComparison.OrdinalIgnoreCase) == 0)
						{
							flag3 = true;
						}
						else if (string.Compare(strA, "href", StringComparison.OrdinalIgnoreCase) == 0)
						{
							text2 = text3;
						}
						if (flag2 && flag3 && text2 != null)
						{
							return text2;
						}
					}
				}
				else if (value == "body")
				{
					break;
				}
			}
			else if ((match = endtagRegex.Match(text, num)).Success)
			{
				flag = true;
			}
			else if ((match = commentRegex.Match(text, num)).Success)
			{
				flag = true;
			}
			num += match.Length;
		}
		while (num != text.Length && flag);
		return null;
	}
}
