using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Provides a mechanism that data source controls use to bind to application variables, user identities and choices, and other data. Serves as the base class for all ASP.NET parameter types. </summary>
[DefaultProperty("DefaultValue")]
public class Parameter : ICloneable, IStateManager
{
	private StateBag viewState;

	private bool isTrackingViewState;

	private ParameterCollection _owner;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Specifies a default value for the parameter, should the value that the parameter is bound to be uninitialized when the <see cref="M:System.Web.UI.WebControls.Parameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method is called.</summary>
	/// <returns>A string that serves as a default value for the <see cref="T:System.Web.UI.WebControls.Parameter" /> when the value it is bound to cannot be resolved or is uninitialized.</returns>
	[WebCategory("Parameter")]
	[DefaultValue(null)]
	[WebSysDescription("Default value to be used in case value is null.")]
	public string DefaultValue
	{
		get
		{
			return ViewState.GetString("DefaultValue", null);
		}
		set
		{
			if (DefaultValue != value)
			{
				ViewState["DefaultValue"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Indicates whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is used to bind a value to a control, or the control can be used to change the value.</summary>
	/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. <see cref="P:System.Web.UI.WebControls.Parameter.Direction" /> is set to <see cref="F:System.Data.ParameterDirection.Input" /> by default.</returns>
	[WebCategory("Parameter")]
	[DefaultValue("Input")]
	[WebSysDescription("Parameter's direction.")]
	public ParameterDirection Direction
	{
		get
		{
			return (ParameterDirection)ViewState.GetInt("Direction", 1);
		}
		set
		{
			if (Direction != value)
			{
				ViewState["Direction"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets or sets the name of the parameter.</summary>
	/// <returns>The name of the parameter. The default value is <see cref="F:System.String.Empty" />.</returns>
	[WebCategory("Parameter")]
	[DefaultValue("")]
	[WebSysDescription("Parameter's name.")]
	public string Name
	{
		get
		{
			if (ViewState["Name"] is string result)
			{
				return result;
			}
			return string.Empty;
		}
		set
		{
			if (Name != value)
			{
				ViewState["Name"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the value that the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is bound to should be converted to <see langword="null" /> if it is <see cref="F:System.String.Empty" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the value that the <see cref="T:System.Web.UI.WebControls.Parameter" /> is bound to should be converted to <see langword="null" /> when it is <see cref="F:System.String.Empty" />; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebCategory("Parameter")]
	[DefaultValue(true)]
	[WebSysDescription("Checks whether an empty string is treated as a null value.")]
	public bool ConvertEmptyStringToNull
	{
		get
		{
			return ViewState.GetBool("ConvertEmptyStringToNull", def: true);
		}
		set
		{
			if (ConvertEmptyStringToNull != value)
			{
				ViewState["ConvertEmptyStringToNull"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets or sets the database type of the parameter.</summary>
	/// <returns>The database type of the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance. The default value is <see cref="F:System.Data.DbType.Object" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is not in the <see cref="T:System.Data.DbType" /> enumeration.</exception>
	[WebCategory("Parameter")]
	[DefaultValue(DbType.Object)]
	[WebSysDescription("Parameter's DbType.")]
	public DbType DbType
	{
		get
		{
			object obj = ViewState["DbType"];
			if (obj == null)
			{
				return DbType.Object;
			}
			return (DbType)obj;
		}
		set
		{
			if (DbType != value)
			{
				ViewState["DbType"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets or sets the size of the parameter.</summary>
	/// <returns>The size of the parameter expressed as an integer.</returns>
	[DefaultValue(0)]
	public int Size
	{
		get
		{
			return ViewState.GetInt("Size", 0);
		}
		set
		{
			if (Size != value)
			{
				ViewState["Size"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets or sets the type of the parameter.</summary>
	/// <returns>The type of the <see cref="T:System.Web.UI.WebControls.Parameter" />. The default value is <see cref="F:System.TypeCode.Object" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The parameter type is not one of the <see cref="T:System.TypeCode" /> values.</exception>
	[DefaultValue(TypeCode.Empty)]
	[WebCategory("Parameter")]
	[WebSysDescription("Represents type of the parameter.")]
	public TypeCode Type
	{
		get
		{
			return (TypeCode)ViewState.GetInt("Type", 0);
		}
		set
		{
			if (Type != value)
			{
				ViewState["Type"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.Parameter" /> object across multiple requests for the same page.</summary>
	/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view-state information.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected StateBag ViewState
	{
		get
		{
			if (viewState == null)
			{
				viewState = new StateBag();
				if (IsTrackingViewState)
				{
					viewState.TrackViewState();
				}
			}
			return viewState;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => isTrackingViewState;

	/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class.</summary>
	public Parameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class with the values of the original, specified instance.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.Parameter" /> instance from which the current instance is initialized. </param>
	protected Parameter(Parameter original)
	{
		if (original == null)
		{
			throw new NullReferenceException(".NET emulation");
		}
		DefaultValue = original.DefaultValue;
		Direction = original.Direction;
		ConvertEmptyStringToNull = original.ConvertEmptyStringToNull;
		Type = original.Type;
		Name = original.Name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name.</summary>
	/// <param name="name">The name of the parameter. </param>
	public Parameter(string name)
	{
		Name = name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name and type.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">A <see cref="T:System.TypeCode" /> that describes the type of the parameter. </param>
	public Parameter(string name, TypeCode type)
		: this(name)
	{
		Type = type;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name, the specified type, and the specified string for its <see cref="P:System.Web.UI.WebControls.Parameter.DefaultValue" /> property.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">A <see cref="T:System.TypeCode" /> that describes the type of the parameter. </param>
	/// <param name="defaultValue">A string that serves as a default value for the parameter, if the <see cref="T:System.Web.UI.WebControls.Parameter" /> is bound to a value that is not yet initialized when <see cref="M:System.Web.UI.WebControls.Parameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> is called. </param>
	public Parameter(string name, TypeCode type, string defaultValue)
		: this(name, type)
	{
		DefaultValue = defaultValue;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name and database type.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="dbType">The database type of the parameter. </param>
	public Parameter(string name, DbType dbType)
		: this(name)
	{
		DbType = dbType;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Parameter" /> class, using the specified name, the specified database type, and the specified value for its <see cref="P:System.Web.UI.WebControls.Parameter.DefaultValue" /> property.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance. </param>
	/// <param name="dbType">The database type of the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance. </param>
	/// <param name="defaultValue">The default value for the <see cref="T:System.Web.UI.WebControls.Parameter" /> instance, if the <see cref="T:System.Web.UI.WebControls.Parameter" /> is bound to a value that is not yet initialized when <see cref="M:System.Web.UI.WebControls.Parameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> is called. </param>
	public Parameter(string name, DbType dbType, string defaultValue)
		: this(name, dbType)
	{
		DefaultValue = defaultValue;
	}

	/// <summary>Converts a <see cref="T:System.Data.DbType" /> value to an equivalent <see cref="T:System.TypeCode" /> value.</summary>
	/// <param name="dbType">A <see cref="T:System.Data.DbType" /> value to convert to an equivalent <see cref="T:System.TypeCode" /> value.</param>
	/// <returns>A <see cref="T:System.TypeCode" /> value that is equivalent to the specified <see cref="T:System.Data.DbType" /> value.</returns>
	public static TypeCode ConvertDbTypeToTypeCode(DbType dbType)
	{
		switch (dbType)
		{
		case DbType.AnsiString:
		case DbType.String:
		case DbType.AnsiStringFixedLength:
		case DbType.StringFixedLength:
			return TypeCode.String;
		case DbType.Binary:
		case DbType.Guid:
		case DbType.Object:
		case DbType.Xml:
		case DbType.DateTimeOffset:
			return TypeCode.Object;
		case DbType.Byte:
			return TypeCode.Byte;
		case DbType.Boolean:
			return TypeCode.Boolean;
		case DbType.Currency:
		case DbType.Decimal:
		case DbType.VarNumeric:
			return TypeCode.Decimal;
		case DbType.Date:
		case DbType.DateTime:
		case DbType.Time:
		case DbType.DateTime2:
			return TypeCode.DateTime;
		case DbType.Double:
			return TypeCode.Double;
		case DbType.Int16:
			return TypeCode.Int16;
		case DbType.Int32:
			return TypeCode.Int32;
		case DbType.Int64:
			return TypeCode.Int64;
		case DbType.SByte:
			return TypeCode.SByte;
		case DbType.Single:
			return TypeCode.Single;
		case DbType.UInt16:
			return TypeCode.UInt16;
		case DbType.UInt32:
			return TypeCode.UInt32;
		case DbType.UInt64:
			return TypeCode.UInt64;
		default:
			return TypeCode.Object;
		}
	}

	/// <summary>Converts a <see cref="T:System.TypeCode" /> value to an equivalent <see cref="T:System.Data.DbType" /> value.</summary>
	/// <param name="typeCode">The <see cref="T:System.TypeCode" /> value to convert to an equivalent <see cref="T:System.Data.DbType" /> value.</param>
	/// <returns>A <see cref="T:System.Data.DbType" /> value that is equivalent to the specified <see cref="T:System.TypeCode" /> value.</returns>
	public static DbType ConvertTypeCodeToDbType(TypeCode typeCode)
	{
		switch (typeCode)
		{
		case TypeCode.Empty:
		case TypeCode.Object:
		case TypeCode.DBNull:
			return DbType.Object;
		case TypeCode.Boolean:
			return DbType.Boolean;
		case TypeCode.Char:
			return DbType.StringFixedLength;
		case TypeCode.SByte:
			return DbType.SByte;
		case TypeCode.Byte:
			return DbType.Byte;
		case TypeCode.Int16:
			return DbType.Int16;
		case TypeCode.UInt16:
			return DbType.UInt16;
		case TypeCode.Int32:
			return DbType.Int32;
		case TypeCode.UInt32:
			return DbType.UInt32;
		case TypeCode.Int64:
			return DbType.Int64;
		case TypeCode.UInt64:
			return DbType.UInt64;
		case TypeCode.Single:
			return DbType.Single;
		case TypeCode.Double:
			return DbType.Double;
		case TypeCode.Decimal:
			return DbType.Decimal;
		case TypeCode.DateTime:
			return DbType.DateTime;
		case TypeCode.String:
			return DbType.String;
		default:
			return DbType.Object;
		}
	}

	/// <summary>Gets the <see cref="T:System.Data.DbType" /> value that is equivalent to the CLR type of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</summary>
	/// <returns>The <see cref="T:System.Data.DbType" /> value that is equivalent to the CLR type of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.Parameter.DbType" /> property is already set to a value other than <see cref="F:System.Data.DbType.Object" />.</exception>
	public DbType GetDatabaseType()
	{
		if (DbType != DbType.Object)
		{
			throw new InvalidOperationException("The DbType property is already set to a value other than DbType.Object.");
		}
		return ConvertTypeCodeToDbType(Type);
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Parameter" /> that is an exact duplicate of the current one.</returns>
	protected virtual Parameter Clone()
	{
		return new Parameter(this);
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.ParameterCollection.OnParametersChanged(System.EventArgs)" /> method of the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> collection that contains the <see cref="T:System.Web.UI.WebControls.Parameter" /> object.</summary>
	protected void OnParameterChanged()
	{
		if (_owner != null)
		{
			_owner.CallOnParameterChanged();
		}
	}

	/// <summary>Restores the data source view's previously saved view state.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.Parameter" /> state to restore. </param>
	protected virtual void LoadViewState(object savedState)
	{
		ViewState.LoadViewState(savedState);
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view state since the time the page was posted back to the server.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> view state. If there is no view state associated with the object, this method returns <see langword="null" />.</returns>
	protected virtual object SaveViewState()
	{
		return ViewState.SaveViewState();
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.Parameter" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> object and persisted across requests for the same page.</summary>
	protected virtual void TrackViewState()
	{
		isTrackingViewState = true;
		if (viewState != null)
		{
			viewState.TrackViewState();
		}
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.Parameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Parameter" /> that is a copy of the current object.</returns>
	object ICloneable.Clone()
	{
		return Clone();
	}

	/// <summary>Restores the data source view's previously saved view state.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.WebControls.Parameter" /> state to restore. </param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view state since the time the page was posted back to the server.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.Parameter" /> object's view state. If there is no view state associated with the object, this method returns <see langword="null" />.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.Parameter" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> object and persisted across requests for the same page.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
	/// <returns>A string representation of the value of this instance.</returns>
	public override string ToString()
	{
		return Name;
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.Parameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> the parameter is bound to. If the parameter is not bound to a control, the <paramref name="control" /> parameter is ignored. </param>
	/// <returns>An <see langword="object" /> that represents the updated and current value of the parameter.</returns>
	protected internal virtual object Evaluate(HttpContext context, Control control)
	{
		return null;
	}

	internal void UpdateValue(HttpContext context, Control control)
	{
		object objA = ViewState["ParameterValue"];
		object obj = Evaluate(context, control);
		if (!object.Equals(objA, obj))
		{
			ViewState["ParameterValue"] = obj;
			OnParameterChanged();
		}
	}

	internal object GetValue(HttpContext context, Control control)
	{
		UpdateValue(context, control);
		object obj = ConvertValue(ViewState["ParameterValue"]);
		if (obj == null)
		{
			obj = ConvertValue(DefaultValue);
		}
		return obj;
	}

	internal object ConvertValue(object val)
	{
		if (val == null)
		{
			return null;
		}
		if (ConvertEmptyStringToNull && val.Equals(string.Empty))
		{
			return null;
		}
		if (Type == TypeCode.Empty)
		{
			return val;
		}
		return Convert.ChangeType(val, Type);
	}

	/// <summary>Marks the <see cref="T:System.Web.UI.WebControls.Parameter" /> object so its state will be recorded in view state.</summary>
	protected internal virtual void SetDirty()
	{
		ViewState.SetDirty(dirty: true);
	}

	internal void SetOwnerCollection(ParameterCollection own)
	{
		_owner = own;
	}
}
