using System.Runtime.InteropServices;

namespace System.EnterpriseServices;

/// <summary>Sets the description on an assembly (application), component, method, or interface. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
[ComVisible(false)]
public sealed class DescriptionAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.DescriptionAttribute" /> class.</summary>
	/// <param name="desc">The description of the assembly (application), component, method, or interface.</param>
	public DescriptionAttribute(string desc)
	{
	}
}
