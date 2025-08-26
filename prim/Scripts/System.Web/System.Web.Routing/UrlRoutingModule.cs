using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.Security;

namespace System.Web.Routing;

/// <summary>Matches a URL request to a defined route.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class UrlRoutingModule : IHttpModule
{
	private static readonly object _contextKey = new object();

	private static readonly object _requestDataKey = new object();

	private RouteCollection _routeCollection;

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

	/// <summary>Disposes of the resources (other than memory) that are used by the module.</summary>
	protected virtual void Dispose()
	{
	}

	/// <summary>Initializes a module and prepares it to handle requests.</summary>
	/// <param name="application">An object that provides access to the methods, properties, and events common to all application objects in an ASP.NET application.</param>
	protected virtual void Init(HttpApplication application)
	{
		if (application.Context.Items[_contextKey] == null)
		{
			application.Context.Items[_contextKey] = _contextKey;
			application.PostResolveRequestCache += OnApplicationPostResolveRequestCache;
		}
	}

	private void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
	{
		HttpContextBase context = new HttpContextWrapper(((HttpApplication)sender).Context);
		PostResolveRequestCache(context);
	}

	/// <summary>Assigns the HTTP handler for the current request to the context.</summary>
	/// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Routing.RouteData.RouteHandler" /> property for the route is <see langword="null" />.</exception>
	[Obsolete("This method is obsolete. Override the Init method to use the PostMapRequestHandler event.")]
	public virtual void PostMapRequestHandler(HttpContextBase context)
	{
	}

	/// <summary>Matches the HTTP request to a route, retrieves the handler for that route, and sets the handler as the HTTP handler for the current request.</summary>
	/// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
	public virtual void PostResolveRequestCache(HttpContextBase context)
	{
		RouteData routeData = RouteCollection.GetRouteData(context);
		if (routeData == null)
		{
			return;
		}
		IRouteHandler routeHandler = routeData.RouteHandler;
		if (routeHandler == null)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A RouteHandler must be specified for the selected route.")));
		}
		if (routeHandler is StopRoutingHandler)
		{
			return;
		}
		RequestContext requestContext = new RequestContext(context, routeData);
		context.Request.RequestContext = requestContext;
		IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
		if (httpHandler == null)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method."), routeHandler.GetType()));
		}
		if (httpHandler is UrlAuthFailureHandler)
		{
			if (!FormsAuthenticationModule.FormsAuthRequired)
			{
				throw new HttpException(401, global::SR.GetString("An error occurred while accessing the resources required to serve this request. You might not have permission to view the requested resources."));
			}
			UrlAuthorizationModule.ReportUrlAuthorizationFailure(HttpContext.Current, this);
		}
		else
		{
			context.RemapHandler(httpHandler);
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpModule.Dispose" />.</summary>
	void IHttpModule.Dispose()
	{
		Dispose();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.Routing.UrlRoutingModule.System#Web#IHttpModule#Init(System.Web.HttpApplication)" />.</summary>
	/// <param name="application">An object that provides access to the methods, properties, and events that are common to all application objects in an ASP.NET application.</param>
	void IHttpModule.Init(HttpApplication application)
	{
		Init(application);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.UrlRoutingModule" /> class. </summary>
	public UrlRoutingModule()
	{
	}
}
