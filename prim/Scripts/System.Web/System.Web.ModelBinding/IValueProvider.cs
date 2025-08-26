namespace System.Web.ModelBinding;

/// <summary>Defines the methods that are required for a value provider. </summary>
public interface IValueProvider
{
	/// <summary>Returns a value that specifies whether the collection contains the specified prefix.</summary>
	/// <param name="prefix">The prefix.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains the specified prefix; otherwise, <see langword="false" />.</returns>
	bool ContainsPrefix(string prefix);

	/// <summary>Returns a value object using the specified key.</summary>
	/// <param name="key">The key.</param>
	/// <returns>The value object.</returns>
	ValueProviderResult GetValue(string key);
}
