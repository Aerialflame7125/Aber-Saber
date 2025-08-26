using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>Searches HTTP response text for return values for Web service clients.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public sealed class PatternMatcher
{
	private MatchType matchType;

	/// <summary>Creates a <see cref="T:System.Web.Services.Protocols.PatternMatcher" /> instance based on the input type.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> that specifies the return type for a Web method.</param>
	public PatternMatcher(Type type)
	{
		matchType = MatchType.Reflect(type);
	}

	/// <summary>Searches a text input to deserialize an object representing a Web method return value.</summary>
	/// <param name="text">The text to search, which is the body of the HTTP response.</param>
	/// <returns>An object representing a Web method return value.</returns>
	public object Match(string text)
	{
		return matchType.Match(text);
	}
}
