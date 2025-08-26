using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the base class for all data control field types, which represent a column of data in tabular data-bound controls such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> and <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
[DefaultProperty("HeaderText")]
[TypeConverter(typeof(ExpandableObjectConverter))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class DataControlField : IStateManager, IDataSourceViewSchemaAccessor
{
	private static readonly object fieldChangedEvent = new object();

	private bool tracking;

	private StateBag viewState;

	private Control control;

	private Style controlStyle;

	private TableItemStyle footerStyle;

	private TableItemStyle headerStyle;

	private TableItemStyle itemStyle;

	private bool sortingEnabled;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.DataControlField" /> object across multiple requests for the same page.</summary>
	/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view-state information.</returns>
	protected StateBag ViewState => viewState;

	internal bool ControlStyleCreated => controlStyle != null;

	internal bool HeaderStyleCreated => headerStyle != null;

	internal bool FooterStyleCreated => footerStyle != null;

	internal bool ItemStyleCreated => itemStyle != null;

	/// <summary>Gets or sets text that is rendered as the <see langword="AbbreviatedText" /> property value in some controls.</summary>
	/// <returns>A string that represents abbreviated text read by screen readers. The default value is an empty string ("").</returns>
	[MonoTODO("Render this")]
	[DefaultValue("")]
	[Localizable(true)]
	[WebCategory("Accessibility")]
	public virtual string AccessibleHeaderText
	{
		get
		{
			object obj = viewState["accessibleHeaderText"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["accessibleHeaderText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets a reference to the data control that the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is associated with.</summary>
	/// <returns>The data control that owns the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	protected Control Control => control;

	/// <summary>Gets the style of any Web server controls contained by the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that governs the appearance of Web server controls contained by the field.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public Style ControlStyle
	{
		get
		{
			if (controlStyle == null)
			{
				controlStyle = new Style();
				if (IsTrackingViewState)
				{
					controlStyle.TrackViewState();
				}
			}
			return controlStyle;
		}
	}

	/// <summary>Gets a value indicating whether a data control field is currently viewed in a design-time environment.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is currently viewed in a design-time environment; otherwise, <see langword="false" />.</returns>
	protected bool DesignMode
	{
		get
		{
			if (control == null || control.Site == null)
			{
				return false;
			}
			return control.Site.DesignMode;
		}
	}

	/// <summary>Gets or sets the style of the footer of the data control field.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of the footer item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebCategory("Styles")]
	public TableItemStyle FooterStyle
	{
		get
		{
			if (footerStyle == null)
			{
				footerStyle = new TableItemStyle();
				if (IsTrackingViewState)
				{
					footerStyle.TrackViewState();
				}
			}
			return footerStyle;
		}
	}

	/// <summary>Gets or sets the text that is displayed in the footer item of a data control field.</summary>
	/// <returns>A string that is displayed in the footer item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	[Localizable(true)]
	[WebCategory("Appearance")]
	[DefaultValue("")]
	public virtual string FooterText
	{
		get
		{
			object obj = viewState["footerText"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["footerText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL of an image that is displayed in the header item of a data control field.</summary>
	/// <returns>A string that represents a fully qualified or relative URL to an image that is displayed in the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WebCategory("Appearance")]
	public virtual string HeaderImageUrl
	{
		get
		{
			object obj = viewState["headerImageUrl"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["headerImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the style of the header of the data control field.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> header item.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	public TableItemStyle HeaderStyle
	{
		get
		{
			if (headerStyle == null)
			{
				headerStyle = new TableItemStyle();
				if (IsTrackingViewState)
				{
					headerStyle.TrackViewState();
				}
			}
			return headerStyle;
		}
	}

	/// <summary>Gets or sets the text that is displayed in the header item of a data control field.</summary>
	/// <returns>A string that is displayed in the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebCategory("Appearance")]
	public virtual string HeaderText
	{
		get
		{
			object obj = viewState["headerText"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["headerText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is visible when its parent data-bound control is in insert mode.</summary>
	/// <returns>
	///     <see langword="true" /> if the field is visible when its parent data-bound control is rendered in insert mode; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebCategory("Behavior")]
	[DefaultValue(true)]
	public virtual bool InsertVisible
	{
		get
		{
			object obj = viewState["InsertVisible"];
			if (obj == null)
			{
				return true;
			}
			return (bool)obj;
		}
		set
		{
			viewState["InsertVisible"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets the style of any text-based content displayed by a data control field.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of text displayed in a <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebCategory("Styles")]
	[DefaultValue(null)]
	public TableItemStyle ItemStyle
	{
		get
		{
			if (itemStyle == null)
			{
				itemStyle = new TableItemStyle();
				if (IsTrackingViewState)
				{
					itemStyle.TrackViewState();
				}
			}
			return itemStyle;
		}
	}

	/// <summary>Gets or sets a value indicating whether the header item of a data control field is rendered.</summary>
	/// <returns>
	///     <see langword="true" /> if the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is rendered; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[WebCategory("Behavior")]
	[DefaultValue(true)]
	public virtual bool ShowHeader
	{
		get
		{
			object obj = viewState["showHeader"];
			if (obj == null)
			{
				return true;
			}
			return (bool)obj;
		}
		set
		{
			viewState["showHeader"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a sort expression that is used by a data source control to sort data.</summary>
	/// <returns>A sort expression that is used by a data source control to sort data. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[WebCategory("Behavior")]
	public virtual string SortExpression
	{
		get
		{
			object obj = viewState["sortExpression"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			viewState["sortExpression"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether a data control field is rendered.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is rendered; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebCategory("Behavior")]
	[DefaultValue(true)]
	public bool Visible
	{
		get
		{
			object obj = viewState["visible"];
			if (obj == null)
			{
				return true;
			}
			return (bool)obj;
		}
		set
		{
			if (value != Visible)
			{
				viewState["visible"] = value;
				OnFieldChanged();
			}
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => tracking;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is saving changes to its view state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Gets or sets the schema associated with this <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <returns>The schema associated with this <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	object IDataSourceViewSchemaAccessor.DataSourceViewSchema
	{
		get
		{
			return viewState["dataSourceViewSchema"];
		}
		set
		{
			viewState["dataSourceViewSchema"] = value;
		}
	}

	internal event EventHandler FieldChanged
	{
		add
		{
			events.AddHandler(fieldChangedEvent, value);
		}
		remove
		{
			events.RemoveHandler(fieldChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> class.</summary>
	protected DataControlField()
	{
		viewState = new StateBag();
	}

	internal void SetDirty()
	{
		viewState.SetDirty(dirty: true);
	}

	/// <summary>Extracts the value of the data control field from the current table cell and adds the value to the specified <see cref="T:System.Collections.IDictionary" /> collection.</summary>
	/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />.</param>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="includeReadOnly">
	///       <see langword="true" /> to indicate that the values of read-only fields are included in the <paramref name="dictionary" /> collection; otherwise, <see langword="false" />.</param>
	public virtual void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
	{
	}

	/// <summary>Performs basic instance initialization for a data control field.</summary>
	/// <param name="sortingEnabled">A value that indicates whether the control supports the sorting of columns of data.</param>
	/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
	/// <returns>Always returns <see langword="false" />.</returns>
	public virtual bool Initialize(bool sortingEnabled, Control control)
	{
		this.sortingEnabled = sortingEnabled;
		this.control = control;
		return false;
	}

	/// <summary>Adds text or controls to a cell's controls collection.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
	/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" />.</param>
	/// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> is contained in.</param>
	public virtual void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
	{
		switch (cellType)
		{
		case DataControlCellType.Header:
			if (HeaderText.Length > 0 && sortingEnabled && SortExpression.Length > 0)
			{
				cell.Controls.Add((Control)DataControlButton.CreateButton((!string.IsNullOrEmpty(HeaderImageUrl)) ? ButtonType.Image : ButtonType.Link, control, HeaderText, HeaderImageUrl, "Sort", SortExpression, allowCallback: true));
			}
			else if (HeaderImageUrl.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = HeaderImageUrl;
				cell.Controls.Add(image);
			}
			else
			{
				cell.Text = ((HeaderText.Length > 0) ? HeaderText : "&nbsp;");
			}
			break;
		case DataControlCellType.Footer:
		{
			string footerText = FooterText;
			cell.Text = ((footerText.Length > 0) ? footerText : "&nbsp;");
			break;
		}
		}
	}

	/// <summary>Creates a duplicate copy of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</summary>
	/// <returns>A duplicate copy of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	protected internal DataControlField CloneField()
	{
		DataControlField dataControlField = CreateField();
		CopyProperties(dataControlField);
		return dataControlField;
	}

	/// <summary>When overridden in a derived class, creates an empty <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</summary>
	/// <returns>An empty <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</returns>
	protected abstract DataControlField CreateField();

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
	protected virtual void CopyProperties(DataControlField newField)
	{
		newField.AccessibleHeaderText = AccessibleHeaderText;
		newField.ControlStyle.CopyFrom(ControlStyle);
		newField.FooterStyle.CopyFrom(FooterStyle);
		newField.FooterText = FooterText;
		newField.HeaderImageUrl = HeaderImageUrl;
		newField.HeaderStyle.CopyFrom(HeaderStyle);
		newField.HeaderText = HeaderText;
		newField.InsertVisible = InsertVisible;
		newField.ItemStyle.CopyFrom(ItemStyle);
		newField.ShowHeader = ShowHeader;
		newField.SortExpression = SortExpression;
		newField.Visible = Visible;
	}

	/// <summary>Raises the <see langword="FieldChanged" /> event.</summary>
	protected virtual void OnFieldChanged()
	{
		if (events[fieldChangedEvent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}

	/// <summary>Restores the data source view's previously saved view state.</summary>
	/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.DataControlField" /> state to restore.</param>
	protected virtual void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			viewState.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)ControlStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)FooterStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)ItemStyle).LoadViewState(array[4]);
			}
		}
	}

	/// <summary>Saves the changes made to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state since the time the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state. If there is no view state associated with the object, this method returns <see langword="null" />.</returns>
	protected virtual object SaveViewState()
	{
		object[] array = new object[5]
		{
			viewState.SaveViewState(),
			null,
			null,
			null,
			null
		};
		if (controlStyle != null)
		{
			array[1] = ((IStateManager)controlStyle).SaveViewState();
		}
		if (footerStyle != null)
		{
			array[2] = ((IStateManager)footerStyle).SaveViewState();
		}
		if (headerStyle != null)
		{
			array[3] = ((IStateManager)headerStyle).SaveViewState();
		}
		if (itemStyle != null)
		{
			array[4] = ((IStateManager)itemStyle).SaveViewState();
		}
		if (array[0] == null && array[1] == null && array[2] == null && array[3] == null && array[4] == null)
		{
			return null;
		}
		return array;
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.WebControls.DataControlField.ViewState" /> property and persisted across requests for the same page.</summary>
	protected virtual void TrackViewState()
	{
		if (controlStyle != null)
		{
			((IStateManager)controlStyle).TrackViewState();
		}
		if (footerStyle != null)
		{
			((IStateManager)footerStyle).TrackViewState();
		}
		if (headerStyle != null)
		{
			((IStateManager)headerStyle).TrackViewState();
		}
		if (itemStyle != null)
		{
			((IStateManager)itemStyle).TrackViewState();
		}
		viewState.TrackViewState();
		tracking = true;
	}

	/// <summary>When overridden in a derived class, signals that the controls contained by a field support callbacks.</summary>
	/// <exception cref="T:System.NotSupportedException">The method is called on a default instance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> class.</exception>
	public virtual void ValidateSupportsCallback()
	{
		throw new NotSupportedException("Callback not supported");
	}

	/// <summary>Restores the data control field's previously saved view state.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>Saves the changes made to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state since the time the page was posted back to the server.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.WebControls.DataControlField.ViewState" /> property and persisted across requests for the same page.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	internal Exception GetNotSupportedPropException(string propName)
	{
		return new NotSupportedException("The property '" + propName + "' is not supported in " + GetType().Name);
	}

	/// <summary>Returns a string that represents this <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
	public override string ToString()
	{
		if (string.IsNullOrEmpty(HeaderText))
		{
			return base.ToString();
		}
		return HeaderText;
	}
}
