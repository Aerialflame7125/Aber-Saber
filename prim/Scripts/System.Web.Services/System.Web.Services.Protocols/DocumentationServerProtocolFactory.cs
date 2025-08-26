namespace System.Web.Services.Protocols;

internal class DocumentationServerProtocolFactory : ServerProtocolFactory
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
		return new DocumentationServerProtocol();
	}
}
