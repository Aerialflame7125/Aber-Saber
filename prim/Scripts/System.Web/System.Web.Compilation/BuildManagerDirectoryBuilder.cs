using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation;

internal sealed class BuildManagerDirectoryBuilder
{
	private sealed class BuildProviderItem
	{
		public BuildProvider Provider;

		public int ListIndex;

		public int ParentIndex;

		public BuildProviderItem(BuildProvider bp, int listIndex, int parentIndex)
		{
			Provider = bp;
			ListIndex = listIndex;
			ParentIndex = parentIndex;
		}
	}

	private readonly VirtualPath virtualPath;

	private readonly string virtualPathDirectory;

	private CompilationSection compilationSection;

	private Dictionary<string, BuildProvider> buildProviders;

	private VirtualPathProvider vpp;

	private CompilationSection CompilationSection
	{
		get
		{
			if (compilationSection == null)
			{
				compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
			}
			return compilationSection;
		}
	}

	public BuildManagerDirectoryBuilder(VirtualPath virtualPath)
	{
		if (virtualPath == null)
		{
			throw new ArgumentNullException("virtualPath");
		}
		vpp = HostingEnvironment.VirtualPathProvider;
		this.virtualPath = virtualPath;
		virtualPathDirectory = VirtualPathUtility.GetDirectory(virtualPath.Absolute);
	}

	public List<BuildProviderGroup> Build(bool single)
	{
		if (StrUtils.StartsWith(virtualPath.AppRelative, "~/App_Themes/"))
		{
			ThemeDirectoryBuildProvider themeDirectoryBuildProvider = new ThemeDirectoryBuildProvider();
			themeDirectoryBuildProvider.SetVirtualPath(virtualPath);
			return GetSingleBuildProviderGroup(themeDirectoryBuildProvider);
		}
		BuildProviderCollection buildProviderCollection = CompilationSection?.BuildProviders;
		if (buildProviderCollection == null || buildProviderCollection.Count == 0)
		{
			return null;
		}
		if (virtualPath.IsFake)
		{
			BuildProvider buildProvider = GetBuildProvider(virtualPath, buildProviderCollection);
			if (buildProvider == null)
			{
				return null;
			}
			return GetSingleBuildProviderGroup(buildProvider);
		}
		if (single)
		{
			AddVirtualFile(GetVirtualFile(virtualPath.Absolute), buildProviderCollection);
		}
		else
		{
			Dictionary<string, bool> cache = new Dictionary<string, bool>(RuntimeHelpers.StringEqualityComparer);
			AddVirtualDir(GetVirtualDirectory(virtualPath.Absolute), buildProviderCollection, cache);
			cache = null;
			if (buildProviders == null || buildProviders.Count == 0)
			{
				AddVirtualFile(GetVirtualFile(virtualPath.Absolute), buildProviderCollection);
			}
		}
		if (buildProviders == null || buildProviders.Count == 0)
		{
			return null;
		}
		List<BuildProviderGroup> list = new List<BuildProviderGroup>();
		foreach (BuildProvider value in buildProviders.Values)
		{
			AssignToGroup(value, list);
		}
		if (list == null || list.Count == 0)
		{
			list = null;
			return null;
		}
		list.Reverse();
		return list;
	}

	private bool AddBuildProvider(BuildProvider buildProvider)
	{
		if (buildProviders == null)
		{
			buildProviders = new Dictionary<string, BuildProvider>(RuntimeHelpers.StringEqualityComparer);
		}
		string key = buildProvider.VirtualPath;
		if (buildProviders.ContainsKey(key))
		{
			return false;
		}
		buildProviders.Add(key, buildProvider);
		return true;
	}

