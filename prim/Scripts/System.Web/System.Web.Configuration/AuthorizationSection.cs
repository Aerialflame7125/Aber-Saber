using System.Configuration;
using System.Security.Principal;

namespace System.Web.Configuration;

/// <summary>Configures a Web application authorization. This class cannot be inherited.</summary>
public sealed class AuthorizationSection : ConfigurationSection
{
	private static ConfigurationProperty rulesProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> of <see cref="T:System.Web.Configuration.AuthorizationRule" /> rules.</summary>
	/// <returns>Gets the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> of <see cref="T:System.Web.Configuration.AuthorizationRule" /> rules defined by the <see cref="T:System.Web.Configuration.AuthorizationSection" />.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public AuthorizationRuleCollection Rules => (AuthorizationRuleCollection)base[rulesProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AuthorizationSection()
	{
		rulesProp = new ConfigurationProperty(string.Empty, typeof(AuthorizationRuleCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
		properties = new ConfigurationPropertyCollection();
		properties.Add(rulesProp);
	}

	protected override void PostDeserialize()
	{
		base.PostDeserialize();
	}

	internal bool IsValidUser(IPrincipal user, string verb)
	{
		string user2 = ((user == null) ? string.Empty : user.Identity.Name);
		foreach (AuthorizationRule rule in Rules)
		{
			if ((rule.Verbs.Count == 0 || rule.CheckVerb(verb)) && (rule.CheckUser(user2) || (user != null && rule.CheckRole(user))))
			{
				return rule.Action == AuthorizationRuleAction.Allow;
			}
		}
		return true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.AuthorizationSection" /> class using default settings.</summary>
	public AuthorizationSection()
	{
	}
}
