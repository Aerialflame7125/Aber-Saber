namespace System.Web.Security;

/// <summary>Describes how information in a cookie is protected.</summary>
public enum CookieProtection
{
	/// <summary>Do not protect information in the cookie. Information in the cookie is stored in clear text and not validated when sent back to the server.</summary>
	None,
	/// <summary>Ensure that the information in the cookie has not been altered before being sent back to the server.</summary>
	Validation,
	/// <summary>Encrypt the information in the cookie.</summary>
	Encryption,
	/// <summary>Use both <see cref="F:System.Web.Security.CookieProtection.Validation" /> and <see cref="F:System.Web.Security.CookieProtection.Encryption" /> to protect the information in the cookie.</summary>
	All
}
