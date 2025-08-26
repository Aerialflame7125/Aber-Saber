namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the data representation (syntax) type of a <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
public enum ActiveDirectorySyntax
{
	/// <summary>A case-sensitive string type.</summary>
	CaseExactString,
	/// <summary>A case-insensitive string type.</summary>
	CaseIgnoreString,
	/// <summary>A numeric value represented as a string.</summary>
	NumericString,
	/// <summary>A directory string specification.</summary>
	DirectoryString,
	/// <summary>A byte array represented as a string.</summary>
	OctetString,
	/// <summary>A security descriptor value type.</summary>
	SecurityDescriptor,
	/// <summary>A 32-bit integer value type.</summary>
	Int,
	/// <summary>A 64 bit (large) integer value type.</summary>
	Int64,
	/// <summary>A Boolean value type.</summary>
	Bool,
	/// <summary>An OID value type.</summary>
	Oid,
	/// <summary>A time expressed in generalized time format.</summary>
	GeneralizedTime,
	/// <summary>A time expressed in Coordinated Universal Time format.</summary>
	UtcTime,
	/// <summary>A distinguished name of a directory service object.</summary>
	DN,
	/// <summary>An ADS_DN_WITH_BINARY structure used for mapping a distinguished name to a non-varying GUID. For more information, see the ADS_DN_WITH_BINARY article.</summary>
	DNWithBinary,
	/// <summary>An ADS_DN_WITH_STRING structure used for mapping a distinguished name to a non-varying string value. For more information, see the ADS_DN_WITH_STRING article.</summary>
	DNWithString,
	/// <summary>An enumeration value type.</summary>
	Enumeration,
	/// <summary>An IA5 character set string.</summary>
	IA5String,
	/// <summary>A printable character set string.</summary>
	PrintableString,
	/// <summary>An SID value type.</summary>
	Sid,
	/// <summary>An AccessPoint object type.</summary>
	AccessPointDN,
	/// <summary>An OR-Name object type.</summary>
	ORName,
	/// <summary>A Presentation-Address object type.</summary>
	PresentationAddress,
	/// <summary>A Replica-Link object type.</summary>
	ReplicaLink
}
