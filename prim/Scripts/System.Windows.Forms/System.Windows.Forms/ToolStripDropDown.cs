using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a control that allows the user to select a single item from a list that is displayed when the user clicks a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> and <see cref="T:System.Windows.Forms.ToolStripDropDown" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class ToolStripDropDown : ToolStrip
{
	/// <summary>Provides information about the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control to accessibility client applications.</summary>
	[ComVisible(true)]
	public class ToolStripDropDownAccessibleObject : ToolStripAccessibleObject
	{
		/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" />.</summary>
		/// <returns>The string representing the name.</returns>
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" />.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Table" /> value.</returns>
		public override AccessibleRole Role => AccessibleRole.MenuPopup;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that owns the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject" />.</param>
		public ToolStripDropDownAccessibleObject(ToolStripDropDown owner)
			: base(owner)
		{
		}
	}

	private bool allow_transparency;

	private bool auto_close;

	private bool can_overflow;

	private bool drop_shadow_enabled = true;

	private double opacity = 1.0;

	private ToolStripItem owner_item;

	private static object ClosedEvent;

	private static object ClosingEvent;

	private static object OpenedEvent;

	private static object OpeningEvent;

	private static object ScrollEvent;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true to enable item reordering; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool AllowItemReorder
	{
		get
		{
			return base.AllowItemReorder;
		}
		set
		{
			base.AllowItemReorder = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ToolStripDropDown.Opacity" /> of the form can be adjusted.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.ToolStripDropDown.Opacity" /> of the form can be adjusted; otherwise, false. </returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool AllowTransparency
	{
		get
		{
			return allow_transparency;
		}
		set
		{
			if (value == allow_transparency || (XplatUI.SupportsTransparency() & TransparencySupport.Set) == 0)
			{
				return;
			}
			allow_transparency = value;
			if (base.IsHandleCreated)
			{
				if (value)
				{
					XplatUI.SetWindowTransparency(Handle, Opacity, Color.Empty);
				}
				else
				{
					UpdateStyles();
				}
			}
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control should automatically close when it has lost activation.  </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control automatically closes; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool AutoClose
	{
		get
		{
			return auto_close;
		}
		set
		{
			auto_close = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> automatically adjusts its size when the form is resized. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control automatically resizes; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
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

	/// <summary>Gets or sets a value indicating whether the items in a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> can be sent to an overflow menu.</summary>
	/// <returns>true to send <see cref="T:System.Windows.Forms.ToolStripDropDown" /> items to an overflow menu; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DefaultValue(false)]
	public new bool CanOverflow
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new ContextMenu ContextMenu
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
	public override ToolStripDropDownDirection DefaultDropDownDirection
	{
		get
		{
			return base.DefaultDropDownDirection;
		}
		set
		{
			base.DefaultDropDownDirection = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DefaultValue(DockStyle.None)]
	[EditorBrowsable(EditorBrowsableState.Always)]
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

	/// <summary>Gets or sets a value indicating whether a three-dimensional shadow effect appears when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed. </summary>
	/// <returns>true to enable the shadow effect; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool DropShadowEnabled
	{
		get
		{
			return drop_shadow_enabled;
		}
		set
		{
			if (drop_shadow_enabled != value)
			{
				drop_shadow_enabled = value;
				UpdateStyles();
			}
		}
	}

	/// <summary>Gets or sets the font of the text displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
	public override Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			base.Font = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>One of <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> the values.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new ToolStripGripDisplayStyle GripDisplayStyle => ToolStripGripDisplayStyle.Vertical;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Padding GripMargin
	{
		get
		{
			return Padding.Empty;
		}
		set
		{
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Rectangle GripRectangle => Rectangle.Empty;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DefaultValue(ToolStripGripStyle.Hidden)]
	public new ToolStripGripStyle GripStyle
	{
		get
		{
			return base.GripStyle;
		}
		set
		{
			base.GripStyle = value;
		}
	}

	/// <summary>Gets a value indicating whether this <see cref="T:System.Windows.Forms.ToolStripDropDown" /> was automatically generated. </summary>
	/// <returns>true if this <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is generated automatically; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool IsAutoGenerated => this is ToolStripOverflow;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Point Location
	{
		get
		{
			return base.Location;
		}
		set
		{
			base.Location = value;
		}
	}

	/// <summary>Determines the opacity of the form.</summary>
	/// <returns>The level of opacity for the form. The default is 1.00.</returns>
	[TypeConverter(typeof(OpacityConverter))]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DefaultValue(1.0)]
	public double Opacity
	{
		get
		{
			return opacity;
		}
		set
		{
			if (opacity != value)
			{
				opacity = value;
				allow_transparency = true;
				if (base.IsHandleCreated)
				{
					UpdateStyles();
					XplatUI.SetWindowTransparency(Handle, opacity, Color.Empty);
				}
			}
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new ToolStripOverflowButton OverflowButton => base.OverflowButton;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown" />. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Browsable(false)]
	public ToolStripItem OwnerItem
	{
		get
		{
			return owner_item;
		}
		set
		{
			owner_item = value;
			if (owner_item != null)
			{
				if (owner_item.Owner != null && owner_item.Owner.RenderMode != ToolStripRenderMode.ManagerRenderMode)
				{
					base.Renderer = owner_item.Owner.Renderer;
				}
				Font = owner_item.Font;
			}
		}
	}

	/// <summary>Gets or sets the window region associated with the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	/// <returns>The window <see cref="T:System.Drawing.Region" /> associated with the control.</returns>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new Region Region
	{
		get
		{
			return base.Region;
		}
		set
		{
			base.Region = value;
		}
	}

	[AmbientValue(RightToLeft.Inherit)]
	[Localizable(true)]
	public override RightToLeft RightToLeft
	{
		get
		{
			return base.RightToLeft;
		}
		set
		{
			base.RightToLeft = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true to enable stretching; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool Stretch
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Int32" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new int TabIndex
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	/// <summary>Specifies the direction in which to draw the text on the item.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal" />.</returns>
	[Browsable(false)]
	[DefaultValue(ToolStripTextDirection.Horizontal)]
	public override ToolStripTextDirection TextDirection
	{
		get
		{
			return base.TextDirection;
		}
		set
		{
			base.TextDirection = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is a top-level control.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is a top-level control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool TopLevel
	{
		get
		{
			return GetTopLevel();
		}
		set
		{
			SetTopLevel(value);
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is visible or hidden. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is visible; otherwise, false. The default is false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[DefaultValue(false)]
	[Localizable(true)]
	public new bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	/// <summary>Gets parameters of a new window.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.CreateParams" /> used when creating a new window.</returns>
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.Style = -2113929216;
			createParams.ClassStyle |= 131072;
			createParams.ExStyle |= 136;
			if (Opacity < 1.0 && allow_transparency)
			{
				createParams.ExStyle |= 524288;
			}
			if (TopMost)
			{
				createParams.ExStyle |= 8;
			}
			return createParams;
		}
	}

	protected override DockStyle DefaultDock => DockStyle.None;

	protected override Padding DefaultPadding => new Padding(1, 2, 1, 2);

	protected override bool DefaultShowItemToolTips => true;

	/// <summary>Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />, in pixels.</returns>
	protected internal override Size MaxItemSize => new Size(Screen.PrimaryScreen.Bounds.Width - 2, Screen.PrimaryScreen.Bounds.Height - 34);

	/// <summary>Gets or sets a value indicating whether the form should be displayed as a topmost form.</summary>
	/// <returns>true in all cases.</returns>
	protected virtual bool TopMost => true;

	internal override bool ActivateOnShow => false;

	internal ToolStripItem TopLevelOwnerItem
	{
		get
		{
			ToolStripItem ownerItem = OwnerItem;
			ToolStrip toolStrip = null;
			while (ownerItem != null)
			{
				toolStrip = ownerItem.Owner;
				if (toolStrip != null && toolStrip is ToolStripDropDown)
				{
					ownerItem = (toolStrip as ToolStripDropDown).OwnerItem;
					continue;
				}
				return ownerItem;
			}
			return null;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.BindingContext" /> property changes.</summary>
	[Browsable(false)]
	public new event EventHandler BindingContextChanged
	{
		add
		{
			base.BindingContextChanged += value;
		}
		remove
		{
			base.BindingContextChanged -= value;
		}
	}

	/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event UICuesEventHandler ChangeUICues
	{
		add
		{
			base.ChangeUICues += value;
		}
		remove
		{
			base.ChangeUICues -= value;
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is closed.</summary>
	/// <filterpriority>1</filterpriority>
	public event ToolStripDropDownClosedEventHandler Closed
	{
		add
		{
			base.Events.AddHandler(ClosedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClosedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is about to close.</summary>
	public event ToolStripDropDownClosingEventHandler Closing
	{
		add
		{
			base.Events.AddHandler(ClosingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClosingEvent, value);
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ContextMenuChanged
	{
		add
		{
			base.ContextMenuChanged += value;
		}
		remove
		{
			base.ContextMenuChanged -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event EventHandler ContextMenuStripChanged
	{
		add
		{
			base.ContextMenuStripChanged += value;
		}
		remove
		{
			base.ContextMenuStripChanged -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event EventHandler DockChanged
	{
		add
		{
			base.DockChanged += value;
		}
		remove
		{
			base.DockChanged -= value;
		}
	}

	/// <summary>Occurs when the focus enters the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event EventHandler Enter
	{
		add
		{
			base.Enter += value;
		}
		remove
		{
			base.Enter -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripDropDown.Font" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event EventHandler FontChanged
	{
		add
		{
			base.FontChanged += value;
		}
		remove
		{
			base.FontChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.ForeColor" /> property changes.</summary>
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

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event GiveFeedbackEventHandler GiveFeedback
	{
		add
		{
			base.GiveFeedback += value;
		}
		remove
		{
			base.GiveFeedback -= value;
		}
	}

	/// <summary>Occurs when the user requests help for a control.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event HelpEventHandler HelpRequested
	{
		add
		{
			base.HelpRequested += value;
		}
		remove
		{
			base.HelpRequested -= value;
		}
	}

	/// <summary>Occurs when the <see cref="E:System.Windows.Forms.ToolStripDropDown.ImeModeChanged" /> property has changed.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
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

	/// <summary>Occurs when a key is pressed and held down while the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> has focus.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event KeyEventHandler KeyDown
	{
		add
		{
			base.KeyDown += value;
		}
		remove
		{
			base.KeyDown -= value;
		}
	}

	/// <summary>Occurs when a key is pressed while the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> has focus.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event KeyPressEventHandler KeyPress
	{
		add
		{
			base.KeyPress += value;
		}
		remove
		{
			base.KeyPress -= value;
		}
	}

	/// <summary>Occurs when a key is released while the control has focus.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event KeyEventHandler KeyUp
	{
		add
		{
			base.KeyUp += value;
		}
		remove
		{
			base.KeyUp -= value;
		}
	}

	/// <summary>Occurs when the input focus leaves the control.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new event EventHandler Leave
	{
		add
		{
			base.Leave += value;
		}
		remove
		{
			base.Leave -= value;
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is opened.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Opened
	{
		add
		{
			base.Events.AddHandler(OpenedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(OpenedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control is opening.</summary>
	public event CancelEventHandler Opening
	{
		add
		{
			base.Events.AddHandler(OpeningEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(OpeningEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripDropDown.Region" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event EventHandler RegionChanged
	{
		add
		{
			base.RegionChanged += value;
		}
		remove
		{
			base.RegionChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event ScrollEventHandler Scroll
	{
		add
		{
			base.Events.AddHandler(ScrollEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ScrollEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> style changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event EventHandler StyleChanged
	{
		add
		{
			base.StyleChanged += value;
		}
		remove
		{
			base.StyleChanged -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler Validated
	{
		add
		{
			base.Validated += value;
		}
		remove
		{
			base.Validated -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event CancelEventHandler Validating
	{
		add
		{
			base.Validating += value;
		}
		remove
		{
			base.Validating -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> class. </summary>
	public ToolStripDropDown()
	{
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.ResizeRedraw, value: true);
		auto_close = true;
		is_visible = false;
		DefaultDropDownDirection = ToolStripDropDownDirection.Right;
		GripStyle = ToolStripGripStyle.Hidden;
		is_toplevel = true;
	}

	static ToolStripDropDown()
	{
		Closed = new object();
		Closing = new object();
		Opened = new object();
		Opening = new object();
		Scroll = new object();
	}

	/// <summary>Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
	public void Close()
	{
		Close(ToolStripDropDownCloseReason.CloseCalled);
	}

	/// <summary>Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control for the specified reason.</summary>
	/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
	public void Close(ToolStripDropDownCloseReason reason)
	{
		if (!Visible)
		{
			return;
		}
		ToolStripDropDownClosingEventArgs toolStripDropDownClosingEventArgs = new ToolStripDropDownClosingEventArgs(reason);
		OnClosing(toolStripDropDownClosingEventArgs);
		if (toolStripDropDownClosingEventArgs.Cancel || (!auto_close && reason != ToolStripDropDownCloseReason.CloseCalled))
		{
			return;
		}
		ToolStripManager.AppClicked -= ToolStripMenuTracker_AppClicked;
		ToolStripManager.AppFocusChange -= ToolStripMenuTracker_AppFocusChange;
		Hide();
		if (owner_item != null)
		{
			owner_item.Invalidate();
		}
		foreach (ToolStripItem item in Items)
		{
			item.Dismiss(reason);
		}
		OnClosed(new ToolStripDropDownClosedEventArgs(reason));
	}

	/// <summary>Displays the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control in its default position.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new void Show()
	{
		Show(Location, DefaultDropDownDirection);
	}

	/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified screen location.</summary>
	/// <param name="screenLocation">The horizontal and vertical location of the screen's upper-left corner, in pixels.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show(Point screenLocation)
	{
		Show(screenLocation, DefaultDropDownDirection);
	}

	/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control location.</summary>
	/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
	/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
	/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show(Control control, Point position)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		XplatUI.SetOwner(Handle, control.Handle);
		Show(control.PointToScreen(position), DefaultDropDownDirection);
	}

	/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified screen coordinates.</summary>
	/// <param name="x">The horizontal screen coordinate, in pixels.</param>
	/// <param name="y">The vertical screen coordinate, in pixels.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show(int x, int y)
	{
		Show(new Point(x, y), DefaultDropDownDirection);
	}

	/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control location and with the specified direction relative to the parent control.</summary>
	/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
	/// <param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</param>
	public void Show(Point position, ToolStripDropDownDirection direction)
	{
		PerformLayout();
		Point point = position;
		Point point2 = new Point(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
		if (this is ContextMenuStrip)
		{
			switch (direction)
			{
			case ToolStripDropDownDirection.AboveLeft:
				if (point.X - base.Width < 0)
				{
					direction = ToolStripDropDownDirection.AboveRight;
				}
				break;
			case ToolStripDropDownDirection.BelowLeft:
				if (point.X - base.Width < 0)
				{
					direction = ToolStripDropDownDirection.BelowRight;
				}
				break;
			case ToolStripDropDownDirection.Left:
				if (point.X - base.Width < 0)
				{
					direction = ToolStripDropDownDirection.Right;
				}
				break;
			case ToolStripDropDownDirection.AboveRight:
				if (point.X + base.Width > point2.X)
				{
					direction = ToolStripDropDownDirection.AboveLeft;
				}
				break;
			case ToolStripDropDownDirection.BelowRight:
			case ToolStripDropDownDirection.Default:
				if (point.X + base.Width > point2.X)
				{
					direction = ToolStripDropDownDirection.BelowLeft;
				}
				break;
			case ToolStripDropDownDirection.Right:
				if (point.X + base.Width > point2.X)
				{
					direction = ToolStripDropDownDirection.Left;
				}
				break;
			}
			switch (direction)
			{
			case ToolStripDropDownDirection.AboveLeft:
				if (point.Y - base.Height < 0)
				{
					direction = ToolStripDropDownDirection.BelowLeft;
				}
				break;
			case ToolStripDropDownDirection.AboveRight:
				if (point.Y - base.Height < 0)
				{
					direction = ToolStripDropDownDirection.BelowRight;
				}
				break;
			case ToolStripDropDownDirection.BelowLeft:
				if (point.Y + base.Height > point2.Y)
				{
					direction = ToolStripDropDownDirection.AboveLeft;
				}
				break;
			case ToolStripDropDownDirection.BelowRight:
			case ToolStripDropDownDirection.Default:
				if (point.Y + base.Height > point2.Y)
				{
					direction = ToolStripDropDownDirection.AboveRight;
				}
				break;
			case ToolStripDropDownDirection.Left:
				if (point.Y + base.Height > point2.Y)
				{
					direction = ToolStripDropDownDirection.AboveLeft;
				}
				break;
			case ToolStripDropDownDirection.Right:
				if (point.Y + base.Height > point2.Y)
				{
					direction = ToolStripDropDownDirection.AboveRight;
				}
				break;
			}
		}
		switch (direction)
		{
		case ToolStripDropDownDirection.AboveLeft:
			point.Y -= base.Height;
			point.X -= base.Width;
			break;
		case ToolStripDropDownDirection.AboveRight:
			point.Y -= base.Height;
			break;
		case ToolStripDropDownDirection.BelowLeft:
			point.X -= base.Width;
			break;
		case ToolStripDropDownDirection.Left:
			point.X -= base.Width;
			break;
		}
		if (point.X + base.Width > point2.X)
		{
			point.X = point2.X - base.Width;
		}
		if (point.X < 0)
		{
			point.X = 0;
		}
		if (Location != point)
		{
			Location = point;
		}
		CancelEventArgs cancelEventArgs = new CancelEventArgs();
		OnOpening(cancelEventArgs);
		if (!cancelEventArgs.Cancel)
		{
			ToolStripManager.AppClicked += ToolStripMenuTracker_AppClicked;
			ToolStripManager.AppFocusChange += ToolStripMenuTracker_AppFocusChange;
			base.Show();
			ToolStripManager.SetActiveToolStrip(this, ToolStripManager.ActivatedByKeyboard);
			OnOpened(EventArgs.Empty);
		}
	}

	/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control's horizontal and vertical screen coordinates.</summary>
	/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
	/// <param name="x">The horizontal screen coordinate of the control, in pixels.</param>
	/// <param name="y">The vertical screen coordinate of the control, in pixels.</param>
	/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show(Control control, int x, int y)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		Show(control, new Point(x, y));
	}

	/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control at the specified location and with the specified direction relative to the parent control.</summary>
	/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
	/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
	/// <param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
	public void Show(Control control, Point position, ToolStripDropDownDirection direction)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		XplatUI.SetOwner(Handle, control.Handle);
		Show(control.PointToScreen(position), direction);
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripDropDown" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripDropDownAccessibleObject(this);
	}

	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <summary>Applies various layout options to the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.LayoutSettings" /> for this <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
	/// <param name="style">One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The possibilities are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow" />.</param>
	protected override LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle style)
	{
		return base.CreateLayoutSettings(style);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> that contains the event data.</param>
	protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
	{
		((ToolStripDropDownClosedEventHandler)base.Events[Closed])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs" /> that contains the event data.</param>
	protected virtual void OnClosing(ToolStripDropDownClosingEventArgs e)
	{
		((ToolStripDropDownClosingEventHandler)base.Events[Closing])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
		if (Application.MWFThread.Current.Context != null && Application.MWFThread.Current.Context.MainForm != null)
		{
			XplatUI.SetOwner(Handle, Application.MWFThread.Current.Context.MainForm.Handle);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
	protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
	{
		base.OnItemClicked(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		int num = 0;
		foreach (ToolStripItem item in Items)
		{
			if (item.Available)
			{
				item.SetPlacement(ToolStripItemPlacement.Main);
				num = Math.Max(num, item.GetPreferredSize(Size.Empty).Width + item.Margin.Horizontal);
			}
		}
		num += base.Padding.Horizontal;
		int left = base.Padding.Left;
		int num2 = base.Padding.Top;
		foreach (ToolStripItem item2 in Items)
		{
			if (item2.Available)
			{
				num2 += item2.Margin.Top;
				int num3 = 0;
				Size preferredSize = item2.GetPreferredSize(Size.Empty);
				num3 = ((preferredSize.Height > 22) ? preferredSize.Height : ((!(item2 is ToolStripSeparator)) ? 22 : 7));
				item2.SetBounds(new Rectangle(left, num2, preferredSize.Width, num3));
				num2 += num3 + item2.Margin.Bottom;
			}
		}
		base.Size = new Size(num, num2 + base.Padding.Bottom);
		SetDisplayedItems();
		OnLayoutCompleted(EventArgs.Empty);
		Invalidate();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
	/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseUp(MouseEventArgs mea)
	{
		base.OnMouseUp(mea);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnOpened(EventArgs e)
	{
		((EventHandler)base.Events[Opened])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
	protected virtual void OnOpening(CancelEventArgs e)
	{
		((CancelEventHandler)base.Events[Opening])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
		if (base.Parent is ToolStrip)
		{
			base.Renderer = (base.Parent as ToolStrip).Renderer;
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnVisibleChanged(EventArgs e)
	{
		base.OnVisibleChanged(e);
		if (owner_item != null && owner_item is ToolStripDropDownItem)
		{
			ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)owner_item;
			if (Visible)
			{
				toolStripDropDownItem.OnDropDownOpened(EventArgs.Empty);
			}
			else
			{
				toolStripDropDownItem.OnDropDownClosed(EventArgs.Empty);
			}
		}
	}

	/// <summary>Processes a dialog box character.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override bool ProcessDialogChar(char charCode)
	{
		return base.ProcessDialogChar(charCode);
	}

	/// <summary>Processes a dialog box key.</summary>
	/// <returns>true if the key was processed by the control; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		if (keyData == (Keys.Tab | Keys.Control) || keyData == (Keys.Tab | Keys.Shift | Keys.Control))
		{
			return true;
		}
		return base.ProcessDialogKey(keyData);
	}

	/// <summary>Processes a mnemonic character.</summary>
	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process.</param>
	protected override bool ProcessMnemonic(char charCode)
	{
		return base.ProcessMnemonic(charCode);
	}

	protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		base.ScaleControl(factor, specified);
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <param name="dx">The horizontal scaling factor.</param>
	/// <param name="dy">The vertical scaling factor.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override void ScaleCore(float dx, float dy)
	{
		base.ScaleCore(dx, dy);
	}

	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <summary>Adjusts the size of the owner <see cref="T:System.Windows.Forms.ToolStrip" /> to accommodate the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed, or clears and resets active <see cref="T:System.Windows.Forms.ToolStripDropDown" /> child controls of the <see cref="T:System.Windows.Forms.ToolStrip" /> if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not currently displayed.</summary>
	/// <param name="visible">true if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed; otherwise, false. </param>
	protected override void SetVisibleCore(bool visible)
	{
		base.SetVisibleCore(visible);
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		if (m.Msg == 33)
		{
			m.Result = (IntPtr)3;
		}
		else
		{
			base.WndProc(ref m);
		}
	}

	internal override void Dismiss(ToolStripDropDownCloseReason reason)
	{
		Close(reason);
		base.Dismiss(reason);
	}

	internal override ToolStrip GetTopLevelToolStrip()
	{
		if (OwnerItem == null)
		{
			return this;
		}
		return OwnerItem.GetTopLevelToolStrip();
	}

	internal override bool ProcessArrowKey(Keys keyData)
	{
		switch (keyData)
		{
		case Keys.Tab:
		case Keys.Down:
			SelectNextToolStripItem(GetCurrentlySelectedItem(), forward: true);
			return true;
		case Keys.Up:
		case Keys.Tab | Keys.Shift:
			SelectNextToolStripItem(GetCurrentlySelectedItem(), forward: false);
			return true;
		case Keys.Right:
			GetTopLevelToolStrip().SelectNextToolStripItem(TopLevelOwnerItem, forward: true);
			return true;
		case Keys.Escape:
		case Keys.Left:
		{
			Dismiss(ToolStripDropDownCloseReason.Keyboard);
			if (OwnerItem == null)
			{
				return true;
			}
			ToolStrip toolStrip = OwnerItem.Parent;
			ToolStripManager.SetActiveToolStrip(toolStrip, keyboard: true);
			if (toolStrip is MenuStrip && keyData == Keys.Left)
			{
				toolStrip.SelectNextToolStripItem(TopLevelOwnerItem, forward: false);
				TopLevelOwnerItem.Invalidate();
			}
			else if (toolStrip is MenuStrip && keyData == Keys.Escape)
			{
				(toolStrip as MenuStrip).MenuDroppedDown = false;
				TopLevelOwnerItem.Select();
			}
			return true;
		}
		default:
			return false;
		}
	}

	internal override ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
	{
		ToolStripItem nextItem = GetNextItem(start, (!forward) ? ArrowDirection.Up : ArrowDirection.Down);
		if (nextItem != null)
		{
			ChangeSelection(nextItem);
		}
		return nextItem;
	}

	private void ToolStripMenuTracker_AppFocusChange(object sender, EventArgs e)
	{
		GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
	}

	private void ToolStripMenuTracker_AppClicked(object sender, EventArgs e)
	{
		GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppClicked);
	}
}
