using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices;

/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.UnknownWrapper" />, where the <see cref="P:System.Runtime.InteropServices.UnknownWrapper.WrappedObject" /> is <see langword="null" />. This class cannot be inherited.</summary>
[Serializable]
[ComVisible(true)]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
public sealed class IUnknownConstantAttribute : CustomConstantAttribute
{
	/// <summary>Gets the <see langword="IUnknown" /> constant stored in this attribute.</summary>
	/// <returns>The <see langword="IUnknown" /> constant stored in this attribute. Only <see langword="null" /> is allowed for an <see langword="IUnknown" /> constant value.</returns>
	public override object Value => new UnknownWrapper(null);

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IUnknownConstantAttribute" /> class.</summary>
	public IUnknownConstantAttribute()
	{
	}
}
