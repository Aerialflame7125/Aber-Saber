using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of a client-side HTTP cookie to a parameter object. The parameter can be used in a parameterized query or command to select, filter, or update data.</summary>
[DefaultProperty("CookieName")]
public class CookieParameter : Parameter
{
	/// <summary>Gets or sets the name of the HTTP cookie that the parameter binds to.</summary>
	/// <returns>A string that identifies the client-side HTTP cookie that the parameter binds to.</returns>
	[DefaultValue("")]
	public string CookieName
	{
		get
		{
			return base.ViewState.GetString("CookieName", string.Empty);
		}
		set
		{
			if (CookieName != value)
			{
				base.ViewState["CookieName"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class.</summary>
	public CookieParameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.CookieParameter" /> from which the current instance is initialized. </param>
	protected CookieParameter(CookieParameter original)
		: base(original)
	{
		CookieName = original.CookieName;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class, using the specified string to identify which HTTP cookie to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public CookieParameter(string name, string cookieName)
		: base(name)
	{
		CookieName = cookieName;
	}

	/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class, using the specified string to identify which HTTP cookie to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
	/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public CookieParameter(string name, TypeCode type, string cookieName)
		: base(name, type)
	{
		CookieName = cookieName;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class that has the specified name and database type and that is bound to the specified HTTP cookie.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="dbType">The database type that the parameter represents. </param>
	/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
	public CookieParameter(string name, DbType dbType, string cookieName)
		: base(name, dbType)
	{
		CookieName = cookieName;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.CookieParameter" /> instance.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.CookieParameter" /> that is an exact duplicate of the current one.</returns>
	protected override Parameter Clone()
	{
		return new CookieParameter(this);
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
	/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Forms page where the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> is used. </param>
	/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is <see langword="null" />, the <see cref="M:System.Web.UI.WebControls.CookieParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		if (context == null || context.Request == null)
		{
			return null;
		}
		return context.Request.Cookies[CookieName]?.Value;
	}
}
