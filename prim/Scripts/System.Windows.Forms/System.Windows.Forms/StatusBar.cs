using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows status bar control. Although <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.StatusBar" /> control of previous versions, <see cref="T:System.Windows.Forms.StatusBar" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[DefaultEvent("PanelClick")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.StatusBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("Text")]
public class StatusBar : Control
{
	/// <summary>Represents the collection of panels in a <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
	[ListBindable(false)]
	public class StatusBarPanelCollection : ICollection, IEnumerable, IList
	{
		private StatusBar owner;

		private ArrayList panels = new ArrayList();

		private int last_index_by_key;

		private static object UIACollectionChangedEvent;

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => panels.IsSynchronized;

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>The object used to synchronize access to the collection.</returns>
		object ICollection.SyncRoot => panels.SyncRoot;

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</exception>
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is StatusBarPanel))
				{
					throw new ArgumentException("Value must be of type StatusBarPanel.", "value");
				}
				this[index] = (StatusBarPanel)value;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => false;

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects in the collection.</returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public int Count => panels.Count;

		/// <summary>Gets a value indicating whether this collection is read-only.</summary>
		/// <returns>true if this collection is read-only; otherwise, false.</returns>
		public bool IsReadOnly => false;

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel located at the specified index within the collection.</returns>
		/// <param name="index">The index of the panel in the collection to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> assigned to the collection was null. </exception>
		public virtual StatusBarPanel this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (StatusBarPanel)panels[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("index");
				}
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, index));
				value.SetParent(owner);
				panels[index] = value;
				OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, index));
			}
		}

		/// <summary>Gets an item with the specified key from the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</returns>
		/// <param name="key">The name of the item to retrieve from the collection.</param>
		public virtual StatusBarPanel this[string key]
		{
			get
			{
				int num = IndexOfKey(key);
				if (num >= 0 && num < Count)
				{
					return (StatusBarPanel)panels[num];
				}
				return null;
			}
		}

		internal event CollectionChangeEventHandler UIACollectionChanged
		{
			add
			{
				owner.Events.AddHandler(UIACollectionChangedEvent, value);
			}
			remove
			{
				owner.Events.RemoveHandler(UIACollectionChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.StatusBar" /> control that contains this collection. </param>
		public StatusBarPanelCollection(StatusBar owner)
		{
			this.owner = owner;
		}

		static StatusBarPanelCollection()
		{
			UIACollectionChanged = new object();
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> is greater than the available space from index to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type in the collection cannot be cast automatically to the type of <paramref name="array" />.</exception>
		void ICollection.CopyTo(Array dest, int index)
		{
			panels.CopyTo(dest, index);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> to the collection.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.-or-The parent of value is not null.</exception>
		int IList.Add(object value)
		{
			if (!(value is StatusBarPanel))
			{
				throw new ArgumentException("Value must be of type StatusBarPanel.", "value");
			}
			return AddInternal((StatusBarPanel)value, refresh: true);
		}

		/// <summary>Determines whether the specified panel is located within the collection.</summary>
		/// <returns>true if panel is a <see cref="T:System.Windows.Forms.StatusBarPanel" /> located within the collection; otherwise, false.</returns>
		/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
		bool IList.Contains(object panel)
		{
			return panels.Contains(panel);
		}

		/// <summary>Returns the index of the specified panel within the collection.</summary>
		/// <returns>The zero-based index of panel, if found, within the entire collection; otherwise, -1.</returns>
		/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
		int IList.IndexOf(object panel)
		{
			return panels.IndexOf(panel);
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index location where the panel is inserted. </param>
		/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than zero or greater than the value of the Count property.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.-or-The parent of value is not null.</exception>
		void IList.Insert(int index, object value)
		{
			if (!(value is StatusBarPanel))
			{
				throw new ArgumentException("Value must be of type StatusBarPanel.", "value");
			}
			Insert(index, (StatusBarPanel)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to remove from the collection.</param>
		void IList.Remove(object value)
		{
			StatusBarPanel value2 = value as StatusBarPanel;
			Remove(value2);
		}

		internal void OnUIACollectionChanged(CollectionChangeEventArgs e)
		{
			((CollectionChangeEventHandler)owner.Events[UIACollectionChanged])?.Invoke(owner, e);
		}

		private int AddInternal(StatusBarPanel p, bool refresh)
		{
			if (p == null)
			{
				throw new ArgumentNullException("value");
			}
			p.SetParent(owner);
			int num = panels.Add(p);
			if (refresh)
			{
				owner.CalcPanelSizes();
				owner.Refresh();
			}
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, num));
			return num;
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> to the collection.</summary>
		/// <returns>The zero-based index of the item in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> being added to the collection was null. </exception>
		/// <exception cref="T:System.ArgumentException">The parent of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> specified in the <paramref name="value" /> parameter is not null. </exception>
		public virtual int Add(StatusBarPanel value)
		{
			return AddInternal(value, refresh: true);
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified text to the collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel that was added to the collection.</returns>
		/// <param name="text">The text for the <see cref="T:System.Windows.Forms.StatusBarPanel" /> that is being added. </param>
		public virtual StatusBarPanel Add(string text)
		{
			StatusBarPanel statusBarPanel = new StatusBarPanel();
			statusBarPanel.Text = text;
			Add(statusBarPanel);
			return statusBarPanel;
		}

		/// <summary>Adds an array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects to the collection.</summary>
		/// <param name="panels">An array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects being added to the collection was null. </exception>
		public virtual void AddRange(StatusBarPanel[] panels)
		{
			if (panels == null)
			{
				throw new ArgumentNullException("panels");
			}
			if (panels.Length != 0)
			{
				for (int i = 0; i < panels.Length; i++)
				{
					AddInternal(panels[i], refresh: false);
				}
				owner.Refresh();
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		public virtual void Clear()
		{
			panels.Clear();
			owner.Refresh();
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, -1));
		}

		/// <summary>Determines whether the specified panel is located within the collection.</summary>
		/// <returns>true if the panel is located within the collection; otherwise, false.</returns>
		/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection. </param>
		public bool Contains(StatusBarPanel panel)
		{
			return panels.Contains(panel);
		}

		/// <summary>Determines whether the collection contains a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key. </summary>
		/// <returns>true to indicate the collection contains a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key; otherwise, false. </returns>
		/// <param name="key">The name of the item to find in the collection.</param>
		public virtual bool ContainsKey(string key)
		{
			int num = IndexOfKey(key);
			return num >= 0 && num < Count;
		}

		/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return panels.GetEnumerator();
		}

		/// <summary>Returns the index within the collection of the specified panel.</summary>
		/// <returns>The zero-based index where the panel is located within the collection; otherwise, negative one (-1).</returns>
		/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection. </param>
		public int IndexOf(StatusBarPanel panel)
		{
			return panels.IndexOf(panel);
		}

		/// <summary>Returns the index of the first occurrence of a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</summary>
		/// <returns>The zero-based index of the first occurrence of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key, if found; otherwise, -1.</returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to find in the collection.</param>
		public virtual int IndexOfKey(string key)
		{
			if (key == null || key == string.Empty)
			{
				return -1;
			}
			if (last_index_by_key >= 0 && last_index_by_key < Count && string.Compare(((StatusBarPanel)panels[last_index_by_key]).Name, key, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return last_index_by_key;
			}
			for (int i = 0; i < Count; i++)
			{
				if (panels[i] is StatusBarPanel statusBarPanel && string.Compare(statusBarPanel.Name, key, StringComparison.OrdinalIgnoreCase) == 0)
				{
					last_index_by_key = i;
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index location where the panel is inserted. </param>
		/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel to insert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter's parent is not null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property of the <paramref name="value" /> parameter's panel is not a valid <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> value. </exception>
		public virtual void Insert(int index, StatusBarPanel value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index > Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			value.SetParent(owner);
			panels.Insert(index, value);
			owner.Refresh();
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, index));
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> assigned to the <paramref name="value" /> parameter is null. </exception>
		public virtual void Remove(StatusBarPanel value)
		{
			int num = IndexOf(value);
			panels.Remove(value);
			if (num >= 0)
			{
				OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, num));
			}
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.StatusBarPanel" /> located at the specified index within the collection.</summary>
		/// <param name="index">The zero-based index of the item to remove. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class. </exception>
		public virtual void RemoveAt(int index)
		{
			panels.RemoveAt(index);
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, index));
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key from the collection.</summary>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to remove from the collection.</param>
		public virtual void RemoveByKey(string key)
		{
			int num = IndexOfKey(key);
			if (num >= 0 && num < Count)
			{
				RemoveAt(num);
			}
		}
	}

	private StatusBarPanelCollection panels;

	private bool show_panels;

	private bool sizing_grip = true;

	private Timer tooltip_timer;

	private ToolTip tooltip_window;

	private StatusBarPanel tooltip_currently_showing;

	private static object DrawItemEvent;

	private static object PanelClickEvent;

	/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that is the background color of the control</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
		}
	}

	/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.StatusBar" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
		}
	}

	/// <summary>Gets or sets the layout of the background image of the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override ImageLayout BackgroundImageLayout
	{
		get
		{
			return base.BackgroundImageLayout;
		}
		set
		{
			base.BackgroundImageLayout = value;
		}
	}

	/// <summary>Gets or sets the docking behavior of the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is Bottom.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(DockStyle.Bottom)]
	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			base.Dock = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker, however this property has no effect on the <see cref="T:System.Windows.Forms.StatusBar" /> control</summary>
	/// <returns>true if the control has a secondary buffer; otherwise, false. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override bool DoubleBuffered
	{
		get
		{
			return base.DoubleBuffered;
		}
		set
		{
			base.DoubleBuffered = value;
		}
	}

	/// <summary>Gets or sets the font the <see cref="T:System.Windows.Forms.StatusBar" /> control will use to display information.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> of the text. The default is the font of the container, unless you override it.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public override Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			if (value != Font)
			{
				base.Font = value;
				UpdateStatusBar();
			}
		}
	}

	/// <summary>Gets or sets the forecolor for the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the forecolor of the control. The default is Empty.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new ImeMode ImeMode
	{
		get
		{
			return base.ImeMode;
		}
		set
		{
			base.ImeMode = value;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.StatusBar" /> panels contained within the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> containing the <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects of the <see cref="T:System.Windows.Forms.StatusBar" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	[MergableProperty(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(true)]
	public StatusBarPanelCollection Panels
	{
		get
		{
			if (panels == null)
			{
				panels = new StatusBarPanelCollection(this);
			}
			return panels;
		}
	}

	/// <summary>Gets or sets a value indicating whether any panels that have been added to the control are displayed.</summary>
	/// <returns>true if panels are displayed; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool ShowPanels
	{
		get
		{
			return show_panels;
		}
		set
		{
			if (show_panels != value)
			{
				show_panels = value;
				UpdateStatusBar();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether a sizing grip is displayed in the lower-right corner of the control.</summary>
	/// <returns>true if a sizing grip is displayed; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool SizingGrip
	{
		get
		{
			return sizing_grip;
		}
		set
		{
			if (sizing_grip != value)
			{
				sizing_grip = value;
				UpdateStatusBar();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the user will be able to tab to the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
	/// <returns>true if the tab key moves focus to the <see cref="T:System.Windows.Forms.StatusBar" />; otherwise false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public new bool TabStop
	{
		get
		{
			return base.TabStop;
		}
		set
		{
			base.TabStop = value;
		}
	}

	/// <summary>Gets or sets the text associated with the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
	/// <returns>The text associated with the <see cref="T:System.Windows.Forms.StatusBar" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			if (!(value == Text))
			{
				base.Text = value;
				UpdateStatusBar();
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.CreateParams" /> used to create the handle for this control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.CreateParams" /> used to create the handle for this control.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	protected override ImeMode DefaultImeMode => ImeMode.Disable;

	/// <summary>Gets the default size of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.StatusBarDefaultSize;

	private Timer ToolTipTimer
	{
		get
		{
			if (tooltip_timer == null)
			{
				tooltip_timer = new Timer();
				tooltip_timer.Enabled = false;
				tooltip_timer.Interval = 500;
				tooltip_timer.Tick += ToolTipTimer_Tick;
			}
			return tooltip_timer;
		}
	}

	private ToolTip ToolTipWindow
	{
		get
		{
			if (tooltip_window == null)
			{
				tooltip_window = new ToolTip();
			}
			return tooltip_window;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackColorChanged
	{
		add
		{
			base.BackColorChanged += value;
		}
		remove
		{
			base.BackColorChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackgroundImage" /> property is changed.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			base.BackgroundImageChanged += value;
		}
		remove
		{
			base.BackgroundImageChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackgroundImageLayout" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.BackgroundImageLayoutChanged += value;
		}
		remove
		{
			base.BackgroundImageLayoutChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.ForeColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler ForeColorChanged
	{
		add
		{
			base.ForeColorChanged += value;
		}
		remove
		{
			base.ForeColorChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.ImeMode" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler ImeModeChanged
	{
		add
		{
			base.ImeModeChanged += value;
		}
		remove
		{
			base.ImeModeChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.StatusBar" /> control is redrawn.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event PaintEventHandler Paint
	{
		add
		{
			base.Paint += value;
		}
		remove
		{
			base.Paint -= value;
		}
	}

	/// <summary>Occurs when a visual aspect of an owner-drawn status bar control changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event StatusBarDrawItemEventHandler DrawItem
	{
		add
		{
			base.Events.AddHandler(DrawItemEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DrawItemEvent, value);
		}
	}

	/// <summary>Occurs when a <see cref="T:System.Windows.Forms.StatusBarPanel" /> object on a <see cref="T:System.Windows.Forms.StatusBar" /> control is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event StatusBarPanelClickEventHandler PanelClick
	{
		add
		{
			base.Events.AddHandler(PanelClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PanelClickEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBar" /> class.</summary>
	public StatusBar()
	{
		Dock = DockStyle.Bottom;
		TabStop = false;
		SetStyle(ControlStyles.UserPaint | ControlStyles.Selectable, value: false);
		base.MouseMove += StatusBar_MouseMove;
		base.MouseLeave += StatusBar_MouseLeave;
	}

	static StatusBar()
	{
		DrawItem = new object();
		PanelClick = new object();
	}

	/// <summary>Returns a string representation for this control.</summary>
	/// <returns>String </returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Panels.Count: " + Panels.Count + ((Panels.Count <= 0) ? string.Empty : (", Panels[0]: " + Panels[0]));
	}

	/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.CreateHandle" />.</summary>
	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnDrawItem(System.Windows.Forms.StatusBarDrawItemEventArgs)" /> event.</summary>
	/// <param name="sbdievent">A <see cref="T:System.Windows.Forms.StatusBarDrawItemEventArgs" /> that contains the event data. </param>
	protected virtual void OnDrawItem(StatusBarDrawItemEventArgs sbdievent)
	{
		((StatusBarDrawItemEventHandler)base.Events[DrawItem])?.Invoke(this, sbdievent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
		CalcPanelSizes();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <summary>Raises the Layout event.</summary>
	/// <param name="levent">A LayoutEventArgs that contains the event data. </param>
	protected override void OnLayout(LayoutEventArgs levent)
	{
		base.OnLayout(levent);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (panels == null)
		{
			return;
		}
		float num = 0f;
		float num2 = ThemeEngine.Current.StatusBarHorzGapWidth;
		for (int i = 0; i < panels.Count; i++)
		{
			float num3 = (float)panels[i].Width + num + ((i != panels.Count - 1) ? (num2 / 2f) : num2);
			if ((float)e.X >= num && (float)e.X <= num3)
			{
				OnPanelClick(new StatusBarPanelClickEventArgs(panels[i], e.Button, e.Clicks, e.X, e.Y));
				break;
			}
			num = num3;
		}
		base.OnMouseDown(e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnPanelClick(System.Windows.Forms.StatusBarPanelClickEventArgs)" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.StatusBarPanelClickEventArgs" /> that contains the event data. </param>
	protected virtual void OnPanelClick(StatusBarPanelClickEventArgs e)
	{
		((StatusBarPanelClickEventHandler)base.Events[PanelClick])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnResize(System.EventArgs)" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		if (base.Width > 0 && base.Height > 0)
		{
			UpdateStatusBar();
		}
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal void OnDrawItemInternal(StatusBarDrawItemEventArgs e)
	{
		OnDrawItem(e);
	}

	internal void UpdatePanel(StatusBarPanel panel)
	{
		if (panel.AutoSize == StatusBarPanelAutoSize.Contents)
		{
			UpdateStatusBar();
		}
		else
		{
			UpdateStatusBar();
		}
	}

	internal void UpdatePanelContents(StatusBarPanel panel)
	{
		if (panel.AutoSize == StatusBarPanelAutoSize.Contents)
		{
			UpdateStatusBar();
			Invalidate();
		}
		else
		{
			Invalidate(new Rectangle(panel.X + 2, 2, panel.Width - 4, bounds.Height - 4));
		}
	}

	private void UpdateStatusBar()
	{
		CalcPanelSizes();
		Refresh();
	}

	internal override void OnPaintInternal(PaintEventArgs pevent)
	{
		Draw(pevent.Graphics, pevent.ClipRectangle);
	}

	private void CalcPanelSizes()
	{
		if (panels == null || !show_panels || base.Width == 0 || base.Height == 0)
		{
			return;
		}
		int num = 2;
		int statusBarHorzGapWidth = ThemeEngine.Current.StatusBarHorzGapWidth;
		int num2 = 0;
		ArrayList arrayList = null;
		num2 = num;
		for (int i = 0; i < panels.Count; i++)
		{
			StatusBarPanel statusBarPanel = panels[i];
			if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.None)
			{
				num2 += statusBarPanel.Width;
				num2 += statusBarHorzGapWidth;
			}
			else if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Contents)
			{
				int num3 = (int)(TextRenderer.MeasureString(statusBarPanel.Text, Font).Width + 0.5f);
				if (statusBarPanel.Icon != null)
				{
					num3 += 21;
				}
				statusBarPanel.SetWidth(num3 + 8);
				num2 += statusBarPanel.Width;
				num2 += statusBarHorzGapWidth;
			}
			else if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Spring)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				arrayList.Add(statusBarPanel);
				num2 += statusBarHorzGapWidth;
			}
		}
		if (arrayList != null)
		{
			int count = arrayList.Count;
			int num4 = base.Width - num2 - (SizingGrip ? ThemeEngine.Current.StatusBarSizeGripWidth : 0);
			for (int j = 0; j < count; j++)
			{
				StatusBarPanel statusBarPanel2 = (StatusBarPanel)arrayList[j];
				int num5 = num4 / count;
				statusBarPanel2.SetWidth((num5 < statusBarPanel2.MinWidth) ? statusBarPanel2.MinWidth : num5);
			}
		}
		num2 = num;
		for (int k = 0; k < panels.Count; k++)
		{
			StatusBarPanel statusBarPanel3 = panels[k];
			statusBarPanel3.X = num2;
			num2 += statusBarPanel3.Width + statusBarHorzGapWidth;
		}
	}

	private void Draw(Graphics dc, Rectangle clip)
	{
		ThemeEngine.Current.DrawStatusBar(dc, clip, this);
	}

	private void StatusBar_MouseMove(object sender, MouseEventArgs e)
	{
		if (show_panels)
		{
			StatusBarPanel panelAtPoint = GetPanelAtPoint(e.Location);
			if (panelAtPoint != tooltip_currently_showing)
			{
				MouseLeftPanel(tooltip_currently_showing);
			}
			if (panelAtPoint != null && tooltip_currently_showing == null)
			{
				MouseEnteredPanel(panelAtPoint);
			}
		}
	}

	private void StatusBar_MouseLeave(object sender, EventArgs e)
	{
		if (tooltip_currently_showing != null)
		{
			MouseLeftPanel(tooltip_currently_showing);
		}
	}

	private StatusBarPanel GetPanelAtPoint(Point point)
	{
		foreach (StatusBarPanel panel in Panels)
		{
			if (point.X >= panel.X && point.X <= panel.X + panel.Width)
			{
				return panel;
			}
		}
		return null;
	}

	private void MouseEnteredPanel(StatusBarPanel item)
	{
		tooltip_currently_showing = item;
		ToolTipTimer.Start();
	}

	private void MouseLeftPanel(StatusBarPanel item)
	{
		ToolTipTimer.Stop();
		ToolTipWindow.Hide(this);
		tooltip_currently_showing = null;
	}

	private void ToolTipTimer_Tick(object o, EventArgs args)
	{
		string toolTipText = tooltip_currently_showing.ToolTipText;
		if (toolTipText != null && toolTipText.Length > 0)
		{
			ToolTipWindow.Present(this, toolTipText);
		}
		ToolTipTimer.Stop();
	}
}
