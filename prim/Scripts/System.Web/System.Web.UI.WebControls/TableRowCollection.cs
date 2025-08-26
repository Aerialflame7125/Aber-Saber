using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Encapsulates a collection of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects that represent a single row in a <see cref="T:System.Web.UI.WebControls.Table" /> control. This class cannot be inherited.</summary>
[Editor("System.Web.UI.Design.WebControls.TableRowsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public sealed class TableRowCollection : IList, ICollection, IEnumerable
{
	private ControlCollection cc;

	private Table owner;

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.TableRow" /> objects in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. The default is 0.</returns>
	public int Count => cc.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index.</summary>
	/// <param name="index">An ordinal index value that specifies which <see cref="T:System.Web.UI.WebControls.TableRow" /> object to return. This index is zero-based.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableRow" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</returns>
	public TableRow this[int index] => (TableRow)cc[index];

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
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

	internal TableRowCollection(Table table)
	{
		if (table == null)
		{
			throw new ArgumentNullException("table");
		}
		cc = table.Controls;
		owner = table;
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to add to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.TableRow" />.</returns>
	public int Add(TableRow row)
	{
		if (row == null)
		{
			throw new NullReferenceException();
		}
		if (row.TableRowSectionSet)
		{
			owner.GenerateTableSections = true;
		}
		row.Container = this;
		int num = cc.IndexOf(row);
		if (num < 0)
		{
			cc.Add(row);
			num = cc.Count;
		}
		return num;
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> object to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index location.</summary>
	/// <param name="index">The location in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at which to add the <see cref="T:System.Web.UI.WebControls.TableRow" />. </param>
	/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to add to the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
	public void AddAt(int index, TableRow row)
	{
		if (row == null)
		{
			throw new NullReferenceException();
		}
		if (cc.IndexOf(row) < 0)
		{
			if (row.TableRowSectionSet)
			{
				owner.GenerateTableSections = true;
			}
			row.Container = this;
			cc.AddAt(index, row);
		}
	}

	/// <summary>Appends the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects from the specified array to the end of the collection.</summary>
	/// <param name="rows">The array containing the <see cref="T:System.Web.UI.WebControls.TableRow" /> objects to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="rows" /> parameter is <see langword="null" />. </exception>
	public void AddRange(TableRow[] rows)
	{
		foreach (TableRow tableRow in rows)
		{
			if (tableRow == null)
			{
				throw new NullReferenceException();
			}
			if (cc.IndexOf(tableRow) < 0)
			{
				if (tableRow.TableRowSectionSet)
				{
					owner.GenerateTableSections = true;
				}
				tableRow.Container = this;
				cc.Add(tableRow);
			}
		}
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.TableRow" /> controls from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
	public void Clear()
	{
		owner.GenerateTableSections = false;
		cc.Clear();
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive copied contents. </param>
	public void CopyTo(Array array, int index)
	{
		cc.CopyTo(array, index);
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableRow" /> objects within the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.TableRow" /> objects within the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return cc.GetEnumerator();
	}

	/// <summary>Returns a value that represents the index of the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to search for in the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
	/// <returns>The ordinal index position of the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> within the collection. The default is -1, which indicates that the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> has not been found.</returns>
	public int GetRowIndex(TableRow row)
	{
		return cc.IndexOf(row);
	}

	internal void RowTableSectionSet()
	{
		owner.GenerateTableSections = true;
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.WebControls.TableRow" /> object to remove from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
	public void Remove(TableRow row)
	{
		if (row != null)
		{
			row.Container = null;
		}
		cc.Remove(row);
	}

	/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.TableRow" /> from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" /> at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.TableRow" /> object to remove from the <see cref="T:System.Web.UI.WebControls.TableRowCollection" />. </param>
	public void RemoveAt(int index)
	{
		TableRow tableRow = this[index];
		if (tableRow != null)
		{
			tableRow.Container = null;
		}
		cc.RemoveAt(index);
	}

	/// <summary>Adds an object to the collection.</summary>
	/// <param name="o">The object to add to the collection.</param>
	/// <returns>The index at which the object was added to the collection.</returns>
	int IList.Add(object value)
	{
		return Add(value as TableRow);
	}

	/// <summary>Determines whether the specified object is contained within the collection.</summary>
	/// <param name="o">The object to locate within the collection.</param>
	/// <returns>
	///     <see langword="true" /> if the object is in the collection; otherwise, <see langword="false" />.</returns>
	bool IList.Contains(object value)
	{
		return cc.Contains(value as TableRow);
	}

	/// <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the collection.</summary>
	/// <param name="o">The object to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1 if the object is not in the collection.</returns>
	int IList.IndexOf(object value)
	{
		return cc.IndexOf(value as TableRow);
	}

	/// <summary>Inserts an object into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
	/// <param name="o">The object to insert into the collection.</param>
	void IList.Insert(int index, object value)
	{
		AddAt(index, value as TableRow);
	}

	/// <summary>Removes an object from the collection.</summary>
	/// <param name="o">The object to remove from the collection.</param>
	void IList.Remove(object value)
	{
		Remove(value as TableRow);
	}
}
