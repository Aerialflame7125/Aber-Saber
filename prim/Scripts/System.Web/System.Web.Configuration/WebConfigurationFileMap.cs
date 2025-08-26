using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines the configuration file mappings for a Web application. This class cannot be inherited.</summary>
public sealed class WebConfigurationFileMap : ConfigurationFileMap
{
	private VirtualDirectoryMappingCollection virtualDirectories;

	/// <summary>Gets the listed collection of virtual directories for a Web application.</summary>
	/// <returns>A collection of <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> objects.</returns>
	public VirtualDirectoryMappingCollection VirtualDirectories => virtualDirectories;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class.</summary>
	public WebConfigurationFileMap()
	{
		virtualDirectories = new VirtualDirectoryMappingCollection();
	}

	/// <summary>Creates a new instance of a <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class with the same value as the existing instance.</summary>
	/// <returns>A new instance of a <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> class.</returns>
	public override object Clone()
	{
		WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
		webConfigurationFileMap.MachineConfigFilename = base.MachineConfigFilename;
		webConfigurationFileMap.virtualDirectories = new VirtualDirectoryMappingCollection();
		foreach (VirtualDirectoryMapping virtualDirectory in virtualDirectories)
		{
			VirtualDirectoryMapping mapping = new VirtualDirectoryMapping(virtualDirectory.PhysicalDirectory, virtualDirectory.IsAppRoot, virtualDirectory.ConfigFileBaseName);
			webConfigurationFileMap.virtualDirectories.Add(virtualDirectory.VirtualDirectory, mapping);
		}
		return webConfigurationFileMap;
	}
}
