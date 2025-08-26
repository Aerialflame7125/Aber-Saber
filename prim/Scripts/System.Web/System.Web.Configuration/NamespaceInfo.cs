using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a single configuration namespace reference, similar to the <see langword="Import" /> directive. This class cannot be inherited.</summary>
public sealed class NamespaceInfo : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty namespaceProp;

	/// <summary>Gets or sets the namespace reference.</summary>
	/// <returns>A string that specifies the name of the namespace.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("namespace", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
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

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static NamespaceInfo()
	{
		namespaceProp = new ConfigurationProperty("namespace", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(namespaceProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.NamespaceInfo" /> class with the specified namespace reference.</summary>
	/// <param name="name">A namespace reference for the new <see cref="T:System.Web.Configuration.NamespaceInfo" /> object.</param>
	public NamespaceInfo(string name)
	{
		Namespace = name;
	}

	/// <summary>Compares the current instance to the passed <see cref="T:System.Web.Configuration.NamespaceInfo" /> object.</summary>
	/// <param name="namespaceInformation">A <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to compare to.</param>
	/// <returns>
	///     <see langword="true" /> if the two objects are identical. </returns>
	public override bool Equals(object namespaceInformation)
	{
		if (!(namespaceInformation is NamespaceInfo namespaceInfo))
		{
			return false;
		}
		return Namespace == namespaceInfo.Namespace;
	}

	/// <summary>Returns a hash value for the current instance.</summary>
	/// <returns>A hash value for the current instance.</returns>
	public override int GetHashCode()
	{
		return Namespace.GetHashCode();
	}
}
