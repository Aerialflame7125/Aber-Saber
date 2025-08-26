using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Provides functionality to parse a particular file type and generate code during compilation of a dynamic resource. This class cannot be inherited.</summary>
public sealed class BuildProvider : ConfigurationElement
{
	private static ConfigurationProperty extensionProp;

	private static ConfigurationProperty typeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the file extension to map to during compilation of a dynamic resource.</summary>
	/// <returns>A string specifying the file extension to map to during compilation of a dynamic resource.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("extension", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Extension
	{
		get
		{
			return (string)base[extensionProp];
		}
		set
		{
			string value2 = (string.IsNullOrEmpty(value) ? value : value.ToLowerInvariant());
			base[extensionProp] = value2;
		}
	}

	/// <summary>Gets or set the comma-separated class and assembly combination that indicates the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use.</summary>
	/// <returns>A comma-separated class and assembly combination that indicates the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
	public string Type
	{
		get
		{
			return (string)base[typeProp];
		}
		set
		{
			base[typeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static BuildProvider()
	{
		extensionProp = new ConfigurationProperty("extension", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		typeProp = new ConfigurationProperty("type", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(extensionProp);
		properties.Add(typeProp);
	}

	internal BuildProvider()
	{
	}

	/// <summary>Creates an instance of a <see cref="T:System.Web.Configuration.BuildProvider" /> class, initialized to the provided values.</summary>
	/// <param name="extension">The file extension of the dynamic resource used during compilation.</param>
	/// <param name="type">The type that represents the <see cref="T:System.Web.Configuration.BuildProvider" /> instance to use when parsing and compiling the given extension.</param>
	public BuildProvider(string extension, string type)
	{
		Extension = extension;
		Type = type;
	}

	/// <summary>Determines whether the specified build provider object is equal to the current object.</summary>
	/// <param name="provider">The build provider object to compare with the current object.</param>
	/// <returns>
	///     <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object provider)
	{
		if (!(provider is BuildProvider buildProvider))
		{
			return false;
		}
		if (Extension == buildProvider.Extension)
		{
			return Type == buildProvider.Type;
		}
		return false;
	}

	/// <summary>Generates the hash code for the build provider.</summary>
	/// <returns>An integer representing the hash code for the build provider.</returns>
	public override int GetHashCode()
	{
		return Extension.GetHashCode() + Type.GetHashCode();
	}
}
