using System.Security.Permissions;

namespace System.Web.Services.Discovery;

/// <summary>Establishes an interface for file extension search patterns for discoverable file types.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public abstract class DiscoverySearchPattern
{
	/// <summary>Gets the file name pattern to use as a search target.</summary>
	/// <returns>A file name pattern.</returns>
	public abstract string Pattern { get; }

	/// <summary>When overridden in a derived class, returns the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> object for a given file name.</summary>
	/// <param name="filename">The name of a discovery file or a file that appears in a dynamically generated discovery document. For example, an .asmx or .xsd file.</param>
	/// <returns>A file name.</returns>
	public abstract DiscoveryReference GetDiscoveryReference(string filename);

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoverySearchPattern" /> class. </summary>
	protected DiscoverySearchPattern()
	{
	}
}
