namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the Active Directory Lightweight Directory (AD LDS) Services replication authentication mode.</summary>
public enum ReplicationSecurityLevel
{
	/// <summary>Kerberos authentication, using service principal names (SPNs), required. If Kerberos authentication fails, the AD LDS instances will not replicate.</summary>
	MutualAuthentication = 2,
	/// <summary>Kerberos authentication (using SPNs) is attempted first. If Kerberos fails, NTLM authentication is attempted. If NTLM fails, the AD LDS instances will not replicate.</summary>
	Negotiate = 1,
	/// <summary>All AD LDS instances in the configuration set use an identical account name and password as the AD LDS service account.</summary>
	NegotiatePassThrough = 0
}
