using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> within an XML Web service. It specifies the SOAP header types used to transmit error information within the SOAP header.</summary>
public class SoapHeaderFaultBinding : ServiceDescriptionFormatExtension
{
	private XmlQualifiedName message = XmlQualifiedName.Empty;

	private string part;

	private SoapBindingUse use;

	private string encoding;

	private string ns;

	/// <summary>Gets or sets a value specifying the name of the <see cref="T:System.Web.Services.Description.Message" /> within the XML Web service to which the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> applies.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" /> representing the name of the <see cref="T:System.Web.Services.Description.Message" />. The default value is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets a value indicating which <see cref="T:System.Web.Services.Description.MessagePart" /> within the XML Web service the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> applies to.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> applies.</returns>
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

	/// <summary>Specifies whether the header is encoded using rules specified by the <see cref="P:System.Web.Services.Description.SoapHeaderBinding.Encoding" /> property, or is encapsulated within a concrete schema.</summary>
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

	/// <summary>Gets or sets a URI representing the encoding style used to encode the error message for the SOAP header.</summary>
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

	/// <summary>Get or sets a URI representing the location of the specifications for encoding content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapHeaderFaultBinding.Encoding" /> property.</summary>
	/// <returns>Returns a string representing a URI.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapHeaderFaultBinding" /> class. </summary>
	public SoapHeaderFaultBinding()
	{
	}
}
