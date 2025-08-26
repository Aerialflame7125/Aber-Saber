namespace System.Web;

/// <summary>Indicates when events and other life-cycle events occur while a <see cref="T:System.Web.HttpApplication" /> request is being processed.</summary>
[Flags]
public enum RequestNotification
{
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.BeginRequest" /> event was raised for the request and is processing.</summary>
	BeginRequest = 1,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event was raised for the request and is processing.</summary>
	AuthenticateRequest = 2,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event was raised for the request and is processing.</summary>
	AuthorizeRequest = 4,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event was raised for the request and is processing.</summary>
	ResolveRequestCache = 8,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event was raised for the request and is processing.</summary>
	MapRequestHandler = 0x10,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event was raised for the request and is processing.</summary>
	AcquireRequestState = 0x20,
	/// <summary>Indicates a point in the application life cycle just before the handler that processes the request is mapped.</summary>
	PreExecuteRequestHandler = 0x40,
	/// <summary>Indicates that the handler that is mapped to the requested resource is being invoked to process the request.</summary>
	ExecuteRequestHandler = 0x80,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event was raised for the request and is processing.</summary>
	ReleaseRequestState = 0x100,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event was raised for the request and is processing.</summary>
	UpdateRequestCache = 0x200,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.LogRequest" /> event was raised for the request and is processing.</summary>
	LogRequest = 0x400,
	/// <summary>Indicates that the <see cref="E:System.Web.HttpApplication.EndRequest" /> event was raised for the request and is processing.</summary>
	EndRequest = 0x800,
	/// <summary>Indicates that processing of the request is complete and that the response is being sent.</summary>
	SendResponse = 0x20000000
}
