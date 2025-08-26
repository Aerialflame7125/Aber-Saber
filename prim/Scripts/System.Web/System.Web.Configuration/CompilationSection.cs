using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings that are used to support the compilation infrastructure of Web applications. This class cannot be inherited.</summary>
public sealed class CompilationSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty compilersProp;

	private static ConfigurationProperty tempDirectoryProp;

	private static ConfigurationProperty debugProp;

	private static ConfigurationProperty strictProp;

	private static ConfigurationProperty explicitProp;

	private static ConfigurationProperty batchProp;

	private static ConfigurationProperty batchTimeoutProp;

	private static ConfigurationProperty maxBatchSizeProp;

	private static ConfigurationProperty maxBatchGeneratedFileSizeProp;

	private static ConfigurationProperty numRecompilesBeforeAppRestartProp;

	private static ConfigurationProperty defaultLanguageProp;

	private static ConfigurationProperty assembliesProp;

	private static ConfigurationProperty assemblyPostProcessorTypeProp;

	private static ConfigurationProperty buildProvidersProp;

	private static ConfigurationProperty expressionBuildersProp;

	private static ConfigurationProperty urlLinePragmasProp;

	private static ConfigurationProperty codeSubDirectoriesProp;

	private static ConfigurationProperty optimizeCompilationsProp;

	private static ConfigurationProperty targetFrameworkProp;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.AssemblyCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.AssemblyCollection" /> that contains the assembly objects used during compilation of an ASP.NET resource.</returns>
	[ConfigurationProperty("assemblies")]
	public AssemblyCollection Assemblies => (AssemblyCollection)base[assembliesProp];

	/// <summary>Gets or sets a value specifying a post-process compilation step for an assembly.</summary>
	/// <returns>A string value specifying the post-process compilation step for an assembly.</returns>
	[ConfigurationProperty("assemblyPostProcessorType", DefaultValue = "")]
	public string AssemblyPostProcessorType
	{
		get
		{
			return (string)base[assemblyPostProcessorTypeProp];
		}
		set
		{
			base[assemblyPostProcessorTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether batch compilation is attempted.</summary>
	/// <returns>
	///     <see langword="true" /> if batch compilation is attempted; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("batch", DefaultValue = "True")]
	public bool Batch
	{
		get
		{
			return (bool)base[batchProp];
		}
		set
		{
			base[batchProp] = value;
		}
	}

	/// <summary>Gets or sets the time-out period, in seconds, for batch compilation.</summary>
	/// <returns>A value indicating the amount of time in seconds granted for batch compilation to occur.  </returns>
	[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00")]
	[ConfigurationProperty("batchTimeout", DefaultValue = "00:15:00")]
	public TimeSpan BatchTimeout
	{
		get
		{
			return (TimeSpan)base[batchTimeoutProp];
		}
		set
		{
			base[batchTimeoutProp] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProviderCollection" />  collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.BuildProviderCollection" /> that contains the build providers used during a compilation.</returns>
	[ConfigurationProperty("buildProviders")]
	public BuildProviderCollection BuildProviders => (BuildProviderCollection)base[buildProvidersProp];

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection that contains an ordered collection of subdirectories containing files compiled at run time.</returns>
	[ConfigurationProperty("codeSubDirectories")]
	public CodeSubDirectoriesCollection CodeSubDirectories => (CodeSubDirectoriesCollection)base[codeSubDirectoriesProp];

	/// <summary>Gets the <see cref="T:System.Web.Configuration.CompilerCollection" /> collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.CompilerCollection" /> collection that contains a collection of <see cref="T:System.Web.Configuration.Compiler" /> objects.</returns>
	[ConfigurationProperty("compilers")]
	public CompilerCollection Compilers => (CompilerCollection)base[compilersProp];

	/// <summary>Gets or sets a value specifying whether to compile release binaries or debug binaries. </summary>
	/// <returns>
	///     <see langword="true" /> if debug binaries will be used for compilation; otherwise, <see langword="false" />. <see langword="false" /> specifies that release binaries will be used for compilation. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("debug", DefaultValue = "False")]
	public bool Debug
	{
		get
		{
			return (bool)base[debugProp];
		}
		set
		{
			base[debugProp] = value;
		}
	}

	/// <summary>Gets or sets the default programming language to use in dynamic-compilation files.</summary>
	/// <returns>A value specifying the default programming language to use in dynamic-compilation files.</returns>
	[ConfigurationProperty("defaultLanguage", DefaultValue = "vb")]
	public string DefaultLanguage
	{
		get
		{
			return (string)base[defaultLanguageProp];
		}
		set
		{
			base[defaultLanguageProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether to use the Microsoft Visual Basic <see langword="explicit" /> compile option.</summary>
	/// <returns>
	///     <see langword="true" /> if the Visual Basic <see langword="explicit" /> compile option is enabled; otherwise, <see langword="false" />. <see langword="false" /> specifies that the Visual Basic <see langword="explicit" /> compile option is disabled. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("explicit", DefaultValue = "True")]
	public bool Explicit
	{
		get
		{
			return (bool)base[explicitProp];
		}
		set
		{
			base[explicitProp] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> that contains <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects.</returns>
	[ConfigurationProperty("expressionBuilders")]
	public ExpressionBuilderCollection ExpressionBuilders => (ExpressionBuilderCollection)base[expressionBuildersProp];

	/// <summary>Gets or sets the maximum combined size of the generated source files per batched compilation.</summary>
	/// <returns>An integer value indicating the maximum combined size of the generated source files per batched compilation.</returns>
	[ConfigurationProperty("maxBatchGeneratedFileSize", DefaultValue = "1000")]
	public int MaxBatchGeneratedFileSize
	{
		get
		{
			return (int)base[maxBatchGeneratedFileSizeProp];
		}
		set
		{
			base[maxBatchGeneratedFileSizeProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of pages per batched compilation.</summary>
	/// <returns>An integer value indicating the maximum number of pages that will be compiled into a single batch. The default number of pages is 1000.</returns>
	[ConfigurationProperty("maxBatchSize", DefaultValue = "1000")]
	public int MaxBatchSize
	{
		get
		{
			return (int)base[maxBatchSizeProp];
		}
		set
		{
			base[maxBatchSizeProp] = value;
		}
	}

	/// <summary>Gets or sets the number of dynamic recompiles of resources that can occur before the application restarts.</summary>
	/// <returns>A value indicating the number of dynamic recompiles of resources that can occur before the application restarts. The default is 15 recompilations.</returns>
	[ConfigurationProperty("numRecompilesBeforeAppRestart", DefaultValue = "15")]
	public int NumRecompilesBeforeAppRestart
	{
		get
		{
			return (int)base[numRecompilesBeforeAppRestartProp];
		}
		set
		{
			base[numRecompilesBeforeAppRestartProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the compilation must be optimized.</summary>
	/// <returns>
	///     <see langword="true" /> if the compilation must be optimized; otherwise, <see langword="false" />. The default is <see langword="false" />. </returns>
	[ConfigurationProperty("optimizeCompilations", DefaultValue = "False")]
	public bool OptimizeCompilations
	{
		get
		{
			return (bool)base[optimizeCompilationsProp];
		}
		set
		{
			base[optimizeCompilationsProp] = value;
		}
	}

	/// <summary>Gets or sets the Visual Basic <see langword="strict" /> compile option.</summary>
	/// <returns>
	///     <see langword="true" /> if the Visual Basic <see langword="strict" /> compile option is used; otherwise, <see langword="false" />. The default is <see langword="true" />. </returns>
	[ConfigurationProperty("strict", DefaultValue = "False")]
	public bool Strict
	{
		get
		{
			return (bool)base[strictProp];
		}
		set
		{
			base[strictProp] = value;
		}
	}

	/// <summary>Gets or sets the version of the .NET Framework that the Web site targets. </summary>
	/// <returns>The version of the .NET Framework that the Web site targets. The default value is <see langword="null" />.</returns>
	[ConfigurationProperty("targetFramework", DefaultValue = null)]
	public string TargetFramework
	{
		get
		{
			return (string)base[targetFrameworkProp];
		}
		set
		{
			base[targetFrameworkProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies the directory to use for temporary file storage during compilation.</summary>
	/// <returns>A value specifying the directory to use for temporary file storage during compilation.</returns>
	[ConfigurationProperty("tempDirectory", DefaultValue = "")]
	public string TempDirectory
	{
		get
		{
			return (string)base[tempDirectoryProp];
		}
		set
		{
			base[tempDirectoryProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether instructions to the compiler use physical paths or URLs.</summary>
	/// <returns>
	///     <see langword="true" /> if instructions to the compiler use URLs rather than physical paths; otherwise, <see langword="false" />. The default is <see langword="false" />. </returns>
	[ConfigurationProperty("urlLinePragmas", DefaultValue = "False")]
	public bool UrlLinePragmas
	{
		get
		{
			return (bool)base[urlLinePragmasProp];
		}
		set
		{
			base[urlLinePragmasProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static CompilationSection()
	{
		assembliesProp = new ConfigurationProperty("assemblies", typeof(AssemblyCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		assemblyPostProcessorTypeProp = new ConfigurationProperty("assemblyPostProcessorType", typeof(string), "");
		batchProp = new ConfigurationProperty("batch", typeof(bool), true);
		buildProvidersProp = new ConfigurationProperty("buildProviders", typeof(BuildProviderCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		batchTimeoutProp = new ConfigurationProperty("batchTimeout", typeof(TimeSpan), new TimeSpan(0, 15, 0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		codeSubDirectoriesProp = new ConfigurationProperty("codeSubDirectories", typeof(CodeSubDirectoriesCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		debugProp = new ConfigurationProperty("debug", typeof(bool), false);
		defaultLanguageProp = new ConfigurationProperty("defaultLanguage", typeof(string), "vb");
		expressionBuildersProp = new ConfigurationProperty("expressionBuilders", typeof(ExpressionBuilderCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		explicitProp = new ConfigurationProperty("explicit", typeof(bool), true);
		maxBatchSizeProp = new ConfigurationProperty("maxBatchSize", typeof(int), 1000);
		maxBatchGeneratedFileSizeProp = new ConfigurationProperty("maxBatchGeneratedFileSize", typeof(int), 3000);
		numRecompilesBeforeAppRestartProp = new ConfigurationProperty("numRecompilesBeforeAppRestart", typeof(int), 15);
		strictProp = new ConfigurationProperty("strict", typeof(bool), false);
		tempDirectoryProp = new ConfigurationProperty("tempDirectory", typeof(string), "");
		urlLinePragmasProp = new ConfigurationProperty("urlLinePragmas", typeof(bool), false);
		optimizeCompilationsProp = new ConfigurationProperty("optimizeCompilations", typeof(bool), false);
		targetFrameworkProp = new ConfigurationProperty("targetFramework", typeof(string), null);
		properties = new ConfigurationPropertyCollection();
		properties.Add(assembliesProp);
		properties.Add(assemblyPostProcessorTypeProp);
		properties.Add(batchProp);
		properties.Add(buildProvidersProp);
		properties.Add(batchTimeoutProp);
		properties.Add(codeSubDirectoriesProp);
		properties.Add(compilersProp);
		properties.Add(debugProp);
		properties.Add(defaultLanguageProp);
		properties.Add(expressionBuildersProp);
		properties.Add(explicitProp);
		properties.Add(maxBatchSizeProp);
		properties.Add(maxBatchGeneratedFileSizeProp);
		properties.Add(numRecompilesBeforeAppRestartProp);
		properties.Add(strictProp);
		properties.Add(tempDirectoryProp);
		properties.Add(urlLinePragmasProp);
		properties.Add(optimizeCompilationsProp);
		properties.Add(targetFrameworkProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CompilationSection" /> class by using default settings.</summary>
	public CompilationSection()
	{
	}

	protected override void PostDeserialize()
	{
		base.PostDeserialize();
	}

	[MonoTODO("why override this?")]
	protected internal override object GetRuntimeObject()
	{
		return this;
	}
}
