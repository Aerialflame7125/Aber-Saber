using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(HttpModuleAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class HttpModuleActionCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets or sets an item in this collection.</summary>
	/// <param name="index">Module collection index.</param>
	/// <returns>The item at the specified <paramref name="index" />.</returns>
	public HttpModuleAction this[int index]
	{
		get
		{
			return (HttpModuleAction)BaseGet(index);
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

	static HttpModuleActionCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpModuleActionCollection" /> class.</summary>
	public HttpModuleActionCollection()
	{
	}

	/// <summary>Adds an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object to the collection.</summary>
	/// <param name="httpModule">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">An attempt was made to add an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object that already exists in the collection. </exception>
	public void Add(HttpModuleAction httpModule)
	{
		BaseAdd(httpModule);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new HttpModuleAction();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((HttpModuleAction)element).Name;
	}

	/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.HttpModuleAction" /> module.</summary>
	/// <param name="action">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module for which to get the collection index.</param>
	/// <returns>The collection index value for the specified module.</returns>
	public int IndexOf(HttpModuleAction action)
	{
		return BaseIndexOf(action);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object from the collection.</summary>
	/// <param name="name">The key that identifies the <see cref="T:System.Web.Configuration.HttpModuleAction" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpModuleAction" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> object from the collection.</summary>
	/// <param name="action">The <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to remove.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.HttpModuleAction" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(HttpModuleAction action)
	{
		BaseRemove(action.Name);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpModuleAction" /> module at the specified index from the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.HttpModuleAction" /> module to remove.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpModuleAction" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	protected override bool IsElementRemovable(ConfigurationElement element)
	{
		return base.IsElementRemovable(element);
	}
}
