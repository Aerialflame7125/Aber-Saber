using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(FormsAuthenticationUser), AddItemName = "user", CollectionType = ConfigurationElementCollectionType.BasicMap)]
public sealed class FormsAuthenticationUserCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets all the collection's keys.</summary>
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

	/// <summary>Gets the type of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" />.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	protected override string ElementName => "user";

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> at the specified index.</summary>
	/// <param name="index">The collection user's index. </param>
	/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> that contains the user name and password.</returns>
	public FormsAuthenticationUser this[int index]
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> with the specified name.</summary>
	/// <param name="name">The user's name. </param>
	/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object that contains the user name and password.</returns>
	public new FormsAuthenticationUser this[string name] => Get(name);

	protected override bool ThrowOnDuplicate => false;

	static FormsAuthenticationUserCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> class.</summary>
	public FormsAuthenticationUserCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to the collection.</summary>
	/// <param name="user">The <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to add to the collection. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object already exists in the collection, or the collection is read-only.</exception>
	public void Add(FormsAuthenticationUser user)
	{
		BaseAdd(user);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> objects from the collection.</summary>
	/// <exception cref="T:System.Configuration.ConfigurationException">The collection is read-only.</exception>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new FormsAuthenticationUser("", "");
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> collection element at the specified index.</summary>
	/// <param name="index">The collection user's index. </param>
	/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> that contains the user name and password.</returns>
	public FormsAuthenticationUser Get(int index)
	{
		return (FormsAuthenticationUser)BaseGet(index);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> collection element with the specified name.</summary>
	/// <param name="name">The user's name. </param>
	/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object that contains the user name and password.</returns>
	public FormsAuthenticationUser Get(string name)
	{
		return (FormsAuthenticationUser)BaseGet(name);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((FormsAuthenticationUser)element).Name;
	}

	/// <summary>Gets the key at the specified <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> collection index.</summary>
	/// <param name="index">The index in the collection.</param>
	/// <returns>The key at the specified index of the <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" />.</returns>
	public string GetKey(int index)
	{
		return Get(index).Name;
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object from the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to remove from the collection. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object at the specified index from the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object to remove from the collection. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object at the specified index in the collection, the element has already been removed, or the collection is read only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Modifies the specified <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> object contained in the collection.</summary>
	/// <param name="user">The <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> object that must be changed. </param>
	public void Set(FormsAuthenticationUser user)
	{
		FormsAuthenticationUser formsAuthenticationUser = Get(user.Name);
		if (formsAuthenticationUser == null)
		{
			Add(user);
			return;
		}
		int index = BaseIndexOf(formsAuthenticationUser);
		RemoveAt(index);
		BaseAdd(index, user);
	}
}
