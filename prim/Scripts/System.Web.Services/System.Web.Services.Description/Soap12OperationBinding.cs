using System.ComponentModel;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents a service description format extension applied to an <see cref="T:System.Web.Services.Description.OperationBinding" /> when an XML Web service supports the SOAP protocol version 1.2. This class cannot be inherited.</summary>
[XmlFormatExtension("operation", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(OperationBinding))]
public sealed class Soap12OperationBinding : SoapOperationBinding
{
	private bool soapActionRequired;

	private Soap12OperationBinding duplicateBySoapAction;

	private Soap12OperationBinding duplicateByRequestElement;

	private SoapReflectedMethod method;

	/// <summary>Gets or sets a value indicating whether an XML Web service anticipates requiring the <see langword="SOAPAction" /> HTTP header.</summary>
	/// <returns>
	///     <see langword="true" /> if an XML Web service anticipates requiring the <see langword="SOAPAction" /> HTTP header; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[XmlAttribute("soapActionRequired")]
	[DefaultValue(false)]
	public bool SoapActionRequired
	{
		get
		{
			return soapActionRequired;
		}
		set
		{
			soapActionRequired = value;
		}
	}

	internal SoapReflectedMethod Method
	{
		get
		{
			return method;
		}
		set
		{
			method = value;
		}
	}

	internal Soap12OperationBinding DuplicateBySoapAction
	{
		get
		{
			return duplicateBySoapAction;
		}
		set
		{
			duplicateBySoapAction = value;
		}
	}

	internal Soap12OperationBinding DuplicateByRequestElement
	{
		get
		{
			return duplicateByRequestElement;
		}
		set
		{
			duplicateByRequestElement = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Soap12OperationBinding" /> class. </summary>
	public Soap12OperationBinding()
	{
	}
}
