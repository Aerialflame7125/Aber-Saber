using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines the alias associated with the target user agent for which ASP.NET server controls should render content. This class cannot be inherited.</summary>
public sealed class ClientTarget : ConfigurationElement
{
	private static ConfigurationProperty aliasProp;

	private static ConfigurationProperty userAgentProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the user agent's alias.</summary>
	/// <returns>The name used to refer to a specific user agent. </returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("alias", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Alias
	{
		get
		{
			return (string)base[aliasProp];
		}
		internal set
		{
			base[aliasProp] = value;
		}
	}

	/// <summary>Gets the user agent's identification name.</summary>
	/// <returns>The user agent's identification name.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("userAgent", Options = ConfigurationPropertyOptions.IsRequired)]
	public string UserAgent
	{
		get
		{
			return (string)base[userAgentProp];
		}
		internal set
		{
			base[userAgentProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ClientTarget()
	{
		aliasProp = new ConfigurationProperty("alias", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		userAgentProp = new ConfigurationProperty("userAgent", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(aliasProp);
		properties.Add(userAgentProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ClientTarget" /> class.</summary>
	/// <param name="alias">The name used to refer to a specific user agent.</param>
	/// <param name="userAgent">The user agent's identification name.</param>
	public ClientTarget(string alias, string userAgent)
	{
		Alias = alias;
		UserAgent = userAgent;
	}
}
