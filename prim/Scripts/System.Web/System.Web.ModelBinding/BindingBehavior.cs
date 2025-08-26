namespace System.Web.ModelBinding;

/// <summary>Enumerates model-binding behavior options.</summary>
public enum BindingBehavior
{
	/// <summary>The property should be model bound if a value is available from the value provider.</summary>
	Optional,
	/// <summary>The property should be excluded from model binding.</summary>
	Never,
	/// <summary>The property is required for model binding.</summary>
	Required
}
