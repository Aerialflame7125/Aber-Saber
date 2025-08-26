using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents the simple binding between the property value of an object and the property value of a control.</summary>
/// <filterpriority>1</filterpriority>
[TypeConverter(typeof(ListBindingConverter))]
public class Binding
{
	private string property_name;

	private object data_source;

	private string data_member;

	private bool is_binding;

	private bool checked_isnull;

	private BindingMemberInfo binding_member_info;

	private IBindableComponent control;

	private BindingManagerBase manager;

	private PropertyDescriptor control_property;

	private PropertyDescriptor is_null_desc;

	private object data;

	private Type data_type;

	private DataSourceUpdateMode datasource_update_mode;

	private ControlUpdateMode control_update_mode;

	private object datasource_null_value = Convert.DBNull;

	private object null_value;

	private IFormatProvider format_info;

	private string format_string;

	private bool formatting_enabled;

	/// <summary>Gets the control the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[DefaultValue(null)]
	public IBindableComponent BindableComponent => control;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> for this <see cref="T:System.Windows.Forms.Binding" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> that manages this <see cref="T:System.Windows.Forms.Binding" />.</returns>
	/// <filterpriority>1</filterpriority>
	public BindingManagerBase BindingManagerBase => manager;

	/// <summary>Gets an object that contains information about this binding based on the <paramref name="dataMember" /> parameter in the <see cref="Overload:System.Windows.Forms.Binding.#ctor" /> constructor.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.BindingMemberInfo" /> that contains information about this <see cref="T:System.Windows.Forms.Binding" />.</returns>
	/// <filterpriority>1</filterpriority>
	public BindingMemberInfo BindingMemberInfo => binding_member_info;

	/// <summary>Gets the control that the binding belongs to.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the binding belongs to.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	[DefaultValue(null)]
	public Control Control => control as Control;

	/// <summary>Gets or sets when changes to the data source are propagated to the bound control property.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ControlUpdateMode" /> values. The default is <see cref="F:System.Windows.Forms.ControlUpdateMode.OnPropertyChanged" />.</returns>
	[DefaultValue(ControlUpdateMode.OnPropertyChanged)]
	public ControlUpdateMode ControlUpdateMode
	{
		get
		{
			return control_update_mode;
		}
		set
		{
			control_update_mode = value;
		}
	}

	/// <summary>Gets the data source for this binding.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the data source.</returns>
	/// <filterpriority>1</filterpriority>
	public object DataSource => data_source;

	/// <summary>Gets or sets a value that indicates when changes to the bound control property are propagated to the data source.</summary>
	/// <returns>A value that indicates when changes are propagated. The default is <see cref="F:System.Windows.Forms.DataSourceUpdateMode.OnValidation" />.</returns>
	[DefaultValue(DataSourceUpdateMode.OnValidation)]
	public DataSourceUpdateMode DataSourceUpdateMode
	{
		get
		{
			return datasource_update_mode;
		}
		set
		{
			datasource_update_mode = value;
		}
	}

