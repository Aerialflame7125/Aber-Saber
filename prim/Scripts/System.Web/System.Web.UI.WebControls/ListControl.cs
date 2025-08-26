using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the abstract base class that defines the properties, methods, and events common for all list-type controls.</summary>
[DataBindingHandler("System.Web.UI.Design.WebControls.ListControlDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultEvent("SelectedIndexChanged")]
[Designer("System.Web.UI.Design.WebControls.ListControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(true, "Items")]
[ControlValueProperty("SelectedValue", null)]
public abstract class ListControl : DataBoundControl, IEditableTextControl, ITextControl
{
	private static readonly object SelectedIndexChangedEvent;

	private static readonly object TextChangedEvent;

	private ListItemCollection items;

	private int _selectedIndex = -2;

	private string _selectedValue;

	/// <summary>Gets or sets a value that indicates whether list items are cleared before data binding.</summary>
	/// <returns>
	///     <see langword="true" /> if list items are not cleared before data binding; otherwise, <see langword="false" />, if the items collection is cleared before data binding is performed. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AppendDataBoundItems
	{
		get
		{
			return ViewState.GetBool("AppendDataBoundItems", def: false);
		}
		set
		{
			ViewState["AppendDataBoundItems"] = value;
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether a postback to the server automatically occurs when the user changes the list selection.</summary>
	/// <returns>
	///     <see langword="true" /> if a postback to the server automatically occurs whenever the user changes the selection of the list; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Themeable(false)]
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AutoPostBack
	{
		get
		{
			return ViewState.GetBool("AutoPostBack", def: false);
		}
		set
		{
			ViewState["AutoPostBack"] = value;
		}
	}

	/// <summary>Gets or sets the field of the data source that provides the text content of the list items.</summary>
	/// <returns>The field of the data source that provides the text content of the list items. The default is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataTextField
	{
		get
		{
			return ViewState.GetString("DataTextField", string.Empty);
		}
		set
		{
			ViewState["DataTextField"] = value;
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}
	}

	/// <summary>Gets or sets the formatting string used to control how data bound to the list control is displayed.</summary>
	/// <returns>The formatting string for data bound to the control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataTextFormatString
	{
		get
		{
			return ViewState.GetString("DataTextFormatString", string.Empty);
		}
		set
		{
			ViewState["DataTextFormatString"] = value;
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}
	}

	/// <summary>Gets or sets the field of the data source that provides the value of each list item.</summary>
	/// <returns>The field of the data source that provides the value of each list item. The default is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataValueField
	{
		get
		{
			return ViewState.GetString("DataValueField", string.Empty);
		}
		set
		{
			ViewState["DataValueField"] = value;
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}
	}

	/// <summary>Gets the collection of items in the list control.</summary>
	/// <returns>The items within the list. The default is an empty list.</returns>
	[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual ListItemCollection Items
	{
		get
		{
			if (items == null)
			{
				items = new ListItemCollection();
				if (base.IsTrackingViewState)
				{
					((IStateManager)items).TrackViewState();
				}
			}
			return items;
		}
	}

	/// <summary>Gets or sets the lowest ordinal index of the selected items in the list.</summary>
	/// <returns>The lowest ordinal index of the selected items in the list. The default is -1, which indicates that nothing is selected.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered. </exception>
	[Bindable(true)]
	[Browsable(false)]
	[DefaultValue(0)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual int SelectedIndex
	{
		get
		{
			if (items == null)
			{
				return -1;
			}
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i].Selected)
				{
					return i;
				}
			}
			return -1;
		}
		set
		{
			_selectedIndex = value;
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (value < Items.Count)
			{
				ClearSelection();
				if (value != -1)
				{
					items[value].Selected = true;
				}
			}
		}
	}

	/// <summary>Gets the selected item with the lowest index in the list control.</summary>
	/// <returns>The lowest indexed item selected from the list control. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual ListItem SelectedItem
	{
		get
		{
			int selectedIndex = SelectedIndex;
			if (selectedIndex == -1)
			{
				return null;
			}
			return Items[selectedIndex];
		}
	}

	/// <summary>Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.</summary>
	/// <returns>The value of the selected item in the list control. The default is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not in the list of available values and view state or other state has been loaded (a postback has been performed). For more information, see the Remarks section.</exception>
	[Bindable(true, BindingDirection.TwoWay)]
	[Themeable(false)]
	[Browsable(false)]
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual string SelectedValue
	{
		get
		{
			int selectedIndex = SelectedIndex;
			if (selectedIndex == -1)
			{
				return string.Empty;
			}
			return Items[selectedIndex].Value;
		}
		set
		{
			_selectedValue = value;
			SetSelectedValue(value);
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> property of the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> of the <see cref="T:System.Web.UI.WebControls.ListControl" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string Text
	{
		get
		{
			return SelectedValue;
		}
		set
		{
			SelectedValue = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.ListControl" /> control. </summary>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Select;

	/// <summary>Gets or sets a value indicating whether validation is performed when a control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.WebControls.ListControl" /> control is clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Themeable(false)]
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool CausesValidation
	{
		get
		{
			return ViewState.GetBool("CausesValidation", def: false);
		}
		set
		{
			ViewState["CausesValidation"] = value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class causes validation when it posts back to the server. </summary>
	/// <returns>The group of controls for which the derived <see cref="T:System.Web.UI.WebControls.ListControl" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string ValidationGroup
	{
		get
		{
			return ViewState.GetString("ValidationGroup", "");
		}
		set
		{
			ViewState["ValidationGroup"] = value;
		}
	}

	/// <summary>Occurs when the selection from the list control changes between posts to the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler SelectedIndexChanged
	{
		add
		{
			base.Events.AddHandler(SelectedIndexChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectedIndexChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Web.UI.WebControls.ListControl.Text" /> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> properties change.</summary>
	public event EventHandler TextChanged
	{
		add
		{
			base.Events.AddHandler(TextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListControl" /> class.</summary>
	public ListControl()
		: base(HtmlTextWriterTag.Select)
	{
	}

	private bool SetSelectedValue(string value)
	{
		if (items != null && items.Count > 0)
		{
			int count = items.Count;
			ListItemCollection listItemCollection = Items;
			for (int i = 0; i < count; i++)
			{
				if (listItemCollection[i].Value == value)
				{
					ClearSelection();
					listItemCollection[i].Selected = true;
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>Applies HTML attributes and styles to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
	}

	/// <summary>Clears out the list selection and sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of all items to false.</summary>
	public virtual void ClearSelection()
	{
		if (items != null)
		{
			int count = Items.Count;
			for (int i = 0; i < count; i++)
			{
				items[i].Selected = false;
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected override void OnDataBinding(EventArgs e)
	{
		base.OnDataBinding(e);
		IEnumerable data = GetData().ExecuteSelect(DataSourceSelectArguments.Empty);
		InternalPerformDataBinding(data);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null && base.IsEnabled)
		{
			page.RegisterEnabledControl(this);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ListControl.TextChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnTextChanged(EventArgs e)
	{
		((EventHandler)base.Events[TextChanged])?.Invoke(this, e);
	}

	/// <summary>Binds the specified data source to the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class.</summary>
	/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The cached value of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex" /> is out of range.</exception>
	/// <exception cref="T:System.ArgumentException">The cached values of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex" /> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> do not match.</exception>
	protected internal override void PerformDataBinding(IEnumerable dataSource)
	{
		if (dataSource != null)
		{
			if (!AppendDataBoundItems)
			{
				Items.Clear();
			}
			string text = DataTextFormatString;
			if (text.Length == 0)
			{
				text = null;
			}
			string text2 = DataTextField;
			string text3 = DataValueField;
			if (text2.Length == 0)
			{
				text2 = null;
			}
			if (text3.Length == 0)
			{
				text3 = null;
			}
			ListItemCollection listItemCollection = Items;
			foreach (object item in dataSource)
			{
				string text4;
				string text5 = (text4 = null);
				if (text2 != null)
				{
					text5 = DataBinder.GetPropertyValue(item, text2, text);
				}
				if (text3 != null)
				{
					text4 = DataBinder.GetPropertyValue(item, text3).ToString();
				}
				else if (text2 == null)
				{
					text5 = (text4 = item.ToString());
					if (text != null)
					{
						text5 = string.Format(text, item);
					}
				}
				else if (text5 != null)
				{
					text4 = text5;
				}
				if (text5 == null)
				{
					text5 = text4;
				}
				listItemCollection.Add(new ListItem(text5, text4));
			}
		}
		if (!string.IsNullOrEmpty(_selectedValue))
		{
			if (!SetSelectedValue(_selectedValue))
			{
				throw new ArgumentOutOfRangeException("value", $"'{ID}' has a SelectedValue which is invalid because it does not exist in the list of items.");
			}
			if (_selectedIndex >= 0 && _selectedIndex != SelectedIndex)
			{
				throw new ArgumentException("SelectedIndex and SelectedValue are mutually exclusive.");
			}
		}
		else if (_selectedIndex >= 0)
		{
			SelectedIndex = _selectedIndex;
		}
	}

	/// <summary>Retrieves data from the associated data source.</summary>
	[MonoTODO("why override?")]
	protected override void PerformSelect()
	{
		OnDataBinding(EventArgs.Empty);
		base.RequiresDataBinding = false;
		MarkAsDataBound();
		OnDataBound(EventArgs.Empty);
	}

	/// <summary>Renders the items in the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		bool flag = false;
		Page page = Page;
		for (int i = 0; i < Items.Count; i++)
		{
			ListItem listItem = Items[i];
			page?.ClientScript.RegisterForEventValidation(UniqueID, listItem.Value);
			writer.WriteBeginTag("option");
			if (listItem.Selected)
			{
				if (flag)
				{
					VerifyMultiSelect();
				}
				writer.WriteAttribute("selected", "selected", fEncode: false);
				flag = true;
			}
			writer.WriteAttribute("value", listItem.Value, fEncode: true);
			if (listItem.HasAttributes)
			{
				listItem.Attributes.Render(writer);
			}
			writer.Write(">");
			string value = HttpUtility.HtmlEncode(listItem.Text);
			writer.Write(value);
			writer.WriteEndTag("option");
			writer.WriteLine();
		}
	}

	internal ArrayList GetSelectedIndicesInternal()
	{
		ArrayList arrayList = null;
		int count;
		if (items != null && (count = items.Count) > 0)
		{
			arrayList = new ArrayList();
			for (int i = 0; i < count; i++)
			{
				if (items[i].Selected)
				{
					arrayList.Add(i);
				}
			}
		}
		return arrayList;
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control and the items it contains.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</returns>
	protected override object SaveViewState()
	{
		object y = null;
		object x = base.SaveViewState();
		IStateManager stateManager = items;
		if (stateManager != null)
		{
			y = stateManager.SaveViewState();
		}
		return new Pair(x, y);
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control.</param>
	protected override void LoadViewState(object savedState)
	{
		object savedState2 = null;
		object obj = null;
		if (savedState is Pair pair)
		{
			savedState2 = pair.First;
			obj = pair.Second;
		}
		base.LoadViewState(savedState2);
		if (obj != null)
		{
			((IStateManager)Items).LoadViewState(obj);
		}
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of a <see cref="T:System.Web.UI.WebControls.ListItem" /> control after a page is posted.</summary>
	/// <param name="selectedIndex">The index of the selected item in the <see cref="P:System.Web.UI.WebControls.ListControl.Items" /> collection.</param>
	[MonoTODO("Not implemented")]
	protected void SetPostDataSelection(int selectedIndex)
	{
		throw new NotImplementedException();
	}

	/// <summary>Marks the starting point to begin tracking and saving view-state changes to a <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		((IStateManager)items)?.TrackViewState();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ListControl.SelectedIndexChanged" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectedIndexChanged(EventArgs e)
	{
		((EventHandler)base.Events[SelectedIndexChanged])?.Invoke(this, e);
	}

	/// <summary>Determines whether the list control supports multiselection mode.</summary>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.UI.WebControls.ListBox.SelectionMode" /> is set to <see cref="F:System.Web.UI.WebControls.ListSelectionMode.Single" />.</exception>
	protected internal virtual void VerifyMultiSelect()
	{
		if (!MultiSelectOk())
		{
			throw new HttpException("Multi select is not supported");
		}
	}

	internal virtual bool MultiSelectOk()
	{
		return false;
	}

	static ListControl()
	{
		SelectedIndexChanged = new object();
		TextChanged = new object();
	}
}
