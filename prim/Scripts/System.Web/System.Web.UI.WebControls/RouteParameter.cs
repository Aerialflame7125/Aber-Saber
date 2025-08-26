using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls;

/// <summary>Binds the value of a URL segment to a parameter object.</summary>
[DefaultProperty("RouteKey")]
public class RouteParameter : Parameter
{
	private string routeKey;

	/// <summary>Gets or sets the name of the route segment from which to retrieve the value for the route parameter.</summary>
	/// <returns>The name of the route segment that contains the value for the parameter.</returns>
	[DefaultValue("")]
	public string RouteKey
	{
		get
		{
			return routeKey;
		}
		set
		{
			routeKey = value ?? string.Empty;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class. </summary>
	public RouteParameter()
	{
		RouteKey = string.Empty;
		base.Name = string.Empty;
		base.Type = TypeCode.Empty;
		base.Direction = ParameterDirection.Input;
		base.DefaultValue = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the values of the specified instance. </summary>
	/// <param name="original">An object from which the current instance is initialized.</param>
	protected RouteParameter(RouteParameter original)
		: base(original)
	{
		RouteKey = original.RouteKey;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name for the parameter and the specified key for route data. </summary>
	/// <param name="name">The name of the parameter instance.</param>
	/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
	public RouteParameter(string name, string routeKey)
		: base(name)
	{
		RouteKey = routeKey;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name and database type for the parameter, and by using the specified key for the route data. </summary>
	/// <param name="name">The name of the parameter instance.</param>
	/// <param name="dbType">The database type of the parameter instance.</param>
	/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
	public RouteParameter(string name, DbType dbType, string routeKey)
		: base(name, dbType)
	{
		RouteKey = routeKey;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name and type for the parameter, and by using the specified key for the route data. </summary>
	/// <param name="name">The name of the parameter instance.</param>
	/// <param name="type">The type that the parameter represents.</param>
	/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
	public RouteParameter(string name, TypeCode type, string routeKey)
		: base(name, type)
	{
		RouteKey = routeKey;
	}

	/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.RouteParameter" /> instance.</summary>
	/// <returns>An object that is a duplicate of the current one.</returns>
	protected override Parameter Clone()
	{
		return new RouteParameter(this);
	}

	/// <summary>Evaluates the request URL and returns the value of the parameter.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> instance of the request.</param>
	/// <param name="control">The control that the parameter is bound to.</param>
	/// <returns>The current value of the parameter.</returns>
	protected internal override object Evaluate(HttpContext context, Control control)
	{
		if (context == null || control == null)
		{
			return null;
		}
		return (control.Page ?? throw new NullReferenceException(".NET emulation")).RouteData?.Values[RouteKey];
	}
}
