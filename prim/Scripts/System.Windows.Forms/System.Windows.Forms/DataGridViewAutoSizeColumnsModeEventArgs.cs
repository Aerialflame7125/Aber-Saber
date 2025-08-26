namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnsModeChanged" /> event. </summary>
public class DataGridViewAutoSizeColumnsModeEventArgs : EventArgs
{
	private DataGridViewAutoSizeColumnMode[] previousModes;

	/// <summary>Gets an array of the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> properties.</summary>
	/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values representing the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> properties.</returns>
	public DataGridViewAutoSizeColumnMode[] PreviousModes => previousModes;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs" /> class. </summary>
	/// <param name="previousModes">An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values representing the previous <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property values of each column. </param>
	public DataGridViewAutoSizeColumnsModeEventArgs(DataGridViewAutoSizeColumnMode[] previousModes)
	{
		this.previousModes = previousModes;
	}
}