	private void AddVirtualDir(VirtualDirectory vdir, BuildProviderCollection bpcoll, Dictionary<string, bool> cache)
	{
		if (vdir == null)
		{
			return;
		}
		List<string> list = new List<string>();
		foreach (VirtualFile file in vdir.Files)
		{
			string text = file.VirtualPath;
			if (BuildManager.IgnoreVirtualPath(text))
			{
				continue;
			}
			BuildProvider buildProvider = GetBuildProvider(text, bpcoll);
			if (buildProvider == null || !AddBuildProvider(buildProvider))
			{
				continue;
			}
			IDictionary<string, bool> dictionary = buildProvider.ExtractDependencies();
			if (dictionary == null)
			{
				continue;
			}
			list.Clear();
			foreach (KeyValuePair<string, bool> item in dictionary)
			{
				string key = item.Key;
				string directory = VirtualPathUtility.GetDirectory(key);
				if (!cache.ContainsKey(directory))
				{
					cache.Add(directory, value: true);
					AddVirtualDir(GetVirtualDirectory(key), bpcoll, cache);
				}
			}
		}
	}

	private void AddVirtualFile(VirtualFile file, BuildProviderCollection bpcoll)
	{
		if (file != null && !BuildManager.IgnoreVirtualPath(file.VirtualPath))
		{
			BuildProvider buildProvider = GetBuildProvider(file.VirtualPath, bpcoll);
			if (buildProvider != null)
			{
				AddBuildProvider(buildProvider);
			}
		}
	}

	private List<BuildProviderGroup> GetSingleBuildProviderGroup(BuildProvider bp)
	{
		List<BuildProviderGroup> list = new List<BuildProviderGroup>();
		BuildProviderGroup buildProviderGroup = new BuildProviderGroup();
		buildProviderGroup.AddProvider(bp);
		list.Add(buildProviderGroup);
		return list;
	}

	private VirtualDirectory GetVirtualDirectory(string virtualPath)
	{
		if (!vpp.DirectoryExists(VirtualPathUtility.GetDirectory(virtualPath)))
		{
			return null;
		}
		return vpp.GetDirectory(virtualPath);
	}

	private VirtualFile GetVirtualFile(string virtualPath)
	{
		if (!vpp.FileExists(virtualPath))
		{
			return null;
		}
		return vpp.GetFile(virtualPath);
	}

	private Type GetBuildProviderCodeDomType(BuildProvider bp)
	{
		CompilerType compilerType = bp.CodeCompilerType;
		if (compilerType == null)
		{
			string text = bp.LanguageName;
			if (string.IsNullOrEmpty(text))
			{
				text = CompilationSection.DefaultLanguage;
			}
			compilerType = BuildManager.GetDefaultCompilerTypeForLanguage(text, CompilationSection, throwOnMissing: false);
		}
		Type obj = compilerType?.CodeDomProviderType;
		if (obj == null)
		{
			throw new HttpException("Unable to determine code compilation language provider for virtual path '" + bp.VirtualPath + "'.");
		}
		return obj;
	}

