using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.UrlMapping" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(UrlMapping), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class UrlMappingCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets an array of the keys for all of the configuration elements contained in the <see cref="T:System.Web.Configuration.UrlMappingCollection" />.</summary>
	/// <returns>An array of the keys for all of the <see cref="T:System.Web.Configuration.UrlMapping" /> objects contained in the <see cref="T:System.Web.Configuration.UrlMappingCollection" />.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].Url;
			}
			return array;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.UrlMapping" /> object at the specified index.</summary>
	/// <param name="index">The index of the object to get.</param>
	/// <returns>The object at the specified index.</returns>
	public UrlMapping this[int index]
	{
		get
		{
			return (UrlMapping)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified name.</summary>
	/// <param name="name">The name of the collection object.</param>
	/// <returns>The collection object that has the specified name.</returns>
	public new UrlMapping this[string name] => (UrlMapping)BaseGet(name);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static UrlMappingCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.UrlMapping" /> to the collection.</summary>
	/// <param name="urlMapping">The <see cref="T:System.Web.Configuration.UrlMapping" /> object to add to the collection.</param>
	public void Add(UrlMapping urlMapping)
	{
		BaseAdd(urlMapping);
	}

	/// <summary>Removes all the <see cref="T:System.Web.Configuration.UrlMapping" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new UrlMapping();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((UrlMapping)element).Url;
	}

	/// <summary>Gets the collection key of the specified element.</summary>
	/// <param name="index">The collection index of the element to get.</param>
	/// <returns>A string representing the value of the key. </returns>
	public string GetKey(int index)
	{
		return (string)BaseGetKey(index);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified name from the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove from the collection.</param>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.Configuration.UrlMapping" /> object from the collection.</summary>
	/// <param name="urlMapping">The <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove from the collection.</param>
	public void Remove(UrlMapping urlMapping)
	{
		BaseRemove(urlMapping.Url);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.UrlMapping" /> object with the specified index from the collection.</summary>
	/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.UrlMapping" /> object to remove.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.UrlMappingCollection" /> class.</summary>
	public UrlMappingCollection()
	{
	}
}
