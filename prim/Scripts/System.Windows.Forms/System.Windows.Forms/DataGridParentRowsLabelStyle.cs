namespace System.Windows.Forms;

/// <summary>Specifies how the parent row labels of a <see cref="T:System.Windows.Forms.DataGrid" /> control are displayed.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridParentRowsLabelStyle
{
	/// <summary>Display no parent row labels.</summary>
	None,
	/// <summary>Displays the parent table name.</summary>
	TableName,
	/// <summary>Displays the parent column name.</summary>
	ColumnName,
	/// <summary>Displays both the parent table and column names.</summary>
	Both
}
