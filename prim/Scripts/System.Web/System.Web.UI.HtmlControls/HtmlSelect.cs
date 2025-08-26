using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;select&gt;" /> element on the server.</summary>
[DefaultEvent("ServerChange")]
[ValidationProperty("Value")]
[ControlBuilder(typeof(HtmlSelectBuilder))]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlSelect : HtmlContainerControl, IPostBackDataHandler, IParserAccessor
{
	private static readonly object EventServerChange = new object();

	private DataSourceView _boundDataSourceView;

	private bool requiresDataBinding;

	private bool _initialized;

	private object datasource;

	private ListItemCollection items;

	/// <summary>Gets or sets the set of data to bind to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control from a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property with multiple sets of data.</summary>
	/// <returns>The set of data to bind to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control from a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> with multiple sets of data. The default value is an empty string (""), which indicates the property has not been set.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" /> property is set during the data-binding phase of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. </exception>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataMember
	{
		get
		{
			string text = base.Attributes["datamember"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("datamember");
			}
			else
			{
				base.Attributes["datamember"] = value;
			}
		}
	}

	/// <summary>Gets or sets the source of information to bind to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to this control. The default value is <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">The specified data source is not compatible with either <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" />, and it is not <see langword="null" />. </exception>
	/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property and the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property. </exception>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual object DataSource
	{
		get
		{
			return datasource;
		}
		set
		{
			if (value != null && !(value is IEnumerable) && !(value is IListSource))
			{
				throw new ArgumentException();
			}
			datasource = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control should use to retrieve its data source.</summary>
	/// <returns>The programmatic identifier assigned to the data source control. The default value is an empty string (""), which indicates that the property has not been set.</returns>
	/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property and the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property. </exception>
	[DefaultValue("")]
	public virtual string DataSourceID
	{
		get
		{
			return ViewState.GetString("DataSourceID", "");
		}
		set
		{
			if (!(DataSourceID == value))
			{
				ViewState["DataSourceID"] = value;
				if (_boundDataSourceView != null)
				{
					_boundDataSourceView.DataSourceViewChanged -= OnDataSourceViewChanged;
				}
				_boundDataSourceView = null;
				OnDataPropertyChanged();
			}
		}
	}

	/// <summary>Gets or sets the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>The field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. The default value is an empty string (""), which indicates that the property has not been set.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataTextField
	{
		get
		{
			string text = base.Attributes["datatextfield"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("datatextfield");
			}
			else
			{
				base.Attributes["datatextfield"] = value;
			}
		}
	}

	/// <summary>Gets or sets the field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>The field from the data source to bind to the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of each item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. The default value is an empty string (""), which indicates that the property has not been set.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataValueField
	{
		get
		{
			string text = base.Attributes["datavaluefield"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("datavaluefield");
			}
			else
			{
				base.Attributes["datavaluefield"] = value;
			}
		}
	}

	/// <summary>Gets or sets the content between the opening and closing tags of the control without automatically converting special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
	/// <returns>The content between the opening and closing tags of the control.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read from or assign a value to this property. </exception>
	public override string InnerHtml
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the content between the opening and closing tags of the control with automatic conversion of special characters to their equivalent HTML entities. This property is not supported for this control.</summary>
	/// <returns>The content between the opening and closing tags of the control.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt is made to read from or assign a value to this property. </exception>
	public override string InnerText
	{
		get
		{
			throw new NotSupportedException();
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets a value indicating whether a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is defined for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. </summary>
	/// <returns>
	///     <see langword="true" /> if a data source control is defined; otherwise, <see langword="false" />.</returns>
	protected bool IsBoundUsingDataSourceID => DataSourceID.Length != 0;

	/// <summary>Gets a collection that contains the items listed in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> that contains the items listed in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ListItemCollection Items
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

	/// <summary>Gets or sets a value indicating whether multiple items can be selected concurrently in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if multiple items can be concurrently selected in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public bool Multiple
	{
		get
		{
			if (base.Attributes["multiple"] == null)
			{
				return false;
			}
			return true;
		}
		set
		{
			if (!value)
			{
				base.Attributes.Remove("multiple");
			}
			else
			{
				base.Attributes["multiple"] = "multiple";
			}
		}
	}

	/// <summary>Gets or sets the unique identifier name associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>The unique identifier name associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string Name
	{
		get
		{
			return UniqueID;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control needs to bind to its specified data source.</summary>
	/// <returns>
	///     <see langword="true" /> if the control needs to bind to a data source; otherwise, <see langword="false" />.</returns>
	protected bool RequiresDataBinding
	{
		get
		{
			return requiresDataBinding;
		}
		set
		{
			requiresDataBinding = value;
		}
	}

	/// <summary>Gets or sets the ordinal index of the selected item in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>The ordinal index of the selected item in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. A value of <see langword="-1" /> indicates that no item is selected.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than <see langword="-1" />.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual int SelectedIndex
	{
		get
		{
			ListItemCollection listItemCollection = Items;
			for (int i = 0; i < listItemCollection.Count; i++)
			{
				if (listItemCollection[i].Selected)
				{
					return i;
				}
			}
			if (!Multiple && Size <= 1)
			{
				if (listItemCollection.Count > 0)
				{
					listItemCollection[0].Selected = true;
				}
				return 0;
			}
			return -1;
		}
		set
		{
			ClearSelection();
			if (value != -1 && items != null)
			{
				if (value < 0 || value >= items.Count)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				items[value].Selected = true;
			}
		}
	}

	/// <summary>Gets a collection that contains the zero-based indexes of all currently selected items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>A collection that contains the zero-based indexes of all currently selected items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
	protected virtual int[] SelectedIndices
	{
		get
		{
			ArrayList arrayList = new ArrayList();
			int count = Items.Count;
			for (int i = 0; i < count; i++)
			{
				if (Items[i].Selected)
				{
					arrayList.Add(i);
				}
			}
			return (int[])arrayList.ToArray(typeof(int));
		}
	}

	/// <summary>Gets or sets the height (in rows) of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>The height (in rows) of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Size
	{
		get
		{
			string text = base.Attributes["size"];
			if (text == null)
			{
				return -1;
			}
			return int.Parse(text, Helpers.InvariantCulture);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("size");
			}
			else
			{
				base.Attributes["size"] = value.ToString();
			}
		}
	}

	/// <summary>Gets the value of the selected item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or sets the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property of the control to the index of the first item in the list with the specified value.</summary>
	/// <returns>The value of the selected item in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. If no item is selected in the control, <see cref="F:System.String.Empty" /> is returned.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.Value" /> property was set to an item greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than <see langword="-1" />.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Value
	{
		get
		{
			int selectedIndex = SelectedIndex;
			if (selectedIndex >= 0 && selectedIndex < Items.Count)
			{
				return Items[selectedIndex].Value;
			}
			return string.Empty;
		}
		set
		{
			int num = Items.IndexOf(value);
			if (num >= 0)
			{
				SelectedIndex = num;
			}
		}
	}

	private bool IsDataBound
	{
		get
		{
			return ViewState.GetBool("_DataBound", def: false);
		}
		set
		{
			ViewState["_DataBound"] = value;
		}
	}

	/// <summary>Occurs when the selected items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control change between posts to the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerChange
	{
		add
		{
			base.Events.AddHandler(EventServerChange, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventServerChange, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> class.</summary>
	public HtmlSelect()
		: base("select")
	{
	}

	/// <summary>Adds a parsed child control to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <param name="obj">The parsed child control to add. </param>
	/// <exception cref="T:System.Web.HttpException">The child control specified by the <paramref name="obj" /> parameter must be of the type <see cref="T:System.Web.UI.WebControls.ListItem" />.</exception>
	protected override void AddParsedSubObject(object obj)
	{
		if (!(obj is ListItem))
		{
			throw new HttpException("HtmlSelect can only contain ListItem");
		}
		Items.Add((ListItem)obj);
		base.AddParsedSubObject(obj);
	}

	/// <summary>Clears the list selection of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control and sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of all items to <see langword="false" />.</summary>
	protected virtual void ClearSelection()
	{
		if (items != null)
		{
			int count = items.Count;
			for (int i = 0; i < count; i++)
			{
				items[i].Selected = false;
			}
		}
	}

	/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> to contain the current server control's child server controls. </returns>
	protected override ControlCollection CreateControlCollection()
	{
		return base.CreateControlCollection();
	}

	/// <summary>Verifies that the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.Control.DataBind" /> method.</summary>
	protected void EnsureDataBound()
	{
		if (IsBoundUsingDataSourceID && RequiresDataBinding)
		{
			DataBind();
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.IEnumerable" /> object that represents the data source that is bound to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> object. If no data source is specified, a default value of <see langword="null" /> is returned.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IDataSource" />.- or - The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">Both a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> and a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property are defined for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.- or -The requested data view cannot be found.</exception>
	protected virtual IEnumerable GetData()
	{
		if (DataSource != null && IsBoundUsingDataSourceID)
		{
			throw new HttpException("Control bound using both DataSourceID and DataSource properties.");
		}
		if (DataSource != null)
		{
			return DataSourceResolver.ResolveDataSource(DataSource, DataMember);
		}
		if (!IsBoundUsingDataSourceID)
		{
			return null;
		}
		IEnumerable result = null;
		ConnectToDataSource().Select(DataSourceSelectArguments.Empty, delegate(IEnumerable data)
		{
			result = data;
		});
		return result;
	}

	/// <summary>Restores the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's view state information from a previous page request that was saved by the <see cref="M:System.Web.UI.HtmlControls.HtmlSelect.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
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

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event of an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IDataSource" />.- or - The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of type <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">Both a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> and a <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property are defined for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.- or -The requested data view cannot be found.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than <see langword="-1" />.</exception>
	protected override void OnDataBinding(EventArgs e)
	{
		base.OnDataBinding(e);
		ListItemCollection listItemCollection = Items;
		listItemCollection.Clear();
		IEnumerable data = GetData();
		if (data == null)
		{
			return;
		}
		foreach (object item2 in data)
		{
			string text = null;
			string text2 = null;
			if (DataTextField == string.Empty && DataValueField == string.Empty)
			{
				text = item2.ToString();
				text2 = text;
			}
			else
			{
				if (DataTextField != string.Empty)
				{
					text = DataBinder.Eval(item2, DataTextField).ToString();
				}
				text2 = ((!(DataValueField != string.Empty)) ? text : DataBinder.Eval(item2, DataValueField).ToString());
				if (text == null && text2 != null)
				{
					text = text2;
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (text2 == null)
			{
				text2 = string.Empty;
			}
			ListItem item = new ListItem(text, text2);
			listItemCollection.Add(item);
		}
		RequiresDataBinding = false;
		IsDataBound = true;
	}

	/// <summary>Invoked when the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" />, <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" />, or <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is changed.</summary>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
	protected virtual void OnDataPropertyChanged()
	{
		if (_initialized)
		{
			RequiresDataBinding = true;
		}
	}

	/// <summary>Invoked when the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" />, <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" />, or <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is changed.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
	{
		RequiresDataBinding = true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Page.PreLoad += OnPagePreLoad;
	}

	protected virtual void OnPagePreLoad(object sender, EventArgs e)
	{
		Initialize();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <exception cref="T:System.Web.HttpException">The ID specified in the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property cannot be found.- or -The control specified in the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property is not of the type <see cref="T:System.Web.UI.IDataSource" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSource" /> property and the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataSourceID" /> property. - or -The requested <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.DataMember" /> property could not be found.</exception>
	protected internal override void OnLoad(EventArgs e)
	{
		if (!_initialized)
		{
			Initialize();
		}
		base.OnLoad(e);
	}

	private void Initialize()
	{
		_initialized = true;
		if (!IsDataBound)
		{
			RequiresDataBinding = true;
		}
		if (IsBoundUsingDataSourceID)
		{
			ConnectToDataSource();
		}
	}

	private DataSourceView ConnectToDataSource()
	{
		if (_boundDataSourceView != null)
		{
			return _boundDataSourceView;
		}
		object obj = null;
		Page page = Page;
		if (page != null)
		{
			obj = page.FindControl(DataSourceID);
		}
		if (obj == null || !(obj is IDataSource))
		{
			string format = ((obj != null) ? "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  '{1}' is not an IDataSource." : "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.");
			throw new HttpException(string.Format(format, ID, DataSourceID));
		}
		_boundDataSourceView = ((IDataSource)obj).GetView(string.Empty);
		_boundDataSourceView.DataSourceViewChanged += OnDataSourceViewChanged;
		return _boundDataSourceView;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		EnsureDataBound();
		base.OnPreRender(e);
		Page page = Page;
		if (page != null && !base.Disabled)
		{
			page.RegisterRequiresPostBack(this);
			page.RegisterEnabledControl(this);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlSelect.ServerChange" /> event of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnServerChange(EventArgs e)
	{
		((EventHandler)base.Events[EventServerChange])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(UniqueID);
		writer.WriteAttribute("name", Name);
		base.Attributes.Remove("name");
		base.Attributes.Remove("datamember");
		base.Attributes.Remove("datatextfield");
		base.Attributes.Remove("datavaluefield");
		base.RenderAttributes(writer);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's child controls to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	/// <exception cref="T:System.Web.HttpException">Multiple items were selected but the <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.Multiple" /> property is set to <see langword="false" />.</exception>
	protected internal override void RenderChildren(HtmlTextWriter writer)
	{
		base.RenderChildren(writer);
		if (items == null)
		{
			return;
		}
		writer.WriteLine();
		bool flag = false;
		int count = items.Count;
		for (int i = 0; i < count; i++)
		{
			ListItem listItem = items[i];
			writer.Indent++;
			writer.WriteBeginTag("option");
			if (listItem.Selected && !flag)
			{
				writer.WriteAttribute("selected", "selected");
				if (!Multiple)
				{
					flag = true;
				}
			}
			writer.WriteAttribute("value", listItem.Value, fEncode: true);
			if (listItem.HasAttributes)
			{
				AttributeCollection attributes = listItem.Attributes;
				foreach (string key in attributes.Keys)
				{
					writer.WriteAttribute(key, HttpUtility.HtmlAttributeEncode(attributes[key]));
				}
			}
			writer.Write('>');
			writer.Write(HttpUtility.HtmlEncode(listItem.Text));
			writer.WriteEndTag("option");
			writer.WriteLine();
			writer.Indent--;
		}
	}

	/// <summary>Saves any <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control view state changes that have occurred since the page was posted back to the server.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> view state. If no view state is associated with the object, this method returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	protected override object SaveViewState()
	{
		object obj = null;
		object obj2 = null;
		obj = base.SaveViewState();
		IStateManager stateManager = items;
		if (stateManager != null)
		{
			obj2 = stateManager.SaveViewState();
		}
		if (obj == null && obj2 == null)
		{
			return null;
		}
		return new Pair(obj, obj2);
	}

	/// <summary>Selects multiple items of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.Items" /> collection.</summary>
	/// <param name="selectedIndices">An <see cref="T:System.Array" /> of type <see cref="T:System.Int32" /> that contains the items to select.</param>
	protected virtual void Select(int[] selectedIndices)
	{
		if (items == null)
		{
			return;
		}
		ClearSelection();
		int count = items.Count;
		foreach (int num in selectedIndices)
		{
			if (num >= 0 && num < count)
			{
				items[num].Selected = true;
			}
		}
	}

	/// <summary>Tracks view state changes to the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control so the changes can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		((IStateManager)items)?.TrackViewState();
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlSelect.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		OnServerChange(EventArgs.Empty);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's state has changed as a result of a postback; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than <see langword="-1" />.</exception>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		string[] values = postCollection.GetValues(postDataKey);
		bool flag = false;
		if (values != null)
		{
			if (Multiple)
			{
				int num = values.Length;
				int[] selectedIndices = SelectedIndices;
				int[] array = new int[num];
				int num2 = selectedIndices.Length;
				for (int i = 0; i < num; i++)
				{
					array[i] = Items.IndexOf(values[i]);
					if (num2 != num || selectedIndices[i] != array[i])
					{
						flag = true;
					}
				}
				if (flag)
				{
					Select(array);
				}
			}
			else
			{
				int num3 = Items.IndexOf(values[0]);
				if (num3 != SelectedIndex)
				{
					SelectedIndex = num3;
					flag = true;
				}
			}
		}
		if (flag)
		{
			ValidateEvent(postDataKey, string.Empty);
		}
		return flag;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's state has changed as a result of a postback; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.HtmlControls.HtmlSelect.SelectedIndex" /> property was set to a value greater than the number of items in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control or less than <see langword="-1" />.</exception>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />. </summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
