using System.Collections;
using System.Globalization;

namespace System.Web.Compilation;

/// <summary>Defines methods a class implements to act as an implicit resource provider.</summary>
public interface IImplicitResourceProvider
{
	/// <summary>Gets an object representing the value of the specified resource key.</summary>
	/// <param name="key">The resource key containing the prefix, filter, and property.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture for which the resource is localized.</param>
	/// <returns>An <see cref="T:System.Object" /> representing the localized value of an implicit resource key.</returns>
	object GetObject(ImplicitResourceKey key, CultureInfo culture);

	/// <summary>Gets a collection of implicit resource keys as specified by the prefix.</summary>
	/// <param name="keyPrefix">The prefix of the implicit resource keys to be collected.</param>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> of implicit resource keys.</returns>
	ICollection GetImplicitResourceKeys(string keyPrefix);
}
