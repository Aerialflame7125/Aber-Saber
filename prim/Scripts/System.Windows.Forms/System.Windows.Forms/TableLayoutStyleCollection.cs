using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Implements the basic functionality for a collection of table layout styles.</summary>
[Editor("System.Windows.Forms.Design.StyleCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public abstract class TableLayoutStyleCollection : ICollection, IEnumerable, IList
{
	private ArrayList al = new ArrayList();

	private TableLayoutPanel table;

	/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
	/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
	bool IList.IsFixedSize => al.IsFixedSize;

	/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.IsReadOnly" /> property.</summary>
	/// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.</returns>
	bool IList.IsReadOnly => al.IsReadOnly;

	/// <summary>For a description of this method, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
	/// <returns>The element at the specified index.</returns>
	/// <param name="index">The zero-based index of the element to get or set.</param>
	object IList.this[int index]
	{
		get
		{
			return al[index];
		}
		set
		{
			if (((TableLayoutStyle)value).Owner != null)
			{
				throw new ArgumentException("Style is already owned");
			}
			((TableLayoutStyle)value).Owner = table;
			al[index] = value;
			table.PerformLayout();
		}
	}

	/// <summary>For a description of this method, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
	object ICollection.SyncRoot => al.SyncRoot;

	/// <summary>For a description of this method, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
	/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
	bool ICollection.IsSynchronized => al.IsSynchronized;

	/// <summary>Gets the number of styles actually contained in the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</summary>
	/// <returns>The number of styles actually contained in the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</returns>
	public int Count => al.Count;

	/// <summary>Gets or sets <see cref="T:System.Windows.Forms.TableLayoutStyle" /> at the specified index.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutStyle" /> at the specified index.</returns>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to get or set.</param>
	/// <exception cref="T:System.ArgumentException">The property value is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
	public TableLayoutStyle this[int index]
	{
		get
		{
			return (TableLayoutStyle)((IList)this)[index];
		}
		set
		{
			((IList)this)[index] = value;
		}
	}

	internal TableLayoutStyleCollection(TableLayoutPanel table)
	{
		this.table = table;
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
	/// <returns>The position into which <paramref name="style" /> was inserted.</returns>
	/// <param name="style">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
	int IList.Add(object style)
	{
		TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)style;
		if (tableLayoutStyle.Owner != null)
		{
			throw new ArgumentException("Style is already owned");
		}
		tableLayoutStyle.Owner = table;
		int result = al.Add(tableLayoutStyle);
		if (table != null)
		{
			table.PerformLayout();
		}
		return result;
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
	/// <returns>true if <paramref name="style" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
	/// <param name="style">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
	bool IList.Contains(object style)
	{
		return al.Contains((TableLayoutStyle)style);
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
	/// <returns>The index of <paramref name="style" /> if found in the list; otherwise, -1.</returns>
	/// <param name="style">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
	int IList.IndexOf(object style)
	{
		return al.IndexOf((TableLayoutStyle)style);
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which <paramref name="style" /> should be inserted.</param>
	/// <param name="style">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="style" /> is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
	void IList.Insert(int index, object style)
	{
		if (((TableLayoutStyle)style).Owner != null)
		{
			throw new ArgumentException("Style is already owned");
		}
		((TableLayoutStyle)style).Owner = table;
		al.Insert(index, (TableLayoutStyle)style);
		table.PerformLayout();
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
	/// <param name="style">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
	void IList.Remove(object style)
	{
		((TableLayoutStyle)style).Owner = null;
		al.Remove((TableLayoutStyle)style);
		table.PerformLayout();
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="startIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	void ICollection.CopyTo(Array array, int startIndex)
	{
		al.CopyTo(array, startIndex);
	}

	/// <summary>For a description of this method, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return al.GetEnumerator();
	}

	/// <summary>Adds a new <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to the end of the current collection.</summary>
	/// <returns>The position into which the new element was inserted.</returns>
	/// <param name="style">The <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutStyleCollection" />.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="style" /> is already assigned to another owner. You must first remove it from its current location or clone it.</exception>
	public int Add(TableLayoutStyle style)
	{
		return ((IList)this).Add((object)style);
	}

	/// <summary>Disassociates the collection from its associated <see cref="T:System.Windows.Forms.TableLayoutPanel" /> and empties the collection.</summary>
	public void Clear()
	{
		foreach (TableLayoutStyle item in al)
		{
			item.Owner = null;
		}
		al.Clear();
		table.PerformLayout();
	}

	/// <summary>Removes the style at the specified index of the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> to be removed.</param>
	public void RemoveAt(int index)
	{
		((TableLayoutStyle)al[index]).Owner = null;
		al.RemoveAt(index);
		table.PerformLayout();
	}
}
