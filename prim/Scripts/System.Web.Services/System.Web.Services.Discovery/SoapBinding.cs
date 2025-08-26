using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents a SOAP binding in a discovery document. This class cannot be inherited.</summary>
[XmlRoot("soap", Namespace = "http://schemas.xmlsoap.org/disco/soap/")]
public sealed class SoapBinding
{
	/// <summary>The XML namespace of the element that specifies a SOAP binding within a discovery document.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/disco/soap/";

	private XmlQualifiedName binding;

	private string address = "";

	/// <summary>Gets or sets the URL of the XML Web service implementing the SOAP binding.</summary>
	/// <returns>The URL of the XML Web service implementing the SOAP binding. The default value is <see cref="F:System.String.Empty" />.</returns>
	[XmlAttribute("address")]
	public string Address
	{
		get
		{
			return address;
		}
		set
		{
			if (value == null)
			{
				address = "";
			}
			else
			{
				address = value;
			}
		}
	}

	/// <summary>Gets or sets the XML qualified name of the SOAP binding implemented by the XML Web service.</summary>
	/// <returns>The <see cref="T:System.Xml.XmlQualifiedName" /> of the SOAP binding implemented by the XML Web service.</returns>
	[XmlAttribute("binding")]
	public XmlQualifiedName Binding
	{
		get
		{
			return binding;
		}
		set
		{
			binding = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.SoapBinding" /> class. </summary>
	public SoapBinding()
	{
	}
}
