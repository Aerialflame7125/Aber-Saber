namespace System.Web.ModelBinding;

/// <summary>Defines the methods that are required for a value provider that supports skipping request validation.</summary>
public interface IUnvalidatedValueProviderSource : IValueProviderSource
{
	/// <summary>Gets or sets a value that indicates whether the provider validates input.</summary>
	/// <returns>
	///     <see langword="true" /> if the provider validates input; otherwise, <see langword="false" />.</returns>
	bool ValidateInput { get; set; }
}
