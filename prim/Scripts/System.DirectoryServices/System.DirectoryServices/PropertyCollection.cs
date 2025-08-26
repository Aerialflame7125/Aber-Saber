using System.Collections;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.PropertyCollection" /> class contains the properties of a <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
public class PropertyCollection : IDictionary, ICollection, IEnumerable
{
	private ArrayList m_oKeys = new ArrayList();

	private Hashtable m_oValues = new Hashtable();

	private DirectoryEntry _parent;

	/// <summary>Gets the number of properties in this collection.</summary>
	/// <returns>The number of properties in this collection.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	/// <exception cref="T:System.NotSupportedException">The directory cannot report the number of properties.</exception>
	public int Count => m_oValues.Count;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
	/// <returns>
	///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool ICollection.IsSynchronized => m_oValues.IsSynchronized;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
	object ICollection.SyncRoot => m_oValues.SyncRoot;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, <see langword="false" />.</returns>
	bool IDictionary.IsFixedSize => m_oKeys.IsFixedSize;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, <see langword="false" />.</returns>
	bool IDictionary.IsReadOnly => m_oKeys.IsReadOnly;

	/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</returns>
	ICollection IDictionary.Keys => m_oValues.Keys;

	/// <summary>Gets the names of the properties in this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains the names of the properties in this collection.</returns>
	public ICollection PropertyNames => m_oValues.Keys;

	/// <summary>Gets or sets the element with the specified key.</summary>
	/// <param name="key">The key of the element to get or set.</param>
	/// <returns>The element with the specified key.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IDictionary" /> object is read-only.  
	///  -or-  
	///  The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</exception>
	object IDictionary.this[object oKey]
	{
		get
		{
			return m_oValues[oKey];
		}
		set
		{
			m_oValues[oKey] = value;
		}
	}

	/// <summary>Gets the values of the properties in this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values of the properties in this collection.</returns>
	public ICollection Values => m_oValues.Values;

	/// <summary>Gets the specified property.</summary>
	/// <param name="propertyName">The name of the property to retrieve.</param>
	/// <returns>The value of the specified property.</returns>
	public PropertyValueCollection this[string propertyName]
	{
		get
		{
			if (Contains(propertyName))
			{
				return (PropertyValueCollection)m_oValues[propertyName.ToLower()];
			}
			PropertyValueCollection propertyValueCollection = new PropertyValueCollection(_parent);
			Add(propertyName.ToLower(), propertyValueCollection);
			return propertyValueCollection;
		}
	}

	internal PropertyCollection()
		: this(null)
	{
	}

	internal PropertyCollection(DirectoryEntry parent)
	{
		_parent = parent;
	}

	private void ICopyTo(Array oArray, int iArrayIndex)
	{
		m_oValues.CopyTo(oArray, iArrayIndex);
	}

	/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.  
	/// -or-  
	/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
	void ICollection.CopyTo(Array oArray, int iArrayIndex)
	{
		ICopyTo(oArray, iArrayIndex);
	}

	/// <summary>Copies the all objects in this collection to an array, starting at the specified index in the target array.</summary>
	/// <param name="array">The array of <see cref="T:System.DirectoryServices.PropertyValueCollection" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public void CopyTo(PropertyValueCollection[] array, int index)
	{
		ICopyTo(array, index);
	}

	private void Add(object oKey, object oValue)
	{
		m_oKeys.Add(oKey);
		m_oValues.Add(oKey, oValue);
	}

	/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
	/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" /> object.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> is read-only.  
	///  -or-  
	///  The <see cref="T:System.Collections.IDictionary" /> has a fixed size.</exception>
	void IDictionary.Add(object oKey, object oValue)
	{
		Add(oKey, oValue);
	}

	/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.</exception>
	void IDictionary.Clear()
	{
		m_oValues.Clear();
		m_oKeys.Clear();
	}

	private bool IContains(object oKey)
	{
		return m_oValues.Contains(oKey);
	}

	/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
	/// <param name="value">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> is <see langword="null" />.</exception>
	bool IDictionary.Contains(object oKey)
	{
		return IContains(oKey);
	}

	/// <summary>Determines whether the specified property is in this collection.</summary>
	/// <param name="propertyName">The name of the property to find.</param>
	/// <returns>The return value is <see langword="true" /> if the specified property belongs to this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(string propertyName)
	{
		return IContains(propertyName.ToLower());
	}

	/// <summary>Returns an enumerator that you can use to iterate through this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> that you can use to iterate through this collection.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public IDictionaryEnumerator GetEnumerator()
	{
		return m_oValues.GetEnumerator();
	}

	/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
	/// <param name="key">The key of the element to remove.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="key" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.  
	///  -or-  
	///  The <see cref="T:System.Collections.IDictionary" /> has a fixed size.</exception>
	void IDictionary.Remove(object oKey)
	{
		m_oValues.Remove(oKey);
		m_oKeys.Remove(oKey);
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" /> object.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" />.object.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return m_oValues.GetEnumerator();
	}
}
