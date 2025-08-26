using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> class allows you to programmatically access and modify the <see langword="authorization" /> section of a configuration file. This class cannot be inherited.</summary>
public sealed class AuthorizationRule : ConfigurationElement
{
	private static ConfigurationProperty rolesProp;

	private static ConfigurationProperty usersProp;

	private static ConfigurationProperty verbsProp;

	private static ConfigurationPropertyCollection properties;

	private AuthorizationRuleAction action;

	private ConfigurationSaveMode saveMode = ConfigurationSaveMode.Full;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.AuthorizationRule" /> action.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.AuthorizationRuleAction" /> values.</returns>
	public AuthorizationRuleAction Action
	{
		get
		{
			return action;
		}
		set
		{
			action = value;
		}
	}

	/// <summary>Gets the roles associated with the resource.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the roles whose authorization must be verified.</returns>
	[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
	[ConfigurationProperty("roles")]
	public StringCollection Roles => (StringCollection)base[rolesProp];

	/// <summary>Gets the users associated with the resource.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the users whose authorization must be verified.</returns>
	[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
	[ConfigurationProperty("users")]
	public StringCollection Users => (StringCollection)base[usersProp];

	/// <summary>Gets the verbs associated with the resource.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the verbs whose authorization must be verified. </returns>
	[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
	[ConfigurationProperty("verbs")]
	public StringCollection Verbs => (StringCollection)base[verbsProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AuthorizationRule()
	{
		rolesProp = new ConfigurationProperty("roles", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		usersProp = new ConfigurationProperty("users", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		verbsProp = new ConfigurationProperty("verbs", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(rolesProp);
		properties.Add(usersProp);
		properties.Add(verbsProp);
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.AuthorizationRule" /> class using the passed object. </summary>
	/// <param name="action">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to use to initialize the new instance.</param>
	public AuthorizationRule(AuthorizationRuleAction action)
	{
		this.action = action;
		base[rolesProp] = new CommaDelimitedStringCollection();
		base[usersProp] = new CommaDelimitedStringCollection();
		base[verbsProp] = new CommaDelimitedStringCollection();
	}

	/// <summary>Determines whether the specified object is equal to the current object.</summary>
	/// <param name="obj">The object to compare with the current object.</param>
	/// <returns>
	///     <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is AuthorizationRule authorizationRule))
		{
			return false;
		}
		if (action != authorizationRule.Action)
		{
			return false;
		}
		if (Roles.Count != authorizationRule.Roles.Count || Users.Count != authorizationRule.Users.Count || Verbs.Count != authorizationRule.Verbs.Count)
		{
			return false;
		}
		for (int i = 0; i < Roles.Count; i++)
		{
			if (Roles[i] != authorizationRule.Roles[i])
			{
				return false;
			}
		}
		for (int i = 0; i < Users.Count; i++)
		{
			if (Users[i] != authorizationRule.Users[i])
			{
				return false;
			}
		}
		for (int i = 0; i < Verbs.Count; i++)
		{
			if (Verbs[i] != authorizationRule.Verbs[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>Serves as a hash function for this object.</summary>
	/// <returns>An integer representing the hash code for the current object.</returns>
	public override int GetHashCode()
	{
		int num = (int)action;
		for (int i = 0; i < Roles.Count; i++)
		{
			num += Roles[i].GetHashCode();
		}
		for (int i = 0; i < Users.Count; i++)
		{
			num += Users[i].GetHashCode();
		}
		for (int i = 0; i < Verbs.Count; i++)
		{
			num += Verbs[i].GetHashCode();
		}
		return num;
	}

	[MonoTODO("Not implemented")]
	protected internal override bool IsModified()
	{
		if (((CommaDelimitedStringCollection)Roles).IsModified || ((CommaDelimitedStringCollection)Users).IsModified || ((CommaDelimitedStringCollection)Verbs).IsModified)
		{
			return true;
		}
		return false;
	}

	private void VerifyData()
	{
		if (Roles.Count == 0 && Users.Count == 0)
		{
			throw new ConfigurationErrorsException("You must supply either a list of users or roles when creating an AuthorizationRule");
		}
	}

	protected override void PostDeserialize()
	{
		base.PostDeserialize();
		VerifyData();
	}

	protected override void PreSerialize(XmlWriter writer)
	{
		base.PreSerialize(writer);
		VerifyData();
	}

	protected internal override void Reset(ConfigurationElement parentElement)
	{
		AuthorizationRule authorizationRule = (AuthorizationRule)parentElement;
		Action = authorizationRule.Action;
		base.Reset(parentElement);
	}

	protected internal override void ResetModified()
	{
		base.ResetModified();
	}

	protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
	{
		if (saveMode != ConfigurationSaveMode.Full && !IsModified())
		{
			return true;
		}
		PreSerialize(writer);
		writer.WriteStartElement((action == AuthorizationRuleAction.Allow) ? "allow" : "deny");
		if (Roles.Count > 0)
		{
			writer.WriteAttributeString("roles", Roles.ToString());
		}
		if (Users.Count > 0)
		{
			writer.WriteAttributeString("users", Users.ToString());
		}
		if (Verbs.Count > 0)
		{
			writer.WriteAttributeString("verbs", Verbs.ToString());
		}
		writer.WriteEndElement();
		return true;
	}

	protected internal override void SetReadOnly()
	{
		base.SetReadOnly();
	}

	protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
	{
		base.Unmerge(sourceElement, parentElement, saveMode);
		this.saveMode = saveMode;
		if (sourceElement is AuthorizationRule authorizationRule)
		{
			action = authorizationRule.Action;
		}
	}

	internal bool CheckVerb(string verb)
	{
		StringEnumerator enumerator = Verbs.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				if (string.Compare(enumerator.Current, verb, ignoreCase: true, Helpers.InvariantCulture) == 0)
				{
					return true;
				}
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		return false;
	}

	internal bool CheckUser(string user)
	{
		StringEnumerator enumerator = Users.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				if (string.Compare(current, user, ignoreCase: true, Helpers.InvariantCulture) == 0 || current == "*" || (current == "?" && user == ""))
				{
					return true;
				}
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		return false;
	}

	internal bool CheckRole(IPrincipal user)
	{
		StringEnumerator enumerator = Roles.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				if (user.IsInRole(current))
				{
					return true;
				}
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
		return false;
	}
}
