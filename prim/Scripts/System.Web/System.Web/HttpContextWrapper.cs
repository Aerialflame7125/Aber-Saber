using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Profile;
using System.Web.SessionState;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that contains HTTP-specific information about an individual HTTP request.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpContextWrapper : HttpContextBase
{
	private HttpContext w;

	/// <summary>Gets an array of errors (if any) that accumulated when an HTTP request was being processed.</summary>
	/// <returns>An array of <see cref="T:System.Exception" /> objects for the current HTTP request, or <see langword="null" /> if no errors accumulated during the HTTP request processing.</returns>
	public override Exception[] AllErrors => w.AllErrors;

	/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current HTTP request.</summary>
	/// <returns>The state object for the current HTTP request.</returns>
	public override HttpApplicationStateBase Application => new HttpApplicationStateWrapper(w.Application);

	/// <summary>Gets or sets the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request.</summary>
	/// <returns>The object for the current HTTP request.</returns>
	public override HttpApplication ApplicationInstance
	{
		get
		{
			return w.ApplicationInstance;
		}
		set
		{
			w.ApplicationInstance = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
	/// <returns>The cache object for the current application domain.</returns>
	public override Cache Cache => w.Cache;

	/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object that represents the handler that is currently executing.</summary>
	/// <returns>An object that represents the handler that is currently executing.</returns>
	public override IHttpHandler CurrentHandler => w.CurrentHandler;

	/// <summary>Gets a <see cref="T:System.Web.RequestNotification" /> value that indicates the current <see cref="T:System.Web.HttpApplication" /> event that is processing.</summary>
	/// <returns>One of the <see cref="T:System.Web.RequestNotification" /> values.</returns>
	public override RequestNotification CurrentNotification => w.CurrentNotification;

	/// <summary>Gets the first error (if any) that accumulated when an HTTP request was being processed.</summary>
	/// <returns>The first exception for the current HTTP request, or <see langword="null" /> if no errors accumulated when the HTTP request was being processed. The default is <see langword="null" />.</returns>
	public override Exception Error => w.Error;

	/// <summary>Gets or sets the <see cref="T:System.Web.IHttpHandler" /> object that is responsible for processing the HTTP request.</summary>
	/// <returns>The object that is responsible for processing the HTTP request.</returns>
	public override IHttpHandler Handler
	{
		get
		{
			return w.Handler;
		}
		set
		{
			w.Handler = value;
		}
	}

	/// <summary>Gets a value that indicates whether custom errors are enabled for the current HTTP request.</summary>
	/// <returns>
	///     <see langword="true" /> if custom errors are enabled; otherwise, <see langword="false" />.</returns>
	public override bool IsCustomErrorEnabled => w.IsCustomErrorEnabled;

	/// <summary>Gets a value that indicates whether the current HTTP request is in debug mode.</summary>
	/// <returns>
	///     <see langword="true" /> if the request is in debug mode; otherwise, <see langword="false" />.</returns>
	public override bool IsDebuggingEnabled => w.IsDebuggingEnabled;

	/// <summary>Gets a value that indicates whether an <see cref="T:System.Web.HttpApplication" /> event has finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the event has finished processing; otherwise, <see langword="false" />.</returns>
	public override bool IsPostNotification => w.IsPostNotification;

	/// <summary>Gets a key/value collection that can be used to organize and share data between a module and a handler during an HTTP request.</summary>
	/// <returns>A key/value collection that provides access to an individual value in the collection by using a specified key.</returns>
	public override IDictionary Items => w.Items;

	/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object for the parent handler.</summary>
	/// <returns>An <see cref="T:System.Web.IHttpHandler" /> object that represents the parent handler, or <see langword="null" /> if no parent handler was found.</returns>
	public override IHttpHandler PreviousHandler => w.PreviousHandler;

	/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileBase" /> object for the current user profile.</summary>
	/// <returns>If profile properties are defined in the application configuration file and profiles are enabled for the application, an object that represents the current user profile; otherwise, <see langword="null" />.</returns>
	public override ProfileBase Profile => w.Profile;

	/// <summary>Gets the <see cref="T:System.Web.HttpRequestBase" /> object for the current HTTP request.</summary>
	/// <returns>The current HTTP request.</returns>
	public override HttpRequestBase Request => new HttpRequestWrapper(w.Request);

	/// <summary>Gets the <see cref="T:System.Web.HttpResponseBase" /> object for the current HTTP response.</summary>
	/// <returns>The current HTTP response.</returns>
	public override HttpResponseBase Response => new HttpResponseWrapper(w.Response);

	/// <summary>Gets the <see cref="T:System.Web.HttpServerUtilityBase" /> object that provides methods that are used when Web requests are being processed.</summary>
	/// <returns>The server utility object for the current HTTP request.</returns>
	public override HttpServerUtilityBase Server => new HttpServerUtilityWrapper(w.Server);

	/// <summary>Gets the <see cref="T:System.Web.HttpSessionStateBase" /> object for the current HTTP request.</summary>
	/// <returns>The session-state object for the current HTTP request.</returns>
	public override HttpSessionStateBase Session
	{
		get
		{
			if (w.Session != null)
			{
				return new HttpSessionStateWrapper(w.Session);
			}
			return null;
		}
	}

	/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object should skip the authorization check for the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Web.Security.UrlAuthorizationModule" /> should skip the authorization check; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SkipAuthorization
	{
		get
		{
			return w.SkipAuthorization;
		}
		set
		{
			w.SkipAuthorization = value;
		}
	}

	/// <summary>Gets the initial timestamp of the current HTTP request.</summary>
	/// <returns>The timestamp of the current HTTP request.</returns>
	public override DateTime Timestamp => w.Timestamp;

	/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current HTTP response.</summary>
	/// <returns>The trace object for the current HTTP response.</returns>
	public override TraceContext Trace => w.Trace;

	/// <summary>Gets or sets security information for the current HTTP request.</summary>
	/// <returns>An object that contains security information for the current HTTP request.</returns>
	public override IPrincipal User
	{
		get
		{
			return w.User;
		}
		set
		{
			w.User = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpContextWrapper" /> class by using the specified context object.</summary>
	/// <param name="httpContext">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpContext" /> is <see langword="null" />.</exception>
	public HttpContextWrapper(HttpContext httpContext)
	{
		if (httpContext == null)
		{
			throw new ArgumentNullException("httpContext");
		}
		w = httpContext;
	}

	/// <summary>Adds an exception to the exception collection for the current HTTP request.</summary>
	/// <param name="errorInfo">The exception to add to the exception collection.</param>
	public override void AddError(Exception errorInfo)
	{
		w.AddError(errorInfo);
	}

	/// <summary>Clears all errors for the current HTTP request.</summary>
	public override void ClearError()
	{
		w.ClearError();
	}

	/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
	/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <returns>The requested application-level resource object, or <see langword="null" /> if no matching resource object is found.</returns>
	public override object GetGlobalResourceObject(string classKey, string resourceKey)
	{
		return HttpContext.GetGlobalResourceObject(classKey, resourceKey);
	}

	/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource.</param>
	/// <returns>The requested application-level resource object, which is localized for the specified culture, or <see langword="null" /> if no matching resource object is found.</returns>
	public override object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
	{
		return HttpContext.GetGlobalResourceObject(classKey, resourceKey, culture);
	}

	/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
	/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <returns>The requested page-level resource object, or <see langword="null" /> if no matching resource object is found.</returns>
	public override object GetLocalResourceObject(string virtualPath, string resourceKey)
	{
		return HttpContext.GetLocalResourceObject(virtualPath, resourceKey);
	}

	/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource object.</param>
	/// <returns>The requested local resource object, which is localized for the specified culture, or <see langword="null" /> if no matching resource object is found.</returns>
	public override object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
	{
		return HttpContext.GetLocalResourceObject(virtualPath, resourceKey, culture);
	}

	/// <summary>Gets the specified configuration section of the current application's default configuration.</summary>
	/// <param name="sectionName">The configuration section path (in XPath format) and the configuration element name.</param>
	/// <returns>The specified section, or <see langword="null" /> if the section does not exist.</returns>
	public override object GetSection(string sectionName)
	{
		return w.GetSection(sectionName);
	}

	/// <summary>Returns an object for the current service type.</summary>
	/// <param name="serviceType">The type of service to get.</param>
	/// <returns>The current service type, or <see langword="null" /> if no service is found.</returns>
	public override object GetService(Type serviceType)
	{
		return ((IServiceProvider)w).GetService(serviceType);
	}

	/// <summary>Enables you to specify a handler for the request.</summary>
	/// <param name="handler">The object that should process the request.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpContextWrapper.RemapHandler(System.Web.IHttpHandler)" /> method was called after the <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event occurred.</exception>
	public override void RemapHandler(IHttpHandler handler)
	{
		w.RemapHandler(handler);
	}

	/// <summary>Rewrites the URL by using the specified path.</summary>
	/// <param name="path">The replacement path.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="path" /> is not in the current application's root directory.</exception>
	public override void RewritePath(string path)
	{
		w.RewritePath(path);
	}

	/// <summary>Rewrites the URL by using the specified path and a value that specifies whether the virtual path for server resources is modified.</summary>
	/// <param name="path">The path to rewrite to.</param>
	/// <param name="rebaseClientPath">
	///       <see langword="true" /> to reset the virtual path; <see langword="false" /> to keep the virtual path unchanged.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="path" /> is not in the current application's root directory.</exception>
	public override void RewritePath(string path, bool rebaseClientPath)
	{
		w.RewritePath(path, rebaseClientPath);
	}

	/// <summary>Rewrites the URL by using the specified path, path information, and query string information.</summary>
	/// <param name="filePath">The replacement path.</param>
	/// <param name="pathInfo">Additional path information for a resource.</param>
	/// <param name="queryString">The request query string.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filePath" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="filePath" /> parameter is not in the current application's root directory.</exception>
	public override void RewritePath(string filePath, string pathInfo, string queryString)
	{
		w.RewritePath(filePath, pathInfo, queryString);
	}

	/// <summary>Rewrites the URL by using the specified path, path information, query string information, and a value that specifies whether the client file path is set to the rewrite path.</summary>
	/// <param name="filePath">The replacement path.</param>
	/// <param name="pathInfo">Additional path information for a resource.</param>
	/// <param name="queryString">The request query string.</param>
	/// <param name="setClientFilePath">
	///       <see langword="true" /> to set the file path used for client resources to the value of the <paramref name="filePath" /> parameter; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="filePath" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="filePath" /> is not in the current application's root directory.</exception>
	public override void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
	{
		w.RewritePath(filePath, pathInfo, queryString, setClientFilePath);
	}

	/// <summary>Sets the type of session state behavior that is required in order to support an HTTP request.</summary>
	/// <param name="sessionStateBehavior">One of the enumeration values that specifies what type of session state behavior is required.</param>
	public override void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
	{
		w.SetSessionStateBehavior(sessionStateBehavior);
	}
}
