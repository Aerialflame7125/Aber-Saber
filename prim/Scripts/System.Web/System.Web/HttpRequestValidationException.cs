using System.Security.Permissions;

namespace System.Web;

/// <summary>The exception that is thrown when a potentially malicious input string is received from the client as part of the request data. This class cannot be inherited.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpRequestValidationException : HttpException
{
	internal override string Description => "Request validation detected a potentially dangerous input value from the client and aborted the request. This might be an attemp of using cross-site scripting to compromise the security of your site. You can disable request validation using the 'validateRequest=false' attribute in your page or setting it in your machine.config or web.config configuration files. If you disable it, you're encouraged to properly check the input values you get from the client.<br>\r\nYou can get more information on input validation <a href=\"http://www.cert.org/tech_tips/malicious_code_mitigation.html\">here</a>.";

	/// <summary>Creates a new instance of the <see cref="T:System.Web.HttpRequestValidationException" /> class.</summary>
	public HttpRequestValidationException()
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.HttpRequestValidationException" /> exception with the specified error message.</summary>
	/// <param name="message">A string that describes the error.</param>
	public HttpRequestValidationException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpRequestValidationException" /> class with a specified error message and a reference to the inner exception that is the cause of the exception.</summary>
	/// <param name="message">An error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If this parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
	public HttpRequestValidationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
