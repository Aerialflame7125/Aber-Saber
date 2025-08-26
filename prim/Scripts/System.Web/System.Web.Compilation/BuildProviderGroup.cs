using System.Collections.Generic;

namespace System.Web.Compilation;

internal class BuildProviderGroup : List<BuildProvider>
{
	public string NamePrefix { get; private set; }

	public bool Standalone { get; set; }

	public bool Application { get; private set; }

	public bool Master { get; set; }

	public CompilerType CompilerType { get; private set; }

	public void AddProvider(BuildProvider bp)
	{
		if (base.Count == 0)
		{
			if (bp is ApplicationFileBuildProvider)
			{
				NamePrefix = "App_global.asax";
				Application = true;
			}
			else if (bp is ThemeDirectoryBuildProvider)
			{
				NamePrefix = "App_Theme";
				Master = true;
			}
			else
			{
				NamePrefix = "App_Web";
			}
			CompilerType defaultCompilerTypeForLanguage = BuildManager.GetDefaultCompilerTypeForLanguage(bp.LanguageName, null);
			if (defaultCompilerTypeForLanguage != null)
			{
				CompilerType = defaultCompilerTypeForLanguage;
			}
		}
		Add(bp);
	}
}
