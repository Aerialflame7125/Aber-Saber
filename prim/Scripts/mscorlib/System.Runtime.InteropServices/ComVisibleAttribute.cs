namespace System.Runtime.InteropServices;

/// <summary>Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.</summary>
[ComVisible(true)]
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
public sealed class ComVisibleAttribute : Attribute
{
	internal bool _val;

	/// <summary>Gets a value that indicates whether the COM type is visible.</summary>
	/// <returns>
	///   <see langword="true" /> if the type is visible; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	public bool Value => _val;

	/// <summary>Initializes a new instance of the <see langword="ComVisibleAttribute" /> class.</summary>
	/// <param name="visibility">
	///   <see langword="true" /> to indicate that the type is visible to COM; otherwise, <see langword="false" />. The default is <see langword="true" />.</param>
	public ComVisibleAttribute(bool visibility)
	{
		_val = visibility;
	}
}
