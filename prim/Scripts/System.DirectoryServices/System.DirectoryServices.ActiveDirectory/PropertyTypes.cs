namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the property types to select when calling the <see cref="M:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema.FindAllProperties(System.DirectoryServices.ActiveDirectory.PropertyTypes)" /> method.</summary>
[Flags]
public enum PropertyTypes
{
	/// <summary>A property that is indexed.</summary>
	Indexed = 2,
	/// <summary>A property that is replicated in the global catalog.</summary>
	InGlobalCatalog = 4
}
