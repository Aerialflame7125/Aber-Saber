namespace System.Web.ModelBinding;

/// <summary>Defines the method that is required for a value provider source.</summary>
public interface IValueProviderSource
{
	/// <summary>Returns a value provider.</summary>
	/// <param name="modelBindingExecutionContext">The execution context.</param>
	/// <returns>The value provider.</returns>
	IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext);
}
