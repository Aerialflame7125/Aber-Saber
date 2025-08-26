using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.HttpHandlerAction" /> elements. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(HttpHandlerAction), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMapAlternate)]
public sealed class HttpHandlerActionCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the type of <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" />.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.AddRemoveClearMapAlternate;

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets or sets an item in this collection.</summary>
	/// <param name="index">The item index.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object selected.</returns>
	public HttpHandlerAction this[int index]
	{
		get
		{
			return (HttpHandlerAction)BaseGet(index);
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

	protected override bool ThrowOnDuplicate => false;

	static HttpHandlerActionCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> class.</summary>
	public HttpHandlerActionCollection()
	{
	}

	/// <summary>Adds an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to the collection.</summary>
	/// <param name="httpHandlerAction">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to add to the collection. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to add already exists in the collection or the collection is read-only.</exception>
	public void Add(HttpHandlerAction httpHandlerAction)
	{
		HttpApplication.ClearHandlerCache();
		BaseAdd(httpHandlerAction);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects from the collection.</summary>
	public void Clear()
	{
		HttpApplication.ClearHandlerCache();
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new HttpHandlerAction();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((HttpHandlerAction)element).Path + "-" + ((HttpHandlerAction)element).Verb;
	}

	/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object.</summary>
	/// <param name="action">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object for which to get the collection index. </param>
	/// <returns>The collection index value.</returns>
	public int IndexOf(HttpHandlerAction action)
	{
		return BaseIndexOf(action);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object with the specified <see cref="P:System.Web.Configuration.HttpHandlerAction.Verb" /> and <see cref="P:System.Web.Configuration.HttpHandlerAction.Path" /> properties from the collection.</summary>
	/// <param name="verb">The <see cref="P:System.Web.Configuration.HttpHandlerAction.Verb" /> property value that belongs to the handler to remove.</param>
	/// <param name="path">The <see cref="P:System.Web.Configuration.HttpHandlerAction.Path" /> property value that belongs to the handler to remove.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object with the specified key in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(string verb, string path)
	{
		HttpApplication.ClearHandlerCache();
		BaseRemove(path + "-" + verb);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object from the collection.</summary>
	/// <param name="action">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object to remove. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(HttpHandlerAction action)
	{
		HttpApplication.ClearHandlerCache();
		BaseRemove(action.Path + "-" + action.Verb);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object at the specified index location from the collection.</summary>
	/// <param name="index">The collection index of the object to remove.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object at the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		HttpApplication.ClearHandlerCache();
		BaseRemoveAt(index);
	}
}
