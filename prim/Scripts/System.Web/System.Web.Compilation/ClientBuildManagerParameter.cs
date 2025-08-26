namespace System.Web.Compilation;

/// <summary>Contains values passed to the ASP.NET compiler during precompilation.</summary>
[Serializable]
public class ClientBuildManagerParameter
{
	private PrecompilationFlags precompilationFlags;

	private string strongNameKeyContainer;

	private string strongNameKeyFile;

	/// <summary>Gets or sets the flags that determine precompilation behavior.</summary>
	/// <returns>The <see cref="T:System.Web.Compilation.PrecompilationFlags" /> for a client build.</returns>
	public PrecompilationFlags PrecompilationFlags
	{
		get
		{
			return precompilationFlags;
		}
		set
		{
			precompilationFlags = value;
		}
	}

	/// <summary>Gets or sets the key container used during compilation.</summary>
	/// <returns>A <see cref="T:System.String" /> of the value for the key container.</returns>
	public string StrongNameKeyContainer
	{
		get
		{
			return strongNameKeyContainer;
		}
		set
		{
			strongNameKeyContainer = value;
		}
	}

	/// <summary>Gets or sets the key file used during compilation.</summary>
	/// <returns>A <see cref="T:System.String" /> of the value for the key file.</returns>
	public string StrongNameKeyFile
	{
		get
		{
			return strongNameKeyFile;
		}
		set
		{
			strongNameKeyFile = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManagerParameter" /> class. </summary>
	public ClientBuildManagerParameter()
	{
	}
}
