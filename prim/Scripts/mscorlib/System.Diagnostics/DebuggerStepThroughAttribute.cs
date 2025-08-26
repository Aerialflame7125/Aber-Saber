using System.Runtime.InteropServices;

namespace System.Diagnostics;

/// <summary>Instructs the debugger to step through the code instead of stepping into the code. This class cannot be inherited.</summary>
[Serializable]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
[ComVisible(true)]
public sealed class DebuggerStepThroughAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerStepThroughAttribute" /> class.</summary>
	public DebuggerStepThroughAttribute()
	{
	}
}
