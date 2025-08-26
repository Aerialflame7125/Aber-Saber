namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.DataGrid.ItemDataBound" /> events of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. This class cannot be inherited.</summary>
public class DataGridItemEventArgs : EventArgs
{
	private DataGridItem item;

	/// <summary>Gets the referenced item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when the event is raised.</summary>
	/// <returns>The referenced item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when the event is raised.</returns>
	public DataGridItem Item => item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> class.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that represents an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
	public DataGridItemEventArgs(DataGridItem item)
	{
		this.item = item;
	}
}
