using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace System.Web.UI.WebControls;

/// <summary>Represents a menu item displayed in the <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
[ParseChildren(true, "ChildItems")]
public sealed class MenuItem : IStateManager, ICloneable
{
	private StateBag ViewState = new StateBag();

	private MenuItemCollection items;

	private bool marked;

	private Menu menu;

	private MenuItem parent;

	private int index;

	private string path;

	private int depth = -1;

	private object dataItem;

	private IHierarchyData hierarchyData;

	private bool gotBinding;

	private MenuItemBinding binding;

	private PropertyDescriptorCollection boundProperties;

	/// <summary>Gets the level at which a menu item is displayed.</summary>
	/// <returns>The level at which a menu item is displayed.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int Depth
	{
		get
		{
			if (depth != -1)
			{
				return depth;
			}
			if (Parent == null)
			{
				depth = 0;
			}
			else
			{
				depth = Parent.Depth + 1;
			}
			return depth;
		}
	}

	internal Menu Menu
	{
		get
		{
			return menu;
		}
		set
		{
			menu = value;
			if (items != null)
			{
				items.SetMenu(menu);
			}
			ResetPathData();
		}
	}

	/// <summary>Gets a value indicating whether the menu item was created through data binding.</summary>
	/// <returns>
	///     <see langword="true" /> if the menu item was created through data binding; otherwise, <see langword="false" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue(false)]
	[Browsable(false)]
	public bool DataBound
	{
		get
		{
			if (ViewState["DataBound"] != null)
			{
				return (bool)ViewState["DataBound"];
			}
			return false;
		}
		private set
		{
			ViewState["DataBound"] = value;
		}
	}

	/// <summary>Gets the data item that is bound to the menu item.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the data item that is bound to the menu item. The default value is <see langword="null" />, which indicates that the menu item is not bound to any data item.</returns>
	[DefaultValue(null)]
	[Browsable(false)]
	public object DataItem
	{
		get
		{
			if (!DataBound)
			{
				throw new InvalidOperationException("MenuItem is not data bound.");
			}
			return dataItem;
		}
	}

