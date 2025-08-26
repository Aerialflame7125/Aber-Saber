using System.Collections;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(ExpressionBuilder), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class ExpressionBuilderCollection : ConfigurationElementCollection
{
	private static ConfigurationPropertyCollection props;

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> at the specified index in the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
	/// <param name="index">An integer value specifying an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object within the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object at the specified index or <see langword="null" /> if there is no object at that index.</returns>
	public ExpressionBuilder this[int index]
	{
		get
		{
			return (ExpressionBuilder)BaseGet(index);
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object with the specified name.</summary>
	/// <param name="name">The name of an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object in the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> or <see langword="null" /> if there is no object with that name in the collection.</returns>
	public new ExpressionBuilder this[string name] => (ExpressionBuilder)BaseGet(name);

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return props;
		}
	}

	static ExpressionBuilderCollection()
	{
		props = new ConfigurationPropertyCollection();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> class.</summary>
	public ExpressionBuilderCollection()
		: base(CaseInsensitiveComparer.DefaultInvariant)
	{
	}

	/// <summary>Adds an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object to the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" />.</summary>
	/// <param name="buildProvider">A string value specifying the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> reference.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object to add already exists in the collection, or the collection is read-only.</exception>
	public void Add(ExpressionBuilder buildProvider)
	{
		BaseAdd(buildProvider);
	}

	/// <summary>Clears all the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
	/// <param name="name">A string value specifying the <see cref="T:System.Web.Configuration.ExpressionBuilder" /> reference.</param>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes an <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object from the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</summary>
	/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.ExpressionBuilder" /> object within the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> collection.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new ExpressionBuilder();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((ExpressionBuilder)element).ExpressionPrefix;
	}
}
