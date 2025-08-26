namespace System.Web.Security;

/// <summary>Specifies the connection protection options supported by the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> class.</summary>
public enum ActiveDirectoryConnectionProtection
{
	/// <summary>No transport layer security is used. Explicit credentials for the Active Directory connection must be provided in the configuration file.</summary>
	None,
	/// <summary>An SSL connection is used to connect to the Active Directory server.</summary>
	Ssl,
	/// <summary>The connection to the Active Directory server is secured by digitally signing and encrypting each packet sent to the server. </summary>
	SignAndSeal
}
