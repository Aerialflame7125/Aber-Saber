using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents an item (row) in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DataGridItem : TableRow, INamingContainer, IDataItemContainer
{
	private object item;

	private int dataset_index;

	private int item_index;

	private ListItemType item_type;

	/// <summary>Gets or sets the data item represented by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	public virtual object DataItem
	{
		get
		{
			return item;
		}
		set
		{
			item = value;
		}
	}

	/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object from the bound data source.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the bound data source.</returns>
	public virtual int DataSetIndex => dataset_index;

	/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	public virtual int ItemIndex => item_index;

	/// <summary>Gets the type of the item represented by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
	public virtual ListItemType ItemType => item_type;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
	/// <returns>An object that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	object IDataItemContainer.DataItem => item;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the bound data source.</returns>
	int IDataItemContainer.DataItemIndex => item_index;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	int IDataItemContainer.DisplayIndex => item_index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
	/// <param name="itemIndex">The index of the item from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
	/// <param name="dataSetIndex">The index number of the item, from the bound data source, that appears in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public DataGridItem(int itemIndex, int dataSetIndex, ListItemType itemType)
	{
		item_index = itemIndex;
		dataset_index = dataSetIndex;
		item_type = itemType;
	}

	/// <summary>Used internally by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is CommandEventArgs)
		{
			RaiseBubbleEvent(this, new DataGridCommandEventArgs(this, source, (CommandEventArgs)e));
			return true;
		}
		return base.OnBubbleEvent(source, e);
	}

	/// <summary>Used internally by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</param>
	protected internal virtual void SetItemType(ListItemType itemType)
	{
		item_type = itemType;
	}
}
