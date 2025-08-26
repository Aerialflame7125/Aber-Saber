namespace System.Web.Profile;

/// <summary>Identifies the profile provider for a user-profile property.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ProfileProviderAttribute : Attribute
{
	private string providerName;

	/// <summary>Gets the name of the profile provider for the user-profile property.</summary>
	/// <returns>The name of the profile provider for the user-profile property.</returns>
	public string ProviderName => providerName;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.ProfileProviderAttribute" /> class with the specified profile provider name.</summary>
	/// <param name="providerName">The name of the profile provider for the property.</param>
	public ProfileProviderAttribute(string providerName)
	{
		this.providerName = providerName;
	}
}
