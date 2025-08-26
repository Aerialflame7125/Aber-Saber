using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.Binding" /> within an XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtension("binding", "http://schemas.xmlsoap.org/wsdl/http/", typeof(Binding))]
[XmlFormatExtensionPrefix("http", "http://schemas.xmlsoap.org/wsdl/http/")]
public sealed class HttpBinding : ServiceDescriptionFormatExtension
{
	private string verb;

	/// <summary>Specifies the URI for the XML namespace representing the HTTP transport for use with SOAP. This field is constant.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/wsdl/http/";

	/// <summary>Gets or sets a value indicating whether the HTTP request will be made using the "GET" or "POST" method.</summary>
	/// <returns>A string containing one of two possible values, "GET" or "POST". The default value is an empty string ("").</returns>
	[XmlAttribute("verb")]
	public string Verb
	{
		get
		{
			return verb;
		}
		set
		{
			verb = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.HttpBinding" /> class. </summary>
	public HttpBinding()
	{
	}
}
