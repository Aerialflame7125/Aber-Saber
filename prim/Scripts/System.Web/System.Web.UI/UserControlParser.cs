using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;

namespace System.Web.UI;

internal class UserControlParser : TemplateControlParser
{
	private string masterPage;

	private string providerName;

	internal override Type DefaultBaseType
	{
		get
		{
			Type defaultUserControlBaseType = PageParser.DefaultUserControlBaseType;
			if (defaultUserControlBaseType == null)
			{
				return base.DefaultBaseType;
			}
			return defaultUserControlBaseType;
		}
	}

	internal override string DefaultBaseTypeName => base.PagesConfig.UserControlBaseType;

	internal override string DefaultDirectiveName => "control";

	internal string MasterPageFile => masterPage;

	internal string ProviderName => providerName;

	internal UserControlParser(VirtualPath virtualPath, string inputFile, HttpContext context)
		: this(virtualPath, inputFile, context, null)
	{
	}

	internal UserControlParser(VirtualPath virtualPath, string inputFile, List<string> deps, HttpContext context)
		: this(virtualPath, inputFile, context, null)
	{
		base.Dependencies = deps;
	}

	internal UserControlParser(VirtualPath virtualPath, string inputFile, HttpContext context, string type)
	{
		base.VirtualPath = virtualPath;
		base.Context = context;
		BaseVirtualDir = virtualPath.DirectoryNoNormalize;
		base.InputFile = inputFile;
		SetBaseType(type);
		AddApplicationAssembly();
		LoadConfigDefaults();
	}

	internal UserControlParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
		: this(virtualPath, null, reader, context)
	{
	}

	internal UserControlParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
	{
		base.VirtualPath = virtualPath;
		base.Context = context;
		BaseVirtualDir = virtualPath.DirectoryNoNormalize;
		if (string.IsNullOrEmpty(inputFile))
		{
			base.InputFile = virtualPath.PhysicalPath;
		}
		else
		{
			base.InputFile = inputFile;
		}
		Reader = reader;
		SetBaseType(null);
		AddApplicationAssembly();
		LoadConfigDefaults();
	}

	internal UserControlParser(TextReader reader, int? uniqueSuffix, HttpContext context)
	{
		base.Context = context;
		string filePath = context.Request.FilePath;
		base.VirtualPath = new VirtualPath(filePath);
		BaseVirtualDir = VirtualPathUtility.GetDirectory(filePath, normalize: false);
		base.InputFile = VirtualPathUtility.GetFileName(filePath) + "#" + (uniqueSuffix.HasValue ? uniqueSuffix.Value.ToString("x") : "0");
		Reader = reader;
		SetBaseType(null);
		AddApplicationAssembly();
		LoadConfigDefaults();
	}

	internal static Type GetCompiledType(TextReader reader, int? inputHashCode, HttpContext context)
	{
		return new UserControlParser(reader, inputHashCode, context).CompileIntoType();
	}

	internal static Type GetCompiledType(string virtualPath, string inputFile, List<string> deps, HttpContext context)
	{
		return new UserControlParser(new VirtualPath(virtualPath), inputFile, deps, context).CompileIntoType();
	}

	public static Type GetCompiledType(string virtualPath, string inputFile, HttpContext context)
	{
		return new UserControlParser(new VirtualPath(virtualPath), inputFile, context).CompileIntoType();
	}

	internal override Type CompileIntoType()
	{
		return new AspGenerator(this).GetCompiledType();
	}

	internal override void ProcessMainAttributes(IDictionary atts)
	{
		masterPage = BaseParser.GetString(atts, "MasterPageFile", null);
		if (masterPage != null)
		{
			AddDependency(masterPage);
		}
		base.ProcessMainAttributes(atts);
	}

	internal override void ProcessOutputCacheAttributes(IDictionary atts)
	{
		providerName = BaseParser.GetString(atts, "ProviderName", null);
		base.ProcessOutputCacheAttributes(atts);
	}
}
