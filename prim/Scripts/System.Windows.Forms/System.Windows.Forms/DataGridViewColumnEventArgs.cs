namespace System.Windows.Forms;

/// <summary>Provides data for column-related events of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewColumnEventArgs : EventArgs
{
	private DataGridViewColumn dataGridViewColumn;

	/// <summary>Gets the column that the event occurs for.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewColumn Column => dataGridViewColumn;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> class. </summary>
	/// <param name="dataGridViewColumn">The column that the event occurs for.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumn" /> is null.</exception>
	public DataGridViewColumnEventArgs(DataGridViewColumn dataGridViewColumn)
	{
		this.dataGridViewColumn = dataGridViewColumn;
	}
}
