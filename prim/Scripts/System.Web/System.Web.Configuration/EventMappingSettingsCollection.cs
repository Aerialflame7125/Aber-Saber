using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Provides a collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(EventMappingSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class EventMappingSettingsCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object at the specified index location.</summary>
	/// <param name="index">A valid index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public EventMappingSettings this[int index]
	{
		get
		{
			return (EventMappingSettings)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object based on the specified key in the collection.</summary>
	/// <param name="key">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object contained in the collection.</param>
	/// <returns>An <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</returns>
	public new EventMappingSettings this[string key] => (EventMappingSettings)BaseGet(key);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static EventMappingSettingsCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to the collection.</summary>
	/// <param name="eventMappingSettings">An <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
	public void Add(EventMappingSettings eventMappingSettings)
	{
		BaseAdd(eventMappingSettings);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Indicates whether the collection contains an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified name.</summary>
	/// <param name="name">The name of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified name; otherwise, <see langword="false" />.</returns>
	public bool Contains(string name)
	{
		return BaseGet(name) != null;
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new EventMappingSettings();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((EventMappingSettings)element).Name;
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</summary>
	/// <param name="name">The name of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
	/// <returns>The index of the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object, or -1 if the object is not found in the collection.</returns>
	public int IndexOf(string name)
	{
		EventMappingSettings eventMappingSettings = (EventMappingSettings)BaseGet(name);
		if (eventMappingSettings == null)
		{
			return -1;
		}
		return BaseIndexOf(eventMappingSettings);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to the specified index point in the collection.</summary>
	/// <param name="index">A valid index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
	/// <param name="eventMappingSettings">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to insert into the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.EventMappingSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read-only.</exception>
	[MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
	public void Insert(int index, EventMappingSettings eventMappingSettings)
	{
		BaseAdd(index, eventMappingSettings);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object from the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object from the collection.</summary>
	/// <param name="index">The index of an <see cref="T:System.Web.Configuration.EventMappingSettings" /> object in the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.EventMappingSettings" /> object with the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> class.</summary>
	public EventMappingSettingsCollection()
	{
	}
}