	/// <summary>Gets the path to the data that is bound to the menu item.</summary>
	/// <returns>The path to the data that is bound to the node. This value comes from the hierarchical data source control to which the <see cref="T:System.Web.UI.WebControls.Menu" /> control is bound. The default value is an empty string ("").</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	[Browsable(false)]
	public string DataPath
	{
		get
		{
			if (ViewState["DataPath"] != null)
			{
				return (string)ViewState["DataPath"];
			}
			return string.Empty;
		}
		private set
		{
			ViewState["DataPath"] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object that contains the submenu items of the current menu item.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> that contains the submenu items of the current menu item. The default is <see langword="null" />, which indicates that this menu item does not contain any submenu items.</returns>
	[MergableProperty(false)]
	[Browsable(false)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	public MenuItemCollection ChildItems
	{
		get
		{
			if (items == null)
			{
				items = new MenuItemCollection(this);
				if (((IStateManager)this).IsTrackingViewState)
				{
					((IStateManager)items).TrackViewState();
				}
			}
			return items;
		}
	}

	/// <summary>Gets or sets the URL to an image that is displayed next to the text in a menu item.</summary>
	/// <returns>The URL to a custom image that is displayed next to the text of a menu item. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string ImageUrl
	{
		get
		{
			if (ViewState["ImageUrl"] != null)
			{
				return (string)ViewState["ImageUrl"];
			}
			return string.Empty;
		}
		set
		{
			ViewState["ImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the URL to navigate to when the menu item is clicked.</summary>
	/// <returns>The URL to navigate to when the menu item is clicked. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string NavigateUrl
	{
		get
		{
			if (ViewState["NavigateUrl"] != null)
			{
				return (string)ViewState["NavigateUrl"];
			}
			return string.Empty;
		}
		set
		{
			ViewState["NavigateUrl"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image that is displayed in a menu item to indicate that the menu item has a dynamic submenu.</summary>
	/// <returns>The URL to an image that is displayed in a menu item to indicate that the menu item has a dynamic submenu. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string PopOutImageUrl
	{
		get
		{
			if (ViewState["PopOutImageUrl"] != null)
			{
				return (string)ViewState["PopOutImageUrl"];
			}
			return string.Empty;
		}
		set
		{
			ViewState["PopOutImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content associated with a menu item.</summary>
	/// <returns>The target window or frame in which to display the linked Web page content. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
	[DefaultValue("")]
	public string Target
	{
		get
		{
			if (ViewState["Target"] != null)
			{
				return (string)ViewState["Target"];
			}
			return string.Empty;
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed for the menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>The text displayed for the menu item in the <see cref="T:System.Web.UI.WebControls.Menu" /> control. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string Text
	{
		get
		{
			object obj = ViewState["Text"];
			if (obj == null)
			{
				obj = ViewState["Value"];
			}
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets the ToolTip text for the menu item.</summary>
	/// <returns>The ToolTip text for the menu item. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string ToolTip
	{
		get
		{
			if (ViewState["ToolTip"] != null)
			{
				return (string)ViewState["ToolTip"];
			}
			return string.Empty;
		}
		set
		{
			ViewState["ToolTip"] = value;
		}
	}

	/// <summary>Gets or sets a non-displayed value used to store any additional data about the menu item, such as data used for handling postback events.</summary>
	/// <returns>Supplemental data about the menu item that is not displayed. The default value is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public string Value
	{
		get
		{
			object obj = ViewState["Value"];
			if (obj == null)
			{
				obj = ViewState["Text"];
			}
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["Value"] = value;
		}
	}

	/// <summary>Gets or sets the URL to an image displayed at the bottom of a menu item to separate it from other menu items.</summary>
	/// <returns>The URL to an image used to separate the current menu item from other menu items.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string SeparatorImageUrl
	{
		get
		{
			if (ViewState["SeparatorImageUrl"] != null)
			{
				return (string)ViewState["SeparatorImageUrl"];
			}
			return string.Empty;
		}
		set
		{
			ViewState["SeparatorImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object can be selected, or is "clickable."</summary>
	/// <returns>
	///     <see langword="true" /> if the menu item can be selected; otherwise, <see langword="false" />.</returns>
	[Browsable(true)]
	[DefaultValue(true)]
	public bool Selectable
	{
		get
		{
			if (ViewState["Selectable"] != null)
			{
				return (bool)ViewState["Selectable"];
			}
			return true;
		}
		set
		{
			ViewState["Selectable"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is enabled, allowing the item to display a pop-out image and any child menu items.</summary>
	/// <returns>
	///     <see langword="true" /> if the menu item is enabled; otherwise, <see langword="false" />.</returns>
	[Browsable(true)]
	[DefaultValue(true)]
	public bool Enabled
	{
		get
		{
			if (ViewState["Enabled"] != null)
			{
				return (bool)ViewState["Enabled"];
			}
			return true;
		}
		set
		{
			ViewState["Enabled"] = value;
		}
	}

	internal bool BranchEnabled
	{
		get
		{
			if (Enabled)
			{
				if (parent != null)
				{
					return parent.BranchEnabled;
				}
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets or sets a value indicating whether the current menu item is selected in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the current menu item is selected in a <see cref="T:System.Web.UI.WebControls.Menu" /> control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Browsable(true)]
	public bool Selected
	{
		get
		{
			if (menu != null)
			{
				return menu.SelectedItem == this;
			}
			return false;
		}
		set
		{
			if (menu != null)
			{
				if (!value && menu.SelectedItem == this)
				{
					menu.SetSelectedItem(null);
				}
				else if (value)
				{
					menu.SetSelectedItem(this);
				}
			}
		}
	}

	/// <summary>Gets the parent menu item of the current menu item.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the parent menu item of the current menu item.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public MenuItem Parent => parent;

	/// <summary>Gets the path from the root menu item to the current menu item.</summary>
	/// <returns>A delimiter-separated list of menu item values that form a path from the root menu item to the current menu item.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string ValuePath
	{
		get
		{
			if (menu == null)
			{
				return Value;
			}
			StringBuilder stringBuilder = new StringBuilder(Value);
			for (MenuItem menuItem = parent; menuItem != null; menuItem = menuItem.Parent)
			{
				stringBuilder.Insert(0, menu.PathSeparator);
				stringBuilder.Insert(0, menuItem.Value);
			}
			return stringBuilder.ToString();
		}
	}

	internal int Index
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
			ResetPathData();
		}
	}

	internal string Path
	{
		get
		{
			if (path != null)
			{
				return path;
			}
			StringBuilder stringBuilder = new StringBuilder(index.ToString());
			for (MenuItem menuItem = parent; menuItem != null; menuItem = menuItem.Parent)
			{
				stringBuilder.Insert(0, '_');
				stringBuilder.Insert(0, menuItem.Index.ToString());
			}
			path = stringBuilder.ToString();
			return path;
		}
	}

	internal bool HasChildData => items != null;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => marked;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class without menu text or a value.</summary>
	public MenuItem()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text. </summary>
	/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
	public MenuItem(string text)
	{
		Text = text;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text and value. </summary>
	/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
	/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
	public MenuItem(string text, string value)
	{
		Text = text;
		Value = value;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, and URL to an image. </summary>
	/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
	/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
	/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item.</param>
	public MenuItem(string text, string value, string imageUrl)
	{
		Text = text;
		Value = value;
		ImageUrl = imageUrl;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, image URL, and navigation URL. </summary>
	/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control.</param>
	/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events.</param>
	/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item.</param>
	/// <param name="navigateUrl">The URL to link to when the menu item is clicked.</param>
	public MenuItem(string text, string value, string imageUrl, string navigateUrl)
	{
		Text = text;
		Value = value;
		ImageUrl = imageUrl;
		NavigateUrl = navigateUrl;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> class using the specified menu text, value, image URL, navigation URL, and target. </summary>
	/// <param name="text">The text displayed for a menu item in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. </param>
	/// <param name="value">The supplemental data associated with the menu item, such as data used for handling postback events. </param>
	/// <param name="imageUrl">The URL to an image displayed next to the text in a menu item. </param>
	/// <param name="navigateUrl">The URL to link to when the menu item is clicked. </param>
	/// <param name="target">The target window or frame in which to display the Web page content linked to a menu item when the menu item is clicked. </param>
	public MenuItem(string text, string value, string imageUrl, string navigateUrl, string target)
	{
		Text = text;
		Value = value;
		ImageUrl = imageUrl;
		NavigateUrl = navigateUrl;
		Target = target;
	}

	private void ResetPathData()
	{
		path = null;
		depth = -1;
		gotBinding = false;
	}

	internal void SetParent(MenuItem item)
	{
		parent = item;
		ResetPathData();
	}

	/// <summary>Loads the menu item's previously saved view state.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			ViewState.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)ChildItems).LoadViewState(array[1]);
			}
		}
	}

	/// <summary>Saves the view-state changes to an <see cref="T:System.Object" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view-state changes.</returns>
	object IStateManager.SaveViewState()
	{
		object[] array = new object[2]
		{
			ViewState.SaveViewState(),
			(items == null) ? null : ((IStateManager)items).SaveViewState()
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		if (!marked)
		{
			marked = true;
			ViewState.TrackViewState();
			if (items != null)
			{
				((IStateManager)items).TrackViewState();
			}
		}
	}

	internal void SetDirty()
	{
		ViewState.SetDirty(dirty: true);
		if (items != null)
		{
			items.SetDirty();
		}
	}

	/// <summary>Creates a copy of the current <see cref="T:System.Web.UI.WebControls.MenuItem" /> object. </summary>
	/// <returns>An <see cref="T:System.Object" /> that represents a copy of the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</returns>
	object ICloneable.Clone()
	{
		MenuItem menuItem = new MenuItem();
		foreach (DictionaryEntry item in ViewState)
		{
			menuItem.ViewState[(string)item.Key] = item.Value;
		}
		foreach (ICloneable childItem in ChildItems)
		{
			menuItem.ChildItems.Add((MenuItem)childItem.Clone());
		}
		return menuItem;
	}

	internal void Bind(IHierarchyData hierarchyData)
	{
		this.hierarchyData = hierarchyData;
		DataBound = true;
		DataPath = hierarchyData.Path;
		dataItem = hierarchyData.Item;
		MenuItemBinding menuItemBinding = GetBinding();
		if (menuItemBinding != null)
		{
			if (menuItemBinding.EnabledField != "")
			{
				try
				{
					Enabled = Convert.ToBoolean(GetBoundPropertyValue(menuItemBinding.EnabledField));
				}
				catch
				{
					Enabled = menuItemBinding.Enabled;
				}
			}
			else
			{
				Enabled = menuItemBinding.Enabled;
			}
			if (menuItemBinding.ImageUrlField.Length > 0)
			{
				ImageUrl = Convert.ToString(GetBoundPropertyValue(menuItemBinding.ImageUrlField));
				if (ImageUrl.Length == 0)
				{
					ImageUrl = menuItemBinding.ImageUrl;
				}
			}
			else if (menuItemBinding.ImageUrl.Length > 0)
			{
				ImageUrl = menuItemBinding.ImageUrl;
			}
			if (menuItemBinding.NavigateUrlField.Length > 0)
			{
				NavigateUrl = Convert.ToString(GetBoundPropertyValue(menuItemBinding.NavigateUrlField));
				if (NavigateUrl.Length == 0)
				{
					NavigateUrl = menuItemBinding.NavigateUrl;
				}
			}
			else if (menuItemBinding.NavigateUrl.Length > 0)
			{
				NavigateUrl = menuItemBinding.NavigateUrl;
			}
			if (menuItemBinding.PopOutImageUrlField.Length > 0)
			{
				PopOutImageUrl = Convert.ToString(GetBoundPropertyValue(menuItemBinding.PopOutImageUrlField));
				if (PopOutImageUrl.Length == 0)
				{
					PopOutImageUrl = menuItemBinding.PopOutImageUrl;
				}
			}
			else if (menuItemBinding.PopOutImageUrl.Length > 0)
			{
				PopOutImageUrl = menuItemBinding.PopOutImageUrl;
			}
			if (menuItemBinding.SelectableField != "")
			{
				try
				{
					Selectable = Convert.ToBoolean(GetBoundPropertyValue(menuItemBinding.SelectableField));
				}
				catch
				{
					Selectable = menuItemBinding.Selectable;
				}
			}
			else
			{
				Selectable = menuItemBinding.Selectable;
			}
			if (menuItemBinding.SeparatorImageUrlField.Length > 0)
			{
				SeparatorImageUrl = Convert.ToString(GetBoundPropertyValue(menuItemBinding.SeparatorImageUrlField));
				if (SeparatorImageUrl.Length == 0)
				{
					SeparatorImageUrl = menuItemBinding.SeparatorImageUrl;
				}
			}
			else if (menuItemBinding.SeparatorImageUrl.Length > 0)
			{
				SeparatorImageUrl = menuItemBinding.SeparatorImageUrl;
			}
			if (menuItemBinding.TargetField.Length > 0)
			{
				Target = Convert.ToString(GetBoundPropertyValue(menuItemBinding.TargetField));
				if (Target.Length == 0)
				{
					Target = menuItemBinding.Target;
				}
			}
			else if (menuItemBinding.Target.Length > 0)
			{
				Target = menuItemBinding.Target;
			}
			if (menuItemBinding.ToolTipField.Length > 0)
			{
				ToolTip = Convert.ToString(GetBoundPropertyValue(menuItemBinding.ToolTipField));
				if (ToolTip.Length == 0)
				{
					ToolTip = menuItemBinding.ToolTip;
				}
			}
			else if (menuItemBinding.ToolTip.Length > 0)
			{
				ToolTip = menuItemBinding.ToolTip;
			}
			string value = null;
			if (menuItemBinding.ValueField.Length > 0)
			{
				value = Convert.ToString(GetBoundPropertyValue(menuItemBinding.ValueField));
			}
			if (string.IsNullOrEmpty(value))
			{
				value = ((menuItemBinding.Value.Length > 0) ? menuItemBinding.Value : ((menuItemBinding.Text.Length <= 0) ? string.Empty : menuItemBinding.Text));
			}
			Value = value;
			string text = null;
			if (menuItemBinding.TextField.Length > 0)
			{
				text = Convert.ToString(GetBoundPropertyValue(menuItemBinding.TextField));
				if (menuItemBinding.FormatString.Length > 0)
				{
					text = string.Format(menuItemBinding.FormatString, text);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = ((menuItemBinding.Text.Length > 0) ? menuItemBinding.Text : ((menuItemBinding.Value.Length <= 0) ? string.Empty : menuItemBinding.Value));
			}
			Text = text;
		}
		else
		{
			string text2 = (Value = GetDefaultBoundText());
			Text = text2;
		}
		if (hierarchyData is INavigateUIData navigateUIData)
		{
			ToolTip = navigateUIData.Description;
			Text = navigateUIData.ToString();
			NavigateUrl = navigateUIData.NavigateUrl;
		}
	}

	internal void SetDataItem(object item)
	{
		dataItem = item;
	}

	internal void SetDataPath(string path)
	{
		DataPath = path;
	}

	internal void SetDataBound(bool bound)
	{
		DataBound = bound;
	}

	private string GetDefaultBoundText()
	{
		if (hierarchyData != null)
		{
			return hierarchyData.ToString();
		}
		if (dataItem != null)
		{
			return dataItem.ToString();
		}
		return string.Empty;
	}

	private string GetDataItemType()
	{
		if (hierarchyData != null)
		{
			return hierarchyData.Type;
		}
		if (dataItem != null)
		{
			return dataItem.GetType().ToString();
		}
		return string.Empty;
	}

	private MenuItemBinding GetBinding()
	{
		if (menu == null)
		{
			return null;
		}
		if (gotBinding)
		{
			return binding;
		}
		binding = menu.FindBindingForItem(GetDataItemType(), Depth);
		gotBinding = true;
		return binding;
	}

	private object GetBoundPropertyValue(string name)
	{
		if (boundProperties == null)
		{
			if (hierarchyData != null)
			{
				boundProperties = TypeDescriptor.GetProperties(hierarchyData);
			}
			else
			{
				boundProperties = TypeDescriptor.GetProperties(dataItem);
			}
		}
		PropertyDescriptor propertyDescriptor = boundProperties.Find(name, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
		}
		if (hierarchyData != null)
		{
			return propertyDescriptor.GetValue(hierarchyData);
		}
		return propertyDescriptor.GetValue(dataItem);
	}
}
