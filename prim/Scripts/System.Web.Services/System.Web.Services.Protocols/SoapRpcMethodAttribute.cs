using System.Runtime.InteropServices;
using System.Web.Services.Description;

namespace System.Web.Services.Protocols;

/// <summary>Specifies that SOAP messages sent to and from the method use <see langword="RPC" /> formatting.</summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class SoapRpcMethodAttribute : Attribute
{
	private string action;

	private string requestName;

	private string responseName;

	private string requestNamespace;

	private string responseNamespace;

	private bool oneWay;

	private string binding;

	private SoapBindingUse use = SoapBindingUse.Encoded;

	/// <summary>Gets or sets the <see langword="SOAPAction" /> HTTP header field of the SOAP request.</summary>
	/// <returns>The <see langword="SOAPAction" /> HTTP header field of the SOAP request. The default is http://tempuri.org/MethodName where MethodName is the name of the XML Web service method.</returns>
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

	/// <summary>Gets or sets the binding that an XML Web service method implements an operation for.</summary>
	/// <returns>The binding an XML Web service method implements an operation for. The default is the name of the XML Web service with "Soap" appended.</returns>
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

	/// <summary>Gets or sets whether an XML Web service client waits for the Web server to finish processing an XML Web service method.</summary>
	/// <returns>
	///     <see langword="true" /> if the XML Web service client does not wait for the Web server to completely process an XML Web service method; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
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

	/// <summary>Gets or sets the XML namespace associated with the SOAP request for an XML Web service method.</summary>
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

	/// <summary>Gets or sets the XML element associated with the SOAP request for an XML Web service method.</summary>
	/// <returns>The XML element associated with the SOAP request for an XML Web service method. The default value is the name of the XML Web service method.</returns>
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

	/// <summary>Gets or sets the binding used when invoking the method.</summary>
	/// <returns>A member of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> enumeration specifying the binding used when invoking the method.</returns>
	[ComVisible(false)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> class, setting all properties to their default values.</summary>
	public SoapRpcMethodAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapRpcMethodAttribute" /> class, setting the <see cref="P:System.Web.Services.Protocols.SoapRpcMethodAttribute.Action" /> property to the value of the <paramref name="action" /> parameter.</summary>
	/// <param name="action">The intent of the SOAP request. Sets the <see cref="P:System.Web.Services.Protocols.SoapRpcMethodAttribute.Action" /> property. </param>
	public SoapRpcMethodAttribute(string action)
	{
		this.action = action;
	}
}
