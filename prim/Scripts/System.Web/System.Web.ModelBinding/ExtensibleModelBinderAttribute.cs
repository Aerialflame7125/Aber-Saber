namespace System.Web.ModelBinding;

/// <summary>Specifies the binder type for a model type.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
public sealed class ExtensibleModelBinderAttribute : Attribute
{
	/// <summary>Gets the model binder type.</summary>
	/// <returns>The model binder type.</returns>
	public Type BinderType { get; private set; }

	/// <summary>Gets or sets a value that specifies whether the prefix check should be suppressed.</summary>
	/// <returns>
	///     <see langword="true" /> if the prefix check should be suppressed; otherwise, <see langword="false" />.</returns>
	public bool SuppressPrefixCheck { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ExtensibleModelBinderAttribute" /> class.</summary>
	/// <param name="binderType">The model binder type.</param>
	public ExtensibleModelBinderAttribute(Type binderType)
	{
		BinderType = binderType;
	}
}
