using System.Web.Services.Configuration;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> within an XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtension("urlEncoded", "http://schemas.xmlsoap.org/wsdl/http/", typeof(InputBinding))]
public sealed class HttpUrlEncodedBinding : ServiceDescriptionFormatExtension
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.HttpUrlEncodedBinding" /> class. </summary>
	public HttpUrlEncodedBinding()
	{
	}
}
