namespace System.Web.Configuration;

/// <summary>Specifies the authentication mode to use in a Web application.</summary>
public enum AuthenticationMode
{
	/// <summary>Specifies no authentication.</summary>
	None,
	/// <summary>Specifies Windows as the authentication mode. This mode applies when using the Internet Information Services (IIS) authentication methods Basic, Digest, Integrated Windows (NTLM/Kerberos), or certificates.</summary>
	Windows,
	/// <summary>Specifies Microsoft Passport as the authentication mode.</summary>
	[Obsolete("This field is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	Passport,
	/// <summary>Specifies ASP.NET Forms-based authentication as the authentication mode.</summary>
	Forms
}
