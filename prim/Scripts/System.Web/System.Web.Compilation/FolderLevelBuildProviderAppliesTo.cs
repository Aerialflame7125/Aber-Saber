namespace System.Web.Compilation;

/// <summary>Represents an enumeration that specifies the target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</summary>
[Flags]
public enum FolderLevelBuildProviderAppliesTo
{
	/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object does not apply to any directory.</summary>
	None = 0,
	/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to a folder that contains code.</summary>
	Code = 1,
	/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the Web content directory.</summary>
	WebReferences = 2,
	/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the local resources directory.</summary>
	LocalResources = 4,
	/// <summary>Specifies that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to the global resources directory.</summary>
	GlobalResources = 8
}
