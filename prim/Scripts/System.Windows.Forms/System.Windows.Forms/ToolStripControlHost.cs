using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Hosts custom controls or Windows Forms controls.</summary>
/// <filterpriority>2</filterpriority>
public class ToolStripControlHost : ToolStripItem
{
	private Control control;

	private ContentAlignment control_align;

	private bool double_click_enabled;

	private static object EnterEvent;

	private static object GotFocusEvent;

	private static object KeyDownEvent;

	private static object KeyPressEvent;

	private static object KeyUpEvent;

	private static object LeaveEvent;

	private static object LostFocusEvent;

	private static object ValidatedEvent;

	private static object ValidatingEvent;

	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Color BackColor
	{
		get
		{
			return control.BackColor;
		}
		set
		{
			control.BackColor = value;
		}
	}

	/// <summary>Gets or sets the background image displayed in the control.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
	[DefaultValue(null)]
	[Localizable(true)]
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

	/// <summary>Gets or sets the background image layout as defined in the ImageLayout enumeration.</summary>
	/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />:<see cref="F:System.Windows.Forms.ImageLayout.Center" /><see cref="F:System.Windows.Forms.ImageLayout.None" /><see cref="F:System.Windows.Forms.ImageLayout.Stretch" /><see cref="F:System.Windows.Forms.ImageLayout.Tile" /> (default)<see cref="F:System.Windows.Forms.ImageLayout.Zoom" /></returns>
	[Localizable(true)]
	[DefaultValue(ImageLayout.Tile)]
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

	/// <summary>Gets a value indicating whether the control can be selected.</summary>
	/// <returns>true if the control can be selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public override bool CanSelect => control.CanSelect;

