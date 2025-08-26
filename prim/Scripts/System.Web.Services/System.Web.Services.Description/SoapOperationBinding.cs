using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extension added to an <see cref="T:System.Web.Services.Description.OperationBinding" /> within an XML Web service.</summary>
[XmlFormatExtension("operation", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(OperationBinding))]
public class SoapOperationBinding : ServiceDescriptionFormatExtension
{
	private string soapAction;

	private SoapBindingStyle style;

	/// <summary>Gets or sets the URI for the SOAP header.</summary>
	/// <returns>A string containing the URI for the SOAP header.</returns>
	[XmlAttribute("soapAction")]
	public string SoapAction
	{
		get
		{
			if (soapAction != null)
			{
				return soapAction;
			}
			return string.Empty;
		}
		set
		{
			soapAction = value;
		}
	}

	/// <summary>Gets or sets the type of SOAP binding used by the <see cref="T:System.Web.Services.Description.SoapOperationBinding" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Description.SoapBindingStyle" /> values. The default is <see langword="Document" />.</returns>
	[XmlAttribute("style")]
	[DefaultValue(SoapBindingStyle.Default)]
	public SoapBindingStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			style = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapOperationBinding" /> class. </summary>
	public SoapOperationBinding()
	{
	}
}
