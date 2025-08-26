using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines a configuration element containing tag-related information.</summary>
public sealed class TagPrefixInfo : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty tagPrefixProp;

	private static ConfigurationProperty namespaceProp;

	private static ConfigurationProperty assemblyProp;

	private static ConfigurationProperty tagNameProp;

	private static ConfigurationProperty sourceProp;

	private static ConfigurationElementProperty elementProperty;

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	/// <summary>Gets or sets the name of the assembly containing the control implementation.</summary>
	/// <returns>The name of the assembly (without an extension). The default is an empty string.</returns>
	[ConfigurationProperty("assembly")]
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

	/// <summary>Gets or sets the namespace in which the control resides.</summary>
	/// <returns>The name of the namespace. The default is an empty string.</returns>
	[ConfigurationProperty("namespace")]
	public string Namespace
	{
		get
		{
			return (string)base[namespaceProp];
		}
		set
		{
			base[namespaceProp] = value;
		}
	}

	/// <summary>Gets or sets the name and path of the file containing the user control.</summary>
	/// <returns>The name and path of the file containing the user control.</returns>
	[ConfigurationProperty("src")]
	public string Source
	{
		get
		{
			return (string)base[sourceProp];
		}
		set
		{
			base[sourceProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the user control.</summary>
	/// <returns>The name of the user control.</returns>
	[ConfigurationProperty("tagName")]
	public string TagName
	{
		get
		{
			return (string)base[tagNameProp];
		}
		set
		{
			base[tagNameProp] = value;
		}
	}

	/// <summary>Gets or sets the tag prefix that is being associated with a source file or namespace and assembly. </summary>
	/// <returns>The tag prefix. </returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("tagPrefix", DefaultValue = "/", Options = ConfigurationPropertyOptions.IsRequired)]
	public string TagPrefix
	{
		get
		{
			return (string)base[tagPrefixProp];
		}
		set
		{
			base[tagPrefixProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TagPrefixInfo()
	{
		tagPrefixProp = new ConfigurationProperty("tagPrefix", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		namespaceProp = new ConfigurationProperty("namespace", typeof(string));
		assemblyProp = new ConfigurationProperty("assembly", typeof(string));
		tagNameProp = new ConfigurationProperty("tagName", typeof(string));
		sourceProp = new ConfigurationProperty("src", typeof(string));
		properties = new ConfigurationPropertyCollection();
		properties.Add(tagPrefixProp);
		properties.Add(namespaceProp);
		properties.Add(assemblyProp);
		properties.Add(tagNameProp);
		properties.Add(sourceProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(TagPrefixInfo), ValidateElement));
	}

	internal TagPrefixInfo()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagPrefixInfo" /> class using the passed values.</summary>
	/// <param name="tagPrefix">The tag prefix being mapped to a source file or namespace and assembly. </param>
	/// <param name="nameSpace">The namespace associated with the tag prefix.</param>
	/// <param name="assembly">The assembly where the namespace resides.</param>
	/// <param name="tagName">The name of the control to be used in the page.</param>
	/// <param name="source">The name of the file that contains the user control.</param>
	public TagPrefixInfo(string tagPrefix, string nameSpace, string assembly, string tagName, string source)
	{
		TagPrefix = tagPrefix;
		Namespace = nameSpace;
		Assembly = assembly;
		TagName = tagName;
		Source = source;
	}

	private static void ValidateElement(object o)
	{
	}

	/// <summary>Compares this instance to another object.</summary>
	/// <param name="prefix">Object to compare.</param>
	/// <returns>
	///     <see langword="true" /> if the objects are identical; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object prefix)
	{
		if (!(prefix is TagPrefixInfo tagPrefixInfo))
		{
			return false;
		}
		if (Namespace == tagPrefixInfo.Namespace && Source == tagPrefixInfo.Source && TagName == tagPrefixInfo.TagName)
		{
			return TagPrefix == tagPrefixInfo.TagPrefix;
		}
		return false;
	}

	/// <summary>Returns a hash value for the current instance.</summary>
	/// <returns>A hash value for the current instance.</returns>
	public override int GetHashCode()
	{
		return Namespace.GetHashCode() + Source.GetHashCode() + TagName.GetHashCode() + TagPrefix.GetHashCode();
	}
}
