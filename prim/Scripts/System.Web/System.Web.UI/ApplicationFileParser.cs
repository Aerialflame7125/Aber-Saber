using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.UI;

internal sealed class ApplicationFileParser : TemplateParser
{
	private static List<string> dependencies;

	private TextReader reader;

	internal static List<string> FileDependencies => dependencies;

	internal override Type DefaultBaseType
	{
		get
		{
			Type defaultApplicationBaseType = PageParser.DefaultApplicationBaseType;
			if (defaultApplicationBaseType == null)
			{
				return base.DefaultBaseType;
			}
			return defaultApplicationBaseType;
		}
	}

	internal override string DefaultBaseTypeName => "System.Web.HttpApplication";

	internal override string DefaultDirectiveName => "application";

	internal override string BaseVirtualDir => base.Context.Request.ApplicationPath;

	internal override TextReader Reader
	{
		get
		{
			return reader;
		}
		set
		{
			reader = value;
		}
	}

	public ApplicationFileParser(string fname, HttpContext context)
	{
		base.InputFile = fname;
		base.Context = context;
		base.VirtualPath = new VirtualPath("/" + Path.GetFileName(fname));
		LoadConfigDefaults();
	}

	internal ApplicationFileParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
		: this(virtualPath, null, reader, context)
	{
	}

	internal ApplicationFileParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
	{
		base.VirtualPath = virtualPath;
		base.Context = context;
		Reader = reader;
		if (string.IsNullOrEmpty(inputFile))
		{
			base.InputFile = virtualPath.PhysicalPath;
		}
		else
		{
			base.InputFile = inputFile;
		}
		SetBaseType(null);
		LoadConfigDefaults();
	}

	internal override Type CompileIntoType()
	{
		return GlobalAsaxCompiler.CompileApplicationType(this);
	}

	internal static Type GetCompiledApplicationType(string inputFile, HttpContext context)
	{
		ApplicationFileParser applicationFileParser = new ApplicationFileParser(inputFile, context);
		Type compiledType = new AspGenerator(applicationFileParser).GetCompiledType();
		dependencies = applicationFileParser.Dependencies;
		return compiledType;
	}

	internal override void AddDirective(string directive, IDictionary atts)
	{
		if (string.Compare(directive, "application", ignoreCase: true, Helpers.InvariantCulture) != 0 && string.Compare(directive, "Import", ignoreCase: true, Helpers.InvariantCulture) != 0 && string.Compare(directive, "Assembly", ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			ThrowParseException("Invalid directive: " + directive);
		}
		base.AddDirective(directive, atts);
	}
}
