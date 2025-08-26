using System.Runtime.InteropServices;

namespace System.EnterpriseServices;

/// <summary>Enables you to pass context properties from the COM Transaction Integrator (COMTI) into the COM+ context.</summary>
[AttributeUsage(AttributeTargets.Class)]
[ComVisible(false)]
public sealed class COMTIIntrinsicsAttribute : Attribute
{
	private bool val;

	/// <summary>Gets a value indicating whether the COM Transaction Integrator (COMTI) context properties are passed into the COM+ context.</summary>
	/// <returns>
	///   <see langword="true" /> if the COMTI context properties are passed into the COM+ context; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool Value => val;

	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property to <see langword="true" />.</summary>
	public COMTIIntrinsicsAttribute()
	{
		val = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, enabling the setting of the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property.</summary>
	/// <param name="val">
	///   <see langword="true" /> if the COMTI context properties are passed into the COM+ context; otherwise, <see langword="false" />.</param>
	public COMTIIntrinsicsAttribute(bool val)
	{
		this.val = val;
	}
}
