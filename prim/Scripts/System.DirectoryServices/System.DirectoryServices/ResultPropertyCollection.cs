using System.Collections;

namespace System.DirectoryServices;

/// <summary>Contains the properties of a <see cref="T:System.DirectoryServices.SearchResult" /> instance.</summary>
public class ResultPropertyCollection : DictionaryBase
{
	/// <summary>Gets the property from this collection that has the specified name.</summary>
	/// <param name="name">The name of the property to retrieve.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ResultPropertyValueCollection" /> that has the specified name.</returns>
	public ResultPropertyValueCollection this[string name] => (ResultPropertyValueCollection)base.Dictionary[name.ToLower()];

	/// <summary>Gets the names of the properties in this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the properties in this collection.</returns>
	public ICollection PropertyNames => base.Dictionary.Keys;

	/// <summary>Gets the values of the properties in this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values of the properties in this collection.</returns>
	public ICollection Values => base.Dictionary.Values;

	internal ResultPropertyCollection()
	{
	}

	internal void Add(string key, ResultPropertyValueCollection rpcoll)
	{
		base.Dictionary.Add(key.ToLower(), rpcoll);
	}

	/// <summary>Determines whether the property that has the specified name belongs to this collection.</summary>
	/// <param name="propertyName">The name of the property to find.</param>
	/// <returns>The return value is <see langword="true" /> if the specified property belongs to this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(string propertyName)
	{
		return base.Dictionary.Contains(propertyName.ToLower());
	}

	/// <summary>Copies the properties from this collection to an array, starting at a particular index of the array.</summary>
	/// <param name="array">An array of type <see cref="T:System.DirectoryServices.ResultPropertyValueCollection" /> that receives this collection's properties.</param>
	/// <param name="index">The zero-based array index at which to begin copying the properties.</param>
	public void CopyTo(ResultPropertyValueCollection[] array, int index)
	{
		foreach (ResultPropertyValueCollection value in Values)
		{
			array[index++] = value;
		}
	}
}
