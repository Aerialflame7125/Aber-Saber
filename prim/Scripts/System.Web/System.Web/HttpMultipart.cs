using System.IO;
using System.Text;
using System.Web.Util;

namespace System.Web;

internal class HttpMultipart
{
	public class Element
	{
		public string ContentType;

		public string Name;

		public string Filename;

		public long Start;

		public long Length;

		public override string ToString()
		{
			return "ContentType " + ContentType + ", Name " + Name + ", Filename " + Filename + ", Start " + Start + ", Length " + Length;
		}
	}

	private Stream data;

	private string boundary;

	private byte[] boundary_bytes;

	private byte[] buffer;

	private bool at_eof;

	private Encoding encoding;

	private StringBuilder sb;

	private const byte HYPHEN = 45;

	private const byte LF = 10;

	private const byte CR = 13;

	public HttpMultipart(Stream data, string b, Encoding encoding)
	{
		this.data = data;
		boundary = b;
		boundary_bytes = encoding.GetBytes(b);
		buffer = new byte[boundary_bytes.Length + 2];
		this.encoding = encoding;
		sb = new StringBuilder();
	}

	private string ReadLine()
	{
		bool flag = false;
		int num = 0;
		sb.Length = 0;
		while (true)
		{
			num = data.ReadByte();
			switch (num)
			{
			case -1:
				return null;
			case 10:
				if (flag)
				{
					sb.Length--;
				}
				return sb.ToString();
			}
			flag = num == 13;
			sb.Append((char)num);
		}
	}

	private static string GetContentDispositionAttribute(string l, string name)
	{
		int num = l.IndexOf(name + "=\"");
		if (num < 0)
		{
			return null;
		}
		int num2 = num + name.Length + "=\"".Length;
		int num3 = l.IndexOf('"', num2);
		if (num3 < 0)
		{
			return null;
		}
		if (num2 == num3)
		{
			return "";
		}
		return l.Substring(num2, num3 - num2);
	}

	private string GetContentDispositionAttributeWithEncoding(string l, string name)
	{
		int num = l.IndexOf(name + "=\"");
		if (num < 0)
		{
			return null;
		}
		int num2 = num + name.Length + "=\"".Length;
		int num3 = l.IndexOf('"', num2);
		if (num3 < 0)
		{
			return null;
		}
		if (num2 == num3)
		{
			return "";
		}
		string text = l.Substring(num2, num3 - num2);
		byte[] array = new byte[text.Length];
		for (int num4 = text.Length - 1; num4 >= 0; num4--)
		{
			array[num4] = (byte)text[num4];
		}
		return encoding.GetString(array);
	}

	private bool ReadBoundary()
	{
		try
		{
			string text = ReadLine();
			while (text == "")
			{
				text = ReadLine();
			}
			if (text[0] != '-' || text[1] != '-')
			{
				return false;
			}
			if (!StrUtils.EndsWith(text, boundary, ignore_case: false))
			{
				return true;
			}
		}
		catch
		{
		}
		return false;
	}

	private string ReadHeaders()
	{
		string text = ReadLine();
		if (text == "")
		{
			return null;
		}
		return text;
	}

	private bool CompareBytes(byte[] orig, byte[] other)
	{
		for (int num = orig.Length - 1; num >= 0; num--)
		{
			if (orig[num] != other[num])
			{
				return false;
			}
		}
		return true;
	}

	private long MoveToNextBoundary()
	{
		long num = 0L;
		bool flag = false;
		int num2 = 0;
		int num3 = data.ReadByte();
		while (num3 != -1)
		{
			if (num2 == 0 && num3 == 10)
			{
				num = data.Position - 1;
				if (flag)
				{
					num--;
				}
				num2 = 1;
				num3 = data.ReadByte();
				continue;
			}
			switch (num2)
			{
			case 0:
				flag = num3 == 13;
				num3 = data.ReadByte();
				continue;
			case 1:
				if (num3 != 45)
				{
					break;
				}
				num3 = data.ReadByte();
				switch (num3)
				{
				case -1:
					return -1L;
				default:
					num2 = 0;
					flag = false;
					break;
				case 45:
				{
					int num4 = data.Read(buffer, 0, buffer.Length);
					int num5 = buffer.Length;
					if (num4 != num5)
					{
						return -1L;
					}
					if (!CompareBytes(boundary_bytes, buffer))
					{
						num2 = 0;
						data.Position = num + 2;
						if (flag)
						{
							data.Position++;
							flag = false;
						}
						num3 = data.ReadByte();
						break;
					}
					if (buffer[num5 - 2] == 45 && buffer[num5 - 1] == 45)
					{
						at_eof = true;
					}
					else if (buffer[num5 - 2] != 13 || buffer[num5 - 1] != 10)
					{
						num2 = 0;
						data.Position = num + 2;
						if (flag)
						{
							data.Position++;
							flag = false;
						}
						num3 = data.ReadByte();
						break;
					}
					data.Position = num + 2;
					if (flag)
					{
						data.Position++;
					}
					return num;
				}
				}
				continue;
			}
			num2 = 0;
		}
		return -1L;
	}

	public Element ReadNextElement()
	{
		if (at_eof || ReadBoundary())
		{
			return null;
		}
		Element element = new Element();
		string text;
		while ((text = ReadHeaders()) != null)
		{
			if (StrUtils.StartsWith(text, "Content-Disposition:", ignore_case: true))
			{
				element.Name = GetContentDispositionAttribute(text, "name");
				element.Filename = StripPath(GetContentDispositionAttributeWithEncoding(text, "filename"));
			}
			else if (StrUtils.StartsWith(text, "Content-Type:", ignore_case: true))
			{
				element.ContentType = text.Substring("Content-Type:".Length).Trim();
			}
		}
		long num = (element.Start = data.Position);
		long num2 = MoveToNextBoundary();
		if (num2 == -1)
		{
			return null;
		}
		element.Length = num2 - num;
		return element;
	}

	private static string StripPath(string path)
	{
		if (path == null || path.Length == 0)
		{
			return path;
		}
		if (path.IndexOf(":\\") != 1 && !path.StartsWith("\\\\"))
		{
			return path;
		}
		return path.Substring(path.LastIndexOf('\\') + 1);
	}
}
