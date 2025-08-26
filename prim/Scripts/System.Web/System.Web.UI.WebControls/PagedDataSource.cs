using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Encapsulates the paging-related properties of a data-bound control (such as <see cref="T:System.Web.UI.WebControls.DataGrid" />, <see cref="T:System.Web.UI.WebControls.GridView" />, <see cref="T:System.Web.UI.WebControls.DetailsView" />, and <see cref="T:System.Web.UI.WebControls.FormView" />) that allow it to perform paging. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class PagedDataSource : ICollection, IEnumerable, ITypedList
{
	private int page_size;

	private int current_page_index;

	private int virtual_count;

	private bool allow_paging;

	private bool allow_custom_paging;

	private IEnumerable source;

	private bool allow_server_paging;

	/// <summary>Gets or sets a value indicating whether custom paging is enabled in a data-bound control.</summary>
	/// <returns>
	///     <see langword="true" /> if custom paging is enabled; otherwise, <see langword="false" />.</returns>
	public bool AllowCustomPaging
	{
		get
		{
			return allow_custom_paging;
		}
		set
		{
			allow_custom_paging = value;
			if (allow_custom_paging)
			{
				allow_server_paging = false;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether paging is enabled in a data-bound control.</summary>
	/// <returns>
	///     <see langword="true" /> if paging is enabled; otherwise, <see langword="false" />.</returns>
	public bool AllowPaging
	{
		get
		{
			return allow_paging;
		}
		set
		{
			allow_paging = value;
		}
	}

	/// <summary>Gets the number of items to be used from the data source.</summary>
	/// <returns>The number of items to be used from the data source.</returns>
	public int Count
	{
		get
		{
			if (source == null)
			{
				return 0;
			}
			if (IsPagingEnabled)
			{
				if (IsCustomPagingEnabled || !IsLastPage)
				{
					return page_size;
				}
				return DataSourceCount - FirstIndexInPage;
			}
			return DataSourceCount;
		}
	}

	/// <summary>Gets or sets the index of the current page.</summary>
	/// <returns>The index of the current page.</returns>
	public int CurrentPageIndex
	{
		get
		{
			return current_page_index;
		}
		set
		{
			current_page_index = value;
		}
	}

	/// <summary>Gets or sets the data source.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerable" /> implemented object that represents the data source.</returns>
	public IEnumerable DataSource
	{
		get
		{
			return source;
		}
		set
		{
			source = value;
		}
	}

	/// <summary>Gets the number of items in the data source.</summary>
	/// <returns>The number of items in the data source.</returns>
	/// <exception cref="T:System.Web.HttpException">The data source is not an <see cref="T:System.Collections.ICollection" /> implemented object.</exception>
	public int DataSourceCount
	{
		get
		{
			if (source == null)
			{
				return 0;
			}
			if (IsCustomPagingEnabled || IsServerPagingEnabled)
			{
				return virtual_count;
			}
			if (source is ICollection)
			{
				return ((ICollection)source).Count;
			}
			throw new HttpException("The data source must implement ICollection");
		}
	}

	/// <summary>Gets the index of the first record displayed on the page.</summary>
	/// <returns>The index of the first record displayed on the page.</returns>
	public int FirstIndexInPage
	{
		get
		{
			if (!IsPagingEnabled || IsCustomPagingEnabled || IsServerPagingEnabled || source == null)
			{
				return 0;
			}
			return current_page_index * page_size;
		}
	}

	/// <summary>Gets a value indicating whether custom paging is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if custom paging is enabled; otherwise, <see langword="false" />.</returns>
	public bool IsCustomPagingEnabled
	{
		get
		{
			if (IsPagingEnabled)
			{
				return allow_custom_paging;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether server-side paging support is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if paging is enabled and server-side paging is indicated using the <see cref="P:System.Web.UI.WebControls.PagedDataSource.AllowServerPaging" /> property; otherwise, <see langword="false" />.</returns>
	public bool IsServerPagingEnabled
	{
		get
		{
			if (IsPagingEnabled)
			{
				return allow_server_paging;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the current page is the first page.</summary>
	/// <returns>
	///     <see langword="true" /> if the current page is the first page; otherwise, <see langword="false" />.</returns>
	public bool IsFirstPage
	{
		get
		{
			if (!allow_paging)
			{
				return true;
			}
			return current_page_index == 0;
		}
	}

	/// <summary>Gets a value indicating whether the current page is the last page.</summary>
	/// <returns>
	///     <see langword="true" /> if the current page is the last page; otherwise, <see langword="false" />.</returns>
	public bool IsLastPage
	{
		get
		{
			if (!allow_paging || page_size == 0)
			{
				return true;
			}
			return current_page_index == PageCount - 1;
		}
	}

	/// <summary>Gets a value indicating whether paging is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if paging is enabled; otherwise, <see langword="false" />.</returns>
	public bool IsPagingEnabled
	{
		get
		{
			if (allow_paging)
			{
				return page_size != 0;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the data source is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets a value indicating whether access to the data source is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the total number of pages necessary to display all items in the data source.</summary>
	/// <returns>The number of pages necessary to display all items in the data source.</returns>
	public int PageCount
	{
		get
		{
			if (source == null)
			{
				return 0;
			}
			if (!IsPagingEnabled || DataSourceCount == 0 || page_size == 0)
			{
				return 1;
			}
			return (DataSourceCount + page_size - 1) / page_size;
		}
	}

	/// <summary>Gets or sets the number of items to display on a single page.</summary>
	/// <returns>The number of items to display on a single page.</returns>
	public int PageSize
	{
		get
		{
			return page_size;
		}
		set
		{
			page_size = value;
		}
	}

	/// <summary>Gets the object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Gets or sets the virtual number of items in the data source when custom paging is used.</summary>
	/// <returns>The virtual number of items in the data source when custom paging is used.</returns>
	public int VirtualCount
	{
		get
		{
			return virtual_count;
		}
		set
		{
			virtual_count = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether server-side paging is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if server-side paging is enabled; otherwise, <see langword="false" />.</returns>
	public bool AllowServerPaging
	{
		get
		{
			return allow_server_paging;
		}
		set
		{
			allow_server_paging = value;
			if (allow_server_paging)
			{
				allow_custom_paging = false;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> class.</summary>
	public PagedDataSource()
	{
		page_size = 10;
	}

	/// <summary>Copies all the items from the data source to the specified <see cref="T:System.Array" />, starting at the specified index in the <see cref="T:System.Array" />.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the data source. </param>
	/// <param name="index">The first position in the specified <see cref="T:System.Array" /> to receive the copied contents. </param>
	public void CopyTo(Array array, int index)
	{
		foreach (object item in source)
		{
			array.SetValue(item, index++);
		}
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all items in the data source.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all items in the data source.</returns>
	public IEnumerator GetEnumerator()
	{
		IList list = source as IList;
		int num = 0;
		if (list != null)
		{
			num = FirstIndexInPage;
			int count = ((ICollection)source).Count;
			int num2 = ((num + page_size > count) ? (count - num) : page_size);
			return GetListEnum(list, num, num + num2);
		}
		if (source is ICollection collection)
		{
			num = FirstIndexInPage;
			int count = collection.Count;
			int num2 = ((num + page_size > count) ? (count - num) : page_size);
			return GetEnumeratorEnum(collection.GetEnumerator(), num, num + page_size);
		}
		return source.GetEnumerator();
	}

	/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
	/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that contains the list name returned. This can be <see langword="null" />.</param>
	/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
	public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
	{
		if (!(source is ITypedList typedList))
		{
			return null;
		}
		return typedList.GetItemProperties(listAccessors);
	}

	/// <summary>Returns the name of the list. This method does not apply to this class.</summary>
	/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that contains the list name returned. This can be <see langword="null" />. </param>
	/// <returns>
	///     <see cref="F:System.String.Empty" /> for all cases.</returns>
	public string GetListName(PropertyDescriptor[] listAccessors)
	{
		return string.Empty;
	}

	private IEnumerator GetListEnum(IList list, int start, int end)
	{
		if (!AllowPaging)
		{
			end = list.Count;
		}
		else if (start >= list.Count)
		{
			yield break;
		}
		for (int i = start; i < end; i++)
		{
			yield return list[i];
		}
	}

	private IEnumerator GetEnumeratorEnum(IEnumerator e, int start, int end)
	{
		for (int i = 0; i < start; i++)
		{
			e.MoveNext();
		}
		for (int j = start; !allow_paging || j < end; j++)
		{
			if (!e.MoveNext())
			{
				break;
			}
			yield return e.Current;
		}
	}
}
