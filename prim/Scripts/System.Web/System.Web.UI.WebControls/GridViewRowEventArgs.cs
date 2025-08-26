namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCreated" /> and <see cref="E:System.Web.UI.WebControls.GridView.RowDataBound" /> events.</summary>
public class GridViewRowEventArgs : EventArgs
{
	private GridViewRow _row;

	/// <summary>Gets the row being created or data-bound.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row being created or data-bound.</returns>
	public GridViewRow Row => _row;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs" /> class.</summary>
	/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row being created or data-bound. </param>
	public GridViewRowEventArgs(GridViewRow row)
	{
		_row = row;
	}
}
