namespace System.Web.Compilation;

/// <summary>Defines an attribute that specifies the scope where a build provider will be applied when a resource is located. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class BuildProviderAppliesToAttribute : Attribute
{
	private BuildProviderAppliesTo _appliesTo;

	/// <summary>Gets a value that indicates where the specified <see cref="T:System.Web.Compilation.BuildProvider" /> class will be applied when a resource with the appropriate extension is found.</summary>
	/// <returns>A <see cref="T:System.Web.Compilation.BuildProviderAppliesTo" /> value that indicates where the specified <see cref="T:System.Web.Compilation.BuildProvider" /> class will be applied when a resource with the appropriate extension is found.</returns>
	public BuildProviderAppliesTo AppliesTo => _appliesTo;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> class that applies to the specified resource location. </summary>
	/// <param name="appliesTo">One of the <see cref="T:System.Web.Compilation.BuildProviderAppliesTo" /> values.</param>
	public BuildProviderAppliesToAttribute(BuildProviderAppliesTo appliesTo)
	{
		_appliesTo = appliesTo;
	}
}
