namespace System.Web.ModelBinding;

/// <summary>Provides a base class for model-binding behavior attributes. </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class BindingBehaviorAttribute : Attribute
{
	private static readonly object _typeId = new object();

	/// <summary>Gets the model-binding behavior value.</summary>
	/// <returns>The model-binding behavior value.</returns>
	public BindingBehavior Behavior { get; private set; }

	/// <summary>Gets the unique identifier for this attribute.</summary>
	/// <returns>The unique identifier for this attribute.</returns>
	public override object TypeId => _typeId;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindingBehaviorAttribute" /> class.</summary>
	/// <param name="behavior">The model-binding behavior.</param>
	public BindingBehaviorAttribute(BindingBehavior behavior)
	{
		Behavior = behavior;
	}
}
