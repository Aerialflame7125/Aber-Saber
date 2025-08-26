using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.FaultBinding" /> within an XML Web service.</summary>
[XmlFormatExtension("fault", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(FaultBinding))]
public class SoapFaultBinding : ServiceDescriptionFormatExtension
{
	private SoapBindingUse use;

	private string ns;

	private string encoding;

	private string name;

	/// <summary>Specifies whether the fault message is encoded using rules specified by the <see cref="P:System.Web.Services.Description.SoapFaultBinding.Encoding" /> property, or is encapsulated within a concrete XML schema.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> values. The default is <see langword="Default" />.</returns>
	[XmlAttribute("use")]
	[DefaultValue(SoapBindingUse.Default)]
	public SoapBindingUse Use
	{
		get
		{
			return use;
		}
		set
		{
			use = value;
		}
	}

	/// <summary>Gets or sets the value of the name attribute that relates the soap fault to the wsdl fault defined for the specified operation.</summary>
	/// <returns>A <see cref="T:System.String" /> object that contains the name attribute that relates the soap fault to the wsdl fault defined for the operation.</returns>
	[XmlAttribute("name")]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Get or sets the URI representing the location of the specification for encoding of content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapFaultBinding.Encoding" /> property.</summary>
	/// <returns>A string representing a URI.</returns>
	[XmlAttribute("namespace")]
	public string Namespace
	{
		get
		{
			if (ns != null)
			{
				return ns;
			}
			return string.Empty;
		}
		set
		{
			ns = value;
		}
	}

	/// <summary>Gets or sets a URI representing the encoding style used to encode the SOAP fault message.</summary>
	/// <returns>A string containing a URI. The default value is an empty string ("").</returns>
	[XmlAttribute("encodingStyle")]
	[DefaultValue("")]
	public string Encoding
	{
		get
		{
			if (encoding != null)
			{
				return encoding;
			}
			return string.Empty;
		}
		set
		{
			encoding = value;
		}
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.SoapFaultBinding" /> class.</summary>
	public SoapFaultBinding()
	{
	}
}
