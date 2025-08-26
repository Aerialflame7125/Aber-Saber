namespace System.Web.Configuration;

/// <summary>Defines the encryption format for storing passwords.</summary>
public enum FormsAuthPasswordFormat
{
	/// <summary>Specifies that passwords are not encrypted. This field is constant.</summary>
	Clear,
	/// <summary>Specifies that passwords are encrypted using the SHA1 hash algorithm. This field is constant.</summary>
	SHA1,
	/// <summary>Specifies that passwords are encrypted using the MD5 hash algorithm. This field is constant.</summary>
	MD5,
	/// <summary>Specifies that passwords are encrypted using the SHA256 hash algorithm. This field is constant.</summary>
	SHA256,
	/// <summary>Specifies that passwords are encrypted using the SHA384 hash algorithm. This field is constant.</summary>
	SHA384,
	/// <summary>Specifies that passwords are encrypted using the SHA512 hash algorithm. This field is constant.</summary>
	SHA512
}
