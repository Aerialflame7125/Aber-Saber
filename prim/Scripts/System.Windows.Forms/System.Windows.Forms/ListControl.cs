using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides a common implementation of members for the <see cref="T:System.Windows.Forms.ListBox" /> and <see cref="T:System.Windows.Forms.ComboBox" /> classes.</summary>
/// <filterpriority>1</filterpriority>
[LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public abstract class ListControl : Control
{
	private object data_source;

	private BindingMemberInfo value_member;

	private string display_member;

	private CurrencyManager data_manager;

	private BindingContext last_binding_context;

	private IFormatProvider format_info;

	private string format_string = string.Empty;

	private bool formatting_enabled;

	private static object DataSourceChangedEvent;

	private static object DisplayMemberChangedEvent;

	private static object FormatEvent;

	private static object FormatInfoChangedEvent;

	private static object FormatStringChangedEvent;

	private static object FormattingEnabledChangedEvent;

	private static object SelectedValueChangedEvent;

	private static object ValueMemberChangedEvent;

	/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior. </summary>
	/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[DefaultValue(null)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IFormatProvider FormatInfo
	{
		get
		{
			return format_info;
		}
		set
		{
			if (format_info != value)
			{
				format_info = value;
				RefreshItems();
				OnFormatInfoChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the format-specifier characters that indicate how a value is to be displayed.</summary>
	/// <returns>The string of format-specifier characters that indicates how a value is to be displayed.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue("")]
	[MergableProperty(false)]
	[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string FormatString
	{
		get
		{
			return format_string;
		}
		set
		{
			if (format_string != value)
			{
				format_string = value;
				RefreshItems();
				OnFormatStringChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether formatting is applied to the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property of the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
	/// <returns>true if formatting of the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is enabled; otherwise, false. The default is false.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(false)]
	public bool FormattingEnabled
	{
		get
		{
			return formatting_enabled;
		}
		set
		{
			if (formatting_enabled != value)
			{
				formatting_enabled = value;
				RefreshItems();
				OnFormattingEnabledChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the data source for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
	/// <returns>An object that implements the <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> interfaces, such as a <see cref="T:System.Data.DataSet" /> or an <see cref="T:System.Array" />. The default is null.</returns>
	/// <exception cref="T:System.ArgumentException">The assigned value does not implement the <see cref="T:System.Collections.IList" /> or <see cref="T:System.ComponentModel.IListSource" /> interfaces.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[AttributeProvider(typeof(IListSource))]
	[MWFCategory("Data")]
	public object DataSource
	{
		get
		{
			return data_source;
		}
		set
		{
			if (data_source != value)
			{
				if (value == null)
				{
					display_member = string.Empty;
				}
				else if (!(value is IList) && !(value is IListSource))
				{
					throw new Exception("Complex DataBinding accepts as a data source either an IList or an IListSource");
				}
				data_source = value;
				ConnectToDataSource();
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the property to display for this <see cref="T:System.Windows.Forms.ListControl" />.</summary>
	/// <returns>A <see cref="T:System.String" /> specifying the name of an object property that is contained in the collection specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. The default is an empty string (""). </returns>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Data")]
	[DefaultValue("")]
	[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DisplayMember
	{
		get
		{
			return display_member;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (!(display_member == value))
			{
				display_member = value;
				ConnectToDataSource();
				OnDisplayMemberChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>When overridden in a derived class, gets or sets the zero-based index of the currently selected item.</summary>
	/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
	/// <filterpriority>1</filterpriority>
	public abstract int SelectedIndex { get; set; }

	/// <summary>Gets or sets the value of the member property specified by the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property.</summary>
	/// <returns>An object containing the value of the member of the data source specified by the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property.</returns>
	/// <exception cref="T:System.InvalidOperationException">The assigned value is null or the empty string ("").</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Bindable(BindableSupport.Yes)]
	[Browsable(false)]
	public object SelectedValue
	{
		get
		{
			if (data_manager == null || SelectedIndex == -1)
			{
				return null;
			}
			object item = data_manager[SelectedIndex];
			return FilterItemOnProperty(item, ValueMember);
		}
		set
		{
			if (data_manager == null)
			{
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			PropertyDescriptorCollection itemProperties = data_manager.GetItemProperties();
			PropertyDescriptor propertyDescriptor = itemProperties.Find(ValueMember, ignoreCase: true);
			for (int i = 0; i < data_manager.Count; i++)
			{
				if (value.Equals(propertyDescriptor.GetValue(data_manager[i])))
				{
					SelectedIndex = i;
					return;
				}
			}
			SelectedIndex = -1;
		}
	}

	/// <summary>Gets or sets the property to use as the actual value for the items in the <see cref="T:System.Windows.Forms.ListControl" />.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the name of an object property that is contained in the collection specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. The default is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> property. </exception>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[MWFCategory("Data")]
	[DefaultValue("")]
	public string ValueMember
	{
		get
		{
			return value_member.BindingMember;
		}
		set
		{
			BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
			if (!value_member.Equals(bindingMemberInfo))
			{
				value_member = bindingMemberInfo;
				if (display_member == string.Empty)
				{
					DisplayMember = value_member.BindingMember;
				}
				ConnectToDataSource();
				OnValueMemberChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a value indicating whether the list enables selection of list items.</summary>
	/// <returns>true if the list enables list item selection; otherwise, false. The default is true.</returns>
	protected virtual bool AllowSelection => true;

	internal override bool ScaleChildrenInternal => false;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with this control. The default is null.</returns>
	protected CurrencyManager DataManager => data_manager;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.DataSource" /> changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DataSourceChanged
	{
		add
		{
			base.Events.AddHandler(DataSourceChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DataSourceChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DisplayMemberChanged
	{
		add
		{
			base.Events.AddHandler(DisplayMemberChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DisplayMemberChangedEvent, value);
		}
	}

	/// <summary>Occurs when the control is bound to a data value.</summary>
	/// <filterpriority>1</filterpriority>
	public event ListControlConvertEventHandler Format
	{
		add
		{
			base.Events.AddHandler(FormatEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FormatEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListControl.FormatInfo" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public event EventHandler FormatInfoChanged
	{
		add
		{
			base.Events.AddHandler(FormatInfoChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FormatInfoChangedEvent, value);
		}
	}

	/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.ListControl.FormatString" /> property changes</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler FormatStringChanged
	{
		add
		{
			base.Events.AddHandler(FormatStringChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FormatStringChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListControl.FormattingEnabled" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler FormattingEnabledChanged
	{
		add
		{
			base.Events.AddHandler(FormattingEnabledChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FormattingEnabledChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.SelectedValue" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SelectedValueChanged
	{
		add
		{
			base.Events.AddHandler(SelectedValueChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectedValueChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListControl.ValueMember" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ValueMemberChanged
	{
		add
		{
			base.Events.AddHandler(ValueMemberChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValueMemberChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListControl" /> class. </summary>
	protected ListControl()
	{
		value_member = new BindingMemberInfo(string.Empty);
		display_member = string.Empty;
		SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, value: false);
	}

	static ListControl()
	{
		DataSourceChanged = new object();
		DisplayMemberChanged = new object();
		Format = new object();
		FormatInfoChanged = new object();
		FormatStringChanged = new object();
		FormattingEnabledChanged = new object();
		SelectedValueChanged = new object();
		ValueMemberChanged = new object();
	}

	/// <summary>Retrieves the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object, given the item.</summary>
	/// <returns>The filtered object.</returns>
	/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
	protected object FilterItemOnProperty(object item)
	{
		return FilterItemOnProperty(item, string.Empty);
	}

	/// <summary>Returns the current value of the <see cref="T:System.Windows.Forms.ListControl" /> item, if it is a property of an object given the item and the property name.</summary>
	/// <returns>The filtered object.</returns>
	/// <param name="item">The object the <see cref="T:System.Windows.Forms.ListControl" /> item is bound to.</param>
	/// <param name="field">The property name of the item the <see cref="T:System.Windows.Forms.ListControl" /> is bound to.</param>
	protected object FilterItemOnProperty(object item, string field)
	{
		if (item == null)
		{
			return null;
		}
		if (field == null || field == string.Empty)
		{
			return item;
		}
		PropertyDescriptor propertyDescriptor = null;
		if (data_manager != null)
		{
			PropertyDescriptorCollection itemProperties = data_manager.GetItemProperties();
			propertyDescriptor = itemProperties.Find(field, ignoreCase: true);
		}
		else
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item);
			propertyDescriptor = properties.Find(field, ignoreCase: true);
		}
		if (propertyDescriptor == null)
		{
			return item;
		}
		return propertyDescriptor.GetValue(item);
	}

	/// <summary>Returns the text representation of the specified item.</summary>
	/// <returns>If the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property is not specified, the value returned by <see cref="M:System.Windows.Forms.ListControl.GetItemText(System.Object)" /> is the value of the item's ToString method. Otherwise, the method returns the string value of the member specified in the <see cref="P:System.Windows.Forms.ListControl.DisplayMember" /> property for the object specified in the <paramref name="item" /> parameter.</returns>
	/// <param name="item">The object from which to get the contents to display. </param>
	/// <filterpriority>1</filterpriority>
	public string GetItemText(object item)
	{
		object obj = FilterItemOnProperty(item, DisplayMember);
		if (obj == null)
		{
			obj = item;
		}
		string text = obj.ToString();
		if (FormattingEnabled)
		{
			ListControlConvertEventArgs listControlConvertEventArgs = new ListControlConvertEventArgs(text, typeof(string), item);
			OnFormat(listControlConvertEventArgs);
			if (listControlConvertEventArgs.Value.ToString() != text)
			{
				return listControlConvertEventArgs.Value.ToString();
			}
			if (obj is IFormattable)
			{
				return ((IFormattable)obj).ToString((!string.IsNullOrEmpty(FormatString)) ? FormatString : null, FormatInfo);
			}
		}
		return text;
	}

	/// <summary>Handles special input keys, such as PAGE UP, PAGE DOWN, HOME, END, and so on.</summary>
	/// <returns>true if the <paramref name="keyData" /> parameter specifies the <see cref="F:System.Windows.Forms.Keys.End" />, <see cref="F:System.Windows.Forms.Keys.Home" />, <see cref="F:System.Windows.Forms.Keys.PageUp" />, or <see cref="F:System.Windows.Forms.Keys.PageDown" /> key; false if the <paramref name="keyData" /> parameter specifies <see cref="F:System.Windows.Forms.Keys.Alt" />.</returns>
	/// <param name="keyData">One of the values of <see cref="T:System.Windows.Forms.Keys" />.</param>
	protected override bool IsInputKey(Keys keyData)
	{
		switch (keyData)
		{
		case Keys.ShiftKey:
		case Keys.ControlKey:
		case Keys.Space:
		case Keys.PageUp:
		case Keys.PageDown:
		case Keys.End:
		case Keys.Home:
		case Keys.Left:
		case Keys.Up:
		case Keys.Right:
		case Keys.Down:
			return true;
		default:
			return false;
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBindingContextChanged(EventArgs e)
	{
		base.OnBindingContextChanged(e);
		if (last_binding_context == BindingContext)
		{
			return;
		}
		last_binding_context = BindingContext;
		ConnectToDataSource();
		if (DataManager != null)
		{
			SetItemsCore(DataManager.List);
			if (AllowSelection)
			{
				SelectedIndex = DataManager.Position;
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DataSourceChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnDataSourceChanged(EventArgs e)
	{
		((EventHandler)base.Events[DataSourceChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DisplayMemberChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnDisplayMemberChanged(EventArgs e)
	{
		((EventHandler)base.Events[DisplayMemberChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.Format" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ListControlConvertEventArgs" /> that contains the event data. </param>
	protected virtual void OnFormat(ListControlConvertEventArgs e)
	{
		((ListControlConvertEventHandler)base.Events[Format])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormatInfoChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnFormatInfoChanged(EventArgs e)
	{
		((EventHandler)base.Events[FormatInfoChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormatStringChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnFormatStringChanged(EventArgs e)
	{
		((EventHandler)base.Events[FormatStringChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.FormattingEnabledChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnFormattingEnabledChanged(EventArgs e)
	{
		((EventHandler)base.Events[FormattingEnabledChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectedIndexChanged(EventArgs e)
	{
		if (data_manager != null && data_manager.Position != SelectedIndex)
		{
			data_manager.Position = SelectedIndex;
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectedValueChanged(EventArgs e)
	{
		((EventHandler)base.Events[SelectedValueChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.ValueMemberChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnValueMemberChanged(EventArgs e)
	{
		((EventHandler)base.Events[ValueMemberChanged])?.Invoke(this, e);
	}

	/// <summary>When overridden in a derived class, resynchronizes the data of the object at the specified index with the contents of the data source.</summary>
	/// <param name="index">The zero-based index of the item whose data to refresh. </param>
	protected abstract void RefreshItem(int index);

	/// <summary>When overridden in a derived class, resynchronizes the item data with the contents of the data source.</summary>
	protected virtual void RefreshItems()
	{
	}

	/// <summary>When overridden in a derived class, sets the object with the specified index in the derived class.</summary>
	/// <param name="index">The array index of the object.</param>
	/// <param name="value">The object.</param>
	protected virtual void SetItemCore(int index, object value)
	{
	}

	/// <summary>When overridden in a derived class, sets the specified array of objects in a collection in the derived class.</summary>
	/// <param name="items">An array of items.</param>
	protected abstract void SetItemsCore(IList items);

	internal void BindDataItems()
	{
		object itemsCore;
		if (data_manager != null)
		{
			IList list = data_manager.List;
			itemsCore = list;
		}
		else
		{
			itemsCore = new object[0];
		}
		SetItemsCore((IList)itemsCore);
	}

	private void ConnectToDataSource()
	{
		if (BindingContext == null)
		{
			return;
		}
		CurrencyManager currencyManager = null;
		if (data_source != null)
		{
			currencyManager = (CurrencyManager)BindingContext[data_source];
		}
		if (currencyManager != data_manager)
		{
			if (data_manager != null)
			{
				data_manager.PositionChanged -= OnPositionChanged;
				data_manager.ItemChanged -= OnItemChanged;
			}
			if (currencyManager != null)
			{
				currencyManager.PositionChanged += OnPositionChanged;
				currencyManager.ItemChanged += OnItemChanged;
			}
			data_manager = currencyManager;
		}
	}

	private void OnItemChanged(object sender, ItemChangedEventArgs e)
	{
		if (e.Index == -1)
		{
			SetItemsCore(data_manager.List);
		}
		else
		{
			RefreshItem(e.Index);
		}
		if (AllowSelection && SelectedIndex == -1 && data_manager.Count == 1)
		{
			SelectedIndex = data_manager.Position;
		}
	}

	private void OnPositionChanged(object sender, EventArgs e)
	{
		if (AllowSelection && data_manager.Count > 1)
		{
			SelectedIndex = data_manager.Position;
		}
	}
}
