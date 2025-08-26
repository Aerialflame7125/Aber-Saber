using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of a session variable to a parameter object. </summary>
[DefaultProperty("SessionField")]
public class SessionParameter : Parameter
{
	/// <summary>Gets or sets the name of the session variable that the parameter binds to.</summary>
	/// <returns>A string that identifies the <see cref="T:System.Web.SessionState.HttpSessionState" /> that the parameter binds to.</returns>
	[DefaultValue("")]
	[WebCategory("Parameter")]
	public string SessionField
	{
		get
		{
			if (base.ViewState["SessionField"] is string result)
			{
				return result;
			}
			return "";
		}
		set
		{
			if (SessionField != value)
			{
				base.ViewState["SessionField"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class.</summary>
	public SessionParameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.SessionParameter" /> from which the current instance is initialized. </param>
	protected SessionParameter(SessionParameter original)
		: base(original)
	{
		SessionField = original.SessionField;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class, using the specified string to identify which session state name/value pair to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="sessionField">The name of the <see cref="T:System.Web.SessionState.HttpSessionState" /> name/value pair that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public SessionParameter(string name, string sessionField)
		: base(name)
	{
		SessionField = sessionField;
	}

	/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class, using the specified string to identify which session state name/value pair to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
	/// <param name="sessionField">The name of the <see cref="T:System.Web.SessionState.HttpSessionState" /> name/value pair that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public SessionParameter(string name, TypeCode type, string sessionField)
		: base(name, type)
	{
		SessionField = sessionField;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> class, by using the specified name and type, and binding the parameter to the specified session state name/value pair. This constructor is for database types.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="dbType">The database type that the parameter represents.</param>
	/// <param name="sessionField">The name of the <see cref="T:System.Web.SessionState.HttpSessionState" /> name/value pair that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public SessionParameter(string name, DbType dbType, string sessionField)
		: base(name, dbType)
	{
		SessionField = sessionField;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.SessionParameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.SessionParameter" /> that is an exact duplicate of the current one.</returns>
	protected override Parameter Clone()
	{
		return new SessionParameter(this);
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
	/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Forms page where the <see cref="T:System.Web.UI.WebControls.SessionParameter" /> is used. </param>
	/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is <see langword="null" />, the <see cref="M:System.Web.UI.WebControls.SessionParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns <see langword="null" />.</returns>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		if (context == null || context.Session == null)
		{
			return null;
		}
		return context.Session[SessionField];
	}
}
