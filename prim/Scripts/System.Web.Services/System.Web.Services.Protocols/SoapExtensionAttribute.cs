namespace System.Web.Services.Protocols;

/// <summary>When overridden in a derived class, specifies a SOAP extension should run with an XML Web service method.</summary>
public abstract class SoapExtensionAttribute : Attribute
{
	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Type" /> of the SOAP extension.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the SOAP extension.</returns>
	public abstract Type ExtensionType { get; }

	/// <summary>When overridden in a derived class, gets or set the priority of the SOAP extension.</summary>
	/// <returns>The priority of the SOAP extension.</returns>
	public abstract int Priority { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapExtensionAttribute" /> class. </summary>
	protected SoapExtensionAttribute()
	{
	}
}
