using System.Configuration;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Contains a set of <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> objects.</summary>
[ConfigurationCollection(typeof(ProfilePropertySettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public class ProfilePropertySettingsCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Returns an array containing the names of all the <see cref="T:System.Web.Configuration.ProfileSection" /> objects contained in the collection.</summary>
	/// <returns>An array containing the names of all the <see cref="T:System.Web.Configuration.ProfileSection" /> objects contained in the collection or an empty array if the collection is empty.</returns>
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

	/// <summary>Gets a value indicating whether the &lt;clear&gt; element is valid as a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	protected virtual bool AllowClear => false;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index location.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index location.</returns>
	public ProfilePropertySettings this[int index]
	{
		get
		{
			return Get(index);
		}
		set
		{
			if (Get(index) != null)
			{
				BaseRemoveAt(index);
			}
			BaseAdd(index, value);
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object with the specified name.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object with the specified name.</returns>
	public new ProfilePropertySettings this[string name] => (ProfilePropertySettings)BaseGet(name);

	/// <summary>Gets a value indicating whether an error should be thrown if an attempt to create a duplicate object is made.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	protected override bool ThrowOnDuplicate => true;

	/// <summary>Gets a collection of configuration properties.</summary>
	/// <returns>A collection of configuration properties.</returns>
	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProfilePropertySettingsCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to the collection.</summary>
	/// <param name="propertySettings">A <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to add already exists in the collection or the collection is read only.</exception>
	public void Add(ProfilePropertySettings propertySettings)
	{
		BaseAdd(propertySettings);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
	/// <returns>A new <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
	protected override ConfigurationElement CreateNewElement()
	{
		return new ProfilePropertySettings();
	}

	/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileSection" /> object at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.ProfileSection" /> to get.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public ProfilePropertySettings Get(int index)
	{
		return (ProfilePropertySettings)BaseGet(index);
	}

	/// <summary>Returns the <see cref="T:System.Web.Configuration.ProfileSection" /> object with the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSection" /> to get.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object with the specified name, or <see langword="null" /> if the name does not exist.</returns>
	public ProfilePropertySettings Get(string name)
	{
		return (ProfilePropertySettings)BaseGet(name);
	}

	/// <summary>Gets the key for the specified configuration element.</summary>
	/// <param name="element">A <see cref="T:System.Configuration.ConfigurationElement" /> in the collection.</param>
	/// <returns>The name of the element.</returns>
	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((ProfilePropertySettings)element).Name;
	}

	/// <summary>Handles the reading of unrecognized configuration elements from a configuration file and causes the configuration system to throw an exception if the element cannot be handled.</summary>
	/// <param name="elementName">The name of the unrecognized element.</param>
	/// <param name="reader">An input stream that reads XML from the configuration file.</param>
	/// <returns>
	///     <see langword="true" /> if the unrecognized element was deserialized successfully; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
	///         <paramref name="elementName" /> equals "clear"- or -
	///         <paramref name="elementName" /> equals "group".</exception>
	protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
	{
		return base.OnDeserializeUnrecognizedElement(elementName, reader);
	}

	/// <summary>Gets the name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> at the specified index location.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> in the collection.</param>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> at the specified index location.</returns>
	public string GetKey(int index)
	{
		return Get(index)?.Name;
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
	/// <param name="propertySettings">A <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection.</param>
	/// <returns>The index of the specified <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object, or -1 if the object is not found in the collection.</returns>
	public int IndexOf(ProfilePropertySettings propertySettings)
	{
		return BaseIndexOf(propertySettings);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object from the collection.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object with the specified key in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object in the collection.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object at the specified index in the collection.- or -The element has already been removed.- or -The collection is read-only. </exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to the collection.</summary>
	/// <param name="propertySettings">A <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
	public void Set(ProfilePropertySettings propertySettings)
	{
		ProfilePropertySettings profilePropertySettings = Get(propertySettings.Name);
		if (profilePropertySettings == null)
		{
			Add(propertySettings);
			return;
		}
		int index = BaseIndexOf(profilePropertySettings);
		RemoveAt(index);
		BaseAdd(index, propertySettings);
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> class.</summary>
	public ProfilePropertySettingsCollection()
	{
	}
}
