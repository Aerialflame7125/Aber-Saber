using System.Collections;

namespace System.Windows.Forms.Layout;

/// <summary>Represents a collection of objects.</summary>
public class ArrangedElementCollection : ICollection, IEnumerable, IList
{
	internal ArrayList list;

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
	/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
	bool IList.IsFixedSize => IsFixedSize;

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
	/// <returns>The element at the specified index.</returns>
	/// <param name="index">The zero-based index of the element to get.</param>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			this[index] = value;
		}
	}

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
	/// <returns>true if access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is synchronized (thread safe); otherwise, false.</returns>
	bool ICollection.IsSynchronized => list.IsSynchronized;

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
	object ICollection.SyncRoot => list.IsSynchronized;

	/// <summary>Gets the number of elements in the collection.</summary>
	/// <returns>The number of elements currently contained in the collection.</returns>
	public virtual int Count => list.Count;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
	public virtual bool IsReadOnly => list.IsReadOnly;

	internal bool IsFixedSize => list.IsFixedSize;

	internal object this[int index]
	{
		get
		{
			return list[index];
		}
		set
		{
			list[index] = value;
		}
	}

	internal ArrangedElementCollection()
	{
		list = new ArrayList();
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
	/// <returns>The position into which the new element was inserted.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
	int IList.Add(object value)
	{
		return Add(value);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Clear" /> method.</summary>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
	/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
	bool IList.Contains(object value)
	{
		return Contains(value);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
	/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf(value);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
	void IList.Insert(int index, object value)
	{
		throw new NotSupportedException();
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
	/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
	void IList.Remove(object value)
	{
		Remove(value);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method.</summary>
	/// <param name="index">The zero-based index of the item to remove.</param>
	void IList.RemoveAt(int index)
	{
		list.RemoveAt(index);
	}

	/// <summary>Copies the entire contents of this collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source element cannot be cast automatically to the type of <paramref name="array" />.</exception>
	public void CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Determines whether two <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> instances are equal.</summary>
	/// <returns>true if the specified <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is equal to the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> to compare with the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</param>
	public override bool Equals(object obj)
	{
		if (obj is ArrangedElementCollection && this == obj)
		{
			return true;
		}
		return false;
	}

	/// <summary>Returns an enumerator for the entire collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.</returns>
	public virtual IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	internal int Add(object value)
	{
		return list.Add(value);
	}

	internal void Clear()
	{
		list.Clear();
	}

	internal bool Contains(object value)
	{
		return list.Contains(value);
	}

	internal int IndexOf(object value)
	{
		return list.IndexOf(value);
	}

	internal void Insert(int index, object value)
	{
		list.Insert(index, value);
	}

	internal void Remove(object value)
	{
		list.Remove(value);
	}

	internal void InternalRemoveAt(int index)
	{
		list.RemoveAt(index);
	}
}
