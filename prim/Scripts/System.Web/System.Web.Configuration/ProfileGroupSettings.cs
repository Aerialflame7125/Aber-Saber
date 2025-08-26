using System.Configuration;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Provides programmatic access to the <see langword="group" /> subsection of the <see langword="profiles" /> configuration file section.</summary>
public sealed class ProfileGroupSettings : ConfigurationElement
{
	private static ConfigurationProperty propertySettingsProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets name of the group of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects this object contains.</summary>
	/// <returns>A string containing the name of the group of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects this object contains. The default value is an empty string ("").</returns>
	[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
	public string Name
	{
		get
		{
			return (string)base[nameProp];
		}
		internal set
		{
			base[nameProp] = value;
		}
	}

	/// <summary>Gets the collection of profile property settings this object contains.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> collection that contains all the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects contained in this group.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public ProfilePropertySettingsCollection PropertySettings => (ProfilePropertySettingsCollection)base[propertySettingsProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProfileGroupSettings()
	{
		propertySettingsProp = new ConfigurationProperty(null, typeof(ProfilePropertySettingsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);
		nameProp = new ConfigurationProperty("name", typeof(string), null, null, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(propertySettingsProp);
		properties.Add(nameProp);
	}

	internal ProfileGroupSettings()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> class using default settings.</summary>
	/// <param name="name">The name of the new group.</param>
	public ProfileGroupSettings(string name)
	{
		Name = name;
	}

	/// <summary>Determines whether the specified <see langword="Object" /> is equal to the current <see langword="Object" />.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see langword="Object" />.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see langword="Object" /> is equal to the current <see langword="Object" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is ProfileGroupSettings profileGroupSettings))
		{
			return false;
		}
		if (GetType() != profileGroupSettings.GetType())
		{
			return false;
		}
		return Name.Equals(profileGroupSettings.Name);
	}

	/// <summary>Gets a unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</summary>
	/// <returns>A unique value representing the current <see cref="T:System.Configuration.ConfigurationElement" /> instance.</returns>
	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	internal void DoDeserialize(XmlReader reader)
	{
		DeserializeElement(reader, serializeCollectionKey: false);
	}
}
