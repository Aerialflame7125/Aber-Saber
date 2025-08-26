namespace System.Windows.Forms;

/// <summary>Specifies the <see cref="T:System.Windows.Forms.DataGridView" /> entity that owns the cell style that was changed.</summary>
[Flags]
public enum DataGridViewCellStyleScopes
{
	/// <summary>The owning entity is unspecified.</summary>
	None = 0,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property changed.</summary>
	Cell = 1,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewColumn.DefaultCellStyle" /> property changed.</summary>
	Column = 2,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewRow.DefaultCellStyle" /> property changed.</summary>
	Row = 4,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.DefaultCellStyle" /> property changed.</summary>
	DataGridView = 8,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyle" /> property changed.</summary>
	ColumnHeaders = 0x10,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property changed.</summary>
	RowHeaders = 0x20,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.RowsDefaultCellStyle" /> property changed.</summary>
	Rows = 0x40,
	/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyle" /> property changed.</summary>
	AlternatingRows = 0x80
}
