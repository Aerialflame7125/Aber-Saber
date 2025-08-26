using System.Configuration;

namespace System.Web.Configuration;

/// <summary>A collection of <see cref="T:System.Web.Configuration.RuleSettings" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(RuleSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class RuleSettingsCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.RuleSettings" /> object based on the specified key in the collection.</summary>
	/// <param name="key">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object contained in the collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.RuleSettings" /> object.</returns>
	public new RuleSettings this[string key] => (RuleSettings)BaseGet(key);

	/// <summary>Gets the <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified numeric index.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index.</returns>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Index is out of range.</exception>
	public RuleSettings this[int index]
	{
		get
		{
			return (RuleSettings)BaseGet(index);
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

	static RuleSettingsCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.RuleSettings" /> object to the collection.</summary>
	/// <param name="ruleSettings">A <see cref="T:System.Web.Configuration.RuleSettings" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to add already exists in the collection or the collection is read-only.</exception>
	public void Add(RuleSettings ruleSettings)
	{
		BaseAdd(ruleSettings);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.RuleSettings" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Returns <see langword="true" /> if the collection contains a <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified name.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains a <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified name; otherwise, <see langword="false" />.</returns>
	public bool Contains(string name)
	{
		return BaseGet(name) != null;
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new RuleSettings();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((RuleSettings)element).Name;
	}

	/// <summary>Finds the index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection with the specified name.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
	/// <returns>The index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection with the specified name.</returns>
	public int IndexOf(string name)
	{
		RuleSettings ruleSettings = (RuleSettings)BaseGet(name);
		if (ruleSettings == null)
		{
			return -1;
		}
		return BaseIndexOf(ruleSettings);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.RuleSettings" /> object to the specified index point in the collection.</summary>
	/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
	/// <param name="eventSettings">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to insert into the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.RuleSettings" /> object to add already exists in the collection, the index is invalid, or the collection is read only.</exception>
	[MonoTODO("why did they use 'Insert' and not 'Add' as other collections do?")]
	public void Insert(int index, RuleSettings eventSettings)
	{
		BaseAdd(index, eventSettings);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.RuleSettings" /> object from the collection.</summary>
	/// <param name="name">The name of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.RuleSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index location from the collection.</summary>
	/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.RuleSettings" /> object in the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.RuleSettings" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> class.</summary>
	public RuleSettingsCollection()
	{
	}
}
