using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web;

/// <summary>Provides methods for encoding and decoding URLs when processing Web requests. This class cannot be inherited. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpUtility
{
	private sealed class HttpQSCollection : NameValueCollection
	{
		public override string ToString()
		{
			int count = Count;
			if (count == 0)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] allKeys = AllKeys;
			for (int i = 0; i < count; i++)
			{
				stringBuilder.AppendFormat("{0}={1}&", allKeys[i], UrlEncode(base[allKeys[i]]));
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length--;
			}
			return stringBuilder.ToString();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUtility" /> class.</summary>
	public HttpUtility()
	{
	}

	/// <summary>Minimally converts a string into an HTML-encoded string and sends the encoded string to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
	/// <param name="s">The string to encode </param>
	/// <param name="output">A <see cref="T:System.IO.TextWriter" /> output stream. </param>
	public static void HtmlAttributeEncode(string s, TextWriter output)
	{
		if (output == null)
		{
			throw new ArgumentNullException("output");
		}
		HttpEncoder.Current.HtmlAttributeEncode(s, output);
	}

	/// <summary>Minimally converts a string to an HTML-encoded string.</summary>
	/// <param name="s">The string to encode. </param>
	/// <returns>An encoded string.</returns>
	public static string HtmlAttributeEncode(string s)
	{
		if (s == null)
		{
			return null;
		}
		using StringWriter stringWriter = new StringWriter();
		HttpEncoder.Current.HtmlAttributeEncode(s, stringWriter);
		return stringWriter.ToString();
	}

	/// <summary>Converts a string that has been encoded for transmission in a URL into a decoded string.</summary>
	/// <param name="str">The string to decode. </param>
	/// <returns>A decoded string.</returns>
	public static string UrlDecode(string str)
	{
		return UrlDecode(str, Encoding.UTF8);
	}

	private static char[] GetChars(MemoryStream b, Encoding e)
	{
		return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
	}

	private static void WriteCharBytes(IList buf, char ch, Encoding e)
	{
		if (ch > 'ÿ')
		{
			byte[] bytes = e.GetBytes(new char[1] { ch });
			foreach (byte b in bytes)
			{
				buf.Add(b);
			}
		}
		else
		{
			buf.Add((byte)ch);
		}
	}

	/// <summary>Converts a URL-encoded string into a decoded string, using the specified encoding object.</summary>
	/// <param name="str">The string to decode. </param>
	/// <param name="e">The <see cref="T:System.Text.Encoding" /> that specifies the decoding scheme. </param>
	/// <returns>A decoded string.</returns>
	public static string UrlDecode(string str, Encoding e)
	{
		if (str == null)
		{
			return null;
		}
		if (str.IndexOf('%') == -1 && str.IndexOf('+') == -1)
		{
			return str;
		}
		if (e == null)
		{
			e = Encoding.UTF8;
		}
		long num = str.Length;
		List<byte> list = new List<byte>();
		for (int i = 0; i < num; i++)
		{
			char c = str[i];
			if (c == '%' && i + 2 < num && str[i + 1] != '%')
			{
				int @char;
				if (str[i + 1] == 'u' && i + 5 < num)
				{
					@char = GetChar(str, i + 2, 4);
					if (@char != -1)
					{
						WriteCharBytes(list, (char)@char, e);
						i += 5;
					}
					else
					{
						WriteCharBytes(list, '%', e);
					}
				}
				else if ((@char = GetChar(str, i + 1, 2)) != -1)
				{
					WriteCharBytes(list, (char)@char, e);
					i += 2;
				}
				else
				{
					WriteCharBytes(list, '%', e);
				}
			}
			else if (c == '+')
			{
				WriteCharBytes(list, ' ', e);
			}
			else
			{
				WriteCharBytes(list, c, e);
			}
		}
		byte[] bytes = list.ToArray();
		list = null;
		return e.GetString(bytes);
	}

	/// <summary>Converts a URL-encoded byte array into a decoded string using the specified decoding object.</summary>
	/// <param name="bytes">The array of bytes to decode. </param>
	/// <param name="e">The <see cref="T:System.Text.Encoding" /> that specifies the decoding scheme. </param>
	/// <returns>A decoded string.</returns>
	public static string UrlDecode(byte[] bytes, Encoding e)
	{
		if (bytes == null)
		{
			return null;
		}
		return UrlDecode(bytes, 0, bytes.Length, e);
	}

	private static int GetInt(byte b)
	{
		char c = (char)b;
		if (c >= '0' && c <= '9')
		{
			return c - 48;
		}
		if (c >= 'a' && c <= 'f')
		{
			return c - 97 + 10;
		}
		if (c >= 'A' && c <= 'F')
		{
			return c - 65 + 10;
		}
		return -1;
	}

	private static int GetChar(byte[] bytes, int offset, int length)
	{
		int num = 0;
		int num2 = length + offset;
		for (int i = offset; i < num2; i++)
		{
			int @int = GetInt(bytes[i]);
			if (@int == -1)
			{
				return -1;
			}
			num = (num << 4) + @int;
		}
		return num;
	}

	private static int GetChar(string str, int offset, int length)
	{
		int num = 0;
		int num2 = length + offset;
		for (int i = offset; i < num2; i++)
		{
			char c = str[i];
			if (c > '\u007f')
			{
				return -1;
			}
			int @int = GetInt((byte)c);
			if (@int == -1)
			{
				return -1;
			}
			num = (num << 4) + @int;
		}
		return num;
	}

	/// <summary>Converts a URL-encoded byte array into a decoded string using the specified encoding object, starting at the specified position in the array, and continuing for the specified number of bytes.</summary>
	/// <param name="bytes">The array of bytes to decode. </param>
	/// <param name="offset">The position in the byte to begin decoding. </param>
	/// <param name="count">The number of bytes to decode. </param>
	/// <param name="e">The <see cref="T:System.Text.Encoding" /> object that specifies the decoding scheme. </param>
	/// <returns>A decoded string.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="bytes" /> is <see langword="null" />, but <paramref name="count" /> does not equal <see langword="0" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="offset" /> is less than <see langword="0" /> or greater than the length of the <paramref name="bytes" /> array.- or -
	///         <paramref name="count" /> is less than <see langword="0" />, or <paramref name="count" /> + <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
	public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
	{
		if (bytes == null)
		{
			return null;
		}
		if (count == 0)
		{
			return string.Empty;
		}
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (offset < 0 || offset > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		if (count < 0 || offset + count > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("count");
		}
		StringBuilder stringBuilder = new StringBuilder();
		MemoryStream memoryStream = new MemoryStream();
		int num = count + offset;
		for (int i = offset; i < num; i++)
		{
			if (bytes[i] == 37 && i + 2 < count && bytes[i + 1] != 37)
			{
				int @char;
				if (bytes[i + 1] == 117 && i + 5 < num)
				{
					if (memoryStream.Length > 0)
					{
						stringBuilder.Append(GetChars(memoryStream, e));
						memoryStream.SetLength(0L);
					}
					@char = GetChar(bytes, i + 2, 4);
					if (@char != -1)
					{
						stringBuilder.Append((char)@char);
						i += 5;
						continue;
					}
				}
				else if ((@char = GetChar(bytes, i + 1, 2)) != -1)
				{
					memoryStream.WriteByte((byte)@char);
					i += 2;
					continue;
				}
			}
			if (memoryStream.Length > 0)
			{
				stringBuilder.Append(GetChars(memoryStream, e));
				memoryStream.SetLength(0L);
			}
			if (bytes[i] == 43)
			{
				stringBuilder.Append(' ');
			}
			else
			{
				stringBuilder.Append((char)bytes[i]);
			}
		}
		if (memoryStream.Length > 0)
		{
			stringBuilder.Append(GetChars(memoryStream, e));
		}
		memoryStream = null;
		return stringBuilder.ToString();
	}

	/// <summary>Converts a URL-encoded array of bytes into a decoded array of bytes.</summary>
	/// <param name="bytes">The array of bytes to decode. </param>
	/// <returns>A decoded array of bytes.</returns>
	public static byte[] UrlDecodeToBytes(byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		return UrlDecodeToBytes(bytes, 0, bytes.Length);
	}

	/// <summary>Converts a URL-encoded string into a decoded array of bytes.</summary>
	/// <param name="str">The string to decode. </param>
	/// <returns>A decoded array of bytes.</returns>
	public static byte[] UrlDecodeToBytes(string str)
	{
		return UrlDecodeToBytes(str, Encoding.UTF8);
	}

	/// <summary>Converts a URL-encoded string into a decoded array of bytes using the specified decoding object.</summary>
	/// <param name="str">The string to decode. </param>
	/// <param name="e">The <see cref="T:System.Text.Encoding" /> object that specifies the decoding scheme. </param>
	/// <returns>A decoded array of bytes.</returns>
	public static byte[] UrlDecodeToBytes(string str, Encoding e)
	{
		if (str == null)
		{
			return null;
		}
		if (e == null)
		{
			throw new ArgumentNullException("e");
		}
		return UrlDecodeToBytes(e.GetBytes(str));
	}

	/// <summary>Converts a URL-encoded array of bytes into a decoded array of bytes, starting at the specified position in the array and continuing for the specified number of bytes.</summary>
	/// <param name="bytes">The array of bytes to decode. </param>
	/// <param name="offset">The position in the byte array at which to begin decoding. </param>
	/// <param name="count">The number of bytes to decode. </param>
	/// <returns>A decoded array of bytes.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="bytes" /> is <see langword="null" />, but <paramref name="count" /> does not equal <see langword="0" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="offset" /> is less than <see langword="0" /> or greater than the length of the <paramref name="bytes" /> array.- or -
	///         <paramref name="count" /> is less than <see langword="0" />, or <paramref name="count" /> + <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
	public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
	{
		if (bytes == null)
		{
			return null;
		}
		if (count == 0)
		{
			return new byte[0];
		}
		int num = bytes.Length;
		if (offset < 0 || offset >= num)
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		if (count < 0 || offset > num - count)
		{
			throw new ArgumentOutOfRangeException("count");
		}
		MemoryStream memoryStream = new MemoryStream();
		int num2 = offset + count;
		for (int i = offset; i < num2; i++)
		{
			char c = (char)bytes[i];
			switch (c)
			{
			case '+':
				c = ' ';
				break;
			case '%':
				if (i < num2 - 2)
				{
					int @char = GetChar(bytes, i + 1, 2);
					if (@char != -1)
					{
						c = (char)@char;
						i += 2;
					}
				}
				break;
			}
			memoryStream.WriteByte((byte)c);
		}
		return memoryStream.ToArray();
	}

	/// <summary>Encodes a URL string.</summary>
	/// <param name="str">The text to encode. </param>
	/// <returns>An encoded string.</returns>
	public static string UrlEncode(string str)
	{
		return UrlEncode(str, Encoding.UTF8);
	}

	/// <summary>Encodes a URL string using the specified encoding object.</summary>
	/// <param name="str">The text to encode. </param>
	/// <param name="e">The <see cref="T:System.Text.Encoding" /> object that specifies the encoding scheme. </param>
	/// <returns>An encoded string.</returns>
	public static string UrlEncode(string str, Encoding e)
	{
		if (str == null)
		{
			return null;
		}
		if (str == string.Empty)
		{
			return string.Empty;
		}
		bool flag = false;
		int length = str.Length;
		for (int i = 0; i < length; i++)
		{
			char c = str[i];
			if ((c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z') && !HttpEncoder.NotEncoded(c))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return str;
		}
		byte[] bytes = new byte[e.GetMaxByteCount(str.Length)];
		int bytes2 = e.GetBytes(str, 0, str.Length, bytes, 0);
		return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, 0, bytes2));
	}

	/// <summary>Converts a byte array into an encoded URL string.</summary>
	/// <param name="bytes">The array of bytes to encode. </param>
	/// <returns>An encoded string.</returns>
	public static string UrlEncode(byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length == 0)
		{
			return string.Empty;
		}
		return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, 0, bytes.Length));
	}

	/// <summary>Converts a byte array into a URL-encoded string, starting at the specified position in the array and continuing for the specified number of bytes.</summary>
	/// <param name="bytes">The array of bytes to encode. </param>
	/// <param name="offset">The position in the byte array at which to begin encoding. </param>
	/// <param name="count">The number of bytes to encode. </param>
	/// <returns>An encoded string.</returns>
	public static string UrlEncode(byte[] bytes, int offset, int count)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length == 0)
		{
			return string.Empty;
		}
		return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, offset, count));
	}

	/// <summary>Converts a string into a URL-encoded array of bytes.</summary>
	/// <param name="str">The string to encode. </param>
	/// <returns>An encoded array of bytes.</returns>
	public static byte[] UrlEncodeToBytes(string str)
	{
		return UrlEncodeToBytes(str, Encoding.UTF8);
	}

	/// <summary>Converts a string into a URL-encoded array of bytes using the specified encoding object.</summary>
	/// <param name="str">The string to encode </param>
	/// <param name="e">The <see cref="T:System.Text.Encoding" /> that specifies the encoding scheme. </param>
	/// <returns>An encoded array of bytes.</returns>
	public static byte[] UrlEncodeToBytes(string str, Encoding e)
	{
		if (str == null)
		{
			return null;
		}
		if (str.Length == 0)
		{
			return new byte[0];
		}
		byte[] bytes = e.GetBytes(str);
		return UrlEncodeToBytes(bytes, 0, bytes.Length);
	}

	/// <summary>Converts an array of bytes into a URL-encoded array of bytes.</summary>
	/// <param name="bytes">The array of bytes to encode. </param>
	/// <returns>An encoded array of bytes.</returns>
	public static byte[] UrlEncodeToBytes(byte[] bytes)
	{
		if (bytes == null)
		{
			return null;
		}
		if (bytes.Length == 0)
		{
			return new byte[0];
		}
		return UrlEncodeToBytes(bytes, 0, bytes.Length);
	}

	/// <summary>Converts an array of bytes into a URL-encoded array of bytes, starting at the specified position in the array and continuing for the specified number of bytes.</summary>
	/// <param name="bytes">The array of bytes to encode. </param>
	/// <param name="offset">The position in the byte array at which to begin encoding. </param>
	/// <param name="count">The number of bytes to encode. </param>
	/// <returns>An encoded array of bytes.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="bytes" /> is <see langword="null" />, but <paramref name="count" /> does not equal <see langword="0" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="offset" /> is less than <see langword="0" /> or greater than the length of the <paramref name="bytes" /> array.- or -
	///         <paramref name="count" /> is less than <see langword="0" />, or <paramref name="count" /> + <paramref name="offset" /> is greater than the length of the <paramref name="bytes" /> array.</exception>
	public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
	{
		if (bytes == null)
		{
			return null;
		}
		return HttpEncoder.Current.UrlEncode(bytes, offset, count);
	}

	/// <summary>Converts a string into a Unicode string.</summary>
	/// <param name="str">The string to convert. </param>
	/// <returns>A Unicode string in %<paramref name="UnicodeValue" /> notation.</returns>
	public static string UrlEncodeUnicode(string str)
	{
		if (str == null)
		{
			return null;
		}
		return Encoding.ASCII.GetString(UrlEncodeUnicodeToBytes(str));
	}

	/// <summary>Converts a Unicode string into an array of bytes.</summary>
	/// <param name="str">The string to convert. </param>
	/// <returns>A byte array.</returns>
	public static byte[] UrlEncodeUnicodeToBytes(string str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.Length == 0)
		{
			return new byte[0];
		}
		MemoryStream memoryStream = new MemoryStream(str.Length);
		for (int i = 0; i < str.Length; i++)
		{
			HttpEncoder.UrlEncodeChar(str[i], memoryStream, isUnicode: true);
		}
		return memoryStream.ToArray();
	}

	/// <summary>Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.</summary>
	/// <param name="s">The string to decode. </param>
	/// <returns>A decoded string.</returns>
	public static string HtmlDecode(string s)
	{
		if (s == null)
		{
			return null;
		}
		using StringWriter stringWriter = new StringWriter();
		HttpEncoder.Current.HtmlDecode(s, stringWriter);
		return stringWriter.ToString();
	}

	/// <summary>Converts a string that has been HTML-encoded into a decoded string, and sends the decoded string to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
	/// <param name="s">The string to decode. </param>
	/// <param name="output">A <see cref="T:System.IO.TextWriter" /> stream of output. </param>
	public static void HtmlDecode(string s, TextWriter output)
	{
		if (output == null)
		{
			throw new ArgumentNullException("output");
		}
		if (!string.IsNullOrEmpty(s))
		{
			HttpEncoder.Current.HtmlDecode(s, output);
		}
	}

	/// <summary>Converts a string to an HTML-encoded string.</summary>
	/// <param name="s">The string to encode. </param>
	/// <returns>An encoded string.</returns>
	public static string HtmlEncode(string s)
	{
		if (s == null)
		{
			return null;
		}
		using StringWriter stringWriter = new StringWriter();
		HttpEncoder.Current.HtmlEncode(s, stringWriter);
		return stringWriter.ToString();
	}

	/// <summary>Converts a string into an HTML-encoded string, and returns the output as a <see cref="T:System.IO.TextWriter" /> stream of output.</summary>
	/// <param name="s">The string to encode </param>
	/// <param name="output">A <see cref="T:System.IO.TextWriter" /> output stream. </param>
	public static void HtmlEncode(string s, TextWriter output)
	{
		if (output == null)
		{
			throw new ArgumentNullException("output");
		}
		if (!string.IsNullOrEmpty(s))
		{
			HttpEncoder.Current.HtmlEncode(s, output);
		}
	}

	/// <summary>Converts an object's string representation into an HTML-encoded string, and returns the encoded string.</summary>
	/// <param name="value">An object.</param>
	/// <returns>An encoded string.</returns>
	public static string HtmlEncode(object value)
	{
		if (value == null)
		{
			return null;
		}
		if (value is IHtmlString htmlString)
		{
			return htmlString.ToHtmlString();
		}
		return HtmlEncode(value.ToString());
	}

	/// <summary>Encodes a string.</summary>
	/// <param name="value">A string to encode.</param>
	/// <returns>An encoded string.</returns>
	public static string JavaScriptStringEncode(string value)
	{
		return JavaScriptStringEncode(value, addDoubleQuotes: false);
	}

	/// <summary>Encodes a string.</summary>
	/// <param name="value">A string to encode.</param>
	/// <param name="addDoubleQuotes">A value that indicates whether double quotation marks will be included around the encoded string.</param>
	/// <returns>An encoded string.</returns>
	public static string JavaScriptStringEncode(string value, bool addDoubleQuotes)
	{
		if (string.IsNullOrEmpty(value))
		{
			if (!addDoubleQuotes)
			{
				return string.Empty;
			}
			return "\"\"";
		}
		int length = value.Length;
		bool flag = false;
		for (int i = 0; i < length; i++)
		{
			char c = value[i];
			if ((c >= '\0' && c <= '\u001f') || c == '"' || c == '\'' || c == '<' || c == '>' || c == '\\')
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			if (!addDoubleQuotes)
			{
				return value;
			}
			return "\"" + value + "\"";
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (addDoubleQuotes)
		{
			stringBuilder.Append('"');
		}
		for (int j = 0; j < length; j++)
		{
			char c = value[j];
			if (c < '\0' || c > '\a')
			{
				switch (c)
				{
				default:
					if (c != '\'' && c != '<' && c != '>')
					{
						switch (c)
						{
						case 8:
							stringBuilder.Append("\\b");
							break;
						case 9:
							stringBuilder.Append("\\t");
							break;
						case 10:
							stringBuilder.Append("\\n");
							break;
						case 12:
							stringBuilder.Append("\\f");
							break;
						case 13:
							stringBuilder.Append("\\r");
							break;
						case 34:
							stringBuilder.Append("\\\"");
							break;
						case 92:
							stringBuilder.Append("\\\\");
							break;
						default:
							stringBuilder.Append(c);
							break;
						}
						continue;
					}
					break;
				case '\v':
				case '\u000e':
				case '\u000f':
				case '\u0010':
				case '\u0011':
				case '\u0012':
				case '\u0013':
				case '\u0014':
				case '\u0015':
				case '\u0016':
				case '\u0017':
				case '\u0018':
				case '\u0019':
				case '\u001a':
				case '\u001b':
				case '\u001c':
				case '\u001d':
				case '\u001e':
				case '\u001f':
					break;
				}
			}
			stringBuilder.AppendFormat("\\u{0:x4}", (int)c);
		}
		if (addDoubleQuotes)
		{
			stringBuilder.Append('"');
		}
		return stringBuilder.ToString();
	}

	/// <summary>Do not use; intended only for browser compatibility. Use <see cref="M:System.Web.HttpUtility.UrlEncode(System.String)" />.</summary>
	/// <param name="str">The text to encode. </param>
	/// <returns>The encoded text.</returns>
	public static string UrlPathEncode(string str)
	{
		return HttpEncoder.Current.UrlPathEncode(str);
	}

	/// <summary>Parses a query string into a <see cref="T:System.Collections.Specialized.NameValueCollection" /> using <see cref="P:System.Text.Encoding.UTF8" /> encoding.</summary>
	/// <param name="query">The query string to parse.</param>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of query parameters and values.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="query" /> is <see langword="null" />. </exception>
	public static NameValueCollection ParseQueryString(string query)
	{
		return ParseQueryString(query, Encoding.UTF8);
	}

	/// <summary>Parses a query string into a <see cref="T:System.Collections.Specialized.NameValueCollection" /> using the specified <see cref="T:System.Text.Encoding" />. </summary>
	/// <param name="query">The query string to parse.</param>
	/// <param name="encoding">The <see cref="T:System.Text.Encoding" /> to use.</param>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of query parameters and values.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="query" /> is <see langword="null" />.- or -
	///         <paramref name="encoding" /> is <see langword="null" />.</exception>
	public static NameValueCollection ParseQueryString(string query, Encoding encoding)
	{
		if (query == null)
		{
			throw new ArgumentNullException("query");
		}
		if (encoding == null)
		{
			throw new ArgumentNullException("encoding");
		}
		if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
		{
			return new HttpQSCollection();
		}
		if (query[0] == '?')
		{
			query = query.Substring(1);
		}
		NameValueCollection result = new HttpQSCollection();
		ParseQueryString(query, encoding, result);
		return result;
	}

	internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
	{
		if (query.Length == 0)
		{
			return;
		}
		string text = HtmlDecode(query);
		int length = text.Length;
		int num = 0;
		bool flag = true;
		while (num <= length)
		{
			int num2 = -1;
			int num3 = -1;
			for (int i = num; i < length; i++)
			{
				if (num2 == -1 && text[i] == '=')
				{
					num2 = i + 1;
				}
				else if (text[i] == '&')
				{
					num3 = i;
					break;
				}
			}
			if (flag)
			{
				flag = false;
				if (text[num] == '?')
				{
					num++;
				}
			}
			string name;
			if (num2 == -1)
			{
				name = null;
				num2 = num;
			}
			else
			{
				name = UrlDecode(text.Substring(num, num2 - num - 1), encoding);
			}
			if (num3 < 0)
			{
				num = -1;
				num3 = text.Length;
			}
			else
			{
				num = num3 + 1;
			}
			string value = UrlDecode(text.Substring(num2, num3 - num2), encoding);
			result.Add(name, value);
			if (num == -1)
			{
				break;
			}
		}
	}
}
