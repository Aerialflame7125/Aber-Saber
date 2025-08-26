using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace System.Web.Services.Configuration;

/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> objects.</summary>
[ConfigurationCollection(typeof(SoapExtensionTypeElement))]
public sealed class SoapExtensionTypeElementCollection : ConfigurationElementCollection
{
	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> having the specified key in the collection.</summary>
	/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to get or set in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key.</returns>
	/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key was not found in the collection.</exception>
	public SoapExtensionTypeElement this[object key]
	{
		get
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return ((SoapExtensionTypeElement)BaseGet(key)) ?? throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, Res.GetString("ConfigKeyNotFoundInElementCollection"), key.ToString()));
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
	/// <returns>The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> that exists at the specified index.</returns>
	public SoapExtensionTypeElement this[int index]
	{
		get
		{
			return (SoapExtensionTypeElement)BaseGet(index);
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

	/// <summary>Adds a <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to the collection.</summary>
	/// <param name="element">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to add.</param>
	public void Add(SoapExtensionTypeElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		BaseAdd(element);
	}

	/// <summary>Removes all <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Returns a <see cref="T:System.Boolean" /> that indicates whether a <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key exists in the collection.</summary>
	/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to find in the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the collection contains a <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key; otherwise, <see langword="false" />.</returns>
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
		return new SoapExtensionTypeElement();
	}

	/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
	/// <param name="array">An array of type <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to which to copy the contents of the collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(SoapExtensionTypeElement[] array, int index)
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
		return element;
	}

	/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> in the collection.</summary>
	/// <param name="element">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to find in the collection.</param>
	/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" />, or -1 if the element was not found in the collection.</returns>
	public int IndexOf(SoapExtensionTypeElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		return BaseIndexOf(element);
	}

	/// <summary>Removes a specified <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> from the collection.</summary>
	/// <param name="element">The <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to remove from the collection.</param>
	public void Remove(SoapExtensionTypeElement element)
	{
		if (element == null)
		{
			throw new ArgumentNullException("element");
		}
		BaseRemove(GetElementKey(element));
	}

	/// <summary>Removes the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> with the specified key from the collection.</summary>
	/// <param name="key">The key of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to be removed from the collection.</param>
	public void RemoveAt(object key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		BaseRemove(key);
	}

	/// <summary>Removes the element at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElement" /> to remove from the collection.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Creates a new <see cref="T:System.Web.Services.Configuration.SoapExtensionTypeElementCollection" />.</summary>
	public SoapExtensionTypeElementCollection()
	{
	}
}
