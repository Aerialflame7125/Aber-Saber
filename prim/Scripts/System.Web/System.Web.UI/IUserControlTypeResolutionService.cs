namespace System.Web.UI;

/// <summary>Defines the method a class must implement to return a control's type for a specified tag prefix and tag name.</summary>
public interface IUserControlTypeResolutionService
{
	/// <summary>Retrieves a type based on a parsed ASP.NET tag prefix and the name of the tag.</summary>
	/// <param name="tagPrefix">The parsed prefix of an ASP.NET tag.</param>
	/// <param name="tagName">The name of the parsed ASP.NET tag.</param>
	/// <returns>A <see cref="T:System.Type" /> that represents the type of control the prefix and tag identify.</returns>
	Type GetType(string tagPrefix, string tagName);
}
