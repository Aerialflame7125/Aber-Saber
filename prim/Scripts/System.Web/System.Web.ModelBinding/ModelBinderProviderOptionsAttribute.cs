namespace System.Web.ModelBinding;

/// <summary>Represents an attribute that specifies options for a model-binder provider.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class ModelBinderProviderOptionsAttribute : Attribute
{
	/// <summary>Gets or sets a value that specifies whether a model binder provider should appear at the beginning of the list of model-binder providers.</summary>
	/// <returns>
	///     <see langword="true" /> if the model binder provider should go at the beginning of the list; otherwise, <see langword="false" />.</returns>
	public bool FrontOfList { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderProviderOptionsAttribute" /> class.</summary>
	public ModelBinderProviderOptionsAttribute()
	{
	}
}
