using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices;

/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />, where the <see cref="P:System.Runtime.InteropServices.DispatchWrapper.WrappedObject" /> is <see langword="null" />.</summary>
[Serializable]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
[ComVisible(true)]
public sealed class IDispatchConstantAttribute : CustomConstantAttribute
{
	/// <summary>Gets the <see langword="IDispatch" /> constant stored in this attribute.</summary>
	/// <returns>The <see langword="IDispatch" /> constant stored in this attribute. Only <see langword="null" /> is allowed for an <see langword="IDispatch" /> constant value.</returns>
	public override object Value => new DispatchWrapper(null);

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IDispatchConstantAttribute" /> class.</summary>
	public IDispatchConstantAttribute()
	{
	}
}
