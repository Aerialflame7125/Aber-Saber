using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TrustLevel" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(TrustLevel), AddItemName = "trustLevel")]
public sealed class TrustLevelCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the type of the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
	/// <returns>A value from the <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> enumeration representing the type of the collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	protected override string ElementName => "trustLevel";

	/// <summary>Gets the <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index.</summary>
	/// <param name="key">The index of the <see cref="T:System.Web.Configuration.TrustLevel" />.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.TrustLevel" /> that exists at the specified index of the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</returns>
	public new TrustLevel this[string key] => (TrustLevel)BaseGet(key);

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TrustLevel" /> item at the specified index within the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
	/// <param name="index">The numeric index of the <see cref="T:System.Web.Configuration.TrustLevel" />.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.TrustLevel" /> at the specified index.</returns>
	public TrustLevel this[int index]
	{
		get
		{
			return (TrustLevel)BaseGet(index);
		}
		set
		{
			Set(index, value);
		}
	}

	protected override bool ThrowOnDuplicate => false;

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TrustLevelCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.TrustLevel" /> object to the collection.</summary>
	/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to add to the collection.</param>
	public void Add(TrustLevel trustLevel)
	{
		BaseAdd(trustLevel);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.TrustLevel" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Gets the <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.TrustLevel" /> at the specified index.</returns>
	public TrustLevel Get(int index)
	{
		return (TrustLevel)BaseGet(index);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.TrustLevel" /> object from the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
	/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to remove from the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
	public void Remove(TrustLevel trustLevel)
	{
		BaseRemove(trustLevel.Name);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.TrustLevel" /> object at the specified index location from the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object.</summary>
	/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.TrustLevel" /> to remove from the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Configuration.TrustLevel" /> object to the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> object at the specified index.</summary>
	/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.TrustLevel" /> to be set within the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
	/// <param name="trustLevel">The <see cref="T:System.Web.Configuration.TrustLevel" /> to be set within the <see cref="T:System.Web.Configuration.TrustLevelCollection" />.</param>
	public void Set(int index, TrustLevel trustLevel)
	{
		if (BaseGet(index) != null)
		{
			BaseRemoveAt(index);
		}
		BaseAdd(index, trustLevel);
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new TrustLevel();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((TrustLevel)element).Name;
	}

	protected override bool IsElementName(string elementname)
	{
		return elementname == "trustlevel";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TrustLevelCollection" /> class.</summary>
	public TrustLevelCollection()
	{
	}
}
