namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStyleContentChanged" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellStyleContentChangedEventArgs : EventArgs
{
	private DataGridViewCellStyle cellStyle;

	private DataGridViewCellStyleScopes cellStyleScope;

	/// <summary>Gets the changed <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <returns>The changed <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCellStyle CellStyle => cellStyle;

	/// <summary>Gets the scope that is affected by the changed cell style.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyleScopes" /> that indicates which <see cref="T:System.Windows.Forms.DataGridView" /> entity owns the cell style that changed.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCellStyleScopes CellStyleScope => cellStyleScope;

	internal DataGridViewCellStyleContentChangedEventArgs(DataGridViewCellStyle cellStyle, DataGridViewCellStyleScopes cellStyleScope)
	{
		this.cellStyle = cellStyle;
		this.cellStyleScope = cellStyleScope;
	}
}
