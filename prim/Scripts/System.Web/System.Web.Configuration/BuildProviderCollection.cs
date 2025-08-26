using System.Collections;
using System.Configuration;
using System.Web.Compilation;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.BuildProvider" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(BuildProvider), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class BuildProviderCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection props;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProvider" /> object at the specified index of the collection.</summary>
	/// <param name="index">An integer value specifying a particular <see cref="T:System.Web.Configuration.BuildProvider" /> object within the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</returns>
	public BuildProvider this[int index]
	{
		get
		{
			return (BuildProvider)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProvider" /> collection element based on the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.BuildProvider" /> object to return from the collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</returns>
	public new BuildProvider this[string name]
	{
		get
		{
			string key = (string.IsNullOrEmpty(name) ? name : name.ToLowerInvariant());
			return (BuildProvider)BaseGet(key);
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return props;
		}
	}

	static BuildProviderCollection()
	{
		props = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BuildProviderCollection" /> class.</summary>
	public BuildProviderCollection()
		: base(CaseInsensitiveComparer.DefaultInvariant)
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.Configuration.BuildProvider" /> object to the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
	/// <param name="buildProvider">A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</param>
	public void Add(BuildProvider buildProvider)
	{
		BaseAdd(buildProvider);
	}

	/// <summary>Clears all <see cref="T:System.Web.Configuration.BuildProvider" /> objects from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.BuildProvider" /> object from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
	/// <param name="name">A string value specifying the <see cref="T:System.Web.Configuration.BuildProvider" /> reference.</param>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.BuildProvider" /> object at the specified index from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
	/// <param name="index">An integer value specifying the location of a specific <see cref="T:System.Web.Configuration.BuildProvider" /> object within the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new BuildProvider();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((BuildProvider)element).Extension;
	}

	internal Type GetProviderTypeForExtension(string extension)
	{
		return System.Web.Compilation.BuildProvider.GetProviderTypeForExtension(extension);
	}

	internal System.Web.Compilation.BuildProvider GetProviderInstanceForExtension(string extension)
	{
		return System.Web.Compilation.BuildProvider.GetProviderInstanceForExtension(extension);
	}
}
