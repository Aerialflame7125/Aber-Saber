using System.Security.Permissions;

namespace System.Web.Services.Description;

/// <summary>Provides a common interface and functionality for classes to add SOAP extension information to a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object on a per-method basis.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public abstract class SoapExtensionReflector
{
	private ProtocolReflector protocolReflector;

	/// <summary>Gets or sets the instance of a class derived from the abstract <see cref="T:System.Web.Services.Description.ProtocolReflector" /> class that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionReflector.ReflectMethod" /> method.</summary>
	/// <returns>The instance of a class derived from the abstract <see cref="T:System.Web.Services.Description.ProtocolReflector" /> class that invokes the <see cref="M:System.Web.Services.Description.SoapExtensionReflector.ReflectMethod" /> method.</returns>
	public ProtocolReflector ReflectionContext
	{
		get
		{
			return protocolReflector;
		}
		set
		{
			protocolReflector = value;
		}
	}

	/// <summary>
	///     <see langword="Abstract" /> method that a derived class must implement to add SOAP extension information to a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object on a per-method basis.</summary>
	public abstract void ReflectMethod();

	/// <summary>Generates service-specific description information that gets placed in a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object corresponding to a binding.</summary>
	public virtual void ReflectDescription()
	{
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.SoapExtensionReflector" /> class</summary>
	protected SoapExtensionReflector()
	{
	}
}
