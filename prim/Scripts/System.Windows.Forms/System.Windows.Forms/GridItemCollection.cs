using System.Collections;

namespace System.Windows.Forms;

/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.GridItem" /> objects.</summary>
/// <filterpriority>2</filterpriority>
public class GridItemCollection : ICollection, IEnumerable
{
	internal class GridItemEnumerator : IEnumerator
	{
		private int nIndex;

		private GridItemCollection collection;

		object IEnumerator.Current => collection[nIndex];

		public GridItemEnumerator(GridItemCollection coll)
		{
			collection = coll;
			nIndex = -1;
		}

		public bool MoveNext()
		{
			nIndex++;
			return nIndex < collection.Count;
		}

		public void Reset()
		{
			nIndex = -1;
		}
	}

	private SortedList list;

	/// <summary>Specifies that the <see cref="T:System.Windows.Forms.GridItemCollection" /> has no entries. </summary>
	/// <filterpriority>1</filterpriority>
	public static GridItemCollection Empty = new GridItemCollection();

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => list.IsSynchronized;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
	object ICollection.SyncRoot => list.SyncRoot;

	/// <summary>Gets the number of grid items in the collection.</summary>
	/// <returns>The number of grid items in the collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int Count => list.Count;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.GridItem" /> at the specified index.</returns>
	/// <param name="index">The index of the grid item to return. </param>
	/// <filterpriority>1</filterpriority>
	public GridItem this[int index]
	{
		get
		{
			if (index >= list.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return (GridItem)list.GetByIndex(index);
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> with the matching label.</summary>
	/// <returns>The grid item whose label matches the <paramref name="label" /> parameter.</returns>
	/// <param name="label">A string value to match to a grid item label </param>
	/// <filterpriority>1</filterpriority>
	public GridItem this[string label] => (GridItem)list[label];

	internal GridItemCollection()
	{
		list = new SortedList();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
	/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in the array at which copying begins.</param>
	void ICollection.CopyTo(Array dest, int index)
	{
		list.CopyTo(dest, index);
	}

	internal void Add(GridItem grid_item)
	{
		string text = grid_item.Label;
		while (list.ContainsKey(text))
		{
			text += "_";
		}
		list.Add(text, grid_item);
	}

	internal void AddRange(GridItemCollection items)
	{
		foreach (GridItem item in items)
		{
			Add(item);
		}
	}

	internal int IndexOf(GridItem grid_item)
	{
		return list.IndexOfValue(grid_item);
	}

	/// <summary>Returns an enumeration of all the grid items in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
	/// <filterpriority>1</filterpriority>
	public IEnumerator GetEnumerator()
	{
		return new GridItemEnumerator(this);
	}

	internal void Clear()
	{
		list.Clear();
	}
}
