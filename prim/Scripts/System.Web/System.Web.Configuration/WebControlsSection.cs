using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the <see langword="webControls" /> section. This class cannot be inherited. </summary>
public sealed class WebControlsSection : ConfigurationSection
{
	private static ConfigurationProperty clientScriptsLocationProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the client scripts location.</summary>
	/// <returns>The location of the client scripts.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("clientScriptsLocation", DefaultValue = "/aspnet_client/{0}/{1}/", Options = ConfigurationPropertyOptions.IsRequired)]
	public string ClientScriptsLocation => (string)base[clientScriptsLocationProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static WebControlsSection()
	{
		clientScriptsLocationProp = new ConfigurationProperty("clientScriptsLocation", typeof(string), "/aspnet_client/{0}/{1}/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(clientScriptsLocationProp);
	}

	protected internal override object GetRuntimeObject()
	{
		return new Hashtable { { "clientScriptsLocation", ClientScriptsLocation } };
	}

	/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.WebControlsSection" />.</summary>
	public WebControlsSection()
	{
	}
}
