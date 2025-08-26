using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Services.Protocols;
using System.Web.SessionState;

namespace System.Web.Services;

/// <summary>Defines the optional base class for XML Web services, which provides direct access to common ASP.NET objects, such as application and session state.</summary>
public class WebService : MarshalByValueComponent
{
	private HttpContext context;

	internal static readonly string SoapVersionContextSlot = "WebServiceSoapVersion";

	/// <summary>Gets the application object for the current HTTP request.</summary>
	/// <returns>An <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Description("The ASP.NET application object for the current request.")]
	public HttpApplicationState Application
	{
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		get
		{
			return Context.Application;
		}
	}

	/// <summary>Gets the ASP.NET <see cref="T:System.Web.HttpContext" /> for the current request, which encapsulates all HTTP-specific context used by the HTTP server to process Web requests.</summary>
	/// <returns>The ASP.NET <see cref="T:System.Web.HttpContext" /> for the current request.</returns>
	/// <exception cref="T:System.Exception">
	///         <paramref name="Context" /> is <see langword="null" />. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("WebServiceContext")]
	public HttpContext Context
	{
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		get
		{
			PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
			if (context == null)
			{
				context = HttpContext.Current;
			}
			if (context == null)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingHelpContext"));
			}
			return context;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> instance for the current request.</summary>
	/// <returns>An <see cref="T:System.Web.SessionState.HttpSessionState" /> representing the ASP.NET session state object for the current session.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("WebServiceSession")]
	public HttpSessionState Session
	{
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		get
		{
			return Context.Session;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> for the current request.</summary>
	/// <returns>An <see cref="T:System.Web.HttpServerUtility" /> object.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("WebServiceServer")]
	public HttpServerUtility Server
	{
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		get
		{
			return Context.Server;
		}
	}

	/// <summary>Gets the ASP.NET server <see cref="P:System.Web.HttpContext.User" /> object. Can be used to authenticate whether a user is authorized to execute the request.</summary>
	/// <returns>A <see cref="T:System.Security.Principal.IPrincipal" /> representing the ASP.NET server <see cref="P:System.Web.HttpContext.User" /> object.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("WebServiceUser")]
	public IPrincipal User
	{
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		get
		{
			return Context.User;
		}
	}

	/// <summary>Gets the version of the SOAP protocol used to make the SOAP request to the XML Web service.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Default" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("WebServiceSoapVersion")]
	[ComVisible(false)]
	public SoapProtocolVersion SoapVersion
	{
		[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
		get
		{
			object obj = Context.Items[SoapVersionContextSlot];
			if (obj != null && obj is SoapProtocolVersion)
			{
				return (SoapProtocolVersion)obj;
			}
			return SoapProtocolVersion.Default;
		}
	}

	internal void SetContext(HttpContext context)
	{
		this.context = context;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.WebService" /> class.</summary>
	public WebService()
	{
	}
}
