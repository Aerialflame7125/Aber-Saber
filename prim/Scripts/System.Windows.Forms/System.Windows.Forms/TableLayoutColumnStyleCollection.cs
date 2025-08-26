using System.Collections;

namespace System.Windows.Forms;

/// <summary>A collection that stores <see cref="T:System.Windows.Forms.ColumnStyle" /> objects.</summary>
public class TableLayoutColumnStyleCollection : TableLayoutStyleCollection
{
	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ColumnStyle" /> at the specified index.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ColumnStyle" /> at the specified index.</returns>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ColumnStyle" /> to get or set.</param>
	public new ColumnStyle this[int index]
	{
		get
		{
			return (ColumnStyle)base[index];
		}
		set
		{
			base[index] = value;
		}
	}

	internal TableLayoutColumnStyleCollection(TableLayoutPanel panel)
		: base(panel)
	{
	}

	/// <summary>Adds an item to the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
	/// <returns>The position into which the new element was inserted.</returns>
	/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
	public int Add(ColumnStyle columnStyle)
	{
		return Add((TableLayoutStyle)columnStyle);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.ColumnStyle" /> is in the collection.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ColumnStyle" /> is found in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />; otherwise, false.</returns>
	/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />. The value can be null.</param>
	public bool Contains(ColumnStyle columnStyle)
	{
		return ((IList)this).Contains((object)columnStyle);
	}

	/// <summary>Determines the index of a specific item in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
	/// <returns>The index of <paramref name="columnStyle" /> if found in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />; otherwise, -1.</returns>
	/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
	public int IndexOf(ColumnStyle columnStyle)
	{
		return ((IList)this).IndexOf((object)columnStyle);
	}

	/// <summary>Inserts a <see cref="T:System.Windows.Forms.ColumnStyle" /> into the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> at the specified position.</summary>
	/// <param name="index">The zero-based index at which <see cref="T:System.Windows.Forms.ColumnStyle" /> should be inserted.</param>
	/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to insert into the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
	public void Insert(int index, ColumnStyle columnStyle)
	{
		((IList)this).Insert(index, (object)columnStyle);
	}

	/// <summary>Removes the first occurrence of a specific <see cref="T:System.Windows.Forms.ColumnStyle" /> from the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
	/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to remove from the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />. The value can be null.</param>
	public void Remove(ColumnStyle columnStyle)
	{
		((IList)this).Remove((object)columnStyle);
	}
}
