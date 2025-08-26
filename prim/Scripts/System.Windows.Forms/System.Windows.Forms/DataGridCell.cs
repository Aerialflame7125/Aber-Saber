namespace System.Windows.Forms;

/// <summary>Identifies a cell in the grid.</summary>
/// <filterpriority>2</filterpriority>
public struct DataGridCell
{
	private int row;

	private int column;

	/// <summary>Gets or sets the number of a column in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	/// <returns>The number of the column.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnNumber
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

	/// <summary>Gets or sets the number of a row in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	/// <returns>The number of the row.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowNumber
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridCell" /> class.</summary>
	/// <param name="r">The number of a row in the <see cref="T:System.Windows.Forms.DataGrid" />. </param>
	/// <param name="c">The number of a column in the <see cref="T:System.Windows.Forms.DataGrid" />. </param>
	public DataGridCell(int r, int c)
	{
		row = r;
		column = c;
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.DataGridCell" /> is identical to a second <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
	/// <returns>true if the second object is identical to the first; otherwise, false.</returns>
	/// <param name="o">An object you are to comparing. </param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object o)
	{
		if (!(o is DataGridCell dataGridCell))
		{
			return false;
		}
		return dataGridCell.ColumnNumber == column && dataGridCell.RowNumber == row;
	}

	/// <summary>Gets a hash value that can be added to a <see cref="T:System.Collections.Hashtable" />.</summary>
	/// <returns>A number that uniquely identifies the <see cref="T:System.Windows.Forms.DataGridCell" /> in a <see cref="T:System.Collections.Hashtable" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return row ^ column;
	}

	/// <summary>Gets the row number and column number of the cell.</summary>
	/// <returns>A string containing the row number and column number.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return "DataGridCell {RowNumber = " + row + ", ColumnNumber = " + column + "}";
	}
}
