using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>References an assembly to be linked to during compilation of a dynamic resource. This class cannot be inherited.</summary>
public sealed class AssemblyInfo : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty assemblyProp;

	/// <summary>Gets or sets an assembly reference to use during compilation of a dynamic resource.</summary>
	/// <returns>A comma-separated string value specifying the version, culture, and public-key tokens of an assembly.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("assembly", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Assembly
	{
		get
		{
			return (string)base[assemblyProp];
		}
		set
		{
			base[assemblyProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AssemblyInfo()
	{
		assemblyProp = new ConfigurationProperty("assembly", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(assemblyProp);
	}

	internal AssemblyInfo()
	{
	}

	/// <summary>Creates an instance of an <see cref="T:System.Web.Configuration.AssemblyInfo" /> class.</summary>
	/// <param name="assemblyName">Specifies a comma-separated assembly name combination consisting of version, culture, and public-key tokens.</param>
	public AssemblyInfo(string assemblyName)
	{
		Assembly = assemblyName;
	}
}