	/// <summary>Gets or sets the value to be stored in the data source if the control value is null or empty.</summary>
	/// <returns>The <see cref="T:System.Object" /> to be stored in the data source when the control property is empty or null. The default is <see cref="T:System.DBNull" /> for value types and null for non-value types.</returns>
	public object DataSourceNullValue
	{
		get
		{
			return datasource_null_value;
		}
		set
		{
			datasource_null_value = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether type conversion and formatting is applied to the control property data.</summary>
	/// <returns>true if type conversion and formatting of control property data is enabled; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
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
				PushData();
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior.</summary>
	/// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public IFormatProvider FormatInfo
	{
		get
		{
			return format_info;
		}
		set
		{
			if (value != format_info)
			{
				format_info = value;
				if (formatting_enabled)
				{
					PushData();
				}
			}
		}
	}

	/// <summary>Gets or sets the format specifier characters that indicate how a value is to be displayed.</summary>
	/// <returns>The string of format specifier characters that indicate how a value is to be displayed.</returns>
	/// <filterpriority>1</filterpriority>
	public string FormatString
	{
		get
		{
			return format_string;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (!(value == format_string))
			{
				format_string = value;
				if (formatting_enabled)
				{
					PushData();
				}
			}
		}
	}

	/// <summary>Gets a value indicating whether the binding is active.</summary>
	/// <returns>true if the binding is active; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsBinding
	{
		get
		{
			if (manager == null || manager.IsSuspended)
			{
				return false;
			}
			return is_binding;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. </summary>
	/// <returns>The <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	public object NullValue
	{
		get
		{
			return null_value;
		}
		set
		{
			if (value != null_value)
			{
				null_value = value;
				if (formatting_enabled)
				{
					PushData();
				}
			}
		}
	}

	/// <summary>Gets or sets the name of the control's data-bound property.</summary>
	/// <returns>The name of a control property to bind to.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	public string PropertyName => property_name;

	internal string DataMember => data_member;

	/// <summary>Occurs when the property of a control is bound to a data value.</summary>
	/// <filterpriority>1</filterpriority>
	public event ConvertEventHandler Format;

	/// <summary>Occurs when the value of a data-bound control changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event ConvertEventHandler Parse;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Binding.FormattingEnabled" /> property is set to true and a binding operation is complete, such as when data is pushed from the control to the data source or vice versa</summary>
	public event BindingCompleteEventHandler BindingComplete;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that simple-binds the indicated control property to the specified data member of the data source.</summary>
	/// <param name="propertyName">The name of the control property to bind. </param>
	/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
	/// <param name="dataMember">The property or list to bind to. </param>
	/// <exception cref="T:System.Exception">
	///   <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
	/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.</exception>
	public Binding(string propertyName, object dataSource, string dataMember)
		: this(propertyName, dataSource, dataMember, formattingEnabled: false, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the data source, and optionally enables formatting to be applied.</summary>
	/// <param name="propertyName">The name of the control property to bind. </param>
	/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
	/// <param name="dataMember">The property or list to bind to. </param>
	/// <param name="formattingEnabled">true to format the displayed data; otherwise, false. </param>
	/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The property given is a read-only property.</exception>
	/// <exception cref="T:System.Exception">Formatting is disabled and <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
	public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
		: this(propertyName, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting and propagates values to the data source based on the specified update setting.</summary>
	/// <param name="propertyName">The name of the control property to bind. </param>
	/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
	/// <param name="dataMember">The property or list to bind to.</param>
	/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
	/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
	/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
	public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
		: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, null, string.Empty, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the specified data source. Optionally enables formatting, propagates values to the data source based on the specified update setting, and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
	/// <param name="propertyName">The name of the control property to bind. </param>
	/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
	/// <param name="dataMember">The property or list to bind to.</param>
	/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
	/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
	/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
	/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
	public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue)
		: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
	/// <param name="propertyName">The name of the control property to bind. </param>
	/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
	/// <param name="dataMember">The property or list to bind to.</param>
	/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
	/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
	/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
	/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
	/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
	public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString)
		: this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class with the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; enables formatting with the specified format string; sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source; and sets the specified format provider.</summary>
	/// <param name="propertyName">The name of the control property to bind. </param>
	/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
	/// <param name="dataMember">The property or list to bind to.</param>
	/// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
	/// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
	/// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
	/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
	/// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
	/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
	public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
	{
		property_name = propertyName;
		data_source = dataSource;
		data_member = dataMember;
		binding_member_info = new BindingMemberInfo(dataMember);
		datasource_update_mode = dataSourceUpdateMode;
		null_value = nullValue;
		format_string = formatString;
		format_info = formatInfo;
	}

	/// <summary>Sets the control property to the value read from the data source.</summary>
	public void ReadValue()
	{
		PushData(force: true);
	}

	/// <summary>Reads the current value from the control property and writes it to the data source.</summary>
	public void WriteValue()
	{
		PullData(force: true);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
	protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
	{
		if (this.BindingComplete != null)
		{
			this.BindingComplete(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Format" /> event.</summary>
	/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
	protected virtual void OnFormat(ConvertEventArgs cevent)
	{
		if (this.Format != null)
		{
			this.Format(this, cevent);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Parse" /> event.</summary>
	/// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
	protected virtual void OnParse(ConvertEventArgs cevent)
	{
		if (this.Parse != null)
		{
			this.Parse(this, cevent);
		}
	}

	internal void SetControl(IBindableComponent control)
	{
		if (control == this.control)
		{
			return;
		}
		control_property = TypeDescriptor.GetProperties(control).Find(property_name, ignoreCase: true);
		if (control_property == null)
		{
			throw new ArgumentException("Cannot bind to property '" + property_name + "' on target control.");
		}
		if (control_property.IsReadOnly)
		{
			throw new ArgumentException("Cannot bind to property '" + property_name + "' because it is read only.");
		}
		data_type = control_property.PropertyType;
		if (control is Control control2)
		{
			control2.Validating += ControlValidatingHandler;
			if (!control2.IsHandleCreated)
			{
				control2.HandleCreated += ControlCreatedHandler;
			}
		}
		GetPropertyChangedEvent(control, property_name)?.AddEventHandler(control, new EventHandler(ControlPropertyChangedHandler));
		this.control = control;
		UpdateIsBinding();
	}

	internal void Check()
	{
		if (control == null || control.BindingContext == null)
		{
			return;
		}
		if (manager == null)
		{
			manager = control.BindingContext[data_source, binding_member_info.BindingPath];
			if (manager.Position > -1 && binding_member_info.BindingField != string.Empty && TypeDescriptor.GetProperties(manager.Current).Find(binding_member_info.BindingField, ignoreCase: true) == null)
			{
				throw new ArgumentException("Cannot bind to property '" + binding_member_info.BindingField + "' on DataSource.", "dataMember");
			}
			manager.AddBinding(this);
			manager.PositionChanged += PositionChangedHandler;
			if (manager is PropertyManager)
			{
				GetPropertyChangedEvent(manager.Current, binding_member_info.BindingField)?.AddEventHandler(manager.Current, new EventHandler(SourcePropertyChangedHandler));
			}
		}
		if (manager.Position != -1)
		{
			if (!checked_isnull)
			{
				is_null_desc = TypeDescriptor.GetProperties(manager.Current).Find(property_name + "IsNull", ignoreCase: false);
				checked_isnull = true;
			}
			PushData();
		}
	}

	internal bool PullData()
	{
		return PullData(force: false);
	}

	private bool PullData(bool force)
	{
		if (!IsBinding || manager.Current == null)
		{
			return true;
		}
		if (!force && datasource_update_mode == DataSourceUpdateMode.Never)
		{
			return true;
		}
		data = control_property.GetValue(control);
		if (data == null)
		{
			data = datasource_null_value;
		}
		try
		{
			SetPropertyValue(data);
		}
		catch (Exception ex)
		{
			if (formatting_enabled)
			{
				FireBindingComplete(BindingCompleteContext.DataSourceUpdate, ex, ex.Message);
				return false;
			}
			throw ex;
		}
		if (formatting_enabled)
		{
			FireBindingComplete(BindingCompleteContext.DataSourceUpdate, null, null);
		}
		return true;
	}

	internal void PushData()
	{
		PushData(force: false);
	}

	private void PushData(bool force)
	{
		if (manager == null || manager.IsSuspended || manager.Count == 0 || manager.Position == -1 || (!force && control_update_mode == ControlUpdateMode.Never))
		{
			return;
		}
		if (is_null_desc != null && (bool)is_null_desc.GetValue(manager.Current))
		{
			data = Convert.DBNull;
			return;
		}
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(manager.Current).Find(binding_member_info.BindingField, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			data = manager.Current;
		}
		else
		{
			data = propertyDescriptor.GetValue(manager.Current);
		}
		if ((data == null || data == DBNull.Value) && null_value != null)
		{
			data = null_value;
		}
		try
		{
			data = FormatData(data);
			SetControlValue(data);
		}
		catch (Exception ex)
		{
			if (formatting_enabled)
			{
				FireBindingComplete(BindingCompleteContext.ControlUpdate, ex, ex.Message);
				return;
			}
			throw ex;
		}
		if (formatting_enabled)
		{
			FireBindingComplete(BindingCompleteContext.ControlUpdate, null, null);
		}
	}

	internal void UpdateIsBinding()
	{
		is_binding = false;
		if (control != null && (!(control is Control) || ((Control)control).IsHandleCreated))
		{
			is_binding = true;
			PushData();
		}
	}

	private void SetControlValue(object data)
	{
		control_property.SetValue(control, data);
	}

	private void SetPropertyValue(object data)
	{
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(manager.Current).Find(binding_member_info.BindingField, ignoreCase: true);
		if (!propertyDescriptor.IsReadOnly)
		{
			data = ParseData(data, propertyDescriptor.PropertyType);
			propertyDescriptor.SetValue(manager.Current, data);
		}
	}

	private void ControlValidatingHandler(object sender, CancelEventArgs e)
	{
		if (datasource_update_mode == DataSourceUpdateMode.OnValidation)
		{
			bool flag = true;
			try
			{
				flag = PullData();
			}
			catch
			{
				flag = false;
			}
			e.Cancel = !flag;
		}
	}

	private void ControlCreatedHandler(object o, EventArgs args)
	{
		UpdateIsBinding();
	}

	private void PositionChangedHandler(object sender, EventArgs e)
	{
		Check();
		PushData();
	}

	private EventDescriptor GetPropertyChangedEvent(object o, string property_name)
	{
		if (o == null || property_name == null || property_name.Length == 0)
		{
			return null;
		}
		string text = property_name + "Changed";
		Type typeFromHandle = typeof(EventHandler);
		EventDescriptor result = null;
		foreach (EventDescriptor @event in TypeDescriptor.GetEvents(o))
		{
			if (@event.Name == text && (object)@event.EventType == typeFromHandle)
			{
				result = @event;
				break;
			}
		}
		return result;
	}

	private void SourcePropertyChangedHandler(object o, EventArgs args)
	{
		PushData();
	}

	private void ControlPropertyChangedHandler(object o, EventArgs args)
	{
		if (datasource_update_mode == DataSourceUpdateMode.OnPropertyChanged)
		{
			PullData();
		}
	}

	private object ParseData(object data, Type data_type)
	{
		ConvertEventArgs convertEventArgs = new ConvertEventArgs(data, data_type);
		OnParse(convertEventArgs);
		if (data_type.IsInstanceOfType(convertEventArgs.Value))
		{
			return convertEventArgs.Value;
		}
		if (convertEventArgs.Value == Convert.DBNull)
		{
			return convertEventArgs.Value;
		}
		if (convertEventArgs.Value == null)
		{
			bool flag = data_type.IsGenericType && !data_type.ContainsGenericParameters && (object)data_type.GetGenericTypeDefinition() == typeof(Nullable<>);
			return (!data_type.IsValueType || flag) ? null : Convert.DBNull;
		}
		return ConvertData(convertEventArgs.Value, data_type);
	}

	private object FormatData(object data)
	{
		ConvertEventArgs convertEventArgs = new ConvertEventArgs(data, data_type);
		OnFormat(convertEventArgs);
		if (data_type.IsInstanceOfType(convertEventArgs.Value))
		{
			return convertEventArgs.Value;
		}
		if (formatting_enabled)
		{
			if ((convertEventArgs.Value == null || convertEventArgs.Value == Convert.DBNull) && null_value != null)
			{
				return null_value;
			}
			if (convertEventArgs.Value is IFormattable && (object)data_type == typeof(string))
			{
				IFormattable formattable = (IFormattable)convertEventArgs.Value;
				return formattable.ToString(format_string, format_info);
			}
		}
		if (convertEventArgs.Value == null && (object)data_type == typeof(object))
		{
			return Convert.DBNull;
		}
		return ConvertData(data, data_type);
	}

	private object ConvertData(object data, Type data_type)
	{
		if (data == null)
		{
			return null;
		}
		TypeConverter converter = TypeDescriptor.GetConverter(data.GetType());
		if (converter != null && converter.CanConvertTo(data_type))
		{
			return converter.ConvertTo(data, data_type);
		}
		converter = TypeDescriptor.GetConverter(data_type);
		if (converter != null && converter.CanConvertFrom(data.GetType()))
		{
			return converter.ConvertFrom(data);
		}
		if (data is IConvertible)
		{
			object obj = Convert.ChangeType(data, data_type);
			if (data_type.IsInstanceOfType(obj))
			{
				return obj;
			}
		}
		return null;
	}

	private void FireBindingComplete(BindingCompleteContext context, Exception exc, string error_message)
	{
		BindingCompleteEventArgs bindingCompleteEventArgs = new BindingCompleteEventArgs(this, (exc != null) ? BindingCompleteState.Exception : BindingCompleteState.Success, context);
		if (exc != null)
		{
			bindingCompleteEventArgs.SetException(exc);
			bindingCompleteEventArgs.SetErrorText(error_message);
		}
		OnBindingComplete(bindingCompleteEventArgs);
	}
}
