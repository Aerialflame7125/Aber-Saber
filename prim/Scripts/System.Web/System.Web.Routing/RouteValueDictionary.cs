using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Represents a case-insensitive collection of key/value pairs that you use in various places in the routing framework, such as when you define the default values for a route or when you generate a URL that is based on a route.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class RouteValueDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
{
	private Dictionary<string, object> _dictionary;

	/// <summary>Gets the number of key/value pairs that are in the collection.</summary>
	/// <returns>The number of key/value pairs that are in the collection.</returns>
	public int Count => _dictionary.Count;

	/// <summary>Gets a collection that contains the keys in the dictionary.</summary>
	/// <returns>A collection that contains the keys in the dictionary.</returns>
	public Dictionary<string, object>.KeyCollection Keys => _dictionary.Keys;

	/// <summary>Gets a collection that contains the values in the dictionary.</summary>
	/// <returns>A collection that contains the values in the dictionary.</returns>
	public Dictionary<string, object>.ValueCollection Values => _dictionary.Values;

	/// <summary>Gets or sets the value that is associated with the specified key.</summary>
	/// <param name="key">The key of the value to get or set.</param>
	/// <returns>The value that is associated with the specified key, or <see langword="null" /> if the key does not exist in the collection.</returns>
	public object this[string key]
	{
		get
		{
			TryGetValue(key, out var value);
			return value;
		}
		set
		{
			_dictionary[key] = value;
		}
	}

	ICollection<string> IDictionary<string, object>.Keys => _dictionary.Keys;

	ICollection<object> IDictionary<string, object>.Values => _dictionary.Values;

	bool ICollection<KeyValuePair<string, object>>.IsReadOnly => ((ICollection<KeyValuePair<string, object>>)_dictionary).IsReadOnly;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class that is empty. </summary>
	public RouteValueDictionary()
	{
		_dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class and adds values that are based on properties from the specified object. </summary>
	/// <param name="values">An object that contains properties that will be added as elements to the new collection.</param>
	public RouteValueDictionary(object values)
	{
		_dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		AddValues(values);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class and adds elements from the specified collection. </summary>
	/// <param name="dictionary">A collection whose elements are copied to the new collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="dictionary" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
	public RouteValueDictionary(IDictionary<string, object> dictionary)
	{
		_dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
	}

	/// <summary>Adds the specified value to the dictionary by using the specified key.</summary>
	/// <param name="key">The key of the element to add.</param>
	/// <param name="value">The value of the element to add.</param>
	public void Add(string key, object value)
	{
		_dictionary.Add(key, value);
	}

	private void AddValues(object values)
	{
		if (values == null)
		{
			return;
		}
		foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(values))
		{
			object value = property.GetValue(values);
			Add(property.Name, value);
		}
	}

	/// <summary>Removes all keys and values from the dictionary.</summary>
	public void Clear()
	{
		_dictionary.Clear();
	}

	/// <summary>Determines whether the dictionary contains the specified key.</summary>
	/// <param name="key">The key to locate in the dictionary.</param>
	/// <returns>
	///     <see langword="true" /> if the dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
	public bool ContainsKey(string key)
	{
		return _dictionary.ContainsKey(key);
	}

	/// <summary>Determines whether the dictionary contains a specific value.</summary>
	/// <param name="value">The value to locate in the dictionary.</param>
	/// <returns>
	///     <see langword="true" /> if the dictionary contains an element that has the specified value; otherwise, <see langword="false" />.</returns>
	public bool ContainsValue(object value)
	{
		return _dictionary.ContainsValue(value);
	}

	/// <summary>Returns an enumerator that you can use to iterate through the dictionary.</summary>
	/// <returns>A structure for reading data in the dictionary.</returns>
	public Dictionary<string, object>.Enumerator GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}

	/// <summary>Removes the value that has the specified key from the dictionary.</summary>
	/// <param name="key">The key of the element to remove.</param>
	/// <returns>
	///     <see langword="true" /> if the element is found and removed; otherwise, <see langword="false" />. This method returns <see langword="false" /> if <paramref name="key" /> is not found in the dictionary.</returns>
	public bool Remove(string key)
	{
		return _dictionary.Remove(key);
	}

	/// <summary>Gets a value that indicates whether a value is associated with the specified key.</summary>
	/// <param name="key">The key of the value to get.</param>
	/// <param name="value">When this method returns, contains the value that is associated with the specified key, if the key is found; otherwise, contains the appropriate default value for the type of the <paramref name="value" /> parameter that you provided as an <see langword="out" /> parameter. This parameter is passed uninitialized.</param>
	/// <returns>
	///     <see langword="true" /> if the dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
	public bool TryGetValue(string key, out object value)
	{
		return _dictionary.TryGetValue(key, out value);
	}

	void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
	{
		((ICollection<KeyValuePair<string, object>>)_dictionary).Add(item);
	}

	bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
	{
		return ((ICollection<KeyValuePair<string, object>>)_dictionary).Contains(item);
	}

	void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
	{
		((ICollection<KeyValuePair<string, object>>)_dictionary).CopyTo(array, arrayIndex);
	}

	bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
	{
		return ((ICollection<KeyValuePair<string, object>>)_dictionary).Remove(item);
	}

	IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
	/// <returns>A structure for reading data in the dictionary.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
