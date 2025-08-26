namespace System.Web.UI.WebControls;

/// <summary>Represents a container that holds the contents of a templated menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
public sealed class MenuItemTemplateContainer : Control, IDataItemContainer, INamingContainer
{
	private object dataItem;

	private int index;

	/// <summary>Gets or sets the menu item associated with the container.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item associated with the container.</returns>
	public object DataItem
	{
		get
		{
			return dataItem;
		}
		set
		{
			dataItem = value;
		}
	}

	/// <summary>Gets the index of the menu item associated with the container.</summary>
	/// <returns>The index of the menu item associated with the container.</returns>
	public int ItemIndex => index;

	/// <summary>Gets the index value of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</summary>
	/// <returns>The index value of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</returns>
	int IDataItemContainer.DataItemIndex => index;

	/// <summary>Gets the index value of the menu item for the container.</summary>
	/// <returns>The index value of the menu item for the container.</returns>
	int IDataItemContainer.DisplayIndex => index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemTemplateContainer" /> class using the specified menu item index and menu item.</summary>
	/// <param name="itemIndex">The index of the menu item.</param>
	/// <param name="dataItem">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> object associated with the container.</param>
	public MenuItemTemplateContainer(int itemIndex, MenuItem dataItem)
	{
		index = itemIndex;
		this.dataItem = dataItem;
	}

	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (!(e is CommandEventArgs originalArgs))
		{
			return false;
		}
		MenuEventArgs args = new MenuEventArgs((MenuItem)DataItem, source, originalArgs);
		RaiseBubbleEvent(this, args);
		return true;
	}

	protected internal override void Render(HtmlTextWriter writer)
	{
		base.Render(writer);
	}
}
