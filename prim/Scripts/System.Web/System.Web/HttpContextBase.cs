using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Caching;
using System.Web.Profile;
using System.Web.SessionState;

namespace System.Web;

/// <summary>Serves as the base class for classes that contain HTTP-specific information about an individual HTTP request.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class HttpContextBase : IServiceProvider
{
	/// <summary>When overridden in a derived class, gets an array of errors (if any) that accumulated when an HTTP request was being processed.</summary>
	/// <returns>An array of <see cref="T:System.Exception" /> objects for the current HTTP request, or <see langword="null" /> if no errors accumulated during the HTTP request processing.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Exception[] AllErrors
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current HTTP request.</summary>
	/// <returns>The application state object for the current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpApplicationStateBase Application
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request.</summary>
	/// <returns>The object for the current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpApplication ApplicationInstance
	{
		get
		{
			NotImplemented();
			return null;
		}
		set
		{
			NotImplemented();
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
	/// <returns>The cache for the current application domain.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Cache Cache
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.IHttpHandler" /> object that represents the handler that is currently executing.</summary>
	/// <returns>An object that represents the currently executing handler. </returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IHttpHandler CurrentHandler
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets a <see cref="T:System.Web.RequestNotification" /> value that indicates the <see cref="T:System.Web.HttpApplication" /> event that is currently processing. </summary>
	/// <returns>One of the <see cref="T:System.Web.RequestNotification" /> values.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual RequestNotification CurrentNotification
	{
		get
		{
			NotImplemented();
			return (RequestNotification)0;
		}
	}

	/// <summary>When overridden in a derived class, gets the first error (if any) that accumulated when an HTTP request was being processed.</summary>
	/// <returns>The first exception for the current HTTP request/response process, or <see langword="null" /> if no errors accumulated during the HTTP request processing.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Exception Error
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets or sets the <see cref="T:System.Web.IHttpHandler" /> object that is responsible for processing the HTTP request.</summary>
	/// <returns>The object that is responsible for processing the HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IHttpHandler Handler
	{
		get
		{
			NotImplemented();
			return null;
		}
		set
		{
			NotImplemented();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether custom errors are enabled for the current HTTP request.</summary>
	/// <returns>
	///     <see langword="true" /> if custom errors are enabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsCustomErrorEnabled
	{
		get
		{
			NotImplemented();
			return false;
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the current HTTP request is in debug mode.</summary>
	/// <returns>
	///     <see langword="true" /> if the request is in debug mode; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsDebuggingEnabled
	{
		get
		{
			NotImplemented();
			return false;
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether an <see cref="T:System.Web.HttpApplication" /> event has finished processing. </summary>
	/// <returns>
	///     <see langword="true" /> if the event has finished processing; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsPostNotification
	{
		get
		{
			NotImplemented();
			return false;
		}
	}

	/// <summary>When overridden in a derived class, gets a key/value collection that can be used to organize and share data between a module and a handler during an HTTP request.</summary>
	/// <returns>A key/value collection that provides access to an individual value in the collection by using a specified key.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IDictionary Items
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.IHttpHandler" /> object for the parent handler.</summary>
	/// <returns>An <see cref="T:System.Web.IHttpHandler" /> object that represents the parent handler, or <see langword="null" /> if no parent handler was found.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IHttpHandler PreviousHandler
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.Profile.ProfileBase" /> object for the current user profile.</summary>
	/// <returns>If the profile properties are defined in the application configuration file and profiles are enabled for the application, an object that represents the current user profile; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual ProfileBase Profile
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.</summary>
	/// <returns>The current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpRequestBase Request
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpResponse" /> object for the current HTTP response.</summary>
	/// <returns>The current HTTP response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpResponseBase Response
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.HttpServerUtility" /> object that provides methods that are used when Web requests are being processed.</summary>
	/// <returns>The server utility object for the current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpServerUtilityBase Server
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current HTTP request.</summary>
	/// <returns>The session-state object for the current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpSessionStateBase Session
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets or sets a value that specifies whether the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object should skip the authorization check for the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Web.Security.UrlAuthorizationModule" /> should skip the authorization check; otherwise, <see langword="false" />. </returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SkipAuthorization
	{
		get
		{
			NotImplemented();
			return false;
		}
		set
		{
			NotImplemented();
		}
	}

	/// <summary>When overridden in a derived class, gets the initial timestamp of the current HTTP request.</summary>
	/// <returns>The timestamp of the current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual DateTime Timestamp
	{
		get
		{
			NotImplemented();
			return DateTime.MinValue;
		}
	}

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Web.TraceContext" /> object for the current HTTP response.</summary>
	/// <returns>The trace object for the current HTTP response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual TraceContext Trace
	{
		get
		{
			NotImplemented();
			return null;
		}
	}

	/// <summary>When overridden in a derived class, gets or sets security information for the current HTTP request.</summary>
	/// <returns>An object that contains security information for the current HTTP request.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IPrincipal User
	{
		get
		{
			NotImplemented();
			return null;
		}
		set
		{
			NotImplemented();
		}
	}

	private void NotImplemented()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, adds an exception to the exception collection for the current HTTP request.</summary>
	/// <param name="errorInfo">The exception to add to the exception collection.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddError(Exception errorInfo)
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, clears all errors for the current HTTP request.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void ClearError()
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
	/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <returns>The requested application-level resource object, or <see langword="null" /> if no matching resource object is found.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetGlobalResourceObject(string classKey, string resourceKey)
	{
		NotImplemented();
		return null;
	}

	/// <summary>When overridden in a derived class, gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource.</param>
	/// <returns>The requested application-level resource object, which is localized for the specified culture, or <see langword="null" /> if no matching resource object is found.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
	{
		NotImplemented();
		return null;
	}

	/// <summary>When overridden in a derived class, gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
	/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <returns>The requested page-level resource object, or <see langword="null" /> if no matching resource object is found.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetLocalResourceObject(string virtualPath, string resourceKey)
	{
		NotImplemented();
		return null;
	}

	/// <summary>When overridden in a derived class, gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="virtualPath">A string that represents the <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property of the local resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource object.</param>
	/// <returns>The requested local resource object, which is localized for the specified culture, or <see langword="null" /> if no matching resource object is found.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
	{
		NotImplemented();
		return null;
	}

	/// <summary>When overridden in a derived class, gets the specified configuration section of the current application's default configuration. </summary>
	/// <param name="sectionName">The configuration section path (in XPath format) and the configuration element name.</param>
	/// <returns>The specified section, or <see langword="null" /> if the section does not exist.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetSection(string sectionName)
	{
		NotImplemented();
		return null;
	}

	/// <summary>When overridden in a derived class, returns an object for the current service type.</summary>
	/// <param name="serviceType">The type of service object to get.</param>
	/// <returns>The current service type, or <see langword="null" /> if no service is found.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object GetService(Type serviceType)
	{
		NotImplemented();
		return null;
	}

	/// <summary>When overridden in a derived class, specifies a handler for the request.</summary>
	/// <param name="handler">The object that should process the request.</param>
	/// <exception cref="T:System.NotImplementedException">A derived type fails to implement this method.</exception>
	public virtual void RemapHandler(IHttpHandler handler)
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, rewrites the URL by using the specified path.</summary>
	/// <param name="path">The replacement path.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RewritePath(string path)
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, rewrites the URL by using the specified path and a value that specifies whether the virtual path for server resources is modified.</summary>
	/// <param name="path">The replacement path.</param>
	/// <param name="rebaseClientPath">
	///       <see langword="true" /> to reset the virtual path; <see langword="false" /> to keep the virtual path unchanged.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RewritePath(string path, bool rebaseClientPath)
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, rewrites the URL by using the specified path, path information, and query string information.</summary>
	/// <param name="filePath">The replacement path.</param>
	/// <param name="pathInfo">Additional path information for a resource.</param>
	/// <param name="queryString">The request query string.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RewritePath(string filePath, string pathInfo, string queryString)
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, rewrites the URL by using the specified path, path information, query string information, and a value that specifies whether the client file path is set to the rewrite path. </summary>
	/// <param name="filePath">The replacement path.</param>
	/// <param name="pathInfo">Additional path information for a resource.</param>
	/// <param name="queryString">The request query string.</param>
	/// <param name="setClientFilePath">
	///       <see langword="true" /> to set the file path used for client resources to the value of the <paramref name="filePath" /> parameter; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
	{
		NotImplemented();
	}

	/// <summary>When overridden in a derived class, sets the type of session state behavior that is required to support an HTTP request.</summary>
	/// <param name="sessionStateBehavior">One of the enumeration values that specifies what type of session state behavior is required.</param>
	/// <exception cref="T:System.NotImplementedException">A derived type fails to implement this method.</exception>
	public virtual void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
	{
		NotImplemented();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpContextBase()
	{
	}
}
