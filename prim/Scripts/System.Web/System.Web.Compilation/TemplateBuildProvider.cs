using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation;

internal abstract class TemplateBuildProvider : GenericBuildProvider<TemplateParser>
{
	private delegate void ExtractDirectiveDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp);

	private static SortedDictionary<string, ExtractDirectiveDependencies> directiveAttributes;

	private static char[] directiveValueTrimChars;

	private SortedDictionary<string, bool> dependencies;

	private string compilationLanguage;

	internal override string LanguageName
	{
		get
		{
			if (string.IsNullOrEmpty(compilationLanguage))
			{
				ExtractDependencies();
				if (string.IsNullOrEmpty(compilationLanguage))
				{
					compilationLanguage = base.LanguageName;
				}
			}
			return compilationLanguage;
		}
	}

	static TemplateBuildProvider()
	{
		directiveValueTrimChars = new char[6] { ' ', '\t', '\r', '\n', '"', '\'' };
		directiveAttributes = new SortedDictionary<string, ExtractDirectiveDependencies>(StringComparer.InvariantCultureIgnoreCase);
		directiveAttributes.Add("Control", ExtractControlDependencies);
		directiveAttributes.Add("Master", ExtractPageOrMasterDependencies);
		directiveAttributes.Add("MasterType", ExtractPreviousPageTypeOrMasterTypeDependencies);
		directiveAttributes.Add("Page", ExtractPageOrMasterDependencies);
		directiveAttributes.Add("PreviousPageType", ExtractPreviousPageTypeOrMasterTypeDependencies);
		directiveAttributes.Add("Reference", ExtractReferenceDependencies);
		directiveAttributes.Add("Register", ExtractRegisterDependencies);
		directiveAttributes.Add("WebHandler", ExtractLanguage);
		directiveAttributes.Add("WebService", ExtractLanguage);
	}

	private static string ExtractDirectiveAttribute(string baseDirectory, string name, CaptureCollection names, CaptureCollection values)
	{
		return ExtractDirectiveAttribute(baseDirectory, name, names, values, isPath: true);
	}

	private static string ExtractDirectiveAttribute(string baseDirectory, string name, CaptureCollection names, CaptureCollection values, bool isPath)
	{
		if (names.Count == 0)
		{
			return string.Empty;
		}
		int num = 0;
		int count = values.Count;
		foreach (Capture name2 in names)
		{
			if (string.Compare(name2.Value, name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				num++;
				continue;
			}
			if (num > count)
			{
				return string.Empty;
			}
			if (isPath)
			{
				string value = values[num].Value.Trim(directiveValueTrimChars);
				if (string.IsNullOrEmpty(value))
				{
					return string.Empty;
				}
				return new VirtualPath(value, baseDirectory).Absolute;
			}
			return values[num].Value.Trim();
		}
		return string.Empty;
	}

	private static void ExtractControlDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		ExtractLanguage(baseDirectory, names, values, bp);
		ExtractCodeBehind(baseDirectory, names, values, bp);
	}

	private static void ExtractLanguage(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		string value = ExtractDirectiveAttribute(baseDirectory, "Language", names, values, isPath: false);
		if (!string.IsNullOrEmpty(value))
		{
			bp.compilationLanguage = value;
			ExtractCodeBehind(baseDirectory, names, values, bp);
		}
	}

	private static void ExtractPageOrMasterDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		ExtractLanguage(baseDirectory, names, values, bp);
		string text = ExtractDirectiveAttribute(baseDirectory, "MasterPageFile", names, values);
		if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
		{
			bp.dependencies.Add(text, value: true);
		}
		ExtractCodeBehind(baseDirectory, names, values, bp);
	}

	private static void ExtractCodeBehind(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		string[] array = new string[2]
		{
			ExtractDirectiveAttribute(baseDirectory, "CodeFile", names, values),
			ExtractDirectiveAttribute(baseDirectory, "Src", names, values)
		};
		foreach (string text in array)
		{
			if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
			{
				bp.dependencies.Add(text, value: true);
			}
		}
	}

	private static void ExtractRegisterDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		string text = ExtractDirectiveAttribute(baseDirectory, "Src", names, values);
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(ExtractDirectiveAttribute(baseDirectory, "TagName", names, values)) && !string.IsNullOrEmpty(ExtractDirectiveAttribute(baseDirectory, "TagPrefix", names, values)) && !bp.dependencies.ContainsKey(text))
		{
			bp.dependencies.Add(text, value: true);
		}
	}

	private static void ExtractPreviousPageTypeOrMasterTypeDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		string text = ExtractDirectiveAttribute(baseDirectory, "VirtualPath", names, values);
		if (!string.IsNullOrEmpty(text) && !bp.dependencies.ContainsKey(text))
		{
			bp.dependencies.Add(text, value: true);
		}
	}

	private static void ExtractReferenceDependencies(string baseDirectory, CaptureCollection names, CaptureCollection values, TemplateBuildProvider bp)
	{
		string text = ExtractDirectiveAttribute(baseDirectory, "Control", names, values);
		string text2 = ExtractDirectiveAttribute(baseDirectory, "VirtualPath", names, values);
		string text3 = ExtractDirectiveAttribute(baseDirectory, "Page", names, values);
		bool flag = string.IsNullOrEmpty(text);
		bool flag2 = string.IsNullOrEmpty(text2);
		bool flag3 = string.IsNullOrEmpty(text3);
		if (!(flag && flag2 && flag3) && (flag ? 1 : 0) + (flag2 ? 1 : 0) + (flag3 ? 1 : 0) == 2)
		{
			string key = ((!flag) ? text : (flag2 ? text3 : text2));
			if (!bp.dependencies.ContainsKey(key))
			{
				bp.dependencies.Add(key, value: true);
			}
		}
	}

	private IDictionary<string, bool> AddParsedDependencies(IDictionary<string, bool> dict)
	{
		if (base.Parsed)
		{
			List<string> list = base.Parser.Dependencies;
			if (list == null || list.Count > 0)
			{
				return dict;
			}
			if (dict == null)
			{
				dict = dependencies;
				if (dict == null)
				{
					dict = (dependencies = new SortedDictionary<string, bool>(StringComparer.OrdinalIgnoreCase));
				}
			}
			foreach (string item in list)
			{
				if (item is string key && !dict.ContainsKey(key))
				{
					dict.Add(key, value: true);
				}
			}
		}
		if (dict == null || dict.Count == 0)
		{
			return null;
		}
		return dict;
	}

	internal override IDictionary<string, bool> ExtractDependencies()
	{
		if (dependencies != null)
		{
			return AddParsedDependencies(dependencies);
		}
		string virtualPath = base.VirtualPath;
		if (string.IsNullOrEmpty(virtualPath))
		{
			return AddParsedDependencies(null);
		}
		VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
		if (!virtualPathProvider.FileExists(virtualPath))
		{
			return AddParsedDependencies(null);
		}
		VirtualFile file = virtualPathProvider.GetFile(virtualPath);
		if (file == null)
		{
			return AddParsedDependencies(null);
		}
		string text;
		using (Stream stream = file.Open())
		{
			if (stream == null || !stream.CanRead)
			{
				return AddParsedDependencies(null);
			}
			using StreamReader streamReader = new StreamReader(stream, WebEncoding.FileEncoding);
			text = streamReader.ReadToEnd();
		}
		if (string.IsNullOrEmpty(text))
		{
			return AddParsedDependencies(null);
		}
		MatchCollection matchCollection = AspGenerator.DirectiveRegex.Matches(text);
		if (matchCollection == null || matchCollection.Count == 0)
		{
			return AddParsedDependencies(null);
		}
		dependencies = new SortedDictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
		string directory = VirtualPathUtility.GetDirectory(virtualPath);
		foreach (Match item in matchCollection)
		{
			GroupCollection groups = item.Groups;
			if (groups.Count >= 6)
			{
				CaptureCollection captures = groups[3].Captures;
				string value = captures[0].Value;
				if (directiveAttributes.TryGetValue(value, out var value2))
				{
					value2(directory, captures, groups[5].Captures, this);
				}
			}
		}
		return AddParsedDependencies(dependencies);
	}

	protected override string GetClassType(BaseCompiler compiler, TemplateParser parser)
	{
		return compiler?.MainClassType;
	}

	protected override ICollection GetParserDependencies(TemplateParser parser)
	{
		return parser?.Dependencies;
	}

	protected override string GetParserLanguage(TemplateParser parser)
	{
		return parser?.Language;
	}

	protected override string GetCodeBehindSource(TemplateParser parser)
	{
		if (parser != null)
		{
			if (string.IsNullOrEmpty(parser.CodeBehindSource))
			{
				return null;
			}
			return parser.CodeBehindSource;
		}
		return null;
	}

	protected override AspGenerator CreateAspGenerator(TemplateParser parser)
	{
		if (parser != null)
		{
			return new AspGenerator(parser);
		}
		return null;
	}

	protected override List<string> GetReferencedAssemblies(TemplateParser parser)
	{
		if (parser == null)
		{
			return null;
		}
		List<string> assemblies = parser.Assemblies;
		if (assemblies == null || assemblies.Count == 0)
		{
			return null;
		}
		List<string> list = new List<string>();
		foreach (string item in assemblies)
		{
			if (!string.IsNullOrEmpty(item) && !list.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}
}
