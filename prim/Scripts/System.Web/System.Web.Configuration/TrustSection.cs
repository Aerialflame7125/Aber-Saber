using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the code-access security level that is applied to an application. This class cannot be inherited.</summary>
public sealed class TrustSection : ConfigurationSection
{
	private static ConfigurationProperty levelProp;

	private static ConfigurationProperty originUrlProp;

	private static ConfigurationProperty processRequestInApplicationTrustProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the name of the security level under which the application will run. </summary>
	/// <returns>The name of the trust level. The default is <see langword="&quot;Full&quot;" />.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("level", DefaultValue = "Full", Options = ConfigurationPropertyOptions.IsRequired)]
	public string Level
	{
		get
		{
			return (string)base[levelProp];
		}
		set
		{
			base[levelProp] = value;
		}
	}

	/// <summary>Specifies the URL of origin for an application.</summary>
	/// <returns>A well-formed HTTP URL or an empty string (""). The default is an empty string.</returns>
	[ConfigurationProperty("originUrl", DefaultValue = "")]
	public string OriginUrl
	{
		get
		{
			return (string)base[originUrlProp];
		}
		set
		{
			base[originUrlProp] = value;
		}
	}

	/// <summary>Gets or set a value that indicates whether page requests are automatically restricted to the permissions that are configured in the trust policy file that is applied to the ASP.NET application.</summary>
	/// <returns>
	///     <see langword="true" /> if requests are automatically restricted to the permissions that are configured in the trust policy file; otherwise, <see langword="false" />.</returns>
	[ConfigurationProperty("processRequestInApplicationTrust", DefaultValue = "True")]
	public bool ProcessRequestInApplicationTrust
	{
		get
		{
			return (bool)base[processRequestInApplicationTrustProp];
		}
		set
		{
			base[processRequestInApplicationTrustProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TrustSection()
	{
		levelProp = new ConfigurationProperty("level", typeof(string), "Full", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		originUrlProp = new ConfigurationProperty("originUrl", typeof(string), "");
		processRequestInApplicationTrustProp = new ConfigurationProperty("processRequestInApplicationTrust", typeof(bool), true);
		properties = new ConfigurationPropertyCollection();
		properties.Add(levelProp);
		properties.Add(originUrlProp);
		properties.Add(processRequestInApplicationTrustProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TrustSection" /> class using default settings.</summary>
	public TrustSection()
	{
	}
}
