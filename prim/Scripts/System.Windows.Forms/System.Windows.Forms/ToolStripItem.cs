using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents the abstract base class that manages events and layout for all the elements that a <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripDropDown" /> can contain.</summary>
/// <filterpriority>1</filterpriority>
[DesignTimeVisible(false)]
[DefaultProperty("Text")]
[DefaultEvent("Click")]
[Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxItem(false)]
public abstract class ToolStripItem : Component, IDisposable, IComponent, IDropTarget
{
	/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripItem" /> for users with impairments.</summary>
	[ComVisible(true)]
	public class ToolStripItemAccessibleObject : AccessibleObject
	{
		internal ToolStripItem owner_item;

		/// <summary>Gets the bounds of the accessible object, in screen coordinates.</summary>
		/// <returns>An object of type <see cref="T:System.Drawing.Rectangle" /> representing the bounds.</returns>
		public override Rectangle Bounds => (!owner_item.Visible) ? Rectangle.Empty : owner_item.Bounds;

		/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A description of the default action of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		public override string DefaultAction => base.DefaultAction;

		/// <summary>Gets the description of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</summary>
		/// <returns>A string describing the <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" />.</returns>
		public override string Description => base.Description;

		/// <summary>Gets the description of what the object does or how the object is used.</summary>
		/// <returns>A string describing what the object does or how the object is used.</returns>
		public override string Help => base.Help;

		/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
		/// <returns>The shortcut key or access key for the accessible object, or null if there is no shortcut key associated with the object.</returns>
		public override string KeyboardShortcut => base.KeyboardShortcut;

