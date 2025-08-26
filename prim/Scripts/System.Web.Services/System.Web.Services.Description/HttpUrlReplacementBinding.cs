using System.Web.Services.Configuration;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> within an XML Web service. It specifies the format for data transmission through HTTP. This class cannot be inherited.</summary>
[XmlFormatExtension("urlReplacement", "http://schemas.xmlsoap.org/wsdl/http/", typeof(InputBinding))]
public sealed class HttpUrlReplacementBinding : ServiceDescriptionFormatExtension
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.HttpUrlReplacementBinding" /> class. </summary>
	public HttpUrlReplacementBinding()
	{
	}
}
