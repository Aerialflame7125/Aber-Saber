namespace System.Runtime.CompilerServices;

/// <summary>A class whose static <see cref="M:System.Runtime.CompilerServices.RuntimeFeature.IsSupported(System.String)" /> method checks whether a specified feature is supported by the common language runtime.</summary>
public static class RuntimeFeature
{
	/// <summary>Gets the name of the portable PDB feature.</summary>
	public const string PortablePdb = "PortablePdb";

	/// <summary>Determines whether a specified feature is supported by the common language runtime.</summary>
	/// <param name="feature">The name of the feature.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="feature" /> is supported; otherwise, <see langword="false" />.</returns>
	public static bool IsSupported(string feature)
	{
		if (feature == "PortablePdb")
		{
			return true;
		}
		return false;
	}
}
