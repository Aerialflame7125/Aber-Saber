using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(TransformerInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class TransformerInfoCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.TagMapInfo" /> object at the specified index location.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object in the collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.TransformerInfo" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public TransformerInfo this[int index]
	{
		get
		{
			return (TransformerInfo)BaseGet(index);
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

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TransformerInfoCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to the collection.</summary>
	/// <param name="transformerInfo">A <see cref="T:System.Web.Configuration.TransformerInfo" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.TransformerInfo" /> object to add already exists in the collection.- or -The collection is read-only. </exception>
	public void Add(TransformerInfo transformerInfo)
	{
		BaseAdd(transformerInfo);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.TransformerInfo" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new TransformerInfo("", "");
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((TransformerInfo)element).Name;
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.TransformerInfo" /> object with the passed <see cref="P:System.Web.Configuration.TransformerInfo.Name" /> property value from the collection.</summary>
	/// <param name="s">The name of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to remove from the collection.</param>
	public void Remove(string s)
	{
		BaseRemove(s);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.TransformerInfo" /> object from the collection at the passed index.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.TransformerInfo" /> object to remove from the collection.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TransformerInfoCollection" /> class.</summary>
	public TransformerInfoCollection()
	{
	}
}
