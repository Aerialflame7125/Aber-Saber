using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows toolbar. Although <see cref="T:System.Windows.Forms.ToolStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ToolBar" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBar" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>1</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
[DefaultEvent("ButtonClick")]
[DefaultProperty("Buttons")]
[Designer("System.Windows.Forms.Design.ToolBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class ToolBar : Control
{
	/// <summary>Encapsulates a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls for use by the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
	public class ToolBarButtonCollection : ICollection, IEnumerable, IList
	{
		private ArrayList list;

		private ToolBar owner;

		private bool redraw;

		private static object UIACollectionChangedEvent;

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		bool ICollection.IsSynchronized => list.IsSynchronized;

		/// <summary>Gets an object that can be used to synchronize access to the collection of buttons.</summary>
		object ICollection.SyncRoot => list.SyncRoot;

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => list.IsFixedSize;

		/// <summary>Gets or sets the item at a specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "value");
				}
				this[index] = (ToolBarButton)value;
			}
		}

		/// <summary>Gets the number of buttons in the toolbar button collection.</summary>
		/// <returns>The number of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar.</returns>
		[Browsable(false)]
		public int Count => list.Count;

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		public bool IsReadOnly => list.IsReadOnly;

		/// <summary>Gets or sets the toolbar button at the specified indexed location in the toolbar button collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolBarButton" /> that represents the toolbar button at the specified indexed location.</returns>
		/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="index" /> value is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero.-or- The <paramref name="index" /> value is greater than the number of buttons in the collection, and the collection of buttons is not null. </exception>
		public virtual ToolBarButton this[int index]
		{
			get
			{
				return (ToolBarButton)list[index];
			}
			set
			{
				OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, index));
				value.SetParent(owner);
				list[index] = value;
				owner.Redraw(recalculate: true);
				OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, index));
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key from the collection.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolBarButton" /> whose <see cref="P:System.Windows.Forms.ToolBarButton.Name" /> property matches the specified key.</returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to retrieve.</param>
		public virtual ToolBarButton this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				foreach (ToolBarButton item in list)
				{
					if (string.Compare(item.Name, key, ignoreCase: true) == 0)
					{
						return item;
					}
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> class and assigns it to the specified toolbar.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolBar" /> that is the parent of the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. </param>
		public ToolBarButtonCollection(ToolBar owner)
		{
			list = new ArrayList();
			this.owner = owner;
			redraw = true;
		}

		static ToolBarButtonCollection()
		{
			UIACollectionChanged = new object();
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins. </param>
		void ICollection.CopyTo(Array dest, int index)
		{
			list.CopyTo(dest, index);
		}

		/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
		int IList.Add(object button)
		{
			if (!(button is ToolBarButton))
			{
				throw new ArgumentException("Not of type ToolBarButton", "button");
			}
			return Add((ToolBarButton)button);
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the item is found in the collection; otherwise, false.</returns>
		/// <param name="button">The item to locate in the collection. </param>
		bool IList.Contains(object button)
		{
			if (!(button is ToolBarButton))
			{
				throw new ArgumentException("Not of type ToolBarButton", "button");
			}
			return Contains((ToolBarButton)button);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <returns>The index of <paramref name="button" /> if found in the list; otherwise, -1.</returns>
		/// <param name="button">The item to locate in the collection. </param>
		int IList.IndexOf(object button)
		{
			if (!(button is ToolBarButton))
			{
				throw new ArgumentException("Not of type ToolBarButton", "button");
			}
			return IndexOf((ToolBarButton)button);
		}

		/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the toolbar button. </param>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
		void IList.Insert(int index, object button)
		{
			if (!(button is ToolBarButton))
			{
				throw new ArgumentException("Not of type ToolBarButton", "button");
			}
			Insert(index, (ToolBarButton)button);
		}

		/// <summary>Removes the first occurrence of an item from the collection.</summary>
		/// <param name="button">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />. </param>
		void IList.Remove(object button)
		{
			if (!(button is ToolBarButton))
			{
				throw new ArgumentException("Not of type ToolBarButton", "button");
			}
			Remove((ToolBarButton)button);
		}

		internal void OnUIACollectionChanged(CollectionChangeEventArgs e)
		{
			((CollectionChangeEventHandler)owner.Events[UIACollectionChanged])?.Invoke(owner, e);
		}

		/// <summary>Adds a new toolbar button to the end of the toolbar button collection with the specified <see cref="P:System.Windows.Forms.ToolBarButton.Text" /> property value.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
		/// <param name="text">The text to display on the new <see cref="T:System.Windows.Forms.ToolBarButton" />. </param>
		public int Add(string text)
		{
			ToolBarButton button = new ToolBarButton(text);
			return Add(button);
		}

		/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
		/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons. </param>
		public int Add(ToolBarButton button)
		{
			button.SetParent(owner);
			int num = list.Add(button);
			if (redraw)
			{
				owner.Redraw(recalculate: true);
			}
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, num));
			return num;
		}

		/// <summary>Adds a collection of toolbar buttons to this toolbar button collection.</summary>
		/// <param name="buttons">The collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls to add to this <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> contained in an array. </param>
		public void AddRange(ToolBarButton[] buttons)
		{
			try
			{
				redraw = false;
				foreach (ToolBarButton button in buttons)
				{
					Add(button);
				}
			}
			finally
			{
				redraw = true;
				owner.Redraw(recalculate: true);
			}
		}

		/// <summary>Removes all buttons from the toolbar button collection.</summary>
		public void Clear()
		{
			list.Clear();
			owner.Redraw(recalculate: false);
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, -1));
		}

		/// <summary>Determines if the specified toolbar button is a member of the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolBarButton" /> is a member of the collection; otherwise, false.</returns>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection. </param>
		public bool Contains(ToolBarButton button)
		{
			return list.Contains(button);
		}

		/// <summary>Determines if a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key is contained in the collection.</summary>
		/// <returns>true to indicate a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key is found; otherwise, false. </returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to search for.</param>
		public virtual bool ContainsKey(string key)
		{
			return this[key] != null;
		}

		/// <summary>Returns an enumerator that can be used to iterate through the toolbar button collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>Retrieves the index of the specified toolbar button in the collection.</summary>
		/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection. </param>
		public int IndexOf(ToolBarButton button)
		{
			return list.IndexOf(button);
		}

		/// <summary>Retrieves the index of the first occurrence of a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key.</summary>
		/// <returns>The index of the first occurrence of a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key, if found; otherwise, -1.</returns>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to search for.</param>
		public virtual int IndexOfKey(string key)
		{
			return IndexOf(this[key]);
		}

		/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
		/// <param name="index">The indexed location within the collection to insert the toolbar button. </param>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert. </param>
		public void Insert(int index, ToolBarButton button)
		{
			list.Insert(index, button);
			owner.Redraw(recalculate: true);
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, index));
		}

		/// <summary>Removes a given button from the toolbar button collection.</summary>
		/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection. </param>
		public void Remove(ToolBarButton button)
		{
			list.Remove(button);
			owner.Redraw(recalculate: true);
		}

		/// <summary>Removes a given button from the toolbar button collection.</summary>
		/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0, or it is greater than the number of buttons in the collection. </exception>
		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
			owner.Redraw(recalculate: true);
			OnUIACollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, index));
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key from the collection.</summary>
		/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection.</param>
		public virtual void RemoveByKey(string key)
		{
			Remove(this[key]);
		}
	}

	internal const int text_padding = 3;

	private bool size_specified;

	private ToolBarItem current_item;

	internal ToolBarItem[] items;

	internal Size default_size;

	private static object ButtonClickEvent;

	private static object ButtonDropDownEvent;

	private ToolBarAppearance appearance;

	private bool autosize = true;

	private ToolBarButtonCollection buttons;

	private Size button_size;

	private bool divider = true;

	private bool drop_down_arrows = true;

	private ImageList image_list;

	private ImeMode ime_mode = ImeMode.Disable;

	private bool show_tooltips = true;

	private ToolBarTextAlign text_alignment;

	private bool wrappable = true;

	private ToolBarButton button_for_focus;

	private int requested_size = -1;

	private ToolTip tip_window;

	private Timer tipdown_timer;

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			if (appearance == ToolBarAppearance.Flat)
			{
				createParams.Style |= 2048;
			}
			return createParams;
		}
	}

	/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	protected override ImeMode DefaultImeMode => ImeMode.Disable;

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.ToolBarDefaultSize;

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
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

	/// <summary>Gets or set the value that determines the appearance of a toolbar control and its buttons.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. The default is ToolBarAppearance.Normal.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ToolBarAppearance.Normal)]
	public ToolBarAppearance Appearance
	{
		get
		{
			return appearance;
		}
		set
		{
			if (value != appearance)
			{
				appearance = value;
				Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the toolbar adjusts its size automatically, based on the size of the buttons and the dock style.</summary>
	/// <returns>true if the toolbar adjusts its size automatically, based on the size of the buttons and dock style; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	[Localizable(true)]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool AutoSize
	{
		get
		{
			return autosize;
		}
		set
		{
			if (value != autosize)
			{
				autosize = value;
				if (base.IsHandleCreated)
				{
					Redraw(recalculate: true);
				}
			}
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Color BackColor
	{
		get
		{
			return background_color;
		}
		set
		{
			if (!(value == background_color))
			{
				background_color = value;
				OnBackColorChanged(EventArgs.Empty);
				Redraw(recalculate: false);
			}
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
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

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets the border style of the toolbar control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-504)]
	[DefaultValue(BorderStyle.None)]
	public BorderStyle BorderStyle
	{
		get
		{
			return base.InternalBorderStyle;
		}
		set
		{
			base.InternalBorderStyle = value;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> that contains a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(true)]
	[MergableProperty(false)]
	public ToolBarButtonCollection Buttons => buttons;

	/// <summary>Gets or sets the size of the buttons on the toolbar control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> object that represents the size of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls on the toolbar. The default size has a width of 24 pixels and a height of 22 pixels, or large enough to accommodate the <see cref="T:System.Drawing.Image" /> and text, whichever is greater.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Width" /> or <see cref="P:System.Drawing.Size.Height" /> property of the <see cref="T:System.Drawing.Size" /> object is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.All)]
	public Size ButtonSize
	{
		get
		{
			if (!button_size.IsEmpty)
			{
				return button_size;
			}
			if (buttons.Count == 0)
			{
				return new Size(39, 36);
			}
			Size result = CalcButtonSize();
			if (result.IsEmpty)
			{
				return new Size(24, 22);
			}
			return result;
		}
		set
		{
			size_specified = value != Size.Empty;
			if (!(button_size == value))
			{
				button_size = value;
				Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the toolbar displays a divider.</summary>
	/// <returns>true if the toolbar displays a divider; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool Divider
	{
		get
		{
			return divider;
		}
		set
		{
			if (value != divider)
			{
				divider = value;
				Redraw(recalculate: false);
			}
		}
	}

	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(DockStyle.Top)]
	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			if (base.Dock == value)
			{
				if (value != 0)
				{
					base.Dock = value;
				}
				return;
			}
			if (Vertical)
			{
				SetStyle(ControlStyles.FixedWidth, AutoSize);
				SetStyle(ControlStyles.FixedHeight, value: false);
			}
			else
			{
				SetStyle(ControlStyles.FixedHeight, AutoSize);
				SetStyle(ControlStyles.FixedWidth, value: false);
			}
			LayoutToolBar();
			base.Dock = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether drop-down buttons on a toolbar display down arrows.</summary>
	/// <returns>true if drop-down toolbar buttons display down arrows; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[Localizable(true)]
	public bool DropDownArrows
	{
		get
		{
			return drop_down_arrows;
		}
		set
		{
			if (value != drop_down_arrows)
			{
				drop_down_arrows = value;
				Redraw(recalculate: true);
			}
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
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
			return foreground_color;
		}
		set
		{
			if (!(value == foreground_color))
			{
				foreground_color = value;
				OnForeColorChanged(EventArgs.Empty);
				Redraw(recalculate: false);
			}
		}
	}

	/// <summary>Gets or sets the collection of images available to the toolbar button controls.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains images available to the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	public ImageList ImageList
	{
		get
		{
			return image_list;
		}
		set
		{
			if (image_list != value)
			{
				image_list = value;
				Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Gets the size of the images in the image list assigned to the toolbar.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the images (in the <see cref="T:System.Windows.Forms.ImageList" />) assigned to the <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public Size ImageSize
	{
		get
		{
			if (ImageList == null)
			{
				return Size.Empty;
			}
			return ImageList.ImageSize;
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ImeMode ImeMode
	{
		get
		{
			return ime_mode;
		}
		set
		{
			if (value != ime_mode)
			{
				ime_mode = value;
				OnImeModeChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.RightToLeft" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override RightToLeft RightToLeft
	{
		get
		{
			return base.RightToLeft;
		}
		set
		{
			if (value != base.RightToLeft)
			{
				base.RightToLeft = value;
				OnRightToLeftChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the toolbar displays a ToolTip for each button.</summary>
	/// <returns>true if the toolbar display a ToolTip for each button; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[Localizable(true)]
	public bool ShowToolTips
	{
		get
		{
			return show_tooltips;
		}
		set
		{
			show_tooltips = value;
		}
	}

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

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.String" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Bindable(false)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			if (!(value == base.Text))
			{
				base.Text = value;
				Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Gets or sets the alignment of text in relation to each image displayed on the toolbar button controls.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. The default is ToolBarTextAlign.Underneath.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ToolBarTextAlign.Underneath)]
	public ToolBarTextAlign TextAlign
	{
		get
		{
			return text_alignment;
		}
		set
		{
			if (value != text_alignment)
			{
				text_alignment = value;
				Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the toolbar buttons wrap to the next line if the toolbar becomes too small to display all the buttons on the same line.</summary>
	/// <returns>true if the toolbar buttons wrap to another line if the toolbar becomes too small to display all the buttons on the same line; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(true)]
	public bool Wrappable
	{
		get
		{
			return wrappable;
		}
		set
		{
			if (value != wrappable)
			{
				wrappable = value;
				Redraw(recalculate: true);
			}
		}
	}

	internal int CurrentItem
	{
		get
		{
			return Array.IndexOf(items, current_item);
		}
		set
		{
			if (current_item != null)
			{
				current_item.Hilight = false;
			}
			current_item = ((value != -1) ? items[value] : null);
			if (current_item != null)
			{
				current_item.Hilight = true;
			}
		}
	}

	private Timer TipDownTimer
	{
		get
		{
			if (tipdown_timer == null)
			{
				tipdown_timer = new Timer();
				tipdown_timer.Enabled = false;
				tipdown_timer.Interval = 5000;
				tipdown_timer.Tick += PopDownTip;
			}
			return tipdown_timer;
		}
	}

	internal bool SizeSpecified => size_specified;

	internal bool Vertical => Dock == DockStyle.Left || Dock == DockStyle.Right;

	private Size AdjustedButtonSize
	{
		get
		{
			Size result = ((!default_size.IsEmpty && Appearance != 0) ? default_size : ButtonSize);
			if (size_specified)
			{
				if (Appearance == ToolBarAppearance.Flat)
				{
					result = CalcButtonSize();
				}
				else
				{
					int toolBarImageGripWidth = ThemeEngine.Current.ToolBarImageGripWidth;
					if (result.Width < ImageSize.Width + 2 * toolBarImageGripWidth)
					{
						result.Width = ImageSize.Width + 2 * toolBarImageGripWidth;
					}
					if (result.Height < ImageSize.Height + 2 * toolBarImageGripWidth)
					{
						result.Height = ImageSize.Height + 2 * toolBarImageGripWidth;
					}
				}
			}
			return result;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolBar.AutoSize" /> property has changed.</summary>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event EventHandler AutoSizeChanged
	{
		add
		{
			base.AutoSizeChanged += value;
		}
		remove
		{
			base.AutoSizeChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImage" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImageLayout" /> property changes.</summary>
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

	/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolBarButton" /> on the <see cref="T:System.Windows.Forms.ToolBar" /> is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event ToolBarButtonClickEventHandler ButtonClick
	{
		add
		{
			base.Events.AddHandler(ButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when a drop-down style <see cref="T:System.Windows.Forms.ToolBarButton" /> or its down arrow is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event ToolBarButtonClickEventHandler ButtonDropDown
	{
		add
		{
			base.Events.AddHandler(ButtonDropDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ButtonDropDownEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.ForeColor" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.ImeMode" /> property changes.</summary>
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

	/// <summary>This member is not meaningful for this control.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.RightToLeft" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler RightToLeftChanged
	{
		add
		{
			base.RightToLeftChanged += value;
		}
		remove
		{
			base.RightToLeftChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
	public ToolBar()
	{
		background_color = ThemeEngine.Current.DefaultControlBackColor;
		foreground_color = ThemeEngine.Current.DefaultControlForeColor;
		buttons = new ToolBarButtonCollection(this);
		Dock = DockStyle.Top;
		base.GotFocus += FocusChanged;
		base.LostFocus += FocusChanged;
		base.MouseDown += ToolBar_MouseDown;
		base.MouseHover += ToolBar_MouseHover;
		base.MouseLeave += ToolBar_MouseLeave;
		base.MouseMove += ToolBar_MouseMove;
		base.MouseUp += ToolBar_MouseUp;
		BackgroundImageChanged += ToolBar_BackgroundImageChanged;
		TabStop = false;
		SetStyle(ControlStyles.UserPaint, value: false);
		SetStyle(ControlStyles.FixedHeight, value: true);
		SetStyle(ControlStyles.FixedWidth, value: false);
	}

	static ToolBar()
	{
		ButtonClick = new object();
		ButtonDropDown = new object();
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
	/// <returns>A String that represents the current <see cref="T:System.Windows.Forms.ToolBar" />. </returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		int count = Buttons.Count;
		if (count == 0)
		{
			return $"System.Windows.Forms.ToolBar, Buttons.Count: 0";
		}
		return $"System.Windows.Forms.ToolBar, Buttons.Count: {count}, Buttons[0]: {Buttons[0].ToString()}";
	}

	protected override void CreateHandle()
	{
		base.CreateHandle();
		default_size = CalcButtonSize();
		if (appearance != ToolBarAppearance.Flat)
		{
			Redraw(recalculate: true);
		}
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBar" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			ImageList = null;
		}
		base.Dispose(disposing);
	}

	internal void UIAPerformClick(ToolBarButton button)
	{
		ToolBarItem toolBarItem = current_item;
		current_item = null;
		ToolBarItem[] array = items;
		foreach (ToolBarItem toolBarItem2 in array)
		{
			if (toolBarItem2.Button == button)
			{
				current_item = toolBarItem2;
				break;
			}
		}
		try
		{
			if (current_item == null)
			{
				throw new ArgumentException("button", "The button specified is not part of this toolbar");
			}
			PerformButtonClick(new ToolBarButtonClickEventArgs(button));
		}
		finally
		{
			current_item = toolBarItem;
		}
	}

	private void PerformButtonClick(ToolBarButtonClickEventArgs e)
	{
		if (e.Button.Style == ToolBarButtonStyle.ToggleButton)
		{
			if (!e.Button.Pushed)
			{
				e.Button.Pushed = true;
			}
			else
			{
				e.Button.Pushed = false;
			}
		}
		current_item.Pressed = false;
		current_item.Invalidate();
		button_for_focus = current_item.Button;
		button_for_focus.UIAHasFocus = true;
		OnButtonClick(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data. </param>
	protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e)
	{
		((ToolBarButtonClickEventHandler)base.Events[ButtonClick])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonDropDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data. </param>
	protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e)
	{
		((ToolBarButtonClickEventHandler)base.Events[ButtonDropDown])?.Invoke(this, e);
		if (e.Button.DropDownMenu != null)
		{
			ShowDropDownMenu(current_item);
		}
	}

	internal void ShowDropDownMenu(ToolBarItem item)
	{
		Point pos = new Point(item.Rectangle.X + 1, item.Rectangle.Bottom + 1);
		((ContextMenu)item.Button.DropDownMenu).Show(this, pos);
		item.DDPressed = false;
		item.Hilight = false;
		item.Invalidate();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		Redraw(recalculate: true);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		LayoutToolBar();
	}

	/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
	/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
	protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		specified &= ~BoundsSpecified.Height;
		base.ScaleControl(factor, specified);
	}

	/// <param name="dx">The horizontal scaling factor.</param>
	/// <param name="dy">The vertical scaling factor.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override void ScaleCore(float dx, float dy)
	{
		dy = 1f;
		base.ScaleCore(dx, dy);
	}

	/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
	/// <param name="x">The new Left property value of the control.</param>
	/// <param name="y">The new Top property value of the control.</param>
	/// <param name="width">The new Width property value of the control.</param>
	/// <param name="height">Not used.</param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		if (Vertical)
		{
			if (!AutoSize && requested_size != width && (specified & BoundsSpecified.Width) != 0)
			{
				requested_size = width;
			}
		}
		else if (!AutoSize && requested_size != height && (specified & BoundsSpecified.Height) != 0)
		{
			requested_size = height;
		}
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override bool InternalPreProcessMessage(ref Message msg)
	{
		if (msg.Msg == 256)
		{
			Keys key_data = (Keys)msg.WParam.ToInt32();
			if (HandleKeyDown(ref msg, key_data))
			{
				return true;
			}
		}
		return base.InternalPreProcessMessage(ref msg);
	}

	private void FocusChanged(object sender, EventArgs args)
	{
		if (!Focused && button_for_focus != null)
		{
			button_for_focus.UIAHasFocus = false;
		}
		button_for_focus = null;
		if (Appearance != ToolBarAppearance.Flat || Buttons.Count == 0)
		{
			return;
		}
		ToolBarItem toolBarItem = null;
		ToolBarItem[] array = items;
		foreach (ToolBarItem toolBarItem2 in array)
		{
			if (toolBarItem2.Hilight)
			{
				toolBarItem = toolBarItem2;
				break;
			}
		}
		if (Focused && toolBarItem == null)
		{
			ToolBarItem[] array2 = items;
			foreach (ToolBarItem toolBarItem3 in array2)
			{
				if (toolBarItem3.Button.Enabled)
				{
					toolBarItem3.Hilight = true;
					break;
				}
			}
		}
		else if (toolBarItem != null)
		{
			toolBarItem.Hilight = false;
		}
	}

	private bool HandleKeyDown(ref Message msg, Keys key_data)
	{
		if (Appearance != ToolBarAppearance.Flat || Buttons.Count == 0)
		{
			return false;
		}
		if (HandleKeyOnDropDown(ref msg, key_data))
		{
			return true;
		}
		switch (key_data)
		{
		case Keys.Left:
		case Keys.Up:
			HighlightButton(-1);
			return true;
		case Keys.Right:
		case Keys.Down:
			HighlightButton(1);
			return true;
		case Keys.Return:
		case Keys.Space:
			if (current_item != null)
			{
				OnButtonClick(new ToolBarButtonClickEventArgs(current_item.Button));
				return true;
			}
			break;
		}
		return false;
	}

	private bool HandleKeyOnDropDown(ref Message msg, Keys key_data)
	{
		if (current_item == null || current_item.Button.Style != ToolBarButtonStyle.DropDownButton || current_item.Button.DropDownMenu == null)
		{
			return false;
		}
		Menu dropDownMenu = current_item.Button.DropDownMenu;
		if (dropDownMenu.Tracker.active)
		{
			dropDownMenu.ProcessCmdKey(ref msg, key_data);
			return true;
		}
		if (key_data == Keys.Up || key_data == Keys.Down)
		{
			current_item.DDPressed = true;
			current_item.Invalidate();
			OnButtonDropDown(new ToolBarButtonClickEventArgs(current_item.Button));
			return true;
		}
		return false;
	}

	private void HighlightButton(int offset)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int num2 = -1;
		ToolBarItem toolBarItem = null;
		ToolBarItem[] array = items;
		foreach (ToolBarItem toolBarItem2 in array)
		{
			if (toolBarItem2.Hilight)
			{
				num2 = num;
				toolBarItem = toolBarItem2;
			}
			if (toolBarItem2.Button.Enabled)
			{
				arrayList.Add(toolBarItem2);
				num++;
			}
		}
		int num3 = (num2 + offset) % num;
		if (num3 < 0)
		{
			num3 = num - 1;
		}
		if (num3 != num2)
		{
			if (toolBarItem != null)
			{
				toolBarItem.Hilight = false;
			}
			current_item = arrayList[num3] as ToolBarItem;
			current_item.Hilight = true;
		}
	}

	private void ToolBar_BackgroundImageChanged(object sender, EventArgs args)
	{
		Redraw(recalculate: false, force: true);
	}

	private void ToolBar_MouseDown(object sender, MouseEventArgs me)
	{
		if (!base.Enabled || (me.Button & MouseButtons.Left) == 0)
		{
			return;
		}
		Point pt = new Point(me.X, me.Y);
		if (ItemAtPoint(pt) == null)
		{
			return;
		}
		if (tip_window != null && tip_window.Visible && (me.Button & MouseButtons.Left) == MouseButtons.Left)
		{
			TipDownTimer.Stop();
			tip_window.Hide(this);
		}
		ToolBarItem[] array = items;
		foreach (ToolBarItem toolBarItem in array)
		{
			if (!toolBarItem.Button.Enabled || !toolBarItem.Rectangle.Contains(pt))
			{
				continue;
			}
			if (toolBarItem.Button.Style == ToolBarButtonStyle.DropDownButton)
			{
				Rectangle rectangle = toolBarItem.Rectangle;
				if (DropDownArrows)
				{
					rectangle.Width = ThemeEngine.Current.ToolBarDropDownWidth;
					rectangle.X = toolBarItem.Rectangle.Right - rectangle.Width;
				}
				if (rectangle.Contains(pt))
				{
					if (toolBarItem.Button.DropDownMenu != null)
					{
						toolBarItem.DDPressed = true;
						Invalidate(rectangle);
					}
					break;
				}
			}
			toolBarItem.Pressed = true;
			toolBarItem.Inside = true;
			toolBarItem.Invalidate();
			break;
		}
	}

	private void ToolBar_MouseUp(object sender, MouseEventArgs me)
	{
		if (!base.Enabled || (me.Button & MouseButtons.Left) == 0)
		{
			return;
		}
		Point pt = new Point(me.X, me.Y);
		ArrayList arrayList = new ArrayList(items);
		foreach (ToolBarItem item in arrayList)
		{
			if (item.Button.Enabled && item.Rectangle.Contains(pt))
			{
				if (item.Button.Style == ToolBarButtonStyle.DropDownButton)
				{
					Rectangle rectangle = item.Rectangle;
					rectangle.Width = ThemeEngine.Current.ToolBarDropDownWidth;
					rectangle.X = item.Rectangle.Right - rectangle.Width;
					if (rectangle.Contains(pt))
					{
						current_item = item;
						if (item.DDPressed)
						{
							OnButtonDropDown(new ToolBarButtonClickEventArgs(item.Button));
						}
						continue;
					}
				}
				current_item = item;
				if (item.Pressed && (me.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					PerformButtonClick(new ToolBarButtonClickEventArgs(item.Button));
				}
			}
			else if (item.Pressed)
			{
				item.Pressed = false;
				item.Invalidate();
			}
		}
	}

	private ToolBarItem ItemAtPoint(Point pt)
	{
		ToolBarItem[] array = items;
		foreach (ToolBarItem toolBarItem in array)
		{
			if (toolBarItem.Rectangle.Contains(pt))
			{
				return toolBarItem;
			}
		}
		return null;
	}

	private void PopDownTip(object o, EventArgs args)
	{
		tip_window.Hide(this);
	}

	private void ToolBar_MouseHover(object sender, EventArgs e)
	{
		if (!base.Capture)
		{
			if (tip_window == null)
			{
				tip_window = new ToolTip();
			}
			ToolBarItem toolBarItem = (current_item = ItemAtPoint(PointToClient(Control.MousePosition)));
			if (toolBarItem != null && toolBarItem.Button.ToolTipText.Length != 0)
			{
				tip_window.Present(this, toolBarItem.Button.ToolTipText);
				TipDownTimer.Start();
			}
		}
	}

	private void ToolBar_MouseLeave(object sender, EventArgs e)
	{
		if (tipdown_timer != null)
		{
			tipdown_timer.Dispose();
		}
		tipdown_timer = null;
		if (tip_window != null)
		{
			tip_window.Dispose();
		}
		tip_window = null;
		if (base.Enabled && current_item != null)
		{
			current_item.Hilight = false;
			current_item = null;
		}
	}

	private void ToolBar_MouseMove(object sender, MouseEventArgs me)
	{
		if (!base.Enabled)
		{
			return;
		}
		if (tip_window != null && tip_window.Visible)
		{
			TipDownTimer.Stop();
			TipDownTimer.Start();
		}
		Point pt = new Point(me.X, me.Y);
		if (base.Capture)
		{
			ToolBarItem[] array = items;
			foreach (ToolBarItem toolBarItem in array)
			{
				if (toolBarItem.Pressed && toolBarItem.Inside != toolBarItem.Rectangle.Contains(pt))
				{
					toolBarItem.Inside = toolBarItem.Rectangle.Contains(pt);
					toolBarItem.Hilight = false;
					break;
				}
			}
			return;
		}
		if (current_item != null && current_item.Rectangle.Contains(pt))
		{
			if (ThemeEngine.Current.ToolBarHasHotElementStyles(this) && !current_item.Hilight && (ThemeEngine.Current.ToolBarHasHotCheckedElementStyles || !current_item.Button.Pushed) && current_item.Button.Enabled)
			{
				current_item.Hilight = true;
			}
			return;
		}
		if (tip_window != null)
		{
			if (tip_window.Visible)
			{
				tip_window.Hide(this);
				TipDownTimer.Stop();
			}
			current_item = ItemAtPoint(pt);
			if (current_item != null && current_item.Button.ToolTipText.Length > 0)
			{
				tip_window.Present(this, current_item.Button.ToolTipText);
				TipDownTimer.Start();
			}
		}
		if (!ThemeEngine.Current.ToolBarHasHotElementStyles(this))
		{
			return;
		}
		ToolBarItem[] array2 = items;
		foreach (ToolBarItem toolBarItem2 in array2)
		{
			if (toolBarItem2.Rectangle.Contains(pt) && toolBarItem2.Button.Enabled)
			{
				current_item = toolBarItem2;
				if (!current_item.Hilight && (ThemeEngine.Current.ToolBarHasHotCheckedElementStyles || !current_item.Button.Pushed))
				{
					current_item.Hilight = true;
				}
			}
			else if (toolBarItem2.Hilight)
			{
				toolBarItem2.Hilight = false;
			}
		}
	}

	internal override void OnPaintInternal(PaintEventArgs pevent)
	{
		if (!GetStyle(ControlStyles.UserPaint))
		{
			ThemeEngine.Current.DrawToolBar(pevent.Graphics, pevent.ClipRectangle, this);
			pevent.Handled = true;
		}
	}

	internal void Redraw(bool recalculate)
	{
		Redraw(recalculate, force: true);
	}

	internal void Redraw(bool recalculate, bool force)
	{
		bool flag = true;
		if (recalculate)
		{
			flag = LayoutToolBar();
		}
		if (force || flag)
		{
			Invalidate();
		}
	}

	private Size CalcButtonSize()
	{
		if (Buttons.Count == 0)
		{
			return Size.Empty;
		}
		string text = Buttons[0].Text;
		for (int i = 1; i < Buttons.Count; i++)
		{
			if (Buttons[i].Text.Length > text.Length)
			{
				text = Buttons[i].Text;
			}
		}
		Size result = Size.Empty;
		if (text != null && text.Length > 0)
		{
			SizeF sizeF = TextRenderer.MeasureString(text, Font);
			if (sizeF != SizeF.Empty)
			{
				result = new Size((int)Math.Ceiling(sizeF.Width) + 6, (int)Math.Ceiling(sizeF.Height));
			}
		}
		Size size = ((ImageList != null) ? ImageSize : new Size(16, 16));
		Theme current = ThemeEngine.Current;
		int num = size.Width + 2 * current.ToolBarImageGripWidth;
		int num2 = size.Height + 2 * current.ToolBarImageGripWidth;
		if (text_alignment == ToolBarTextAlign.Right)
		{
			result.Width = num + result.Width;
			result.Height = ((result.Height <= num2) ? num2 : result.Height);
		}
		else
		{
			result.Height = num2 + result.Height;
			result.Width = ((result.Width <= num) ? num : result.Width);
		}
		result.Width += current.ToolBarImageGripWidth;
		result.Height += current.ToolBarImageGripWidth;
		return result;
	}

	private bool LayoutToolBar()
	{
		bool result = false;
		Theme current = ThemeEngine.Current;
		int num = current.ToolBarGripWidth;
		int num2 = current.ToolBarGripWidth;
		Size adjustedButtonSize = AdjustedButtonSize;
		int num3 = ((!Vertical) ? adjustedButtonSize.Height : adjustedButtonSize.Width) + current.ToolBarGripWidth;
		int num4 = -1;
		items = new ToolBarItem[buttons.Count];
		for (int i = 0; i < buttons.Count; i++)
		{
			ToolBarButton toolBarButton = buttons[i];
			ToolBarItem toolBarItem = new ToolBarItem(toolBarButton);
			items[i] = toolBarItem;
			if (!toolBarButton.Visible)
			{
				continue;
			}
			result = ((!size_specified || toolBarButton.Style == ToolBarButtonStyle.Separator) ? toolBarItem.Layout(Vertical, num3) : toolBarItem.Layout(adjustedButtonSize));
			bool flag = toolBarButton.Style == ToolBarButtonStyle.Separator;
			if (Vertical)
			{
				if (num2 + toolBarItem.Rectangle.Height < base.Height || flag || !Wrappable)
				{
					if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
					{
						result = true;
					}
					toolBarItem.Location = new Point(num, num2);
					num2 += toolBarItem.Rectangle.Height;
					if (flag)
					{
						num4 = i;
					}
				}
				else if (num4 > 0)
				{
					i = num4;
					num4 = -1;
					num2 = current.ToolBarGripWidth;
					num += num3;
				}
				else
				{
					num2 = current.ToolBarGripWidth;
					num += num3;
					if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
					{
						result = true;
					}
					toolBarItem.Location = new Point(num, num2);
					num2 += toolBarItem.Rectangle.Height;
				}
			}
			else if (num + toolBarItem.Rectangle.Width < base.Width || flag || !Wrappable)
			{
				if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
				{
					result = true;
				}
				toolBarItem.Location = new Point(num, num2);
				num += toolBarItem.Rectangle.Width;
				if (flag)
				{
					num4 = i;
				}
			}
			else if (num4 > 0)
			{
				i = num4;
				num4 = -1;
				num = current.ToolBarGripWidth;
				num2 += num3;
			}
			else
			{
				num = current.ToolBarGripWidth;
				num2 += num3;
				if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
				{
					result = true;
				}
				toolBarItem.Location = new Point(num, num2);
				num += toolBarItem.Rectangle.Width;
			}
		}
		if (base.Parent == null)
		{
			return result;
		}
		if (Wrappable)
		{
			num3 += ((!Vertical) ? num2 : num);
		}
		if (base.IsHandleCreated)
		{
			if (Vertical)
			{
				base.Width = num3;
			}
			else
			{
				base.Height = num3;
			}
		}
		return result;
	}
}
