using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.AuthorizationRule" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(AuthorizationRule), AddItemName = "allow,deny", CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
public sealed class AuthorizationRuleCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the type of this <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" />.</summary>
	/// <returns>A value from the <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> enumeration representing the type of this collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMapAlternate;

	protected override string ElementName => string.Empty;

	/// <summary>Gets or sets an item in this collection.</summary>
	/// <param name="index">
	///       <see cref="T:System.Web.Configuration.AuthorizationRule" /> collection index</param>
	/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</returns>
	public AuthorizationRule this[int index]
	{
		get
		{
			return Get(index);
		}
		set
		{
			Set(index, value);
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AuthorizationRuleCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to the collection.</summary>
	/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to add to the collection. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object already exists in the collection, or the collection is read-only.</exception>
	public void Add(AuthorizationRule rule)
	{
		BaseAdd(rule, throwIfExists: false);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.AuthorizationRule" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement(string elementName)
	{
		return new AuthorizationRule((elementName == "allow") ? AuthorizationRuleAction.Allow : AuthorizationRuleAction.Deny);
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new AuthorizationRule(AuthorizationRuleAction.Allow);
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</summary>
	/// <param name="index">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> index. </param>
	/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</returns>
	public AuthorizationRule Get(int index)
	{
		return (AuthorizationRule)BaseGet(index);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((AuthorizationRule)element).Action.ToString();
	}

	/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object.</summary>
	/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object whose index is returned.</param>
	/// <returns>The index of the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object.</returns>
	public int IndexOf(AuthorizationRule rule)
	{
		return BaseIndexOf(rule);
	}

	protected override bool IsElementName(string elementname)
	{
		if (!(elementname == "allow"))
		{
			return elementname == "deny";
		}
		return true;
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object from the collection.</summary>
	/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" />  object to remove.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.AuthorizationRule" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void Remove(AuthorizationRule rule)
	{
		BaseRemove(rule.Action.ToString());
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object from the collection at the specified index.</summary>
	/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.AuthorizationRule" /> to remove.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.AuthorizationRule" /> object with the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to the collection at the specified index.</summary>
	/// <param name="index">The index location at which to add the specified <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> object. </param>
	/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to be added.</param>
	public void Set(int index, AuthorizationRule rule)
	{
		if (BaseGet(index) != null)
		{
			BaseRemoveAt(index);
		}
		BaseAdd(index, rule);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> class. </summary>
	public AuthorizationRuleCollection()
	{
	}
}
