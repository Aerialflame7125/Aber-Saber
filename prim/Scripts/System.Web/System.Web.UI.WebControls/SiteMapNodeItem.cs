using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> class is used by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control to functionally represent a <see cref="T:System.Web.SiteMapNode" />.</summary>
[ToolboxItem(false)]
public class SiteMapNodeItem : WebControl, IDataItemContainer, INamingContainer
{
	private int itemIndex;

	private SiteMapNodeItemType itemType;

	private SiteMapNode node;

	/// <summary>Retrieves the index that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to track and manage the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> in its internal collections.</summary>
	/// <returns>An integer that represents the location of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
	public virtual int ItemIndex => itemIndex;

	/// <summary>Retrieves the functional type of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" />.</summary>
	/// <returns>A member of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> enumeration that indicates the functional role of the node item in the navigation path hierarchy.</returns>
	public virtual SiteMapNodeItemType ItemType => itemType;

	/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object that the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> represents.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> object that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to display a site navigation user interface.</returns>
	public virtual SiteMapNode SiteMapNode
	{
		get
		{
			return node;
		}
		set
		{
			node = value;
		}
	}

	/// <summary>Gets an object that is used in simplified data-binding operations.</summary>
	/// <returns>An object that represents the value to use when data-binding operations are performed.</returns>
	object IDataItemContainer.DataItem => node;

	/// <summary>Gets the index of the data item that is bound to the control.</summary>
	/// <returns>An integer that represents the location of the data item in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
	int IDataItemContainer.DataItemIndex => itemIndex;

	/// <summary>Gets the position of the data item as displayed in the control.</summary>
	/// <returns>An integer that represents the location of the data item in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
	int IDataItemContainer.DisplayIndex => itemIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> class, using the specified index and <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" />.</summary>
	/// <param name="itemIndex">The index in the <see cref="P:System.Web.UI.Control.Controls" /> collection that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to track the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> object. </param>
	/// <param name="itemType">The functional type of <see cref="T:System.Web.SiteMapNode" /> that this <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> represents. </param>
	public SiteMapNodeItem(int itemIndex, SiteMapNodeItemType itemType)
	{
		this.itemIndex = itemIndex;
		SetItemType(itemType);
	}

	/// <summary>Sets the current <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /><see cref="P:System.Web.UI.WebControls.SiteMapNodeItem.ItemType" /> property.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> values. </param>
	protected internal virtual void SetItemType(SiteMapNodeItemType itemType)
	{
		this.itemType = itemType;
	}
}
