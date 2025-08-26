using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.Port" /> within an XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtension("address", "http://schemas.xmlsoap.org/wsdl/http/", typeof(Port))]
public sealed class HttpAddressBinding : ServiceDescriptionFormatExtension
{
	private string location;

	/// <summary>Gets or sets a value representing the URL of the XML Web service.</summary>
	/// <returns>A string specifying the URI for the <see cref="T:System.Web.Services.Description.Port" />. The default value is an empty string ("").</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.HttpAddressBinding" /> class. </summary>
	public HttpAddressBinding()
	{
	}
}
