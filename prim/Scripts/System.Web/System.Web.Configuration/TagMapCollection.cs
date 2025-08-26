using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects. </summary>
[ConfigurationCollection(typeof(TagMapInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class TagMapCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index location.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TagMapInfo" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public TagMapInfo this[int index]
	{
		get
		{
			return (TagMapInfo)BaseGet(index);
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

	static TagMapCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TagMapCollection" /> class.</summary>
	public TagMapCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.TagMapInfo" /> object to the collection.</summary>
	/// <param name="tagMapInformation">A <see cref="T:System.Web.Configuration.TagMapInfo" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TagMapInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
	public void Add(TagMapInfo tagMapInformation)
	{
		BaseAdd(tagMapInformation);
	}

	/// <summary>Clears all object from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new TagMapInfo();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((TagMapInfo)element).TagType;
	}

	/// <summary>Removes the specified object from the collection.</summary>
	/// <param name="tagMapInformation">A <see cref="T:System.Web.Configuration.TagMapInfo" /> object in the collection.</param>
	public void Remove(TagMapInfo tagMapInformation)
	{
		BaseRemove(tagMapInformation.TagType);
	}
}
