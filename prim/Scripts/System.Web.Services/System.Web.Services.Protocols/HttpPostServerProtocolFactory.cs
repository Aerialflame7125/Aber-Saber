namespace System.Web.Services.Protocols;

internal class HttpPostServerProtocolFactory : ServerProtocolFactory
{
	protected override ServerProtocol CreateIfRequestCompatible(HttpRequest request)
	{
		if (request.PathInfo.Length < 2)
		{
			return null;
		}
		if (request.HttpMethod != "POST")
		{
			return new UnsupportedRequestProtocol(405);
		}
		return new HttpPostServerProtocol();
	}
}
