using System.Runtime.Serialization;

namespace System.Web.Configuration.nBrowser;

internal class Exception : System.Exception
{
	public Exception()
	{
	}

	public Exception(string errorMessage)
		: base(errorMessage)
	{
	}

	public Exception(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	protected Exception(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
