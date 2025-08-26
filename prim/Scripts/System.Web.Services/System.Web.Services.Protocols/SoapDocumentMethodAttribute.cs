using System.Web.Services.Description;

namespace System.Web.Services.Protocols;

/// <summary>Applying the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> to a method specifies that SOAP messages to and from the method use <see langword="Document" /> formatting.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class SoapDocumentMethodAttribute : Attribute
{
	private string action;

	private string requestName;

	private string responseName;

	private string requestNamespace;

	private string responseNamespace;

	private bool oneWay;

	private SoapBindingUse use;

	private SoapParameterStyle style;

	private string binding;

	/// <summary>Gets or sets the <see langword="SOAPAction" /> HTTP header field of the SOAP request.</summary>
	/// <returns>The <see langword="SOAPAction" /> HTTP header field of the SOAP request. The default is http://tempuri.org/MethodName, where MethodName is the name of the XML Web service method.</returns>
	public string Action
	{
		get
		{
			return action;
		}
		set
		{
			action = value;
		}
	}

	/// <summary>Gets or sets whether an XML Web service client waits for the Web server to finish processing an XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if the XML Web service client does not wait for the Web server to completely process an XML Web service method. The default value is <see langword="false" />.</returns>
	public bool OneWay
	{
		get
		{
			return oneWay;
		}
		set
		{
			oneWay = value;
		}
	}

	/// <summary>Gets or sets the namespace associated with the SOAP request for an XML Web service method.</summary>
	/// <returns>The XML namespace associated with the SOAP request for an XML Web service method. The default is http://tempuri.org/.</returns>
	public string RequestNamespace
	{
		get
		{
			return requestNamespace;
		}
		set
		{
			requestNamespace = value;
		}
	}

	/// <summary>Gets or sets the XML namespace associated with the SOAP response for an XML Web service method.</summary>
	/// <returns>The XML namespace associated with the SOAP response for an XML Web service method. The default is http://tempuri.org/.</returns>
	public string ResponseNamespace
	{
		get
		{
			return responseNamespace;
		}
		set
		{
			responseNamespace = value;
		}
	}

	/// <summary>Gets or sets the XML element associated with the SOAP request for an XML Web service method, which is defined in a service description as an operation.</summary>
	/// <returns>The XML element associated with the SOAP request for an XML Web service method, which is defined in an service description as an operation. The default value is the name of the XML Web service method.</returns>
	public string RequestElementName
	{
		get
		{
			if (requestName != null)
			{
				return requestName;
			}
			return string.Empty;
		}
		set
		{
			requestName = value;
		}
	}

	/// <summary>Gets or sets the XML element associated with the SOAP response for an XML Web service method.</summary>
	/// <returns>The XML element associated with the SOAP request for an XML Web service method. The default value is WebServiceNameResult, where WebServiceName is the name of the XML Web service method.</returns>
	public string ResponseElementName
	{
		get
		{
			if (responseName != null)
			{
				return responseName;
			}
			return string.Empty;
		}
		set
		{
			responseName = value;
		}
	}

	/// <summary>Gets or sets the parameter formatting for an XML Web service method within the XML portion of a SOAP message.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.SoapBindingUse" /> for the XML Web service method. The default is <see cref="F:System.Web.Services.Description.SoapBindingUse.Literal" />.</returns>
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

	/// <summary>Gets or sets whether parameters are encapsulated within a single XML element beneath the <see langword="Body" /> element in the XML portion of a SOAP message.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> for SOAP messages sent to and from an XML Web service method. The default value is <see cref="F:System.Web.Services.Protocols.SoapParameterStyle.Wrapped" />.</returns>
	public SoapParameterStyle ParameterStyle
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

	/// <summary>Gets or sets the binding an XML Web service method is implementing an operation for.</summary>
	/// <returns>The binding an XML Web service method is implementing an operation for. The default is the name of the XML Web service with "Soap" appended.</returns>
	public string Binding
	{
		get
		{
			if (binding != null)
			{
				return binding;
			}
			return string.Empty;
		}
		set
		{
			binding = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> class.</summary>
	public SoapDocumentMethodAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentMethodAttribute" /> class, setting the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.Action" /> property to the value of the <paramref name="action" /> parameter.</summary>
	/// <param name="action">The <see langword="SOAPAction" /> HTTP header field of the SOAP request. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentMethodAttribute.Action" /> property. </param>
	public SoapDocumentMethodAttribute(string action)
	{
		this.action = action;
	}
}
