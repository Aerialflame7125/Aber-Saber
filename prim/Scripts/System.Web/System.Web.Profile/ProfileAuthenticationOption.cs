namespace System.Web.Profile;

/// <summary>Describes the authentication type of user profiles to be searched.</summary>
public enum ProfileAuthenticationOption
{
	/// <summary>Search only anonymous profiles.</summary>
	Anonymous,
	/// <summary>Search only authenticated profiles.</summary>
	Authenticated,
	/// <summary>Search all profiles.</summary>
	All
}
