using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001;

/// <summary>Wraps an XML <see langword="NMTOKENS" /> attribute.</summary>
[Serializable]
[ComVisible(true)]
public sealed class SoapNmtokens : ISoapXsd
{
	private string _value;

	/// <summary>Gets or sets an XML <see langword="NMTOKENS" /> attribute.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKENS" /> attribute.</returns>
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
	public static string XsdType => "NMTOKENS";

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class.</summary>
	public SoapNmtokens()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> class with an XML <see langword="NMTOKENS" /> attribute.</summary>
	/// <param name="value">A <see cref="T:System.String" /> that contains an XML <see langword="NMTOKENS" /> attribute.</param>
	public SoapNmtokens(string value)
	{
		_value = SoapHelper.Normalize(value);
	}

	/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
	/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
	public string GetXsdType()
	{
		return XsdType;
	}

	/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object.</summary>
	/// <param name="value">The <see langword="String" /> to convert.</param>
	/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens" /> object that is obtained from <paramref name="value" />.</returns>
	public static SoapNmtokens Parse(string value)
	{
		return new SoapNmtokens(value);
	}

	/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" /> as a <see cref="T:System.String" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtokens.Value" />.</returns>
	public override string ToString()
	{
		return _value;
	}
}
