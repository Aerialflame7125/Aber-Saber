using System.Configuration;

namespace System.Web.Configuration;

/// <summary>A collection of <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(BufferModeSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class BufferModesCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object with the specified numeric index in the collection.</summary>
	/// <param name="index">A valid index of a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.BufferModeSettings" /> object at the specified index.</returns>
	public BufferModeSettings this[int index]
	{
		get
		{
			return (BufferModeSettings)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object based on the specified key in the collection.</summary>
	/// <param name="key">The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object contained in the collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.BufferModeSettings" /> object.</returns>
	public new BufferModeSettings this[string key] => (BufferModeSettings)BaseGet(key);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static BufferModesCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object to the collection.</summary>
	/// <param name="bufferModeSettings">A <see cref="T:System.Web.Configuration.BufferModeSettings" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.BufferModeSettings" /> object to add already exists in the collection, or the collection is read-only.</exception>
	public void Add(BufferModeSettings bufferModeSettings)
	{
		BaseAdd(bufferModeSettings);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new BufferModeSettings();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((BufferModeSettings)element).Name;
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object from the collection.</summary>
	/// <param name="s">The name of a <see cref="T:System.Web.Configuration.BufferModeSettings" /> object in the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.BufferModeSettings" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string s)
	{
		BaseRemove(s);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModesCollection" /> class.</summary>
	public BufferModesCollection()
	{
	}
}
