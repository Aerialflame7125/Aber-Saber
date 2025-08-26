namespace System.Web.Configuration;

/// <summary>Specifies the hashing algorithm that ASP.NET uses for forms authentication and for validating view state data, and for out-of-process session state identification.</summary>
public enum MachineKeyValidation
{
	/// <summary>Specifies that ASP.NET uses the Message Digest 5 (<see langword="MD5" />) hashing algorithm. </summary>
	MD5,
	/// <summary>Specifies that ASP.NET uses the <see langword="HMACSHA1" /> hash algorithm.</summary>
	SHA1,
	/// <summary>Specifies that ASP.NET uses the TripleDES (<see langword="3DES" />) encryption algorithm. </summary>
	TripleDES,
	/// <summary>Specifies that ASP.NET uses the <see langword="AES" /> (Rijndael) encryption algorithm.</summary>
	AES,
	/// <summary>Specifies that ASP.NET uses the <see langword="HMACSHA256" /> hashing algorithm.  This is the default value.</summary>
	HMACSHA256,
	/// <summary>Specifies that ASP.NET uses the <see langword="HMACSHA384" /> hashing algorithm.</summary>
	HMACSHA384,
	/// <summary>Specifies that ASP.NET uses the <see langword="HMACSHA512" /> hashing algorithm.</summary>
	HMACSHA512,
	/// <summary>Specifies that ASP.NET uses a custom hashing algorithm. </summary>
	Custom
}
