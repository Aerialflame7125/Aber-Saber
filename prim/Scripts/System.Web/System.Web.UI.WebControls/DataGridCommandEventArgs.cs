namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataGrid.UpdateCommand" /> events of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
public class DataGridCommandEventArgs : CommandEventArgs
{
	private DataGridItem item;

	private object commandSource;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>The source of the command.</returns>
	public object CommandSource => commandSource;

	/// <summary>Gets the item containing the command source in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	public DataGridItem Item => item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> class.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
	/// <param name="commandSource">The source of the command. </param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
	public DataGridCommandEventArgs(DataGridItem item, object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		this.item = item;
		this.commandSource = commandSource;
	}
}
