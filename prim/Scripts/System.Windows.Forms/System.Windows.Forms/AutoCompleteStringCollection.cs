using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls. </summary>
/// <filterpriority>2</filterpriority>
public class AutoCompleteStringCollection : ICollection, IEnumerable, IList
{
	private ArrayList list;

	/// <summary>Gets a value indicating whether the collection has a fixed size. For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
	/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets a value indicating whether the collection is read-only. For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
	/// <returns>true if the collection is read-only; otherwise, false.</returns>
	bool IList.IsReadOnly => false;

	/// <summary>Gets the element at a specified index. For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
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
			this[index] = (string)value;
		}
	}

	/// <filterpriority>1</filterpriority>
	public int Count => list.Count;

	/// <filterpriority>2</filterpriority>
	public bool IsSynchronized => false;

	/// <filterpriority>1</filterpriority>
	public object SyncRoot => this;

	/// <filterpriority>1</filterpriority>
	public bool IsReadOnly => false;

	/// <returns>The <see cref="T:System.String" /> at the specified position.</returns>
	/// <param name="index">The index at which to get or set the <see cref="T:System.String" />.</param>
	/// <filterpriority>1</filterpriority>
	public string this[int index]
	{
		get
		{
			return (string)list[index];
		}
		set
		{
			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, list[index]));
			list[index] = value;
			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
		}
	}

	public event CollectionChangeEventHandler CollectionChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> class. </summary>
	public AutoCompleteStringCollection()
	{
		list = new ArrayList();
	}

	/// <summary>Copies the strings of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index. For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the strings copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Adds a string to the collection. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
	/// <returns>The index at which the <paramref name="value" /> has been added. </returns>
	/// <param name="value">The string to be added to the collection</param>
	int IList.Add(object value)
	{
		return Add((string)value);
	}

	/// <summary>Determines where the collection contains a specified string. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
	/// <returns>true if <paramref name="value" /> is found in the collection; otherwise, false.</returns>
	/// <param name="value">The string to locate in the collection.</param>
	bool IList.Contains(object value)
	{
		return Contains((string)value);
	}

	/// <summary>Determines the index of a specified string in the collection. For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
	/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
	/// <param name="value">The string to locate in the collection.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf((string)value);
	}

	/// <summary>Inserts an item to the collection at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The string to insert into the collection.</param>
	void IList.Insert(int index, object value)
	{
		Insert(index, (string)value);
	}

	/// <summary>Removes the first occurrence of a specific string from the collection. For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
	/// <param name="value">The string to remove from the collection.</param>
	void IList.Remove(object value)
	{
		Remove((string)value);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.AutoCompleteStringCollection.CollectionChanged" /> event. </summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
	protected void OnCollectionChanged(CollectionChangeEventArgs e)
	{
		if (this.CollectionChanged != null)
		{
			this.CollectionChanged(this, e);
		}
	}

	/// <filterpriority>1</filterpriority>
	public IEnumerator GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Copies an array of <see cref="T:System.String" /> objects into the collection, starting at the specified position.</summary>
	/// <param name="array">The <see cref="T:System.String" /> objects to add to the collection.</param>
	/// <param name="index">The position within the collection at which to start the insertion. </param>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(string[] array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Inserts a new <see cref="T:System.String" /> into the collection.</summary>
	/// <returns>The position in the collection where the <see cref="T:System.String" /> was added.</returns>
	/// <param name="value">The <see cref="T:System.String" /> to add to the collection.</param>
	/// <filterpriority>1</filterpriority>
	public int Add(string value)
	{
		int result = list.Add(value);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
		return result;
	}

	/// <summary>Adds the elements of a <see cref="T:System.String" /> collection to the end. </summary>
	/// <param name="value">The strings to add to the collection.</param>
	/// <filterpriority>1</filterpriority>
	public void AddRange(string[] value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value", "Argument cannot be null!");
		}
		list.AddRange(value);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Removes all strings from the collection.</summary>
	/// <filterpriority>1</filterpriority>
	public void Clear()
	{
		list.Clear();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Indicates whether the <see cref="T:System.String" /> exists within the collection.</summary>
	/// <returns>true if the <see cref="T:System.String" /> exists within the collection; otherwise, false.</returns>
	/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(string value)
	{
		return list.Contains(value);
	}

	/// <summary>Obtains the position of the specified string within the collection.</summary>
	/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(string value)
	{
		return list.IndexOf(value);
	}

	/// <summary>Inserts the string into a specific index in the collection.</summary>
	/// <param name="index">The position at which to insert the string.</param>
	/// <param name="value">The string to insert.</param>
	/// <filterpriority>1</filterpriority>
	public void Insert(int index, string value)
	{
		list.Insert(index, value);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
	}

	/// <summary>Removes a string from the collection. </summary>
	/// <param name="value">The <see cref="T:System.String" /> to remove.</param>
	/// <filterpriority>1</filterpriority>
	public void Remove(string value)
	{
		list.Remove(value);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
	}

	/// <summary>Removes the string at the specified index.</summary>
	/// <param name="index">The zero-based index of the string to remove.</param>
	/// <filterpriority>1</filterpriority>
	public void RemoveAt(int index)
	{
		string element = this[index];
		list.RemoveAt(index);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, element));
	}
}