		/// <summary>Gets or sets the name of the accessible object.</summary>
		/// <returns>The object name, or null if the property has not been set.</returns>
		public override string Name
		{
			get
			{
				if (name == string.Empty)
				{
					return owner_item.Text;
				}
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		/// <summary>Gets or sets the parent of an accessible object.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.AccessibleObject" /> representing the parent.</returns>
		public override AccessibleObject Parent => base.Parent;

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
		public override AccessibleRole Role => base.Role;

		/// <summary>Gets the state of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" /> if no state has been set.</returns>
		public override AccessibleStates State => base.State;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" /> class.</summary>
		/// <param name="ownerItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that owns this <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ownerItem" /> parameter is null. </exception>
		public ToolStripItemAccessibleObject(ToolStripItem ownerItem)
		{
			if (ownerItem == null)
			{
				throw new ArgumentNullException("ownerItem");
			}
			owner_item = ownerItem;
			default_action = string.Empty;
			keyboard_shortcut = string.Empty;
			name = string.Empty;
			value = string.Empty;
		}

		/// <summary>Adds a <see cref="P:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject.State" /> if <see cref="T:System.Windows.Forms.AccessibleStates" /> is <see cref="F:System.Windows.Forms.AccessibleStates.None" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values other than <see cref="F:System.Windows.Forms.AccessibleStates.None" />.</param>
		public void AddState(AccessibleStates state)
		{
			base.state = state;
		}

		/// <summary>Performs the default action associated with this accessible object. </summary>
		public override void DoDefaultAction()
		{
			base.DoDefaultAction();
		}

		/// <summary>Gets an identifier for a Help topic and the path to the Help file associated with this accessible object.</summary>
		/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter will contain the path to the Help file associated with this accessible object, or null if there is no IAccessible interface specified.</returns>
		/// <param name="fileName">When this method returns, contains a string that represents the path to the Help file associated with this accessible object. This parameter is passed without being initialized. </param>
		public override int GetHelpTopic(out string fileName)
		{
			return base.GetHelpTopic(out fileName);
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
		/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" />  values.</param>
		public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
		{
			return base.Navigate(navigationDirection);
		}

		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return $"ToolStripItemAccessibleObject: Owner = {owner_item.ToString()}";
		}
	}

	private AccessibleObject accessibility_object;

	private string accessible_default_action_description;

	private bool allow_drop;

	private ToolStripItemAlignment alignment;

	private AnchorStyles anchor;

	private bool available;

	private bool auto_size;

	private bool auto_tool_tip;

	private Color back_color;

	private Image background_image;

	private ImageLayout background_image_layout;

	private Rectangle bounds;

	private bool can_select;

	private ToolStripItemDisplayStyle display_style;

	private DockStyle dock;

	private bool double_click_enabled;

	private bool enabled;

	private Size explicit_size;

	private Font font;

	private Color fore_color;

	private Image image;

	private ContentAlignment image_align;

	private int image_index;

	private string image_key;

	private ToolStripItemImageScaling image_scaling;

	private Color image_transparent_color;

	private bool is_disposed;

	internal bool is_pressed;

	private bool is_selected;

	private Padding margin;

	private MergeAction merge_action;

	private int merge_index;

	private string name;

	private ToolStripItemOverflow overflow;

	private ToolStrip owner;

	internal ToolStripItem owner_item;

	private Padding padding;

	private ToolStripItemPlacement placement;

	private RightToLeft right_to_left;

	private bool right_to_left_auto_mirror_image;

	private object tag;

	private string text;

	private ContentAlignment text_align;

	private ToolStripTextDirection text_direction;

	private TextImageRelation text_image_relation;

	private string tool_tip_text;

	private bool visible;

	private EventHandler frame_handler;

	private ToolStrip parent;

	private Size text_size;

	private static object AvailableChangedEvent;

	private static object BackColorChangedEvent;

	private static object ClickEvent;

	private static object DisplayStyleChangedEvent;

	private static object DoubleClickEvent;

	private static object DragDropEvent;

	private static object DragEnterEvent;

	private static object DragLeaveEvent;

	private static object DragOverEvent;

	private static object EnabledChangedEvent;

	private static object ForeColorChangedEvent;

	private static object GiveFeedbackEvent;

	private static object LocationChangedEvent;

	private static object MouseDownEvent;

	private static object MouseEnterEvent;

	private static object MouseHoverEvent;

	private static object MouseLeaveEvent;

	private static object MouseMoveEvent;

	private static object MouseUpEvent;

	private static object OwnerChangedEvent;

	private static object PaintEvent;

	private static object QueryAccessibilityHelpEvent;

	private static object QueryContinueDragEvent;

	private static object RightToLeftChangedEvent;

	private static object TextChangedEvent;

	private static object VisibleChangedEvent;

	private static object UIASelectionChangedEvent;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control; if no <see cref="T:System.Windows.Forms.AccessibleObject" /> is currently assigned to the control, a new instance is created when this property is first accessed </returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public AccessibleObject AccessibilityObject
	{
		get
		{
			if (accessibility_object == null)
			{
				accessibility_object = CreateAccessibilityInstance();
			}
			return accessibility_object;
		}
	}

	/// <summary>Gets or sets the default action description of the control for use by accessibility client applications.</summary>
	/// <returns>The default action description of the control, for use by accessibility client applications.</returns>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string AccessibleDefaultActionDescription
	{
		get
		{
			if (accessibility_object == null)
			{
				return null;
			}
			return accessible_default_action_description;
		}
		set
		{
			accessible_default_action_description = value;
		}
	}

	/// <summary>Gets or sets the description that will be reported to accessibility client applications.</summary>
	/// <returns>The description of the control used by accessibility client applications. The default is null.</returns>
	/// <filterpriority>2</filterpriority>
	[Localizable(true)]
	[DefaultValue(null)]
	public string AccessibleDescription
	{
		get
		{
			if (accessibility_object == null)
			{
				return null;
			}
			return AccessibilityObject.Description;
		}
		set
		{
			AccessibilityObject.description = value;
		}
	}

	/// <summary>Gets or sets the name of the control for use by accessibility client applications.</summary>
	/// <returns>The name of the control, for use by accessibility client applications. The default is null.</returns>
	/// <filterpriority>2</filterpriority>
	[Localizable(true)]
	[DefaultValue(null)]
	public string AccessibleName
	{
		get
		{
			if (accessibility_object == null)
			{
				return null;
			}
			return AccessibilityObject.Name;
		}
		set
		{
			AccessibilityObject.Name = value;
		}
	}

	/// <summary>Gets or sets the accessible role of the control, which specifies the type of user interface element of the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleRole.PushButton" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. </exception>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(AccessibleRole.Default)]
	public AccessibleRole AccessibleRole
	{
		get
		{
			if (accessibility_object == null)
			{
				return AccessibleRole.Default;
			}
			return AccessibilityObject.Role;
		}
		set
		{
			AccessibilityObject.role = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the item aligns towards the beginning or end of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Left" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(ToolStripItemAlignment.Left)]
	public ToolStripItemAlignment Alignment
	{
		get
		{
			return alignment;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripItemAlignment), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripItemAlignment");
			}
			if (alignment != value)
			{
				alignment = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.</summary>
	/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <see cref="P:System.Windows.Forms.ToolStripItem.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to true. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoTODO("Stub, does nothing")]
	[Browsable(false)]
	[DefaultValue(false)]
	public virtual bool AllowDrop
	{
		get
		{
			return allow_drop;
		}
		set
		{
			allow_drop = value;
		}
	}

	/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStripItem" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" />  is resized with its parent.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
	public AnchorStyles Anchor
	{
		get
		{
			return anchor;
		}
		set
		{
			anchor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the item is automatically sized.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically sized; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	[Localizable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[RefreshProperties(RefreshProperties.All)]
	public bool AutoSize
	{
		get
		{
			return auto_size;
		}
		set
		{
			auto_size = value;
			CalculateAutoSize();
		}
	}

	/// <summary>Gets or sets a value indicating whether to use the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property or the <see cref="P:System.Windows.Forms.ToolStripItem.ToolTipText" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> ToolTip. </summary>
	/// <returns>true to use the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property for the ToolTip; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool AutoToolTip
	{
		get
		{
			return auto_tool_tip;
		}
		set
		{
			auto_tool_tip = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> should be placed on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is placed on a <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool Available
	{
		get
		{
			return available;
		}
		set
		{
			if (available != value)
			{
				available = value;
				visible = value;
				if (parent != null)
				{
					parent.PerformLayout();
				}
				OnAvailableChanged(EventArgs.Empty);
				OnVisibleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the background color for the item.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual Color BackColor
	{
		get
		{
			if (back_color != Color.Empty)
			{
				return back_color;
			}
			if (Parent != null)
			{
				return parent.BackColor;
			}
			return Control.DefaultBackColor;
		}
		set
		{
			if (back_color != value)
			{
				back_color = value;
				OnBackColorChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the background image displayed in the item.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the item.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(null)]
	public virtual Image BackgroundImage
	{
		get
		{
			return background_image;
		}
		set
		{
			if (background_image != value)
			{
				background_image = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the background image layout used for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default value is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(ImageLayout.Tile)]
	public virtual ImageLayout BackgroundImageLayout
	{
		get
		{
			return background_image_layout;
		}
		set
		{
			if (background_image_layout != value)
			{
				background_image_layout = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets the size and location of the item.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual Rectangle Bounds => bounds;

	/// <summary>Gets a value indicating whether the item can be selected.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual bool CanSelect => can_select;

	/// <summary>Gets the area where content, such as text and icons, can be placed within a <see cref="T:System.Windows.Forms.ToolStripItem" /> without overwriting background borders.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> containing four integers that represent the location and size of <see cref="T:System.Windows.Forms.ToolStripItem" /> contents, excluding its border.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Rectangle ContentRectangle
	{
		get
		{
			if (this is ToolStripLabel || this is ToolStripStatusLabel)
			{
				return new Rectangle(0, 0, bounds.Width, bounds.Height);
			}
			if (this is ToolStripDropDownButton && (this as ToolStripDropDownButton).ShowDropDownArrow)
			{
				return new Rectangle(2, 2, bounds.Width - 13, bounds.Height - 4);
			}
			return new Rectangle(2, 2, bounds.Width - 4, bounds.Height - 4);
		}
	}

	/// <summary>Gets or sets whether text and images are displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" /> .</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual ToolStripItemDisplayStyle DisplayStyle
	{
		get
		{
			return display_style;
		}
		set
		{
			if (display_style != value)
			{
				display_style = value;
				CalculateAutoSize();
				OnDisplayStyleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a value indicating whether the object has been disposed of.</summary>
	/// <returns>true if the control has been disposed of; otherwise, false.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	public bool IsDisposed => is_disposed;

	/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStripItem" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" /> is resized with its parent.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DefaultValue(DockStyle.None)]
	public DockStyle Dock
	{
		get
		{
			return dock;
		}
		set
		{
			if (dock != value)
			{
				if (!Enum.IsDefined(typeof(DockStyle), value))
				{
					throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for DockStyle");
				}
				dock = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be activated by double-clicking the mouse. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be activated by double-clicking the mouse; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool DoubleClickEnabled
	{
		get
		{
			return double_click_enabled;
		}
		set
		{
			double_click_enabled = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled. </summary>
	/// <returns>true if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(true)]
	public virtual bool Enabled
	{
		get
		{
			if (Parent != null && !Parent.Enabled)
			{
				return false;
			}
			if (Owner != null && !Owner.Enabled)
			{
				return false;
			}
			return enabled;
		}
		set
		{
			if (enabled != value)
			{
				enabled = value;
				OnEnabledChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the font of the text displayed by the item.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the <see cref="T:System.Windows.Forms.ToolStripItem" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public virtual Font Font
	{
		get
		{
			if (font != null)
			{
				return font;
			}
			if (Parent != null)
			{
				return Parent.Font;
			}
			return DefaultFont;
		}
		set
		{
			if (font != value)
			{
				font = value;
				CalculateAutoSize();
				OnFontChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the foreground color of the item.</summary>
	/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual Color ForeColor
	{
		get
		{
			if (fore_color != Color.Empty)
			{
				return fore_color;
			}
			if (Parent != null)
			{
				return parent.ForeColor;
			}
			return Control.DefaultForeColor;
		}
		set
		{
			if (fore_color != value)
			{
				fore_color = value;
				OnForeColorChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the height, in pixels, of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the height, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Height
	{
		get
		{
			return Size.Height;
		}
		set
		{
			Size = new Size(Size.Width, value);
			explicit_size.Height = value;
			if (Visible)
			{
				CalculateAutoSize();
				OnBoundsChanged();
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> to be displayed.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public virtual Image Image
	{
		get
		{
			if (image != null)
			{
				return image;
			}
			if (image_index >= 0 && owner != null && owner.ImageList != null && owner.ImageList.Images.Count > image_index)
			{
				return owner.ImageList.Images[image_index];
			}
			if (!string.IsNullOrEmpty(image_key) && owner != null && owner.ImageList != null && owner.ImageList.Images.Count > image_index)
			{
				return owner.ImageList.Images[image_key];
			}
			return null;
		}
		set
		{
			if (image != value)
			{
				StopAnimation();
				image = value;
				image_index = -1;
				image_key = string.Empty;
				CalculateAutoSize();
				Invalidate();
				BeginAnimation();
			}
		}
	}

	/// <summary>Gets or sets the alignment of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ContentAlignment.MiddleCenter)]
	public ContentAlignment ImageAlign
	{
		get
		{
			return image_align;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ContentAlignment), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ContentAlignment");
			}
			if (image_align != value)
			{
				image_align = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets or sets the index value of the image that is displayed on the item.</summary>
	/// <returns>The zero-based index of the image in the <see cref="P:System.Windows.Forms.ToolStrip.ImageList" /> that is displayed for the item. The default is -1, signifying that the image list is empty.</returns>
	/// <exception cref="T:System.ArgumentException">The value specified is less than -1. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[Localizable(true)]
	[Browsable(false)]
	[RelatedImageList("Owner.ImageList")]
	[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
	[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public int ImageIndex
	{
		get
		{
			return image_index;
		}
		set
		{
			if (image_index != value)
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex cannot be less than -1");
				}
				image_index = value;
				image = null;
				image_key = string.Empty;
				CalculateAutoSize();
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.ToolStrip.ImageList" /> that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A string representing the key of the image.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Browsable(false)]
	[RelatedImageList("Owner.ImageList")]
	[TypeConverter(typeof(ImageKeyConverter))]
	[Localizable(true)]
	public string ImageKey
	{
		get
		{
			return image_key;
		}
		set
		{
			if (image_key != value)
			{
				image = null;
				image_index = -1;
				image_key = value;
				CalculateAutoSize();
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether an image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically resized to fit in a container.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemImageScaling.SizeToFit" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ToolStripItemImageScaling.SizeToFit)]
	public ToolStripItemImageScaling ImageScaling
	{
		get
		{
			return image_scaling;
		}
		set
		{
			if (image_scaling != value)
			{
				image_scaling = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets or sets the color to treat as transparent in a <see cref="T:System.Windows.Forms.ToolStripItem" /> image.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.Color" /> values.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public Color ImageTransparentColor
	{
		get
		{
			return image_transparent_color;
		}
		set
		{
			image_transparent_color = value;
		}
	}

	/// <summary>Gets a value indicating whether the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />. </summary>
	/// <returns>true if the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool IsOnDropDown
	{
		get
		{
			if (parent != null && parent is ToolStripDropDown)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.ToolStripItem.Placement" /> property is set to <see cref="F:System.Windows.Forms.ToolStripItemPlacement.Overflow" />.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.ToolStripItem.Placement" /> property is set to <see cref="F:System.Windows.Forms.ToolStripItemPlacement.Overflow" />; otherwise, false.</returns>
	[Browsable(false)]
	public bool IsOnOverflow => placement == ToolStripItemPlacement.Overflow;

	/// <summary>Gets or sets the space between the item and adjacent items.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between the item and adjacent items.</returns>
	/// <filterpriority>1</filterpriority>
	public Padding Margin
	{
		get
		{
			return margin;
		}
		set
		{
			margin = value;
			CalculateAutoSize();
		}
	}

	/// <summary>Gets or sets how child menus are merged with parent menus. </summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.MergeAction" /> values. The default is <see cref="F:System.Windows.Forms.MergeAction.MatchOnly" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.MergeAction" /> values.</exception>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(MergeAction.Append)]
	public MergeAction MergeAction
	{
		get
		{
			return merge_action;
		}
		set
		{
			if (!Enum.IsDefined(typeof(MergeAction), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for MergeAction");
			}
			merge_action = value;
		}
	}

	/// <summary>Gets or sets the position of a merged item within the current <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>An integer representing the index of the merged item, if a match is found, or -1 if a match is not found.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(-1)]
	public int MergeIndex
	{
		get
		{
			return merge_index;
		}
		set
		{
			merge_index = value;
		}
	}

	/// <summary>Gets or sets the name of the item.</summary>
	/// <returns>A string representing the name. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Browsable(false)]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Gets or sets whether the item is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or can float between the two.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemOverflow.AsNeeded" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(ToolStripItemOverflow.AsNeeded)]
	public ToolStripItemOverflow Overflow
	{
		get
		{
			return overflow;
		}
		set
		{
			if (overflow != value)
			{
				if (!Enum.IsDefined(typeof(ToolStripItemOverflow), value))
				{
					throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripItemOverflow");
				}
				overflow = value;
				if (owner != null)
				{
					owner.PerformLayout();
				}
			}
		}
	}

	/// <summary>Gets or sets the owner of this item.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that owns or is to own the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ToolStrip Owner
	{
		get
		{
			return owner;
		}
		set
		{
			if (owner != value)
			{
				if (owner != null)
				{
					owner.Items.Remove(this);
				}
				if (value != null)
				{
					value.Items.Add(this);
				}
				else
				{
					owner = null;
				}
			}
		}
	}

	/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>The parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ToolStripItem OwnerItem => owner_item;

	/// <summary>Gets or sets the internal spacing, in pixels, between the item's contents and its edges.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the item's internal spacing, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual Padding Padding
	{
		get
		{
			return padding;
		}
		set
		{
			padding = value;
			CalculateAutoSize();
			Invalidate();
		}
	}

	/// <summary>Gets the current layout of the item.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemPlacement" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ToolStripItemPlacement Placement => placement;

	/// <summary>Gets a value indicating whether the state of the item is pressed. </summary>
	/// <returns>true if the state of the item is pressed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool Pressed => is_pressed;

	/// <summary>Gets or sets a value indicating whether items are to be placed from right to left and text is to be written from right to left.</summary>
	/// <returns>true if items are to be placed from right to left and text is to be written from right to left; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("RTL not implemented")]
	[Localizable(true)]
	public virtual RightToLeft RightToLeft
	{
		get
		{
			return right_to_left;
		}
		set
		{
			if (right_to_left != value)
			{
				right_to_left = value;
				OnRightToLeftChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Mirrors automatically the <see cref="T:System.Windows.Forms.ToolStripItem" /> image when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />.</summary>
	/// <returns>true to automatically mirror the image; otherwise, false. The default is false.</returns>
	[Localizable(true)]
	[DefaultValue(false)]
	public bool RightToLeftAutoMirrorImage
	{
		get
		{
			return right_to_left_auto_mirror_image;
		}
		set
		{
			if (right_to_left_auto_mirror_image != value)
			{
				right_to_left_auto_mirror_image = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets a value indicating whether the item is selected.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool Selected => is_selected;

	/// <summary>Gets or sets the size of the item.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public virtual Size Size
	{
		get
		{
			if (!AutoSize && explicit_size != Size.Empty)
			{
				return explicit_size;
			}
			return bounds.Size;
		}
		set
		{
			bounds.Size = value;
			explicit_size = value;
			if (Visible)
			{
				CalculateAutoSize();
				OnBoundsChanged();
			}
		}
	}

	/// <summary>Gets or sets the object that contains data about the item.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(false)]
	[Bindable(true)]
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
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

	/// <summary>Gets or sets the text that is to be displayed on the item.</summary>
	/// <returns>A string representing the item's text. The default value is the empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (text != value)
			{
				text = value;
				Invalidate();
				CalculateAutoSize();
				Invalidate();
				OnTextChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the alignment of the text on a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleRight" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ContentAlignment.MiddleCenter)]
	[Localizable(true)]
	public virtual ContentAlignment TextAlign
	{
		get
		{
			return text_align;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ContentAlignment), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ContentAlignment");
			}
			if (text_align != value)
			{
				text_align = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets the orientation of text used on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual ToolStripTextDirection TextDirection
	{
		get
		{
			if (text_direction == ToolStripTextDirection.Inherit)
			{
				if (Parent != null)
				{
					return Parent.TextDirection;
				}
				return ToolStripTextDirection.Horizontal;
			}
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
				CalculateAutoSize();
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the position of <see cref="T:System.Windows.Forms.ToolStripItem" /> text and image relative to each other.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values. The default is <see cref="F:System.Windows.Forms.TextImageRelation.ImageBeforeText" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(TextImageRelation.ImageBeforeText)]
	public TextImageRelation TextImageRelation
	{
		get
		{
			return text_image_relation;
		}
		set
		{
			text_image_relation = value;
			CalculateAutoSize();
			Invalidate();
		}
	}

	/// <summary>Gets or sets the text that appears as a <see cref="T:System.Windows.Forms.ToolTip" /> for a control.</summary>
	/// <returns>A string representing the ToolTip text.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ToolTipText
	{
		get
		{
			return tool_tip_text;
		}
		set
		{
			tool_tip_text = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the item is displayed.</summary>
	/// <returns>true if the item is displayed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public bool Visible
	{
		get
		{
			if (parent == null)
			{
				return false;
			}
			return visible && parent.Visible;
		}
		set
		{
			if (visible != value)
			{
				available = value;
				SetVisibleCore(value);
				if (Owner != null)
				{
					Owner.PerformLayout();
				}
			}
		}
	}

	/// <summary>Gets or sets the width in pixels of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public int Width
	{
		get
		{
			return Size.Width;
		}
		set
		{
			Size = new Size(value, Size.Height);
			explicit_size.Width = value;
			if (Visible)
			{
				CalculateAutoSize();
				OnBoundsChanged();
				Invalidate();
			}
		}
	}

	/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
	/// <returns>false in all cases.</returns>
	protected virtual bool DefaultAutoToolTip => false;

	/// <summary>Gets a value indicating what is displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" />.</returns>
	protected virtual ToolStripItemDisplayStyle DefaultDisplayStyle => ToolStripItemDisplayStyle.ImageAndText;

	/// <summary>Gets the default margin of an item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
	protected internal virtual Padding DefaultMargin => new Padding(0, 1, 0, 2);

	/// <summary>Gets the internal spacing characteristics of the item.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
	protected virtual Padding DefaultPadding => default(Padding);

	/// <summary>Gets the default size of the item.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	protected virtual Size DefaultSize => new Size(23, 23);

	/// <summary>Gets a value indicating whether items on a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> are hidden after they are clicked.</summary>
	/// <returns>true if the item is hidden after it is clicked; otherwise, false.</returns>
	protected internal virtual bool DismissWhenClicked => true;

	/// <summary>Gets or sets the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected internal ToolStrip Parent
	{
		get
		{
			return parent;
		}
		set
		{
			if (parent != value)
			{
				ToolStrip oldParent = parent;
				parent = value;
				OnParentChanged(oldParent, parent);
			}
		}
	}

	/// <summary>Gets a value indicating whether to show or hide shortcut keys.</summary>
	/// <returns>true to show shortcut keys; otherwise, false. The default is true.</returns>
	protected internal virtual bool ShowKeyboardCues => false;

	private static Font DefaultFont => new Font("Tahoma", 8.25f);

	internal virtual ToolStripTextDirection DefaultTextDirection => ToolStripTextDirection.Inherit;

	internal bool ShowMargin
	{
		get
		{
			if (!IsOnDropDown)
			{
				return true;
			}
			if (!(Owner is ToolStripDropDownMenu))
			{
				return false;
			}
			ToolStripDropDownMenu toolStripDropDownMenu = (ToolStripDropDownMenu)Owner;
			return toolStripDropDownMenu.ShowCheckMargin || toolStripDropDownMenu.ShowImageMargin;
		}
	}

	internal bool UseImageMargin
	{
		get
		{
			if (!IsOnDropDown)
			{
				return true;
			}
			if (!(Owner is ToolStripDropDownMenu))
			{
				return false;
			}
			ToolStripDropDownMenu toolStripDropDownMenu = (ToolStripDropDownMenu)Owner;
			return toolStripDropDownMenu.ShowImageMargin || toolStripDropDownMenu.ShowCheckMargin;
		}
	}

	internal virtual bool InternalVisible
	{
		get
		{
			return visible;
		}
		set
		{
			visible = value;
			Invalidate();
		}
	}

	internal ToolStrip InternalOwner
	{
		set
		{
			if (owner != value)
			{
				owner = value;
				CalculateAutoSize();
				OnOwnerChanged(EventArgs.Empty);
			}
		}
	}

	internal Point Location
	{
		get
		{
			return bounds.Location;
		}
		set
		{
			if (bounds.Location != value)
			{
				bounds.Location = value;
				OnLocationChanged(EventArgs.Empty);
			}
		}
	}

	internal int Top
	{
		get
		{
			return bounds.Y;
		}
		set
		{
			if (bounds.Y != value)
			{
				bounds.Y = value;
				OnLocationChanged(EventArgs.Empty);
			}
		}
	}

	internal int Left
	{
		get
		{
			return bounds.X;
		}
		set
		{
			if (bounds.X != value)
			{
				bounds.X = value;
				OnLocationChanged(EventArgs.Empty);
			}
		}
	}

	internal int Right => bounds.Right;

	internal int Bottom => bounds.Bottom;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Available" /> property changes.</summary>
	[Browsable(false)]
	public event EventHandler AvailableChanged
	{
		add
		{
			base.Events.AddHandler(AvailableChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AvailableChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.BackColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BackColorChanged
	{
		add
		{
			base.Events.AddHandler(BackColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BackColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.DisplayStyle" /> has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DisplayStyleChanged
	{
		add
		{
			base.Events.AddHandler(DisplayStyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DisplayStyleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the item is double-clicked with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DoubleClick
	{
		add
		{
			base.Events.AddHandler(DoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DoubleClickEvent, value);
		}
	}

	/// <summary>Occurs when the user drags an item and the user releases the mouse button, indicating that the item should be dropped into this item.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[System.MonoTODO("Event never raised")]
	public event DragEventHandler DragDrop
	{
		add
		{
			base.Events.AddHandler(DragDropEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragDropEvent, value);
		}
	}

	/// <summary>Occurs when the user drags an item into the client area of this item.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[System.MonoTODO("Event never raised")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event DragEventHandler DragEnter
	{
		add
		{
			base.Events.AddHandler(DragEnterEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragEnterEvent, value);
		}
	}

	/// <summary>Occurs when the user drags an item and the mouse pointer is no longer over the client area of this item.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoTODO("Event never raised")]
	[Browsable(false)]
	public event EventHandler DragLeave
	{
		add
		{
			base.Events.AddHandler(DragLeaveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragLeaveEvent, value);
		}
	}

	/// <summary>Occurs when the user drags an item over the client area of this item.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoTODO("Event never raised")]
	[Browsable(false)]
	public event DragEventHandler DragOver
	{
		add
		{
			base.Events.AddHandler(DragOverEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DragOverEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler EnabledChanged
	{
		add
		{
			base.Events.AddHandler(EnabledChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(EnabledChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.ForeColor" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ForeColorChanged
	{
		add
		{
			base.Events.AddHandler(ForeColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ForeColorChangedEvent, value);
		}
	}

	/// <summary>Occurs during a drag operation.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoTODO("Event never raised")]
	[Browsable(false)]
	public event GiveFeedbackEventHandler GiveFeedback
	{
		add
		{
			base.Events.AddHandler(GiveFeedbackEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(GiveFeedbackEvent, value);
		}
	}

	/// <summary>Occurs when the location of a <see cref="T:System.Windows.Forms.ToolStripItem" /> is updated.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler LocationChanged
	{
		add
		{
			base.Events.AddHandler(LocationChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LocationChangedEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer is over the item and a mouse button is pressed.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseDown
	{
		add
		{
			base.Events.AddHandler(MouseDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseDownEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer enters the item.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseEnter
	{
		add
		{
			base.Events.AddHandler(MouseEnterEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseEnterEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer hovers over the item.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseHover
	{
		add
		{
			base.Events.AddHandler(MouseHoverEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseHoverEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer leaves the item.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MouseLeave
	{
		add
		{
			base.Events.AddHandler(MouseLeaveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseLeaveEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer is moved over the item.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseMove
	{
		add
		{
			base.Events.AddHandler(MouseMoveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseMoveEvent, value);
		}
	}

	/// <summary>Occurs when the mouse pointer is over the item and a mouse button is released.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseUp
	{
		add
		{
			base.Events.AddHandler(MouseUpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseUpEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property changes. </summary>
	public event EventHandler OwnerChanged
	{
		add
		{
			base.Events.AddHandler(OwnerChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(OwnerChangedEvent, value);
		}
	}

	/// <summary>Occurs when the item is redrawn.</summary>
	/// <filterpriority>1</filterpriority>
	public event PaintEventHandler Paint
	{
		add
		{
			base.Events.AddHandler(PaintEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PaintEvent, value);
		}
	}

	/// <summary>Occurs when an accessibility client application invokes help for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Event never raised")]
	public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
	{
		add
		{
			base.Events.AddHandler(QueryAccessibilityHelpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(QueryAccessibilityHelpEvent, value);
		}
	}

	/// <summary>Occurs during a drag-and-drop operation and allows the drag source to determine whether the drag-and-drop operation should be canceled.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoTODO("Event never raised")]
	[Browsable(false)]
	public event QueryContinueDragEventHandler QueryContinueDrag
	{
		add
		{
			base.Events.AddHandler(QueryContinueDragEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(QueryContinueDragEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler RightToLeftChanged
	{
		add
		{
			base.Events.AddHandler(RightToLeftChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RightToLeftChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TextChanged
	{
		add
		{
			base.Events.AddHandler(TextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Visible" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler VisibleChanged
	{
		add
		{
			base.Events.AddHandler(VisibleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(VisibleChangedEvent, value);
		}
	}

	internal event EventHandler UIASelectionChanged
	{
		add
		{
			base.Events.AddHandler(UIASelectionChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIASelectionChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class.</summary>
	protected ToolStripItem()
		: this(string.Empty, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified name, image, and event handler.</summary>
	/// <param name="text">A <see cref="T:System.String" /> representing the name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	protected ToolStripItem(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified display text, image, event handler, and name. </summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="image">The Image to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
	protected ToolStripItem(string text, Image image, EventHandler onClick, string name)
	{
		alignment = ToolStripItemAlignment.Left;
		anchor = AnchorStyles.Top | AnchorStyles.Left;
		auto_size = true;
		auto_tool_tip = DefaultAutoToolTip;
		available = true;
		back_color = Color.Empty;
		background_image_layout = ImageLayout.Tile;
		can_select = true;
		display_style = DefaultDisplayStyle;
		dock = DockStyle.None;
		enabled = true;
		fore_color = Color.Empty;
		this.image = image;
		image_align = ContentAlignment.MiddleCenter;
		image_index = -1;
		image_key = string.Empty;
		image_scaling = ToolStripItemImageScaling.SizeToFit;
		image_transparent_color = Color.Empty;
		margin = DefaultMargin;
		merge_action = MergeAction.Append;
		merge_index = -1;
		this.name = name;
		overflow = ToolStripItemOverflow.AsNeeded;
		padding = DefaultPadding;
		placement = ToolStripItemPlacement.None;
		right_to_left = RightToLeft.Inherit;
		bounds.Size = DefaultSize;
		this.text = text;
		text_align = ContentAlignment.MiddleCenter;
		text_direction = DefaultTextDirection;
		text_image_relation = TextImageRelation.ImageBeforeText;
		visible = true;
		Click += onClick;
		OnLayout(new LayoutEventArgs(null, string.Empty));
	}

	static ToolStripItem()
	{
		AvailableChanged = new object();
		BackColorChanged = new object();
		Click = new object();
		DisplayStyleChanged = new object();
		DoubleClick = new object();
		DragDrop = new object();
		DragEnter = new object();
		DragLeave = new object();
		DragOver = new object();
		EnabledChanged = new object();
		ForeColorChanged = new object();
		GiveFeedback = new object();
		LocationChanged = new object();
		MouseDown = new object();
		MouseEnter = new object();
		MouseHover = new object();
		MouseLeave = new object();
		MouseMove = new object();
		MouseUp = new object();
		OwnerChanged = new object();
		Paint = new object();
		QueryAccessibilityHelp = new object();
		QueryContinueDrag = new object();
		RightToLeftChanged = new object();
		TextChanged = new object();
		VisibleChanged = new object();
		UIASelectionChanged = new object();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragDrop" /> event.</summary>
	/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	void IDropTarget.OnDragDrop(DragEventArgs dragEvent)
	{
		OnDragDrop(dragEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragEnter" /> event.</summary>
	/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
	void IDropTarget.OnDragEnter(DragEventArgs dragEvent)
	{
		OnDragEnter(dragEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	void IDropTarget.OnDragLeave(EventArgs e)
	{
		OnDragLeave(e);
	}

	/// <summary>Raises the DragOver event.</summary>
	/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
	void IDropTarget.OnDragOver(DragEventArgs dragEvent)
	{
		OnDragOver(dragEvent);
	}

	/// <summary>Begins a drag-and-drop operation.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
	/// <param name="data">The object to be dragged. </param>
	/// <param name="allowedEffects">The drag operations that can occur. </param>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Stub, does nothing")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
	{
		return allowedEffects;
	}

	/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStrip GetCurrentParent()
	{
		return parent;
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fit.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> ordered pair, representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual Size GetPreferredSize(Size constrainingSize)
	{
		return CalculatePreferredSize(constrainingSize);
	}

	/// <summary>Invalidates the entire surface of the <see cref="T:System.Windows.Forms.ToolStripItem" /> and causes it to be redrawn.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate()
	{
		if (parent != null)
		{
			parent.Invalidate(bounds);
		}
	}

	/// <summary>Invalidates the specified region of the <see cref="T:System.Windows.Forms.ToolStripItem" /> by adding it to the update region of the <see cref="T:System.Windows.Forms.ToolStripItem" />, which is the area that will be repainted at the next paint operation, and causes a paint message to be sent to the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Invalidate(Rectangle r)
	{
		if (parent != null)
		{
			parent.Invalidate(r);
		}
	}

	/// <summary>Activates the <see cref="T:System.Windows.Forms.ToolStripItem" /> when it is clicked with the mouse.</summary>
	public void PerformClick()
	{
		OnClick(EventArgs.Empty);
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetBackColor()
	{
		BackColor = Color.Empty;
	}

	/// <summary>This method is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetDisplayStyle()
	{
		display_style = DefaultDisplayStyle;
	}

	/// <summary>This method is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetFont()
	{
		font = null;
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetForeColor()
	{
		ForeColor = Color.Empty;
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetImage()
	{
		image = null;
	}

	/// <summary>This method is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void ResetMargin()
	{
		margin = DefaultMargin;
	}

	/// <summary>This method is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void ResetPadding()
	{
		padding = DefaultPadding;
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetRightToLeft()
	{
		right_to_left = RightToLeft.Inherit;
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetTextDirection()
	{
		TextDirection = DefaultTextDirection;
	}

	/// <summary>Selects the item.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Select()
	{
		if (is_selected || !CanSelect)
		{
			return;
		}
		is_selected = true;
		if (Parent != null)
		{
			if (Visible && Parent.Focused && this is ToolStripControlHost)
			{
				(this as ToolStripControlHost).Focus();
			}
			Invalidate();
			Parent.NotifySelectedChanged(this);
		}
		OnUIASelectionChanged();
	}

	/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return text;
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripItemAccessibleObject(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripItem" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		if (!is_disposed && disposing)
		{
			is_disposed = true;
		}
		if (image != null)
		{
			StopAnimation();
			image = null;
		}
		if (owner != null)
		{
			owner.Items.Remove(this);
		}
		base.Dispose(disposing);
	}

	/// <summary>Determines whether a character is an input character that the item recognizes.</summary>
	/// <returns>true if the character should be sent directly to the item and not preprocessed; otherwise, false.</returns>
	/// <param name="charCode">The character to test. </param>
	protected internal virtual bool IsInputChar(char charCode)
	{
		return false;
	}

	/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
	/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
	protected internal virtual bool IsInputKey(Keys keyData)
	{
		return false;
	}

	/// <summary>Raises the AvailableChanged event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAvailableChanged(EventArgs e)
	{
		((EventHandler)base.Events[AvailableChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.BackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnBackColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[BackColorChanged])?.Invoke(this, e);
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
	protected virtual void OnBoundsChanged()
	{
		OnLayout(new LayoutEventArgs(null, string.Empty));
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(EventArgs e)
	{
		((EventHandler)base.Events[Click])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DisplayStyleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDisplayStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[DisplayStyleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DoubleClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnDoubleClick(EventArgs e)
	{
		((EventHandler)base.Events[DoubleClick])?.Invoke(this, e);
		if (!double_click_enabled)
		{
			OnClick(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragDrop" /> event.</summary>
	/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragDrop(DragEventArgs dragEvent)
	{
		((DragEventHandler)base.Events[DragDrop])?.Invoke(this, dragEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragEnter" /> event.</summary>
	/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragEnter(DragEventArgs dragEvent)
	{
		((DragEventHandler)base.Events[DragEnter])?.Invoke(this, dragEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragLeave(EventArgs e)
	{
		((EventHandler)base.Events[DragLeave])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragOver" /> event.</summary>
	/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnDragOver(DragEventArgs dragEvent)
	{
		((DragEventHandler)base.Events[DragOver])?.Invoke(this, dragEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnEnabledChanged(EventArgs e)
	{
		((EventHandler)base.Events[EnabledChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnFontChanged(EventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.ForeColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnForeColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[ForeColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.GiveFeedback" /> event.</summary>
	/// <param name="giveFeedbackEvent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent)
	{
		((GiveFeedbackEventHandler)base.Events[GiveFeedback])?.Invoke(this, giveFeedbackEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	protected virtual void OnLayout(LayoutEventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.LocationChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnLocationChanged(EventArgs e)
	{
		((EventHandler)base.Events[LocationChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		if (Enabled)
		{
			is_pressed = true;
			Invalidate();
			((MouseEventHandler)base.Events[MouseDown])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseEnter(EventArgs e)
	{
		Select();
		((EventHandler)base.Events[MouseEnter])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseHover" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseHover(EventArgs e)
	{
		if (Enabled)
		{
			((EventHandler)base.Events[MouseHover])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseLeave(EventArgs e)
	{
		if (CanSelect)
		{
			is_selected = false;
			is_pressed = false;
			Invalidate();
			OnUIASelectionChanged();
		}
		((EventHandler)base.Events[MouseLeave])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseMove" /> event.</summary>
	/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseMove(MouseEventArgs mea)
	{
		if (Enabled)
		{
			((MouseEventHandler)base.Events[MouseMove])?.Invoke(this, mea);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		if (!Enabled)
		{
			return;
		}
		is_pressed = false;
		Invalidate();
		if (IsOnDropDown && (!(this is ToolStripDropDownItem) || !(this as ToolStripDropDownItem).HasDropDownItems || !(this as ToolStripDropDownItem).DropDown.Visible))
		{
			if ((Parent as ToolStripDropDown).OwnerItem != null)
			{
				((Parent as ToolStripDropDown).OwnerItem as ToolStripDropDownItem).HideDropDown();
			}
			else
			{
				(Parent as ToolStripDropDown).Hide();
			}
		}
		((MouseEventHandler)base.Events[MouseUp])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnOwnerChanged(EventArgs e)
	{
		((EventHandler)base.Events[OwnerChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Font" /> property has changed on the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual void OnOwnerFontChanged(EventArgs e)
	{
		CalculateAutoSize();
		OnFontChanged(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected virtual void OnPaint(PaintEventArgs e)
	{
		if (parent != null)
		{
			parent.Renderer.DrawItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
		}
		((PaintEventHandler)base.Events[Paint])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.BackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentBackColorChanged(EventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
	/// <param name="oldParent">The original parent of the item. </param>
	/// <param name="newParent">The new parent of the item. </param>
	protected virtual void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
	{
		text_size = TextRenderer.MeasureText((Text != null) ? text : string.Empty, Font, Size.Empty, TextFormatFlags.HidePrefix);
		oldParent?.PerformLayout();
		newParent?.PerformLayout();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value of the item's container changes.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnParentEnabledChanged(EventArgs e)
	{
		OnEnabledChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.ForeColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnParentForeColorChanged(EventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual void OnParentRightToLeftChanged(EventArgs e)
	{
		OnRightToLeftChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.QueryContinueDrag" /> event.</summary>
	/// <param name="queryContinueDragEvent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEvent)
	{
		((QueryContinueDragEventHandler)base.Events[QueryContinueDrag])?.Invoke(this, queryContinueDragEvent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnRightToLeftChanged(EventArgs e)
	{
		((EventHandler)base.Events[RightToLeftChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.TextChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnTextChanged(EventArgs e)
	{
		((EventHandler)base.Events[TextChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnVisibleChanged(EventArgs e)
	{
		((EventHandler)base.Events[VisibleChanged])?.Invoke(this, e);
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>false in all cases.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		return false;
	}

	/// <summary>Processes a dialog key.</summary>
	/// <returns>true if the key was processed by the item; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal virtual bool ProcessDialogKey(Keys keyData)
	{
		if (Selected && keyData == Keys.Return)
		{
			FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
			return true;
		}
		return false;
	}

	/// <summary>Processes a mnemonic character.</summary>
	/// <returns>true in all cases.</returns>
	/// <param name="charCode">The character to process. </param>
	protected internal virtual bool ProcessMnemonic(char charCode)
	{
		ToolStripManager.SetActiveToolStrip(Parent, keyboard: true);
		PerformClick();
		return true;
	}

	/// <summary>Sets the size and location of the item.</summary>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" /></param>
	protected internal virtual void SetBounds(Rectangle bounds)
	{
		if (this.bounds != bounds)
		{
			this.bounds = bounds;
			OnBoundsChanged();
		}
	}

	/// <summary>Sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visible state. </summary>
	/// <param name="visible">true to make the <see cref="T:System.Windows.Forms.ToolStripItem" /> visible; otherwise, false.</param>
	protected virtual void SetVisibleCore(bool visible)
	{
		this.visible = visible;
		OnVisibleChanged(EventArgs.Empty);
		if (this.visible)
		{
			BeginAnimation();
		}
		else
		{
			StopAnimation();
		}
		Invalidate();
	}

	internal Rectangle AlignInRectangle(Rectangle outer, Size inner, ContentAlignment align)
	{
		int x = 0;
		int y = 0;
		switch (align)
		{
		case ContentAlignment.TopLeft:
		case ContentAlignment.MiddleLeft:
		case ContentAlignment.BottomLeft:
			x = outer.X;
			break;
		case ContentAlignment.TopCenter:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.BottomCenter:
			x = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			break;
		case ContentAlignment.TopRight:
		case ContentAlignment.MiddleRight:
		case ContentAlignment.BottomRight:
			x = outer.Right - inner.Width;
			break;
		}
		switch (align)
		{
		case ContentAlignment.TopLeft:
		case ContentAlignment.TopCenter:
		case ContentAlignment.TopRight:
			y = outer.Y;
			break;
		case ContentAlignment.MiddleLeft:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.MiddleRight:
			y = outer.Y + (outer.Height - inner.Height) / 2;
			break;
		case ContentAlignment.BottomLeft:
		case ContentAlignment.BottomCenter:
		case ContentAlignment.BottomRight:
			y = outer.Bottom - inner.Height;
			break;
		}
		return new Rectangle(x, y, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
	}

	internal void CalculateAutoSize()
	{
		text_size = TextRenderer.MeasureText((Text != null) ? text : string.Empty, Font, Size.Empty, TextFormatFlags.HidePrefix);
		ToolStripTextDirection textDirection = TextDirection;
		if (textDirection == ToolStripTextDirection.Vertical270 || textDirection == ToolStripTextDirection.Vertical90)
		{
			text_size = new Size(text_size.Height, text_size.Width);
		}
		if (!auto_size || this is ToolStripControlHost)
		{
			return;
		}
		Size size = CalculatePreferredSize(Size.Empty);
		if (size != Size)
		{
			bounds.Width = size.Width;
			if (parent != null)
			{
				parent.PerformLayout();
			}
		}
	}

	internal virtual Size CalculatePreferredSize(Size constrainingSize)
	{
		if (!auto_size)
		{
			return explicit_size;
		}
		Size result = DefaultSize;
		switch (display_style)
		{
		case ToolStripItemDisplayStyle.Text:
		{
			int width = text_size.Width + padding.Horizontal;
			int height = text_size.Height + padding.Vertical;
			result = new Size(width, height);
			break;
		}
		case ToolStripItemDisplayStyle.Image:
			if (GetImageSize() == Size.Empty)
			{
				result = DefaultSize;
				break;
			}
			switch (image_scaling)
			{
			case ToolStripItemImageScaling.None:
				result = GetImageSize();
				break;
			case ToolStripItemImageScaling.SizeToFit:
				result = ((parent != null) ? parent.ImageScalingSize : GetImageSize());
				break;
			}
			break;
		case ToolStripItemDisplayStyle.ImageAndText:
		{
			int num = text_size.Width + padding.Horizontal;
			int num2 = text_size.Height + padding.Vertical;
			if (GetImageSize() != Size.Empty)
			{
				Size size = GetImageSize();
				if (image_scaling == ToolStripItemImageScaling.SizeToFit && parent != null)
				{
					size = parent.ImageScalingSize;
				}
				switch (text_image_relation)
				{
				case TextImageRelation.Overlay:
					num = Math.Max(num, size.Width);
					num2 = Math.Max(num2, size.Height);
					break;
				case TextImageRelation.ImageAboveText:
				case TextImageRelation.TextAboveImage:
					num = Math.Max(num, size.Width);
					num2 += size.Height;
					break;
				case TextImageRelation.ImageBeforeText:
				case TextImageRelation.TextBeforeImage:
					num2 = Math.Max(num2, size.Height);
					num += size.Width;
					break;
				}
			}
			result = new Size(num, num2);
			break;
		}
		}
		if (!(this is ToolStripLabel))
		{
			result.Height += 4;
			result.Width += 4;
		}
		return result;
	}

	internal void CalculateTextAndImageRectangles(out Rectangle text_rect, out Rectangle image_rect)
	{
		CalculateTextAndImageRectangles(ContentRectangle, out text_rect, out image_rect);
	}

	internal void CalculateTextAndImageRectangles(Rectangle contentRectangle, out Rectangle text_rect, out Rectangle image_rect)
	{
		text_rect = Rectangle.Empty;
		image_rect = Rectangle.Empty;
		switch (display_style)
		{
		case ToolStripItemDisplayStyle.None:
			break;
		case ToolStripItemDisplayStyle.Text:
			if (text != string.Empty)
			{
				text_rect = AlignInRectangle(contentRectangle, text_size, text_align);
			}
			break;
		case ToolStripItemDisplayStyle.Image:
			if (Image != null && UseImageMargin)
			{
				image_rect = AlignInRectangle(contentRectangle, GetImageSize(), image_align);
			}
			break;
		case ToolStripItemDisplayStyle.ImageAndText:
			if (text != string.Empty && (Image == null || !UseImageMargin))
			{
				text_rect = AlignInRectangle(contentRectangle, text_size, text_align);
			}
			else
			{
				if (text == string.Empty && (Image == null || !UseImageMargin))
				{
					break;
				}
				if (text == string.Empty && Image != null)
				{
					image_rect = AlignInRectangle(contentRectangle, GetImageSize(), image_align);
					break;
				}
				switch (text_image_relation)
				{
				case TextImageRelation.Overlay:
					text_rect = AlignInRectangle(contentRectangle, text_size, text_align);
					image_rect = AlignInRectangle(contentRectangle, GetImageSize(), image_align);
					break;
				case TextImageRelation.ImageAboveText:
				{
					Rectangle outer = new Rectangle(contentRectangle.Left, contentRectangle.Bottom - (text_size.Height - 4), contentRectangle.Width, text_size.Height - 4);
					Rectangle outer2 = new Rectangle(contentRectangle.Left, contentRectangle.Top, contentRectangle.Width, contentRectangle.Height - outer.Height);
					text_rect = AlignInRectangle(outer, text_size, text_align);
					image_rect = AlignInRectangle(outer2, GetImageSize(), image_align);
					break;
				}
				case TextImageRelation.TextAboveImage:
				{
					Rectangle outer = new Rectangle(contentRectangle.Left, contentRectangle.Top, contentRectangle.Width, text_size.Height - 4);
					Rectangle outer2 = new Rectangle(contentRectangle.Left, outer.Bottom, contentRectangle.Width, contentRectangle.Height - outer.Height);
					text_rect = AlignInRectangle(outer, text_size, text_align);
					image_rect = AlignInRectangle(outer2, GetImageSize(), image_align);
					break;
				}
				case TextImageRelation.ImageBeforeText:
					LayoutTextBeforeOrAfterImage(contentRectangle, textFirst: false, text_size, GetImageSize(), text_align, image_align, out text_rect, out image_rect);
					break;
				case TextImageRelation.TextBeforeImage:
					LayoutTextBeforeOrAfterImage(contentRectangle, textFirst: true, text_size, GetImageSize(), text_align, image_align, out text_rect, out image_rect);
					break;
				case (TextImageRelation)3:
				case (TextImageRelation)5:
				case (TextImageRelation)6:
				case (TextImageRelation)7:
					break;
				}
			}
			break;
		}
	}

	internal virtual void Dismiss(ToolStripDropDownCloseReason reason)
	{
		if (is_selected)
		{
			is_selected = false;
			Invalidate();
			OnUIASelectionChanged();
		}
	}

	internal virtual ToolStrip GetTopLevelToolStrip()
	{
		if (Parent != null)
		{
			return Parent.GetTopLevelToolStrip();
		}
		return null;
	}

	private void LayoutTextBeforeOrAfterImage(Rectangle totalArea, bool textFirst, Size textSize, Size imageSize, ContentAlignment textAlign, ContentAlignment imageAlign, out Rectangle textRect, out Rectangle imageRect)
	{
		int num = 0;
		int num2 = textSize.Width + num + imageSize.Width;
		int num3 = totalArea.Width - num2;
		int num4 = 0;
		HorizontalAlignment horizontalAlignment = GetHorizontalAlignment(textAlign);
		HorizontalAlignment horizontalAlignment2 = GetHorizontalAlignment(imageAlign);
		num4 = ((horizontalAlignment2 != 0) ? ((horizontalAlignment2 == HorizontalAlignment.Right && horizontalAlignment == HorizontalAlignment.Right) ? num3 : ((horizontalAlignment2 != HorizontalAlignment.Center || (horizontalAlignment != 0 && horizontalAlignment != HorizontalAlignment.Center)) ? (num4 + 2 * (num3 / 3)) : (num4 + num3 / 3))) : 0);
		Rectangle rectangle;
		Rectangle rectangle2;
		if (textFirst)
		{
			rectangle = new Rectangle(totalArea.Left + num4, AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
			rectangle2 = new Rectangle(rectangle.Right + num, AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
		}
		else
		{
			rectangle2 = new Rectangle(totalArea.Left + num4, AlignInRectangle(totalArea, imageSize, imageAlign).Top, imageSize.Width, imageSize.Height);
			rectangle = new Rectangle(rectangle2.Right + num, AlignInRectangle(totalArea, textSize, textAlign).Top, textSize.Width, textSize.Height);
		}
		textRect = rectangle;
		imageRect = rectangle2;
	}

	private HorizontalAlignment GetHorizontalAlignment(ContentAlignment align)
	{
		switch (align)
		{
		case ContentAlignment.TopLeft:
		case ContentAlignment.MiddleLeft:
		case ContentAlignment.BottomLeft:
			return HorizontalAlignment.Left;
		case ContentAlignment.TopCenter:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.BottomCenter:
			return HorizontalAlignment.Center;
		case ContentAlignment.TopRight:
		case ContentAlignment.MiddleRight:
		case ContentAlignment.BottomRight:
			return HorizontalAlignment.Right;
		default:
			return HorizontalAlignment.Left;
		}
	}

	internal Size GetImageSize()
	{
		if (image_scaling == ToolStripItemImageScaling.None)
		{
			if (image != null)
			{
				return image.Size;
			}
			if ((image_index >= 0 || !string.IsNullOrEmpty(image_key)) && owner != null && owner.ImageList != null)
			{
				return owner.ImageList.ImageSize;
			}
		}
		else
		{
			if (Parent == null)
			{
				return Size.Empty;
			}
			if (image != null)
			{
				return Parent.ImageScalingSize;
			}
			if ((image_index >= 0 || !string.IsNullOrEmpty(image_key)) && owner != null && owner.ImageList != null)
			{
				return Parent.ImageScalingSize;
			}
		}
		return Size.Empty;
	}

	internal string GetToolTip()
	{
		if (auto_tool_tip && string.IsNullOrEmpty(tool_tip_text))
		{
			return Text;
		}
		return tool_tip_text;
	}

	internal void FireEvent(EventArgs e, ToolStripItemEventType met)
	{
		if (Enabled || met == ToolStripItemEventType.Paint)
		{
			switch (met)
			{
			case ToolStripItemEventType.MouseUp:
				HandleClick(e);
				OnMouseUp((MouseEventArgs)e);
				break;
			case ToolStripItemEventType.MouseDown:
				OnMouseDown((MouseEventArgs)e);
				break;
			case ToolStripItemEventType.MouseEnter:
				OnMouseEnter(e);
				break;
			case ToolStripItemEventType.MouseHover:
				OnMouseHover(e);
				break;
			case ToolStripItemEventType.MouseLeave:
				OnMouseLeave(e);
				break;
			case ToolStripItemEventType.MouseMove:
				OnMouseMove((MouseEventArgs)e);
				break;
			case ToolStripItemEventType.Paint:
				OnPaint((PaintEventArgs)e);
				break;
			case ToolStripItemEventType.Click:
				HandleClick(e);
				break;
			}
		}
	}

	internal virtual void HandleClick(EventArgs e)
	{
		Parent.HandleItemClick(this);
		OnClick(e);
	}

	internal virtual void SetPlacement(ToolStripItemPlacement placement)
	{
		this.placement = placement;
	}

	private void BeginAnimation()
	{
		if (image != null && ImageAnimator.CanAnimate(image))
		{
			frame_handler = OnAnimateImage;
			ImageAnimator.Animate(image, frame_handler);
		}
	}

	private void OnAnimateImage(object sender, EventArgs e)
	{
		if (Parent != null && Parent.IsHandleCreated)
		{
			Parent.BeginInvoke(new EventHandler(UpdateAnimatedImage), this, e);
		}
	}

	private void StopAnimation()
	{
		if (frame_handler != null)
		{
			ImageAnimator.StopAnimate(image, frame_handler);
			frame_handler = null;
		}
	}

	private void UpdateAnimatedImage(object sender, EventArgs e)
	{
		if (Parent != null && Parent.IsHandleCreated)
		{
			ImageAnimator.UpdateFrames(image);
			Invalidate();
		}
	}

	internal void OnUIASelectionChanged()
	{
		((EventHandler)base.Events[UIASelectionChanged])?.Invoke(this, EventArgs.Empty);
	}
}
