using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the <see langword="clientTarget" /> section. This class cannot be inherited.</summary>
public sealed class ClientTargetSection : ConfigurationSection
{
	private static ConfigurationProperty clientTargetsProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the collection of <see cref="T:System.Web.Configuration.ClientTarget" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.ClientTargetCollection" /> that contains the <see cref="T:System.Web.Configuration.ClientTarget" /> objects.</returns>
	[ConfigurationProperty("", Options = (ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired))]
	public ClientTargetCollection ClientTargets => (ClientTargetCollection)base[clientTargetsProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ClientTargetSection()
	{
		clientTargetsProp = new ConfigurationProperty(null, typeof(ClientTargetCollection), null, ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(clientTargetsProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ClientTargetSection" /> class.</summary>
	public ClientTargetSection()
	{
	}
}
