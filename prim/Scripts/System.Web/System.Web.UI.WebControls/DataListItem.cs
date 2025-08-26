using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents an item in a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
[ToolboxItem("")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DataListItem : WebControl, INamingContainer, IDataItemContainer
{
	private int index;

	private ListItemType type;

	private object item;

	/// <summary>Gets or sets a data item associated with the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
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

	/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection of the control.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection.</returns>
	public virtual int ItemIndex => index;

	/// <summary>Gets the type of the item represented by the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
	public virtual ListItemType ItemType => type;

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the value to use when data-binding operations are performed.</returns>
	object IDataItemContainer.DataItem => item;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
	/// <returns>An integer representing the index of the data item bound to a control.</returns>
	int IDataItemContainer.DataItemIndex => index;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
	/// <returns>An integer representing the position of the data item as displayed in a control.</returns>
	int IDataItemContainer.DisplayIndex => index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataListItem" /> class.</summary>
	/// <param name="itemIndex">The index of the item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control from the <see cref="P:System.Web.UI.WebControls.DataList.Items" /> collection. </param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public DataListItem(int itemIndex, ListItemType itemType)
	{
		index = itemIndex;
		type = itemType;
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object for the <see cref="T:System.Web.UI.WebControls.DataListItem" /> control, which contains the style properties for the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties of the control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableItemStyle(ViewState);
	}

	/// <summary>Determines whether the event for the control is passed up the server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is CommandEventArgs originalArgs)
		{
			RaiseBubbleEvent(this, new DataListCommandEventArgs(this, source, originalArgs));
			return true;
		}
		return false;
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.DataListItem" /> object on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that contains the output stream for rendering on the client. </param>
	/// <param name="extractRows">
	///       <see langword="true" /> to extract rows; otherwise <see langword="false" />. </param>
	/// <param name="tableLayout">
	///       <see langword="true" /> to display as a table; otherwise <see langword="false" />. </param>
	public virtual void RenderItem(HtmlTextWriter writer, bool extractRows, bool tableLayout)
	{
		bool flag = !extractRows && !tableLayout;
		if (flag)
		{
			writer.RenderBeginTag(TagKey);
		}
		if (HasControls())
		{
			if (extractRows)
			{
				bool flag2 = false;
				foreach (Control control in Controls)
				{
					if (!(control is Table table))
					{
						continue;
					}
					flag2 = true;
					foreach (TableRow row in table.Rows)
					{
						if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
						{
							row.ControlStyle.MergeWith(base.ControlStyle);
						}
						row.RenderControl(writer);
					}
					break;
				}
				if (!flag2)
				{
					throw new HttpException("No Table found in DataList template.");
				}
			}
			else
			{
				RenderContents(writer);
			}
		}
		if (flag)
		{
			writer.RenderEndTag();
		}
	}

	/// <summary>Sets the current <see cref="P:System.Web.UI.WebControls.DataListItem.ItemType" /> property for the <see cref="T:System.Web.UI.WebControls.DataListItem" /> control.</summary>
	/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" />  value.</param>
	protected virtual void SetItemType(ListItemType itemType)
	{
		type = itemType;
	}
}
