namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextNeeded" /> event. </summary>
public class DataGridViewCellToolTipTextNeededEventArgs : DataGridViewCellEventArgs
{
	private string toolTipText;

	/// <summary>Gets or sets the ToolTip text.</summary>
	/// <returns>The current ToolTip text.</returns>
	public string ToolTipText
	{
		get
		{
			return toolTipText;
		}
		set
		{
			toolTipText = value;
		}
	}

	internal DataGridViewCellToolTipTextNeededEventArgs(string toolTipText, int rowIndex, int columnIndex)
		: base(columnIndex, rowIndex)
	{
		this.toolTipText = toolTipText;
	}
}
