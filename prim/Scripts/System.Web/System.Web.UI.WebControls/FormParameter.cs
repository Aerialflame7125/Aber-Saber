using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of an HTTP request <see cref="P:System.Web.HttpRequest.Form" /> field to a parameter object.</summary>
[DefaultProperty("FormField")]
public class FormParameter : Parameter
{
	/// <summary>Gets or sets the name of the form variable that the parameter binds to.</summary>
	/// <returns>A string that identifies the form variable to which the parameter binds.</returns>
	[DefaultValue("")]
	public string FormField
	{
		get
		{
			if (base.ViewState["FormField"] is string result)
			{
				return result;
			}
			return string.Empty;
		}
		set
		{
			if (FormField != value)
			{
				base.ViewState["FormField"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class.</summary>
	public FormParameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.FormParameter" /> instance that the current instance is initialized from. </param>
	protected FormParameter(FormParameter original)
		: base(original)
	{
		FormField = original.FormField;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class, using the specified string to identify which form variable field to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="formField">The name of the form variable that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public FormParameter(string name, string formField)
		: base(name)
	{
		FormField = formField;
	}

	/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class, using the specified string to identify which form variable to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
	/// <param name="formField">The name of the form variable that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public FormParameter(string name, TypeCode type, string formField)
		: base(name, type)
	{
		FormField = formField;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class, using the specified string to identify which form variable field to bind to. </summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="dbType">The database type of the parameter.</param>
	/// <param name="formField">The name of the form variable that the parameter object is bound to. </param>
	public FormParameter(string name, DbType dbType, string formField)
		: base(name, dbType)
	{
		FormField = formField;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.FormParameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FormParameter" /> that is an exact duplicate of the current one.</returns>
	protected override Parameter Clone()
	{
		return new FormParameter(this);
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
	/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the page where the <see cref="T:System.Web.UI.WebControls.FormParameter" /> is used. </param>
	/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is null (<see langword="Nothing" /> in Visual Basic), the <see cref="M:System.Web.UI.WebControls.FormParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		return (context?.Request)?.Form[FormField];
	}
}
