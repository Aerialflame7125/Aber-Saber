namespace System.Windows.Forms;

/// <summary>Describes how cells of a DataGridView control can be selected.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridViewSelectionMode
{
	/// <summary>One or more individual cells can be selected.</summary>
	CellSelect,
	/// <summary>The entire row will be selected by clicking its row's header or a cell contained in that row.</summary>
	FullRowSelect,
	/// <summary>The entire column will be selected by clicking the column's header or a cell contained in that column.</summary>
	FullColumnSelect,
	/// <summary>The row will be selected by clicking in the row's header cell. An individual cell can be selected by clicking that cell.</summary>
	RowHeaderSelect,
	/// <summary>The column will be selected by clicking in the column's header cell. An individual cell can be selected by clicking that cell.</summary>
	ColumnHeaderSelect
}
