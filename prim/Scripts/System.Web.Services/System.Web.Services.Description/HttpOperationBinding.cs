using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.OperationBinding" /> within an XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtension("operation", "http://schemas.xmlsoap.org/wsdl/http/", typeof(OperationBinding))]
public sealed class HttpOperationBinding : ServiceDescriptionFormatExtension
{
	private string location;

	/// <summary>Gets or sets the URL relative to the location specified by the <see cref="T:System.Web.Services.Description.HttpAddressBinding" />, within the Web Services Description Language (WSDL) document, of the operation supported by the <see cref="T:System.Web.Services.Description.HttpOperationBinding" />.</summary>
	/// <returns>An unencoded string representing the relative path. The default value is an empty string ("").</returns>
	[XmlAttribute("location")]
	public string Location
	{
		get
		{
			if (location != null)
			{
				return location;
			}
			return string.Empty;
		}
		set
		{
			location = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.HttpOperationBinding" /> class. </summary>
	public HttpOperationBinding()
	{
	}
}
