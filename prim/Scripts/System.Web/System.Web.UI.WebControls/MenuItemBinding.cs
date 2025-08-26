using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls;

/// <summary>Defines the relationship between a data item and the menu item it is binding to in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited. </summary>
[DefaultProperty("TextField")]
public sealed class MenuItemBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
{
	private StateBag ViewState = new StateBag();

	/// <summary>Gets or sets the data member to bind to a menu item.</summary>
	/// <returns>The data member to bind to a menu item. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	public string DataMember
	{
		get
		{
			return ViewState.GetString("DataMember", string.Empty);
		}
		set
		{
			ViewState["DataMember"] = value;
		}
	}

	/// <summary>Gets or sets the menu depth to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The menu depth to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is -1, which indicates that this property is not set.</returns>
	[DefaultValue(-1)]
	public int Depth
	{
		get
		{
			return ViewState.GetInt("Depth", -1);
		}
		set
		{
			ViewState["Depth"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied is enabled, allowing the item to display a pop-out image and any child menu items.</summary>
	/// <returns>
	///     <see langword="true" /> if the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is enabled; otherwise, <see langword="false" />.</returns>
	[DefaultValue(true)]
	public bool Enabled
	{
		get
		{
			return ViewState.GetBool("Enabled", def: true);
		}
		set
		{
			ViewState["Enabled"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Enabled" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied. </summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Enabled" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string EnabledField
	{
		get
		{
			return ViewState.GetString("EnabledField", string.Empty);
		}
		set
		{
			ViewState["EnabledField"] = value;
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>A formatting string that specifies the display format for the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string FormatString
	{
		get
		{
			return ViewState.GetString("FormatString", string.Empty);
		}
		set
		{
			ViewState["FormatString"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image that is displayed next to the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The URL to an image that is displayed next to the text of a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string ImageUrl
	{
		get
		{
			return ViewState.GetString("ImageUrl", string.Empty);
		}
		set
		{
			ViewState["ImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ImageUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ImageUrlField
	{
		get
		{
			return ViewState.GetString("ImageUrlField", string.Empty);
		}
		set
		{
			ViewState["ImageUrlField"] = value;
		}
	}

	/// <summary>Gets or sets the URL to link to when a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied is clicked.</summary>
	/// <returns>The URL to link to when a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string NavigateUrl
	{
		get
		{
			return ViewState.GetString("NavigateUrl", string.Empty);
		}
		set
		{
			ViewState["NavigateUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.NavigateUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.NavigateUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string NavigateUrlField
	{
		get
		{
			return ViewState.GetString("NavigateUrlField", string.Empty);
		}
		set
		{
			ViewState["NavigateUrlField"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied can be selected, or is "clickable."</summary>
	/// <returns>
	///     <see langword="true" /> if the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied is selectable; otherwise, <see langword="false" />.</returns>
	[DefaultValue(true)]
	public bool Selectable
	{
		get
		{
			return ViewState.GetBool("Selectable", def: true);
		}
		set
		{
			ViewState["Selectable"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Selectable" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Selectable" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string SelectableField
	{
		get
		{
			return ViewState.GetString("SelectableField", string.Empty);
		}
		set
		{
			ViewState["SelectableField"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
	[DefaultValue("")]
	public string Target
	{
		get
		{
			return ViewState.GetString("Target", string.Empty);
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Target" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Target" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string TargetField
	{
		get
		{
			return ViewState.GetString("TargetField", string.Empty);
		}
		set
		{
			ViewState["TargetField"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed for the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The text displayed for the menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("The display text of the menu item.")]
	public string Text
	{
		get
		{
			return ViewState.GetString("Text", string.Empty);
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Text" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Text" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string TextField
	{
		get
		{
			return ViewState.GetString("TextField", string.Empty);
		}
		set
		{
			ViewState["TextField"] = value;
		}
	}

	/// <summary>Gets or sets the ToolTip text for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The ToolTip text for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public string ToolTip
	{
		get
		{
			return ViewState.GetString("ToolTip", string.Empty);
		}
		set
		{
			ViewState["ToolTip"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ToolTip" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.ToolTip" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ToolTipField
	{
		get
		{
			return ViewState.GetString("ToolTipField", string.Empty);
		}
		set
		{
			ViewState["ToolTipField"] = value;
		}
	}

	/// <summary>Gets or sets a nondisplayed value used to store any additional data about a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied, such as data used for handling postback events.</summary>
	/// <returns>Supplemental data about a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied; this data is not displayed. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public string Value
	{
		get
		{
			return ViewState.GetString("Value", string.Empty);
		}
		set
		{
			ViewState["Value"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Value" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.Value" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ValueField
	{
		get
		{
			return ViewState.GetString("ValueField", string.Empty);
		}
		set
		{
			ViewState["ValueField"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image that indicates the presence of a dynamic submenu for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The URL to an image that indicates the presence of a dynamic submenu for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string PopOutImageUrl
	{
		get
		{
			return ViewState.GetString("PopOutImageUrl", string.Empty);
		}
		set
		{
			ViewState["PopOutImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.PopOutImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.PopOutImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string PopOutImageUrlField
	{
		get
		{
			return ViewState.GetString("PopOutImageUrlField", string.Empty);
		}
		set
		{
			ViewState["PopOutImageUrlField"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed below the text of a menu item (to separate it from other menu items) for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The URL to an image displayed below the text of a menu item for a menu item to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string SeparatorImageUrl
	{
		get
		{
			return ViewState.GetString("SeparatorImageUrl", string.Empty);
		}
		set
		{
			ViewState["SeparatorImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.SeparatorImageUrl" /> property of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is applied.</summary>
	/// <returns>The name of the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.MenuItem.SeparatorImageUrl" /> of a <see cref="T:System.Web.UI.WebControls.MenuItem" /> to which the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> is applied. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string SeparatorImageUrlField
	{
		get
		{
			return ViewState.GetString("SeparatorImageUrlField", string.Empty);
		}
		set
		{
			ViewState["SeparatorImageUrlField"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => ViewState.IsTrackingViewState;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema" />.</summary>
	[MonoTODO("Not implemented")]
	object IDataSourceViewSchemaAccessor.DataSourceViewSchema
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Returns a string that represents the current object.</summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString()
	{
		string dataMember = DataMember;
		if (string.IsNullOrEmpty(dataMember))
		{
			return "(Empty)";
		}
		return dataMember;
	}

	/// <summary>Loads the node's previously saved view state.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		ViewState.LoadViewState(savedState);
	}

	/// <summary>Saves the view state changes to an <see cref="T:System.Object" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		return ViewState.SaveViewState();
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		ViewState.TrackViewState();
	}

	/// <summary>Creates a copy of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" />.</returns>
	object ICloneable.Clone()
	{
		MenuItemBinding menuItemBinding = new MenuItemBinding();
		foreach (DictionaryEntry item in ViewState)
		{
			menuItemBinding.ViewState[(string)item.Key] = item.Value;
		}
		return menuItemBinding;
	}

	internal void SetDirty()
	{
		StateBag viewState = ViewState;
		foreach (string key in viewState.Keys)
		{
			viewState.SetItemDirty(key, dirty: true);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemBinding" /> class. </summary>
	public MenuItemBinding()
	{
	}
}
