namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
public class DataGridViewCellErrorTextNeededEventArgs : DataGridViewCellEventArgs
{
	private string errorText;

	/// <summary>Gets or sets the message that is displayed when the cell is selected.</summary>
	/// <returns>The error message.</returns>
	public string ErrorText
	{
		get
		{
			return errorText;
		}
		set
		{
			errorText = value;
		}
	}

	internal DataGridViewCellErrorTextNeededEventArgs(string errorText, int rowIndex, int columnIndex)
		: base(columnIndex, rowIndex)
	{
		this.errorText = errorText;
	}
}
