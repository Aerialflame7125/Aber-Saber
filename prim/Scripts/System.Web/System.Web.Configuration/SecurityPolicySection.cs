using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings that are used to support the security infrastructure of a Web application. This class cannot be inherited.</summary>
public sealed class SecurityPolicySection : ConfigurationSection
{
	private static ConfigurationProperty Prop;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the <see cref="P:System.Web.Configuration.SecurityPolicySection.TrustLevels" /> collection.</summary>
	/// <returns>A collection of <see cref="P:System.Web.Configuration.SecurityPolicySection.TrustLevels" /> objects. </returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public TrustLevelCollection TrustLevels => (TrustLevelCollection)base[Prop];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static SecurityPolicySection()
	{
		Prop = new ConfigurationProperty("", typeof(TrustLevelCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);
		properties = new ConfigurationPropertyCollection();
		properties.Add(Prop);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SecurityPolicySection" /> class by using default settings.</summary>
	public SecurityPolicySection()
	{
	}
}
