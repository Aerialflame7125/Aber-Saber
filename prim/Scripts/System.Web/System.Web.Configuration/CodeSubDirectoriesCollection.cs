using System.Collections;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.CodeSubDirectory" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(CodeSubDirectory), CollectionType = ConfigurationElementCollectionType.BasicMap)]
public sealed class CodeSubDirectoriesCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection props;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> at the specified index in the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection.</summary>
	/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object within the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object.</returns>
	public CodeSubDirectory this[int index]
	{
		get
		{
			return (CodeSubDirectory)BaseGet(index);
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

	/// <summary>Gets the type of the configuration collection.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
	public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

	protected override string ElementName => "add";

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return props;
		}
	}

	static CodeSubDirectoriesCollection()
	{
		props = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> class.</summary>
	public CodeSubDirectoriesCollection()
		: base(CaseInsensitiveComparer.DefaultInvariant)
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.CodeSubDirectory" /> object to the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" />.</summary>
	/// <param name="codeSubDirectory">A string value specifying the <see cref="T:System.Web.Configuration.CodeSubDirectory" /> reference.</param>
	public void Add(CodeSubDirectory codeSubDirectory)
	{
		BaseAdd(codeSubDirectory);
	}

	/// <summary>Removes all items from the collection</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new CodeSubDirectory(null);
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((CodeSubDirectory)element).DirectoryName;
	}

	/// <summary>Removes the specified item from the collection.</summary>
	/// <param name="directoryName">The name of the directory to remove.</param>
	public void Remove(string directoryName)
	{
		BaseRemove(directoryName);
	}

	/// <summary>Removes the item at the specified index in the collection.</summary>
	/// <param name="index">The index position of the item to be removed.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}
}
