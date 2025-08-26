using System.Web.Services.Description;

namespace System.Web.Services.Protocols;

/// <summary>Applying the optional <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> to an XML Web service sets the default format of SOAP requests and responses sent to and from XML Web service methods within the XML Web service.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SoapDocumentServiceAttribute : Attribute
{
	private SoapBindingUse use;

	private SoapParameterStyle paramStyle;

	private SoapServiceRoutingStyle routingStyle;

	/// <summary>Gets or sets the default parameter formatting for an XML Web service.</summary>
	/// <returns>The default <see cref="T:System.Web.Services.Description.SoapBindingUse" /> for the XML Web service. If not set, the default is <see cref="F:System.Web.Services.Description.SoapBindingUse.Literal" />.</returns>
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

	/// <summary>Gets or sets the default setting that controls whether parameters are encapsulated within a single element following the <see langword="&lt;Body&gt;" /> element in the XML portion of a SOAP message for XML Web service methods of the XML Web service.</summary>
	/// <returns>The default <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> for SOAP requests and SOAP responses to and from XML Web service methods within the XML Web service. If not set, the default is <see cref="F:System.Web.Services.Protocols.SoapParameterStyle.Wrapped" />.</returns>
	public SoapParameterStyle ParameterStyle
	{
		get
		{
			return paramStyle;
		}
		set
		{
			paramStyle = value;
		}
	}

	/// <summary>Gets or sets how SOAP messages are routed to the XML Web service.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.SoapServiceRoutingStyle" /> that represents how SOAP messages are routed to the XML Web service. The default value is <see cref="F:System.Web.Services.Protocols.SoapServiceRoutingStyle.SoapAction" />.</returns>
	public SoapServiceRoutingStyle RoutingStyle
	{
		get
		{
			return routingStyle;
		}
		set
		{
			routingStyle = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> class setting all properties to their default values.</summary>
	public SoapDocumentServiceAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> class setting the parameter formatting.</summary>
	/// <param name="use">The parameter formatting of the XML Web service. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentServiceAttribute.Use" /> property. </param>
	public SoapDocumentServiceAttribute(SoapBindingUse use)
	{
		this.use = use;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> class that sets the parameter formatting and sets whether parameters are encapsulated within a single XML element, under the <see langword="Body" /> element, in SOAP messages.</summary>
	/// <param name="use">The parameter formatting style. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentServiceAttribute.Use" /> property. </param>
	/// <param name="paramStyle">Sets whether parameters are encapsulated within a single XML element, under the <see langword="Body" /> element, in SOAP messages sent to and from XML Web service methods within the XML Web service. Sets the <see cref="P:System.Web.Services.Protocols.SoapDocumentServiceAttribute.ParameterStyle" /> property. </param>
	public SoapDocumentServiceAttribute(SoapBindingUse use, SoapParameterStyle paramStyle)
	{
		this.use = use;
		this.paramStyle = paramStyle;
	}
}
