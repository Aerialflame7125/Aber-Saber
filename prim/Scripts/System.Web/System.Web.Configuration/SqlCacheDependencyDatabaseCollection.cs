using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects. This class cannot be inherited.</summary>
[ConfigurationCollection(typeof(SqlCacheDependencyDatabase), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
public sealed class SqlCacheDependencyDatabaseCollection : ConfigurationElementCollection
{
	/// <summary>Gets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> keys.</summary>
	/// <returns>The string array containing the collection keys.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = this[i].Name;
			}
			return array;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object at the specified index.</summary>
	/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object at the specified index.</returns>
	public SqlCacheDependencyDatabase this[int index]
	{
		get
		{
			return (SqlCacheDependencyDatabase)BaseGet(index);
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

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object with the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object with the specified name.</returns>
	public new SqlCacheDependencyDatabase this[string name] => (SqlCacheDependencyDatabase)BaseGet(name);

	/// <summary>Adds a <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to add to the collection.</param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object already exists in the collection or the collection is read only.</exception>
	public void Add(SqlCacheDependencyDatabase name)
	{
		BaseAdd(name);
	}

	/// <summary>Removes all the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects from the collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Returns the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element with the specified name.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element with the specified name.</returns>
	public SqlCacheDependencyDatabase Get(string name)
	{
		return (SqlCacheDependencyDatabase)BaseGet(name);
	}

	/// <summary>Returns the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element at the specified index.</returns>
	public SqlCacheDependencyDatabase Get(int index)
	{
		return (SqlCacheDependencyDatabase)BaseGet(index);
	}

	protected override ConfigurationElement CreateNewElement()
	{
		return new SqlCacheDependencyDatabase();
	}

	protected override object GetElementKey(ConfigurationElement element)
	{
		return ((SqlCacheDependencyDatabase)element).Name;
	}

	/// <summary>Returns the key for the element located at the specified index in the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" />.</summary>
	/// <param name="index">The index of the key to return.</param>
	/// <returns>The key at the specified index.</returns>
	public string GetKey(int index)
	{
		return Get(index)?.Name;
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> object with the specified name from the collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to remove from the collection.</param>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Removes the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> object at the specified index from the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to remove from the collection.</param>
	public void RemoveAt(int index)
	{
		BaseRemoveAt(index);
	}

	/// <summary>Resets a specified <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object that exists within the collection. </summary>
	/// <param name="user">The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> element to reset. </param>
	/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> is read-only or already exists.</exception>
	public void Set(SqlCacheDependencyDatabase user)
	{
		SqlCacheDependencyDatabase sqlCacheDependencyDatabase = Get(user.Name);
		if (sqlCacheDependencyDatabase == null)
		{
			Add(user);
			return;
		}
		int index = BaseIndexOf(sqlCacheDependencyDatabase);
		RemoveAt(index);
		BaseAdd(index, user);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> class.</summary>
	public SqlCacheDependencyDatabaseCollection()
	{
	}
}
