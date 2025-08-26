using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> within an XML Web service.</summary>
[XmlFormatExtension("header", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(InputBinding), typeof(OutputBinding))]
public class SoapHeaderBinding : ServiceDescriptionFormatExtension
{
	private XmlQualifiedName message = XmlQualifiedName.Empty;

	private string part;

	private SoapBindingUse use;

	private string encoding;

	private string ns;

	private bool mapToProperty;

	private SoapHeaderFaultBinding fault;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> instance is mapped to a specific property in generated proxy classes.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> maps to a specific property; otherwise <see langword="false" />.</returns>
	[XmlIgnore]
	public bool MapToProperty
	{
		get
		{
			return mapToProperty;
		}
		set
		{
			mapToProperty = value;
		}
	}

	/// <summary>Gets or sets a value specifying the name of the <see cref="T:System.Web.Services.Description.Message" /> within the XML Web service to which the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> applies.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> representing the name of the <see cref="T:System.Web.Services.Description.Message" />.</returns>
	[XmlAttribute("message")]
	public XmlQualifiedName Message
	{
		get
		{
			return message;
		}
		set
		{
			message = value;
		}
	}

	/// <summary>Gets or sets a value indicating to which <see cref="T:System.Web.Services.Description.MessagePart" /> within the XML Web service the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> applies.</summary>
	/// <returns>A string representing the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> applies.</returns>
	[XmlAttribute("part")]
	public string Part
	{
		get
		{
			return part;
		}
		set
		{
			part = value;
		}
	}

	/// <summary>Specifies whether the header is encoded using rules specified by the <see cref="P:System.Web.Services.Description.SoapHeaderBinding.Encoding" /> property, or is encapsulated within a concrete XML schema.</summary>
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

	/// <summary>Gets or sets a URI representing the encoding style used to encode the SOAP header.</summary>
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

	/// <summary>Get or sets the URI representing the location of the specification for encoding of content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapHeaderBinding.Encoding" /> property.</summary>
	/// <returns>A string representing a URI.</returns>
	[XmlAttribute("namespace")]
	[DefaultValue("")]
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

	/// <summary>Gets or sets the extension type controlling the output in a WSDL document for the <see langword="headerfault" /> XML element of a SOAP header.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> representing the SOAP header types used to transmit error information.</returns>
	[XmlElement("headerfault")]
	public SoapHeaderFaultBinding Fault
	{
		get
		{
			return fault;
		}
		set
		{
			fault = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapHeaderBinding" /> class. </summary>
	public SoapHeaderBinding()
	{
	}
}
