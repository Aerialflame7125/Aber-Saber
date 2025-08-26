using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AspGenerator
{
	private delegate bool CheckBlockEnd(string text);

	private class CodeRenderParser
	{
		private string str;

		private ControlBuilder builder;

		private AspGenerator generator;

		private ILocation location;

		public CodeRenderParser(string str, ControlBuilder builder, ILocation location)
		{
			this.str = str;
			this.builder = builder;
			this.location = location;
		}

		public void AddChildren(AspGenerator generator)
		{
			this.generator = generator;
			if (str.IndexOf("<%") > 0)
			{
				DoParseExpressions(str);
			}
			else
			{
				DoParse(str);
			}
		}

		private void DoParseExpressions(string str)
		{
			int num = 0;
			int num2 = 0;
			Regex regex = new Regex("(<%(?!@)(?<code>(.|\\s)*?)%>)|(<[\\w:\\.]+.*?runat=[\"']?server[\"']?.*?/>)", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
			int length = str.Length;
			while (num2 > -1 && num < length)
			{
				Match match = regex.Match(str, num2);
				if (!match.Success)
				{
					break;
				}
				string value = match.Value;
				num2 = match.Index;
				if (num2 > num)
				{
					TextParsed(null, str.Substring(num, num2 - num));
				}
				DoParse(value);
				num2 += value.Length;
				num = num2;
				if (num2 >= length)
				{
					break;
				}
				num2 = str.IndexOf('<', num2);
			}
			if (num < length)
			{
				TextParsed(null, str.Substring(num));
			}
		}

		private void DoParse(string str)
		{
			AspParser aspParser = location as AspParser;
			int positionOffset = aspParser?.BeginPosition ?? 0;
			AspParser aspParser2 = new AspParser("@@code_render@@", new StringReader(str), location.BeginLine - 1, positionOffset, aspParser);
			aspParser2.Error += ParseError;
			aspParser2.TagParsed += TagParsed;
			aspParser2.TextParsed += TextParsed;
			aspParser2.Parse();
		}

		private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
		{
			switch (tagtype)
			{
			case TagType.CodeRender:
				builder.AppendSubBuilder(new CodeRenderBuilder(tagid, isAssign: false, location));
				return;
			case TagType.CodeRenderExpression:
				builder.AppendSubBuilder(new CodeRenderBuilder(tagid, isAssign: true, location));
				return;
			case TagType.CodeRenderEncode:
				builder.AppendSubBuilder(new CodeRenderBuilder(tagid, isAssign: true, location, doHtmlEncode: true));
				return;
			case TagType.DataBinding:
				builder.AppendSubBuilder(new DataBindingBuilder(tagid, location));
				return;
			case TagType.Tag:
			case TagType.Close:
			case TagType.SelfClosing:
				if (generator != null)
				{
					generator.TagParsed(location, tagtype, tagid, attributes);
					return;
				}
				break;
			}
			string plainText = location.PlainText;
			if (plainText != null && plainText.Trim().Length > 0)
			{
				builder.AppendLiteralString(plainText);
			}
		}

		private void TextParsed(ILocation location, string text)
		{
			builder.AppendLiteralString(text);
		}

		private void ParseError(ILocation location, string message)
		{
			throw new ParseException(location, message);
		}
	}

	private const int READ_BUFFER_SIZE = 8192;

	internal static Regex DirectiveRegex = new Regex("<%\\s*@(\\s*(?<attrname>\\w[\\w:]*(?=\\W))(\\s*(?<equal>=)\\s*\"(?<attrval>[^\"]*)\"|\\s*(?<equal>=)\\s*'(?<attrval>[^']*)'|\\s*(?<equal>=)\\s*(?<attrval>[^\\s%>]*)|(?<equal>)(?<attrval>\\s*?)))*\\s*?%>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	private static readonly Regex runatServer = new Regex("<[\\w:\\.]+.*?runat=[\"']?server[\"']?.*(?:/>|>)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

	private static readonly Regex endOfTag = new Regex("</[\\w:\\.]+\\s*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

	private static readonly Regex expressionRegex = new Regex("<%.*?%>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

	private static readonly Regex clientCommentRegex = new Regex("<!--(.|\\s)*?-->", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

	private ParserStack pstack;

	private BuilderLocationStack stack;

	private TemplateParser tparser;

	private StringBuilder text;

	private RootBuilder rootBuilder;

	private bool inScript;

	private bool javascript;

	private bool ignore_text;

	private ILocation location;

	private bool isApplication;

	private StringBuilder tagInnerText = new StringBuilder();

	private static IDictionary emptyHash = new Dictionary<string, object>();

	private bool inForm;

	private bool useOtherTags;

	private TagType lastTag;

	private AspComponentFoundry componentFoundry;

	private Stream inputStream;

	public RootBuilder RootBuilder => rootBuilder;

	public AspParser Parser => pstack.Parser;

	public string Filename => pstack.Filename;

	private PageParserFilter PageParserFilter
	{
		get
		{
			if (tparser == null)
			{
				return null;
			}
			return tparser.PageParserFilter;
		}
	}

	public ILocation Location => location;

	public AspGenerator(TemplateParser tparser, AspComponentFoundry componentFoundry)
		: this(tparser)
	{
		this.componentFoundry = componentFoundry;
	}

	public AspGenerator(TemplateParser tparser)
	{
		this.tparser = tparser;
		text = new StringBuilder();
		stack = new BuilderLocationStack();
		pstack = new ParserStack();
	}

	private IDictionary GetDirectiveAttributesDictionary(string skipKeyName, CaptureCollection names, CaptureCollection values)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		int num = 0;
		foreach (Capture name in names)
		{
			string value = name.Value;
			if (string.Compare(skipKeyName, value, StringComparison.OrdinalIgnoreCase) == 0)
			{
				num++;
			}
			else
			{
				dictionary.Add(name.Value, values[num++].Value);
			}
		}
		return dictionary;
	}

	private string GetDirectiveName(CaptureCollection names)
	{
		foreach (Capture name in names)
		{
			string value = name.Value;
			if (Directive.IsDirective(value))
			{
				return value;
			}
		}
		return tparser.DefaultDirectiveName;
	}

	private int GetLineNumberForIndex(string fileContents, int index)
	{
		int num = 1;
		bool flag = false;
		for (int i = 0; i < index; i++)
		{
			char num2 = fileContents[i];
			if (num2 == '\n' || flag)
			{
				num++;
				flag = false;
			}
			flag = num2 == '\r';
		}
		return num;
	}

	private int GetNumberOfLinesForRange(string fileContents, int index, int length)
	{
		int num = 0;
		int num2 = index + length;
		bool flag = false;
		for (int i = index; i < num2; i++)
		{
			char num3 = fileContents[i];
			if (num3 == '\n' || flag)
			{
				num++;
				flag = false;
			}
			flag = num3 == '\r';
		}
		return num;
	}

	private Type GetInheritedType(string fileContents, string filename)
	{
		MatchCollection matchCollection = DirectiveRegex.Matches(fileContents);
		if (matchCollection == null || matchCollection.Count == 0)
		{
			return null;
		}
		string text = tparser.DefaultDirectiveName.ToLower(Helpers.InvariantCulture);
		foreach (Match item in matchCollection)
		{
			GroupCollection groups = item.Groups;
			if (groups.Count >= 6)
			{
				CaptureCollection captures = groups[3].Captures;
				string directiveName = GetDirectiveName(captures);
				if (!string.IsNullOrEmpty(directiveName) && string.Compare(directiveName.ToLower(Helpers.InvariantCulture), text, StringComparison.Ordinal) == 0)
				{
					Location location = new Location(null);
					int index = item.Index;
					location.Filename = filename;
					location.BeginLine = GetLineNumberForIndex(fileContents, index);
					location.EndLine = location.BeginLine + GetNumberOfLinesForRange(fileContents, index, item.Length);
					tparser.Location = location;
					tparser.allowedMainDirectives = 2;
					tparser.AddDirective(text, GetDirectiveAttributesDictionary(text, captures, groups[5].Captures));
					return tparser.BaseType;
				}
			}
		}
		return null;
	}

	private string ReadFileContents(Stream inputStream, string filename)
	{
		string text = null;
		if (inputStream != null)
		{
			if (inputStream.CanSeek)
			{
				long position = inputStream.Position;
				inputStream.Seek(0L, SeekOrigin.Begin);
				Encoding fileEncoding = WebEncoding.FileEncoding;
				StringBuilder stringBuilder = new StringBuilder();
				byte[] array = new byte[8192];
				int count;
				while ((count = inputStream.Read(array, 0, 8192)) > 0)
				{
					stringBuilder.Append(fileEncoding.GetString(array, 0, count));
				}
				inputStream.Seek(position, SeekOrigin.Begin);
				text = stringBuilder.ToString();
				stringBuilder.Length = 0;
				stringBuilder.Capacity = 0;
			}
			else if (inputStream is FileStream { Name: var name })
			{
				try
				{
					if (File.Exists(name))
					{
						text = File.ReadAllText(name);
					}
				}
				catch
				{
				}
			}
		}
		if (text == null && !string.IsNullOrEmpty(filename) && string.Compare(filename, "@@inner_string@@", StringComparison.Ordinal) != 0)
		{
			try
			{
				if (File.Exists(filename))
				{
					text = File.ReadAllText(filename);
				}
			}
			catch
			{
			}
		}
		return text;
	}

	private Type GetRootBuilderType(Stream inputStream, string filename)
	{
		Type type = null;
		string text = ((tparser == null) ? null : ReadFileContents(inputStream, filename));
		if (!string.IsNullOrEmpty(text))
		{
			Type inheritedType = GetInheritedType(text, filename);
			text = null;
			if (inheritedType != null)
			{
				FileLevelControlBuilderAttribute fileLevelControlBuilderAttribute;
				try
				{
					object[] customAttributes = inheritedType.GetCustomAttributes(typeof(FileLevelControlBuilderAttribute), inherit: true);
					fileLevelControlBuilderAttribute = ((customAttributes == null || customAttributes.Length == 0) ? null : (customAttributes[0] as FileLevelControlBuilderAttribute));
				}
				catch
				{
					fileLevelControlBuilderAttribute = null;
				}
				type = fileLevelControlBuilderAttribute?.BuilderType;
			}
		}
		if (type == null)
		{
			if (tparser is PageParser)
			{
				return typeof(FileLevelPageControlBuilder);
			}
			if (tparser is UserControlParser)
			{
				return typeof(FileLevelUserControlBuilder);
			}
			return typeof(RootBuilder);
		}
		return type;
	}

	private void CreateRootBuilder(Stream inputStream, string filename)
	{
		if (rootBuilder == null)
		{
			Type rootBuilderType = GetRootBuilderType(inputStream, filename);
			rootBuilder = Activator.CreateInstance(rootBuilderType) as RootBuilder;
			if (rootBuilder == null)
			{
				throw new HttpException("Cannot create an instance of file-level control builder.");
			}
			rootBuilder.Init(tparser, null, null, null, null, null);
			if (componentFoundry != null)
			{
				rootBuilder.Foundry = componentFoundry;
			}
			stack.Push(rootBuilder, null);
			tparser.RootBuilder = rootBuilder;
		}
	}

	private BaseCompiler GetCompilerFromType()
	{
		Type type = tparser.GetType();
		if (type == typeof(PageParser))
		{
			return new PageCompiler((PageParser)tparser);
		}
		if (type == typeof(ApplicationFileParser))
		{
			return new GlobalAsaxCompiler((ApplicationFileParser)tparser);
		}
		if (type == typeof(UserControlParser))
		{
			return new UserControlCompiler((UserControlParser)tparser);
		}
		if (type == typeof(MasterPageParser))
		{
			return new MasterPageCompiler((MasterPageParser)tparser);
		}
		throw new Exception("Got type: " + type);
	}

	private void InitParser(TextReader reader, string filename)
	{
		AspParser aspParser = new AspParser(filename, reader);
		aspParser.Error += ParseError;
		aspParser.TagParsed += TagParsed;
		aspParser.TextParsed += TextParsed;
		aspParser.ParsingComplete += ParsingCompleted;
		tparser.AspGenerator = this;
		CreateRootBuilder(inputStream, filename);
		if (!pstack.Push(aspParser))
		{
			throw new ParseException(Location, "Infinite recursion detected including file: " + filename);
		}
		if (filename != "@@inner_string@@")
		{
			string text = Path.Combine(tparser.BaseVirtualDir, Path.GetFileName(filename));
			if (VirtualPathUtility.IsAbsolute(text))
			{
				text = VirtualPathUtility.ToAppRelative(text);
			}
			tparser.AddDependency(text);
		}
	}

	private void InitParser(string filename)
	{
		StreamReader reader = new StreamReader(filename, WebEncoding.FileEncoding);
		InitParser(reader, filename);
	}

	private void CheckForDuplicateIds(ControlBuilder root, Stack scopes)
	{
		if (root == null)
		{
			return;
		}
		if (scopes == null)
		{
			scopes = new Stack();
		}
		Dictionary<string, bool> dictionary;
		if (scopes.Count == 0 || root.IsNamingContainer)
		{
			dictionary = new Dictionary<string, bool>(StringComparer.Ordinal);
			scopes.Push(dictionary);
		}
		else
		{
			dictionary = scopes.Peek() as Dictionary<string, bool>;
		}
		if (dictionary == null)
		{
			return;
		}
		ArrayList children = root.Children;
		if (children == null)
		{
			return;
		}
		foreach (object item in children)
		{
			if (item is ControlBuilder { ID: { Length: not 0 } iD } controlBuilder)
			{
				if (dictionary.ContainsKey(iD))
				{
					throw new ParseException(controlBuilder.Location, "Id '" + iD + "' is already used by another control.");
				}
				dictionary.Add(iD, value: true);
				CheckForDuplicateIds(controlBuilder, scopes);
			}
		}
	}

	public void Parse(string file)
	{
		Parse(file, doInitParser: false);
	}

	public void Parse(TextReader reader, string filename, bool doInitParser)
	{
		try
		{
			isApplication = tparser.DefaultDirectiveName == "application";
			if (doInitParser)
			{
				InitParser(reader, filename);
			}
			pstack.Parser.Parse();
			if (text.Length > 0)
			{
				FlushText();
			}
			tparser.MD5Checksum = pstack.Parser.MD5Checksum;
			pstack.Pop();
			if (stack.Count > 1 && pstack.Count == 0)
			{
				throw new ParseException(stack.Builder.Location, "Expecting </" + stack.Builder.TagName + "> " + stack.Builder);
			}
			CheckForDuplicateIds(RootBuilder, null);
		}
		finally
		{
			reader?.Close();
		}
	}

	public void Parse(Stream stream, string filename, bool doInitParser)
	{
		inputStream = stream;
		Parse(new StreamReader(stream, WebEncoding.FileEncoding), filename, doInitParser);
	}

	public void Parse(string filename, bool doInitParser)
	{
		StreamReader reader = new StreamReader(filename, WebEncoding.FileEncoding);
		Parse(reader, filename, doInitParser);
	}

	public void Parse()
	{
		string text = tparser.InputFile;
		TextReader reader = tparser.Reader;
		try
		{
			if (string.IsNullOrEmpty(text))
			{
				if (reader is StreamReader { BaseStream: FileStream baseStream })
				{
					text = baseStream.Name;
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "@@inner_string@@";
				}
			}
			if (reader != null)
			{
				Parse(reader, text, doInitParser: true);
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new HttpException("Parser input file is empty, cannot continue.");
			}
			text = Path.GetFullPath(text);
			InitParser(text);
			Parse(text);
		}
		finally
		{
			reader?.Close();
		}
	}

	internal static void AddTypeToCache(List<string> dependencies, string inputFile, Type type)
	{
		if (type == null || inputFile == null || inputFile.Length == 0)
		{
			return;
		}
		if (dependencies != null && dependencies.Count > 0)
		{
			string[] array = dependencies.ToArray();
			HttpRequest httpRequest = HttpContext.Current?.Request;
			if (httpRequest == null)
			{
				throw new HttpException("No current context, cannot compile.");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = httpRequest.MapPath(array[i]);
			}
			HttpRuntime.InternalCache.Insert("@@Type" + inputFile, type, new CacheDependency(array));
		}
		else
		{
			HttpRuntime.InternalCache.Insert("@@Type" + inputFile, type);
		}
	}

	public Type GetCompiledType()
	{
		Type type = (Type)HttpRuntime.InternalCache.Get("@@Type" + tparser.InputFile);
		if (type != null)
		{
			return type;
		}
		Parse();
		type = GetCompilerFromType().GetCompiledType();
		AddTypeToCache(tparser.Dependencies, tparser.InputFile, type);
		return type;
	}

	private void ParseError(ILocation location, string message)
	{
		throw new ParseException(location, message);
	}

	private bool ProcessTagsInAttributes(ILocation location, string tagid, TagAttributes attributes, TagType type)
	{
		if (attributes == null || attributes.Count == 0)
		{
			return false;
		}
		bool flag = false;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("\t<{0}", tagid);
		foreach (string key in attributes.Keys)
		{
			string text2 = attributes[key] as string;
			if (text2 == null || text2.Length < 16)
			{
				stringBuilder.AppendFormat(" {0}=\"{1}\"", key, text2);
				continue;
			}
			Match match = runatServer.Match(attributes[key] as string);
			if (!match.Success)
			{
				stringBuilder.AppendFormat(" {0}=\"{1}\"", key, text2);
				continue;
			}
			if (stringBuilder.Length > 0)
			{
				TextParsed(location, stringBuilder.ToString());
				stringBuilder.Length = 0;
			}
			flag = true;
			Group group = match.Groups[0];
			int index = group.Index;
			int length = group.Length;
			TextParsed(location, $" {key}=\"{((index > 0) ? text2.Substring(0, index) : string.Empty)}");
			FlushText();
			ParseAttributeTag(group.Value, location);
			if (index + length < text2.Length)
			{
				TextParsed(location, text2.Substring(index + length) + "\"");
			}
			else
			{
				TextParsed(location, "\"");
			}
		}
		if (type == TagType.SelfClosing)
		{
			stringBuilder.Append("/>");
		}
		else
		{
			stringBuilder.Append(">");
		}
		if (flag && stringBuilder.Length > 0)
		{
			TextParsed(location, stringBuilder.ToString());
		}
		return flag;
	}

	private void ParseAttributeTag(string code, ILocation location)
	{
		AspParser aspParser = location as AspParser;
		int positionOffset = aspParser?.BeginPosition ?? 0;
		AspParser aspParser2 = new AspParser("@@attribute_tag@@", new StringReader(code), location.BeginLine - 1, positionOffset, aspParser);
		aspParser2.Error += ParseError;
		aspParser2.TagParsed += TagParsed;
		aspParser2.TextParsed += TextParsed;
		aspParser2.Parse();
		if (text.Length > 0)
		{
			FlushText();
		}
	}

	private void ParsingCompleted()
	{
		PageParserFilter?.ParseComplete(RootBuilder);
	}

	private void CheckIfIncludeFileIsSecure(string filePath)
	{
		if (filePath == null || filePath.Length == 0)
		{
			return;
		}
		string text = null;
		Exception ex = null;
		try
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(Path.GetDirectoryName(filePath));
			text = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory(currentDirectory);
			if (text[text.Length - 1] != '/')
			{
				text += "/";
			}
		}
		catch (DirectoryNotFoundException)
		{
			return;
		}
		catch (FileNotFoundException)
		{
			return;
		}
		catch (Exception ex4)
		{
			ex = ex4;
		}
		if (ex == null && StrUtils.StartsWith(text, HttpRuntime.AppDomainAppPath))
		{
			return;
		}
		throw new ParseException(Location, "Files above the application's root directory cannot be included.");
	}

	private string ChopOffTagStart(ILocation location, string content, string tagid)
	{
		string text = "<" + tagid;
		if (content.StartsWith(text))
		{
			TextParsed(location, text);
			content = content.Substring(text.Length);
		}
		return content;
	}

	private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
	{
		this.location = new Location(location);
		if (tparser != null)
		{
			tparser.Location = location;
		}
		if (this.text.Length != 0)
		{
			bool ignoreEmptyString = lastTag == TagType.CodeRender;
			FlushText(ignoreEmptyString);
		}
		if (string.Compare(tagid, "script", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			if (inScript || ignore_text)
			{
				if (ProcessScript(tagtype, attributes))
				{
					return;
				}
			}
			else if (ProcessScript(tagtype, attributes))
			{
				return;
			}
		}
		lastTag = tagtype;
		bool ignored;
		switch (tagtype)
		{
		case TagType.Directive:
			if (tagid.Length == 0)
			{
				tagid = tparser.DefaultDirectiveName;
			}
			tparser.AddDirective(tagid, attributes.GetDictionary(null));
			break;
		case TagType.Tag:
		{
			if (ProcessTag(location, tagid, attributes, tagtype, out ignored))
			{
				if (!ignored)
				{
					useOtherTags = true;
				}
				break;
			}
			if (useOtherTags)
			{
				stack.Builder.EnsureOtherTags();
				stack.Builder.OtherTags.Add(tagid);
			}
			string plainText2 = location.PlainText;
			if (!ProcessTagsInAttributes(location, tagid, attributes, TagType.Tag))
			{
				TextParsed(location, ChopOffTagStart(location, plainText2, tagid));
			}
			break;
		}
		case TagType.Close:
			if ((useOtherTags && TryRemoveTag(tagid, stack.Builder.OtherTags)) || !CloseControl(tagid))
			{
				TextParsed(location, location.PlainText);
			}
			break;
		case TagType.SelfClosing:
		{
			int count = stack.Count;
			if (!ProcessTag(location, tagid, attributes, tagtype, out ignored) && !ignored)
			{
				string plainText = location.PlainText;
				if (!ProcessTagsInAttributes(location, tagid, attributes, TagType.SelfClosing))
				{
					TextParsed(location, ChopOffTagStart(location, plainText, tagid));
				}
			}
			else if (stack.Count != count)
			{
				CloseControl(tagid);
			}
			break;
		}
		case TagType.DataBinding:
		case TagType.CodeRender:
		case TagType.CodeRenderExpression:
		case TagType.CodeRenderEncode:
			if (isApplication)
			{
				throw new ParseException(location, "Invalid content for application file.");
			}
			ProcessCode(tagtype, tagid, location);
			break;
		case TagType.Include:
		{
			if (isApplication)
			{
				throw new ParseException(location, "Invalid content for application file.");
			}
			string text = attributes["virtual"] as string;
			bool num = text != null;
			if (!num)
			{
				text = attributes["file"] as string;
			}
			if (num)
			{
				bool flag = false;
				VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
				if (virtualPathProvider.FileExists(text))
				{
					VirtualFile file = virtualPathProvider.GetFile(text);
					if (file != null)
					{
						Parse(file.Open(), text, doInitParser: true);
						flag = true;
					}
				}
				if (!flag)
				{
					Parse(tparser.MapPath(text), doInitParser: true);
				}
				break;
			}
			string includeFilePath = GetIncludeFilePath(tparser.ParserDir, text);
			CheckIfIncludeFileIsSecure(includeFilePath);
			tparser.PushIncludeDir(Path.GetDirectoryName(includeFilePath));
			try
			{
				Parse(includeFilePath, doInitParser: true);
				break;
			}
			finally
			{
				tparser.PopIncludeDir();
			}
		}
		case TagType.ServerComment:
			break;
		}
	}

	private static bool TryRemoveTag(string tagid, ArrayList otags)
	{
		if (otags == null || otags.Count == 0)
		{
			return false;
		}
		for (int num = otags.Count - 1; num >= 0; num--)
		{
			string strB = (string)otags[num];
			if (string.Compare(tagid, strB, ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				do
				{
					otags.RemoveAt(num);
				}
				while (otags.Count - 1 >= num);
				return true;
			}
		}
		return false;
	}

	private static string GetIncludeFilePath(string basedir, string filename)
	{
		if (Path.DirectorySeparatorChar == '/')
		{
			filename = filename.Replace("\\", "/");
		}
		return Path.GetFullPath(Path.Combine(basedir, filename));
	}

	private bool CheckTagEndNeeded(string text)
	{
		return !text.EndsWith("/>");
	}

	private List<TextBlock> FindRegexBlocks(Regex rxStart, Regex rxEnd, CheckBlockEnd checkEnd, IList blocks, TextBlockType typeForMatches, bool discardBlocks)
	{
		List<TextBlock> list = new List<TextBlock>();
		foreach (TextBlock block in blocks)
		{
			if (block.Type != 0)
			{
				list.Add(block);
				continue;
			}
			int num = 0;
			MatchCollection matchCollection = rxStart.Matches(block.Content);
			bool flag = matchCollection.Count > 0;
			foreach (Match item in matchCollection)
			{
				flag = true;
				int index = item.Index;
				if (num < index)
				{
					list.Add(new TextBlock(TextBlockType.Verbatim, block.Content.Substring(num, index - num)));
				}
				string text = item.Value;
				if (rxEnd != null && checkEnd(text))
				{
					int num2 = index + text.Length;
					Match match = rxEnd.Match(block.Content, num2);
					if (match.Success)
					{
						text = text + block.Content.Substring(num2, match.Index - num2) + match.Value;
					}
				}
				if (!discardBlocks)
				{
					list.Add(new TextBlock(typeForMatches, text));
				}
				num = index + text.Length;
			}
			if (num > 0 && num < block.Content.Length)
			{
				list.Add(new TextBlock(TextBlockType.Verbatim, block.Content.Substring(num)));
			}
			if (!flag)
			{
				list.Add(block);
			}
		}
		return list;
	}

	private IList SplitTextIntoBlocks(string text)
	{
		List<TextBlock> list = new List<TextBlock>();
		list.Add(new TextBlock(TextBlockType.Verbatim, text));
		list = FindRegexBlocks(clientCommentRegex, null, null, list, TextBlockType.Comment, discardBlocks: false);
		list = FindRegexBlocks(runatServer, endOfTag, CheckTagEndNeeded, list, TextBlockType.Tag, discardBlocks: false);
		return FindRegexBlocks(expressionRegex, null, null, list, TextBlockType.Expression, discardBlocks: false);
	}

	private void TextParsed(ILocation location, string text)
	{
		if (ignore_text)
		{
			return;
		}
		if (inScript)
		{
			this.text.Append(text);
			FlushText(ignoreEmptyString: true);
			return;
		}
		foreach (TextBlock item in SplitTextIntoBlocks(text))
		{
			switch (item.Type)
			{
			case TextBlockType.Verbatim:
				this.text.Append(item.Content);
				break;
			case TextBlockType.Expression:
				if (this.text.Length > 0)
				{
					FlushText(ignoreEmptyString: true);
				}
				new CodeRenderParser(item.Content, stack.Builder, location).AddChildren(this);
				break;
			case TextBlockType.Tag:
				ParseAttributeTag(item.Content, location);
				break;
			case TextBlockType.Comment:
			{
				if (javascript)
				{
					this.text.Append(item.Content);
					break;
				}
				this.text.Append("<!--");
				FlushText(ignoreEmptyString: true);
				string text2 = item.Content.Substring(4, item.Length - 7);
				bool flag;
				if (text2.EndsWith("<![endif]"))
				{
					text2 = text2.Substring(0, text2.Length - 9);
					flag = true;
				}
				else
				{
					flag = false;
				}
				AspParser aspParser = location as AspParser;
				int positionOffset = aspParser?.BeginPosition ?? 0;
				AspParser aspParser2 = new AspParser("@@comment_code@@", new StringReader(text2), location.BeginLine - 1, positionOffset, aspParser);
				aspParser2.Error += ParseError;
				aspParser2.TagParsed += TagParsed;
				aspParser2.TextParsed += TextParsed;
				aspParser2.Parse();
				if (flag)
				{
					this.text.Append("<![endif]");
				}
				this.text.Append("-->");
				FlushText(ignoreEmptyString: true);
				break;
			}
			}
		}
	}

	private void FlushText()
	{
		FlushText(ignoreEmptyString: false);
	}

	private void FlushText(bool ignoreEmptyString)
	{
		string text = this.text.ToString();
		this.text.Length = 0;
		if (ignoreEmptyString && text.Trim().Length == 0)
		{
			return;
		}
		if (inScript)
		{
			PageParserFilter pageParserFilter = PageParserFilter;
			if (pageParserFilter == null || pageParserFilter.ProcessCodeConstruct(CodeConstructType.ScriptTag, text))
			{
				tparser.Scripts.Add(new ServerSideScript(text, new Location(tparser.Location)));
			}
			return;
		}
		if (tparser.DefaultDirectiveName == "application" && text.Trim() != "")
		{
			throw new ParseException(location, "Content not valid for application file.");
		}
		ControlBuilder builder = stack.Builder;
		builder.AppendLiteralString(text);
		if (builder.NeedsTagInnerText())
		{
			tagInnerText.Append(text);
		}
	}

	private bool BuilderHasOtherThan(Type type, ControlBuilder cb)
	{
		ArrayList otherTags = cb.OtherTags;
		if (otherTags != null && otherTags.Count > 0)
		{
			return true;
		}
		otherTags = cb.Children;
		if (otherTags != null)
		{
			foreach (object item in otherTags)
			{
				if (item == null)
				{
					continue;
				}
				if (!(item is ControlBuilder controlBuilder))
				{
					if (!(item is string text) || !string.IsNullOrEmpty(text.Trim()))
					{
						return true;
					}
				}
				else if (!(controlBuilder is ContentBuilderInternal) && controlBuilder.ControlType != typeof(Content))
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool OtherControlsAllowed(ControlBuilder cb)
	{
		if (cb == null)
		{
			return true;
		}
		if (!typeof(Content).IsAssignableFrom(cb.ControlType))
		{
			return true;
		}
		if (BuilderHasOtherThan(typeof(Content), RootBuilder))
		{
			return false;
		}
		return true;
	}

	public void AddControl(Type type, IDictionary attributes)
	{
		ControlBuilder builder = stack.Builder;
		ControlBuilder controlBuilder = ControlBuilder.CreateBuilderFromType(tparser, builder, type, null, null, attributes, location.BeginLine, location.Filename);
		if (controlBuilder != null)
		{
			builder.AppendSubBuilder(controlBuilder);
		}
	}

	private bool ProcessTag(ILocation location, string tagid, TagAttributes atts, TagType tagtype, out bool ignored)
	{
		ignored = false;
		if (isApplication && string.Compare(tagid, "object", ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			throw new ParseException(location, "Invalid tag for application file.");
		}
		ControlBuilder builder = stack.Builder;
		ControlBuilder controlBuilder = null;
		if (builder != null && builder.ControlType == typeof(HtmlTable) && (string.Compare(tagid, "thead", ignoreCase: true, Helpers.InvariantCulture) == 0 || string.Compare(tagid, "tbody", ignoreCase: true, Helpers.InvariantCulture) == 0))
		{
			ignored = true;
			return true;
		}
		IDictionary dictionary = ((atts != null) ? atts.GetDictionary(null) : emptyHash);
		if (stack.Count > 1)
		{
			try
			{
				controlBuilder = builder.CreateSubBuilder(tagid, dictionary, null, tparser, location);
			}
			catch (TypeLoadException inner)
			{
				throw new ParseException(Location, "Type not found.", inner);
			}
			catch (Exception ex)
			{
				throw new ParseException(Location, ex.Message, ex);
			}
		}
		bool flag = atts?.IsRunAtServer() ?? false;
		if (controlBuilder == null && flag)
		{
			if (dictionary["id"] is string text && !CodeGenerator.IsValidLanguageIndependentIdentifier(text))
			{
				throw new ParseException(Location, "'" + text + "' is not a valid identifier");
			}
			try
			{
				controlBuilder = RootBuilder.CreateSubBuilder(tagid, dictionary, null, tparser, location);
			}
			catch (TypeLoadException inner2)
			{
				throw new ParseException(Location, "Type not found.", inner2);
			}
			catch (HttpException ex2)
			{
				if (ex2.InnerException is CompilationException ex3)
				{
					throw ex3;
				}
				throw new ParseException(Location, ex2.Message, ex2);
			}
			catch (Exception ex4)
			{
				throw new ParseException(Location, ex4.Message, ex4);
			}
		}
		if (controlBuilder == null)
		{
			return false;
		}
		string plainText = location.PlainText;
		if (!flag && plainText.IndexOf("<%$") == -1 && plainText.IndexOf("<%") > -1)
		{
			return false;
		}
		PageParserFilter pageParserFilter = PageParserFilter;
		if (pageParserFilter != null && !pageParserFilter.AllowControl(controlBuilder.ControlType, controlBuilder))
		{
			throw new ParseException(Location, string.Concat("Control type '", controlBuilder.ControlType, "' not allowed."));
		}
		if (!OtherControlsAllowed(controlBuilder))
		{
			throw new ParseException(Location, "Only Content controls are allowed directly in a content page that contains Content controls.");
		}
		controlBuilder.Location = location;
		controlBuilder.ID = dictionary["id"] as string;
		if (typeof(HtmlForm).IsAssignableFrom(controlBuilder.ControlType))
		{
			if (inForm)
			{
				throw new ParseException(location, "Only one <form> allowed.");
			}
			inForm = true;
		}
		if (controlBuilder.HasBody() && !(controlBuilder is ObjectTagBuilder))
		{
			_ = controlBuilder is TemplateBuilder;
			stack.Push(controlBuilder, location);
		}
		else
		{
			if (!isApplication && controlBuilder is ObjectTagBuilder)
			{
				ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)controlBuilder;
				if (objectTagBuilder.Scope != null && objectTagBuilder.Scope.Length > 0)
				{
					throw new ParseException(location, "Scope not allowed here");
				}
				if (tagtype == TagType.Tag)
				{
					stack.Push(controlBuilder, location);
					return true;
				}
			}
			builder.AppendSubBuilder(controlBuilder);
			controlBuilder.CloseControl();
		}
		return true;
	}

	private string ReadFile(string filename)
	{
		using StreamReader streamReader = new StreamReader(tparser.MapPath(filename), WebEncoding.FileEncoding);
		return streamReader.ReadToEnd();
	}

	private bool ProcessScript(TagType tagtype, TagAttributes attributes)
	{
		if (tagtype != TagType.Close)
		{
			if (attributes != null && attributes.IsRunAtServer())
			{
				string text = (string)attributes["language"];
				if (text != null && text.Length > 0 && tparser.ImplicitLanguage)
				{
					tparser.SetLanguage(text);
				}
				CheckLanguage(text);
				string text2 = (string)attributes["src"];
				if (text2 != null)
				{
					if (text2.Length == 0)
					{
						throw new ParseException(Parser, "src cannot be an empty string");
					}
					string text3 = ReadFile(text2);
					inScript = true;
					TextParsed(Parser, text3);
					FlushText();
					inScript = false;
					if (tagtype != TagType.SelfClosing)
					{
						ignore_text = true;
						Parser.VerbatimID = "script";
					}
				}
				else if (tagtype == TagType.Tag)
				{
					Parser.VerbatimID = "script";
					inScript = true;
				}
				return true;
			}
			if (tagtype != TagType.SelfClosing)
			{
				Parser.VerbatimID = "script";
				javascript = true;
			}
			string text4 = location.PlainText;
			if (text4.StartsWith("<script"))
			{
				TextParsed(location, "<script");
				text4 = text4.Substring(7);
			}
			TextParsed(location, text4);
			return true;
		}
		bool result;
		if (inScript)
		{
			result = inScript;
			inScript = false;
		}
		else if (!ignore_text)
		{
			result = javascript;
			javascript = false;
			TextParsed(location, location.PlainText);
		}
		else
		{
			ignore_text = false;
			result = true;
		}
		return result;
	}

	private bool CloseControl(string tagid)
	{
		ControlBuilder builder = stack.Builder;
		string originalTagName = builder.OriginalTagName;
		if (string.Compare(originalTagName, "tbody", ignoreCase: true, Helpers.InvariantCulture) != 0 && string.Compare(tagid, "tbody", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			if (!builder.ChildrenAsProperties)
			{
				try
				{
					TextParsed(location, location.PlainText);
					FlushText();
				}
				catch
				{
				}
			}
			return true;
		}
		if (builder.ControlType == typeof(HtmlTable) && string.Compare(tagid, "thead", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			return true;
		}
		if (string.Compare(tagid, originalTagName, ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			return false;
		}
		if (builder.NeedsTagInnerText())
		{
			try
			{
				builder.SetTagInnerText(tagInnerText.ToString());
			}
			catch (Exception ex)
			{
				throw new ParseException(builder.Location, ex.Message, ex);
			}
			tagInnerText.Length = 0;
		}
		if (typeof(HtmlForm).IsAssignableFrom(builder.ControlType))
		{
			inForm = false;
		}
		builder.CloseControl();
		stack.Pop();
		stack.Builder.AppendSubBuilder(builder);
		return true;
	}

	private CodeConstructType MapTagTypeToConstructType(TagType tagtype)
	{
		switch (tagtype)
		{
		case TagType.CodeRenderExpression:
			return CodeConstructType.ExpressionSnippet;
		case TagType.CodeRender:
		case TagType.CodeRenderEncode:
			return CodeConstructType.CodeSnippet;
		case TagType.DataBinding:
			return CodeConstructType.DataBindingSnippet;
		default:
			throw new InvalidOperationException("Unexpected tag type.");
		}
	}

	private bool ProcessCode(TagType tagtype, string code, ILocation location)
	{
		PageParserFilter pageParserFilter = PageParserFilter;
		if (pageParserFilter != null && (!pageParserFilter.AllowCode || pageParserFilter.ProcessCodeConstruct(MapTagTypeToConstructType(tagtype), code)))
		{
			return true;
		}
		ControlBuilder controlBuilder = null;
		controlBuilder = tagtype switch
		{
			TagType.CodeRender => new CodeRenderBuilder(code, isAssign: false, location), 
			TagType.CodeRenderExpression => new CodeRenderBuilder(code, isAssign: true, location), 
			TagType.DataBinding => new DataBindingBuilder(code, location), 
			TagType.CodeRenderEncode => new CodeRenderBuilder(code, isAssign: true, location, doHtmlEncode: true), 
			_ => throw new HttpException("Should never happen"), 
		};
		stack.Builder.AppendSubBuilder(controlBuilder);
		return true;
	}

	private void CheckLanguage(string lang)
	{
		if (lang != null && !(lang == "") && string.Compare(lang, tparser.Language, ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			if (compilationSection.Compilers[tparser.Language] != compilationSection.Compilers[lang])
			{
				throw new ParseException(Location, $"Trying to mix language '{tparser.Language}' and '{lang}'.");
			}
		}
	}
}
