using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

internal class ThemeDirectoryBuildProvider : TemplateBuildProvider
{
	protected override bool IsDirectoryBuilder => true;

	protected override void OverrideAssemblyPrefix(TemplateParser parser, AssemblyBuilder assemblyBuilder)
	{
		if (parser != null && assemblyBuilder != null)
		{
			string outputFilesPrefix = assemblyBuilder.OutputFilesPrefix + parser.ClassName + ".";
			assemblyBuilder.OutputFilesPrefix = outputFilesPrefix;
		}
	}

	protected override BaseCompiler CreateCompiler(TemplateParser parser)
	{
		return new PageThemeCompiler(parser as PageThemeParser);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
	{
		return CreateParser(virtualPath, inputFile, context);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string inputFile, HttpContext context)
	{
		string basePath = VirtualPathUtility.AppendTrailingSlash(virtualPath.Original);
		string physicalPath = virtualPath.PhysicalPath;
		if (!Directory.Exists(physicalPath))
		{
			throw new HttpException("Theme '" + virtualPath.Original + "' cannot be found in the application or global theme directories.");
		}
		PageThemeParser pageThemeParser = new PageThemeParser(virtualPath, context);
		string[] files = Directory.GetFiles(physicalPath, "*.css");
		string[] array = new string[files.Length];
		for (int i = 0; i < files.Length; i++)
		{
			array[i] = VirtualPathUtility.Combine(basePath, Path.GetFileName(files[i]));
			pageThemeParser.AddDependency(array[i]);
		}
		Array.Sort(array, StringComparer.OrdinalIgnoreCase);
		pageThemeParser.LinkedStyleSheets = array;
		AspComponentFoundry componentFoundry = new AspComponentFoundry();
		pageThemeParser.RootBuilder = new RootBuilder();
		string[] files2 = Directory.GetFiles(physicalPath, "*.skin");
		foreach (string text in files2)
		{
			string filename = VirtualPathUtility.Combine(basePath, Path.GetFileName(text));
			PageThemeFileParser pageThemeFileParser = new PageThemeFileParser(new VirtualPath(filename), text, context);
			pageThemeParser.AddDependency(filename);
			new AspGenerator(pageThemeFileParser, componentFoundry).Parse();
			if (pageThemeFileParser.RootBuilder.Children != null)
			{
				foreach (object child in pageThemeFileParser.RootBuilder.Children)
				{
					if (child is ControlBuilder)
					{
						pageThemeParser.RootBuilder.AppendSubBuilder((ControlBuilder)child);
					}
				}
			}
			foreach (string assembly in pageThemeFileParser.Assemblies)
			{
				if (!pageThemeParser.Assemblies.Contains(assembly))
				{
					pageThemeParser.AddAssemblyByFileName(assembly);
				}
			}
		}
		return pageThemeParser;
	}
}
