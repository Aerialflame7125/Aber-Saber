namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the context type for an <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object.</summary>
public enum DirectoryContextType
{
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents a domain.</summary>
	Domain,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents a forest.</summary>
	Forest,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents a directory server.</summary>
	DirectoryServer,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents an AD LDS configuration set.</summary>
	ConfigurationSet,
	/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object represents an application partition.</summary>
	ApplicationPartition
}
