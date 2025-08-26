using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace System.Web.Compilation;

internal abstract class GenericBuildProvider<TParser> : BuildProvider
{
	private TParser _parser;

	private CompilerType _compilerType;

	private BaseCompiler _compiler;

	private TextReader _reader;

	private bool _parsed;

	private bool _codeGenerated;

	protected bool Parsed => _parsed;

	public override ICollection VirtualPathDependencies
	{
		get
		{
			TParser parser = Parser;
			return GetParserDependencies(parser);
		}
	}

	internal override string LanguageName
	{
		get
		{
			TParser val = Parse();
			if (val != null)
			{
				return GetParserLanguage(val);
			}
			return base.LanguageName;
		}
	}

	public override CompilerType CodeCompilerType
	{
		get
		{
			if (_compilerType == null)
			{
				_compilerType = GetDefaultCompilerTypeForLanguage(LanguageName);
			}
			return _compilerType;
		}
	}

	public TParser Parser
	{
		get
		{
			if (_parser == null)
			{
				VirtualPath virtualPathInternal = base.VirtualPathInternal;
				if (virtualPathInternal == null)
				{
					throw new HttpException("VirtualPath not set, cannot instantiate parser.");
				}
				if (!IsDirectoryBuilder)
				{
					_reader = SpecialOpenReader(virtualPathInternal, out var physicalPath);
					_parser = CreateParser(virtualPathInternal, physicalPath, _reader, HttpContext.Current);
				}
				else
				{
					_parser = CreateParser(virtualPathInternal, null, HttpContext.Current);
				}
				if (_parser == null)
				{
					throw new HttpException("Unable to create type parser.");
				}
			}
			return _parser;
		}
	}

	protected virtual bool IsDirectoryBuilder => false;

	protected virtual bool NeedsConstructType => true;

	protected virtual bool NeedsLoadFromBin => false;

	internal override CodeCompileUnit CodeUnit
	{
		get
		{
			if (!_codeGenerated)
			{
				GenerateCode();
			}
			return _compiler.CompileUnit;
		}
	}

	protected abstract TParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context);

	protected abstract TParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context);

	protected abstract BaseCompiler CreateCompiler(TParser parser);

	protected abstract string GetParserLanguage(TParser parser);

	protected abstract ICollection GetParserDependencies(TParser parser);

	protected abstract string GetCodeBehindSource(TParser parser);

	protected abstract string GetClassType(BaseCompiler compiler, TParser parser);

	protected abstract AspGenerator CreateAspGenerator(TParser parser);

	protected abstract List<string> GetReferencedAssemblies(TParser parser);

	protected virtual string MapPath(VirtualPath virtualPath)
	{
		return (HttpContext.Current?.Request)?.MapPath(base.VirtualPath);
	}

	protected virtual TParser Parse()
	{
		TParser parser = Parser;
		if (_parsed)
		{
			return parser;
		}
		if (!IsDirectoryBuilder)
		{
			AspGenerator aspGenerator = CreateAspGenerator(parser);
			if (_reader != null)
			{
				aspGenerator.Parse(_reader, MapPath(base.VirtualPathInternal), doInitParser: true);
			}
			else
			{
				aspGenerator.Parse();
			}
		}
		_parsed = true;
		return parser;
	}

	protected virtual void OverrideAssemblyPrefix(TParser parser, AssemblyBuilder assemblyBuilder)
	{
	}

	internal override void GenerateCode()
	{
		TParser parser = Parse();
		_compiler = CreateCompiler(parser);
		if (NeedsConstructType)
		{
			_compiler.ConstructType();
		}
		_codeGenerated = true;
	}

	protected virtual void GenerateCode(AssemblyBuilder assemblyBuilder, TParser parser, BaseCompiler compiler)
	{
		CodeCompileUnit compileUnit = _compiler.CompileUnit;
		if (compileUnit == null)
		{
			throw new HttpException("Unable to generate source code.");
		}
		assemblyBuilder.AddCodeCompileUnit(this, compileUnit);
	}

	public override void GenerateCode(AssemblyBuilder assemblyBuilder)
	{
		if (!_codeGenerated)
		{
			GenerateCode();
		}
		TParser parser = Parse();
		OverrideAssemblyPrefix(parser, assemblyBuilder);
		string codeBehindSource = GetCodeBehindSource(parser);
		if (codeBehindSource != null)
		{
			assemblyBuilder.AddCodeFile(codeBehindSource, this, isVirtual: true);
		}
		List<string> referencedAssemblies = GetReferencedAssemblies(parser);
		if (referencedAssemblies != null && referencedAssemblies.Count > 0)
		{
			foreach (string item in referencedAssemblies)
			{
				assemblyBuilder.AddAssemblyReference(item);
			}
		}
		GenerateCode(assemblyBuilder, parser, _compiler);
	}

	protected virtual Type LoadTypeFromBin(BaseCompiler compiler, TParser parser)
	{
		return null;
	}

	public override Type GetGeneratedType(CompilerResults results)
	{
		if (NeedsLoadFromBin && _compiler != null)
		{
			return LoadTypeFromBin(_compiler, Parser);
		}
		Type type = null;
		Assembly assembly = results?.CompiledAssembly;
		if (assembly != null)
		{
			type = assembly.GetType(GetClassType(_compiler, Parser));
		}
		if (type == null)
		{
			throw new HttpException(500, $"Type {GetClassType(_compiler, Parser)} could not be loaded");
		}
		return type;
	}

	protected virtual TextReader SpecialOpenReader(VirtualPath virtualPath, out string physicalPath)
	{
		physicalPath = null;
		return OpenReader(virtualPath.Original);
	}
}
