namespace System.Windows.Forms;

/// <summary>Specifies how a column contained in a <see cref="T:System.Windows.Forms.ListView" /> should be resized.</summary>
/// <filterpriority>2</filterpriority>
public enum ColumnHeaderAutoResizeStyle
{
	/// <summary>Specifies no resizing should occur.</summary>
	None,
	/// <summary>Specifies the column should be resized based on the length of the column header content.</summary>
	HeaderSize,
	/// <summary>Specifies the column should be resized based on the length of the column content.</summary>
	ColumnContent
}
