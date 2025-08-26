using System.Configuration;

namespace System.Web.Configuration;

internal sealed class MonoSettingsSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty compilersCompatibilityProp;

	private static ConfigurationProperty useCompilersCompatibilityProp;

	private static ConfigurationProperty verificationCompatibilityProp;

	[ConfigurationProperty("compilersCompatibility")]
	public CompilerCollection CompilersCompatibility => (CompilerCollection)base[compilersCompatibilityProp];

	[ConfigurationProperty("useCompilersCompatibility", DefaultValue = "True")]
	public bool UseCompilersCompatibility
	{
		get
		{
			return (bool)base[useCompilersCompatibilityProp];
		}
		set
		{
			base[useCompilersCompatibilityProp] = value;
		}
	}

	[ConfigurationProperty("verificationCompatibility", DefaultValue = "0")]
	public int VerificationCompatibility
	{
		get
		{
			return (int)base[verificationCompatibilityProp];
		}
		set
		{
			base[verificationCompatibilityProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static MonoSettingsSection()
	{
		compilersCompatibilityProp = new ConfigurationProperty("compilersCompatibility", typeof(CompilerCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		useCompilersCompatibilityProp = new ConfigurationProperty("useCompilersCompatibility", typeof(bool), true);
		verificationCompatibilityProp = new ConfigurationProperty("verificationCompatibility", typeof(int), 0);
		properties = new ConfigurationPropertyCollection();
		properties.Add(compilersCompatibilityProp);
		properties.Add(useCompilersCompatibilityProp);
		properties.Add(verificationCompatibilityProp);
	}
}
