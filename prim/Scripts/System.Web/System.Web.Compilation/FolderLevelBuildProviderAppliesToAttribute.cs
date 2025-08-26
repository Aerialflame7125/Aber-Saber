namespace System.Web.Compilation;

/// <summary>Defines an attribute that specifies the scope where a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object should be applied when a resource is located. </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class FolderLevelBuildProviderAppliesToAttribute : Attribute
{
	private FolderLevelBuildProviderAppliesTo _appliesTo;

	/// <summary>Gets or sets the target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</summary>
	/// <returns>The directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</returns>
	public FolderLevelBuildProviderAppliesTo AppliesTo => _appliesTo;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.FolderLevelBuildProviderAppliesToAttribute" /> class.</summary>
	/// <param name="appliesTo">The target directory that a <see cref="T:System.Web.Configuration.FolderLevelBuildProvider" /> object applies to.</param>
	public FolderLevelBuildProviderAppliesToAttribute(FolderLevelBuildProviderAppliesTo appliesTo)
	{
		_appliesTo = appliesTo;
	}
}