	/// <summary>Gets or sets a value indicating whether the hosted control causes and raises validation events on other controls when the hosted control receives focus.</summary>
	/// <returns>true if the hosted control causes and raises validation events on other controls when the hosted control receives focus; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public bool CausesValidation
	{
		get
		{
			return control.CausesValidation;
		}
		set
		{
			control.CausesValidation = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control Control => control;

	/// <summary>Gets or sets the alignment of the control on the form.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleCenter" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripControlHost.ControlAlign" /> property is set to a value that is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(ContentAlignment.MiddleCenter)]
	public ContentAlignment ControlAlign
	{
		get
		{
			return control_align;
		}
		set
		{
			if (control_align != value)
			{
				if (!Enum.IsDefined(typeof(ContentAlignment), value))
				{
					throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ContentAlignment");
				}
				control_align = value;
				if (control != null)
				{
					control.Bounds = AlignInRectangle(Bounds, control.Size, control_align);
				}
			}
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ToolStripItemDisplayStyle DisplayStyle
	{
		get
		{
			return base.DisplayStyle;
		}
		set
		{
			base.DisplayStyle = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if double clicking is enabled; otherwise, false. </returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DefaultValue(false)]
	public new bool DoubleClickEnabled
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

	/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled.</summary>
	/// <returns>true if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
			control.Enabled = value;
		}
	}

	/// <summary>Gets a value indicating whether the control has input focus.</summary>
	/// <returns>true if the control has input focus; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public virtual bool Focused => control.Focused;

	/// <summary>Gets or sets the font to be used on the hosted control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> for the hosted control.</returns>
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
			return control.Font;
		}
		set
		{
			control.Font = value;
		}
	}

	/// <summary>Gets or sets the foreground color of the hosted control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of the hosted control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Color ForeColor
	{
		get
		{
			return control.ForeColor;
		}
		set
		{
			control.ForeColor = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Image Image
	{
		get
		{
			return base.Image;
		}
		set
		{
			base.Image = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.ContentAlignment" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new ContentAlignment ImageAlign
	{
		get
		{
			return base.ImageAlign;
		}
		set
		{
			base.ImageAlign = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ToolStripItemImageScaling ImageScaling
	{
		get
		{
			return base.ImageScaling;
		}
		set
		{
			base.ImageScaling = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Color ImageTransparentColor
	{
		get
		{
			return base.ImageTransparentColor;
		}
		set
		{
			base.ImageTransparentColor = value;
		}
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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
	/// <returns>true if the image is mirrored; otherwise, false.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool RightToLeftAutoMirrorImage
	{
		get
		{
			return base.RightToLeftAutoMirrorImage;
		}
		set
		{
			base.RightToLeftAutoMirrorImage = value;
		}
	}

	/// <summary>Gets a value indicating whether the item is selected.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public override bool Selected => base.Selected;

	/// <summary>Gets or sets the site of the hosted control.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the control.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public override ISite Site
	{
		get
		{
			return control.Site;
		}
		set
		{
			control.Site = value;
		}
	}

	/// <summary>Gets or sets the size of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	public override Size Size
	{
		get
		{
			return base.Size;
		}
		set
		{
			control.Size = value;
			base.Size = value;
			if (base.Owner != null)
			{
				base.Owner.PerformLayout();
			}
		}
	}

	/// <summary>Gets or sets the text to be displayed on the hosted control.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the text.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	public override string Text
	{
		get
		{
			return control.Text;
		}
		set
		{
			base.Text = value;
			control.Text = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.ContentAlignment" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ContentAlignment TextAlign
	{
		get
		{
			return base.TextAlign;
		}
		set
		{
			base.TextAlign = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripTextDirection" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TextImageRelation" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new TextImageRelation TextImageRelation
	{
		get
		{
			return base.TextImageRelation;
		}
		set
		{
			base.TextImageRelation = value;
		}
	}

	/// <summary>Gets the default size of the control.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize
	{
		get
		{
			if (control == null)
			{
				return new Size(23, 23);
			}
			return control.Size;
		}
	}

	internal override ToolStripTextDirection DefaultTextDirection => ToolStripTextDirection.Horizontal;

	internal override bool InternalVisible
	{
		get
		{
			return base.InternalVisible;
		}
		set
		{
			Control.Visible = value;
			base.InternalVisible = value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler DisplayStyleChanged
	{
		add
		{
			base.DisplayStyleChanged += value;
		}
		remove
		{
			base.DisplayStyleChanged -= value;
		}
	}

	/// <summary>Occurs when the hosted control is entered.</summary>
	public event EventHandler Enter
	{
		add
		{
			base.Events.AddHandler(EnterEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(EnterEvent, value);
		}
	}

	/// <summary>Occurs when the hosted control receives focus.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler GotFocus
	{
		add
		{
			base.Events.AddHandler(GotFocusEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(GotFocusEvent, value);
		}
	}

	/// <summary>Occurs when a key is pressed and held down while the hosted control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event KeyEventHandler KeyDown
	{
		add
		{
			base.Events.AddHandler(KeyDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(KeyDownEvent, value);
		}
	}

	/// <summary>Occurs when a key is pressed while the hosted control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event KeyPressEventHandler KeyPress
	{
		add
		{
			base.Events.AddHandler(KeyPressEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(KeyPressEvent, value);
		}
	}

	/// <summary>Occurs when a key is released while the hosted control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event KeyEventHandler KeyUp
	{
		add
		{
			base.Events.AddHandler(KeyUpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(KeyUpEvent, value);
		}
	}

	/// <summary>Occurs when the input focus leaves the hosted control.</summary>
	public event EventHandler Leave
	{
		add
		{
			base.Events.AddHandler(LeaveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LeaveEvent, value);
		}
	}

	/// <summary>Occurs when the hosted control loses focus.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler LostFocus
	{
		add
		{
			base.Events.AddHandler(LostFocusEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LostFocusEvent, value);
		}
	}

	/// <summary>Occurs after the hosted control has been successfully validated.</summary>
	public event EventHandler Validated
	{
		add
		{
			base.Events.AddHandler(ValidatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValidatedEvent, value);
		}
	}

	/// <summary>Occurs while the hosted control is validating.</summary>
	public event CancelEventHandler Validating
	{
		add
		{
			base.Events.AddHandler(ValidatingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValidatingEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class that hosts the specified control.</summary>
	/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> hosted by this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class. </param>
	/// <exception cref="T:System.ArgumentNullException">The control referred to by the <paramref name="c" /> parameter is null.</exception>
	public ToolStripControlHost(Control c)
	{
		if (c == null)
		{
			throw new ArgumentNullException("c");
		}
		RightToLeft = RightToLeft.No;
		control = c;
		control_align = ContentAlignment.MiddleCenter;
		control.TabStop = false;
		control.Resize += ControlResizeHandler;
		Size = DefaultSize;
		OnSubscribeControlEvents(control);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class that hosts the specified control and that has the specified name.</summary>
	/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> hosted by this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripControlHost" />.</param>
	public ToolStripControlHost(Control c, string name)
		: this(c)
	{
		base.Name = name;
	}

	static ToolStripControlHost()
	{
		Enter = new object();
		GotFocus = new object();
		KeyDown = new object();
		KeyPress = new object();
		KeyUp = new object();
		Leave = new object();
		LostFocus = new object();
		Validated = new object();
		Validating = new object();
	}

	/// <summary>Gives the focus to a control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void Focus()
	{
		control.Focus();
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return control.GetPreferredSize(constrainingSize);
	}

	/// <summary>This method is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void ResetBackColor()
	{
		base.ResetBackColor();
	}

	/// <summary>This method is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void ResetForeColor()
	{
		base.ResetForeColor();
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return control.AccessibilityObject;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		if (control.Created && !control.IsDisposed)
		{
			control.Dispose();
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
	protected override void OnBoundsChanged()
	{
		if (control != null)
		{
			control.Bounds = AlignInRectangle(Bounds, control.Size, control_align);
		}
		base.OnBoundsChanged();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Enter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnEnter(EventArgs e)
	{
		((EventHandler)base.Events[Enter])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.GotFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnGotFocus(EventArgs e)
	{
		((EventHandler)base.Events[GotFocus])?.Invoke(this, e);
	}

	private void ControlResizeHandler(object obj, EventArgs args)
	{
		OnHostedControlResize(args);
	}

	/// <summary>Synchronizes the resizing of the control host with the resizing of the hosted control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnHostedControlResize(EventArgs e)
	{
		if (control != null)
		{
			control.Location = AlignInRectangle(Bounds, control.Size, control_align).Location;
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
	protected virtual void OnKeyDown(KeyEventArgs e)
	{
		((KeyEventHandler)base.Events[KeyDown])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyPress" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
	protected virtual void OnKeyPress(KeyPressEventArgs e)
	{
		((KeyPressEventHandler)base.Events[KeyPress])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
	protected virtual void OnKeyUp(KeyEventArgs e)
	{
		((KeyEventHandler)base.Events[KeyUp])?.Invoke(this, e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		base.OnLayout(e);
		if (control != null)
		{
			control.Bounds = AlignInRectangle(Bounds, control.Size, control_align);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Leave" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLeave(EventArgs e)
	{
		((EventHandler)base.Events[Leave])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.LostFocus" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnLostFocus(EventArgs e)
	{
		((EventHandler)base.Events[LostFocus])?.Invoke(this, e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
	}

	/// <param name="oldParent">The original parent of the item. </param>
	/// <param name="newParent">The new parent of the item. </param>
	protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
	{
		base.OnParentChanged(oldParent, newParent);
		oldParent?.Controls.Remove(control);
		newParent?.Controls.Add(control);
	}

	/// <summary>Subscribes events from the hosted control.</summary>
	/// <param name="control">The control from which to subscribe events.</param>
	protected virtual void OnSubscribeControlEvents(Control control)
	{
		this.control.Enter += HandleEnter;
		this.control.GotFocus += HandleGotFocus;
		this.control.KeyDown += HandleKeyDown;
		this.control.KeyPress += HandleKeyPress;
		this.control.KeyUp += HandleKeyUp;
		this.control.Leave += HandleLeave;
		this.control.LostFocus += HandleLostFocus;
		this.control.Validated += HandleValidated;
		this.control.Validating += HandleValidating;
	}

	/// <summary>Unsubscribes events from the hosted control.</summary>
	/// <param name="control">The control from which to unsubscribe events.</param>
	protected virtual void OnUnsubscribeControlEvents(Control control)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Validated" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnValidated(EventArgs e)
	{
		((EventHandler)base.Events[Validated])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Validating" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
	protected virtual void OnValidating(CancelEventArgs e)
	{
		((CancelEventHandler)base.Events[Validating])?.Invoke(this, e);
	}

	/// <returns>false in all cases.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		return base.ProcessCmdKey(ref m, keyData);
	}

	/// <returns>true if the key was processed by the item; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal override bool ProcessDialogKey(Keys keyData)
	{
		return base.ProcessDialogKey(keyData);
	}

	/// <param name="visible">true to make the <see cref="T:System.Windows.Forms.ToolStripItem" /> visible; otherwise, false.</param>
	protected override void SetVisibleCore(bool visible)
	{
		base.SetVisibleCore(visible);
		control.Visible = visible;
		if (control != null)
		{
			control.Bounds = AlignInRectangle(Bounds, control.Size, control_align);
		}
	}

	internal override void Dismiss(ToolStripDropDownCloseReason reason)
	{
		if (Selected)
		{
			base.Parent.Focus();
		}
		base.Dismiss(reason);
	}

	private void HandleEnter(object sender, EventArgs e)
	{
		OnEnter(e);
	}

	private void HandleGotFocus(object sender, EventArgs e)
	{
		OnGotFocus(e);
	}

	private void HandleKeyDown(object sender, KeyEventArgs e)
	{
		OnKeyDown(e);
	}

	private void HandleKeyPress(object sender, KeyPressEventArgs e)
	{
		OnKeyPress(e);
	}

	private void HandleKeyUp(object sender, KeyEventArgs e)
	{
		OnKeyUp(e);
	}

	private void HandleLeave(object sender, EventArgs e)
	{
		OnLeave(e);
	}

	private void HandleLostFocus(object sender, EventArgs e)
	{
		OnLostFocus(e);
	}

	private void HandleValidated(object sender, EventArgs e)
	{
		OnValidated(e);
	}

	private void HandleValidating(object sender, CancelEventArgs e)
	{
		OnValidating(e);
	}
}
