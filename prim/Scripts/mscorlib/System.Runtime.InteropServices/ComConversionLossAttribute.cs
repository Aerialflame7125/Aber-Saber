namespace System.Runtime.InteropServices;

/// <summary>Indicates that information was lost about a class or interface when it was imported from a type library to an assembly.</summary>
[AttributeUsage(AttributeTargets.All, Inherited = false)]
[ComVisible(true)]
public sealed class ComConversionLossAttribute : Attribute
{
	/// <summary>Initializes a new instance of the <see langword="ComConversionLossAttribute" /> class.</summary>
	public ComConversionLossAttribute()
	{
	}
}
