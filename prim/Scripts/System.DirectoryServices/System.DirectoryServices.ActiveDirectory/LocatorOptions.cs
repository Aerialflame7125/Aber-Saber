namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies search flags for finding a domain controller in a domain.</summary>
[Flags]
public enum LocatorOptions : long
{
	/// <summary>Forces cached domain controller data to be ignored when searching for domain controllers.</summary>
	ForceRediscovery = 1L,
	/// <summary>Search only for domain controllers that are currently running the Kerberos Key Distribution Center service.</summary>
	KdcRequired = 0x400L,
	/// <summary>Search only for domain controllers that are currently running the Windows Time service.</summary>
	TimeServerRequired = 0x800L,
	/// <summary>Search only for writeable domain controllers.</summary>
	WriteableRequired = 0x1000L,
	/// <summary>When searching for domain controllers from a domain controller, exclude this domain controller from the search. If the current computer is not a domain controller, this flag is ignored.</summary>
	AvoidSelf = 0x4000L
}
