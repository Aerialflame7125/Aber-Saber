namespace System.Web.ModelBinding;

/// <summary>Provides an attribute that specifies that model binding should exclude a property.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class BindNeverAttribute : BindingBehaviorAttribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindNeverAttribute" /> class.</summary>
	public BindNeverAttribute()
		: base(BindingBehavior.Never)
	{
	}
}
