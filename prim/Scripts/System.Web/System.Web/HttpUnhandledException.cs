using System.Security.Permissions;

namespace System.Web;

/// <summary>The exception that is thrown when a generic exception occurs.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpUnhandledException : HttpException
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUnhandledException" /> class.</summary>
	public HttpUnhandledException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUnhandledException" /> class with the specified error messages.</summary>
	/// <param name="message">The message displayed to the client when the exception is thrown. </param>
	public HttpUnhandledException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpUnhandledException" /> class with the specified error message and inner exception.</summary>
	/// <param name="message">The message displayed to the client when the exception is thrown. </param>
	/// <param name="innerException">The <see cref="P:System.Exception.InnerException" />, if any, that threw the current exception. </param>
	public HttpUnhandledException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
