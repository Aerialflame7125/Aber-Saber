using System.Configuration;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Acts as the top of a two-level named hierarchy of <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> collections.</summary>
[ConfigurationCollection(typeof(ProfilePropertySettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class RootProfilePropertySettingsCollection : ProfilePropertySettingsCollection
{
	private static ConfigurationPropertyCollection properties;

	private ProfileGroupSettingsCollection groupSettings;

	protected override bool AllowClear => true;

	/// <summary>Gets a <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> containing a collection of <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> collection.</returns>
	[ConfigurationProperty("group")]
	public ProfileGroupSettingsCollection GroupSettings => groupSettings;

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	protected override bool ThrowOnDuplicate => true;

	static RootProfilePropertySettingsCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> class using default settings.</summary>
	public RootProfilePropertySettingsCollection()
	{
		groupSettings = new ProfileGroupSettingsCollection();
	}

	/// <summary>Compares the current <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object to another A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object.</summary>
	/// <param name="rootProfilePropertySettingsCollection">A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object to compare to.</param>
	/// <returns>
	///     <see langword="true" /> if the passed <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object is equal to the current object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object rootProfilePropertySettingsCollection)
	{
		if (!(rootProfilePropertySettingsCollection is RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection2))
		{
			return false;
		}
		if (GetType() != rootProfilePropertySettingsCollection2.GetType())
		{
			return false;
		}
		if (base.Count != rootProfilePropertySettingsCollection2.Count)
		{
			return false;
		}
		for (int i = 0; i < base.Count; i++)
		{
			if (!BaseGet(i).Equals(rootProfilePropertySettingsCollection2.BaseGet(i)))
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>Generates a hash code for the collection.</summary>
	/// <returns>Unique integer hash code for the current object.</returns>
	public override int GetHashCode()
	{
		int num = 0;
		for (int i = 0; i < base.Count; i++)
		{
			num += BaseGet(i).GetHashCode();
		}
		return num;
	}

	protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
	{
		if (elementName == "group")
		{
			ProfileGroupSettings profileGroupSettings = new ProfileGroupSettings();
			profileGroupSettings.DoDeserialize(reader);
			GroupSettings.AddNewSettings(profileGroupSettings);
			return true;
		}
		return base.OnDeserializeUnrecognizedElement(elementName, reader);
	}

	protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
	{
		base.Unmerge(sourceElement, parentElement, saveMode);
	}

	protected internal override bool IsModified()
	{
		return base.IsModified();
	}

	protected internal override void ResetModified()
	{
		base.ResetModified();
	}

	protected internal override void Reset(ConfigurationElement parentElement)
	{
		base.Reset(parentElement);
		RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection = (RootProfilePropertySettingsCollection)parentElement;
		if (rootProfilePropertySettingsCollection != null)
		{
			GroupSettings.ResetInternal(rootProfilePropertySettingsCollection.GroupSettings);
		}
	}
}
