using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of an HTTP request query-string field to a parameter object. </summary>
[DefaultProperty("QueryStringField")]
public class QueryStringParameter : Parameter
{
	/// <summary>Gets or sets the name of the query-string field that the parameter binds to.</summary>
	/// <returns>The name of the query-string field that the parameter binds to.</returns>
	[DefaultValue("")]
	public string QueryStringField
	{
		get
		{
			if (base.ViewState["QueryStringField"] is string result)
			{
				return result;
			}
			return "";
		}
		set
		{
			if (QueryStringField != value)
			{
				base.ViewState["QueryStringField"] = value;
				OnParameterChanged();
			}
		}
	}

	/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class.</summary>
	public QueryStringParameter()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the values of the instance that is specified by the <paramref name="original" /> parameter.</summary>
	/// <param name="original">A <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> instance from which the current instance is initialized. </param>
	protected QueryStringParameter(QueryStringParameter original)
		: base(original)
	{
		QueryStringField = original.QueryStringField;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the specified string to identify which query-string field to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="queryStringField">The name of the query-string field that the parameter object is bound to. The default is an empty string (""). </param>
	public QueryStringParameter(string name, string queryStringField)
		: base(name)
	{
		QueryStringField = queryStringField;
	}

	/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the specified string to identify which query-string field to bind to.</summary>
	/// <param name="name">The name of the parameter. </param>
	/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
	/// <param name="queryStringField">The name of the query-string field that the parameter object is bound to. The default is an empty string (""). </param>
	public QueryStringParameter(string name, TypeCode type, string queryStringField)
		: base(name, type)
	{
		QueryStringField = queryStringField;
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> class, using the specified query-string field and the data type of the parameter.</summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="dbType">The data type of the parameter.</param>
	/// <param name="queryStringField">The name of the query-string field that the parameter object is bound to. The default is an empty string ("").</param>
	public QueryStringParameter(string name, DbType dbType, string queryStringField)
		: base(name, dbType)
	{
		QueryStringField = queryStringField;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> instance.</summary>
	/// <returns>A duplicate of the current instance.</returns>
	protected override Parameter Clone()
	{
		return new QueryStringParameter(this);
	}

	/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> object.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> instance of the request.</param>
	/// <param name="control">A Web server control that is associated with the ASP.NET Web page where the <see cref="T:System.Web.UI.WebControls.QueryStringParameter" /> object is used.
	///       Note   This parameter is not used.</param>
	/// <returns>An object that represents the current value of the parameter. If the context or the request is <see langword="null" />, the <see cref="M:System.Web.UI.WebControls.QueryStringParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns <see langword="null" />. </returns>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		if (context == null || context.Request == null)
		{
			return null;
		}
		return context.Request.QueryString[QueryStringField];
	}
}
