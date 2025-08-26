namespace System.Web.Services.Discovery;

internal class InvalidContentTypeException : Exception
{
	private string contentType;

	internal string ContentType => contentType;

	internal InvalidContentTypeException(string message, string contentType)
		: base(message)
	{
		this.contentType = contentType;
	}
}
