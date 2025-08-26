using System.Collections;

namespace System.DirectoryServices;

/// <summary>Contains the values of a <see cref="T:System.DirectoryServices.SearchResult" /> property.</summary>
public class ResultPropertyValueCollection : ReadOnlyCollectionBase
{
	/// <summary>The <see cref="P:System.DirectoryServices.ResultPropertyValueCollection.Item(System.Int32)" /> property gets the property value that is located at a specified index.</summary>
	/// <param name="index">The zero-based index of the property value to retrieve.</param>
	/// <returns>The property value that is located at the specified index.</returns>
	public virtual object this[int index] => base.InnerList[index];

	internal ResultPropertyValueCollection()
	{
	}

	internal void Add(object component)
	{
		base.InnerList.Add(component);
	}

	internal void AddRange(object[] components)
	{
		base.InnerList.AddRange(components);
	}

	/// <summary>The <see cref="M:System.DirectoryServices.ResultPropertyValueCollection.Contains(System.Object)" /> method determines whether a specified property value is in this collection.</summary>
	/// <param name="value">The property value to find.</param>
	/// <returns>The return value is <see langword="true" /> if the specified property belongs to this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(object value)
	{
		return base.InnerList.Contains(value);
	}

	/// <summary>The <see cref="M:System.DirectoryServices.ResultPropertyValueCollection.CopyTo(System.Object[],System.Int32)" /> method copies the property values from this collection to an array, starting at a particular index of the array.</summary>
	/// <param name="values">An array of type <see cref="T:System.Object" /> that receives this collection's property values.</param>
	/// <param name="index">The zero-based array index at which to begin copying the property values.</param>
	public void CopyTo(object[] values, int index)
	{
		base.InnerList.CopyTo(values, index);
	}

	/// <summary>The <see cref="M:System.DirectoryServices.ResultPropertyValueCollection.IndexOf(System.Object)" /> method retrieves the index of a specified property value in this collection.</summary>
	/// <param name="value">The property value to find.</param>
	/// <returns>The zero-based index of the specified property value. If the object is not found, the return value is -1.</returns>
	public int IndexOf(object value)
	{
		return base.InnerList.IndexOf(value);
	}
}
