using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>Provides a common base implementation for readers of request parameters for Web services implemented using HTTP but without SOAP.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public abstract class MimeParameterReader : MimeFormatter
{
	/// <summary>When overridden in a derived class, deserializes an HTTP request into an array of Web method parameter values.</summary>
	/// <param name="request">An <see cref="T:System.Web.HttpRequest" /> object containing the input message for an operation.</param>
	/// <returns>An array of <see cref="T:System.Object" /> objects that contains the deserialized HTTP request.</returns>
	public abstract object[] Read(HttpRequest request);

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.MimeParameterReader" /> class. </summary>
	protected MimeParameterReader()
	{
	}
}
