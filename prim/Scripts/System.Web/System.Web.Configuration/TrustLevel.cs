using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines the mapping of specific security levels to named policy files. This class cannot be inherited.</summary>
public sealed class TrustLevel : ConfigurationElement
{
	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty policyFileProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a named security level that is mapped to a policy file.</summary>
	/// <returns>The <see cref="P:System.Web.Configuration.TrustLevel.Name" /> that is mapped to a policy file.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("name", DefaultValue = "Full", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Name
	{
		get
		{
			return (string)base[nameProp];
		}
		set
		{
			base[nameProp] = value;
		}
	}

	/// <summary>Gets or sets the configuration file reference that contains the security policy settings for the named security level.</summary>
	/// <returns>The configuration file reference that contains the security policy settings for the associated security level.</returns>
	[ConfigurationProperty("policyFile", DefaultValue = "internal", Options = ConfigurationPropertyOptions.IsRequired)]
	public string PolicyFile
	{
		get
		{
			return (string)base[policyFileProp];
		}
		set
		{
			base[policyFileProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TrustLevel()
	{
		nameProp = new ConfigurationProperty("name", typeof(string), "Full", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		policyFileProp = new ConfigurationProperty("policyFile", typeof(string), "internal", ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(nameProp);
		properties.Add(policyFileProp);
	}

	internal TrustLevel()
	{
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Configuration.TrustLevel" /> class that is initialized based on the provided values, which define the mapping of specific security levels to named policy files.</summary>
	/// <param name="name">A named security level that is mapped to a policy file.</param>
	/// <param name="policyFile">The configuration file that contains security policy settings for the named security level.</param>
	public TrustLevel(string name, string policyFile)
	{
		Name = name;
		PolicyFile = policyFile;
	}
}
