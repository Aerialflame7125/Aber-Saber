namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.EditingControlShowing" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewEditingControlShowingEventArgs : EventArgs
{
	private Control control;

	private DataGridViewCellStyle cellStyle;

	/// <summary>Gets or sets the cell style of the edited cell.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> representing the style of the cell being edited.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
	public DataGridViewCellStyle CellStyle
	{
		get
		{
			return cellStyle;
		}
		set
		{
			cellStyle = value;
		}
	}

	/// <summary>The control shown to the user for editing the selected cell's value.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that displays an area for the user to enter or change the selected cell's value.</returns>
	/// <filterpriority>1</filterpriority>
	public Control Control => control;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs" /> class.</summary>
	/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> in which the user will edit the selected cell's contents.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> representing the style of the cell being edited.</param>
	public DataGridViewEditingControlShowingEventArgs(Control control, DataGridViewCellStyle cellStyle)
	{
		this.control = control;
		this.cellStyle = cellStyle;
	}
}
