namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DataList.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataList.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataList.UpdateCommand" /> events of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. This class cannot be inherited.</summary>
public class DataListCommandEventArgs : CommandEventArgs
{
	private DataListItem item;

	private object commandSource;

	/// <summary>Gets the item containing the command source in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> object that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
	public DataListItem Item => item;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>The source of the command.</returns>
	public object CommandSource => commandSource;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> class.</summary>
	/// <param name="item">The selected item from the <see cref="T:System.Web.UI.WebControls.DataList" />. </param>
	/// <param name="commandSource">The source of the command. </param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the original event data. </param>
	public DataListCommandEventArgs(DataListItem item, object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		this.item = item;
		this.commandSource = commandSource;
	}
}
