using System.Security.Permissions;

namespace System.Web.Services.Description;

/// <summary>Serves as a base class for derived classes that import SOAP transmission protocols into XML Web services.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public abstract class SoapTransportImporter
{
	private SoapProtocolImporter protocolImporter;

	/// <summary>Gets or sets a reference to the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> performing the import action.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.Services.Description.SoapProtocolImporter" /> performing the import action.</returns>
	public SoapProtocolImporter ImportContext
	{
		get
		{
			return protocolImporter;
		}
		set
		{
			protocolImporter = value;
		}
	}

	/// <summary>When overridden in a derived class, this method determines whether the specified transport protocol is supported by the XML Web service.</summary>
	/// <param name="transport">A URI representing the transport protocol to be checked. </param>
	/// <returns>
	///     <see langword="true" /> if the transport protocol is supported; otherwise, <see langword="false" />.</returns>
	public abstract bool IsSupportedTransport(string transport);

	/// <summary>When overridden in a derived class, this method uses information contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" /> object model (available through the <see cref="P:System.Web.Services.Description.SoapTransportImporter.ImportContext" /> property) to add transport-specific code to the class being generated.</summary>
	public abstract void ImportClass();

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.SoapTransportImporter" /> class.</summary>
	protected SoapTransportImporter()
	{
	}
}
