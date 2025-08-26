using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001;

/// <summary>Wraps an XML <see langword="NcName" /> type.</summary>
[Serializable]
[ComVisible(true)]
public sealed class SoapNcName : ISoapXsd
{
	private string _value;

	/// <summary>Gets or sets an XML <see langword="NcName" /> type.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NcName" /> type.</returns>
	public string Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
		}
	}

	/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
	/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
	public static string XsdType => "NCName";

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class.</summary>
	public SoapNcName()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class with an XML <see langword="NcName" /> type.</summary>
	/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NcName" /> type.</param>
	public SoapNcName(string value)
	{
		_value = SoapHelper.Normalize(value);
	}

	/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
	/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
	public string GetXsdType()
	{
		return XsdType;
	}

	/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object.</summary>
	/// <param name="value">The <see langword="String" /> to convert.</param>
	/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object that is obtained from <paramref name="value" />.</returns>
	public static SoapNcName Parse(string value)
	{
		return new SoapNcName(value);
	}

	/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" /> as a <see cref="T:System.String" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" />.</returns>
	public override string ToString()
	{
		return _value;
	}
}
