namespace System.Windows.Forms;

/// <summary>Defines values for specifying how the row header width is adjusted. </summary>
public enum DataGridViewRowHeadersWidthSizeMode
{
	/// <summary>Users can adjust the column header width with the mouse.</summary>
	EnableResizing,
	/// <summary>Users cannot adjust the column header width with the mouse.</summary>
	DisableResizing,
	/// <summary>The row header width adjusts to fit the contents of all the row header cells. </summary>
	AutoSizeToAllHeaders,
	/// <summary>The row header width adjusts to fit the contents of all the row headers in the currently displayed rows. </summary>
	AutoSizeToDisplayedHeaders,
	/// <summary>The row header width adjusts to fit the contents of the first row header.</summary>
	AutoSizeToFirstHeader
}
