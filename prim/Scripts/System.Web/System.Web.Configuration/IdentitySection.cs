using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the identity of a Web application. This class cannot be inherited.</summary>
public sealed class IdentitySection : ConfigurationSection
{
	private static ConfigurationProperty impersonateProp;

	private static ConfigurationProperty passwordProp;

	private static ConfigurationProperty userNameProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether client impersonation is used on each request.</summary>
	/// <returns>
	///     <see langword="true" /> if client impersonation is used; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[ConfigurationProperty("impersonate", DefaultValue = "False")]
	public bool Impersonate
	{
		get
		{
			return (bool)base[impersonateProp];
		}
		set
		{
			base[impersonateProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the password to use for impersonation. </summary>
	/// <returns>The password to use for impersonation. </returns>
	[ConfigurationProperty("password", DefaultValue = "")]
	public string Password
	{
		get
		{
			return (string)base[passwordProp];
		}
		set
		{
			base[passwordProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the user name to use for impersonation.</summary>
	/// <returns>The user name to use for impersonation. </returns>
	[ConfigurationProperty("userName", DefaultValue = "")]
	public string UserName
	{
		get
		{
			return (string)base[userNameProp];
		}
		set
		{
			base[userNameProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static IdentitySection()
	{
		impersonateProp = new ConfigurationProperty("impersonate", typeof(bool), false);
		passwordProp = new ConfigurationProperty("password", typeof(string), "");
		userNameProp = new ConfigurationProperty("userName", typeof(string), "");
		properties = new ConfigurationPropertyCollection();
		properties.Add(impersonateProp);
		properties.Add(passwordProp);
		properties.Add(userNameProp);
	}

	[MonoTODO("why override this?")]
	protected internal override object GetRuntimeObject()
	{
		return this;
	}

	protected internal override void Reset(ConfigurationElement parentElement)
	{
	}

	protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.IdentitySection" /> class using default parameters.</summary>
	public IdentitySection()
	{
	}
}
