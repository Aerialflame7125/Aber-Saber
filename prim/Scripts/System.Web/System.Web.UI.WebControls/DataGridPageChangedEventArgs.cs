namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.PageIndexChanged" /> event of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
public class DataGridPageChangedEventArgs : EventArgs
{
	private object commandSource;

	private int newPageIndex;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>The source of the command.</returns>
	public object CommandSource => commandSource;

	/// <summary>Gets the index of the page selected by the user in the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The index of the page selected by the user in the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	public int NewPageIndex => newPageIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridPageChangedEventArgs" /> class.</summary>
	/// <param name="commandSource">The source of the command. </param>
	/// <param name="newPageIndex">The index of the page selected by the user from the page selection element of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
	public DataGridPageChangedEventArgs(object commandSource, int newPageIndex)
	{
		this.commandSource = commandSource;
		this.newPageIndex = newPageIndex;
	}
}
