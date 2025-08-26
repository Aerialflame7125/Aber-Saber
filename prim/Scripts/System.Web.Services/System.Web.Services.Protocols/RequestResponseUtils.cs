using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

internal class RequestResponseUtils
{
	private static class HttpUtility
	{
		private static class HtmlEntities
		{
			private static object _lookupLockObject = new object();

			private static string[] _entitiesList = new string[252]
			{
				"\"-quot", "&-amp", "<-lt", ">-gt", "\u00a0-nbsp", "¡-iexcl", "¢-cent", "£-pound", "¤-curren", "¥-yen",
				"¦-brvbar", "§-sect", "\u00a8-uml", "©-copy", "ª-ordf", "«-laquo", "¬-not", "\u00ad-shy", "®-reg", "\u00af-macr",
				"°-deg", "±-plusmn", "²-sup2", "³-sup3", "\u00b4-acute", "µ-micro", "¶-para", "·-middot", "\u00b8-cedil", "¹-sup1",
				"º-ordm", "»-raquo", "¼-frac14", "½-frac12", "¾-frac34", "¿-iquest", "À-Agrave", "Á-Aacute", "Â-Acirc", "Ã-Atilde",
				"Ä-Auml", "Å-Aring", "Æ-AElig", "Ç-Ccedil", "È-Egrave", "É-Eacute", "Ê-Ecirc", "Ë-Euml", "Ì-Igrave", "Í-Iacute",
				"Î-Icirc", "Ï-Iuml", "Ð-ETH", "Ñ-Ntilde", "Ò-Ograve", "Ó-Oacute", "Ô-Ocirc", "Õ-Otilde", "Ö-Ouml", "×-times",
				"Ø-Oslash", "Ù-Ugrave", "Ú-Uacute", "Û-Ucirc", "Ü-Uuml", "Ý-Yacute", "Þ-THORN", "ß-szlig", "à-agrave", "á-aacute",
				"â-acirc", "ã-atilde", "ä-auml", "å-aring", "æ-aelig", "ç-ccedil", "è-egrave", "é-eacute", "ê-ecirc", "ë-euml",
				"ì-igrave", "í-iacute", "î-icirc", "ï-iuml", "ð-eth", "ñ-ntilde", "ò-ograve", "ó-oacute", "ô-ocirc", "õ-otilde",
				"ö-ouml", "÷-divide", "ø-oslash", "ù-ugrave", "ú-uacute", "û-ucirc", "ü-uuml", "ý-yacute", "þ-thorn", "ÿ-yuml",
				"Œ-OElig", "œ-oelig", "Š-Scaron", "š-scaron", "Ÿ-Yuml", "ƒ-fnof", "ˆ-circ", "\u02dc-tilde", "Α-Alpha", "Β-Beta",
				"Γ-Gamma", "Δ-Delta", "Ε-Epsilon", "Ζ-Zeta", "Η-Eta", "Θ-Theta", "Ι-Iota", "Κ-Kappa", "Λ-Lambda", "Μ-Mu",
				"Ν-Nu", "Ξ-Xi", "Ο-Omicron", "Π-Pi", "Ρ-Rho", "Σ-Sigma", "Τ-Tau", "Υ-Upsilon", "Φ-Phi", "Χ-Chi",
				"Ψ-Psi", "Ω-Omega", "α-alpha", "β-beta", "γ-gamma", "δ-delta", "ε-epsilon", "ζ-zeta", "η-eta", "θ-theta",
				"ι-iota", "κ-kappa", "λ-lambda", "μ-mu", "ν-nu", "ξ-xi", "ο-omicron", "π-pi", "ρ-rho", "ς-sigmaf",
				"σ-sigma", "τ-tau", "υ-upsilon", "φ-phi", "χ-chi", "ψ-psi", "ω-omega", "ϑ-thetasym", "ϒ-upsih", "ϖ-piv",
				"\u2002-ensp", "\u2003-emsp", "\u2009-thinsp", "\u200c-zwnj", "\u200d-zwj", "\u200e-lrm", "\u200f-rlm", "–-ndash", "—-mdash", "‘-lsquo",
				"’-rsquo", "‚-sbquo", "“-ldquo", "”-rdquo", "„-bdquo", "†-dagger", "‡-Dagger", "•-bull", "…-hellip", "‰-permil",
				"′-prime", "″-Prime", "‹-lsaquo", "›-rsaquo", "‾-oline", "⁄-frasl", "€-euro", "ℑ-image", "℘-weierp", "ℜ-real",
				"™-trade", "ℵ-alefsym", "←-larr", "↑-uarr", "→-rarr", "↓-darr", "↔-harr", "↵-crarr", "⇐-lArr", "⇑-uArr",
				"⇒-rArr", "⇓-dArr", "⇔-hArr", "∀-forall", "∂-part", "∃-exist", "∅-empty", "∇-nabla", "∈-isin", "∉-notin",
				"∋-ni", "∏-prod", "∑-sum", "−-minus", "∗-lowast", "√-radic", "∝-prop", "∞-infin", "∠-ang", "∧-and",
				"∨-or", "∩-cap", "∪-cup", "∫-int", "∴-there4", "∼-sim", "≅-cong", "≈-asymp", "≠-ne", "≡-equiv",
				"≤-le", "≥-ge", "⊂-sub", "⊃-sup", "⊄-nsub", "⊆-sube", "⊇-supe", "⊕-oplus", "⊗-otimes", "⊥-perp",
				"⋅-sdot", "⌈-lceil", "⌉-rceil", "⌊-lfloor", "⌋-rfloor", "〈-lang", "〉-rang", "◊-loz", "♠-spades", "♣-clubs",
				"♥-hearts", "♦-diams"
			};

