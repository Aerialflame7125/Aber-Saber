namespace System.Web.UI;

/// <summary>Defines a service implemented by objects to resolve relative URLs based on contextual information.</summary>
public interface IUrlResolutionService
{
	/// <summary>Returns a resolved URL that is suitable for use by the client.</summary>
	/// <param name="relativeUrl">A URL relative to the current page.</param>
	/// <returns>A <see cref="T:System.String" /> containing the resolved URL.</returns>
	string ResolveClientUrl(string relativeUrl);
}
