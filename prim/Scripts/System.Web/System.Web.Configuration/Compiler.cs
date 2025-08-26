using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines a compiler that is used to support the compilation infrastructure of Web applications. This class cannot be inherited.</summary>
public sealed class Compiler : ConfigurationElement
{
	private static ConfigurationProperty compilerOptionsProp;

	private static ConfigurationProperty extensionProp;

	private static ConfigurationProperty languageProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationProperty warningLevelProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets a list of compiler-specific options to use during compilation.</summary>
	/// <returns>A value specifying the compiler-specific options to use during compilation. This is not a merged set but rather overrides any previously defined values in earlier configuration entries.</returns>
	[ConfigurationProperty("compilerOptions", DefaultValue = "")]
	public string CompilerOptions
	{
		get
		{
			return (string)base[compilerOptionsProp];
		}
		internal set
		{
			base[compilerOptionsProp] = value;
		}
	}

	/// <summary>Gets a list of file-name extensions used for dynamic code-behind files. </summary>
	/// <returns>A value specifying the file-name extensions used for dynamic code-behind files, files in the code directory, and other referenced files.</returns>
	[ConfigurationProperty("extension", DefaultValue = "")]
	public string Extension
	{
		get
		{
			return (string)base[extensionProp];
		}
		internal set
		{
			base[extensionProp] = value;
		}
	}

	/// <summary>Gets a list of programming languages to use in dynamic compilation files.</summary>
	/// <returns>A value specifying the programming languages to use in dynamic compilation files.</returns>
	[ConfigurationProperty("language", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Language
	{
		get
		{
			return (string)base[languageProp];
		}
		internal set
		{
			base[languageProp] = value;
		}
	}

	/// <summary>Gets the compiler type name of the language provider for dynamic compilation files. </summary>
	/// <returns>A value specifying the type name of the language compiler to use in dynamic compilation files.</returns>
	[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
	public string Type
	{
		get
		{
			return (string)base[typeProp];
		}
		internal set
		{
			base[typeProp] = value;
		}
	}

	/// <summary>Gets the compiler warning level.</summary>
	/// <returns>A value specifying the compiler warning level.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = 4)]
	[ConfigurationProperty("warningLevel", DefaultValue = "0")]
	public int WarningLevel
	{
		get
		{
			return (int)base[warningLevelProp];
		}
		internal set
		{
			base[warningLevelProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static Compiler()
	{
		compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), "");
		extensionProp = new ConfigurationProperty("extension", typeof(string), "");
		languageProp = new ConfigurationProperty("language", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
		warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(compilerOptionsProp);
		properties.Add(extensionProp);
		properties.Add(languageProp);
		properties.Add(typeProp);
		properties.Add(warningLevelProp);
	}

	internal Compiler()
	{
	}

	/// <summary>Creates an instance of a <see cref="T:System.Web.Configuration.Compiler" /> initialized to the provided values.</summary>
	/// <param name="compilerOptions">Lists additional compiler-specific options to pass during compilation.</param>
	/// <param name="extension">Provides a semicolon-separated list of file-name extensions used for dynamic code-behind files. For example, ".cs".</param>
	/// <param name="language">Provides a semicolon-separated list of languages used in dynamic compilation files. For example, "c#;cs;csharp".</param>
	/// <param name="type">Specifies a comma-separated class/assembly combination that indicates the .NET Framework class.</param>
	/// <param name="warningLevel">Specifies compiler warning levels.</param>
	public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
	{
		CompilerOptions = compilerOptions;
		Extension = extension;
		Language = language;
		Type = type;
		WarningLevel = warningLevel;
	}
}
