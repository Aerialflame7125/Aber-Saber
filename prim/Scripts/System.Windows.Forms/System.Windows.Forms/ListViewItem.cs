using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Windows.Forms;

/// <summary>Represents an item in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
/// <filterpriority>1</filterpriority>
[Serializable]
[DefaultProperty("Text")]
[ToolboxItem(false)]
[TypeConverter(typeof(ListViewItemConverter))]
[DesignTimeVisible(false)]
public class ListViewItem : ISerializable, ICloneable
{
	/// <summary>Represents a subitem of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	[Serializable]
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[TypeConverter(typeof(ListViewSubItemConverter))]
	public class ListViewSubItem
	{
		[Serializable]
		private class SubItemStyle
		{
			public Color backColor;

			public Color foreColor;

			public Font font;

			public SubItemStyle()
			{
			}

			public SubItemStyle(Color foreColor, Color backColor, Font font)
			{
				this.foreColor = foreColor;
				this.backColor = backColor;
				this.font = font;
			}

			public void Reset()
			{
				foreColor = Color.Empty;
				backColor = Color.Empty;
				font = null;
			}
		}

		[NonSerialized]
		internal ListViewItem owner;

		private string text = string.Empty;

		private string name;

		private object userData;

		private SubItemStyle style;

		[NonSerialized]
		internal Rectangle bounds;

		/// <summary>Gets or sets the background color of the subitem's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem's text.</returns>
		public Color BackColor
		{
			get
			{
				if (style.backColor != Color.Empty)
				{
					return style.backColor;
				}
				if (owner != null && owner.ListView != null)
				{
					return owner.ListView.BackColor;
				}
				return ThemeEngine.Current.ColorWindow;
			}
			set
			{
				style.backColor = value;
				Invalidate();
			}
		}

