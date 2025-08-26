using System.Globalization;
using System.Resources;

namespace System.Web.Compilation;

/// <summary>Defines the interface a class must implement to act as a resource provider.</summary>
public interface IResourceProvider
{
	/// <summary>Gets an object to read resource values from a source.</summary>
	/// <returns>The <see cref="T:System.Resources.IResourceReader" /> associated with the current resource provider.</returns>
	IResourceReader ResourceReader { get; }

	/// <summary>Returns a resource object for the key and culture.</summary>
	/// <param name="resourceKey">The key identifying a particular resource.</param>
	/// <param name="culture">The culture identifying a localized value for the resource.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the resource value for the <paramref name="resourceKey" /> and <paramref name="culture" />.</returns>
	object GetObject(string resourceKey, CultureInfo culture);
}
