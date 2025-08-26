using System.Collections;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(OutputCacheProfile), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class OutputCacheProfileCollection : ConfigurationElementCollection, ICollection, IEnumerable
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> keys.</summary>
	/// <returns>The <see langword="string" /> array containing the collection keys.</returns>
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

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object at the specified index.</summary>
	/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object </param>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> at the specified index.</returns>
	public OutputCacheProfile this[int index]
	{
		get
		{
			return (OutputCacheProfile)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> with the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> with the specified name.</returns>
	public new OutputCacheProfile this[string name] => (OutputCacheProfile)BaseGet(name);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static OutputCacheProfileCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object to the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object already exists in the collection or the collection is read only.</exception>
	public void Add(OutputCacheProfile name)
	{
		BaseAdd(name);
	}

	/// <summary>Removes all the  <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects from the collection.</summary>
	/// <exception cref="T:System.Configuration.ConfigurationException">The collection is read only.</exception>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new OutputCacheProfile();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((OutputCacheProfile)element).Name;
	}

	/// <summary>Gets the key at the specified <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index.</summary>
	/// <param name="index">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index of the key. </param>
	/// <returns>The key with the specified <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> index.</returns>
	public string GetKey(int index)
	{
		return (string)BaseGetKey(index);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element with the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element with the specified name.</returns>
	public OutputCacheProfile Get(string name)
	{
		return (OutputCacheProfile)BaseGet(name);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element at the specified index.</returns>
	public OutputCacheProfile Get(int index)
	{
		return (OutputCacheProfile)BaseGet(index);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified name from the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element to remove from the collection. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object at the specified index from the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> element to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Sets the specified <see cref="T:System.Web.Configuration.OutputCacheProfile" /> object. </summary>
	/// <param name="user">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> element to set.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> is read-only.</exception>
	public void Set(OutputCacheProfile user)
	{
		OutputCacheProfile outputCacheProfile = Get(user.Name);
		if (outputCacheProfile == null)
		{
			Add(user);
			return;
		}
		int index = BaseIndexOf(outputCacheProfile);
		RemoveAt(index);
		BaseAdd(index, user);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> class.</summary>
	public OutputCacheProfileCollection()
	{
	}
}
