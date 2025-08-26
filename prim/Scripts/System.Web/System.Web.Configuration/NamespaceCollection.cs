using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Contains a collection of namespace objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(NamespaceInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class NamespaceCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty autoImportVBNamespaceProp;

	/// <summary>Gets or sets a value that determines whether the Visual Basic namespace is imported without having to specify it.</summary>
	/// <returns>
	///     <see langword="true" /> if the Visual Basic namespace is imported automatically; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("autoImportVBNamespace", DefaultValue = true)]
	public bool AutoImportVBNamespace
	{
		get
		{
			return (bool)base[autoImportVBNamespaceProp];
		}
		set
		{
			base[autoImportVBNamespaceProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index in the collection.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object in the collection.</param>
	/// <returns>
	///     <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index, or <see langword="null" /> if there is no object at that index.</returns>
	public NamespaceInfo this[int index]
	{
		get
		{
			return (NamespaceInfo)BaseGet(index);
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

	static NamespaceCollection()
	{
		autoImportVBNamespaceProp = new ConfigurationProperty("autoImportVBNamespace", typeof(bool), true);
		properties = new ConfigurationPropertyCollection();
		properties.Add(autoImportVBNamespaceProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.NamespaceCollection" /> class.</summary>
	public NamespaceCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to the collection.</summary>
	/// <param name="namespaceInformation">A <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to add already exists in the collection or the collection is read-only.</exception>
	public void Add(NamespaceInfo namespaceInformation)
	{
		BaseAdd(namespaceInformation);
	}

	/// <summary>Removes all <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new NamespaceInfo(null);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((NamespaceInfo)element).Namespace;
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.NamespaceInfo" /> object with the specified key from the collection.</summary>
	/// <param name="s">The namespace of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.NamespaceInfo" /> object with the specified key in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
	public void Remove(string s)
	{
		BaseRemove(s);
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> object from the specified index in the collection.</summary>
	/// <param name="index">The index of a <see cref="T:System.Web.Configuration.NamespaceInfo" /> object to remove from the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.NamespaceInfo" /> object at the specified index in the collection.- or -The element has already been removed.- or -The collection is read-only.</exception>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}
}
