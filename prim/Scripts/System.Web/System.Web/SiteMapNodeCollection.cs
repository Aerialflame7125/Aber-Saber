using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web;

/// <summary>Provides a strongly typed collection for <see cref="T:System.Web.SiteMapNode" /> objects and implements the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> interface to support navigating through the collection. </summary>
public class SiteMapNodeCollection : IList, ICollection, IEnumerable, IHierarchicalEnumerable
{
	private ArrayList list;

	internal static SiteMapNodeCollection EmptyList;

	internal static SiteMapNodeCollection EmptyCollection => EmptyList;

	private ArrayList List
	{
		get
		{
			if (list == null)
			{
				list = new ArrayList();
			}
			return list;
		}
	}

	/// <summary>Gets the number of elements contained in the collection.</summary>
	/// <returns>The number of elements in the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	public virtual int Count
	{
		get
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}
	}

	/// <summary>Gets a Boolean value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="true" /> if access is synchronized; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the  collection. </summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	public virtual object SyncRoot => this;

	/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object at the specified index in the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.SiteMapNode" /> to find. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents an element in the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is great than the <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The value supplied to the setter is <see langword="null" />.</exception>
	public virtual SiteMapNode this[int index]
	{
		get
		{
			return (SiteMapNode)List[index];
		}
		set
		{
			List[index] = value;
		}
	}

	/// <summary>Gets a Boolean value indicating whether nodes can be added to or subtracted from the collection.</summary>
	/// <returns>
	///     <see langword="true" /> if you can add <see cref="T:System.Web.SiteMapNode" /> objects to or remove <see cref="T:System.Web.SiteMapNode" /> objects from the <see cref="T:System.Web.SiteMapNodeCollection" />; otherwise, <see langword="false" />. </returns>
	public virtual bool IsFixedSize => List.IsFixedSize;

	/// <summary>Gets a Boolean value indicating whether the collection is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if you can modify the <see cref="T:System.Web.SiteMapNodeCollection" />; otherwise, <see langword="false" />.</returns>
	public virtual bool IsReadOnly
	{
		get
		{
			if (list != null)
			{
				return list.IsReadOnly;
			}
			return false;
		}
	}

	/// <summary>Gets the <see cref="T:System.Collections.IList" /> element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element to get.</param>
	/// <returns>The element at the specified index.</returns>
	object IList.this[int index]
	{
		get
		{
			return List[index];
		}
		set
		{
			OnValidate(value);
			List[index] = value;
		}
	}

	/// <summary>Gets a <see langword="Boolean" /> value indicating whether the collection has a fixed size. For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
	bool IList.IsFixedSize => IsFixedSize;

	/// <summary>Gets a <see langword="Boolean" /> value indicating whether the collection is read-only. For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
	bool IList.IsReadOnly => IsReadOnly;

	/// <summary>Gets the number of elements that are contained in the <see cref="T:System.Collections.ICollection" /> interface. This class cannot be inherited.</summary>
	/// <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
	int ICollection.Count => Count;

	/// <summary>Gets a <see langword="Boolean" /> value indicating whether access to the <see cref="T:System.Collections.ICollection" /> interface is synchronized (thread safe). This class cannot be inherited.</summary>
	/// <returns>
	///     <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool ICollection.IsSynchronized => IsSynchronized;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" /> interface. This class cannot be inherited.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
	object ICollection.SyncRoot => SyncRoot;

	static SiteMapNodeCollection()
	{
		EmptyList = new SiteMapNodeCollection();
		EmptyList.list = ArrayList.ReadOnly(new ArrayList());
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class, which is the default instance.</summary>
	public SiteMapNodeCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class with the specified initial capacity.</summary>
	/// <param name="capacity">The initial capacity of the <see cref="T:System.Web.SiteMapNodeCollection" />.</param>
	public SiteMapNodeCollection(int capacity)
	{
		list = new ArrayList(capacity);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class and adds the <see cref="T:System.Web.SiteMapNode" /> object to the <see cref="P:System.Collections.CollectionBase.InnerList" /> property for the collection.</summary>
	/// <param name="value">A <see cref="T:System.Web.SiteMapNode" /> to add to the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public SiteMapNodeCollection(SiteMapNode value)
	{
		Add(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class and adds the array of type <see cref="T:System.Web.SiteMapNode" /> to the <see cref="P:System.Collections.CollectionBase.InnerList" /> property for the collection.</summary>
	/// <param name="value">An array of type <see cref="T:System.Web.SiteMapNode" /> to add to the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public SiteMapNodeCollection(SiteMapNode[] value)
	{
		AddRangeInternal(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNodeCollection" /> class and adds all the list items of the specified <see cref="T:System.Web.SiteMapNodeCollection" /> collection to the <see cref="P:System.Collections.CollectionBase.InnerList" /> property for the collection.</summary>
	/// <param name="value">A <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the <see cref="T:System.Web.SiteMapNode" /> to add to the current <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public SiteMapNodeCollection(SiteMapNodeCollection value)
	{
		AddRangeInternal(value);
	}

	/// <summary>Retrieves a reference to an enumerator object, which is used to iterate over the collection. </summary>
	/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" />.</returns>
	public virtual IEnumerator GetEnumerator()
	{
		if (list == null)
		{
			return Type.EmptyTypes.GetEnumerator();
		}
		return list.GetEnumerator();
	}

	/// <summary>Removes all items from the collection.</summary>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.</exception>
	public virtual void Clear()
	{
		if (list != null)
		{
			list.Clear();
		}
	}

	/// <summary>Removes the <see cref="T:System.Web.SiteMapNode" /> object at the specified index of the  collection.</summary>
	/// <param name="index">The zero-based index of the element to remove. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.- or -The <see cref="T:System.Web.SiteMapNodeCollection" /> has a fixed sized.</exception>
	public virtual void RemoveAt(int index)
	{
		List.RemoveAt(index);
	}

	/// <summary>Adds a single <see cref="T:System.Web.SiteMapNode" /> object to the  collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to add to the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <returns>The index of the <see cref="P:System.Collections.CollectionBase.InnerList" /> where the <see cref="T:System.Web.SiteMapNode" /> was inserted.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
	public virtual int Add(SiteMapNode value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		return List.Add(value);
	}

	/// <summary>Adds an array of type <see cref="T:System.Web.SiteMapNode" /> to the collection.</summary>
	/// <param name="value">An array of type <see cref="T:System.Web.SiteMapNode" /> to add to the current <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="value" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
	public virtual void AddRange(SiteMapNode[] value)
	{
		AddRangeInternal(value);
	}

	/// <summary>Adds the nodes in the specified  <see cref="T:System.Web.SiteMapNodeCollection" /> to the current collection.</summary>
	/// <param name="value">A <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the <see cref="T:System.Web.SiteMapNode" /> objects to add to the current <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="value" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only. </exception>
	public virtual void AddRange(SiteMapNodeCollection value)
	{
		AddRangeInternal(value);
	}

	internal virtual void AddRangeInternal(IList value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		List.AddRange(value);
	}

	/// <summary>Determines whether the collection contains a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
	/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to locate in the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.SiteMapNodeCollection" /> contains the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, <see langword="false" />.</returns>
	public virtual bool Contains(SiteMapNode value)
	{
		return List.Contains(value);
	}

	/// <summary>Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
	/// <param name="array">The one-dimensional array that must have zero-based indexing and is the destination of the elements copied from the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is multidimensional.-or- The number of <see cref="T:System.Web.SiteMapNode" /> objects in the source <see cref="T:System.Web.SiteMapNodeCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
	public virtual void CopyTo(SiteMapNode[] array, int index)
	{
		List.CopyTo(array, index);
	}

	/// <summary>Searches for the specified <see cref="T:System.Web.SiteMapNode" /> object, and then returns the zero-based index of the first occurrence within the entire collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to locate in the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Web.SiteMapNodeCollection" />, if found; otherwise, -1.</returns>
	public virtual int IndexOf(SiteMapNode value)
	{
		return List.IndexOf(value);
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.SiteMapNode" /> object into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which the <see cref="T:System.Web.SiteMapNode" /> is inserted. </param>
	/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to insert. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.-or- 
	///         <paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" />. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.-or- The <see cref="T:System.Web.SiteMapNodeCollection" /> has a fixed size. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public virtual void Insert(int index, SiteMapNode value)
	{
		List.Insert(index, value);
	}

	/// <summary>Performs additional custom processes when validating a value.</summary>
	/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to validate. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="value" /> is not a <see cref="T:System.Web.SiteMapNode" />.</exception>
	protected virtual void OnValidate(object value)
	{
		if (!(value is SiteMapNode))
		{
			throw new ArgumentException("Invalid type");
		}
	}

	/// <summary>Returns a read-only collection that contains the nodes in the specified <see cref="T:System.Web.SiteMapNodeCollection" /> collection.</summary>
	/// <param name="collection">The <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the <see cref="T:System.Web.SiteMapNode" /> objects to add to the read-only <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> with the same <see cref="T:System.Web.SiteMapNode" /> elements and structure as the original <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="collection" /> is <see langword="null" />.</exception>
	public static SiteMapNodeCollection ReadOnly(SiteMapNodeCollection collection)
	{
		SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection();
		if (collection.list != null)
		{
			siteMapNodeCollection.list = ArrayList.ReadOnly(collection.list);
		}
		else
		{
			siteMapNodeCollection.list = ArrayList.ReadOnly(new ArrayList());
		}
		return siteMapNodeCollection;
	}

	/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.SiteMapNode" /> to remove from the <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="value" /> does not exist in the collection. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapNodeCollection" /> is read-only.-or- The <see cref="T:System.Web.SiteMapNodeCollection" /> has a fixed size. </exception>
	public virtual void Remove(SiteMapNode value)
	{
		List.Remove(value);
	}

	/// <summary>Returns a hierarchical data item for the specified enumerated item.</summary>
	/// <param name="enumeratedItem">The object for which to return an <see cref="T:System.Web.UI.IHierarchyData" />.</param>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchyData" /> that represents the object passed to the <see cref="M:System.Web.SiteMapNodeCollection.GetHierarchyData(System.Object)" />.</returns>
	public virtual IHierarchyData GetHierarchyData(object enumeratedItem)
	{
		return enumeratedItem as IHierarchyData;
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> object that is associated with the nodes in the current collection.</summary>
	/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control that the view is associated with.</param>
	/// <param name="viewName">The name of the view.</param>
	/// <returns>A named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> for the <see cref="T:System.Web.SiteMapNode" /> objects in the current <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
	{
		return new SiteMapDataSourceView(owner, viewName, this);
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> object that is associated with the nodes in the current collection.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> for the <see cref="T:System.Web.SiteMapNode" /> objects in the current <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
	{
		return new SiteMapHierarchicalDataSourceView(this);
	}

	/// <summary>Adds an item to the collection in the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
	/// <param name="value">The object to add to the <see cref="T:System.Collections.IList" />.</param>
	/// <returns>The position into which the new element was inserted.</returns>
	int IList.Add(object value)
	{
		OnValidate(value);
		return List.Add(value);
	}

	/// <summary>Determines whether the collection in the <see cref="T:System.Collections.IList" /> interface contains the specified Boolean value.</summary>
	/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
	/// <returns>
	///     <see langword="true" /> if the object is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
	bool IList.Contains(object value)
	{
		return List.Contains(value);
	}

	/// <summary>Determines the index of the specific item in the collection that is returned by the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
	/// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
	/// <returns>The index of the value, in the list, if found; otherwise, -1.</returns>
	int IList.IndexOf(object value)
	{
		return List.IndexOf(value);
	}

	/// <summary>Inserts an item into the collection in the <see cref="T:System.Collections.IList" /> interface at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
	/// <param name="index">The zero-based <paramref name="index" /> at which to insert <paramref name="value" />.</param>
	/// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
	void IList.Insert(int index, object value)
	{
		OnValidate(value);
		List.Insert(index, value);
	}

	/// <summary>Removes the first occurrence of a specified object from the collection in the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
	/// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
	void IList.Remove(object value)
	{
		OnValidate(value);
		List.Remove(value);
	}

	/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> interface to an array, starting at a particular array index. This class cannot be inherited.</summary>
	/// <param name="array">A one-dimensional array that must have zero-based indexing and is the destination of the elements copied from the <see cref="T:System.Collections.CollectionBase" />. </param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="array" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is multidimensional.-or- The number of <see cref="T:System.Web.SiteMapNode" /> objects in the source <see cref="T:System.Web.SiteMapNodeCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
	void ICollection.CopyTo(Array array, int index)
	{
		List.CopyTo(array, index);
	}

	/// <summary>Removes all items from the collection in the <see cref="T:System.Collections.IList" /> interface. For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
	/// <param name="index">The zero-based index of the item to remove.</param>
	void IList.RemoveAt(int index)
	{
		RemoveAt(index);
	}

	/// <summary>Returns an enumerator that iterates through a collection. For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.SiteMapNodeCollection" />.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>Returns a hierarchical data item for the specified enumerated item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchicalEnumerable.GetHierarchyData(System.Object)" />.</summary>
	/// <param name="enumeratedItem">The object for which to return an <see cref="T:System.Web.UI.IHierarchyData" />. </param>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchyData" /> that represents the object passed to the <see cref="M:System.Web.SiteMapNodeCollection.System#Web#UI#IHierarchicalEnumerable#GetHierarchyData(System.Object)" />.</returns>
	IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
	{
		return GetHierarchyData(enumeratedItem);
	}
}
