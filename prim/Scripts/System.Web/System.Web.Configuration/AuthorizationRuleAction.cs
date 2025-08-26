namespace System.Web.Configuration;

/// <summary>Specifies the type of authorization to apply when accessing a resource.</summary>
public enum AuthorizationRuleAction
{
	/// <summary>The authorization type denies access to the resource.</summary>
	Deny,
	/// <summary>The authorization type allows access to the resource.</summary>
	Allow
}
