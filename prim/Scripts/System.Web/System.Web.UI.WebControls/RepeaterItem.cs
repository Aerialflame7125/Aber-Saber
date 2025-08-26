using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents an item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
[ToolboxItem("")]
public class RepeaterItem : Control, INamingContainer, IDataItemContainer
{
	private object data_item;

	private int idx;

	private ListItemType type;

	/// <summary>Gets or sets a data item associated with the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	/// <returns>A <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</returns>
	public virtual object DataItem
	{
		get
		{
			return data_item;
		}
		set
		{
			data_item = value;
		}
	}

	/// <summary>Gets the index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control.</summary>
	/// <returns>The index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control.</returns>
	public virtual int ItemIndex => idx;

	/// <summary>Gets the type of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
	public virtual ListItemType ItemType => type;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
	/// <returns>An <see cref="P:System.Web.UI.WebControls.RepeaterItem.ItemIndex" /> property.</returns>
	int IDataItemContainer.DataItemIndex => ItemIndex;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
	/// <returns>An <see cref="P:System.Web.UI.WebControls.RepeaterItem.ItemIndex" /> property.</returns>
	int IDataItemContainer.DisplayIndex => ItemIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> class.</summary>
	/// <param name="itemIndex">The index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public RepeaterItem(int itemIndex, ListItemType itemType)
	{
		idx = itemIndex;
		type = itemType;
	}

	/// <summary>Assigns any sources of the event and its information to the parent <see cref="T:System.Web.UI.WebControls.Repeater" /> control, if the <see cref="T:System.EventArgs" /> parameter is an instance of <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" />.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <returns>
	///     <see langword="true" /> if the event assigned to the parent was raised, otherwise <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is CommandEventArgs originalArgs)
		{
			RaiseBubbleEvent(this, new RepeaterCommandEventArgs(this, source, originalArgs));
			return true;
		}
		return false;
	}
}
