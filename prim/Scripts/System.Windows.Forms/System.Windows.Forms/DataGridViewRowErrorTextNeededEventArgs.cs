namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
public class DataGridViewRowErrorTextNeededEventArgs : EventArgs
{
	private int rowIndex;

	private string errorText;

	/// <summary>Gets or sets the error text for the row.</summary>
	/// <returns>A string that represents the error text for the row.</returns>
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

	/// <summary>Gets the row that raised the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event.</summary>
	/// <returns>The zero based row index for the row.</returns>
	public int RowIndex => rowIndex;

	internal DataGridViewRowErrorTextNeededEventArgs(int rowIndex, string errorText)
	{
		this.rowIndex = rowIndex;
		this.errorText = errorText;
	}
}