		/// <summary>Gets the bounding rectangle of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
		/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</returns>
		[Browsable(false)]
		public Rectangle Bounds
		{
			get
			{
				Rectangle result = bounds;
				if (owner != null)
				{
					result.X += owner.Bounds.X;
					result.Y += owner.Bounds.Y;
				}
				return result;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the subitem.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
		[Localizable(true)]
		public Font Font
		{
			get
			{
				if (style.font != null)
				{
					return style.font;
				}
				if (owner != null)
				{
					return owner.Font;
				}
				return ThemeEngine.Current.DefaultFont;
			}
			set
			{
				if (style.font != value)
				{
					style.font = value;
					Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the subitem's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem's text.</returns>
		public Color ForeColor
		{
			get
			{
				if (style.foreColor != Color.Empty)
				{
					return style.foreColor;
				}
				if (owner != null && owner.ListView != null)
				{
					return owner.ListView.ForeColor;
				}
				return ThemeEngine.Current.ColorWindowText;
			}
			set
			{
				style.foreColor = value;
				Invalidate();
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />, or an empty string ("") if a name has not been set.</returns>
		[Localizable(true)]
		public string Name
		{
			get
			{
				if (name == null)
				{
					return string.Empty;
				}
				return name;
			}
			set
			{
				name = value;
			}
		}

		/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />. The default is null.</returns>
		[DefaultValue(null)]
		[Localizable(false)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return userData;
			}
			set
			{
				userData = value;
			}
		}

		/// <summary>Gets or sets the text of the subitem.</summary>
		/// <returns>The text to display for the subitem.</returns>
		[Localizable(true)]
		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				if (!(text == value))
				{
					if (value == null)
					{
						text = string.Empty;
					}
					else
					{
						text = value;
					}
					Invalidate();
					OnUIATextChanged();
				}
			}
		}

		internal int Height => bounds.Height;

		[field: NonSerialized]
		internal event EventHandler UIATextChanged;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with default values.</summary>
		public ListViewSubItem()
			: this(null, string.Empty, Color.Empty, Color.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner and text.</summary>
		/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem. </param>
		/// <param name="text">The text to display for the subitem. </param>
		public ListViewSubItem(ListViewItem owner, string text)
			: this(owner, text, Color.Empty, Color.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> class with the specified owner, text, foreground color, background color, and font values.</summary>
		/// <param name="owner">A <see cref="T:System.Windows.Forms.ListViewItem" /> that represents the item that owns the subitem. </param>
		/// <param name="text">The text to display for the subitem. </param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the subitem's text in. </param>
		public ListViewSubItem(ListViewItem owner, string text, Color foreColor, Color backColor, Font font)
		{
			this.owner = owner;
			Text = text;
			style = new SubItemStyle(foreColor, backColor, font);
		}

		private void OnUIATextChanged()
		{
			if (this.UIATextChanged != null)
			{
				this.UIATextChanged(this, EventArgs.Empty);
			}
		}

		/// <summary>Resets the styles applied to the subitem to the default font and colors.</summary>
		public void ResetStyle()
		{
			style.Reset();
			Invalidate();
		}

		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return string.Format("ListViewSubItem {{0}}", text);
		}

		private void Invalidate()
		{
			if (owner != null && owner.owner != null)
			{
				owner.Invalidate();
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			name = null;
			userData = null;
		}

		internal void SetBounds(int x, int y, int width, int height)
		{
			bounds = new Rectangle(x, y, width, height);
		}
	}

	/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects stored in a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	public class ListViewSubItemCollection : ICollection, IEnumerable, IList
	{
		private ArrayList list;

		internal ListViewItem owner;

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true in all cases.</returns>
		bool ICollection.IsSynchronized => list.IsSynchronized;

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object used to synchronize the collection.</returns>
		object ICollection.SyncRoot => list.SyncRoot;

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => list.IsFixedSize;

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified index within the collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the item located at the specified index within the collection.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" />.</exception>
		/// <exception cref="T:System.ArgumentException">The object is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is ListViewSubItem))
				{
					throw new ArgumentException("Not of type ListViewSubItem", "value");
				}
				this[index] = (ListViewSubItem)value;
			}
		}

		/// <summary>Gets the number of subitems in the collection.</summary>
		/// <returns>The number of subitems in the collection.</returns>
		[Browsable(false)]
		public int Count => list.Count;

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		public bool IsReadOnly => false;

		/// <summary>Gets or sets the subitem at the specified index within the collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem located at the specified index within the collection.</returns>
		/// <param name="index">The index of the item in the collection to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
		public ListViewSubItem this[int index]
		{
			get
			{
				return (ListViewSubItem)list[index];
			}
			set
			{
				value.owner = owner;
				list[index] = value;
				owner.Layout();
				owner.Invalidate();
			}
		}

		/// <summary>Gets an item with the specified key from the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> with the specified key.</returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to retrieve.</param>
		public virtual ListViewSubItem this[string key]
		{
			get
			{
				int num = IndexOfKey(key);
				if (num == -1)
				{
					return null;
				}
				return (ListViewSubItem)list[num];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ListViewItem" /> that owns the collection. </param>
		public ListViewSubItemCollection(ListViewItem owner)
			: this(owner, owner.Text)
		{
		}

		internal ListViewSubItemCollection(ListViewItem owner, string text)
		{
			this.owner = owner;
			list = new ArrayList();
			if (text != null)
			{
				Add(text);
			}
		}

		/// <summary>Copies the item and collection of subitems into an array.</summary>
		/// <param name="dest">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		/// <exception cref="T:System.ArrayTypeMismatchException">The array type is not compatible with <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
		void ICollection.CopyTo(Array dest, int index)
		{
			list.CopyTo(dest, index);
		}

		/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
		/// <returns>The zero-based index that indicates the location of the object that was added to the collection.</returns>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
		int IList.Add(object item)
		{
			if (!(item is ListViewSubItem))
			{
				throw new ArgumentException("Not of type ListViewSubItem", "item");
			}
			ListViewSubItem listViewSubItem = (ListViewSubItem)item;
			listViewSubItem.owner = owner;
			listViewSubItem.UIATextChanged += OnUIASubItemTextChanged;
			return list.Add(listViewSubItem);
		}

		/// <summary>Determines whether the specified subitem is located in the collection.</summary>
		/// <returns>true if the subitem is contained in the collection; otherwise, false.</returns>
		/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
		bool IList.Contains(object subItem)
		{
			if (!(subItem is ListViewSubItem))
			{
				throw new ArgumentException("Not of type ListViewSubItem", "subItem");
			}
			return Contains((ListViewSubItem)subItem);
		}

		/// <summary>Returns the index within the collection of the specified subitem.</summary>
		/// <returns>The zero-based index of the subitem if it is in the collection; otherwise, -1.</returns>
		/// <param name="subItem">An object that represents the subitem to locate in the collection.</param>
		int IList.IndexOf(object subItem)
		{
			if (!(subItem is ListViewSubItem))
			{
				throw new ArgumentException("Not of type ListViewSubItem", "subItem");
			}
			return IndexOf((ListViewSubItem)subItem);
		}

		/// <summary>Inserts a subitem into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index location where the item is inserted.</param>
		/// <param name="item">An object that represents the subitem to insert into the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="item" /> is not a <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than 0 or greater than or equal to the value of the Count property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />.</exception>
		void IList.Insert(int index, object item)
		{
			if (!(item is ListViewSubItem))
			{
				throw new ArgumentException("Not of type ListViewSubItem", "item");
			}
			Insert(index, (ListViewSubItem)item);
		}

		/// <summary>Removes a specified item from the collection.</summary>
		/// <param name="item">The item to remove from the collection.</param>
		void IList.Remove(object item)
		{
			if (!(item is ListViewSubItem))
			{
				throw new ArgumentException("Not of type ListViewSubItem", "item");
			}
			Remove((ListViewSubItem)item);
		}

		/// <summary>Adds an existing <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to add to the collection. </param>
		public ListViewSubItem Add(ListViewSubItem item)
		{
			AddSubItem(item);
			owner.Layout();
			owner.Invalidate();
			return item;
		}

		/// <summary>Adds a subitem to the collection with specified text.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
		/// <param name="text">The text to display for the subitem. </param>
		public ListViewSubItem Add(string text)
		{
			ListViewSubItem item = new ListViewSubItem(owner, text);
			return Add(item);
		}

		/// <summary>Adds a subitem to the collection with specified text, foreground color, background color, and font settings.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that was added to the collection.</returns>
		/// <param name="text">The text to display for the subitem. </param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in. </param>
		public ListViewSubItem Add(string text, Color foreColor, Color backColor, Font font)
		{
			ListViewSubItem item = new ListViewSubItem(owner, text, foreColor, backColor, font);
			return Add(item);
		}

		/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects to the collection.</summary>
		/// <param name="items">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects to add to the collection. </param>
		public void AddRange(ListViewSubItem[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (ListViewSubItem listViewSubItem in items)
			{
				if (listViewSubItem != null)
				{
					AddSubItem(listViewSubItem);
				}
			}
			owner.Layout();
			owner.Invalidate();
		}

		/// <summary>Creates new subitems based on an array and adds them to the collection.</summary>
		/// <param name="items">An array of strings representing the text of each subitem to add to the collection. </param>
		public void AddRange(string[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (string text in items)
			{
				if (text != null)
				{
					AddSubItem(new ListViewSubItem(owner, text));
				}
			}
			owner.Layout();
			owner.Invalidate();
		}

		/// <summary>Creates new subitems based on an array and adds them to the collection with specified foreground color, background color, and font.</summary>
		/// <param name="items">An array of strings representing the text of each subitem to add to the collection. </param>
		/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the subitem. </param>
		/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the subitem. </param>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the typeface to display the subitem's text in. </param>
		public void AddRange(string[] items, Color foreColor, Color backColor, Font font)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (string text in items)
			{
				if (text != null)
				{
					AddSubItem(new ListViewSubItem(owner, text, foreColor, backColor, font));
				}
			}
			owner.Layout();
			owner.Invalidate();
		}

		private void AddSubItem(ListViewSubItem subItem)
		{
			subItem.owner = owner;
			list.Add(subItem);
			subItem.UIATextChanged += OnUIASubItemTextChanged;
		}

		/// <summary>Removes all subitems and the parent <see cref="T:System.Windows.Forms.ListViewItem" /> from the collection.</summary>
		public void Clear()
		{
			list.Clear();
		}

		/// <summary>Determines whether the specified subitem is located in the collection.</summary>
		/// <returns>true if the subitem is contained in the collection; otherwise, false.</returns>
		/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection. </param>
		public bool Contains(ListViewSubItem subItem)
		{
			return list.Contains(subItem);
		}

		/// <summary>Determines if the collection contains an item with the specified key.</summary>
		/// <returns>true to indicate the collection contains an item with the specified key; otherwise, false. </returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> to look for.</param>
		public virtual bool ContainsKey(string key)
		{
			return IndexOfKey(key) != -1;
		}

		/// <summary>Returns an enumerator to use to iterate through the subitem collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the subitem collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>Returns the index within the collection of the specified subitem.</summary>
		/// <returns>The zero-based index of the subitem's location in the collection. If the subitem is not located in the collection, the return value is negative one (-1).</returns>
		/// <param name="subItem">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to locate in the collection. </param>
		public int IndexOf(ListViewSubItem subItem)
		{
			return list.IndexOf(subItem);
		}

		/// <summary>Returns the index of the first occurrence of an item with the specified key within the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of an item with the specified key.</returns>
		/// <param name="key">The name of the item to retrieve the index for.</param>
		public virtual int IndexOfKey(string key)
		{
			if (key == null || key.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < list.Count; i++)
			{
				ListViewSubItem listViewSubItem = (ListViewSubItem)list[i];
				if (string.Compare(listViewSubItem.Name, key, ignoreCase: true) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts a subitem into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index location where the item is inserted. </param>
		/// <param name="item">A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> representing the subitem to insert into the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
		public void Insert(int index, ListViewSubItem item)
		{
			item.owner = owner;
			list.Insert(index, item);
			owner.Layout();
			owner.Invalidate();
			item.UIATextChanged += OnUIASubItemTextChanged;
		}

		/// <summary>Removes a specified item from the collection.</summary>
		/// <param name="item">The item to remove from the collection.</param>
		public void Remove(ListViewSubItem item)
		{
			list.Remove(item);
			owner.Layout();
			owner.Invalidate();
			item.UIATextChanged -= OnUIASubItemTextChanged;
		}

		/// <summary>Removes an item with the specified key from the collection.</summary>
		/// <param name="key">The name of the item to remove from the collection.</param>
		public virtual void RemoveByKey(string key)
		{
			int num = IndexOfKey(key);
			if (num != -1)
			{
				RemoveAt(num);
			}
		}

		/// <summary>Removes the subitem at the specified index within the collection.</summary>
		/// <param name="index">The zero-based index of the subitem to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" />. </exception>
		public void RemoveAt(int index)
		{
			if (index >= 0 && index < list.Count)
			{
				((ListViewSubItem)list[index]).UIATextChanged -= OnUIASubItemTextChanged;
			}
			list.RemoveAt(index);
		}

		private void OnUIASubItemTextChanged(object sender, EventArgs args)
		{
			owner.OnUIASubItemTextChanged(new LabelEditEventArgs(list.IndexOf(sender)));
		}
	}

	private int image_index = -1;

	private bool is_checked;

	private int state_image_index = -1;

	private ListViewSubItemCollection sub_items;

	private object tag;

	private bool use_item_style = true;

	private int display_index = -1;

	private ListViewGroup group;

	private string name = string.Empty;

	private string image_key = string.Empty;

	private string tooltip_text = string.Empty;

	private int indent_count;

	private Point position = new Point(-1, -1);

	private Rectangle bounds = Rectangle.Empty;

	private Rectangle checkbox_rect;

	private Rectangle icon_rect;

	private Rectangle item_rect;

	private Rectangle label_rect;

	private ListView owner;

	private Font font;

	private Font hot_font;

	private bool selected;

	internal int row;

	internal int col;

	private Rectangle text_bounds;

	/// <summary>Gets or sets the background color of the item's text.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item's text.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color BackColor
	{
		get
		{
			if (sub_items.Count > 0)
			{
				return sub_items[0].BackColor;
			}
			if (owner != null)
			{
				return owner.BackColor;
			}
			return ThemeEngine.Current.ColorWindow;
		}
		set
		{
			SubItems[0].BackColor = value;
		}
	}

	/// <summary>Gets the bounding rectangle of the item, including subitems.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle of the item.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Rectangle Bounds => GetBounds(ItemBoundsPortion.Entire);

	/// <summary>Gets or sets a value indicating whether the item is checked.</summary>
	/// <returns>true if the item is checked; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(false)]
	public bool Checked
	{
		get
		{
			return is_checked;
		}
		set
		{
			if (is_checked == value)
			{
				return;
			}
			if (owner != null)
			{
				CheckState checkState = (is_checked ? CheckState.Checked : CheckState.Unchecked);
				CheckState checkState2 = (value ? CheckState.Checked : CheckState.Unchecked);
				ItemCheckEventArgs ice = new ItemCheckEventArgs(Index, checkState2, checkState);
				owner.OnItemCheck(ice);
				if (checkState2 != checkState)
				{
					owner.CheckedItems.Reset();
					is_checked = checkState2 == CheckState.Checked;
					Invalidate();
					ItemCheckedEventArgs e = new ItemCheckedEventArgs(this);
					owner.OnItemChecked(e);
				}
			}
			else
			{
				is_checked = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the item has focus within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <returns>true if the item has focus; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool Focused
	{
		get
		{
			if (owner == null)
			{
				return false;
			}
			if (owner.VirtualMode)
			{
				return Index == owner.focused_item_index;
			}
			return owner.FocusedItem == this;
		}
		set
		{
			if (owner != null && Focused != value)
			{
				owner.FocusedItem?.UpdateFocusedState();
				owner.focused_item_index = ((!value) ? (-1) : Index);
				if (value)
				{
					owner.OnUIAFocusedItemChanged();
				}
				UpdateFocusedState();
			}
		}
	}

	/// <summary>Gets or sets the font of the text displayed by the item.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property if the <see cref="T:System.Windows.Forms.ListViewItem" /> is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control; otherwise, the font specified in the <see cref="P:System.Windows.Forms.Control.Font" /> property for the <see cref="T:System.Windows.Forms.ListView" /> control is used.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	public Font Font
	{
		get
		{
			if (font != null)
			{
				return font;
			}
			if (owner != null)
			{
				return owner.Font;
			}
			return ThemeEngine.Current.DefaultFont;
		}
		set
		{
			if (font != value)
			{
				font = value;
				hot_font = null;
				if (owner != null)
				{
					Layout();
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the foreground color of the item's text.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item's text.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Color ForeColor
	{
		get
		{
			if (sub_items.Count > 0)
			{
				return sub_items[0].ForeColor;
			}
			if (owner != null)
			{
				return owner.ForeColor;
			}
			return ThemeEngine.Current.ColorWindowText;
		}
		set
		{
			SubItems[0].ForeColor = value;
		}
	}

	/// <summary>Gets or sets the index of the image that is displayed for the item.</summary>
	/// <returns>The zero-based index of the image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item. The default is -1.</returns>
	/// <exception cref="T:System.ArgumentException">The value specified is less than -1. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue(-1)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
	public int ImageIndex
	{
		get
		{
			return image_index;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentException("Invalid ImageIndex. It must be greater than or equal to -1.");
			}
			image_index = value;
			image_key = string.Empty;
			if (owner != null)
			{
				Layout();
			}
			Invalidate();
		}
	}

	/// <summary>Gets or sets the key for the image that is displayed for the item.</summary>
	/// <returns>The key for the image that is displayed for the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(ImageKeyConverter))]
	public string ImageKey
	{
		get
		{
			return image_key;
		}
		set
		{
			image_key = ((value != null) ? value : string.Empty);
			image_index = -1;
			if (owner != null)
			{
				Layout();
			}
			Invalidate();
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the image displayed with the item.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> used by the <see cref="T:System.Windows.Forms.ListView" /> control that contains the image displayed with the item.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ImageList ImageList
	{
		get
		{
			if (owner == null)
			{
				return null;
			}
			if (owner.View == View.LargeIcon)
			{
				return owner.large_image_list;
			}
			return owner.small_image_list;
		}
	}

	/// <summary>Gets or sets the number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <returns>The number of small image widths by which to indent the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">When setting <see cref="P:System.Windows.Forms.ListViewItem.IndentCount" />, the number specified is less than 0.</exception>
	[DefaultValue(0)]
	public int IndentCount
	{
		get
		{
			return indent_count;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (value != indent_count)
			{
				indent_count = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets the zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <returns>The zero-based index of the item within the <see cref="T:System.Windows.Forms.ListView.ListViewItemCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control, or -1 if the item is not associated with a <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public int Index
	{
		get
		{
			if (owner == null)
			{
				return -1;
			}
			if (owner.VirtualMode)
			{
				return display_index;
			}
			if (display_index == -1)
			{
				return owner.Items.IndexOf(this);
			}
			return owner.GetItemIndex(display_index);
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control that contains the item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> that contains the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ListView ListView => owner;

	/// <summary>Gets or sets the name associated with this <see cref="T:System.Windows.Forms.ListViewItem" />. </summary>
	/// <returns>The name of the <see cref="T:System.Windows.Forms.ListViewItem" />. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = ((value != null) ? value : string.Empty);
		}
	}

	/// <summary>Gets or sets the position of the upper-left corner of the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Point" /> at the upper-left corner of the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListViewItem.Position" /> is set when the containing <see cref="T:System.Windows.Forms.ListView" /> is in virtual mode.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Point Position
	{
		get
		{
			if (owner != null && owner.VirtualMode)
			{
				return owner.GetItemLocation(display_index);
			}
			if (owner != null && !owner.IsHandleCreated)
			{
				return new Point(-1, -1);
			}
			return position;
		}
		set
		{
			if (owner != null && owner.View != View.Details && owner.View != View.List)
			{
				if (owner.VirtualMode)
				{
					throw new InvalidOperationException();
				}
				owner.ChangeItemLocation(display_index, value);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the item is selected.</summary>
	/// <returns>true if the item is selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool Selected
	{
		get
		{
			if (owner != null && owner.VirtualMode)
			{
				return owner.SelectedIndices.Contains(Index);
			}
			return selected;
		}
		set
		{
			if (selected == value && owner != null && !owner.VirtualMode)
			{
				return;
			}
			if (owner != null)
			{
				if (value && !owner.MultiSelect)
				{
					owner.SelectedIndices.Clear();
				}
				if (owner.VirtualMode)
				{
					if (value)
					{
						owner.SelectedIndices.InsertIndex(Index);
					}
					else
					{
						owner.SelectedIndices.RemoveIndex(Index);
					}
				}
				else
				{
					selected = value;
					owner.SelectedIndices.Reset();
				}
				owner.OnItemSelectionChanged(new ListViewItemSelectionChangedEventArgs(this, Index, value));
				owner.OnSelectedIndexChanged();
			}
			else
			{
				selected = value;
			}
			Invalidate();
		}
	}

	/// <summary>Gets or sets the index of the state image (an image such as a selected or cleared check box that indicates the state of the item) that is displayed for the item.</summary>
	/// <returns>The zero-based index of the state image in the <see cref="T:System.Windows.Forms.ImageList" /> that is displayed for the item.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than -1.-or- The value specified for this property is greater than 14. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[RelatedImageList("ListView.StateImageList")]
	[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
	[DefaultValue(-1)]
	public int StateImageIndex
	{
		get
		{
			return state_image_index;
		}
		set
		{
			if (value < -1 || value > 14)
			{
				throw new ArgumentOutOfRangeException("Invalid StateImageIndex. It must be in the range of [-1, 14].");
			}
			state_image_index = value;
		}
	}

	/// <summary>Gets a collection containing all subitems of the item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItemCollection" /> that contains the subitems.</returns>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.ListViewSubItemCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ListViewSubItemCollection SubItems
	{
		get
		{
			if (sub_items.Count == 0)
			{
				sub_items.Add(string.Empty);
			}
			return sub_items;
		}
	}

	/// <summary>Gets or sets an object that contains data to associate with the item.</summary>
	/// <returns>An object that contains information that is associated with the item.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(false)]
	[DefaultValue(null)]
	[Bindable(true)]
	[TypeConverter(typeof(StringConverter))]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	/// <summary>Gets or sets the text of the item.</summary>
	/// <returns>The text to display for the item. This should not exceed 259 characters.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	public string Text
	{
		get
		{
			if (sub_items.Count > 0)
			{
				return sub_items[0].Text;
			}
			return string.Empty;
		}
		set
		{
			if (!(SubItems[0].Text == value))
			{
				sub_items[0].Text = value;
				if (owner != null)
				{
					Layout();
				}
				Invalidate();
				OnUIATextChanged();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ListViewItem.Font" />, <see cref="P:System.Windows.Forms.ListViewItem.ForeColor" />, and <see cref="P:System.Windows.Forms.ListViewItem.BackColor" /> properties for the item are used for all its subitems.</summary>
	/// <returns>true if all subitems use the font, foreground color, and background color settings of the item; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool UseItemStyleForSubItems
	{
		get
		{
			return use_item_style;
		}
		set
		{
			use_item_style = value;
		}
	}

	/// <summary>Gets or sets the group to which the item is assigned.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewGroup" /> to which the item is assigned.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Localizable(true)]
	public ListViewGroup Group
	{
		get
		{
			return group;
		}
		set
		{
			if (group != value)
			{
				if (value == null)
				{
					group.Items.Remove(this);
				}
				else
				{
					value.Items.Add(this);
				}
				group = value;
			}
		}
	}

	/// <summary>Gets or sets the text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <returns>The text shown when the mouse pointer rests on the <see cref="T:System.Windows.Forms.ListViewItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	public string ToolTipText
	{
		get
		{
			return tooltip_text;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			tooltip_text = value;
		}
	}

	internal Rectangle CheckRectReal
	{
		get
		{
			Rectangle result = checkbox_rect;
			Point itemLocation = owner.GetItemLocation(DisplayIndex);
			result.X += itemLocation.X;
			result.Y += itemLocation.Y;
			return result;
		}
	}

	internal Rectangle TextBounds
	{
		get
		{
			if (owner.VirtualMode && bounds == new Rectangle(-1, -1, -1, -1))
			{
				Layout();
			}
			Rectangle result = text_bounds;
			Point itemLocation = owner.GetItemLocation(DisplayIndex);
			result.X += itemLocation.X;
			result.Y += itemLocation.Y;
			return result;
		}
	}

	internal int DisplayIndex
	{
		get
		{
			if (display_index == -1)
			{
				return owner.Items.IndexOf(this);
			}
			return display_index;
		}
		set
		{
			display_index = value;
		}
	}

	internal bool Hot => Index == owner.HotItemIndex;

	internal Font HotFont
	{
		get
		{
			if (hot_font == null)
			{
				hot_font = new Font(Font, Font.Style | FontStyle.Underline);
			}
			return hot_font;
		}
	}

	internal ListView Owner
	{
		set
		{
			if (owner != value)
			{
				owner = value;
			}
		}
	}

	internal event EventHandler UIATextChanged;

	internal event LabelEditEventHandler UIASubItemTextChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with default values.</summary>
	public ListViewItem()
		: this(string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text.</summary>
	/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
	public ListViewItem(string text)
		: this(text, -1)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with an array of strings representing subitems.</summary>
	/// <param name="items">An array of strings that represent the subitems of the new item. </param>
	public ListViewItem(string[] items)
		: this(items, -1)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects.</summary>
	/// <param name="subItems">An array of type <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the subitems of the item. </param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	public ListViewItem(ListViewSubItem[] subItems, int imageIndex)
	{
		sub_items = new ListViewSubItemCollection(this, null);
		for (int i = 0; i < subItems.Length; i++)
		{
			sub_items.Add(subItems[i]);
		}
		image_index = imageIndex;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon.</summary>
	/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	public ListViewItem(string text, int imageIndex)
	{
		image_index = imageIndex;
		sub_items = new ListViewSubItemCollection(this, text);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of strings representing subitems.</summary>
	/// <param name="items">An array of strings that represent the subitems of the new item. </param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	public ListViewItem(string[] items, int imageIndex)
	{
		sub_items = new ListViewSubItemCollection(this, null);
		if (items != null)
		{
			for (int i = 0; i < items.Length; i++)
			{
				sub_items.Add(new ListViewSubItem(this, items[i]));
			}
		}
		image_index = imageIndex;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon; the foreground color, background color, and font of the item; and an array of strings representing subitems.</summary>
	/// <param name="items">An array of strings that represent the subitems of the new item. </param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item. </param>
	/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. </param>
	/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the item's text in. </param>
	public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font)
		: this(items, imageIndex)
	{
		ForeColor = foreColor;
		BackColor = backColor;
		this.font = font;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item and subitem text and image.</summary>
	/// <param name="items">An array containing the text of the subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	public ListViewItem(string[] items, string imageKey)
		: this(items)
	{
		ImageKey = imageKey;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified text and image.</summary>
	/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	public ListViewItem(string text, string imageKey)
		: this(text)
	{
		ImageKey = imageKey;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified subitems and image.</summary>
	/// <param name="subItems">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	public ListViewItem(ListViewSubItem[] subItems, string imageKey)
	{
		sub_items = new ListViewSubItemCollection(this, null);
		for (int i = 0; i < subItems.Length; i++)
		{
			sub_items.Add(subItems[i]);
		}
		ImageKey = imageKey;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the subitems containing the specified text, image, colors, and font.</summary>
	/// <param name="items">An array of strings that represent the text of the subitems for the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
	/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
	/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
	/// <param name="font">A <see cref="T:System.Drawing.Font" /> to apply to the item text.</param>
	public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font)
		: this(items, imageKey)
	{
		ForeColor = foreColor;
		BackColor = backColor;
		this.font = font;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class and assigns it to the specified group.</summary>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(ListViewGroup group)
		: this()
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and assigns it to the specified group.</summary>
	/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(string text, ListViewGroup group)
		: this(text)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with an array of strings representing subitems, and assigns the item to the specified group.</summary>
	/// <param name="items">An array of strings that represent the subitems of the new item. </param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(string[] items, ListViewGroup group)
		: this(items)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects, and assigns the item to the specified group.</summary>
	/// <param name="subItems">An array of type <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> that represents the subitems of the item. </param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(ListViewSubItem[] subItems, int imageIndex, ListViewGroup group)
		: this(subItems, imageIndex)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified subitems, image, and group.</summary>
	/// <param name="subItems">An array of <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> objects that represent the subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
	public ListViewItem(ListViewSubItem[] subItems, string imageKey, ListViewGroup group)
		: this(subItems, imageKey)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified item text and the image index position of the item's icon, and assigns the item to the specified group.</summary>
	/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(string text, int imageIndex, ListViewGroup group)
		: this(text, imageIndex)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified text, image, and group.</summary>
	/// <param name="text">The text to display for the item. This should not exceed 259 characters.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
	public ListViewItem(string text, string imageKey, ListViewGroup group)
		: this(text, imageKey)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon and an array of strings representing subitems, and assigns the item to the specified group.</summary>
	/// <param name="items">An array of strings that represent the subitems of the new item. </param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(string[] items, int imageIndex, ListViewGroup group)
		: this(items, imageIndex)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with subitems containing the specified text, image, and group.</summary>
	/// <param name="items">An array of strings that represents the text for subitems of the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
	public ListViewItem(string[] items, string imageKey, ListViewGroup group)
		: this(items, imageKey)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the image index position of the item's icon; the foreground color, background color, and font of the item; and an array of strings representing subitems. Assigns the item to the specified group.</summary>
	/// <param name="items">An array of strings that represent the subitems of the new item. </param>
	/// <param name="imageIndex">The zero-based index of the image within the <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ListView" /> that contains the item. </param>
	/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item. </param>
	/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. </param>
	/// <param name="font">A <see cref="T:System.Drawing.Font" /> that represents the font to display the item's text in. </param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to. </param>
	public ListViewItem(string[] items, int imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup group)
		: this(items, imageIndex, foreColor, backColor, font)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the subitems containing the specified text, image, colors, font, and group.</summary>
	/// <param name="items">An array of strings that represents the text of the subitems for the <see cref="T:System.Windows.Forms.ListViewItem" />.</param>
	/// <param name="imageKey">The name of the image within the <see cref="P:System.Windows.Forms.ListViewItem.ImageList" /> of the owning <see cref="T:System.Windows.Forms.ListView" /> to display in the item.</param>
	/// <param name="foreColor">A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the item.</param>
	/// <param name="backColor">A <see cref="T:System.Drawing.Color" /> that represents the background color of the item.</param>
	/// <param name="font">A <see cref="T:System.Drawing.Font" /> to apply to the item text.</param>
	/// <param name="group">The <see cref="T:System.Windows.Forms.ListViewGroup" /> to assign the item to.</param>
	public ListViewItem(string[] items, string imageKey, Color foreColor, Color backColor, Font font, ListViewGroup group)
		: this(items, imageKey, foreColor, backColor, font)
	{
		Group = group;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItem" /> class with the specified serialization information and streaming context.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing information about the <see cref="T:System.Windows.Forms.ListViewItem" /> to be initialized.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the source destination and context information of a serialized stream.</param>
	protected ListViewItem(SerializationInfo info, StreamingContext context)
	{
		Deserialize(info, context);
	}

	/// <summary>Serializes the item.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item.  </param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized.</param>
	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
	{
		Serialize(info, context);
	}

	internal void OnUIATextChanged()
	{
		if (this.UIATextChanged != null)
		{
			this.UIATextChanged(this, EventArgs.Empty);
		}
	}

	internal void OnUIASubItemTextChanged(LabelEditEventArgs args)
	{
		if (args.Item == 0)
		{
			OnUIATextChanged();
		}
		if (this.UIASubItemTextChanged != null)
		{
			this.UIASubItemTextChanged(this, args);
		}
	}

	/// <summary>Places the item text into edit mode.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.LabelEdit" /> property of the associated <see cref="T:System.Windows.Forms.ListView" /> is not set to true. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void BeginEdit()
	{
		if (owner != null && owner.LabelEdit)
		{
			owner.item_control.BeginEdit(this);
		}
	}

	/// <summary>Creates an identical copy of the item.</summary>
	/// <returns>An object that represents an item that has the same text, image, and subitems associated with it as the cloned item.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual object Clone()
	{
		ListViewItem listViewItem = new ListViewItem();
		listViewItem.image_index = image_index;
		listViewItem.is_checked = is_checked;
		listViewItem.selected = selected;
		listViewItem.font = font;
		listViewItem.state_image_index = state_image_index;
		listViewItem.sub_items = new ListViewSubItemCollection(this, null);
		foreach (ListViewSubItem sub_item in sub_items)
		{
			listViewItem.sub_items.Add(sub_item.Text, sub_item.ForeColor, sub_item.BackColor, sub_item.Font);
		}
		listViewItem.tag = tag;
		listViewItem.use_item_style = use_item_style;
		listViewItem.owner = null;
		listViewItem.name = name;
		listViewItem.tooltip_text = tooltip_text;
		return listViewItem;
	}

	/// <summary>Ensures that the item is visible within the control, scrolling the contents of the control, if necessary.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void EnsureVisible()
	{
		if (owner != null)
		{
			owner.EnsureVisible(owner.Items.IndexOf(this));
		}
	}

	/// <summary>Finds the next item from the <see cref="T:System.Windows.Forms.ListViewItem" />, searching in the specified direction.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that is closest to the given coordinates, searching in the specified direction.</returns>
	/// <param name="searchDirection">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListView.View" /> property of the containing <see cref="T:System.Windows.Forms.ListView" /> is set to a value other than <see cref="F:System.Windows.Forms.View.SmallIcon" /> or <see cref="F:System.Windows.Forms.View.LargeIcon" />. </exception>
	public ListViewItem FindNearestItem(SearchDirectionHint searchDirection)
	{
		if (owner == null)
		{
			return null;
		}
		Point itemLocation = owner.GetItemLocation(display_index);
		return owner.FindNearestItem(searchDirection, itemLocation);
	}

	/// <summary>Retrieves the specified portion of the bounding rectangle for the item.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified portion of the item.</returns>
	/// <param name="portion">One of the <see cref="T:System.Windows.Forms.ItemBoundsPortion" /> values that represents a portion of the item for which to retrieve the bounding rectangle. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Rectangle GetBounds(ItemBoundsPortion portion)
	{
		if (owner == null)
		{
			return Rectangle.Empty;
		}
		if (owner.VirtualMode && bounds == Rectangle.Empty)
		{
			Layout();
		}
		Rectangle result = portion switch
		{
			ItemBoundsPortion.Icon => icon_rect, 
			ItemBoundsPortion.Label => label_rect, 
			ItemBoundsPortion.ItemOnly => item_rect, 
			ItemBoundsPortion.Entire => bounds, 
			_ => throw new ArgumentException("Invalid value for portion."), 
		};
		Point itemLocation = owner.GetItemLocation(DisplayIndex);
		result.X += itemLocation.X;
		result.Y += itemLocation.Y;
		return result;
	}

	/// <summary>Returns the subitem of the <see cref="T:System.Windows.Forms.ListViewItem" /> at the specified coordinates.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the specified x- and y-coordinates.</returns>
	/// <param name="x">The x-coordinate. </param>
	/// <param name="y">The y-coordinate.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public ListViewSubItem GetSubItemAt(int x, int y)
	{
		if (owner != null && owner.View != View.Details)
		{
			return null;
		}
		foreach (ListViewSubItem sub_item in sub_items)
		{
			if (sub_item.Bounds.Contains(x, y))
			{
				return sub_item;
			}
		}
		return null;
	}

	/// <summary>Removes the item from its associated <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	public virtual void Remove()
	{
		if (owner != null)
		{
			owner.item_control.CancelEdit(this);
			owner.Items.Remove(this);
			owner = null;
		}
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"ListViewItem: {Text}";
	}

	/// <summary>Deserializes the item.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to deserialize the item. </param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being deserialized. </param>
	protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
	{
		sub_items = new ListViewSubItemCollection(this, null);
		int num = 0;
		SerializationInfoEnumerator enumerator = info.GetEnumerator();
		while (enumerator.MoveNext())
		{
			SerializationEntry current = enumerator.Current;
			switch (current.Name)
			{
			case "Text":
				sub_items.Add((string)current.Value);
				break;
			case "Font":
				font = (Font)current.Value;
				break;
			case "Checked":
				is_checked = (bool)current.Value;
				break;
			case "ImageIndex":
				image_index = (int)current.Value;
				break;
			case "StateImageIndex":
				state_image_index = (int)current.Value;
				break;
			case "UseItemStyleForSubItems":
				use_item_style = (bool)current.Value;
				break;
			case "SubItemCount":
				num = (int)current.Value;
				break;
			case "Group":
				group = (ListViewGroup)current.Value;
				break;
			case "ImageKey":
				if (image_index == -1)
				{
					image_key = (string)current.Value;
				}
				break;
			}
		}
		Type typeFromHandle = typeof(ListViewSubItem);
		if (num > 0)
		{
			sub_items.Clear();
			Text = info.GetString("Text");
			for (int i = 0; i < num - 1; i++)
			{
				sub_items.Add((ListViewSubItem)info.GetValue("SubItem" + (i + 1), typeFromHandle));
			}
		}
		ForeColor = (Color)info.GetValue("ForeColor", typeof(Color));
		BackColor = (Color)info.GetValue("BackColor", typeof(Color));
	}

	/// <summary>Serializes the item.</summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the data needed to serialize the item. </param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that represents the source and destination of the stream being serialized. </param>
	protected virtual void Serialize(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Text", Text);
		info.AddValue("Font", Font);
		info.AddValue("ImageIndex", image_index);
		info.AddValue("Checked", is_checked);
		info.AddValue("StateImageIndex", state_image_index);
		info.AddValue("UseItemStyleForSubItems", use_item_style);
		info.AddValue("BackColor", BackColor);
		info.AddValue("ForeColor", ForeColor);
		info.AddValue("ImageKey", image_key);
		info.AddValue("Group", group);
		if (sub_items.Count > 1)
		{
			info.AddValue("SubItemCount", sub_items.Count);
			for (int i = 1; i < sub_items.Count; i++)
			{
				info.AddValue("SubItem" + i, sub_items[i]);
			}
		}
	}

	internal void SetGroup(ListViewGroup group)
	{
		this.group = group;
	}

	internal void SetPosition(Point position)
	{
		this.position = position;
	}

	private void UpdateFocusedState()
	{
		if (owner != null)
		{
			Invalidate();
			Layout();
			Invalidate();
		}
	}

	internal void Invalidate()
	{
		if (owner != null && owner.item_control != null && !owner.updating)
		{
			Rectangle rc = Bounds;
			rc.Inflate(1, 1);
			owner.item_control.Invalidate(rc);
		}
	}

	internal void Layout()
	{
		if (owner == null)
		{
			return;
		}
		Size text_size = owner.text_size;
		checkbox_rect = Rectangle.Empty;
		if (owner.CheckBoxes)
		{
			checkbox_rect.Size = owner.CheckBoxSize;
		}
		switch (owner.View)
		{
		case View.Details:
		{
			int num11 = 0;
			if (owner.SmallImageList != null)
			{
				num11 = indent_count * owner.SmallImageList.ImageSize.Width;
			}
			if (owner.Columns.Count > 0)
			{
				checkbox_rect.X = owner.Columns[0].Rect.X + num11;
			}
			icon_rect = (label_rect = Rectangle.Empty);
			icon_rect.X = checkbox_rect.Right + 2;
			int height = owner.ItemSize.Height;
			if (owner.SmallImageList != null)
			{
				icon_rect.Width = owner.SmallImageList.ImageSize.Width;
			}
			ref Rectangle reference = ref label_rect;
			int height2 = height;
			icon_rect.Height = height2;
			reference.Height = height2;
			checkbox_rect.Y = height - checkbox_rect.Height;
			label_rect.X = ((icon_rect.Width <= 0) ? icon_rect.Right : (icon_rect.Right + 1));
			if (owner.Columns.Count > 0)
			{
				label_rect.Width = owner.Columns[0].Wd - label_rect.X + checkbox_rect.X;
			}
			else
			{
				label_rect.Width = text_size.Width;
			}
			SizeF sizeF3 = TextRenderer.MeasureString(Text, Font);
			text_bounds = label_rect;
			text_bounds.Width = (int)sizeF3.Width;
			Rectangle rectangle = (item_rect = Rectangle.Union(Rectangle.Union(checkbox_rect, icon_rect), label_rect));
			bounds.Size = rectangle.Size;
			item_rect.Width = 0;
			bounds.Width = 0;
			for (int k = 0; k < owner.Columns.Count; k++)
			{
				item_rect.Width += owner.Columns[k].Wd;
				bounds.Width += owner.Columns[k].Wd;
			}
			int num12 = Math.Min(owner.Columns.Count, sub_items.Count);
			for (int l = 0; l < num12; l++)
			{
				Rectangle rect = owner.Columns[l].Rect;
				sub_items[l].SetBounds(rect.X, 0, rect.Width, height);
			}
			break;
		}
		case View.LargeIcon:
		{
			label_rect = (icon_rect = Rectangle.Empty);
			SizeF sizeF2 = TextRenderer.MeasureString(Text, Font);
			if ((int)sizeF2.Width > text_size.Width)
			{
				if (Focused && owner.InternalContainsFocus)
				{
					int width = text_size.Width;
					StringFormat stringFormat = new StringFormat();
					stringFormat.Alignment = StringAlignment.Center;
					text_size.Height = (int)TextRenderer.MeasureString(Text, Font, width, stringFormat).Height;
				}
				else
				{
					text_size.Height = 2 * (int)sizeF2.Height;
				}
			}
			if (owner.LargeImageList != null)
			{
				icon_rect.Width = owner.LargeImageList.ImageSize.Width;
				icon_rect.Height = owner.LargeImageList.ImageSize.Height;
			}
			if (checkbox_rect.Height > icon_rect.Height)
			{
				icon_rect.Y = checkbox_rect.Height - icon_rect.Height;
			}
			else
			{
				checkbox_rect.Y = icon_rect.Height - checkbox_rect.Height;
			}
			if (text_size.Width <= icon_rect.Width)
			{
				icon_rect.X = checkbox_rect.Width + 1;
				label_rect.X = icon_rect.X + (icon_rect.Width - text_size.Width) / 2;
				label_rect.Y = icon_rect.Bottom + 2;
				label_rect.Size = text_size;
			}
			else
			{
				int num10 = text_size.Width / 2;
				icon_rect.X = checkbox_rect.Width + 1 + num10 - icon_rect.Width / 2;
				label_rect.X = checkbox_rect.Width + 1;
				label_rect.Y = icon_rect.Bottom + 2;
				label_rect.Size = text_size;
			}
			item_rect = Rectangle.Union(icon_rect, label_rect);
			Rectangle rectangle = Rectangle.Union(item_rect, checkbox_rect);
			bounds.Size = rectangle.Size;
			break;
		}
		case View.SmallIcon:
		case View.List:
		{
			label_rect = (icon_rect = Rectangle.Empty);
			icon_rect.X = checkbox_rect.Width + 1;
			int height = Math.Max(owner.CheckBoxSize.Height, text_size.Height);
			if (owner.SmallImageList != null)
			{
				height = Math.Max(height, owner.SmallImageList.ImageSize.Height);
				icon_rect.Width = owner.SmallImageList.ImageSize.Width;
				icon_rect.Height = owner.SmallImageList.ImageSize.Height;
			}
			checkbox_rect.Y = height - checkbox_rect.Height;
			label_rect.X = icon_rect.Right + 1;
			label_rect.Width = text_size.Width;
			ref Rectangle reference2 = ref label_rect;
			int height2 = height;
			icon_rect.Height = height2;
			reference2.Height = height2;
			item_rect = Rectangle.Union(icon_rect, label_rect);
			Rectangle rectangle = Rectangle.Union(item_rect, checkbox_rect);
			bounds.Size = rectangle.Size;
			break;
		}
		case View.Tile:
		{
			if (!Application.VisualStylesEnabled)
			{
				goto case View.LargeIcon;
			}
			label_rect = (icon_rect = Rectangle.Empty);
			if (owner.LargeImageList != null)
			{
				icon_rect.Width = owner.LargeImageList.ImageSize.Width;
				icon_rect.Height = owner.LargeImageList.ImageSize.Height;
			}
			int num = 2;
			SizeF sizeF = TextRenderer.MeasureString(Text, Font);
			int num2 = (int)Math.Ceiling(sizeF.Height);
			int num3 = (int)Math.Ceiling(sizeF.Width);
			sub_items[0].bounds.Height = num2;
			int num4 = num2;
			int num5 = num3;
			int num6 = Math.Min(owner.Columns.Count, sub_items.Count);
			for (int i = 1; i < num6; i++)
			{
				ListViewSubItem listViewSubItem = sub_items[i];
				if (listViewSubItem.Text != null && listViewSubItem.Text.Length != 0)
				{
					sizeF = TextRenderer.MeasureString(listViewSubItem.Text, listViewSubItem.Font);
					int num7 = (int)Math.Ceiling(sizeF.Width);
					if (num7 > num5)
					{
						num5 = num7;
					}
					int num8 = (int)Math.Ceiling(sizeF.Height);
					num4 += num8 + num;
					listViewSubItem.bounds.Height = num8;
				}
			}
			num5 = Math.Min(num5, owner.TileSize.Width - (icon_rect.Width + 4));
			label_rect.X = icon_rect.Right + 4;
			label_rect.Y = owner.TileSize.Height / 2 - num4 / 2;
			label_rect.Width = num5;
			label_rect.Height = num4;
			sub_items[0].SetBounds(label_rect.X, label_rect.Y, num5, sub_items[0].bounds.Height);
			int num9 = sub_items[0].bounds.Bottom + num;
			for (int j = 1; j < num6; j++)
			{
				ListViewSubItem listViewSubItem2 = sub_items[j];
				if (listViewSubItem2.Text != null && listViewSubItem2.Text.Length != 0)
				{
					listViewSubItem2.SetBounds(label_rect.X, num9, num5, listViewSubItem2.bounds.Height);
					num9 += listViewSubItem2.Bounds.Height + num;
				}
			}
			item_rect = Rectangle.Union(icon_rect, label_rect);
			bounds.Size = item_rect.Size;
			break;
		}
		}
	}
}
