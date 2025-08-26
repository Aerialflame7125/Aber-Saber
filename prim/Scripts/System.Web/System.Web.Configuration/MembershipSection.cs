using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings to support the infrastructure for configuring and managing membership details. This class cannot be inherited.</summary>
public sealed class MembershipSection : ConfigurationSection
{
	private static ConfigurationProperty defaultProviderProp;

	private static ConfigurationProperty hashAlgorithmTypeProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationProperty userIsOnlineTimeWindowProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the name of the default provider that is used to manage roles. </summary>
	/// <returns>The name of a provider in <see cref="P:System.Web.Configuration.MembershipSection.Providers" />. The default is <see langword="AspNetSqlRoleProvider" />.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlMembershipProvider")]
	public string DefaultProvider
	{
		get
		{
			return (string)base[defaultProviderProp];
		}
		set
		{
			base[defaultProviderProp] = value;
		}
	}

	/// <summary>Gets or sets the type of encryption that is used for sensitive membership information.</summary>
	/// <returns>The type of encryption used to encrypt sensitive membership information.</returns>
	[ConfigurationProperty("hashAlgorithmType", DefaultValue = "")]
	public string HashAlgorithmType
	{
		get
		{
			return (string)base[hashAlgorithmTypeProp];
		}
		set
		{
			base[hashAlgorithmTypeProp] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> object of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the provider's settings, defined within the <see langword="providers" /> subsection of the <see langword="membership" /> section of the configuration file.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

	/// <summary>Gets or sets the length of time, in minutes, before a user is no longer considered to be online.</summary>
	/// <returns>A length of time in minutes.</returns>
	[TypeConverter(typeof(TimeSpanMinutesConverter))]
	[TimeSpanValidator(MinValueString = "00:01:00")]
	[ConfigurationProperty("userIsOnlineTimeWindow", DefaultValue = "00:15:00")]
	public TimeSpan UserIsOnlineTimeWindow
	{
		get
		{
			return (TimeSpan)base[userIsOnlineTimeWindowProp];
		}
		set
		{
			base[userIsOnlineTimeWindowProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static MembershipSection()
	{
		defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlMembershipProvider", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		hashAlgorithmTypeProp = new ConfigurationProperty("hashAlgorithmType", typeof(string), "");
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		userIsOnlineTimeWindowProp = new ConfigurationProperty("userIsOnlineTimeWindow", typeof(TimeSpan), TimeSpan.FromMinutes(15.0), PropertyHelper.TimeSpanMinutesConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(defaultProviderProp);
		properties.Add(hashAlgorithmTypeProp);
		properties.Add(providersProp);
		properties.Add(userIsOnlineTimeWindowProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.MembershipSection" /> class.</summary>
	public MembershipSection()
	{
	}
}
