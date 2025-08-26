namespace System.Web.Services.Protocols;

internal class DiscoveryServerProtocolFactory : ServerProtocolFactory
{
	protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
	{
		if (request.PathInfo.Length > 0)
		{
			return null;
		}
		if (request.HttpMethod != "GET")
		{
			return new UnsupportedRequestProtocol(405);
		}
		string text = request.QueryString[null];
		if (text == null)
		{
			text = "";
		}
		if (request.QueryString["schema"] == null && request.QueryString["wsdl"] == null && string.Compare(text, "wsdl", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(text, "disco", StringComparison.OrdinalIgnoreCase) != 0)
		{
			return null;
		}
		return new DiscoveryServerProtocol();
	}
}
