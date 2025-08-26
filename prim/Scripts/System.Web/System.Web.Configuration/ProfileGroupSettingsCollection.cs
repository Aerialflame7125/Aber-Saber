using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a set of <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects.</summary>
[ConfigurationCollection(typeof(ProfileGroupSettings), AddItemName = "group", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class ProfileGroupSettingsCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets a string array of all the key values in the collection.</summary>
	/// <returns>A string array of all the key values in the collection.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].Name;
			}
			return array;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index location.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public ProfileGroupSettings this[int index]
	{
		get
		{
			return Get(index);
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

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name, or <see langword="null" /> if there is no object with that name.</returns>
	public new ProfileGroupSettings this[string name] => (ProfileGroupSettings)BaseGet(name);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProfileGroupSettingsCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to the collection.</summary>
	/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add already exists in the collection, or the collection is read-only. </exception>
	public void Add(ProfileGroupSettings group)
	{
		BaseAdd(group);
	}

	protected internal override bool IsModified()
	{
		return base.IsModified();
	}

	protected internal override void ResetModified()
	{
		base.ResetModified();
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new ProfileGroupSettings();
	}

	/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to get.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public ProfileGroupSettings Get(int index)
	{
		return (ProfileGroupSettings)BaseGet(index);
	}

	/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to get.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object with the specified name, or <see langword="null" /> if the name does not exist.</returns>
	public ProfileGroupSettings Get(string name)
	{
		return (ProfileGroupSettings)BaseGet(name);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((ProfileGroupSettings)element).Name;
	}

	/// <summary>Returns the name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</param>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public string GetKey(int index)
	{
		return (string)BaseGetKey(index);
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</summary>
	/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object in the collection.</param>
	/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object, or -1 if the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object is not contained in the collection.</returns>
	public int IndexOf(ProfileGroupSettings group)
	{
		return BaseIndexOf(group);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is no <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is no <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only. </exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to the collection.</summary>
	/// <param name="group">A <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object to add already exists in the collection, or the collection is read-only. </exception>
	public void Set(ProfileGroupSettings group)
	{
		ProfileGroupSettings profileGroupSettings = Get(group.Name);
		if (profileGroupSettings == null)
		{
			Add(group);
			return;
		}
		int index = BaseIndexOf(profileGroupSettings);
		RemoveAt(index);
		BaseAdd(index, group);
	}

	internal void ResetInternal(ConfigurationElement parentElement)
	{
		Reset(parentElement);
	}

	internal void AddNewSettings(ProfileGroupSettings newSettings)
	{
		BaseAdd(newSettings, throwIfExists: false);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> class.</summary>
	public ProfileGroupSettingsCollection()
	{
	}
}
