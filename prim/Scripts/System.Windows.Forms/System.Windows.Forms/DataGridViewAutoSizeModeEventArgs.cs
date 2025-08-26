namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="T:System.Windows.Forms.DataGridView" /><see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged" /> and <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged" /> events.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewAutoSizeModeEventArgs : EventArgs
{
	private bool previousModeAutoSized;

	/// <summary>Gets a value specifying whether the <see cref="T:System.Windows.Forms.DataGridView" /> was previously set to automatically resize.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" /> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool PreviousModeAutoSized => previousModeAutoSized;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> class.</summary>
	/// <param name="previousModeAutoSized">true if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" /> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />; otherwise, false.</param>
	public DataGridViewAutoSizeModeEventArgs(bool previousModeAutoSized)
	{
		this.previousModeAutoSized = previousModeAutoSized;
	}
}