	private void AssignToGroup(BuildProvider buildProvider, List<BuildProviderGroup> groups)
	{
		if (IsDependencyCycle(buildProvider))
		{
			throw new HttpException("Dependency cycles are not suppported: " + buildProvider.VirtualPath);
		}
		BuildProviderGroup buildProviderGroup = null;
		string directory = VirtualPathUtility.GetDirectory(buildProvider.VirtualPath);
		if (BuildManager.HasCachedItemNoLock(buildProvider.VirtualPath))
		{
			return;
		}
		StringComparison stringComparison = RuntimeHelpers.StringComparison;
		if (buildProvider is ApplicationFileBuildProvider || buildProvider is ThemeDirectoryBuildProvider)
		{
			buildProviderGroup = new BuildProviderGroup();
			buildProviderGroup.Standalone = true;
			InsertGroup(buildProviderGroup, groups);
		}
		else
		{
			Type buildProviderCodeDomType = GetBuildProviderCodeDomType(buildProvider);
			foreach (BuildProviderGroup group in groups)
			{
				if (group.Standalone)
				{
					continue;
				}
				if (group.Count == 0)
				{
					buildProviderGroup = group;
					break;
				}
				bool flag = true;
				foreach (BuildProvider item in group)
				{
					if (IsDependency(buildProvider, item))
					{
						flag = false;
						break;
					}
					if (string.Compare(directory, VirtualPathUtility.GetDirectory(item.VirtualPath), stringComparison) != 0)
					{
						flag = false;
						break;
					}
					if (buildProviderCodeDomType != null)
					{
						Type buildProviderCodeDomType2 = GetBuildProviderCodeDomType(item);
						if (buildProviderCodeDomType2 != null && buildProviderCodeDomType2 != buildProviderCodeDomType)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					buildProviderGroup = group;
					break;
				}
			}
			if (buildProviderGroup == null)
			{
				buildProviderGroup = new BuildProviderGroup();
				InsertGroup(buildProviderGroup, groups);
			}
		}
		buildProviderGroup.AddProvider(buildProvider);
		if (string.Compare(directory, virtualPathDirectory, stringComparison) == 0)
		{
			buildProviderGroup.Master = true;
		}
	}

	private void InsertGroup(BuildProviderGroup group, List<BuildProviderGroup> groups)
	{
		if (group.Application)
		{
			groups.Insert(groups.Count - 1, group);
			return;
		}
		int num = ((!group.Standalone) ? groups.FindLastIndex(SkipStandaloneGroups) : groups.FindLastIndex(SkipApplicationGroup));
		if (num == -1)
		{
			groups.Add(group);
		}
		else
		{
			groups.Insert((num != 0) ? (num - 1) : 0, group);
		}
	}

	private static bool SkipStandaloneGroups(BuildProviderGroup group)
	{
		return group?.Standalone ?? false;
	}

	private static bool SkipApplicationGroup(BuildProviderGroup group)
	{
		return group?.Application ?? false;
	}

	private bool IsDependency(BuildProvider bp1, BuildProvider bp2)
	{
		IDictionary<string, bool> dictionary = bp1.ExtractDependencies();
		if (dictionary == null)
		{
			return false;
		}
		if (dictionary.ContainsKey(bp2.VirtualPath))
		{
			return true;
		}
		foreach (KeyValuePair<string, bool> item in dictionary)
		{
			if (buildProviders.TryGetValue(item.Key, out var value) && IsDependency(value, bp2))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsDependencyCycle(BuildProvider buildProvider)
	{
		Dictionary<BuildProvider, bool> dictionary = new Dictionary<BuildProvider, bool>();
		dictionary.Add(buildProvider, value: true);
		return IsDependencyCycle(dictionary, buildProvider.ExtractDependencies());
	}

	private bool IsDependencyCycle(Dictionary<BuildProvider, bool> cache, IDictionary<string, bool> deps)
	{
		if (deps == null)
		{
			return false;
		}
		foreach (KeyValuePair<string, bool> dep in deps)
		{
			if (buildProviders.TryGetValue(dep.Key, out var value))
			{
				if (cache.ContainsKey(value))
				{
					return true;
				}
				cache.Add(value, value: true);
				if (IsDependencyCycle(cache, value.ExtractDependencies()))
				{
					return true;
				}
				cache.Remove(value);
			}
		}
		return false;
	}

	public static BuildProvider GetBuildProvider(string virtualPath, BuildProviderCollection coll)
	{
		return GetBuildProvider(new VirtualPath(virtualPath), coll);
	}

	public static BuildProvider GetBuildProvider(VirtualPath virtualPath, BuildProviderCollection coll)
	{
		if (virtualPath == null || string.IsNullOrEmpty(virtualPath.Original) || coll == null)
		{
			return null;
		}
		string extension = virtualPath.Extension;
		BuildProvider buildProvider = coll.GetProviderInstanceForExtension(extension);
		if (buildProvider == null)
		{
			if (string.Compare(extension, ".asax", StringComparison.OrdinalIgnoreCase) == 0)
			{
				buildProvider = new ApplicationFileBuildProvider();
			}
			else if (StrUtils.StartsWith(virtualPath.AppRelative, "~/App_Themes/"))
			{
				buildProvider = new ThemeDirectoryBuildProvider();
			}
			buildProvider?.SetVirtualPath(virtualPath);
			return buildProvider;
		}
		object[] customAttributes = buildProvider.GetType().GetCustomAttributes(typeof(BuildProviderAppliesToAttribute), inherit: true);
		if (customAttributes != null && customAttributes.Length != 0 && (((BuildProviderAppliesToAttribute)customAttributes[0]).AppliesTo & BuildProviderAppliesTo.Web) == 0)
		{
			return null;
		}
		buildProvider.SetVirtualPath(virtualPath);
		return buildProvider;
	}
}
