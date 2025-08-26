using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Serves as base class for classes that enable you to customize how ASP.NET routing processes a request.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class UrlRoutingHandler : IHttpHandler
{
	private RouteCollection _routeCollection;

	/// <summary>Gets a value that indicates whether another request can use the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> instance.</summary>
	/// <returns>Always <see langword="false" />.</returns>
	protected virtual bool IsReusable => false;

	/// <summary>Gets or sets the collection of defined routes for the ASP.NET application.</summary>
	/// <returns>An object that contains the routes.</returns>
	public RouteCollection RouteCollection
	{
		get
		{
			if (_routeCollection == null)
			{
				_routeCollection = RouteTable.Routes;
			}
			return _routeCollection;
		}
		set
		{
			_routeCollection = value;
		}
	}

	/// <summary>Gets a value that indicates whether another request can use the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> instance.</summary>
	/// <returns>Always <see langword="false" />.</returns>
	bool IHttpHandler.IsReusable => IsReusable;

	/// <summary>Processes an HTTP request that matches a route.</summary>
	/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
	/// <exception cref="T:System.Web.HttpException">The request does not match any route.</exception>
	/// <exception cref="T:System.InvalidOperationException">No handler is defined for the route.</exception>
	protected virtual void ProcessRequest(HttpContext httpContext)
	{
		ProcessRequest(new HttpContextWrapper(httpContext));
	}

	/// <summary>Processes an HTTP request that matches a route.</summary>
	/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
	/// <exception cref="T:System.Web.HttpException">The request does not match any route.</exception>
	/// <exception cref="T:System.InvalidOperationException">No handler is defined for the route.</exception>
	protected virtual void ProcessRequest(HttpContextBase httpContext)
	{
		RouteData routeData = RouteCollection.GetRouteData(httpContext);
		if (routeData == null)
		{
			throw new HttpException(404, global::SR.GetString("The incoming request does not match any route."));
		}
		IRouteHandler routeHandler = routeData.RouteHandler;
		if (routeHandler == null)
		{
			throw new InvalidOperationException(global::SR.GetString("A RouteHandler must be specified for the selected route."));
		}
		RequestContext requestContext = new RequestContext(httpContext, routeData);
		IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
		if (httpHandler == null)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method."), routeHandler.GetType()));
		}
		VerifyAndProcessRequest(httpHandler, httpContext);
	}

	/// <summary>When overridden in a derived class, validates the HTTP handler and performs the steps that are required to process the request.</summary>
	/// <param name="httpHandler">The object that is used to process an HTTP request.</param>
	/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
	protected abstract void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext);

	/// <summary>Processes an HTTP request that matches a route.</summary>
	/// <param name="context">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
	void IHttpHandler.ProcessRequest(HttpContext context)
	{
		ProcessRequest(context);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> class. </summary>
	protected UrlRoutingHandler()
	{
	}
}
