namespace System.DirectoryServices;

/// <summary>Specifies the available options for examining security information of a directory object. This enumeration is used with the <see cref="P:System.DirectoryServices.DirectorySearcher.SecurityMasks" /> and <see cref="P:System.DirectoryServices.DirectoryEntryConfiguration.SecurityMasks" /> properties.</summary>
[Flags]
public enum SecurityMasks
{
	/// <summary>Does not read or write security data.</summary>
	None = 0,
	/// <summary>Reads or writes the owner data.</summary>
	Owner = 1,
	/// <summary>Reads or writes the group data.</summary>
	Group = 2,
	/// <summary>Reads or writes the discretionary access-control list (DACL) data.</summary>
	Dacl = 4,
	/// <summary>Reads or writes the system access-control list (SACL) data.</summary>
	Sacl = 8
}
