using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System.Web.Compilation;

internal class AspTokenizer
{
	private class PutBackItem
	{
		public readonly string Value;

		public readonly int Position;

		public readonly int CurrentToken;

		public readonly bool InTag;

		public PutBackItem(string value, int position, int currentToken, bool inTag)
		{
			Value = value;
			Position = position;
			CurrentToken = currentToken;
			InTag = inTag;
		}
	}

	private const int CHECKSUM_BUF_SIZE = 8192;

	private static char[] lfcr = new char[2] { '\n', '\r' };

	private TextReader sr;

	private int current_token;

	private StringBuilder sb;

	private StringBuilder odds;

	private int col;

	private int line;

	private int begcol;

	private int begline;

	private int position;

	private bool inTag;

	private bool expectAttrValue;

	private bool alternatingQuotes;

	private bool hasPutBack;

	private bool verbatim;

	private bool have_value;

	private bool have_unget;

	private int unget_value;

	private string val;

	private Stack putBackBuffer;

	private MD5 checksum;

	private char[] checksum_buf = new char[8192];

	private int checksum_buf_pos = -1;

	public MD5 Checksum => checksum;

	public bool Verbatim
	{
		get
		{
			return verbatim;
		}
		set
		{
			verbatim = value;
		}
	}

	public string Value
	{
		get
		{
			if (have_value)
			{
				return val;
			}
			have_value = true;
			val = sb.ToString();
			return val;
		}
	}

	public string Odds => odds.ToString();

	public bool InTag
	{
		get
		{
			return inTag;
		}
		set
		{
			inTag = value;
		}
	}

	public bool ExpectAttrValue
	{
		get
		{
			return expectAttrValue;
		}
		set
		{
			expectAttrValue = value;
		}
	}

	public bool AlternatingQuotes => alternatingQuotes;

	public int BeginLine => begline;

	public int BeginColumn => begcol;

	public int EndLine => line;

	public int EndColumn => col;

	public int Position => position;

	public AspTokenizer(TextReader reader)
	{
		sr = reader;
		sb = new StringBuilder();
		odds = new StringBuilder();
		col = (line = 1);
		hasPutBack = (inTag = false);
	}

	public void put_back()
	{
		if (hasPutBack && !inTag)
		{
			throw new HttpException("put_back called twice!");
		}
		hasPutBack = true;
		if (putBackBuffer == null)
		{
			putBackBuffer = new Stack();
		}
		string value = Value;
		putBackBuffer.Push(new PutBackItem(value, position, current_token, inTag));
		position -= value.Length;
	}

	public int get_token()
	{
		if (hasPutBack)
		{
			PutBackItem putBackItem;
			if (verbatim)
			{
				putBackItem = putBackBuffer.Pop() as PutBackItem;
				string value = putBackItem.Value;
				switch (value.Length)
				{
				case 1:
					putBackItem = new PutBackItem(string.Empty, putBackItem.Position, value[0], inTag: false);
					break;
				default:
					putBackItem = new PutBackItem(value, putBackItem.Position, value[0], inTag: false);
					break;
				case 0:
					break;
				}
			}
			else
			{
				putBackItem = putBackBuffer.Pop() as PutBackItem;
			}
			hasPutBack = putBackBuffer.Count > 0;
			position = putBackItem.Position;
			have_value = false;
			val = null;
			sb = new StringBuilder(putBackItem.Value);
			current_token = putBackItem.CurrentToken;
			inTag = putBackItem.InTag;
			return current_token;
		}
		begline = line;
		begcol = col;
		have_value = false;
		current_token = NextToken();
		return current_token;
	}

	private bool is_identifier_start_character(char c)
	{
		if (!char.IsLetter(c))
		{
			return c == '_';
		}
		return true;
	}

	private bool is_identifier_part_character(char c)
	{
		if (!char.IsLetterOrDigit(c) && c != '_')
		{
			return c == '-';
		}
		return true;
	}

	private void ungetc(int value)
	{
		have_unget = true;
		unget_value = value;
		position--;
		col--;
	}

