namespace System.Windows.Forms;

/// <summary>Specifies a location in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridViewHitTestType
{
	/// <summary>An empty part of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	None,
	/// <summary>A cell in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	Cell,
	/// <summary>A column header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	ColumnHeader,
	/// <summary>A row header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	RowHeader,
	/// <summary>The top left column header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	TopLeftHeader,
	/// <summary>The horizontal scroll bar of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	HorizontalScrollBar,
	/// <summary>The vertical scroll bar of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	VerticalScrollBar
}
