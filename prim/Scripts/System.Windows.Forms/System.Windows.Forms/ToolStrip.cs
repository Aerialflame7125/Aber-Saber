using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Provides a container for Windows toolbar objects. </summary>
/// <filterpriority>1</filterpriority>
[DesignerSerializer("System.Windows.Forms.Design.ToolStripCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultEvent("ItemClicked")]
[DefaultProperty("Items")]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.ToolStripDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class ToolStrip : ScrollableControl, IDisposable, IComponent, IToolStripData
{
	/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStrip" /> for users with impairments.</summary>
	[ComVisible(true)]
	public class ToolStripAccessibleObject : ControlAccessibleObject
	{
		public override AccessibleRole Role => AccessibleRole.ToolBar;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip.ToolStripAccessibleObject" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStrip" /> that owns this <see cref="T:System.Windows.Forms.ToolStrip.ToolStripAccessibleObject" />. </param>
		public ToolStripAccessibleObject(ToolStrip owner)
			: base(owner)
		{
		}

		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
		/// <param name="index">The zero-based index of the accessible child. </param>
		public override AccessibleObject GetChild(int index)
		{
			return base.GetChild(index);
		}

		/// <returns>The number of children belonging to an accessible object.</returns>
		public override int GetChildCount()
		{
			return (owner as ToolStrip).Items.Count;
		}

		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns null if no object is at the tested location.</returns>
		/// <param name="x">The horizontal screen coordinate. </param>
		/// <param name="y">The vertical screen coordinate. </param>
		public override AccessibleObject HitTest(int x, int y)
		{
			return base.HitTest(x, y);
		}
	}

	private bool allow_item_reorder;

	private bool allow_merge;

	private Color back_color;

	private bool can_overflow;

	private ToolStrip currently_merged_with;

	private ToolStripDropDownDirection default_drop_down_direction;

	internal ToolStripItemCollection displayed_items;

	private Color fore_color;

	private Padding grip_margin;

	private ToolStripGripStyle grip_style;

	private List<ToolStripItem> hidden_merged_items;

	private ImageList image_list;

	private Size image_scaling_size;

	private bool is_currently_merged;

	private ToolStripItemCollection items;

	private bool keyboard_active;

	private LayoutEngine layout_engine;

	private LayoutSettings layout_settings;

	private ToolStripLayoutStyle layout_style;

	private Orientation orientation;

	private ToolStripOverflowButton overflow_button;

	private List<ToolStripItem> pre_merge_items;

	private ToolStripRenderer renderer;

	private ToolStripRenderMode render_mode;

	private ToolStripTextDirection text_direction;

	private Timer tooltip_timer;

	private ToolTip tooltip_window;

	private bool show_item_tool_tips;

	private bool stretch;

	private ToolStripItem mouse_currently_over;

	internal bool menu_selected;

	private ToolStripItem tooltip_currently_showing;

	private static object BeginDragEvent;

	private static object EndDragEvent;

	private static object ItemAddedEvent;

	private static object ItemClickedEvent;

	private static object ItemRemovedEvent;

	private static object LayoutCompletedEvent;

	private static object LayoutStyleChangedEvent;

	private static object PaintGripEvent;

	private static object RendererChangedEvent;

	/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.</summary>
	/// <returns>true to control drag-and-drop and item reordering through events that you implement; otherwise, false.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <see cref="P:System.Windows.Forms.ToolStrip.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Stub, does nothing")]
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

	/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled privately by the <see cref="T:System.Windows.Forms.ToolStrip" /> class.</summary>
	/// <returns>true to cause the <see cref="T:System.Windows.Forms.ToolStrip" /> class to handle drag-and-drop and item reordering automatically; otherwise, false. The default value is false.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <see cref="P:System.Windows.Forms.ToolStrip.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[System.MonoTODO("Stub, does nothing")]
	public bool AllowItemReorder
	{
		get
		{
			return allow_item_reorder;
		}
		set
		{
			allow_item_reorder = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether multiple <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, and other types can be combined. </summary>
	/// <returns>true if combining of types is allowed; otherwise, false. The default is false.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(true)]
	public bool AllowMerge
	{
		get
		{
			return allow_merge;
		}
		set
		{
			allow_merge = value;
		}
	}

	/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStrip" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStrip" /> is resized with its parent.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
	public override AnchorStyles Anchor
	{
		get
		{
			return base.Anchor;
		}
		set
		{
			base.Anchor = value;
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>true to automatically scroll; otherwise, false.</returns>
	/// <exception cref="T:System.NotSupportedException">Automatic scrolling is not supported by <see cref="T:System.Windows.Forms.ToolStrip" /> controls.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Point AutoScrollPosition
	{
		get
		{
			return base.AutoScrollPosition;
		}
		set
		{
			base.AutoScrollPosition = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
	/// <returns>true if the control adjusts its width to closely fit its contents; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
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

	/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.ToolStrip" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public new Color BackColor
	{
		get
		{
			return back_color;
		}
		set
		{
			back_color = value;
		}
	}

	/// <summary>Gets or sets the binding context for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override BindingContext BindingContext
	{
		get
		{
			return base.BindingContext;
		}
		set
		{
			base.BindingContext = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether items in the <see cref="T:System.Windows.Forms.ToolStrip" /> can be sent to an overflow menu.</summary>
	/// <returns>true to send <see cref="T:System.Windows.Forms.ToolStrip" /> items to an overflow menu; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool CanOverflow
	{
		get
		{
			return can_overflow;
		}
		set
		{
			can_overflow = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStrip" /> causes validation to be performed on any controls that require validation when it receives focus.</summary>
	/// <returns>false in all cases.</returns>
	[Browsable(false)]
	[DefaultValue(false)]
	public new bool CausesValidation
	{
		get
		{
			return base.CausesValidation;
		}
		set
		{
			base.CausesValidation = value;
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" /> representing the collection of controls contained within the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ControlCollection Controls => base.Controls;

	/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Cursor Cursor
	{
		get
		{
			return base.Cursor;
		}
		set
		{
			base.Cursor = value;
		}
	}

	/// <summary>Gets or sets a value representing the default direction in which a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</exception>
	[Browsable(false)]
	public virtual ToolStripDropDownDirection DefaultDropDownDirection
	{
		get
		{
			return default_drop_down_direction;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripDropDownDirection), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripDropDownDirection");
			}
			default_drop_down_direction = value;
		}
	}

	/// <summary>Retrieves the current display rectangle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the <see cref="T:System.Windows.Forms.ToolStrip" /> area for item layout.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Rectangle DisplayRectangle
	{
		get
		{
			if (orientation == Orientation.Horizontal)
			{
				if (grip_style == ToolStripGripStyle.Hidden || layout_style == ToolStripLayoutStyle.Flow || layout_style == ToolStripLayoutStyle.Table)
				{
					return new Rectangle(base.Padding.Left, base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical);
				}
				return new Rectangle(GripRectangle.Right + GripMargin.Right, base.Padding.Top, base.Width - base.Padding.Horizontal - GripRectangle.Right - GripMargin.Right, base.Height - base.Padding.Vertical);
			}
			if (grip_style == ToolStripGripStyle.Hidden || layout_style == ToolStripLayoutStyle.Flow || layout_style == ToolStripLayoutStyle.Table)
			{
				return new Rectangle(base.Padding.Left, base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical);
			}
			return new Rectangle(base.Padding.Left, GripRectangle.Bottom + GripMargin.Bottom + base.Padding.Top, base.Width - base.Padding.Horizontal, base.Height - base.Padding.Vertical - GripRectangle.Bottom - GripMargin.Bottom);
		}
	}

	/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStrip" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStrip" /> is resized with its parent.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default value is <see cref="F:System.Windows.Forms.DockStyle.Top" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DockStyle.Top)]
	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			if (base.Dock != value)
			{
				base.Dock = value;
				switch (value)
				{
				case DockStyle.None:
				case DockStyle.Top:
				case DockStyle.Bottom:
					LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
					break;
				case DockStyle.Left:
				case DockStyle.Right:
					LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
					break;
				}
			}
		}
	}

	/// <summary>Gets or sets the font used to display text in the control.</summary>
	/// <returns>The current default font.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			if (base.Font == value)
			{
				return;
			}
			base.Font = value;
			foreach (ToolStripItem item in Items)
			{
				item.OnOwnerFontChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public new Color ForeColor
	{
		get
		{
			return fore_color;
		}
		set
		{
			if (fore_color != value)
			{
				fore_color = value;
				OnForeColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the orientation of the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values. Possible values are <see cref="F:System.Windows.Forms.ToolStripGripDisplayStyle.Horizontal" /> and <see cref="F:System.Windows.Forms.ToolStripGripDisplayStyle.Vertical" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ToolStripGripDisplayStyle GripDisplayStyle => (orientation != Orientation.Vertical) ? ToolStripGripDisplayStyle.Vertical : ToolStripGripDisplayStyle.Horizontal;

	/// <summary>Gets or sets the space around the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" />, which represents the spacing.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Padding GripMargin
	{
		get
		{
			return grip_margin;
		}
		set
		{
			if (grip_margin != value)
			{
				grip_margin = value;
				PerformLayout();
			}
		}
	}

	/// <summary>Gets the boundaries of the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle.</summary>
	/// <returns>An object of type <see cref="T:System.Drawing.Rectangle" />, representing the move handle boundaries. If the boundaries are not visible, the <see cref="P:System.Windows.Forms.ToolStrip.GripRectangle" /> property returns <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Rectangle GripRectangle
	{
		get
		{
			if (grip_style == ToolStripGripStyle.Hidden)
			{
				return Rectangle.Empty;
			}
			if (orientation == Orientation.Horizontal)
			{
				return new Rectangle(grip_margin.Left + base.Padding.Left, base.Padding.Top, 3, base.Height);
			}
			return new Rectangle(base.Padding.Left, grip_margin.Top + base.Padding.Top, base.Width, 3);
		}
	}

	/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle is visible or hidden.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default value is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Visible" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ToolStripGripStyle.Visible)]
	public ToolStripGripStyle GripStyle
	{
		get
		{
			return grip_style;
		}
		set
		{
			if (grip_style != value)
			{
				if (!Enum.IsDefined(typeof(ToolStripGripStyle), value))
				{
					throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripGripStyle");
				}
				grip_style = value;
				PerformLayout(this, "GripStyle");
			}
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStrip" /> has children; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new bool HasChildren => base.HasChildren;

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An instance of the <see cref="T:System.Windows.Forms.HScrollProperties" /> class, which provides basic properties for an <see cref="T:System.Windows.Forms.HScrollBar" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new HScrollProperties HorizontalScroll => base.HorizontalScroll;

	/// <summary>Gets or sets the image list that contains the image displayed on a <see cref="T:System.Windows.Forms.ToolStrip" /> item.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.ImageList" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	[Browsable(false)]
	public ImageList ImageList
	{
		get
		{
			return image_list;
		}
		set
		{
			image_list = value;
		}
	}

	/// <summary>Gets or sets the size, in pixels, of an image used on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the size of the image, in pixels. The default is 16 x 16 pixels.</returns>
	[DefaultValue("{Width=16, Height=16}")]
	public Size ImageScalingSize
	{
		get
		{
			return image_scaling_size;
		}
		set
		{
			image_scaling_size = value;
		}
	}

	/// <summary>Gets a value indicating whether the user is currently moving the <see cref="T:System.Windows.Forms.ToolStrip" /> from one <see cref="T:System.Windows.Forms.ToolStripContainer" /> to another. </summary>
	/// <returns>true if the user is currently moving the <see cref="T:System.Windows.Forms.ToolStrip" /> from one <see cref="T:System.Windows.Forms.ToolStripContainer" /> to another; otherwise, false.</returns>
	[System.MonoTODO("Always returns false, dragging not implemented yet.")]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool IsCurrentlyDragging => false;

	/// <summary>Gets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStrip" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool IsDropDown
	{
		get
		{
			if (this is ToolStripDropDown)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets all the items that belong to a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.ToolStripItemCollection" />, representing all the elements contained by a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[MergableProperty(false)]
	public virtual ToolStripItemCollection Items => items;

	/// <summary>Passes a reference to the cached <see cref="P:System.Windows.Forms.Control.LayoutEngine" /> returned by the layout engine interface.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> that represents the cached layout engine returned by the layout engine interface.</returns>
	/// <filterpriority>2</filterpriority>
	public override LayoutEngine LayoutEngine
	{
		get
		{
			if (layout_engine == null)
			{
				layout_engine = new ToolStripSplitStackLayout();
			}
			return layout_engine;
		}
	}

	/// <summary>Gets or sets layout scheme characteristics.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.LayoutSettings" /> representing the layout scheme characteristics.</returns>
	/// <filterpriority>2</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[DefaultValue(null)]
	public LayoutSettings LayoutSettings
	{
		get
		{
			return layout_settings;
		}
		set
		{
			if (layout_settings != value)
			{
				layout_settings = value;
				PerformLayout(this, "LayoutSettings");
			}
		}
	}

	/// <summary>Gets or sets a value indicating how the <see cref="T:System.Windows.Forms.ToolStrip" /> lays out the items collection.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The possible values are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow" />, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" /> is not one of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	[AmbientValue(ToolStripLayoutStyle.StackWithOverflow)]
	public ToolStripLayoutStyle LayoutStyle
	{
		get
		{
			return layout_style;
		}
		set
		{
			if (layout_style == value)
			{
				return;
			}
			if (!Enum.IsDefined(typeof(ToolStripLayoutStyle), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripLayoutStyle");
			}
			layout_style = value;
			if (layout_style == ToolStripLayoutStyle.Flow)
			{
				layout_engine = new FlowLayout();
			}
			else
			{
				layout_engine = new ToolStripSplitStackLayout();
			}
			if (layout_style == ToolStripLayoutStyle.StackWithOverflow)
			{
				if (Dock == DockStyle.Left || Dock == DockStyle.Right)
				{
					layout_style = ToolStripLayoutStyle.VerticalStackWithOverflow;
				}
				else
				{
					layout_style = ToolStripLayoutStyle.HorizontalStackWithOverflow;
				}
			}
			if (layout_style == ToolStripLayoutStyle.HorizontalStackWithOverflow)
			{
				orientation = Orientation.Horizontal;
			}
			else if (layout_style == ToolStripLayoutStyle.VerticalStackWithOverflow)
			{
				orientation = Orientation.Vertical;
			}
			layout_settings = CreateLayoutSettings(value);
			PerformLayout(this, "LayoutStyle");
			OnLayoutStyleChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets the orientation of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values. The default is <see cref="F:System.Windows.Forms.Orientation.Horizontal" />.</returns>
	[Browsable(false)]
	public Orientation Orientation => orientation;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the overflow button for a <see cref="T:System.Windows.Forms.ToolStrip" /> with overflow enabled.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> with its <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> set to <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Right" /> and its <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> value set to <see cref="F:System.Windows.Forms.ToolStripItemOverflow.Never" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public ToolStripOverflowButton OverflowButton => overflow_button;

	/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the look and feel of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the look and feel of a <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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
				PerformLayout(this, "Renderer");
				OnRendererChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value that indicates which visual styles will be applied to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A value that indicates the visual style to apply. The default is <see cref="F:System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value being set is not one of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> is set to <see cref="F:System.Windows.Forms.ToolStripRenderMode.Custom" /> without the <see cref="P:System.Windows.Forms.ToolStrip.Renderer" /> property being assigned to a new instance of <see cref="T:System.Windows.Forms.ToolStripRenderer" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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
			switch (value)
			{
			case ToolStripRenderMode.Professional:
				Renderer = new ToolStripProfessionalRenderer();
				break;
			case ToolStripRenderMode.System:
				Renderer = new ToolStripSystemRenderer();
				break;
			}
			render_mode = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether ToolTips are to be displayed on <see cref="T:System.Windows.Forms.ToolStrip" /> items. </summary>
	/// <returns>true if ToolTips are to be displayed; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool ShowItemToolTips
	{
		get
		{
			return show_item_tool_tips;
		}
		set
		{
			show_item_tool_tips = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStrip" /> stretches from end to end in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStrip" /> stretches from end to end in its <see cref="T:System.Windows.Forms.ToolStripContainer" />; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool Stretch
	{
		get
		{
			return stretch;
		}
		set
		{
			stretch = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to an item in the <see cref="T:System.Windows.Forms.ToolStrip" /> using the TAB key.</summary>
	/// <returns>true if the user can give the focus to an item in the <see cref="T:System.Windows.Forms.ToolStrip" /> using the TAB key; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[DispId(-516)]
	public new bool TabStop
	{
		get
		{
			return base.TabStop;
		}
		set
		{
			base.TabStop = value;
			SetStyle(ControlStyles.Selectable, value);
		}
	}

	/// <summary>Gets or sets the direction in which to draw text on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal" />. </returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ToolStripTextDirection.Horizontal)]
	public virtual ToolStripTextDirection TextDirection
	{
		get
		{
			return text_direction;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripTextDirection), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripTextDirection");
			}
			if (text_direction != value)
			{
				text_direction = value;
				PerformLayout(this, "TextDirection");
				Invalidate();
			}
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An instance of the <see cref="T:System.Windows.Forms.VScrollProperties" /> class, which provides basic properties for a <see cref="T:System.Windows.Forms.VScrollBar" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new VScrollProperties VerticalScroll => base.VerticalScroll;

	/// <summary>Gets the docking location of the <see cref="T:System.Windows.Forms.ToolStrip" />, indicating which borders are docked to the container.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Top" />.</returns>
	protected virtual DockStyle DefaultDock => DockStyle.Top;

	/// <summary>Gets the default spacing, in pixels, between the sizing grip and the edges of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>
	///   <see cref="T:System.Windows.Forms.Padding" /> values representing the spacing, in pixels.</returns>
	protected virtual Padding DefaultGripMargin => new Padding(2);

	/// <summary>Gets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStrip" /> and the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values. The default is <see cref="F:System.Windows.Forms.Padding.Empty" />.</returns>
	protected override Padding DefaultMargin => Padding.Empty;

	/// <summary>Gets the internal spacing, in pixels, of the contents of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value of (0, 0, 1, 0).</returns>
	protected override Padding DefaultPadding => new Padding(0, 0, 1, 0);

	/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.ToolStrip" /> by default.</summary>
	/// <returns>true in all cases.</returns>
	protected virtual bool DefaultShowItemToolTips => true;

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	protected override Size DefaultSize => new Size(100, 25);

	/// <summary>Gets the subset of items that are currently displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />, including items that are automatically added into the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> representing the items that are currently displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
	protected internal virtual ToolStripItemCollection DisplayedItems => displayed_items;

	/// <summary>Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the control, in pixels.</returns>
	protected internal virtual Size MaxItemSize => new Size(base.Width - ((GripStyle == ToolStripGripStyle.Hidden) ? 1 : 8), base.Height);

	internal virtual bool KeyboardActive
	{
		get
		{
			return keyboard_active;
		}
		set
		{
			if (keyboard_active != value)
			{
				keyboard_active = value;
				if (value)
				{
					Application.KeyboardCapture = this;
				}
				else if (Application.KeyboardCapture == this)
				{
					Application.KeyboardCapture = null;
					ToolStripManager.ActivatedByKeyboard = false;
				}
				Invalidate();
			}
		}
	}

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

	internal ToolStrip CurrentlyMergedWith
	{
		get
		{
			return currently_merged_with;
		}
		set
		{
			currently_merged_with = value;
		}
	}

	internal List<ToolStripItem> HiddenMergedItems
	{
		get
		{
			if (hidden_merged_items == null)
			{
				hidden_merged_items = new List<ToolStripItem>();
			}
			return hidden_merged_items;
		}
	}

	internal bool IsCurrentlyMerged
	{
		get
		{
			return is_currently_merged;
		}
		set
		{
			is_currently_merged = value;
			if (value || !(this is MenuStrip))
			{
				return;
			}
			foreach (ToolStripMenuItem item in Items)
			{
				item.DropDown.IsCurrentlyMerged = value;
			}
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStrip.AutoSize" /> property has changed.</summary>
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

	/// <summary>Occurs when the user begins to drag the <see cref="T:System.Windows.Forms.ToolStrip" /> control.</summary>
	[System.MonoTODO("Event never raised")]
	public event EventHandler BeginDrag
	{
		add
		{
			base.Events.AddHandler(BeginDragEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BeginDragEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStrip.CausesValidation" /> property changes.</summary>
	[Browsable(false)]
	public new event EventHandler CausesValidationChanged
	{
		add
		{
			base.CausesValidationChanged += value;
		}
		remove
		{
			base.CausesValidationChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event ControlEventHandler ControlAdded
	{
		add
		{
			base.ControlAdded += value;
		}
		remove
		{
			base.ControlAdded -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event ControlEventHandler ControlRemoved
	{
		add
		{
			base.ControlRemoved += value;
		}
		remove
		{
			base.ControlRemoved -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="T:System.Windows.Forms.Cursor" /> property changes.</summary>
	[Browsable(false)]
	public new event EventHandler CursorChanged
	{
		add
		{
			base.CursorChanged += value;
		}
		remove
		{
			base.CursorChanged -= value;
		}
	}

	/// <summary>Occurs when the user stops dragging the <see cref="T:System.Windows.Forms.ToolStrip" /> control.</summary>
	[System.MonoTODO("Event never raised")]
	public event EventHandler EndDrag
	{
		add
		{
			base.Events.AddHandler(EndDragEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(EndDragEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.ForeColor" /> property changes.</summary>
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

	/// <summary>Occurs when a new <see cref="T:System.Windows.Forms.ToolStripItem" /> is added to the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	public event ToolStripItemEventHandler ItemAdded
	{
		add
		{
			base.Events.AddHandler(ItemAddedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemAddedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</summary>
	public event ToolStripItemClickedEventHandler ItemClicked
	{
		add
		{
			base.Events.AddHandler(ItemClickedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemClickedEvent, value);
		}
	}

	/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolStripItem" /> is removed from the <see cref="T:System.Windows.Forms.ToolStripItemCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	public event ToolStripItemEventHandler ItemRemoved
	{
		add
		{
			base.Events.AddHandler(ItemRemovedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemRemovedEvent, value);
		}
	}

	/// <summary>Occurs when the layout of the <see cref="T:System.Windows.Forms.ToolStrip" /> is complete.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler LayoutCompleted
	{
		add
		{
			base.Events.AddHandler(LayoutCompletedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LayoutCompletedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.LayoutStyle" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler LayoutStyleChanged
	{
		add
		{
			base.Events.AddHandler(LayoutStyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LayoutStyleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStrip" /> move handle is painted.</summary>
	/// <filterpriority>1</filterpriority>
	public event PaintEventHandler PaintGrip
	{
		add
		{
			base.Events.AddHandler(PaintGripEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PaintGripEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.Renderer" /> property changes.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip" /> class.</summary>
	public ToolStrip()
		: this((ToolStripItem[])null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStrip" /> class with the specified array of <see cref="T:System.Windows.Forms.ToolStripItem" />s.</summary>
	/// <param name="items">An array of <see cref="T:System.Windows.Forms.ToolStripItem" /> objects.</param>
	public ToolStrip(params ToolStripItem[] items)
	{
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		SetStyle(ControlStyles.Selectable, value: false);
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		SuspendLayout();
		this.items = new ToolStripItemCollection(this, items, internalcreated: true);
		allow_merge = true;
		base.AutoSize = true;
		SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
		back_color = Control.DefaultBackColor;
		can_overflow = true;
		base.CausesValidation = false;
		default_drop_down_direction = ToolStripDropDownDirection.BelowRight;
		displayed_items = new ToolStripItemCollection(this, null, internalcreated: true);
		Dock = DefaultDock;
		base.Font = new Font("Tahoma", 8.25f);
		fore_color = Control.DefaultForeColor;
		grip_margin = DefaultGripMargin;
		grip_style = ToolStripGripStyle.Visible;
		image_scaling_size = new Size(16, 16);
		layout_style = ToolStripLayoutStyle.HorizontalStackWithOverflow;
		orientation = Orientation.Horizontal;
		if (!(this is ToolStripDropDown))
		{
			overflow_button = new ToolStripOverflowButton(this);
		}
		renderer = null;
		render_mode = ToolStripRenderMode.ManagerRenderMode;
		show_item_tool_tips = DefaultShowItemToolTips;
		base.TabStop = false;
		text_direction = ToolStripTextDirection.Horizontal;
		ResumeLayout();
		ToolStripManager.AddToolStrip(this);
	}

	static ToolStrip()
	{
		BeginDrag = new object();
		EndDrag = new object();
		ItemAdded = new object();
		ItemClicked = new object();
		ItemRemoved = new object();
		LayoutCompleted = new object();
		LayoutStyleChanged = new object();
		PaintGrip = new object();
		RendererChanged = new object();
	}

	/// <summary>This method is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" />.</returns>
	/// <param name="point">A <see cref="T:System.Drawing.Point" />.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Control GetChildAtPoint(Point point)
	{
		return base.GetChildAtPoint(point);
	}

	/// <summary>This method is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" />.</returns>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> value.</param>
	/// <param name="skipValue">A <see cref="T:System.Windows.Forms.GetChildAtPointSkip" />  value.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
	{
		return base.GetChildAtPoint(pt, skipValue);
	}

	/// <summary>Returns the item located at the specified point in the client area of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> at the specified location, or null if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is not found.</returns>
	/// <param name="point">The <see cref="T:System.Drawing.Point" /> at which to search for the <see cref="T:System.Windows.Forms.ToolStripItem" />. </param>
	/// <filterpriority>1</filterpriority>
	public ToolStripItem GetItemAt(Point point)
	{
		foreach (ToolStripItem displayed_item in displayed_items)
		{
			if (displayed_item.Visible && displayed_item.Bounds.Contains(point))
			{
				return displayed_item;
			}
		}
		return null;
	}

	/// <summary>Returns the item located at the specified x- and y-coordinates of the <see cref="T:System.Windows.Forms.ToolStrip" /> client area.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> located at the specified location, or null if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is not found.</returns>
	/// <param name="x">The horizontal coordinate, in pixels, from the left edge of the client area. </param>
	/// <param name="y">The vertical coordinate, in pixels, from the top edge of the client area. </param>
	/// <filterpriority>1</filterpriority>
	public ToolStripItem GetItemAt(int x, int y)
	{
		return GetItemAt(new Point(x, y));
	}

	/// <summary>Retrieves the next <see cref="T:System.Windows.Forms.ToolStripItem" /> from the specified reference point and moving in the specified direction.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItem" /> that is specified by the <paramref name="start" /> parameter and is next in the order as specified by the <paramref name="direction" /> parameter.</returns>
	/// <param name="start">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the reference point from which to begin the retrieval of the next item.</param>
	/// <param name="direction">One of the values of <see cref="T:System.Windows.Forms.ArrowDirection" /> that specifies the direction to move.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value of the <paramref name="direction" /> parameter is not one of the values of <see cref="T:System.Windows.Forms.ArrowDirection" />.</exception>
	public virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction)
	{
		if (!Enum.IsDefined(typeof(ArrowDirection), direction))
		{
			throw new InvalidEnumArgumentException($"Enum argument value '{direction}' is not valid for ArrowDirection");
		}
		ToolStripItem toolStripItem = null;
		switch (direction)
		{
		case ArrowDirection.Right:
		{
			int num = int.MaxValue;
			if (start != null)
			{
				foreach (ToolStripItem displayedItem in DisplayedItems)
				{
					if (displayedItem.Left >= start.Right && displayedItem.Left < num && displayedItem.Visible && displayedItem.CanSelect)
					{
						toolStripItem = displayedItem;
						num = displayedItem.Left;
					}
				}
			}
			if (toolStripItem != null)
			{
				break;
			}
			foreach (ToolStripItem displayedItem2 in DisplayedItems)
			{
				if (displayedItem2.Left < num && displayedItem2.Visible && displayedItem2.CanSelect)
				{
					toolStripItem = displayedItem2;
					num = displayedItem2.Left;
				}
			}
			break;
		}
		case ArrowDirection.Up:
		{
			int num = int.MinValue;
			if (start != null)
			{
				foreach (ToolStripItem displayedItem3 in DisplayedItems)
				{
					if (displayedItem3.Bottom <= start.Top && displayedItem3.Top > num && displayedItem3.Visible && displayedItem3.CanSelect)
					{
						toolStripItem = displayedItem3;
						num = displayedItem3.Top;
					}
				}
			}
			if (toolStripItem != null)
			{
				break;
			}
			foreach (ToolStripItem displayedItem4 in DisplayedItems)
			{
				if (displayedItem4.Top > num && displayedItem4.Visible && displayedItem4.CanSelect)
				{
					toolStripItem = displayedItem4;
					num = displayedItem4.Top;
				}
			}
			break;
		}
		case ArrowDirection.Left:
		{
			int num = int.MinValue;
			if (start != null)
			{
				foreach (ToolStripItem displayedItem5 in DisplayedItems)
				{
					if (displayedItem5.Right <= start.Left && displayedItem5.Left > num && displayedItem5.Visible && displayedItem5.CanSelect)
					{
						toolStripItem = displayedItem5;
						num = displayedItem5.Left;
					}
				}
			}
			if (toolStripItem != null)
			{
				break;
			}
			foreach (ToolStripItem displayedItem6 in DisplayedItems)
			{
				if (displayedItem6.Left > num && displayedItem6.Visible && displayedItem6.CanSelect)
				{
					toolStripItem = displayedItem6;
					num = displayedItem6.Left;
				}
			}
			break;
		}
		case ArrowDirection.Down:
		{
			int num = int.MaxValue;
			if (start != null)
			{
				foreach (ToolStripItem displayedItem7 in DisplayedItems)
				{
					if (displayedItem7.Top >= start.Bottom && displayedItem7.Bottom < num && displayedItem7.Visible && displayedItem7.CanSelect)
					{
						toolStripItem = displayedItem7;
						num = displayedItem7.Top;
					}
				}
			}
			if (toolStripItem != null)
			{
				break;
			}
			foreach (ToolStripItem displayedItem8 in DisplayedItems)
			{
				if (displayedItem8.Top < num && displayedItem8.Visible && displayedItem8.CanSelect)
				{
					toolStripItem = displayedItem8;
					num = displayedItem8.Top;
				}
			}
			break;
		}
		}
		return toolStripItem;
	}

	/// <summary>This method is not relevant for this class.</summary>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void ResetMinimumSize()
	{
		MinimumSize = new Size(-1, -1);
	}

	/// <summary>This method is not relevant for this class.</summary>
	/// <param name="x">An <see cref="T:System.Int32" />.</param>
	/// <param name="y">An <see cref="T:System.Int32" />.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void SetAutoScrollMargin(int x, int y)
	{
		base.SetAutoScrollMargin(x, y);
	}

	public override string ToString()
	{
		return $"{base.ToString()}, Name: {base.Name}, Items: {items.Count.ToString()}";
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStrip" /> item.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStrip" /> item.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripAccessibleObject(this);
	}

	protected override ControlCollection CreateControlsInstance()
	{
		return base.CreateControlsInstance();
	}

	/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStrip" /> instance.</summary>
	/// <returns>A <see cref="M:System.Windows.Forms.ToolStripButton.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
	/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</param>
	protected internal virtual ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
	{
		if (text == "-")
		{
			return new ToolStripSeparator();
		}
		if (this is ToolStripDropDown)
		{
			return new ToolStripMenuItem(text, image, onClick);
		}
		return new ToolStripButton(text, image, onClick);
	}

	/// <summary>Specifies the visual arrangement for the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is null.</returns>
	/// <param name="layoutStyle">The visual arrangement to be applied to the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
	protected virtual LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle layoutStyle)
	{
		return layoutStyle switch
		{
			ToolStripLayoutStyle.Flow => new FlowLayoutSettings(this), 
			_ => null, 
		};
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStrip" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		if (!base.IsDisposed)
		{
			for (int num = Items.Count - 1; num >= 0; num--)
			{
				Items[num].Dispose();
			}
			if (overflow_button != null && overflow_button.drop_down != null)
			{
				overflow_button.drop_down.Dispose();
			}
			ToolStripManager.RemoveToolStrip(this);
			base.Dispose(disposing);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.BeginDrag" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[System.MonoTODO("Stub, never called")]
	protected virtual void OnBeginDrag(EventArgs e)
	{
		((EventHandler)base.Events[BeginDrag])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnDockChanged(EventArgs e)
	{
		base.OnDockChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.EndDrag" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[System.MonoTODO("Stub, never called")]
	protected virtual void OnEndDrag(EventArgs e)
	{
		((EventHandler)base.Events[EndDrag])?.Invoke(this, e);
	}

	/// <summary>Determines whether a character is an input character that the item recognizes.</summary>
	/// <returns>true if the character should be sent directly to the item and not preprocessed; otherwise, false.</returns>
	/// <param name="charCode">The character to test.</param>
	protected override bool IsInputChar(char charCode)
	{
		return base.IsInputChar(charCode);
	}

	/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
	/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
	protected override bool IsInputKey(Keys keyData)
	{
		return base.IsInputKey(keyData);
	}

	/// <summary>Raises the <see cref="P:System.Windows.Forms.Control.Enabled" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
		foreach (ToolStripItem item in Items)
		{
			item.OnParentEnabledChanged(EventArgs.Empty);
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data. </param>
	protected override void OnInvalidated(InvalidateEventArgs e)
	{
		base.OnInvalidated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemAdded" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> that contains the event data.</param>
	protected internal virtual void OnItemAdded(ToolStripItemEventArgs e)
	{
		if (e.Item.InternalVisible)
		{
			e.Item.Available = true;
		}
		e.Item.SetPlacement(ToolStripItemPlacement.Main);
		if (base.Created)
		{
			PerformLayout();
		}
		((ToolStripItemEventHandler)base.Events[ItemAdded])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data. </param>
	protected virtual void OnItemClicked(ToolStripItemClickedEventArgs e)
	{
		if (KeyboardActive)
		{
			ToolStripManager.SetActiveToolStrip(null, keyboard: false);
		}
		((ToolStripItemClickedEventHandler)base.Events[ItemClicked])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemRemoved" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemEventArgs" /> that contains the event data.</param>
	protected internal virtual void OnItemRemoved(ToolStripItemEventArgs e)
	{
		((ToolStripItemEventHandler)base.Events[ItemRemoved])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		base.OnLayout(e);
		SetDisplayedItems();
		OnLayoutCompleted(EventArgs.Empty);
		Invalidate();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.LayoutCompleted" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLayoutCompleted(EventArgs e)
	{
		((EventHandler)base.Events[LayoutCompleted])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.LayoutStyleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLayoutStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[LayoutStyleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnLeave(EventArgs e)
	{
		base.OnLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseCaptureChanged(EventArgs e)
	{
		base.OnMouseCaptureChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
	/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs mea)
	{
		if (mouse_currently_over != null)
		{
			ToolStripItem currentlyFocusedItem = GetCurrentlyFocusedItem();
			if (currentlyFocusedItem != null && currentlyFocusedItem != mouse_currently_over)
			{
				FocusInternal(skip_check: true);
			}
			if (this is MenuStrip && !menu_selected)
			{
				(this as MenuStrip).FireMenuActivate();
				menu_selected = true;
			}
			mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseDown);
			if (this is MenuStrip && mouse_currently_over is ToolStripMenuItem && !(mouse_currently_over as ToolStripMenuItem).HasDropDownItems)
			{
				return;
			}
		}
		else
		{
			HideMenus(release: true, ToolStripDropDownCloseReason.AppClicked);
		}
		if (this is MenuStrip)
		{
			base.Capture = false;
		}
		base.OnMouseDown(mea);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		if (mouse_currently_over != null)
		{
			MouseLeftItem(mouse_currently_over);
			mouse_currently_over.FireEvent(e, ToolStripItemEventType.MouseLeave);
			mouse_currently_over = null;
		}
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
	/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseMove(MouseEventArgs mea)
	{
		ToolStripItem toolStripItem = ((overflow_button == null || !overflow_button.Visible || !overflow_button.Bounds.Contains(mea.Location)) ? GetItemAt(mea.X, mea.Y) : overflow_button);
		if (toolStripItem != null)
		{
			if (toolStripItem == mouse_currently_over)
			{
				toolStripItem.FireEvent(mea, ToolStripItemEventType.MouseMove);
			}
			else
			{
				if (mouse_currently_over != null)
				{
					MouseLeftItem(toolStripItem);
					mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseLeave);
				}
				mouse_currently_over = toolStripItem;
				toolStripItem.FireEvent(mea, ToolStripItemEventType.MouseEnter);
				MouseEnteredItem(toolStripItem);
				toolStripItem.FireEvent(mea, ToolStripItemEventType.MouseMove);
				if (menu_selected && mouse_currently_over.Enabled && mouse_currently_over is ToolStripDropDownItem && (mouse_currently_over as ToolStripDropDownItem).HasDropDownItems)
				{
					(mouse_currently_over as ToolStripDropDownItem).ShowDropDown();
				}
			}
		}
		else if (mouse_currently_over != null)
		{
			MouseLeftItem(toolStripItem);
			mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseLeave);
			mouse_currently_over = null;
		}
		base.OnMouseMove(mea);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
	/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs mea)
	{
		if (mouse_currently_over != null && !(mouse_currently_over is ToolStripControlHost) && mouse_currently_over.Enabled)
		{
			OnItemClicked(new ToolStripItemClickedEventArgs(mouse_currently_over));
			if (mouse_currently_over != null)
			{
				mouse_currently_over.FireEvent(mea, ToolStripItemEventType.MouseUp);
			}
			if (mouse_currently_over == null)
			{
				return;
			}
		}
		base.OnMouseUp(mea);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		OnPaintGrip(e);
		for (int i = 0; i < displayed_items.Count; i++)
		{
			ToolStripItem toolStripItem = displayed_items[i];
			if (toolStripItem.Visible)
			{
				e.Graphics.TranslateTransform(toolStripItem.Bounds.Left, toolStripItem.Bounds.Top);
				toolStripItem.FireEvent(e, ToolStripItemEventType.Paint);
				e.Graphics.ResetTransform();
			}
		}
		if (overflow_button != null && overflow_button.Visible)
		{
			e.Graphics.TranslateTransform(overflow_button.Bounds.Left, overflow_button.Bounds.Top);
			overflow_button.FireEvent(e, ToolStripItemEventType.Paint);
			e.Graphics.ResetTransform();
		}
		ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(affectedBounds: new Rectangle(Point.Empty, base.Size), g: e.Graphics, toolStrip: this, backColor: Color.Empty);
		toolStripRenderEventArgs.InternalConnectedArea = CalculateConnectedArea();
		Renderer.DrawToolStripBorder(toolStripRenderEventArgs);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event for the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
		ToolStripRenderEventArgs e2 = new ToolStripRenderEventArgs(affectedBounds: new Rectangle(Point.Empty, base.Size), g: e.Graphics, toolStrip: this, backColor: SystemColors.Control);
		Renderer.DrawToolStripBackground(e2);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.PaintGrip" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected internal virtual void OnPaintGrip(PaintEventArgs e)
	{
		if (layout_style == ToolStripLayoutStyle.Flow || layout_style == ToolStripLayoutStyle.Table)
		{
			return;
		}
		((PaintEventHandler)base.Events[PaintGrip])?.Invoke(this, e);
		if (!(this is MenuStrip))
		{
			if (orientation == Orientation.Horizontal)
			{
				e.Graphics.TranslateTransform(2f, 0f);
			}
			else
			{
				e.Graphics.TranslateTransform(0f, 2f);
			}
		}
		Renderer.DrawGrip(new ToolStripGripRenderEventArgs(e.Graphics, this, GripRectangle, GripDisplayStyle, grip_style));
		e.Graphics.ResetTransform();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.RendererChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnRendererChanged(EventArgs e)
	{
		((EventHandler)base.Events[RendererChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
		foreach (ToolStripItem item in Items)
		{
			item.OnParentRightToLeftChanged(e);
		}
	}

	/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
	protected override void OnScroll(ScrollEventArgs se)
	{
		base.OnScroll(se);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnTabStopChanged(EventArgs e)
	{
		base.OnTabStopChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnVisibleChanged(EventArgs e)
	{
		base.OnVisibleChanged(e);
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
	protected override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		return base.ProcessCmdKey(ref m, keyData);
	}

	/// <summary>Processes a dialog box key.</summary>
	/// <returns>true if the key was processed by the control; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		if (!KeyboardActive)
		{
			return false;
		}
		foreach (ToolStripItem item in Items)
		{
			if (item.ProcessDialogKey(keyData))
			{
				return true;
			}
		}
		if (ProcessArrowKey(keyData))
		{
			return true;
		}
		ToolStrip toolStrip = null;
		switch (keyData)
		{
		case Keys.Escape:
			Dismiss(ToolStripDropDownCloseReason.Keyboard);
			return true;
		case Keys.Tab | Keys.Control:
			toolStrip = ToolStripManager.GetNextToolStrip(this, forward: true);
			if (toolStrip != null)
			{
				foreach (ToolStripItem item2 in Items)
				{
					item2.Dismiss(ToolStripDropDownCloseReason.Keyboard);
				}
				ToolStripManager.SetActiveToolStrip(toolStrip, keyboard: true);
				toolStrip.SelectNextToolStripItem(null, forward: true);
			}
			return true;
		case Keys.Tab | Keys.Shift | Keys.Control:
			toolStrip = ToolStripManager.GetNextToolStrip(this, forward: false);
			if (toolStrip != null)
			{
				foreach (ToolStripItem item3 in Items)
				{
					item3.Dismiss(ToolStripDropDownCloseReason.Keyboard);
				}
				ToolStripManager.SetActiveToolStrip(toolStrip, keyboard: true);
				toolStrip.SelectNextToolStripItem(null, forward: true);
			}
			return true;
		case Keys.Left:
		case Keys.Up:
		case Keys.Right:
		case Keys.Down:
			if (GetCurrentlySelectedItem() is ToolStripControlHost)
			{
				return false;
			}
			break;
		}
		return base.ProcessDialogKey(keyData);
	}

	/// <summary>Processes a mnemonic character.</summary>
	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		foreach (ToolStripItem item in Items)
		{
			if (item.Enabled && item.Visible && !string.IsNullOrEmpty(item.Text) && Control.IsMnemonic(charCode, item.Text))
			{
				return item.ProcessMnemonic(charCode);
			}
		}
		string value = char.ToUpper(charCode).ToString();
		if ((Control.ModifierKeys & Keys.Alt) != 0 || this is ToolStripDropDownMenu)
		{
			foreach (ToolStripItem item2 in Items)
			{
				if (item2.Enabled && item2.Visible && !string.IsNullOrEmpty(item2.Text) && item2.Text.ToUpper().StartsWith(value) && !(item2 is ToolStripControlHost))
				{
					return item2.ProcessMnemonic(charCode);
				}
			}
		}
		return base.ProcessMnemonic(charCode);
	}

	/// <summary>Controls the return location of the focus.</summary>
	[System.MonoTODO("Stub, does nothing")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void RestoreFocus()
	{
	}

	/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
	/// <param name="directed">true to specify the direction of the control to select; otherwise, false.</param>
	/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order.</param>
	protected override void Select(bool directed, bool forward)
	{
		foreach (ToolStripItem displayedItem in DisplayedItems)
		{
			if (displayedItem.CanSelect)
			{
				displayedItem.Select();
				break;
			}
		}
	}

	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
	protected virtual void SetDisplayedItems()
	{
		displayed_items.Clear();
		foreach (ToolStripItem item in items)
		{
			if (item.Placement == ToolStripItemPlacement.Main && item.Available)
			{
				displayed_items.AddNoOwnerOrLayout(item);
				item.Parent = this;
			}
			else if (item.Placement == ToolStripItemPlacement.Overflow)
			{
				item.Parent = OverflowButton.DropDown;
			}
		}
		if (OverflowButton != null)
		{
			OverflowButton.DropDown.SetDisplayedItems();
		}
	}

	/// <summary>Anchors a <see cref="T:System.Windows.Forms.ToolStripItem" /> to a particular place on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to anchor.</param>
	/// <param name="location">A <see cref="T:System.Drawing.Point" /> representing the x and y client coordinates of the <see cref="T:System.Windows.Forms.ToolStripItem" /> location, in pixels.</param>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="item" /> parameter is null.</exception>
	/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Windows.Forms.ToolStrip" /> is not the owner of the <see cref="T:System.Windows.Forms.ToolStripItem" /> referred to by the <paramref name="item" /> parameter.</exception>
	protected internal void SetItemLocation(ToolStripItem item, Point location)
	{
		if (item == null)
		{
			throw new ArgumentNullException("item");
		}
		if (item.Owner != this)
		{
			throw new NotSupportedException("The item is not owned by this ToolStrip");
		}
		item.SetBounds(new Rectangle(location, item.Size));
	}

	/// <summary>Enables you to change the parent <see cref="T:System.Windows.Forms.ToolStrip" /> of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> whose <see cref="P:System.Windows.Forms.Control.Parent" /> property is to be changed. </param>
	/// <param name="parent">The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" /> referred to by the <paramref name="item" /> parameter. </param>
	protected internal static void SetItemParent(ToolStripItem item, ToolStrip parent)
	{
		if (item.Owner != null)
		{
			item.Owner.Items.RemoveNoOwnerOrLayout(item);
			if (item.Owner is ToolStripOverflow)
			{
				(item.Owner as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(item);
			}
		}
		parent.Items.AddNoOwnerOrLayout(item);
		item.Parent = parent;
	}

	/// <summary>Retrieves a value that sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visibility state.</summary>
	/// <param name="visible">true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is visible; otherwise, false. </param>
	protected override void SetVisibleCore(bool visible)
	{
		base.SetVisibleCore(visible);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal virtual Rectangle CalculateConnectedArea()
	{
		return Rectangle.Empty;
	}

	internal void ChangeSelection(ToolStripItem nextItem)
	{
		if (Application.KeyboardCapture != this)
		{
			ToolStripManager.SetActiveToolStrip(this, ToolStripManager.ActivatedByKeyboard);
		}
		foreach (ToolStripItem item in Items)
		{
			if (item != nextItem)
			{
				item.Dismiss(ToolStripDropDownCloseReason.Keyboard);
			}
		}
		ToolStripItem currentlySelectedItem = GetCurrentlySelectedItem();
		if (currentlySelectedItem != null && !(currentlySelectedItem is ToolStripControlHost))
		{
			FocusInternal(skip_check: true);
		}
		if (nextItem is ToolStripControlHost)
		{
			(nextItem as ToolStripControlHost).Focus();
		}
		nextItem.Select();
		if (nextItem.Parent is MenuStrip && (nextItem.Parent as MenuStrip).MenuDroppedDown)
		{
			(nextItem as ToolStripMenuItem).HandleAutoExpansion();
		}
	}

	internal virtual void Dismiss()
	{
		Dismiss(ToolStripDropDownCloseReason.AppClicked);
	}

	internal virtual void Dismiss(ToolStripDropDownCloseReason reason)
	{
		KeyboardActive = false;
		menu_selected = false;
		foreach (ToolStripItem item in Items)
		{
			item.Dismiss(reason);
		}
		Invalidate();
	}

	internal ToolStripItem GetCurrentlySelectedItem()
	{
		foreach (ToolStripItem displayedItem in DisplayedItems)
		{
			if (displayedItem.Selected)
			{
				return displayedItem;
			}
		}
		return null;
	}

	internal ToolStripItem GetCurrentlyFocusedItem()
	{
		foreach (ToolStripItem displayedItem in DisplayedItems)
		{
			if (displayedItem is ToolStripControlHost && (displayedItem as ToolStripControlHost).Control.Focused)
			{
				return displayedItem;
			}
		}
		return null;
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		return GetToolStripPreferredSize(proposedSize);
	}

	internal virtual Size GetToolStripPreferredSize(Size proposedSize)
	{
		Size empty = Size.Empty;
		if (LayoutStyle == ToolStripLayoutStyle.Flow)
		{
			Point empty2 = Point.Empty;
			int num = 0;
			foreach (ToolStripItem item in items)
			{
				if (DisplayRectangle.Width - empty2.X < item.Width + item.Margin.Horizontal)
				{
					empty2.Y += num;
					num = 0;
					empty2.X = DisplayRectangle.Left;
				}
				empty2.Offset(item.Margin.Left, 0);
				num = Math.Max(num, item.Height + item.Margin.Vertical);
				empty2.X += item.Width + item.Margin.Right;
			}
			empty2.Y += num;
			return new Size(empty2.X, empty2.Y);
		}
		if (orientation == Orientation.Vertical)
		{
			foreach (ToolStripItem item2 in items)
			{
				if (item2.Available)
				{
					Size preferredSize = item2.GetPreferredSize(Size.Empty);
					empty.Height += preferredSize.Height + item2.Margin.Top + item2.Margin.Bottom;
					if (empty.Width < base.Padding.Horizontal + preferredSize.Width + item2.Margin.Horizontal)
					{
						empty.Width = base.Padding.Horizontal + preferredSize.Width + item2.Margin.Horizontal;
					}
				}
			}
			empty.Height += GripRectangle.Height + GripMargin.Vertical + base.Padding.Vertical + 4;
			if (empty.Width == 0)
			{
				empty.Width = base.ExplicitBounds.Width;
			}
			return empty;
		}
		foreach (ToolStripItem item3 in items)
		{
			if (item3.Available)
			{
				Size preferredSize2 = item3.GetPreferredSize(Size.Empty);
				empty.Width += preferredSize2.Width + item3.Margin.Left + item3.Margin.Right;
				if (empty.Height < base.Padding.Vertical + preferredSize2.Height + item3.Margin.Vertical)
				{
					empty.Height = base.Padding.Vertical + preferredSize2.Height + item3.Margin.Vertical;
				}
			}
		}
		empty.Width += GripRectangle.Width + GripMargin.Horizontal + base.Padding.Horizontal + 4;
		if (empty.Height == 0)
		{
			empty.Height = base.ExplicitBounds.Height;
		}
		if (this is StatusStrip)
		{
			empty.Height = Math.Max(empty.Height, 22);
		}
		return empty;
	}

	internal virtual ToolStrip GetTopLevelToolStrip()
	{
		return this;
	}

	internal virtual void HandleItemClick(ToolStripItem dismissingItem)
	{
		GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.ItemClicked);
	}

	internal void HideMenus(bool release, ToolStripDropDownCloseReason reason)
	{
		if (this is MenuStrip && release && menu_selected)
		{
			(this as MenuStrip).FireMenuDeactivate();
		}
		if (release)
		{
			menu_selected = false;
		}
		NotifySelectedChanged(null);
	}

	internal void NotifySelectedChanged(ToolStripItem tsi)
	{
		foreach (ToolStripItem displayedItem in DisplayedItems)
		{
			if (tsi != displayedItem && displayedItem is ToolStripDropDownItem)
			{
				(displayedItem as ToolStripDropDownItem).HideDropDown(ToolStripDropDownCloseReason.Keyboard);
			}
		}
		if (OverflowButton != null)
		{
			ToolStripItemCollection displayedItems = OverflowButton.DropDown.DisplayedItems;
			foreach (ToolStripItem item in displayedItems)
			{
				if (tsi != item && item is ToolStripDropDownItem)
				{
					(item as ToolStripDropDownItem).HideDropDown(ToolStripDropDownCloseReason.Keyboard);
				}
			}
			OverflowButton.HideDropDown();
		}
		foreach (ToolStripItem item2 in Items)
		{
			if (tsi != item2)
			{
				item2.Dismiss(ToolStripDropDownCloseReason.Keyboard);
			}
		}
	}

	internal virtual bool OnMenuKey()
	{
		return false;
	}

	internal virtual bool ProcessArrowKey(Keys keyData)
	{
		switch (keyData)
		{
		case Keys.Right:
		{
			ToolStripItem currentlySelectedItem = GetCurrentlySelectedItem();
			if (currentlySelectedItem is ToolStripControlHost)
			{
				return false;
			}
			currentlySelectedItem = SelectNextToolStripItem(currentlySelectedItem, forward: true);
			if (currentlySelectedItem is ToolStripControlHost)
			{
				(currentlySelectedItem as ToolStripControlHost).Focus();
			}
			return true;
		}
		case Keys.Tab:
		{
			ToolStripItem currentlySelectedItem = GetCurrentlySelectedItem();
			currentlySelectedItem = SelectNextToolStripItem(currentlySelectedItem, forward: true);
			if (currentlySelectedItem is ToolStripControlHost)
			{
				(currentlySelectedItem as ToolStripControlHost).Focus();
			}
			return true;
		}
		case Keys.Left:
		{
			ToolStripItem currentlySelectedItem = GetCurrentlySelectedItem();
			if (currentlySelectedItem is ToolStripControlHost)
			{
				return false;
			}
			currentlySelectedItem = SelectNextToolStripItem(currentlySelectedItem, forward: false);
			if (currentlySelectedItem is ToolStripControlHost)
			{
				(currentlySelectedItem as ToolStripControlHost).Focus();
			}
			return true;
		}
		case Keys.Tab | Keys.Shift:
		{
			ToolStripItem currentlySelectedItem = GetCurrentlySelectedItem();
			currentlySelectedItem = SelectNextToolStripItem(currentlySelectedItem, forward: false);
			if (currentlySelectedItem is ToolStripControlHost)
			{
				(currentlySelectedItem as ToolStripControlHost).Focus();
			}
			return true;
		}
		default:
			return false;
		}
	}

	internal virtual ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
	{
		ToolStripItem nextItem = GetNextItem(start, forward ? ArrowDirection.Right : ArrowDirection.Left);
		if (nextItem == null)
		{
			return nextItem;
		}
		ChangeSelection(nextItem);
		if (nextItem is ToolStripControlHost)
		{
			(nextItem as ToolStripControlHost).Focus();
		}
		return nextItem;
	}

	private void MouseEnteredItem(ToolStripItem item)
	{
		if (show_item_tool_tips && !(item is ToolStripTextBox))
		{
			tooltip_currently_showing = item;
			ToolTipTimer.Start();
		}
	}

	private void MouseLeftItem(ToolStripItem item)
	{
		ToolTipTimer.Stop();
		ToolTipWindow.Hide(this);
		tooltip_currently_showing = null;
	}

	private void ToolTipTimer_Tick(object o, EventArgs args)
	{
		string toolTip = tooltip_currently_showing.GetToolTip();
		if (!string.IsNullOrEmpty(toolTip))
		{
			ToolTipWindow.Present(this, toolTip);
		}
		tooltip_currently_showing.FireEvent(EventArgs.Empty, ToolStripItemEventType.MouseHover);
		ToolTipTimer.Stop();
	}

	internal void BeginMerge()
	{
		if (IsCurrentlyMerged)
		{
			return;
		}
		IsCurrentlyMerged = true;
		if (pre_merge_items != null)
		{
			return;
		}
		pre_merge_items = new List<ToolStripItem>();
		foreach (ToolStripItem item in Items)
		{
			pre_merge_items.Add(item);
		}
	}

	internal void RevertMergeItem(ToolStripItem item)
	{
		int num = 0;
		if (item.Parent != null && item.Parent != this)
		{
			if (item.Parent is ToolStripOverflow)
			{
				(item.Parent as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(item);
			}
			else
			{
				item.Parent.Items.RemoveNoOwnerOrLayout(item);
			}
			item.Parent = item.Owner;
		}
		num = item.Owner.pre_merge_items.IndexOf(item);
		for (int i = num; i < pre_merge_items.Count; i++)
		{
			if (Items.Contains(pre_merge_items[i]))
			{
				item.Owner.Items.InsertNoOwnerOrLayout(Items.IndexOf(pre_merge_items[i]), item);
				return;
			}
		}
		item.Owner.Items.AddNoOwnerOrLayout(item);
	}
}
