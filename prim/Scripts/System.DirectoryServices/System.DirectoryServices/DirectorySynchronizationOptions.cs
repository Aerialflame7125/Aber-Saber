namespace System.DirectoryServices;

/// <summary>Contains flags that determine how directories within a domain will be synchronized.  These options can be set for the <see cref="P:System.DirectoryServices.DirectorySynchronization.Option" /> property.</summary>
[Flags]
public enum DirectorySynchronizationOptions : long
{
	/// <summary>No flags are set.</summary>
	None = 0L,
	/// <summary>If this flag is not present, the caller must have the right to replicate changes. If this flag is present, the caller requires no rights, but is allowed to see only objects and attributes that are accessible to the caller.</summary>
	ObjectSecurity = 1L,
	/// <summary>Return parents before children, when parents would otherwise appear later in the replication stream.</summary>
	ParentsFirst = 0x800L,
	/// <summary>Do not return private data in the search results.</summary>
	PublicDataOnly = 0x2000L,
	/// <summary>If this flag is not present, all of the values, up to a server-specified limit, in a multi-valued attribute are returned when any value changes. If this flag is present, only the changed values are returned.</summary>
	IncrementalValues = 0x80000000L
}
