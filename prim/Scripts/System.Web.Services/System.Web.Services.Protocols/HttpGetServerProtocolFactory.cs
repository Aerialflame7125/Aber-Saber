namespace System.Web.Services.Protocols;

internal class HttpGetServerProtocolFactory : ServerProtocolFactory
{
	protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
	{
		if (request.PathInfo.Length < 2)
		{
			return null;
		}
		if (request.HttpMethod != "GET")
		{
			return new UnsupportedRequestProtocol(405);
		}
		return new HttpGetServerProtocol();
	}
}
