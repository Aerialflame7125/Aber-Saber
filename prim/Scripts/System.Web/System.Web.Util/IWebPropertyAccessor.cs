namespace System.Web.Util;

/// <summary>Provides the interface for implementing property accessors.</summary>
public interface IWebPropertyAccessor
{
	/// <summary>Gets the value of a specified property.</summary>
	/// <param name="target">The property from which the value is retrieved.</param>
	/// <returns>The value of the specified property.</returns>
	object GetProperty(object target);

	/// <summary>Sets the specified property with the given value.</summary>
	/// <param name="target">The property for which <paramref name="value" /> is set.</param>
	/// <param name="value">The object containing the value of the property.</param>
	void SetProperty(object target, object value);
}
