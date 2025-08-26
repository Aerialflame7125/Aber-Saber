using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.UserDeletingRow" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewRowCancelEventArgs : CancelEventArgs
{
	private DataGridViewRow dataGridViewRow;

	/// <summary>Gets the row that the user is deleting.</summary>
	/// <returns>The row that the user deleted.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewRow Row => dataGridViewRow;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCancelEventArgs" /> class. </summary>
	/// <param name="dataGridViewRow">The row the user is deleting.</param>
	public DataGridViewRowCancelEventArgs(DataGridViewRow dataGridViewRow)
	{
		this.dataGridViewRow = dataGridViewRow;
	}
}
