using System.Configuration;
using System.Configuration.Provider;

namespace System.Web.Profile;

/// <summary>A collection of objects that inherit the <see cref="T:System.Web.Profile.ProfileProvider" /> abstract class.</summary>
public sealed class ProfileProviderCollection : SettingsProviderCollection
{
	/// <summary>Returns the profile provider referenced by the specified provider name.</summary>
	/// <param name="name">The name of the profile provider.</param>
	/// <returns>An object that inherits the <see cref="T:System.Web.Profile.ProfileProvider" /> abstract class.</returns>
	public new ProfileProvider this[string name] => (ProfileProvider)base[name];

	/// <summary>Creates a new, empty profile provider collection.</summary>
	public ProfileProviderCollection()
	{
	}

	/// <summary>Adds a profile provider to the collection.</summary>
	/// <param name="provider">The profile provider to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="provider" /> is not of a type that inherits the <see cref="T:System.Web.Profile.ProfileProvider" /> abstract class.</exception>
	public override void Add(ProviderBase provider)
	{
		base.Add(provider);
	}
}
