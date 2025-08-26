using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents a collection of menu items in a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited.</summary>
public sealed class MenuItemCollection : ICollection, IEnumerable, IStateManager
{
	private ArrayList items = new ArrayList();

	private Menu menu;

	private MenuItem parent;

	private bool marked;

	private bool dirty;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object at the specified index in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.MenuItem" /> to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.MenuItem" /> at the specified index in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</returns>
	public MenuItem this[int index] => (MenuItem)items[index];

	/// <summary>Gets the number of menu items contained in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <returns>The number of menu items contained in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</returns>
	public int Count => items.Count;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object is synchronized (thread safe).</summary>
	/// <returns>Always returns <see langword="false" />.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</returns>
	public object SyncRoot => items;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => marked;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> class using the default values.</summary>
	public MenuItemCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> class using the specified parent menu item (or owner).</summary>
	/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the parent menu item of the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
	public MenuItemCollection(MenuItem owner)
	{
		parent = owner;
		menu = owner.Menu;
	}

	internal MenuItemCollection(Menu menu)
	{
		this.menu = menu;
	}

	internal void SetMenu(Menu menu)
	{
		this.menu = menu;
		foreach (MenuItem item in items)
		{
			item.Menu = menu;
		}
	}

	/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to the end of the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <param name="child">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to append to the end of the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="child" /> is <see langword="null" />.</exception>
	public void Add(MenuItem child)
	{
		child.Index = items.Add(child);
		child.Menu = menu;
		child.SetParent(parent);
		if (marked)
		{
			((IStateManager)child).TrackViewState();
			SetDirty();
		}
	}

	internal void SetDirty()
	{
		for (int i = 0; i < Count; i++)
		{
			this[i].SetDirty();
		}
		dirty = true;
	}

	/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object at the specified index location.</summary>
	/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.MenuItem" />.</param>
	/// <param name="child">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to insert.</param>
	public void AddAt(int index, MenuItem child)
	{
		items.Insert(index, child);
		child.Index = index;
		child.Menu = menu;
		child.SetParent(parent);
		for (int i = index + 1; i < items.Count; i++)
		{
			((MenuItem)items[i]).Index = i;
		}
		if (marked)
		{
			((IStateManager)child).TrackViewState();
			SetDirty();
		}
	}

	/// <summary>Removes all items from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	public void Clear()
	{
		if (menu != null || parent != null)
		{
			foreach (MenuItem item in items)
			{
				item.Menu = null;
				item.SetParent(null);
			}
		}
		items.Clear();
		if (marked)
		{
			SetDirty();
		}
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is in the collection.</summary>
	/// <param name="c">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to find.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object is contained in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(MenuItem c)
	{
		return items.Contains(c);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="array" /> is not an array of <see cref="T:System.Web.UI.WebControls.MenuItem" /> objects.</exception>
	public void CopyTo(Array array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.MenuItem" /> objects, starting at the specified index in the target array.</summary>
	/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.MenuItem" /> objects that receives the copied items from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</param>
	/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
	public void CopyTo(MenuItem[] array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that can be used to iterate through the items in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <returns>An enumerator that can be used to iterate through the items in the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object in the collection.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> to locate.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(MenuItem value)
	{
		return items.IndexOf(value);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object from the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> object to remove.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="value" /> is <see langword="null" />.</exception>
	public void Remove(MenuItem value)
	{
		int num = IndexOf(value);
		if (num != -1)
		{
			items.RemoveAt(num);
			if (menu != null)
			{
				value.Menu = null;
			}
			if (marked)
			{
				SetDirty();
			}
		}
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.MenuItem" /> object at the specified index location from the current <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object.</summary>
	/// <param name="index">The zero-based index location of the menu item to remove.</param>
	public void RemoveAt(int index)
	{
		MenuItem menuItem = (MenuItem)items[index];
		items.RemoveAt(index);
		if (menu != null)
		{
			menuItem.Menu = null;
		}
		if (marked)
		{
			SetDirty();
		}
	}

	void ICollection.CopyTo(Array array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Loads the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object's previously saved view state.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values.</param>
	void IStateManager.LoadViewState(object state)
	{
		if (state == null)
		{
			return;
		}
		object[] array = (object[])state;
		dirty = (bool)array[0];
		if (dirty)
		{
			items.Clear();
			for (int i = 1; i < array.Length; i++)
			{
				MenuItem menuItem = new MenuItem();
				Add(menuItem);
				object obj = array[i];
				if (obj != null)
				{
					((IStateManager)menuItem).LoadViewState(obj);
				}
			}
		}
		else
		{
			for (int j = 1; j < array.Length; j++)
			{
				Pair pair = (Pair)array[j];
				int index = (int)pair.First;
				((IStateManager)(MenuItem)items[index]).LoadViewState(pair.Second);
			}
		}
	}

	/// <summary>Saves the changes to view state to an <see cref="T:System.Object" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object IStateManager.SaveViewState()
	{
		object[] array = null;
		bool flag = false;
		if (dirty)
		{
			if (items.Count > 0)
			{
				flag = true;
				array = new object[items.Count + 1];
				array[0] = true;
				for (int i = 0; i < items.Count; i++)
				{
					object obj = ((IStateManager)(items[i] as MenuItem)).SaveViewState();
					array[i + 1] = obj;
				}
			}
		}
		else
		{
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < items.Count; j++)
			{
				object obj2 = ((IStateManager)(items[j] as MenuItem)).SaveViewState();
				if (obj2 != null)
				{
					flag = true;
					arrayList.Add(new Pair(j, obj2));
				}
			}
			if (flag)
			{
				arrayList.Insert(0, false);
				array = arrayList.ToArray();
			}
		}
		if (flag)
		{
			return array;
		}
		return null;
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.WebControls.MenuItemCollection" /> object to track changes to its view state.</summary>
	void IStateManager.TrackViewState()
	{
		marked = true;
		for (int i = 0; i < items.Count; i++)
		{
			((IStateManager)items[i]).TrackViewState();
		}
	}
}
