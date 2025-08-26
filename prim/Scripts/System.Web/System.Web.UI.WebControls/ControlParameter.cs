using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of a property of a <see cref="T:System.Web.UI.Control" /> to a parameter object. </summary>
[DefaultProperty("ControlID")]
public class ControlParameter : Parameter
{
	/// <summary>Specifies the name of the control that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to.</summary>
	/// <returns>A <see langword="string" /> that represents the name of a Web server control.</returns>
	[WebCategory("Control")]
	[RefreshProperties(RefreshProperties.All)]
	[TypeConverter(typeof(ControlIDConverter))]
	[DefaultValue("")]
	[IDReferenceProperty(typeof(Control))]
	public string ControlID
	{
		get
		{
			return base.ViewState.GetString("ControlID", string.Empty);
		}
		set
		{
			if (ControlID != value)
			{
				base.ViewState["ControlID"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Gets or sets the property name of the control identified by the <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to.</summary>
	/// <returns>A <see langword="string" /> that represents the name of a control's property that the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> binds to.</returns>
	[DefaultValue("")]
	[TypeConverter(typeof(ControlPropertyNameConverter))]
	[WebCategory("Control")]
	public string PropertyName
	{
		get
		{
			return base.ViewState.GetString("PropertyName", string.Empty);
		}
		set
		{
			if (PropertyName != value)
			{
				base.ViewState["PropertyName"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class.</summary>
	public ControlParameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class with values from the specified instance.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.ControlParameter" /> instance from which the current instance is initialized. </param>
	protected ControlParameter(ControlParameter original)
		: base(original)
	{
		ControlID = original.ControlID;
		PropertyName = original.PropertyName;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class, using the specified control name to identify which control to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public ControlParameter(string name, string controlID)
		: base(name)
	{
		ControlID = controlID;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class, using the specified property name and control name to identify which control to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	/// <param name="propertyName">The name of the property on the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public ControlParameter(string name, string controlID, string propertyName)
		: base(name)
	{
		ControlID = controlID;
		PropertyName = propertyName;
	}

	/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class, using the specified property name and control name to identify which control to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
	/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	/// <param name="propertyName">The name of the property of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public ControlParameter(string name, TypeCode type, string controlID, string propertyName)
		: base(name, type)
	{
		ControlID = controlID;
		PropertyName = propertyName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> class by using the specified parameter name, database type, control ID, and property name. </summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="dbType">The data type of the parameter.</param>
	/// <param name="controlID">The name of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />.</param>
	/// <param name="propertyName">The name of the property of the control that the parameter is bound to. The default is <see cref="F:System.String.Empty" />.</param>
	public ControlParameter(string name, DbType dbType, string controlID, string propertyName)
		: base(name, dbType)
	{
		ControlID = controlID;
		PropertyName = propertyName;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.ControlParameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ControlParameter" /> that is an exact duplicate of the current one.</returns>
	protected override Parameter Clone()
	{
		return new ControlParameter(this);
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that the parameter is bound to. </param>
	/// <returns>An <see cref="T:System.Object" /> that represents the updated and current value of the parameter.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property is not set.- or -The <see cref="P:System.Web.UI.WebControls.ControlParameter.PropertyName" /> property is not set and the <see cref="T:System.Web.UI.Control" /> identified by the <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property is not decorated with a <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> attribute. </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="M:System.Web.UI.Control.FindControl(System.String)" /> does not return the specified control.- or -The control identified by the <see cref="P:System.Web.UI.WebControls.ControlParameter.ControlID" /> property does not support the property named by <see cref="P:System.Web.UI.WebControls.ControlParameter.PropertyName" />. </exception>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		if (control == null)
		{
			return null;
		}
		if (control.Page == null)
		{
			return null;
		}
		if (string.IsNullOrEmpty(ControlID))
		{
			throw new ArgumentException("The ControlID property is not set.");
		}
		Control control2 = null;
		for (Control namingContainer = control.NamingContainer; namingContainer != null; namingContainer = namingContainer.NamingContainer)
		{
			control2 = namingContainer.FindControl(ControlID);
			if (control2 != null)
			{
				break;
			}
		}
		if (control2 == null)
		{
			throw new InvalidOperationException("Control '" + ControlID + "' not found.");
		}
		string text = PropertyName;
		if (string.IsNullOrEmpty(text))
		{
			object[] customAttributes = control2.GetType().GetCustomAttributes(typeof(ControlValuePropertyAttribute), inherit: true);
			if (customAttributes.Length == 0)
			{
				throw new ArgumentException("The PropertyName property is not set and the Control identified by the ControlID property is not decorated with a ControlValuePropertyAttribute attribute.");
			}
			text = ((ControlValuePropertyAttribute)customAttributes[0]).Name;
		}
		return DataBinder.Eval(control2, text);
	}
}
