namespace System.Windows.Forms;

/// <summary>Defines constants that indicate whether content is copied from a <see cref="T:System.Windows.Forms.DataGridView" /> control to the Clipboard.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridViewClipboardCopyMode
{
	/// <summary>Copying to the Clipboard is disabled.</summary>
	Disable,
	/// <summary>The text values of selected cells can be copied to the Clipboard. Row or column header text is included for rows or columns that contain selected cells only when the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property is set to <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" /> and at least one header is selected. </summary>
	EnableWithAutoHeaderText,
	/// <summary>The text values of selected cells can be copied to the Clipboard. Header text is not included.</summary>
	EnableWithoutHeaderText,
	/// <summary>The text values of selected cells can be copied to the Clipboard. Header text is included for rows and columns that contain selected cells.  </summary>
	EnableAlwaysIncludeHeaderText
}
