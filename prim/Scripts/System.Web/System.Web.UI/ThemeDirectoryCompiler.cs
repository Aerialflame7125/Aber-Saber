using System.IO;
using System.Web.Compilation;

namespace System.Web.UI;

internal sealed class ThemeDirectoryCompiler
{
	public static Type GetCompiledType(string theme, HttpContext context)
	{
		string text = "~/App_Themes/" + theme + "/";
		string path = context.Request.MapPath(text);
		if (!Directory.Exists(path))
		{
			throw new HttpException($"Theme '{theme}' cannot be found in the application or global theme directories.");
		}
		string[] files = Directory.GetFiles(path, "*.skin");
		PageThemeParser pageThemeParser = new PageThemeParser(new VirtualPath(text), context);
		string[] files2 = Directory.GetFiles(path, "*.css");
		string[] array = new string[files2.Length];
		for (int i = 0; i < files2.Length; i++)
		{
			pageThemeParser.AddDependency(files2[i]);
			array[i] = text + Path.GetFileName(files2[i]);
		}
		Array.Sort(array, StringComparer.OrdinalIgnoreCase);
		pageThemeParser.LinkedStyleSheets = array;
		AspComponentFoundry foundry = new AspComponentFoundry();
		pageThemeParser.RootBuilder = new RootBuilder();
		for (int j = 0; j < files.Length; j++)
		{
			PageThemeFileParser pageThemeFileParser = new PageThemeFileParser(new VirtualPath(VirtualPathUtility.Combine(text, Path.GetFileName(files[j]))), files[j], context);
			pageThemeParser.AddDependency(files[j]);
			AspGenerator aspGenerator = new AspGenerator(pageThemeFileParser);
			pageThemeFileParser.RootBuilder.Foundry = foundry;
			aspGenerator.Parse();
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
		return new PageThemeCompiler(pageThemeParser).GetCompiledType();
	}

	public static PageTheme GetCompiledInstance(string theme, HttpContext context)
	{
		Type compiledType = GetCompiledType(theme, context);
		if (compiledType == null)
		{
			return null;
		}
		return (PageTheme)Activator.CreateInstance(compiledType);
	}
}
