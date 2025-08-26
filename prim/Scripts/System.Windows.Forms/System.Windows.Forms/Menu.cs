using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents the base functionality for all menus. Although <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>1</filterpriority>
[ListBindable(false)]
[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow)]
public abstract class Menu : Component
{
	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
	[ListBindable(false)]
	public class MenuItemCollection : ICollection, IEnumerable, IList
	{
		private Menu owner;

		private ArrayList items = new ArrayList();

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => false;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</returns>
		object ICollection.SyncRoot => this;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => false;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		object IList.this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value indicating the total number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</returns>
		public int Count => items.Count;

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		public bool IsReadOnly => false;

		/// <summary>Retrieves the <see cref="T:System.Windows.Forms.MenuItem" /> at the specified indexed location in the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified location.</returns>
		/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.MenuItem" /> in the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is null.or The <paramref name="index" /> parameter is less than zero.or The <paramref name="index" /> parameter is greater than the number of menu items in the collection, and the collection of menu items is not null. </exception>
		public virtual MenuItem this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				return (MenuItem)items[index];
			}
		}

		/// <summary>Gets an item with the specified key from the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> with the specified key.</returns>
		/// <param name="key">The name of the item to retrieve from the collection.</param>
		public virtual MenuItem this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				foreach (MenuItem item in items)
				{
					if (string.Compare(item.Name, key, ignoreCase: true) == 0)
					{
						return item;
					}
				}
				return null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.Menu" /> that owns this collection. </param>
		public MenuItemCollection(Menu owner)
		{
			this.owner = owner;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The position into which the <see cref="T:System.Windows.Forms.MenuItem" /> was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to add to the collection.</param>
		int IList.Add(object value)
		{
			return Add((MenuItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if the specified object is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise, false.</returns>
		/// <param name="value">The object to locate in the collection.</param>
		bool IList.Contains(object value)
		{
			return Contains((MenuItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The zero-based index if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
		int IList.IndexOf(object value)
		{
			return IndexOf((MenuItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.MenuItem" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to insert into the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</param>
		void IList.Insert(int index, object value)
		{
			Insert(index, (MenuItem)value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
		void IList.Remove(object value)
		{
			Remove((MenuItem)value);
		}

		/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu.</summary>
		/// <returns>The zero-based index where the item is stored in the collection.</returns>
		/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add. </param>
		public virtual int Add(MenuItem item)
		{
			if (item.Parent != null)
			{
				item.Parent.MenuItems.Remove(item);
			}
			items.Add(item);
			item.Index = items.Count - 1;
			UpdateItem(item);
			owner.OnMenuChanged(EventArgs.Empty);
			if (owner.parent_menu != null)
			{
				owner.parent_menu.OnMenuChanged(EventArgs.Empty);
			}
			return items.Count - 1;
		}

		internal void AddNoEvents(MenuItem mi)
		{
			if (mi.Parent != null)
			{
				mi.Parent.MenuItems.Remove(mi);
			}
			items.Add(mi);
			mi.Index = items.Count - 1;
			mi.parent_menu = owner;
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" />, to the end of the current menu, with a specified caption.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
		/// <param name="caption">The caption of the menu item. </param>
		public virtual MenuItem Add(string caption)
		{
			MenuItem menuItem = new MenuItem(caption);
			Add(menuItem);
			return menuItem;
		}

		/// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index within the menu item collection.</summary>
		/// <returns>The zero-based index where the item is stored in the collection.</returns>
		/// <param name="index">The position to add the new item. </param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add. </param>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.MenuItem" /> being added is already in use. </exception>
		/// <exception cref="T:System.ArgumentException">The index supplied in the <paramref name="index" /> parameter is larger than the size of the collection. </exception>
		public virtual int Add(int index, MenuItem item)
		{
			if (index < 0 || index > Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			ArrayList arrayList = new ArrayList(Count + 1);
			for (int i = 0; i < index; i++)
			{
				arrayList.Add(items[i]);
			}
			arrayList.Add(item);
			for (int j = index; j < Count; j++)
			{
				arrayList.Add(items[j]);
			}
			items = arrayList;
			UpdateItemsIndices();
			UpdateItem(item);
			return index;
		}

		private void UpdateItem(MenuItem mi)
		{
			mi.parent_menu = owner;
			owner.OnMenuChanged(EventArgs.Empty);
			if (owner.parent_menu != null)
			{
				owner.parent_menu.OnMenuChanged(EventArgs.Empty);
			}
			if (owner.Tracker != null)
			{
				owner.Tracker.AddShortcuts(mi);
			}
		}

		internal void Insert(int index, MenuItem mi)
		{
			if (index < 0 || index > Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			items.Insert(index, mi);
			UpdateItemsIndices();
			UpdateItem(mi);
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu with a specified caption and a specified event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
		/// <param name="caption">The caption of the menu item. </param>
		/// <param name="onClick">An <see cref="T:System.EventHandler" /> that represents the event handler that is called when the item is clicked by the user, or when a user presses an accelerator or shortcut key for the menu item. </param>
		public virtual MenuItem Add(string caption, EventHandler onClick)
		{
			MenuItem menuItem = new MenuItem(caption, onClick);
			Add(menuItem);
			return menuItem;
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of this menu with the specified caption, <see cref="E:System.Windows.Forms.MenuItem.Click" /> event handler, and items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
		/// <param name="caption">The caption of the menu item. </param>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that this <see cref="T:System.Windows.Forms.MenuItem" /> will contain. </param>
		public virtual MenuItem Add(string caption, MenuItem[] items)
		{
			MenuItem menuItem = new MenuItem(caption, items);
			Add(menuItem);
			return menuItem;
		}

		/// <summary>Adds an array of previously created <see cref="T:System.Windows.Forms.MenuItem" /> objects to the collection.</summary>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects representing the menu items to add to the collection. </param>
		public virtual void AddRange(MenuItem[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (MenuItem item in items)
			{
				Add(item);
			}
		}

		/// <summary>Removes all <see cref="T:System.Windows.Forms.MenuItem" /> objects from the menu item collection.</summary>
		public virtual void Clear()
		{
			MenuTracker tracker = owner.Tracker;
			foreach (MenuItem item in items)
			{
				tracker?.RemoveShortcuts(item);
				item.parent_menu = null;
			}
			items.Clear();
			owner.OnMenuChanged(EventArgs.Empty);
		}

		/// <summary>Determines if the specified <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection. </param>
		public bool Contains(MenuItem value)
		{
			return items.Contains(value);
		}

		/// <summary>Determines whether the collection contains an item with the specified key.</summary>
		/// <returns>true if the collection contains an item with the specified key, otherwise, false. </returns>
		/// <param name="key">The name of the item to look for.</param>
		public virtual bool ContainsKey(string key)
		{
			return this[key] != null;
		}

		/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
		/// <param name="dest">The destination array. </param>
		/// <param name="index">The index in the destination array at which storing begins. </param>
		public void CopyTo(Array dest, int index)
		{
			items.CopyTo(dest, index);
		}

		/// <summary>Finds the items with the specified key, optionally searching the submenu items</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects whose <see cref="P:System.Windows.Forms.Menu.Name" /> property matches the specified <paramref name="key" />. </returns>
		/// <param name="key">The name of the menu item to search for.</param>
		/// <param name="searchAllChildren">true to search child menu items; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null or an empty string.</exception>
		public MenuItem[] Find(string key, bool searchAllChildren)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			List<MenuItem> list = new List<MenuItem>();
			foreach (MenuItem item in items)
			{
				if (string.Compare(item.Name, key, ignoreCase: true) == 0)
				{
					list.Add(item);
				}
			}
			if (searchAllChildren)
			{
				foreach (MenuItem item2 in items)
				{
					list.AddRange(item2.MenuItems.Find(key, searchAllChildren: true));
				}
			}
			return list.ToArray();
		}

		/// <summary>Returns an enumerator that can be used to iterate through the menu item collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the menu item collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return items.GetEnumerator();
		}

		/// <summary>Retrieves the index of a specific item in the collection.</summary>
		/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection. </param>
		public int IndexOf(MenuItem value)
		{
			return items.IndexOf(value);
		}

		/// <summary>Finds the index of the first occurrence of a menu item with the specified key.</summary>
		/// <returns>The zero-based index of the first menu item with the specified key.</returns>
		/// <param name="key">The name of the menu item to search for.</param>
		public virtual int IndexOfKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return -1;
			}
			return IndexOf(this[key]);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection.</summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove. </param>
		public virtual void Remove(MenuItem item)
		{
			RemoveAt(item.Index);
		}

		/// <summary>Removes a <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection at a specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.MenuItem" /> to remove. </param>
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			MenuItem menuItem = (MenuItem)items[index];
			owner.Tracker?.RemoveShortcuts(menuItem);
			menuItem.parent_menu = null;
			items.RemoveAt(index);
			UpdateItemsIndices();
			owner.OnMenuChanged(EventArgs.Empty);
		}

		/// <summary>Removes the menu item with the specified key from the collection.</summary>
		/// <param name="key">The name of the menu item to remove.</param>
		public virtual void RemoveByKey(string key)
		{
			Remove(this[key]);
		}

		private void UpdateItemsIndices()
		{
			for (int i = 0; i < Count; i++)
			{
				((MenuItem)items[i]).Index = i;
			}
		}
	}

	/// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a handle.</summary>
	/// <filterpriority>1</filterpriority>
	public const int FindHandle = 0;

	/// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a shortcut.</summary>
	/// <filterpriority>1</filterpriority>
	public const int FindShortcut = 1;

	internal MenuItemCollection menu_items;

	internal IntPtr menu_handle = IntPtr.Zero;

	internal Menu parent_menu;

	private Rectangle rect;

	internal Control Wnd;

	internal MenuTracker tracker;

	private string control_name;

	private object control_tag;

	private static object MenuChangedEvent;

	/// <summary>Gets a value representing the window handle for the menu.</summary>
	/// <returns>The HMENU value of the menu.</returns>
	/// <filterpriority>2</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IntPtr Handle => menu_handle;

	/// <summary>Gets a value indicating whether this menu contains any menu items. This property is read-only.</summary>
	/// <returns>true if this menu contains <see cref="T:System.Windows.Forms.MenuItem" /> objects; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual bool IsParent
	{
		get
		{
			if (menu_items != null && menu_items.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.MenuItem" /> that is used to display a list of multiple document interface (MDI) child forms.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public MenuItem MdiListItem
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating the collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects associated with the menu.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> that represents the list of <see cref="T:System.Windows.Forms.MenuItem" /> objects stored in the menu.</returns>
	/// <filterpriority>1</filterpriority>
	[MergableProperty(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public MenuItemCollection MenuItems => menu_items;

	/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.Menu" />.</summary>
	/// <returns>A string representing the name.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Name
	{
		get
		{
			return control_name;
		}
		set
		{
			control_name = value;
		}
	}

	/// <summary>Gets or sets user-defined data associated with the control.</summary>
	/// <returns>An object representing the data.</returns>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Data")]
	[DefaultValue(null)]
	[TypeConverter(typeof(StringConverter))]
	[Bindable(true)]
	[Localizable(false)]
	public object Tag
	{
		get
		{
			return control_tag;
		}
		set
		{
			control_tag = value;
		}
	}

	internal Rectangle Rect => rect;

	internal MenuItem SelectedItem
	{
		get
		{
			foreach (MenuItem menuItem in MenuItems)
			{
				if (menuItem.Selected)
				{
					return menuItem;
				}
			}
			return null;
		}
	}

	internal int Height
	{
		get
		{
			return rect.Height;
		}
		set
		{
			rect.Height = value;
		}
	}

	internal int Width
	{
		get
		{
			return rect.Width;
		}
		set
		{
			rect.Width = value;
		}
	}

	internal int X
	{
		get
		{
			return rect.X;
		}
		set
		{
			rect.X = value;
		}
	}

	internal int Y
	{
		get
		{
			return rect.Y;
		}
		set
		{
			rect.Y = value;
		}
	}

	internal MenuTracker Tracker
	{
		get
		{
			Menu menu = this;
			while (menu.parent_menu != null)
			{
				menu = menu.parent_menu;
			}
			return menu.tracker;
		}
	}

	internal event EventHandler MenuChanged
	{
		add
		{
			base.Events.AddHandler(MenuChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MenuChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu" /> class.</summary>
	/// <param name="items">An array of type <see cref="T:System.Windows.Forms.MenuItem" /> containing the objects to add to the menu.</param>
	protected Menu(MenuItem[] items)
	{
		menu_items = new MenuItemCollection(this);
		if (items != null)
		{
			menu_items.AddRange(items);
		}
	}

	static Menu()
	{
		MenuChanged = new object();
	}

	internal virtual void OnMenuChanged(EventArgs e)
	{
		((EventHandler)base.Events[MenuChanged])?.Invoke(this, e);
	}

	/// <summary>Copies the <see cref="T:System.Windows.Forms.Menu" /> that is passed as a parameter to the current <see cref="T:System.Windows.Forms.Menu" />.</summary>
	/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> to copy. </param>
	protected void CloneMenu(Menu menuSrc)
	{
		Dispose(disposing: true);
		menu_items = new MenuItemCollection(this);
		for (int i = 0; i < menuSrc.MenuItems.Count; i++)
		{
			menu_items.Add(menuSrc.MenuItems[i].CloneMenu());
		}
	}

	/// <summary>Creates a new handle to the <see cref="T:System.Windows.Forms.Menu" />.</summary>
	/// <returns>A handle to the menu if the method succeeds; otherwise, null.</returns>
	protected virtual IntPtr CreateMenuHandle()
	{
		return IntPtr.Zero;
	}

	/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.Menu" />.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && menu_handle != IntPtr.Zero)
		{
			menu_handle = IntPtr.Zero;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.MenuItem" /> that contains the value specified. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> that matches value; otherwise, null.</returns>
	/// <param name="type">The type of item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
	/// <param name="value">The item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
	/// <filterpriority>1</filterpriority>
	public MenuItem FindMenuItem(int type, IntPtr value)
	{
		return null;
	}

	/// <summary>Returns the position at which a menu item should be inserted into the menu.</summary>
	/// <returns>The position at which a menu item should be inserted into the menu.</returns>
	/// <param name="mergeOrder">The merge order position for the menu item to be merged.</param>
	protected int FindMergePosition(int mergeOrder)
	{
		int num = MenuItems.Count;
		int num2 = 0;
		while (num2 < num)
		{
			int num3 = (num2 + num) / 2;
			if (MenuItems[num3].MergeOrder > mergeOrder)
			{
				num = num3;
			}
			else
			{
				num2 = num3 + 1;
			}
		}
		return num2;
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	public ContextMenu GetContextMenu()
	{
		for (Menu menu = this; menu != null; menu = menu.parent_menu)
		{
			if (menu is ContextMenu)
			{
				return (ContextMenu)menu;
			}
		}
		return null;
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</returns>
	/// <filterpriority>1</filterpriority>
	public MainMenu GetMainMenu()
	{
		for (Menu menu = this; menu != null; menu = menu.parent_menu)
		{
			if (menu is MainMenu)
			{
				return (MainMenu)menu;
			}
		}
		return null;
	}

	internal virtual void InvalidateItem(MenuItem item)
	{
		if (Wnd != null)
		{
			Wnd.Invalidate(item.bounds);
		}
	}

	/// <summary>Merges the <see cref="T:System.Windows.Forms.MenuItem" /> objects of one menu with the current menu.</summary>
	/// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> whose menu items are merged with the menu items of the current menu. </param>
	/// <exception cref="T:System.ArgumentException">It was attempted to merge the menu with itself. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void MergeMenu(Menu menuSrc)
	{
		if (menuSrc == this)
		{
			throw new ArgumentException("The menu cannot be merged with itself");
		}
		if (menuSrc == null)
		{
			return;
		}
		for (int i = 0; i < menuSrc.MenuItems.Count; i++)
		{
			MenuItem menuItem = menuSrc.MenuItems[i];
			switch (menuItem.MergeType)
			{
			case MenuMerge.Add:
			{
				int index = FindMergePosition(menuItem.MergeOrder);
				MenuItems.Add(index, menuItem.CloneMenu());
				break;
			}
			case MenuMerge.Replace:
			case MenuMerge.MergeItems:
			{
				for (int j = FindMergePosition(menuItem.MergeOrder - 1); j <= MenuItems.Count; j++)
				{
					if (j >= MenuItems.Count || MenuItems[j].MergeOrder != menuItem.MergeOrder)
					{
						MenuItems.Add(j, menuItem.CloneMenu());
						break;
					}
					MenuItem menuItem2 = MenuItems[j];
					if (menuItem2.MergeType != 0)
					{
						if (menuItem.MergeType == MenuMerge.MergeItems && menuItem2.MergeType == MenuMerge.MergeItems)
						{
							menuItem2.MergeMenu(menuItem);
							break;
						}
						MenuItems.Remove(menuItem);
						MenuItems.Add(j, menuItem.CloneMenu());
						break;
					}
				}
				break;
			}
			}
		}
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
	protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (tracker == null)
		{
			return false;
		}
		return tracker.ProcessKeys(ref msg, keyData);
	}

	/// <summary>Returns a <see cref="T:System.String" /> that represents the <see cref="T:System.Windows.Forms.Menu" /> control.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Menu" />.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Items.Count: " + MenuItems.Count;
	}
}
