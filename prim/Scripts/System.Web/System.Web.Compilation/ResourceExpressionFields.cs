namespace System.Web.Compilation;

/// <summary>Contains the fields from a parsed resource expression.</summary>
public sealed class ResourceExpressionFields
{
	private string classKey;

	private string resourceKey;

	/// <summary>Gets the class key for a parsed resource expression.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the class key, or <see cref="F:System.String.Empty" /> if the class key has not been set.</returns>
	public string ClassKey => classKey;

	/// <summary>Gets the resource key for a parsed resource expression.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the resource key, or <see cref="F:System.String.Empty" /> if the resource key has not been set.</returns>
	public string ResourceKey => resourceKey;

	internal ResourceExpressionFields(string classKey, string resourceKey)
	{
		this.classKey = classKey;
		this.resourceKey = resourceKey;
	}

	internal ResourceExpressionFields(string resourceKey)
		: this(null, resourceKey)
	{
	}
}
