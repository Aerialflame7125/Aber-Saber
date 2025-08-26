namespace System.Web.Services.Discovery;

/// <summary>Obtains the file locations and descriptions of ASP.NET Web services. This class cannot be inherited.</summary>
public sealed class ContractSearchPattern : DiscoverySearchPattern
{
	/// <summary>Gets the file name pattern to use as a search target.</summary>
	/// <returns>The literal string "*.asmx".</returns>
	public override string Pattern => "*.asmx";

	/// <summary>Creates the <see cref="T:System.Web.Services.Discovery.ContractReference" /> object for the specified .asmx file.</summary>
	/// <param name="filename">The file-system path of the Web service's .asmx file.</param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.ContractReference" /> object with the specified file name for its .asmx file.</returns>
	public override DiscoveryReference GetDiscoveryReference(string filename)
	{
		return new ContractReference(filename + "?wsdl", filename);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractSearchPattern" /> class. </summary>
	public ContractSearchPattern()
	{
	}
}
