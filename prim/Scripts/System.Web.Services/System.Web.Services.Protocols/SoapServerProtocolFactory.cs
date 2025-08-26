using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>The .NET Framework creates an instance of the <see cref="T:System.Web.Services.Protocols.SoapServerProtocolFactory" /> class to process XML Web service requests.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public class SoapServerProtocolFactory : ServerProtocolFactory
{
	/// <summary>Returns a <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by <paramref name="request" />.</summary>
	/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> that represents the Web service request.</param>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> that can be used to process the XML Web service request specified by <paramref name="request" />.</returns>
	protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
	{
		if (request.PathInfo.Length > 0)
		{
			return null;
		}
		if (request.HttpMethod != "POST")
		{
			return new UnsupportedRequestProtocol(405);
		}
		return new SoapServerProtocol();
	}

	/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.SoapServerProtocolFactory" />.</summary>
	public SoapServerProtocolFactory()
	{
	}
}
