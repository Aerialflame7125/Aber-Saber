using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AspParser : ILocation
{
	private static readonly object errorEvent = new object();

	private static readonly object tagParsedEvent = new object();

	private static readonly object textParsedEvent = new object();

	private static readonly object parsingCompleteEvent = new object();

	private MD5 checksum;

	private AspTokenizer tokenizer;

	private int beginLine;

	private int endLine;

	private int beginColumn;

	private int endColumn;

	private int beginPosition;

	private int endPosition;

	private string filename;

	private string verbatimID;

	private string fileText;

	private StringReader fileReader;

	private bool _internal;

	private int _internalLineOffset;

	private int _internalPositionOffset;

	private AspParser outer;

	private EventHandlerList events = new EventHandlerList();

	public byte[] MD5Checksum
	{
		get
		{
			if (checksum == null)
			{
				return new byte[0];
			}
			return checksum.Hash;
		}
	}

	public int BeginPosition => beginPosition;

	public int EndPosition => endPosition;

	public int BeginLine
	{
		get
		{
			if (_internal)
			{
				return beginLine + _internalLineOffset;
			}
			return beginLine;
		}
	}

	public int BeginColumn => beginColumn;

	public int EndLine
	{
		get
		{
			if (_internal)
			{
				return endLine + _internalLineOffset;
			}
			return endLine;
		}
	}

	public int EndColumn => endColumn;

	public string FileText
	{
		get
		{
			string text = null;
			if (_internal && outer != null)
			{
				text = outer.FileText;
			}
			if (text == null && fileText != null)
			{
				text = fileText;
			}
			return text;
		}
	}

	public string PlainText
	{
		get
		{
			if (beginPosition >= endPosition || fileText == null)
			{
				return null;
			}
			string text = FileText;
			int num;
			int length;
			if (_internal && outer != null)
			{
				num = beginPosition + _internalPositionOffset;
				length = endPosition + _internalPositionOffset - num;
			}
			else
			{
				num = beginPosition;
				length = endPosition - beginPosition;
			}
			return text?.Substring(num, length);
		}
	}

	public string Filename
	{
		get
		{
			if (_internal && outer != null)
			{
				return outer.Filename;
			}
			return filename;
		}
	}

	public string VerbatimID
	{
		set
		{
			tokenizer.Verbatim = true;
			verbatimID = value;
		}
	}

	public event ParseErrorHandler Error
	{
		add
		{
			events.AddHandler(errorEvent, value);
		}
		remove
		{
			events.RemoveHandler(errorEvent, value);
		}
	}

	public event TagParsedHandler TagParsed
	{
		add
		{
			events.AddHandler(tagParsedEvent, value);
		}
		remove
		{
			events.RemoveHandler(tagParsedEvent, value);
		}
	}

	public event TextParsedHandler TextParsed
	{
		add
		{
			events.AddHandler(textParsedEvent, value);
		}
		remove
		{
			events.RemoveHandler(textParsedEvent, value);
		}
	}

	public event ParsingCompleteHandler ParsingComplete
	{
		add
		{
			events.AddHandler(parsingCompleteEvent, value);
		}
		remove
		{
			events.RemoveHandler(parsingCompleteEvent, value);
		}
	}

	public AspParser(string filename, TextReader input)
	{
		this.filename = filename;
		fileText = input.ReadToEnd();
		fileReader = new StringReader(fileText);
		_internalLineOffset = 0;
		tokenizer = new AspTokenizer(fileReader);
	}

	public AspParser(string filename, TextReader input, int startLineOffset, int positionOffset, AspParser outer)
		: this(filename, input)
	{
		_internal = true;
		_internalLineOffset = startLineOffset;
		_internalPositionOffset = positionOffset;
		this.outer = outer;
	}

	private bool Eat(int expected_token)
	{
		if (tokenizer.get_token() != expected_token)
		{
			tokenizer.put_back();
			return false;
		}
		endLine = tokenizer.EndLine;
		endColumn = tokenizer.EndColumn;
		return true;
	}

	private void BeginElement()
	{
		beginLine = tokenizer.BeginLine;
		beginColumn = tokenizer.BeginColumn;
		beginPosition = tokenizer.Position - 1;
	}

	private void EndElement()
	{
		endLine = tokenizer.EndLine;
		endColumn = tokenizer.EndColumn;
		endPosition = tokenizer.Position;
	}

	public void Parse()
	{
		if (tokenizer == null)
		{
			OnError("AspParser not initialized properly.");
			return;
		}
		TagType tagtype = TagType.Text;
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			int token;
			while ((token = tokenizer.get_token()) != 2097152)
			{
				BeginElement();
				if (tokenizer.Verbatim)
				{
					string text = "</" + verbatimID + ">";
					string verbatim = GetVerbatim(token, text);
					if (verbatim == null)
					{
						OnError("Unexpected EOF processing " + verbatimID);
					}
					tokenizer.Verbatim = false;
					EndElement();
					endPosition -= text.Length;
					OnTextParsed(verbatim);
					beginPosition = endPosition;
					endPosition += text.Length;
					OnTagParsed(TagType.Close, verbatimID, null);
				}
				else if (token == 60)
				{
					GetTag(out tagtype, out var id, out var attributes);
					EndElement();
					switch (tagtype)
					{
					case TagType.Text:
						OnTextParsed(id);
						break;
					default:
						OnTagParsed(tagtype, id, attributes);
						break;
					case TagType.ServerComment:
						break;
					}
				}
				else if (tokenizer.Value.Trim().Length != 0 || tagtype != TagType.Directive)
				{
					stringBuilder.Length = 0;
					do
					{
						stringBuilder.Append(tokenizer.Value);
						token = tokenizer.get_token();
					}
					while (token != 60 && token != 2097152);
					tokenizer.put_back();
					EndElement();
					OnTextParsed(stringBuilder.ToString());
				}
			}
		}
		finally
		{
			if (fileReader != null)
			{
				fileReader.Close();
				fileReader = null;
			}
			checksum = tokenizer.Checksum;
			tokenizer = null;
		}
		OnParsingComplete();
	}

	private bool GetInclude(string str, out string pathType, out string filename)
	{
		pathType = null;
		filename = null;
		str = str.Substring(2).Trim();
		int length = str.Length;
		int num = str.LastIndexOf('"');
		if (length < 10 || num != length - 1)
		{
			return false;
		}
		if (!StrUtils.StartsWith(str, "#include ", ignore_case: true))
		{
			return false;
		}
		str = str.Substring(9).Trim();
		bool flag = StrUtils.StartsWith(str, "file", ignore_case: true);
		if (!flag && !StrUtils.StartsWith(str, "virtual", ignore_case: true))
		{
			return false;
		}
		pathType = (flag ? "file" : "virtual");
		if (str.Length < pathType.Length + 3)
		{
			return false;
		}
		str = str.Substring(pathType.Length).Trim();
		if (str.Length < 3 || str[0] != '=')
		{
			return false;
		}
		int i;
		for (i = 1; i < str.Length && (char.IsWhiteSpace(str[i]) || str[i] != '"'); i++)
		{
		}
		if (i == str.Length || i == num)
		{
			return false;
		}
		str = str.Substring(i);
		if (str.Length == 2)
		{
			OnError("Empty file name.");
			return false;
		}
		filename = str.Trim().Substring(i, str.Length - 2);
		if (filename.LastIndexOf('"') != -1)
		{
			return false;
		}
		return true;
	}

	private void GetTag(out TagType tagtype, out string id, out TagAttributes attributes)
	{
		int token = tokenizer.get_token();
		tagtype = TagType.ServerComment;
		id = null;
		attributes = null;
		switch (token)
		{
		case 37:
			GetServerTag(out tagtype, out id, out attributes);
			return;
		case 47:
			if (!Eat(2097153))
			{
				OnError("expecting TAGNAME");
			}
			id = tokenizer.Value;
			if (!Eat(62))
			{
				OnError("expecting '>'. Got '" + id + "'");
			}
			tagtype = TagType.Close;
			return;
		case 33:
		{
			bool num = Eat(2097157);
			if (num)
			{
				tokenizer.put_back();
			}
			tokenizer.Verbatim = true;
			string text = (num ? "-->" : ">");
			string verbatim = GetVerbatim(tokenizer.get_token(), text);
			tokenizer.Verbatim = false;
			if (verbatim == null)
			{
				OnError("Unfinished HTML comment/DTD");
			}
			if (num && GetInclude(verbatim, out var pathType, out var value))
			{
				tagtype = TagType.Include;
				attributes = new TagAttributes();
				attributes.Add(pathType, value);
			}
			else
			{
				tagtype = TagType.Text;
				id = "<!" + verbatim + text;
			}
			return;
		}
		case 2097153:
			if (filename == "@@inner_string@@")
			{
				tagtype = TagType.Text;
				tokenizer.InTag = false;
				id = "<" + tokenizer.Odds + tokenizer.Value;
				return;
			}
			id = tokenizer.Value;
			try
			{
				attributes = GetAttributes();
			}
			catch (Exception ex)
			{
				OnError(ex.Message);
				return;
			}
			tagtype = TagType.Tag;
			if (Eat(47) && Eat(62))
			{
				tagtype = TagType.SelfClosing;
			}
			else if (!Eat(62))
			{
				if (attributes.IsRunAtServer())
				{
					OnError("The server tag is not well formed.");
					return;
				}
				tokenizer.Verbatim = true;
				attributes.Add(string.Empty, GetVerbatim(tokenizer.get_token(), ">") + ">");
				tokenizer.Verbatim = false;
			}
			return;
		}
		string text2 = null;
		if ((ushort)token == 60)
		{
			string odds = tokenizer.Odds;
			if (odds != null && odds.Length > 0 && char.IsWhiteSpace(odds[0]))
			{
				tokenizer.put_back();
				text2 = odds;
			}
			else
			{
				text2 = tokenizer.Value;
			}
		}
		else
		{
			text2 = tokenizer.Value;
		}
		tagtype = TagType.Text;
		tokenizer.InTag = false;
		id = "<" + text2;
	}

	private TagAttributes GetAttributes()
	{
		bool flag = true;
		TagAttributes tagAttributes = new TagAttributes();
		int token;
		while ((token = tokenizer.get_token()) != 2097152)
		{
			if (token == 60 && Eat(37))
			{
				tokenizer.Verbatim = true;
				tagAttributes.Add(string.Empty, "<%" + GetVerbatim(tokenizer.get_token(), "%>") + "%>");
				tokenizer.Verbatim = false;
				tokenizer.InTag = true;
				continue;
			}
			if (token != 2097153)
			{
				break;
			}
			string value = tokenizer.Value;
			if (Eat(61))
			{
				if (Eat(2097155))
				{
					tagAttributes.Add(value, tokenizer.Value);
					flag &= tokenizer.AlternatingQuotes;
					continue;
				}
				if (!Eat(60) || !Eat(37))
				{
					OnError("expected ATTVALUE");
					return null;
				}
				tokenizer.Verbatim = true;
				tagAttributes.Add(value, "<%" + GetVerbatim(tokenizer.get_token(), "%>") + "%>");
				tokenizer.Verbatim = false;
				tokenizer.InTag = true;
			}
			else
			{
				tagAttributes.Add(value, null);
			}
		}
		tokenizer.put_back();
		if (tagAttributes.IsRunAtServer() && !flag)
		{
			OnError("The server tag is not well formed.");
			return null;
		}
		return tagAttributes;
	}

	private string GetVerbatim(int token, string end)
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		int num = 0;
		if (tokenizer.Value.Length > 1)
		{
			stringBuilder.Append(tokenizer.Value);
			token = tokenizer.get_token();
		}
		end = end.ToLower(Helpers.InvariantCulture);
		int num2 = 0;
		for (int i = 0; i < end.Length; i++)
		{
			if (end[0] == end[i])
			{
				num2++;
			}
		}
		while (token != 2097152)
		{
			if (char.ToLower((char)token, Helpers.InvariantCulture) == end[num])
			{
				if (++num >= end.Length)
				{
					break;
				}
				stringBuilder2.Append((char)token);
				token = tokenizer.get_token();
				continue;
			}
			if (num > 0)
			{
				if (num2 > 1 && num == num2 && (ushort)token == end[0])
				{
					stringBuilder.Append((char)token);
					token = tokenizer.get_token();
					continue;
				}
				stringBuilder.Append(stringBuilder2.ToString());
				stringBuilder2.Remove(0, stringBuilder2.Length);
				num = 0;
			}
			stringBuilder.Append((char)token);
			token = tokenizer.get_token();
		}
		if (token == 2097152)
		{
			OnError("Expecting " + end + " and got EOF.");
		}
		return RemoveComments(stringBuilder.ToString());
	}

	private string RemoveComments(string text)
	{
		for (int num = text.IndexOf("<%--"); num != -1; num = text.IndexOf("<%--"))
		{
			int num2 = text.IndexOf("--%>");
			if (num2 == -1 || num2 <= num + 1)
			{
				break;
			}
			text = text.Remove(num, num2 - num + 4);
		}
		return text;
	}

	private void GetServerTag(out TagType tagtype, out string id, out TagAttributes attributes)
	{
		bool expectAttrValue = tokenizer.ExpectAttrValue;
		tokenizer.ExpectAttrValue = false;
		if (Eat(64))
		{
			tokenizer.ExpectAttrValue = expectAttrValue;
			tagtype = TagType.Directive;
			id = "";
			if (Eat(2097154))
			{
				id = tokenizer.Value;
			}
			attributes = GetAttributes();
			if (!Eat(37) || !Eat(62))
			{
				OnError("expecting '%>'");
			}
			return;
		}
		string verbatim;
		if (Eat(2097157))
		{
			tokenizer.ExpectAttrValue = expectAttrValue;
			tokenizer.Verbatim = true;
			verbatim = GetVerbatim(tokenizer.get_token(), "--%>");
			tokenizer.Verbatim = false;
			id = null;
			attributes = null;
			tagtype = TagType.ServerComment;
			return;
		}
		tokenizer.ExpectAttrValue = expectAttrValue;
		bool flag = Eat(61);
		bool flag2 = !flag && Eat(35);
		bool flag3 = !flag2 && !flag && Eat(58);
		string odds = tokenizer.Odds;
		tokenizer.Verbatim = true;
		verbatim = GetVerbatim(tokenizer.get_token(), "%>");
		if (flag2 && odds != null && odds.Length > 0)
		{
			flag2 = false;
			verbatim = "#" + verbatim;
		}
		tokenizer.Verbatim = false;
		id = verbatim;
		attributes = null;
		if (flag2)
		{
			tagtype = TagType.DataBinding;
		}
		else if (flag)
		{
			tagtype = TagType.CodeRenderExpression;
		}
		else if (flag3)
		{
			tagtype = TagType.CodeRenderEncode;
		}
		else
		{
			tagtype = TagType.CodeRender;
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("AspParser {");
		if (filename != null && filename.Length > 0)
		{
			stringBuilder.AppendFormat("{0}:{1}.{2}", filename, beginLine, beginColumn);
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	private void OnError(string msg)
	{
		if (events[errorEvent] is ParseErrorHandler parseErrorHandler)
		{
			parseErrorHandler(this, msg);
		}
	}

	private void OnTagParsed(TagType tagtype, string id, TagAttributes attributes)
	{
		if (events[tagParsedEvent] is TagParsedHandler tagParsedHandler)
		{
			tagParsedHandler(this, tagtype, id, attributes);
		}
	}

	private void OnTextParsed(string text)
	{
		if (events[textParsedEvent] is TextParsedHandler textParsedHandler)
		{
			textParsedHandler(this, text);
		}
	}

	private void OnParsingComplete()
	{
		if (events[parsingCompleteEvent] is ParsingCompleteHandler parsingCompleteHandler)
		{
			parsingCompleteHandler();
		}
	}
}
