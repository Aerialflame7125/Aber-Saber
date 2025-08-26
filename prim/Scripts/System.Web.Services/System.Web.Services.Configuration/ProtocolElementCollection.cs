using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration;

/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> objects.</summary>
[ConfigurationCollection(typeof(ProtocolElement))]
public sealed class ProtocolElementCollection : ConfigurationElementCollection
{
	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> that has the specified key in the collection.</summary>
	/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to get or set in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key.</returns>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key was not found in the collection.</exception>
	public ProtocolElement this[object key]
	{
		get
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return ((ProtocolElement)BaseGet(key)) ?? throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeyNotFoundInElementCollection"), key.ToString()));
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (GetElementKey(value).Equals(key))
			{
				if (BaseGet(key) != null)
				{
					BaseRemove(key);
				}
				Add(value);
				return;
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeysDoNotMatch"), GetElementKey(value).ToString(), key.ToString()));
		}
	}

	/// <summary>Gets or sets the element at a specified index in the collection.</summary>
	/// <param name="index">The zero-based index into the collection.</param>
	/// <returns>The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> that exists at the specified index.</returns>
	public ProtocolElement this[int index]
	{
		get
		{
			return (ProtocolElement)BaseGet(index);
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

	/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to the collection.</summary>
	/// <param name="element">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to add.</param>
	public void Add(ProtocolElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		BaseAdd(element);
	}

	/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key exists in the collection.</summary>
	/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to find in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains a <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key; otherwise, <see langword="false" />.</returns>
	public bool ContainsKey(object key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		return BaseGet(key) != null;
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new ProtocolElement();
	}

	/// <summary>Copies the elements from the collection to an array, starting at a particular index of the array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to which to copy the contents of the collection.</param>
	/// <param name="index">The zero-based index in <paramref name="Array" /> at which copying begins.</param>
	public void CopyTo(ProtocolElement[] array, int index)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		((ICollection)this).CopyTo((Array)array, index);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		return ((ProtocolElement)element).Name.ToString();
	}

	/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> in the collection.</summary>
	/// <param name="element">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to find in the collection.</param>
	/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.ProtocolElement" />, or -1 if the element was not found in the collection.</returns>
	public int IndexOf(ProtocolElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		return BaseIndexOf(element);
	}

	/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> from the collection.</summary>
	/// <param name="element">The <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to remove from the collection.</param>
	public void Remove(ProtocolElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		BaseRemove(GetElementKey(element));
	}

	/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> with the specified key from the collection.</summary>
	/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to be removed from the collection.</param>
	public void RemoveAt(object key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		BaseRemove(key);
	}

	/// <summary>Removes the element at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.ProtocolElement" /> to remove from the collection.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	internal void SetDefaults()
	{
		ProtocolElement element = new ProtocolElement(WebServiceProtocols.HttpSoap12);
		ProtocolElement element2 = new ProtocolElement(WebServiceProtocols.HttpSoap);
		ProtocolElement element3 = new ProtocolElement(WebServiceProtocols.HttpPostLocalhost);
		ProtocolElement element4 = new ProtocolElement(WebServiceProtocols.Documentation);
		Add(element);
		Add(element2);
		Add(element3);
		Add(element4);
	}

	/// <summary>Creates a new <see cref="T:System.Web.Services.Configuration.ProtocolElementCollection" />.</summary>
	public ProtocolElementCollection()
	{
	}
}
