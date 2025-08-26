namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.ReferralChasingOption" /> enumeration specifies if and how referral chasing is pursued.</summary>
[Serializable]
public enum ReferralChasingOption
{
	/// <summary>Chase referrals of either the subordinate or external type.</summary>
	All = 96,
	/// <summary>Chase external referrals.  If no referral chasing option is specified for a directory search, the type of referral chasing performed is  <see cref="F:System.DirectoryServices.ReferralChasingOption.External" />.</summary>
	External = 64,
	/// <summary>Never chase the referred-to server. Setting this option prevents a client from contacting other servers in a referral process.</summary>
	None = 0,
	/// <summary>Chase only subordinate referrals that are a subordinate naming context in a directory tree. The ADSI LDAP provider always turns off this flag for paged searches.</summary>
	Subordinate = 32
}
