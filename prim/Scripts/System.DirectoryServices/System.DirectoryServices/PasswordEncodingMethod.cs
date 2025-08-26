namespace System.DirectoryServices;

/// <summary>Specifies whether SSL is used when you set or change a password. This enumeration is used with the <see cref="P:System.DirectoryServices.DirectoryEntryConfiguration.PasswordEncoding" /> property.</summary>
public enum PasswordEncodingMethod
{
	/// <summary>Passwords are encoded using SSL.</summary>
	PasswordEncodingSsl,
	/// <summary>Passwords are not encoded and are transmitted in plain text.</summary>
	PasswordEncodingClear
}
