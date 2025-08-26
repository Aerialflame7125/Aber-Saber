namespace System.Windows.Forms;

/// <summary>Provides data for row-related <see cref="T:System.Windows.Forms.DataGridView" /> events. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewRowEventArgs : EventArgs
{
	private DataGridViewRow dataGridViewRow;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> associated with the event.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> associated with the event.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewRow Row => dataGridViewRow;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> class. </summary>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that the event occurred for.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewRow" /> is null.</exception>
	public DataGridViewRowEventArgs(DataGridViewRow dataGridViewRow)
	{
		this.dataGridViewRow = dataGridViewRow;
	}
}
