using System.Runtime.InteropServices;

namespace System.Diagnostics;

/// <summary>Indicates the code following the attribute is to be executed in run, not step, mode.</summary>
[Serializable]
[ComVisible(true)]
[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
public sealed class DebuggerStepperBoundaryAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerStepperBoundaryAttribute" /> class.</summary>
	public DebuggerStepperBoundaryAttribute()
	{
	}
}
