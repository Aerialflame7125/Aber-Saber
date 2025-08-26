using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

internal abstract class SimpleBuildProvider : GenericBuildProvider<SimpleWebHandlerParser>
{
	private bool _parsed;

	private bool _needLoadFromBin;

	protected override bool NeedsConstructType => false;

	protected override bool NeedsLoadFromBin => _needLoadFromBin;

	protected override SimpleWebHandlerParser Parse()
	{
		SimpleWebHandlerParser parser = base.Parser;
		if (_parsed)
		{
			return parser;
		}
		_parsed = true;
		return parser;
	}

	protected override void GenerateCode(AssemblyBuilder assemblyBuilder, SimpleWebHandlerParser parser, BaseCompiler compiler)
	{
		if (assemblyBuilder == null || parser == null)
		{
			return;
		}
		string value = parser.Program.Trim();
		if (string.IsNullOrEmpty(value))
		{
			_needLoadFromBin = true;
			return;
		}
		_needLoadFromBin = false;
		using TextWriter textWriter = assemblyBuilder.CreateCodeFile(this);
		textWriter.WriteLine(value);
	}

	protected override Type LoadTypeFromBin(BaseCompiler compiler, SimpleWebHandlerParser parser)
	{
		return parser.GetTypeFromBin(parser.ClassName);
	}

	protected override string GetClassType(BaseCompiler compiler, SimpleWebHandlerParser parser)
	{
		return parser?.ClassName;
	}

	protected override ICollection GetParserDependencies(SimpleWebHandlerParser parser)
	{
		return parser?.Dependencies;
	}

	protected override string GetParserLanguage(SimpleWebHandlerParser parser)
	{
		return parser?.Language;
	}

	protected override string GetCodeBehindSource(SimpleWebHandlerParser parser)
	{
		return null;
	}

	protected override AspGenerator CreateAspGenerator(SimpleWebHandlerParser parser)
	{
		return null;
	}

	protected override BaseCompiler CreateCompiler(SimpleWebHandlerParser parser)
	{
		return new WebServiceCompiler(parser);
	}

	protected override List<string> GetReferencedAssemblies(SimpleWebHandlerParser parser)
	{
		if (parser == null)
		{
			return null;
		}
		ArrayList assemblies = parser.Assemblies;
		if (assemblies == null || assemblies.Count == 0)
		{
			return null;
		}
		List<string> list = new List<string>();
		foreach (object item2 in assemblies)
		{
			if (item2 is string item && !list.Contains(item))
			{
				list.Add(item);
			}
		}
		return list;
	}
}
