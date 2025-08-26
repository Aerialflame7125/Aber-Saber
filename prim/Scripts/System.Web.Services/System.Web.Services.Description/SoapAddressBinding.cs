using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.Port" /> within an XML Web service.</summary>
[XmlFormatExtension("address", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(Port))]
public class SoapAddressBinding : ServiceDescriptionFormatExtension
{
	private string location;

	/// <summary>Gets or sets a value representing the URI for the <see cref="T:System.Web.Services.Description.Port" /> to which the <see cref="T:System.Web.Services.Description.SoapAddressBinding" /> applies.</summary>
	/// <returns>A string containing a URI. The default value is an empty string ("").</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapAddressBinding" /> class. </summary>
	public SoapAddressBinding()
	{
	}
}
