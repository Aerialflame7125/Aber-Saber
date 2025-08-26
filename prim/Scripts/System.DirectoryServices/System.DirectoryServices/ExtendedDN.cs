namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.ExtendedDN" /> enumeration specifies the format in which to return the extended distinguished name. This enumeration is used with the <see cref="P:System.DirectoryServices.DirectorySearcher.ExtendedDN" /> property.</summary>
public enum ExtendedDN
{
	/// <summary>Indicates that the distinguished name uses the distinguished name format.</summary>
	None = -1,
	/// <summary>Indicates that the distinguished name uses the hexadecimal format.</summary>
	HexString,
	/// <summary>Indicates that the distinguished name uses the standard string format.</summary>
	Standard
}
