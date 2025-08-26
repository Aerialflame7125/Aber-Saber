using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.ClientTarget" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(ClientTarget), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class ClientTargetCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Returns an array of the keys for all the configuration elements contained in the <see cref="T:System.Web.Configuration.ClientTargetCollection" /> collection.</summary>
	/// <returns>The array of the keys for all of the <see cref="T:System.Web.Configuration.ClientTarget" /> objects contained in the <see cref="T:System.Web.Configuration.ClientTargetCollection" />.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].Alias;
			}
			return array;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.ClientTarget" /> object at the specified index.</summary>
	/// <param name="index">The collection index of the object.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ClientTarget" /> object at the specified index.</returns>
	public ClientTarget this[int index]
	{
		get
		{
			return (ClientTarget)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified name.</summary>
	/// <param name="name">The user agent's name.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified name.</returns>
	public new ClientTarget this[string name] => (ClientTarget)BaseGet(name);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ClientTargetCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.ClientTarget" /> object to the collection.</summary>
	/// <param name="clientTarget">The <see cref="T:System.Web.Configuration.ClientTarget" /> to add to the collection.</param>
	public void Add(ClientTarget clientTarget)
	{
		BaseAdd(clientTarget);
	}

	/// <summary>Removes all the <see cref="T:System.Web.Configuration.ClientTarget" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new ClientTarget(null, null);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((ClientTarget)element).Alias;
	}

	/// <summary>Gets the collection key for the specified element.</summary>
	/// <param name="index">The collection index of the element to get.</param>
	/// <returns>A string containing the value of the key.</returns>
	public string GetKey(int index)
	{
		return (string)BaseGetKey(index);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified alias from the collection.</summary>
	/// <param name="name">The alias of the <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.Configuration.ClientTarget" /> object from the collection.</summary>
	/// <param name="clientTarget">The <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
	public void Remove(ClientTarget clientTarget)
	{
		BaseRemove(clientTarget.Alias);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.ClientTarget" /> object with the specified collection index.</summary>
	/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.ClientTarget" /> to remove.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ClientTargetCollection" /> class.</summary>
	public ClientTargetCollection()
	{
	}
}
