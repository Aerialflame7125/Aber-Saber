namespace System.Web.Services.Protocols;

internal class HttpGetServerProtocol : HttpServerProtocol
{
	internal HttpGetServerProtocol()
		: base(hasInputPayload: false)
	{
	}
}
