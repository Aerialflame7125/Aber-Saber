using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>References a directory location that is used during compilation of a dynamic resource. This class cannot be inherited.</summary>
public sealed class CodeSubDirectory : ConfigurationElement
{
	private static ConfigurationProperty directoryNameProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the name of the directory that contains files compiled at run time.</summary>
	/// <returns>A string value specifying the name of the directory reference used during compilation.</returns>
	[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
	[ConfigurationProperty("directoryName", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string DirectoryName
	{
		get
		{
			return (string)base[directoryNameProp];
		}
		set
		{
			base[directoryNameProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static CodeSubDirectory()
	{
		directoryNameProp = new ConfigurationProperty("directoryName", typeof(string), "", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(directoryNameProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> class.</summary>
	/// <param name="directoryName">A string value specifying the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> reference.</param>
	public CodeSubDirectory(string directoryName)
	{
		DirectoryName = directoryName;
	}
}
