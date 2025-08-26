using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(ProfileSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class ProfileSettingsCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.ProfileSettings" /> object based on the specified key in the collection.</summary>
	/// <param name="key">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object contained in the collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</returns>
	public new ProfileSettings this[string key] => (ProfileSettings)BaseGet(key);

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified numeric index in the collection.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public ProfileSettings this[int index]
	{
		get
		{
			return (ProfileSettings)BaseGet(index);
		}
		set
		{
			if (BaseGet(index) != null)
			{
				BaseRemoveAt(index);
			}
			BaseAdd(index, value);
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProfileSettingsCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfileSettings" /> object to the collection.</summary>
	/// <param name="profilesSettings">A <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
	public void Add(ProfileSettings profilesSettings)
	{
		BaseAdd(profilesSettings);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfileSettings" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Indicates whether the collection contains a <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified name.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains a <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified <paramref name="name" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(string name)
	{
		return BaseGet(name) != null;
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new ProfileSettings();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((ProfileSettings)element).Name;
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
	/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object, or -1 if the object is not found in the collection.</returns>
	public int IndexOf(string name)
	{
		ProfileSettings profileSettings = (ProfileSettings)BaseGet(name);
		if (profileSettings == null)
		{
			return -1;
		}
		return BaseIndexOf(profileSettings);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index in the collection.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
	/// <param name="authorizationSettings">A <see cref="T:System.Web.Configuration.ProfileSettings" /> object to insert into the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read-only.</exception>
	[MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
	public void Insert(int index, ProfileSettings authorizationSettings)
	{
		BaseAdd(index, authorizationSettings);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object from the collection.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfileSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileSettings" /> object in the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfileSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> class.</summary>
	public ProfileSettingsCollection()
	{
	}
}
