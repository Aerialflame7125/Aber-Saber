namespace System.Runtime.CompilerServices;

/// <summary>Indicates that any private members contained in an assembly's types are not available to reflection.</summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
public sealed class DisablePrivateReflectionAttribute : Attribute
{
	/// <summary>Initializes a new instances of the <see cref="T:System.Runtime.CompilerServices.DisablePrivateReflectionAttribute" /> class.</summary>
	public DisablePrivateReflectionAttribute()
	{
	}
}
