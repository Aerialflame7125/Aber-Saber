namespace System.Web.Services.Protocols;

internal class HttpPostServerProtocol : HttpServerProtocol
{
	internal HttpPostServerProtocol()
		: base(hasInputPayload: true)
	{
	}
}
