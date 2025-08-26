using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Creates a container within which other controls can share horizontal or vertical space.</summary>
[Designer("System.Windows.Forms.Design.ToolStripPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ComVisible(true)]
[ToolboxBitmap("")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ToolStripPanel : ContainerControl, IDisposable, IComponent, IBindableComponent, IDropTarget
{
	/// <summary>Represents all the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects in a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	[ListBindable(false)]
	[ComVisible(false)]
	public class ToolStripPanelRowCollection : ArrangedElementCollection, ICollection, IEnumerable, IList
	{
		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			[System.MonoTODO("Stub, does nothing")]
			set
			{
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsFixedSize => base.IsFixedSize;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>false in all cases.</returns>
		bool IList.IsReadOnly => IsReadOnly;

		/// <summary>Gets a particular <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> within the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> as specified by the <paramref name="index" /> parameter.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> within the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		public new virtual ToolStripPanelRow this[int index] => (ToolStripPanelRow)base[index];

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> class in the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that holds this <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		public ToolStripPanelRowCollection(ToolStripPanel owner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> class with the specified number of rows in the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that holds this <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		/// <param name="value">The number of rows in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		public ToolStripPanelRowCollection(ToolStripPanel owner, ToolStripPanelRow[] value)
			: this(owner)
		{
			if (value != null)
			{
				foreach (ToolStripPanelRow value2 in value)
				{
					Add(value2);
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <returns>The zero-based index of the item to add.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		int IList.Add(object value)
		{
			return Add(value as ToolStripPanelRow);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		void IList.Clear()
		{
			Clear();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> found in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />; otherwise, false.</returns>
		/// <param name="value">The item to locate in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		bool IList.Contains(object value)
		{
			return Contains(value as ToolStripPanelRow);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		int IList.IndexOf(object value)
		{
			return IndexOf(value as ToolStripPanelRow);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to insert into the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		void IList.Insert(int index, object value)
		{
			Insert(index, value as ToolStripPanelRow);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		void IList.Remove(object value)
		{
			Remove(value as ToolStripPanelRow);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
		void IList.RemoveAt(int index)
		{
			InternalRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <returns>The position of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		public int Add(ToolStripPanelRow value)
		{
			return Add((object)value);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		public void AddRange(ToolStripPanelRowCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (ToolStripPanelRow item in value)
			{
				Add(item);
			}
		}

		/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="value">An array of <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		public void AddRange(ToolStripPanelRow[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (ToolStripPanelRow value2 in value)
			{
				Add(value2);
			}
		}

		/// <summary>Removes all <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		public new virtual void Clear()
		{
			base.Clear();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> is in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> is in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
		public bool Contains(ToolStripPanelRow value)
		{
			return Contains((object)value);
		}

		/// <summary>Copies the entire <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> into an existing array at a specified location within the array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> representing the array to copy the contents of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to.</param>
		/// <param name="index">The location within the destination array to copy the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to.</param>
		public void CopyTo(ToolStripPanelRow[] array, int index)
		{
			CopyTo((Array)array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to return the index of.</param>
		public int IndexOf(ToolStripPanelRow value)
		{
			return IndexOf((object)value);
		}

		/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified location in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <param name="index">The zero-based index at which to insert the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to insert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		public void Insert(int index, ToolStripPanelRow value)
		{
			Insert(index, (object)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
		public void Remove(ToolStripPanelRow value)
		{
			Remove((object)value);
		}

		/// <summary>Removes the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified index from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
		public void RemoveAt(int index)
		{
			InternalRemoveAt(index);
		}
	}

	private class ToolStripPanelControlCollection : ControlCollection
	{
		public ToolStripPanelControlCollection(Control owner)
			: base(owner)
		{
		}
	}

	private class TabIndexComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			if (!(x is Control) || !(y is Control))
			{
				throw new ArgumentException();
			}
			return (x as Control).TabIndex - (y as Control).TabIndex;
		}
	}

	private bool done_first_layout;

	private LayoutEngine layout_engine;

	private bool locked;

	private Orientation orientation;

	private ToolStripRenderer renderer;

	private ToolStripRenderMode render_mode;

	private Padding row_margin;

	private ToolStripPanelRowCollection rows;

	private static object RendererChangedEvent;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override bool AllowDrop
	{
		get
		{
			return base.AllowDrop;
		}
		set
		{
			base.AllowDrop = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override bool AutoScroll
	{
		get
		{
			return base.AutoScroll;
		}
		set
		{
			base.AutoScroll = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Size AutoScrollMargin
	{
		get
		{
			return base.AutoScrollMargin;
		}
		set
		{
			base.AutoScrollMargin = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Size AutoScrollMinSize
	{
		get
		{
			return base.AutoScrollMinSize;
		}
		set
		{
			base.AutoScrollMinSize = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripPanel" /> automatically adjusts its size when the form is resized.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripPanel" /> automatically resizes; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	public override bool AutoSize
	{
		get
		{
			return base.AutoSize;
		}
		set
		{
			base.AutoSize = value;
		}
	}

	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			base.Dock = value;
			switch (value)
			{
			case DockStyle.None:
			case DockStyle.Top:
			case DockStyle.Bottom:
				orientation = Orientation.Horizontal;
				break;
			case DockStyle.Left:
			case DockStyle.Right:
				orientation = Orientation.Vertical;
				break;
			}
		}
	}

	public override LayoutEngine LayoutEngine
	{
		get
		{
			if (layout_engine == null)
			{
				layout_engine = new FlowLayout();
			}
			return layout_engine;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripPanel" /> can be moved or resized.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripPanel" /> can be moved or resized; otherwise, false. The default is false.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DefaultValue(false)]
	[Browsable(false)]
	public bool Locked
	{
		get
		{
			return locked;
		}
		set
		{
			locked = value;
		}
	}

	/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
	public Orientation Orientation
	{
		get
		{
			return orientation;
		}
		set
		{
			orientation = value;
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the appearance of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> that handles painting.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ToolStripRenderer Renderer
	{
		get
		{
			if (render_mode == ToolStripRenderMode.ManagerRenderMode)
			{
				return ToolStripManager.Renderer;
			}
			return renderer;
		}
		set
		{
			if (renderer != value)
			{
				renderer = value;
				render_mode = ToolStripRenderMode.Custom;
				OnRendererChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the painting styles to be applied to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</returns>
	public ToolStripRenderMode RenderMode
	{
		get
		{
			return render_mode;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripRenderMode), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripRenderMode");
			}
			if (value == ToolStripRenderMode.Custom && renderer == null)
			{
				throw new NotSupportedException("Must set Renderer property before setting RenderMode to Custom");
			}
			if (value == ToolStripRenderMode.Professional || value == ToolStripRenderMode.System)
			{
				Renderer = new ToolStripProfessionalRenderer();
			}
			render_mode = value;
		}
	}

	/// <summary>Gets or sets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s and the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing, in pixels.</returns>
	public Padding RowMargin
	{
		get
		{
			return row_margin;
		}
		set
		{
			row_margin = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s in this <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> representing the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s in this <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ToolStripPanelRow[] Rows
	{
		get
		{
			ToolStripPanelRow[] array = new ToolStripPanelRow[rows.Count];
			rows.CopyTo(array, 0);
			return array;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the tab index.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new int TabIndex
	{
		get
		{
			return base.TabIndex;
		}
		set
		{
			base.TabIndex = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the display text.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	protected override Padding DefaultMargin => new Padding(0);

	protected override Padding DefaultPadding => new Padding(0);

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanel.AutoSize" /> property changes. </summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanel.Renderer" /> property changes.</summary>
	public event EventHandler RendererChanged
	{
		add
		{
			base.Events.AddHandler(RendererChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RendererChangedEvent, value);
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TabIndexChanged
	{
		add
		{
			base.TabIndexChanged += value;
		}
		remove
		{
			base.TabIndexChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler TabStopChanged
	{
		add
		{
			base.TabStopChanged += value;
		}
		remove
		{
			base.TabStopChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel" /> class. </summary>
	public ToolStripPanel()
	{
		base.AutoSize = true;
		locked = false;
		renderer = null;
		render_mode = ToolStripRenderMode.ManagerRenderMode;
		row_margin = new Padding(3, 0, 0, 0);
		rows = new ToolStripPanelRowCollection(this);
	}

	static ToolStripPanel()
	{
		RendererChanged = new object();
	}

	/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	public void BeginInit()
	{
	}

	/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	public void EndInit()
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
	[System.MonoTODO("Not implemented")]
	public void Join(ToolStrip toolStripToDrag)
	{
		if (!Contains(toolStripToDrag))
		{
			base.Controls.Add(toolStripToDrag);
		}
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> in the specified row.</summary>
	/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
	/// <param name="row">An <see cref="T:System.Int32" /> representing the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to which the <see cref="T:System.Windows.Forms.ToolStrip" /> is added.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="row" /> parameter is less than zero (0).</exception>
	[System.MonoTODO("Not implemented")]
	public void Join(ToolStrip toolStripToDrag, int row)
	{
		Join(toolStripToDrag);
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> at the specified location.</summary>
	/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
	/// <param name="location">A <see cref="T:System.Drawing.Point" /> value representing the x- and y-client coordinates, in pixels, of the new location for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
	[System.MonoTODO("Not implemented")]
	public void Join(ToolStrip toolStripToDrag, Point location)
	{
		Join(toolStripToDrag);
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> at the specified coordinates.</summary>
	/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
	/// <param name="x">The horizontal client coordinate, in pixels.</param>
	/// <param name="y">The vertical client coordinate, in pixels.</param>
	[System.MonoTODO("Not implemented")]
	public void Join(ToolStrip toolStripToDrag, int x, int y)
	{
		Join(toolStripToDrag);
	}

	/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> given a point within the <see cref="T:System.Windows.Forms.ToolStripPanel" /> client area.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> that contains the <paramref name="raftingContainerPoint" />, or null if no such <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> exists.</returns>
	/// <param name="clientLocation">A <see cref="T:System.Drawing.Point" /> used as a reference to find the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
	public ToolStripPanelRow PointToRow(Point clientLocation)
	{
		foreach (ToolStripPanelRow row in rows)
		{
			if (row.Bounds.Contains(clientLocation))
			{
				return row;
			}
		}
		return null;
	}

	/// <summary>Retrieves a collection of <see cref="T:System.Windows.Forms.ToolStripPanel" /> controls.</summary>
	/// <returns>A collection of <see cref="T:System.Windows.Forms.ToolStripPanel" /> controls.</returns>
	protected override ControlCollection CreateControlsInstance()
	{
		return new ToolStripPanelControlCollection(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripPanel" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ControlAdded" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
	protected override void OnControlAdded(ControlEventArgs e)
	{
		if (Dock == DockStyle.Left || Dock == DockStyle.Right)
		{
			(e.Control as ToolStrip).LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
		}
		else
		{
			(e.Control as ToolStrip).LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
		}
		if (done_first_layout && e.Control is ToolStrip)
		{
			AddControlToRows(e.Control);
		}
		base.OnControlAdded(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ControlRemoved" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
	protected override void OnControlRemoved(ControlEventArgs e)
	{
		base.OnControlRemoved(e);
		foreach (ToolStripPanelRow row in rows)
		{
			if (row.controls.Contains(e.Control))
			{
				row.OnControlRemoved(e.Control, 0);
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event. </summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnDockChanged(EventArgs e)
	{
		base.OnDockChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		if (!base.Created)
		{
			return;
		}
		if (!done_first_layout)
		{
			ArrayList arrayList = new ArrayList(base.Controls);
			arrayList.Sort(new TabIndexComparer());
			foreach (ToolStrip item in arrayList)
			{
				AddControlToRows(item);
			}
			done_first_layout = true;
		}
		Point location = DisplayRectangle.Location;
		if (Dock == DockStyle.Left || Dock == DockStyle.Right)
		{
			foreach (ToolStripPanelRow row in rows)
			{
				row.SetBounds(new Rectangle(location, new Size(row.Bounds.Width, base.Height)));
				location.X += row.Bounds.Width;
			}
			if (rows.Count > 0)
			{
				int right = rows[rows.Count - 1].Bounds.Right;
				if (right != base.Width)
				{
					SetBounds(bounds.X, bounds.Y, right, bounds.Bottom);
				}
			}
		}
		else
		{
			foreach (ToolStripPanelRow row2 in rows)
			{
				row2.SetBounds(new Rectangle(location, new Size(base.Width, row2.Bounds.Height)));
				location.Y += row2.Bounds.Height;
			}
			if (rows.Count > 0)
			{
				int bottom = rows[rows.Count - 1].Bounds.Bottom;
				if (bottom != base.Height)
				{
					SetBounds(bounds.X, bounds.Y, bounds.Width, bottom);
				}
			}
		}
		Invalidate();
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
		Renderer.DrawToolStripPanelBackground(new ToolStripPanelRenderEventArgs(e.Graphics, this));
	}

	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripPanel.RendererChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnRendererChanged(EventArgs e)
	{
		((EventHandler)base.Events[RendererChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	private void AddControlToRows(Control control)
	{
		if (rows.Count > 0 && rows[rows.Count - 1].CanMove((ToolStrip)control))
		{
			rows[rows.Count - 1].OnControlAdded(control, 0);
			return;
		}
		ToolStripPanelRow toolStripPanelRow = new ToolStripPanelRow(this);
		if (Dock == DockStyle.Left || Dock == DockStyle.Right)
		{
			toolStripPanelRow.SetBounds(new Rectangle(0, 0, 25, base.Height));
		}
		else
		{
			toolStripPanelRow.SetBounds(new Rectangle(0, 0, base.Width, 25));
		}
		rows.Add(toolStripPanelRow);
		toolStripPanelRow.OnControlAdded(control, 0);
	}
}
