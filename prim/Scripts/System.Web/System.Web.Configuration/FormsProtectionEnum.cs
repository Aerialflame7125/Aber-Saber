namespace System.Web.Configuration;

/// <summary>Defines the type of encryption, if any, to use for cookies. </summary>
public enum FormsProtectionEnum
{
	/// <summary>Specifies that the application use both data validation and encryption to help protect cookies. This option uses the configured data-validation algorithm (based on the <see langword="machineKey" /> element). Triple-DES (3DES) is used for encryption, if it is available and if the key is at least 48 bytes long. <see langword="All" /> is the default (and recommended) value.</summary>
	All,
	/// <summary>Specifies that both encryption and validation are disabled for sites that use cookies only for personalization and thus have weaker security requirements. Using cookies in this manner is not recommended; however, it is the least resource-intensive way to enable personalization using the .NET Framework.</summary>
	None,
	/// <summary>Specifies that cookies are encrypted using Triple-DES or DES, but data validation is not performed on cookies. Cookies used this way might be subject to chosen plaintext security attacks.</summary>
	Encryption,
	/// <summary>Specifies that the application use a validation scheme to verify that the contents of an encrypted cookie have not been altered in transit. The cookie is created by concatenating a validation key with the cookie data, computing a Message Authentication Code (MAC), and appending the MAC to the outgoing cookie.</summary>
	Validation
}
