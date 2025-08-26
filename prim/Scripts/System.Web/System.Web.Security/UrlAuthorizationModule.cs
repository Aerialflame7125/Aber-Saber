using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Verifies that the user has permission to access the URL requested. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class UrlAuthorizationModule : IHttpModule
{
	/// <summary>Creates an instance of the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> class.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public UrlAuthorizationModule()
	{
	}

	/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.UrlAuthorizationModule" />.</summary>
	public void Dispose()
	{
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object.</summary>
	/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
	public void Init(HttpApplication app)
	{
		app.AuthorizeRequest += OnAuthorizeRequest;
	}

	private void OnAuthorizeRequest(object sender, EventArgs args)
	{
		HttpApplication httpApplication = (HttpApplication)sender;
		HttpContext context = httpApplication.Context;
		if (context != null && !context.SkipAuthorization)
		{
			HttpRequest request = context.Request;
			if (!((AuthorizationSection)WebConfigurationManager.GetSection("system.web/authorization", request.Path, context)).IsValidUser(context.User, request.HttpMethod))
			{
				HttpException ex = new HttpException(401, "Unauthorized");
				HttpResponse response = context.Response;
				response.StatusCode = 401;
				response.Write(ex.GetHtmlErrorMessage());
				httpApplication.CompleteRequest();
			}
		}
	}

	/// <summary>Determines whether the user has access to the requested file.</summary>
	/// <param name="virtualPath">The virtual path to the file.</param>
	/// <param name="user">An <see cref="T:System.Security.Principal.IPrincipal" /> object representing the current user.</param>
	/// <param name="verb">The HTTP verb used to make the request.</param>
	/// <returns>
	///     <see langword="true" /> if the current user can access the file; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is <see langword="null" />.- or -
	///         <paramref name="user" /> is <see langword="null" />.- or -
	///         <paramref name="verb" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="virtualPath" /> is outside of the application root path.</exception>
	public static bool CheckUrlAccessForPrincipal(string virtualPath, IPrincipal user, string verb)
	{
		return ((AuthorizationSection)WebConfigurationManager.GetSection("system.web/authorization", virtualPath))?.IsValidUser(user, verb) ?? true;
	}

	internal static void ReportUrlAuthorizationFailure(HttpContext context, object webEventSource)
	{
		context.Response.StatusCode = 401;
		context.Response.Write(new HttpException(401, "Unauthorized").GetHtmlErrorMessage());
		context.ApplicationInstance.CompleteRequest();
	}
}
