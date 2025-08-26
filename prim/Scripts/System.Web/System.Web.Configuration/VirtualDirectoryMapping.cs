namespace System.Web.Configuration;

/// <summary>Specifies a custom virtual-directory hierarchy for a Web application. This class cannot be inherited.</summary>
public sealed class VirtualDirectoryMapping
{
	private string physicalDirectory;

	private bool isAppRoot;

	private string configFileBaseName;

	private string virtualDirectory;

	/// <summary>Gets or sets the name of the configuration file.</summary>
	/// <returns>A value that indicates the name of the configuration file.</returns>
	/// <exception cref="T:System.ArgumentException">The selected value is null or an empty string ("").</exception>
	[MonoTODO("Do something with this")]
	public string ConfigFileBaseName
	{
		get
		{
			return configFileBaseName;
		}
		set
		{
			configFileBaseName = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the virtual directory should be treated as the application root.</summary>
	/// <returns>A value that indicates whether the virtual directory should be treated as the application root.</returns>
	[MonoTODO("Do something with this")]
	public bool IsAppRoot
	{
		get
		{
			return isAppRoot;
		}
		set
		{
			isAppRoot = value;
		}
	}

	/// <summary>Gets or sets a value that specifies the full server path of a Web application.</summary>
	/// <returns>A value that indicates the full server path of a Web application.</returns>
	/// <exception cref="T:System.ArgumentException">The selected value is invalid or fails internal security validation.</exception>
	public string PhysicalDirectory
	{
		get
		{
			return physicalDirectory;
		}
		set
		{
			physicalDirectory = value;
		}
	}

	/// <summary>Gets a value that specifies the virtual directory relative to the root of the Web server.</summary>
	/// <returns>A value that indicates the relative Web-application directory.</returns>
	public string VirtualDirectory => virtualDirectory;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> class based on supplied parameters.</summary>
	/// <param name="physicalDirectory">A string value that specifies the absolute path to a physical directory.</param>
	/// <param name="isAppRoot">A Boolean value that indicates whether the virtual directory is the application root of the Web application.</param>
	public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot)
	{
		this.physicalDirectory = physicalDirectory;
		this.isAppRoot = isAppRoot;
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.VirtualDirectoryMapping" /> class based on supplied parameters.</summary>
	/// <param name="physicalDirectory">A string value that specifies the absolute path to a physical directory.</param>
	/// <param name="isAppRoot">A Boolean value that indicates whether the virtual directory is the application root of the Web application.</param>
	/// <param name="configFileBaseName">The name of the configuration file.</param>
	public VirtualDirectoryMapping(string physicalDirectory, bool isAppRoot, string configFileBaseName)
	{
		this.physicalDirectory = physicalDirectory;
		this.isAppRoot = isAppRoot;
		this.configFileBaseName = configFileBaseName;
	}

	internal void SetVirtualDirectory(string dir)
	{
		virtualDirectory = dir;
	}
}
