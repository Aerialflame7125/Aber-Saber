namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataList.ItemCreated" /> and <see cref="E:System.Web.UI.WebControls.DataList.ItemDataBound" /> events of a <see cref="T:System.Web.UI.WebControls.DataList" /> control. This class cannot be inherited.</summary>
public class DataListItemEventArgs : EventArgs
{
	private DataListItem item;

	/// <summary>Gets the referenced item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the event is raised.</summary>
	/// <returns>The referenced item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the event is raised.</returns>
	public DataListItem Item => item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> class.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.DataListItem" /> object that represents an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. </param>
	public DataListItemEventArgs(DataListItem item)
	{
		this.item = item;
	}
}
