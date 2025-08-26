using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.AssemblyInfo" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(AssemblyInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class AssemblyCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.AssemblyInfo" /> at the specified index in the <see cref="T:System.Web.Configuration.AssemblyCollection" />.</summary>
	/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.AssemblyInfo" /> object within the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</param>
	/// <returns>A <see cref="T:System.Web.Configuration.AssemblyInfo" /> object.</returns>
	public AssemblyInfo this[int index]
	{
		get
		{
			return (AssemblyInfo)BaseGet(index);
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

	/// <summary>Gets the item identified by the specified assembly name.</summary>
	/// <param name="assemblyName">The name identifying the assembly to retrieve.</param>
	/// <returns>The item identified by the specified assembly name.</returns>
	public new AssemblyInfo this[string assemblyName] => (AssemblyInfo)BaseGet(assemblyName);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AssemblyCollection()
	{
		properties = new ConfigurationPropertyCollection();
	}

	/// <summary>Adds an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object to the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
	/// <param name="assemblyInformation">A string value specifying the assembly reference.</param>
	public void Add(AssemblyInfo assemblyInformation)
	{
		BaseAdd(assemblyInformation, throwIfExists: false);
	}

	/// <summary>Clears all the <see cref="T:System.Web.Configuration.AssemblyInfo" /> objects from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new AssemblyInfo();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((AssemblyInfo)element).Assembly;
	}

	/// <summary>Removes a <see cref="T:System.Web.Configuration.AssemblyInfo" /> object from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
	/// <param name="key">A string value specifying the assembly reference.</param>
	public void Remove(string key)
	{
		BaseRemove(key);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
	/// <param name="index">An integer value specifying an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object within the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.AssemblyCollection" /> class.</summary>
	public AssemblyCollection()
	{
	}
}
