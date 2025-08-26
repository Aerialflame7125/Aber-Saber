using System.Security.Permissions;
using System.Security.Policy;

namespace System.Web.Services.Protocols;

/// <summary>The .NET Framework uses the <see cref="T:System.Web.Services.Protocols.ServerType" /> class to process XML Web service requests.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public class ServerType
{
	private Type type;

	internal Type Type => type;

	internal Evidence Evidence
	{
		get
		{
			new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Assert();
			return Type.Assembly.Evidence;
		}
	}

	/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.ServerType" />.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> that exposes the XML Web service.</param>
	public ServerType(Type type)
	{
		this.type = type;
	}
}
