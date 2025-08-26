namespace System.Web.Profile;

/// <summary>Provides a string of custom data to the provider for a profile property.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class CustomProviderDataAttribute : Attribute
{
	private string customProviderData;

	/// <summary>Gets a string of custom data for the profile property provider.</summary>
	/// <returns>A string of custom data for the profile property provider. The default is <see langword="null" />.</returns>
	public string CustomProviderData => customProviderData;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.CustomProviderDataAttribute" /> class and specifies a string of custom data.</summary>
	/// <param name="customProviderData">The string of custom data to supply to the provider.</param>
	public CustomProviderDataAttribute(string customProviderData)
	{
		this.customProviderData = customProviderData;
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Web.Profile.CustomProviderDataAttribute.CustomProviderData" /> property is set to the default value.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.Profile.CustomProviderDataAttribute.CustomProviderData" /> property is set to the default value; otherwise, <see langword="false" />.</returns>
	public override bool IsDefaultAttribute()
	{
		return string.IsNullOrEmpty(CustomProviderData);
	}
}
