namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.SortCommand" /> event of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
public class DataGridSortCommandEventArgs : EventArgs
{
	private string sortExpression;

	private object commandSource;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>The source of the command.</returns>
	public object CommandSource => commandSource;

	/// <summary>Gets the expression used to sort the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The expression used to sort the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	public string SortExpression => sortExpression;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridSortCommandEventArgs" /> class.</summary>
	/// <param name="commandSource">The source of the command. </param>
	/// <param name="dce">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains the event data. </param>
	public DataGridSortCommandEventArgs(object commandSource, DataGridCommandEventArgs dce)
	{
		this.commandSource = commandSource;
		sortExpression = (string)dce.CommandArgument;
	}
}
