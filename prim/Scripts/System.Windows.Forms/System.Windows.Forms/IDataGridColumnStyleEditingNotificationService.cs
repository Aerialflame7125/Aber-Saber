namespace System.Windows.Forms;

/// <summary>Provides an editing notification interface.</summary>
/// <filterpriority>2</filterpriority>
public interface IDataGridColumnStyleEditingNotificationService
{
	/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> that the user has begun editing the column.</summary>
	/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that is editing the column. </param>
	/// <filterpriority>1</filterpriority>
	void ColumnStartedEditing(Control editingControl);
}
