using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</summary>
[ConfigurationCollection(typeof(TagPrefixInfo), CollectionType = ConfigurationElementCollectionType.BasicMap)]
public sealed class TagPrefixCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets the type of the configuration collection.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
	[MonoTODO("why override this?")]
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	[MonoTODO("why override this?")]
	protected override string ElementName => "add";

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object at the specified index location.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public TagPrefixInfo this[int index]
	{
		get
		{
			return (TagPrefixInfo)BaseGet(index);
		}
		set
		{
			if (BaseGet(index) != null)
			{
				BaseRemoveAt(index);
			}
			BaseAdd(index, value);
		}
	}

	protected override bool ThrowOnDuplicate => false;

	static TagPrefixCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.TagPrefixCollection" /> class.</summary>
	public TagPrefixCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to the collection.</summary>
	/// <param name="tagPrefixInformation">The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
	public void Add(TagPrefixInfo tagPrefixInformation)
	{
		BaseAdd(tagPrefixInformation);
	}

	/// <summary>Clears all object from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new TagPrefixInfo();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		TagPrefixInfo tagPrefixInfo = (TagPrefixInfo)element;
		return tagPrefixInfo.TagPrefix + "-" + tagPrefixInfo.TagName + "-" + tagPrefixInfo.Source + "-" + tagPrefixInfo.Namespace + "-" + tagPrefixInfo.Assembly;
	}

	/// <summary>Removes the specified object from the collection.</summary>
	/// <param name="tagPrefixInformation">A <see cref="T:System.Web.Configuration.TagPrefixInfo" /> object in the collection.</param>
	public void Remove(TagPrefixInfo tagPrefixInformation)
	{
		BaseRemove(GetElementKey(tagPrefixInformation));
	}
}
