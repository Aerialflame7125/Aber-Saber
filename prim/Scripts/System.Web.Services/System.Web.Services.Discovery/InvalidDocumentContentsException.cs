namespace System.Web.Services.Discovery;

internal class InvalidDocumentContentsException : Exception
{
	internal InvalidDocumentContentsException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
