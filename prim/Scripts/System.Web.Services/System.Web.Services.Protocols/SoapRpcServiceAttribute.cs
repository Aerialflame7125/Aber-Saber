using System.Runtime.InteropServices;
using System.Web.Services.Description;

namespace System.Web.Services.Protocols;

/// <summary>Sets the default format of SOAP requests and responses sent to and from XML Web service methods within the XML Web service.</summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SoapRpcServiceAttribute : Attribute
{
	private SoapServiceRoutingStyle routingStyle;

	private SoapBindingUse use = SoapBindingUse.Encoded;

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

	/// <summary>Gets or sets the binding used when invoking the Web service's methods.</summary>
	/// <returns>A member of the <see cref="T:System.Web.Services.Description.SoapBindingUse" /> enumeration specifying the binding used when invoking the Web service's methods.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapRpcServiceAttribute" /> class.</summary>
	public SoapRpcServiceAttribute()
	{
	}
}
