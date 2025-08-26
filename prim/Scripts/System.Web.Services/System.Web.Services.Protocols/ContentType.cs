using System.Text;

namespace System.Web.Services.Protocols;

internal class ContentType
{
	internal const string TextBase = "text";

	internal const string TextXml = "text/xml";

	internal const string TextPlain = "text/plain";

	internal const string TextHtml = "text/html";

	internal const string ApplicationBase = "application";

	internal const string ApplicationXml = "application/xml";

	internal const string ApplicationSoap = "application/soap+xml";

	internal const string ApplicationOctetStream = "application/octet-stream";

	internal const string ContentEncoding = "Content-Encoding";

	private ContentType()
	{
	}

	internal static string GetBase(string contentType)
	{
		int num = contentType.IndexOf(';');
		if (num >= 0)
		{
			return contentType.Substring(0, num);
		}
		return contentType;
	}

	internal static string GetMediaType(string contentType)
	{
		string @base = GetBase(contentType);
		int num = @base.IndexOf('/');
		if (num >= 0)
		{
			return @base.Substring(0, num);
		}
		return @base;
	}

	internal static string GetCharset(string contentType)
	{
		return GetParameter(contentType, "charset");
	}

	internal static string GetAction(string contentType)
	{
		return GetParameter(contentType, "action");
	}

	private static string GetParameter(string contentType, string paramName)
	{
		string[] array = contentType.Split(';');
		for (int i = 1; i < array.Length; i++)
		{
			string text = array[i].TrimStart(null);
			if (string.Compare(text, 0, paramName, 0, paramName.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				int num = text.IndexOf('=', paramName.Length);
				if (num >= 0)
				{
					return text.Substring(num + 1).Trim(' ', '\'', '"', '\t');
				}
			}
		}
		return null;
	}

	internal static bool MatchesBase(string contentType, string baseContentType)
	{
		return string.Compare(GetBase(contentType), baseContentType, StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal static bool IsApplication(string contentType)
	{
		return string.Compare(GetMediaType(contentType), "application", StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal static bool IsSoap(string contentType)
	{
		string @base = GetBase(contentType);
		if (string.Compare(@base, "text/xml", StringComparison.OrdinalIgnoreCase) != 0)
		{
			return string.Compare(@base, "application/soap+xml", StringComparison.OrdinalIgnoreCase) == 0;
		}
		return true;
	}

	internal static bool IsXml(string contentType)
	{
		string @base = GetBase(contentType);
		if (string.Compare(@base, "text/xml", StringComparison.OrdinalIgnoreCase) != 0)
		{
			return string.Compare(@base, "application/xml", StringComparison.OrdinalIgnoreCase) == 0;
		}
		return true;
	}

	internal static bool IsHtml(string contentType)
	{
		return string.Compare(GetBase(contentType), "text/html", StringComparison.OrdinalIgnoreCase) == 0;
	}

	internal static string Compose(string contentType, Encoding encoding)
	{
		return Compose(contentType, encoding, null);
	}

	internal static string Compose(string contentType, Encoding encoding, string action)
	{
		if (encoding == null && action == null)
		{
			return contentType;
		}
		StringBuilder stringBuilder = new StringBuilder(contentType);
		if (encoding != null)
		{
			stringBuilder.Append("; charset=");
			stringBuilder.Append(encoding.WebName);
		}
		if (action != null)
		{
			stringBuilder.Append("; action=\"");
			stringBuilder.Append(action);
			stringBuilder.Append("\"");
		}
		return stringBuilder.ToString();
	}
}
