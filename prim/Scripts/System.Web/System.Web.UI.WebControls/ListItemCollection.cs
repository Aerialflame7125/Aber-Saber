using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A collection of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in a list control. This class cannot be inherited.</summary>
[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ListItemCollection : IList, ICollection, IEnumerable, IStateManager
{
	private ArrayList items;

	private bool tracking;

	private bool dirty;

	private int lastDirty;

	/// <summary>Gets or sets the maximum number of items that the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> can store.</summary>
	/// <returns>The maximum number of items that the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> can store.</returns>
	public int Capacity
	{
		get
		{
			return items.Capacity;
		}
		set
		{
			items.Capacity = value;
		}
	}

	/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the collection.</returns>
	public int Count => items.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> is read-only.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsReadOnly => items.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public bool IsSynchronized => items.IsSynchronized;

	/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => items.SyncRoot;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.ListItem" /> at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.ListItem" /> object at the specified index in the collection.</returns>
	public ListItem this[int index] => (ListItem)items[index];

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
	/// <returns>
	///     <see langword="false" />. </returns>
	bool IList.IsFixedSize => items.IsFixedSize;

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
	/// <param name="index">The zero-based index of the element to get. </param>
	/// <returns>The element as the specified index.</returns>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			if (index >= 0 && index < items.Count)
			{
				items[index] = (ListItem)value;
				if (tracking)
				{
					((ListItem)value).TrackViewState();
				}
			}
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the server control is tracking its view state change; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => tracking;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> class.</summary>
	public ListItemCollection()
	{
		items = new ArrayList();
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> to the end of the collection.</summary>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.ListItem" /> to append to the collection. </param>
	public void Add(ListItem item)
	{
		items.Add(item);
		if (tracking)
		{
			item.TrackViewState();
			SetDirty();
		}
	}

	/// <summary>Appends a <see cref="T:System.Web.UI.WebControls.ListItem" /> to the end of the collection that represents the specified string.</summary>
	/// <param name="item">A <see cref="T:System.String" /> that represents the item to add to the end of the collection. </param>
	public void Add(string item)
	{
		ListItem listItem = new ListItem(item);
		items.Add(listItem);
		if (tracking)
		{
			listItem.TrackViewState();
			SetDirty();
		}
	}

	/// <summary>Adds the items in an array of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects to the collection.</summary>
	/// <param name="items">An array of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects that contain the items to add to the collection. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="items" /> is <see langword="null" />.</exception>
	public void AddRange(ListItem[] items)
	{
		for (int i = 0; i < items.Length; i++)
		{
			Add(items[i]);
			if (tracking)
			{
				items[i].TrackViewState();
				SetDirty();
			}
		}
	}

	/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.ListItem" /> objects from the collection.</summary>
	public void Clear()
	{
		items.Clear();
		if (tracking)
		{
			SetDirty();
		}
	}

	/// <summary>Determines whether the collection contains the specified item.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.ListItem" /> to search for in the collection. </param>
	/// <returns>
	///     <see langword="true" /> if the collection contains the specified item; otherwise, <see langword="false" />.</returns>
	public bool Contains(ListItem item)
	{
		return items.Contains(item);
	}

	/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> to the specified <see cref="T:System.Array" />, starting with the specified index.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />. </param>
	/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
	public void CopyTo(Array array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Searches the collection for a <see cref="T:System.Web.UI.WebControls.ListItem" /> with a <see cref="P:System.Web.UI.WebControls.ListItem.Text" /> property that equals the specified text.</summary>
	/// <param name="text">The text to search for. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that contains the text specified by the <paramref name="text" /> parameter.</returns>
	public ListItem FindByText(string text)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (text == this[i].Text)
			{
				return this[i];
			}
		}
		return null;
	}

	/// <summary>Searches the collection for a <see cref="T:System.Web.UI.WebControls.ListItem" /> with a <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property that contains the specified value.</summary>
	/// <param name="value">The value to search for. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that contains the value specified by the <paramref name="value" /> parameter.</returns>
	public ListItem FindByValue(string value)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (value == this[i].Value)
			{
				return this[i];
			}
		}
		return null;
	}

	/// <summary>Returns a <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</summary>
	/// <returns>A <see cref="T:System.Collections.IEnumerator" /> implemented object that contains all <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Determines the index value that represents the position of the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> in the collection.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.ListItem" /> to search for in the collection. </param>
	/// <returns>The index position of the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> in the collection.</returns>
	public int IndexOf(ListItem item)
	{
		return items.IndexOf(item);
	}

	internal int IndexOf(string value)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (value == this[i].Value)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> in the collection at the specified index location.</summary>
	/// <param name="index">The location in the collection to insert the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.ListItem" /> to add to the collection. </param>
	public void Insert(int index, ListItem item)
	{
		items.Insert(index, item);
		if (tracking)
		{
			item.TrackViewState();
			lastDirty = index;
			SetDirty();
		}
	}

	/// <summary>Inserts a <see cref="T:System.Web.UI.WebControls.ListItem" /> which represents the specified string in the collection at the specified index location.</summary>
	/// <param name="index">The location in the collection to insert the <see cref="T:System.Web.UI.WebControls.ListItem" />. </param>
	/// <param name="item">A <see cref="T:System.String" /> that represents the item to insert in the collection. </param>
	public void Insert(int index, string item)
	{
		ListItem listItem = new ListItem(item);
		items.Insert(index, listItem);
		if (tracking)
		{
			listItem.TrackViewState();
			lastDirty = index;
			SetDirty();
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.ListItem" /> from the collection.</summary>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.ListItem" /> to remove from the collection. </param>
	public void Remove(ListItem item)
	{
		items.Remove(item);
		if (tracking)
		{
			SetDirty();
		}
	}

	/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.ListItem" /> from the collection that represents the specified string.</summary>
	/// <param name="item">A <see cref="T:System.String" /> that represents the item to remove from the collection. </param>
	public void Remove(string item)
	{
		for (int i = 0; i < items.Count; i++)
		{
			if (item == this[i].Value)
			{
				items.RemoveAt(i);
				if (tracking)
				{
					SetDirty();
				}
			}
		}
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.ListItem" /> at the specified index from the collection.</summary>
	/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to remove. </param>
	public void RemoveAt(int index)
	{
		items.RemoveAt(index);
		if (tracking)
		{
			SetDirty();
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
	/// <param name="item">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
	/// <returns>The index at which the item has been added. </returns>
	int IList.Add(object value)
	{
		int result = items.Add((ListItem)value);
		if (tracking)
		{
			((IStateManager)value).TrackViewState();
			SetDirty();
		}
		return result;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
	/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />. </returns>
	bool IList.Contains(object value)
	{
		return Contains((ListItem)value);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />. </summary>
	/// <param name="item">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
	/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1. </returns>
	int IList.IndexOf(object value)
	{
		return IndexOf((ListItem)value);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />. </summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="item">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
	void IList.Insert(int index, object value)
	{
		Insert(index, (ListItem)value);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />. </summary>
	/// <param name="item">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
	void IList.Remove(object value)
	{
		Remove((ListItem)value);
	}

	/// <summary>Loads the previously saved state.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		if (!(savedState is Pair pair))
		{
			return;
		}
		bool flag = (bool)pair.First;
		object[] array = (object[])pair.Second;
		int num = ((array != null) ? array.Length : 0);
		if (flag)
		{
			if (num > 0)
			{
				items = new ArrayList(num);
			}
			else
			{
				items = new ArrayList();
			}
		}
		for (int i = 0; i < num; i++)
		{
			ListItem listItem = new ListItem();
			if (flag)
			{
				listItem.LoadViewState(array[i]);
				listItem.SetDirty();
				Add(listItem);
			}
			else if (array[i] != null)
			{
				listItem.LoadViewState(array[i]);
				listItem.SetDirty();
				items[i] = listItem;
			}
		}
	}

	/// <summary>Returns object containing state changes. </summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListItemCollection" />.</returns>
	object IStateManager.SaveViewState()
	{
		bool flag = false;
		int count = items.Count;
		if (count == 0 && !dirty)
		{
			return null;
		}
		object[] array = null;
		if (count > 0)
		{
			array = new object[count];
		}
		for (int i = 0; i < count; i++)
		{
			array[i] = ((IStateManager)items[i]).SaveViewState();
			if (array[i] != null)
			{
				flag = true;
			}
		}
		if (!dirty && !flag)
		{
			return null;
		}
		return new Pair(dirty, array);
	}

	/// <summary>Starts tracking state of changes.</summary>
	void IStateManager.TrackViewState()
	{
		tracking = true;
		for (int i = 0; i < items.Count; i++)
		{
			((ListItem)items[i]).TrackViewState();
		}
	}

	private void SetDirty()
	{
		dirty = true;
		for (int i = lastDirty; i < items.Count; i++)
		{
			((ListItem)items[i]).SetDirty();
		}
		lastDirty = items.Count - 1;
		if (lastDirty < 0)
		{
			lastDirty = 0;
		}
	}
}