	private void TransformNextBlock(int count, bool final)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(checksum_buf, 0, count);
		if (checksum == null)
		{
			checksum = MD5.Create();
		}
		if (final)
		{
			checksum.TransformFinalBlock(bytes, 0, bytes.Length);
		}
		else
		{
			checksum.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
		}
		bytes = null;
		checksum_buf_pos = -1;
	}

	private void UpdateChecksum(int c)
	{
		if (c != -1)
		{
			if (checksum_buf_pos + 1 >= 8192)
			{
				TransformNextBlock(checksum_buf_pos + 1, final: false);
			}
			checksum_buf[++checksum_buf_pos] = (char)c;
		}
		else
		{
			TransformNextBlock(checksum_buf_pos + 1, final: true);
		}
	}

	private int read_char()
	{
		int num;
		if (have_unget)
		{
			num = unget_value;
			have_unget = false;
		}
		else
		{
			num = sr.Read();
			UpdateChecksum(num);
		}
		if (num == 13 && sr.Peek() == 10)
		{
			num = sr.Read();
			UpdateChecksum(num);
			position++;
		}
		if (num == 10)
		{
			col = -1;
			line++;
		}
		if (num != -1)
		{
			col++;
			position++;
		}
		return num;
	}

	private int ReadAttValue(int start)
	{
		int num = 0;
		bool flag = false;
		if (start == 34 || start == 39)
		{
			num = start;
			flag = true;
		}
		else
		{
			sb.Append((char)start);
		}
		int num2 = 0;
		bool flag2 = false;
		int num3;
		for (alternatingQuotes = true; (num3 = sr.Peek()) != -1; sb.Append((char)num3), read_char(), num2 = num3)
		{
			if (num3 == 37 && num2 == 60)
			{
				flag2 = true;
			}
			else if (flag2 && num3 == 62 && num2 == 37)
			{
				flag2 = false;
			}
			else if (!flag2)
			{
				if (!flag && num3 == 47)
				{
					read_char();
					num3 = sr.Peek();
					switch (num3)
					{
					case -1:
						num3 = 47;
						continue;
					case 62:
						break;
					default:
						continue;
					}
					ungetc(47);
					break;
				}
				if (!flag && (num3 == 62 || char.IsWhiteSpace((char)num3)))
				{
					break;
				}
				if (flag && num3 == num && num2 != 92)
				{
					read_char();
					break;
				}
			}
			else if (flag && num3 == num)
			{
				alternatingQuotes = false;
			}
		}
		return 2097155;
	}

	private int NextToken()
	{
		sb.Length = 0;
		odds.Length = 0;
		int num;
		while ((num = read_char()) != -1)
		{
			if (verbatim)
			{
				inTag = false;
				sb.Append((char)num);
				return num;
			}
			if (inTag && expectAttrValue && (num == 34 || num == 39))
			{
				return ReadAttValue(num);
			}
			switch (num)
			{
			case 60:
				inTag = true;
				sb.Append((char)num);
				return num;
			case 62:
				inTag = false;
				sb.Append((char)num);
				return num;
			}
			if (current_token == 60 && "%/!".IndexOf((char)num) != -1)
			{
				sb.Append((char)num);
				return num;
			}
			if (inTag && current_token == 37 && "@#=".IndexOf((char)num) != -1)
			{
				if (odds.Length == 0 || odds.ToString().IndexOfAny(lfcr) < 0)
				{
					sb.Append((char)num);
					return num;
				}
				sb.Append((char)num);
				continue;
			}
			if (inTag && num == 45 && sr.Peek() == 45)
			{
				sb.Append("--");
				read_char();
				return 2097157;
			}
			if (!inTag)
			{
				sb.Append((char)num);
				while ((num = sr.Peek()) != -1 && num != 60)
				{
					sb.Append((char)read_char());
				}
				if (num == -1 && sb.Length <= 0)
				{
					return 2097152;
				}
				return 2097156;
			}
			if (inTag && current_token == 61 && !char.IsWhiteSpace((char)num))
			{
				return ReadAttValue(num);
			}
			if (inTag && is_identifier_start_character((char)num))
			{
				sb.Append((char)num);
				while ((num = sr.Peek()) != -1 && (is_identifier_part_character((char)num) || num == 58))
				{
					sb.Append((char)read_char());
				}
				if (current_token == 64 && Directive.IsDirective(sb.ToString()))
				{
					return 2097154;
				}
				return 2097153;
			}
			if (!char.IsWhiteSpace((char)num))
			{
				sb.Append((char)num);
				return num;
			}
			odds.Append((char)num);
		}
		return 2097152;
	}
}
