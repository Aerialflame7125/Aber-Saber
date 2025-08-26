using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a cell in a <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
[TypeConverter(typeof(TableLayoutPanelCellPositionTypeConverter))]
public struct TableLayoutPanelCellPosition
{
	private int column;

	private int row;

	/// <summary>Gets or sets the column number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
	/// <returns>The column number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
	public int Column
	{
		get
		{
			return column;
		}
		set
		{
			column = value;
		}
	}

	/// <summary>Gets or sets the row number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
	/// <returns>The row number of the current <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
	public int Row
	{
		get
		{
			return row;
		}
		set
		{
			row = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> class.</summary>
	/// <param name="column">The column position of the cell.</param>
	/// <param name="row">The row position of the cell.</param>
	public TableLayoutPanelCellPosition(int column, int row)
	{
		this.column = column;
		this.row = row;
	}

	/// <summary>Converts this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to a human readable string.</summary>
	/// <returns>A string that represents this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
	public override string ToString()
	{
		return column + "," + row;
	}

	/// <summary>Returns a hash code for this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
	/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</returns>
	public override int GetHashCode()
	{
		return column.GetHashCode() ^ row.GetHashCode();
	}

	/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> contains the same row and column as the specified <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />.</summary>
	/// <returns>true if <paramref name="other" /> is a <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> and has the same row and column as the specified <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />; otherwise, false.</returns>
	/// <param name="other">The <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to test.</param>
	public override bool Equals(object other)
	{
		if (other == null)
		{
			return false;
		}
		if (!(other is TableLayoutPanelCellPosition tableLayoutPanelCellPosition))
		{
			return false;
		}
		return tableLayoutPanelCellPosition.column == column && tableLayoutPanelCellPosition.row == row;
	}

	/// <summary>Compares two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects. The result specifies whether the values of the <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Row" /> and <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Column" /> properties of the two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects are equal.</summary>
	/// <returns>true if <paramref name="p1" /> and <paramref name="p2" /> are equal; otherwise, false.</returns>
	/// <param name="p1">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
	/// <param name="p2">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
	public static bool operator ==(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
	{
		return p1.column == p2.column && p1.row == p2.row;
	}

	/// <summary>Compares two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects. The result specifies whether the values of the <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Row" /> and <see cref="P:System.Windows.Forms.TableLayoutPanelCellPosition.Column" /> properties of the two <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> objects are unequal.</summary>
	/// <returns>true if <paramref name="p1" /> and <paramref name="p2" /> differ; otherwise, false.</returns>
	/// <param name="p1">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
	/// <param name="p2">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> to compare.</param>
	public static bool operator !=(TableLayoutPanelCellPosition p1, TableLayoutPanelCellPosition p2)
	{
		return p1.column != p2.column || p1.row != p2.row;
	}
}
