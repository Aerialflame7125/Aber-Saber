using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Encapsulates a collection of <see cref="T:System.Web.UI.WebControls.TableHeaderCell" /> and <see cref="T:System.Web.UI.WebControls.TableCell" /> objects that make up a row in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This class cannot be inherited.</summary>
[Editor("System.Web.UI.Design.WebControls.TableCellsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public sealed class TableCellCollection : IList, ICollection, IEnumerable
{
	private ControlCollection cc;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. The default is 0.</returns>
	public int Count => cc.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index.</summary>
	/// <param name="index">An ordinal index value that specifies the <see cref="T:System.Web.UI.WebControls.TableCell" /> to return. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</returns>
	public TableCell this[int index] => (TableCell)cc[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the list has a fixed size; otherwise, <see langword="false" /></returns>
	bool IList.IsFixedSize => false;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
	/// <param name="index">The zero-based index of the element to get or set. </param>
	/// <returns>The element at the specified index.</returns>
	object IList.this[int index]
	{
		get
		{
			return cc[index];
		}
		set
		{
			cc.AddAt(index, (TableRow)value);
			cc.RemoveAt(index + 1);
		}
	}

	internal TableCellCollection(TableRow tr)
	{
		cc = tr.Controls;
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> to the end of the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to add to the collection. </param>
	/// <returns>The index number of the <see cref="T:System.Web.UI.WebControls.TableCell" />.</returns>
	public int Add(TableCell cell)
	{
		int num = cc.IndexOf(cell);
		if (num < 0)
		{
			cc.Add(cell);
			num = cc.Count;
		}
		return num;
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index location.</summary>
	/// <param name="index">The location in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at which to add the <see cref="T:System.Web.UI.WebControls.TableCell" />. </param>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to add to the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
	public void AddAt(int index, TableCell cell)
	{
		if (cc.IndexOf(cell) < 0)
		{
			cc.AddAt(index, cell);
		}
	}

	/// <summary>Appends the <see cref="T:System.Web.UI.WebControls.TableCell" /> objects from the specified array to the end of the collection.</summary>
	/// <param name="cells">The array containing the <see cref="T:System.Web.UI.WebControls.TableCell" /> objects to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="cells" /> parameter is <see langword="null" />. </exception>
	public void AddRange(TableCell[] cells)
	{
		foreach (TableCell tableCell in cells)
		{
			if (cc.IndexOf(tableCell) < 0)
			{
				cc.Add(tableCell);
			}
		}
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	public void Clear()
	{
		cc.Clear();
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
	public void CopyTo(Array array, int index)
	{
		cc.CopyTo(array, index);
	}

	/// <summary>Returns a value that represents the index of the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to get the index of in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
	/// <returns>The index of the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> within the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. The default is -1, which indicates that a match has not been found.</returns>
	public int GetCellIndex(TableCell cell)
	{
		return cc.IndexOf(cell);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects in the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableCell" /> objects within the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return cc.GetEnumerator();
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to remove from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
	public void Remove(TableCell cell)
	{
		cc.Remove(cell);
	}

	/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.TableCell" /> from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" /> at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.TableCell" /> to remove from the <see cref="T:System.Web.UI.WebControls.TableCellCollection" />. </param>
	public void RemoveAt(int index)
	{
		cc.RemoveAt(index);
	}

	/// <summary>Adds an object to the collection.</summary>
	/// <param name="o">The object to add to the collection.</param>
	/// <returns>The index at which the object was added to the collection.</returns>
	int IList.Add(object value)
	{
		cc.Add((TableRow)value);
		return cc.IndexOf((TableRow)value);
	}

	/// <summary>Determines whether the specified object is contained within the collection.</summary>
	/// <param name="o">The object to locate within the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the object is in the collection; otherwise, <see langword="false" />.</returns>
	bool IList.Contains(object value)
	{
		return cc.Contains((TableRow)value);
	}

	/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="o">The object to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1 if the object is not in the collection.</returns>
	int IList.IndexOf(object value)
	{
		return cc.IndexOf((TableRow)value);
	}

	/// <summary>Inserts an object into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
	/// <param name="o">The object to insert into the collection.</param>
	void IList.Insert(int index, object value)
	{
		cc.AddAt(index, (TableRow)value);
	}

	/// <summary>Removes an object from the collection.</summary>
	/// <param name="o">The object to remove from the collection.</param>
	void IList.Remove(object value)
	{
		cc.Remove((TableRow)value);
	}
}