			private static volatile Hashtable _entitiesLookupTable;

			internal static char Lookup(string entity)
			{
				if (_entitiesLookupTable == null)
				{
					lock (_lookupLockObject)
					{
						if (_entitiesLookupTable == null)
						{
							Hashtable hashtable = new Hashtable();
							string[] entitiesList = _entitiesList;
							foreach (string text in entitiesList)
							{
								hashtable[text.Substring(2)] = text[0];
							}
							_entitiesLookupTable = hashtable;
						}
					}
				}
				object obj = _entitiesLookupTable[entity];
				if (obj != null)
				{
					return (char)obj;
				}
				return '\0';
			}
		}

		private static char[] s_entityEndingChars = new char[2] { ';', '&' };

		internal static string HtmlDecode(string s)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('&') < 0)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter output = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			HtmlDecode(s, output);
			return stringBuilder.ToString();
		}

		public static void HtmlDecode(string s, TextWriter output)
		{
			if (s == null)
			{
				return;
			}
			if (s.IndexOf('&') < 0)
			{
				output.Write(s);
				return;
			}
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (c == '&')
				{
					int num = s.IndexOfAny(s_entityEndingChars, i + 1);
					if (num > 0 && s[num] == ';')
					{
						string text = s.Substring(i + 1, num - i - 1);
						if (text.Length > 1 && text[0] == '#')
						{
							try
							{
								c = ((text[1] != 'x' && text[1] != 'X') ? ((char)int.Parse(text.Substring(1), CultureInfo.InvariantCulture)) : ((char)int.Parse(text.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture)));
								i = num;
							}
							catch (FormatException e)
							{
								i++;
								if (Tracing.On)
								{
									Tracing.ExceptionCatch(TraceEventType.Warning, typeof(HttpUtility), "HtmlDecode", e);
								}
							}
							catch (ArgumentException e2)
							{
								i++;
								if (Tracing.On)
								{
									Tracing.ExceptionCatch(TraceEventType.Warning, typeof(HttpUtility), "HtmlDecode", e2);
								}
							}
						}
						else
						{
							i = num;
							char c2 = HtmlEntities.Lookup(text);
							if (c2 == '\0')
							{
								output.Write('&');
								output.Write(text);
								output.Write(';');
								continue;
							}
							c = c2;
						}
					}
				}
				output.Write(c);
			}
		}
	}

	private RequestResponseUtils()
	{
	}

	internal static Encoding GetEncoding(string contentType)
	{
		string charset = ContentType.GetCharset(contentType);
		Encoding encoding = null;
		try
		{
			if (charset != null && charset.Length > 0)
			{
				encoding = Encoding.GetEncoding(charset);
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, typeof(RequestResponseUtils), "GetEncoding", ex);
			}
		}
		if (encoding != null)
		{
			return encoding;
		}
		return new ASCIIEncoding();
	}

	internal static Encoding GetEncoding2(string contentType)
	{
		if (!ContentType.IsApplication(contentType))
		{
			return GetEncoding(contentType);
		}
		string charset = ContentType.GetCharset(contentType);
		Encoding result = null;
		try
		{
			if (charset != null && charset.Length > 0)
			{
				result = Encoding.GetEncoding(charset);
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, typeof(RequestResponseUtils), "GetEncoding2", ex);
			}
		}
		return result;
	}

	internal static string ReadResponse(WebResponse response)
	{
		return ReadResponse(response, response.GetResponseStream());
	}

	internal static string ReadResponse(WebResponse response, Stream stream)
	{
		Encoding encoding = GetEncoding(response.ContentType);
		if (encoding == null)
		{
			encoding = Encoding.Default;
		}
		StreamReader streamReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks: true);
		try
		{
			return streamReader.ReadToEnd();
		}
		finally
		{
			stream.Close();
		}
	}

	internal static Stream StreamToMemoryStream(Stream stream)
	{
		MemoryStream memoryStream = new MemoryStream(1024);
		byte[] array = new byte[1024];
		int count;
		while ((count = stream.Read(array, 0, array.Length)) != 0)
		{
			memoryStream.Write(array, 0, count);
		}
		memoryStream.Position = 0L;
		return memoryStream;
	}

	internal static string CreateResponseExceptionString(WebResponse response)
	{
		return CreateResponseExceptionString(response, response.GetResponseStream());
	}

	internal static string CreateResponseExceptionString(WebResponse response, Stream stream)
	{
		if (response is HttpWebResponse)
		{
			HttpWebResponse httpWebResponse = (HttpWebResponse)response;
			int statusCode = (int)httpWebResponse.StatusCode;
			if (statusCode >= 400 && statusCode != 500)
			{
				return Res.GetString("WebResponseKnownError", statusCode, httpWebResponse.StatusDescription);
			}
		}
		string text = ((stream != null) ? ReadResponse(response, stream) : string.Empty);
		if (text.Length > 0)
		{
			text = HttpUtility.HtmlDecode(text);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Res.GetString("WebResponseUnknownError"));
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("--");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append(text);
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("--");
			stringBuilder.Append(".");
			return stringBuilder.ToString();
		}
		return Res.GetString("WebResponseUnknownErrorEmptyBody");
	}

	internal static int GetBufferSize(int contentLength)
	{
		if (contentLength == -1)
		{
			return 8000;
		}
		if (contentLength <= 16000)
		{
			return contentLength;
		}
		return 16000;
	}
}
