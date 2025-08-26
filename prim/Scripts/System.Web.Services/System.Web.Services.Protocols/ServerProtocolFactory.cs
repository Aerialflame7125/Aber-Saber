using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

/// <summary>The .NET Framework uses classes that are derived from the <see cref="T:System.Web.Services.Protocols.ServerProtocolFactory" /> class to process XML Web service requests.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public abstract class ServerProtocolFactory
{
	internal ServerProtocol Create(Type type, HttpContext context, HttpRequest request, HttpResponse response, out bool abortProcessing)
	{
		ServerProtocol serverProtocol = null;
		abortProcessing = false;
		serverProtocol = CreateIfRequestCompatible(request);
		try
		{
			serverProtocol?.SetContext(type, context, request, response);
			return serverProtocol;
		}
		catch (Exception ex)
		{
			abortProcessing = true;
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "Create", ex);
			}
			if (serverProtocol != null && !serverProtocol.WriteException(ex, serverProtocol.Response.OutputStream))
			{
				throw new InvalidOperationException(Res.GetString("UnableToHandleRequest0"), ex);
			}
			return null;
		}
	}

	/// <summary>Returns a <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by the <paramref name="request" /> parameter.</summary>
	/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> that represents the Web service request.</param>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by the <paramref name="request" /> parameter.</returns>
	protected abstract ServerProtocol CreateIfRequestCompatible(HttpRequest request);

	/// <summary>The constructor for <see cref="T:System.Web.Services.Protocols.ServerProtocolFactory" />.</summary>
	protected ServerProtocolFactory()
	{
	}
}
