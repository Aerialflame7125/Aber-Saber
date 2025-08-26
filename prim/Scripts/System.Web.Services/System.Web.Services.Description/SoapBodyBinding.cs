using System.ComponentModel;
using System.Text;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" />.</summary>
[XmlFormatExtension("body", "http://schemas.xmlsoap.org/wsdl/soap/", typeof(InputBinding), typeof(OutputBinding), typeof(MimePart))]
public class SoapBodyBinding : ServiceDescriptionFormatExtension
{
	private SoapBindingUse use;

	private string ns;

	private string encoding;

	private string[] parts;

	/// <summary>Indicates whether the message parts are encoded using specified encoding rules, or define the concrete schema of the message.</summary>
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

	/// <summary>Get or sets the URI representing the location of the specifications for encoding of content not specifically defined by the <see cref="P:System.Web.Services.Description.SoapBodyBinding.Encoding" /> property.</summary>
	/// <returns>A string containing a URI.</returns>
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

	/// <summary>Gets or sets a string containing a list of space-delimited URIs. The URIs represent the encoding style (or styles) to be used to encode messages within the SOAP body.</summary>
	/// <returns>A string containing a list of URIs. The default value is an empty string ("").</returns>
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

	/// <summary>Gets or sets a value indicating which parts of the transmitted message appear within the SOAP body portion of the transmission.</summary>
	/// <returns>A space-delimited string containing the appropriate message parts.</returns>
	[XmlAttribute("parts")]
	public string PartsString
	{
		get
		{
			if (parts == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < parts.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(parts[i]);
			}
			return stringBuilder.ToString();
		}
		set
		{
			if (value == null)
			{
				parts = null;
				return;
			}
			parts = value.Split(' ');
		}
	}

	/// <summary>Gets or sets a value indicating which parts of the transmitted message appear within the SOAP body portion of the transmission.</summary>
	/// <returns>A string array containing the names of the appropriate message parts.</returns>
	[XmlIgnore]
	public string[] Parts
	{
		get
		{
			return parts;
		}
		set
		{
			parts = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapBodyBinding" /> class. </summary>
	public SoapBodyBinding()
	{
	}
}
