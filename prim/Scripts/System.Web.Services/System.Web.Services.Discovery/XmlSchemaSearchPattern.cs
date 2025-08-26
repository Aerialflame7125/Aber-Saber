namespace System.Web.Services.Discovery;

/// <summary>Obtains the file locations of XML Schema documents for use in populating a Web services discovery document. This class cannot be inherited.</summary>
public sealed class XmlSchemaSearchPattern : DiscoverySearchPattern
{
	/// <summary>Gets the file name pattern to use as a search target.</summary>
	/// <returns>The literal string "*.xsd".</returns>
	public override string Pattern => "*.xsd";

	/// <summary>Returns the <see cref="T:System.Web.Services.Discovery.SchemaReference" /> object for a given discovery document.</summary>
	/// <param name="filename">The file system path of the XML Schema document.</param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.SchemaReference" /> object that specifies the file name for an XML Schema document.</returns>
	public override DiscoveryReference GetDiscoveryReference(string filename)
	{
		return new SchemaReference(filename);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.XmlSchemaSearchPattern" /> class. </summary>
	public XmlSchemaSearchPattern()
	{
	}
}
