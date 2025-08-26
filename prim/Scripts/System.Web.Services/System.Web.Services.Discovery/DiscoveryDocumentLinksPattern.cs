namespace System.Web.Services.Discovery;

/// <summary>Obtains the file locations of Web services discovery documents for use in populating another Web services discovery document.</summary>
public class DiscoveryDocumentLinksPattern : DiscoverySearchPattern
{
	/// <summary>Gets the file-name pattern to use as a search target.</summary>
	/// <returns>The literal string "*.disco".</returns>
	public override string Pattern => "*.disco";

	/// <summary>Returns the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> object for a given discovery document.</summary>
	/// <param name="filename">The file-system path of the discovery document.</param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> object that specifies the location of a .vsdisco file.</returns>
	public override DiscoveryReference GetDiscoveryReference(string filename)
	{
		return new DiscoveryDocumentReference(filename);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentLinksPattern" /> class. </summary>
	public DiscoveryDocumentLinksPattern()
	{
	}
}
