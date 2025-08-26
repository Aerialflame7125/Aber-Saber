namespace System.Web.ModelBinding;

/// <summary>Provides an attribute that specifies that a property is required for model binding.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class BindRequiredAttribute : BindingBehaviorAttribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindRequiredAttribute" /> class.</summary>
	public BindRequiredAttribute()
		: base(BindingBehavior.Required)
	{
	}
}
