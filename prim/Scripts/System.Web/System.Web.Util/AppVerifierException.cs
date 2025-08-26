using System.Runtime.Serialization;

namespace System.Web.Util;

internal sealed class AppVerifierException : Exception
{
	private readonly AppVerifierErrorCode _errorCode;

	public AppVerifierErrorCode ErrorCode => _errorCode;

	public AppVerifierException(AppVerifierErrorCode errorCode, string message)
		: base(message)
	{
		_errorCode = errorCode;
	}

	private AppVerifierException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
