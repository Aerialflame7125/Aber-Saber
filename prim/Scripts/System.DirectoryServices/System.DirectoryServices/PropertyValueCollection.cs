using System.Collections;

namespace System.DirectoryServices;

/// <summary>Contains the values of a <see cref="T:System.DirectoryServices.DirectoryEntry" /> property.</summary>
public class PropertyValueCollection : CollectionBase
{
	private bool _Mbit;

	private DirectoryEntry _parent;

	internal bool Mbit
	{
		get
		{
			return _Mbit;
		}
		set
		{
			_Mbit = value;
		}
	}

	/// <summary>Gets or sets the property value that is located at a specified index of this collection.</summary>
	/// <param name="index">The zero-based index of the property value.</param>
	/// <returns>The property value at the specified index.</returns>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">The index is less than zero (0) or greater than the size of the collection.</exception>
	public object this[int index]
	{
		get
		{
			return base.List[index];
		}
		set
		{
			base.List[index] = value;
			_Mbit = true;
		}
	}

	/// <summary>Gets the property name for the attributes in the value collection.</summary>
	/// <returns>A string that contains the name of the property with the values that are included in this <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object.</returns>
	[System.MonoTODO]
	public string PropertyName => string.Empty;

	/// <summary>Gets or sets the values of the collection.</summary>
	/// <returns>If the collection is empty, the property value is a null reference (<see langword="Nothing" /> in Visual Basic). If the collection contains one value, the property value is that value. If the collection contains multiple values, the property value equals a copy of an array of those values.  
	///  If setting this property, the value or values are added to the <see cref="T:System.DirectoryServices.PropertyValueCollection" />. Setting this property to a null reference (<see langword="Nothing" />) clears the collection.</returns>
	public object Value
	{
		get
		{
			switch (base.Count)
			{
			case 0:
				return null;
			case 1:
				return base.List[0];
			default:
			{
				Array array = new object[base.Count];
				for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
				{
					array.SetValue(base.List[i], i);
				}
				return array;
			}
			}
		}
		set
		{
			if (value != null || base.List.Count != 0)
			{
				base.List.Clear();
				if (value != null)
				{
					Add(value);
				}
				_Mbit = true;
			}
		}
	}

	internal PropertyValueCollection(DirectoryEntry parent)
	{
		_Mbit = false;
		_parent = parent;
	}

	/// <summary>Appends the specified <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to append to this collection.</param>
	/// <returns>The zero-based index of the <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object that is appended to this collection.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	public int Add(object value)
	{
		if (Contains(value))
		{
			return -1;
		}
		_Mbit = true;
		return base.List.Add(value);
	}

	/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.PropertyValueCollection" /> array that contains the objects to append to this collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	public void AddRange(object[] value)
	{
		foreach (object value2 in value)
		{
			Add(value2);
		}
	}

	/// <summary>Appends the contents of the <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to this collection.</summary>
	/// <param name="value">A <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object that contains the objects to append to this collection.</param>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	public void AddRange(PropertyValueCollection value)
	{
		foreach (object item in value)
		{
			Add(item);
		}
	}

	/// <summary>Retrieves the index of a specified property value in this collection.</summary>
	/// <param name="value">The property value to find.</param>
	/// <returns>The zero-based index of the specified property value. If the object is not found, the return value is -1.</returns>
	public int IndexOf(object value)
	{
		return base.List.IndexOf(value);
	}

	/// <summary>Inserts a property value into this collection at a specified index.</summary>
	/// <param name="index">The zero-based index at which to insert the property value.</param>
	/// <param name="value">The property value to insert.</param>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">The index is less than 0 (zero) or greater than the size of the collection.</exception>
	public void Insert(int index, object value)
	{
		base.List.Insert(index, value);
		_Mbit = true;
	}

	/// <summary>Removes a specified property value from this collection.</summary>
	/// <param name="value">The property value to remove.</param>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public void Remove(object value)
	{
		base.List.Remove(value);
		_Mbit = true;
	}

	/// <summary>Determines whether the specified <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object is in this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.PropertyValueCollection" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the specified property belongs to this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(object value)
	{
		return base.List.Contains(value);
	}

	internal bool ContainsCaselessStringValue(string value)
	{
		for (int i = 0; i < base.Count; i++)
		{
			string strB = (string)base.List[i];
			if (string.Compare(value, strB, ignoreCase: true) == 0)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.PropertyValueCollection" /> objects in this collection to the specified array, starting at the specified index in the target array.</summary>
	/// <param name="array">The array of <see cref="T:System.DirectoryServices.PropertyValueCollection" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
	public void CopyTo(object[] array, int index)
	{
		foreach (object item in base.List)
		{
			array[index++] = item;
		}
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
	[System.MonoTODO]
	protected override void OnClearComplete()
	{
		if (_parent != null)
		{
			_parent.CommitDeferred();
		}
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnInsertComplete(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
	/// <param name="value">The new value of the element at <paramref name="index" />.</param>
	[System.MonoTODO]
	protected override void OnInsertComplete(int index, object value)
	{
		if (_parent != null)
		{
			_parent.CommitDeferred();
		}
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemoveComplete(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which value can be found.</param>
	/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
	[System.MonoTODO]
	protected override void OnRemoveComplete(int index, object value)
	{
		if (_parent != null)
		{
			_parent.CommitDeferred();
		}
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSetComplete(System.Int32,System.Object,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
	/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
	/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
	[System.MonoTODO]
	protected override void OnSetComplete(int index, object oldValue, object newValue)
	{
		if (_parent != null)
		{
			_parent.CommitDeferred();
		}
	}
}
