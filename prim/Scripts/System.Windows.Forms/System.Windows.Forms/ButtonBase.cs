using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Implements the basic functionality common to button controls.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ComVisible(true)]
public abstract class ButtonBase : Control
{
	/// <summary>Provides information that accessibility applications use to adjust an application's user interface for users with disabilities.</summary>
	[ComVisible(true)]
	public class ButtonBaseAccessibleObject : ControlAccessibleObject
	{
		private new Control owner;

		/// <summary>Gets the state of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values.</returns>
		public override AccessibleStates State => base.State;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase.ButtonBaseAccessibleObject" /> class. </summary>
		/// <param name="owner">The owner of this <see cref="T:System.Windows.Forms.ButtonBase.ButtonBaseAccessibleObject" />.</param>
		public ButtonBaseAccessibleObject(Control owner)
			: base(owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.owner = owner;
			default_action = "Press";
			role = AccessibleRole.PushButton;
		}

		/// <summary>Performs the default action associated with this accessible object.</summary>
		public override void DoDefaultAction()
		{
			((ButtonBase)owner).OnClick(EventArgs.Empty);
		}
	}

	private FlatStyle flat_style;

	private int image_index;

	internal Image image;

	internal ImageList image_list;

	private ContentAlignment image_alignment;

	internal ContentAlignment text_alignment;

	private bool is_default;

	internal bool is_pressed;

	internal StringFormat text_format;

	internal bool paint_as_acceptbutton;

	private bool auto_ellipsis;

	private FlatButtonAppearance flat_button_appearance;

	private string image_key;

	private TextImageRelation text_image_relation;

	private TextFormatFlags text_format_flags;

	private bool use_mnemonic;

	private bool use_visual_style_back_color;

	/// <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the control, denoting that the control text extends beyond the specified length of the control.</summary>
	/// <returns>true if the additional label text is to be indicated by an ellipsis; otherwise, false. The default is true.</returns>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[MWFCategory("Behavior")]
	[DefaultValue(false)]
	public bool AutoEllipsis
	{
		get
		{
			return auto_ellipsis;
		}
		set
		{
			if (auto_ellipsis != value)
			{
				auto_ellipsis = value;
				if (auto_ellipsis)
				{
					text_format_flags |= TextFormatFlags.EndEllipsis;
					text_format_flags &= ~TextFormatFlags.WordBreak;
				}
				else
				{
					text_format_flags &= ~TextFormatFlags.EndEllipsis;
					text_format_flags |= TextFormatFlags.WordBreak;
				}
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "AutoEllipsis");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the control resizes based on its contents.</summary>
	/// <returns>true if the control automatically resizes based on its contents; otherwise, false. The default is true.</returns>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[MWFCategory("Layout")]
	[Browsable(true)]
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

	/// <summary>Gets or sets the background color of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
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

	/// <summary>Gets the appearance of the border and the colors used to indicate check state and mouse state.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatButtonAppearance" /> values.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[MWFCategory("Appearance")]
	[Browsable(true)]
	public FlatButtonAppearance FlatAppearance => flat_button_appearance;

	/// <summary>Gets or sets the flat style appearance of the button control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is Standard.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[Localizable(true)]
	[DefaultValue(FlatStyle.Standard)]
	[MWFDescription("Determines look of button")]
	public FlatStyle FlatStyle
	{
		get
		{
			return flat_style;
		}
		set
		{
			if (flat_style != value)
			{
				flat_style = value;
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "FlatStyle");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the image that is displayed on a button control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> displayed on the button control. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[MWFDescription("Sets image to be displayed on button face")]
	[MWFCategory("Appearance")]
	public Image Image
	{
		get
		{
			if (image != null)
			{
				return image;
			}
			if (image_index >= 0 && image_list != null)
			{
				return image_list.Images[image_index];
			}
			if (!string.IsNullOrEmpty(image_key) && image_list != null)
			{
				return image_list.Images[image_key];
			}
			return null;
		}
		set
		{
			if (image != value)
			{
				image = value;
				image_index = -1;
				image_key = string.Empty;
				image_list = null;
				if (AutoSize && base.Parent != null)
				{
					base.Parent.PerformLayout(this, "Image");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the alignment of the image on the button control.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is MiddleCenter.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[Localizable(true)]
	[DefaultValue(ContentAlignment.MiddleCenter)]
	[MWFDescription("Sets the alignment of the image to be displayed on button face")]
	public ContentAlignment ImageAlign
	{
		get
		{
			return image_alignment;
		}
		set
		{
			if (image_alignment != value)
			{
				image_alignment = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the image list index value of the image displayed on the button control.</summary>
	/// <returns>A zero-based index, which represents the image position in an <see cref="T:System.Windows.Forms.ImageList" />. The default is -1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the lower bounds of the <see cref="P:System.Windows.Forms.ButtonBase.ImageIndex" />. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[TypeConverter(typeof(ImageIndexConverter))]
	[DefaultValue(-1)]
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[MWFCategory("Appearance")]
	[MWFDescription("Index of image to display, if ImageList is used for button face images")]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public int ImageIndex
	{
		get
		{
			if (image_list == null)
			{
				return -1;
			}
			return image_index;
		}
		set
		{
			if (image_index != value)
			{
				image_index = value;
				image = null;
				image_key = string.Empty;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.ButtonBase.ImageList" />.</summary>
	/// <returns>A string representing the key of the image.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	[Localizable(true)]
	[MWFCategory("Appearance")]
	[TypeConverter(typeof(ImageKeyConverter))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the <see cref="T:System.Drawing.Image" /> displayed on a button control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageList" />. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[MWFDescription("ImageList used for ImageIndex")]
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
				if (value != null && image != null)
				{
					image = null;
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control. This property is not relevant for this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	/// <filterpriority>2</filterpriority>
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
			return base.ImeMode;
		}
		set
		{
			base.ImeMode = value;
		}
	}

	/// <returns>The text associated with this control.</returns>
	[SettingsBindable(true)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

	/// <summary>Gets or sets the alignment of the text on the button control.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is MiddleCenter.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[DefaultValue(ContentAlignment.MiddleCenter)]
	[Localizable(true)]
	[MWFDescription("Alignment for button text")]
	public virtual ContentAlignment TextAlign
	{
		get
		{
			return text_alignment;
		}
		set
		{
			if (text_alignment != value)
			{
				text_alignment = value;
				text_format_flags &= ~TextFormatFlags.Bottom;
				text_format_flags &= (TextFormatFlags)(-1);
				text_format_flags &= (TextFormatFlags)(-1);
				text_format_flags &= ~TextFormatFlags.Right;
				text_format_flags &= ~TextFormatFlags.HorizontalCenter;
				text_format_flags &= ~TextFormatFlags.VerticalCenter;
				switch (text_alignment)
				{
				case ContentAlignment.TopLeft:
					text_format.Alignment = StringAlignment.Near;
					text_format.LineAlignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopCenter:
					text_format.Alignment = StringAlignment.Center;
					text_format.LineAlignment = StringAlignment.Near;
					text_format_flags |= TextFormatFlags.HorizontalCenter;
					break;
				case ContentAlignment.TopRight:
					text_format.Alignment = StringAlignment.Far;
					text_format.LineAlignment = StringAlignment.Near;
					text_format_flags |= TextFormatFlags.Right;
					break;
				case ContentAlignment.MiddleLeft:
					text_format.Alignment = StringAlignment.Near;
					text_format.LineAlignment = StringAlignment.Center;
					text_format_flags |= TextFormatFlags.VerticalCenter;
					break;
				case ContentAlignment.MiddleCenter:
					text_format.Alignment = StringAlignment.Center;
					text_format.LineAlignment = StringAlignment.Center;
					text_format_flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
					break;
				case ContentAlignment.MiddleRight:
					text_format.Alignment = StringAlignment.Far;
					text_format.LineAlignment = StringAlignment.Center;
					text_format_flags |= TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
					break;
				case ContentAlignment.BottomLeft:
					text_format.Alignment = StringAlignment.Near;
					text_format.LineAlignment = StringAlignment.Far;
					text_format_flags |= TextFormatFlags.Bottom;
					break;
				case ContentAlignment.BottomCenter:
					text_format.Alignment = StringAlignment.Center;
					text_format.LineAlignment = StringAlignment.Far;
					text_format_flags |= TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
					break;
				case ContentAlignment.BottomRight:
					text_format.Alignment = StringAlignment.Far;
					text_format.LineAlignment = StringAlignment.Far;
					text_format_flags |= TextFormatFlags.Right | TextFormatFlags.Bottom;
					break;
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the position of text and image relative to each other.</summary>
	/// <returns>One of the values of <see cref="T:System.Windows.Forms.TextImageRelation" />. The default is <see cref="F:System.Windows.Forms.TextImageRelation.Overlay" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values.</exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[MWFCategory("Appearance")]
	[DefaultValue(TextImageRelation.Overlay)]
	[Localizable(true)]
	public TextImageRelation TextImageRelation
	{
		get
		{
			return text_image_relation;
		}
		set
		{
			if (!Enum.IsDefined(typeof(TextImageRelation), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for TextImageRelation");
			}
			if (text_image_relation != value)
			{
				text_image_relation = value;
				if (AutoSize && base.Parent != null)
				{
					base.Parent.PerformLayout(this, "TextImageRelation");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
	/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	[MWFCategory("Behavior")]
	public bool UseCompatibleTextRendering
	{
		get
		{
			return use_compatible_text_rendering;
		}
		set
		{
			if (use_compatible_text_rendering != value)
			{
				use_compatible_text_rendering = value;
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "UseCompatibleTextRendering");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control.</summary>
	/// <returns>true if the first character that is preceded by an ampersand (&amp;) is used as the mnemonic key of the control; otherwise, false. The default is true.</returns>
	[MWFCategory("Appearance")]
	[DefaultValue(true)]
	public bool UseMnemonic
	{
		get
		{
			return use_mnemonic;
		}
		set
		{
			if (use_mnemonic != value)
			{
				use_mnemonic = value;
				if (use_mnemonic)
				{
					text_format_flags &= ~TextFormatFlags.NoPrefix;
				}
				else
				{
					text_format_flags |= TextFormatFlags.NoPrefix;
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value that determines if the background is drawn using visual styles, if supported.</summary>
	/// <returns>true if the background is drawn using visual styles; otherwise, false.</returns>
	[MWFCategory("Appearance")]
	public bool UseVisualStyleBackColor
	{
		get
		{
			return use_visual_style_back_color;
		}
		set
		{
			if (use_visual_style_back_color != value)
			{
				use_visual_style_back_color = value;
				Invalidate();
			}
		}
	}

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	protected override ImeMode DefaultImeMode => ImeMode.Disable;

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.ButtonBaseDefaultSize;

	/// <summary>Gets or sets a value indicating whether the button control is the default button.</summary>
	/// <returns>true if the button control is the default button; otherwise, false.</returns>
	protected internal bool IsDefault
	{
		get
		{
			return is_default;
		}
		set
		{
			if (is_default != value)
			{
				is_default = value;
				Invalidate();
			}
		}
	}

	internal ButtonState ButtonState
	{
		get
		{
			ButtonState buttonState = ButtonState.Normal;
			if (base.Enabled)
			{
				if (is_entered)
				{
					if (flat_style == FlatStyle.Flat)
					{
						buttonState |= ButtonState.Flat;
					}
				}
				else if (flat_style == FlatStyle.Flat || flat_style == FlatStyle.Popup)
				{
					buttonState |= ButtonState.Flat;
				}
				if (is_entered && is_pressed)
				{
					buttonState |= ButtonState.Pushed;
				}
			}
			else
			{
				buttonState |= ButtonState.Inactive;
				if (flat_style == FlatStyle.Flat || flat_style == FlatStyle.Popup)
				{
					buttonState |= ButtonState.Flat;
				}
			}
			return buttonState;
		}
	}

	internal bool Pressed => is_pressed;

	internal TextFormatFlags TextFormatFlags => text_format_flags;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ButtonBase.AutoSize" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ButtonBase.ImeMode" /> property is changed. This event is not relevant for this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ButtonBase" /> class.</summary>
	protected ButtonBase()
	{
		flat_style = FlatStyle.Standard;
		flat_button_appearance = new FlatButtonAppearance(this);
		image_key = string.Empty;
		text_image_relation = TextImageRelation.Overlay;
		use_mnemonic = true;
		use_visual_style_back_color = true;
		image_index = -1;
		image = null;
		image_list = null;
		image_alignment = ContentAlignment.MiddleCenter;
		ImeMode = ImeMode.Disable;
		text_alignment = ContentAlignment.MiddleCenter;
		is_default = false;
		is_pressed = false;
		text_format = new StringFormat();
		text_format.Alignment = StringAlignment.Center;
		text_format.LineAlignment = StringAlignment.Center;
		text_format.HotkeyPrefix = HotkeyPrefix.Show;
		text_format.FormatFlags |= StringFormatFlags.LineLimit;
		text_format_flags = TextFormatFlags.HorizontalCenter;
		text_format_flags |= TextFormatFlags.VerticalCenter;
		text_format_flags |= TextFormatFlags.TextBoxControl;
		SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.CacheText | ControlStyles.OptimizedDoubleBuffer, value: true);
		SetStyle(ControlStyles.StandardClick, value: false);
	}

	internal bool ShouldSerializeImage()
	{
		return Image != null;
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="proposedSize">The custom-sized area for a control.</param>
	public override Size GetPreferredSize(Size proposedSize)
	{
		return base.GetPreferredSize(proposedSize);
	}

	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ButtonBaseAccessibleObject(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ButtonBase" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnGotFocus(EventArgs e)
	{
		Invalidate();
		base.OnGotFocus(e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
	/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyDown(KeyEventArgs kevent)
	{
		if (kevent.KeyData == Keys.Space)
		{
			is_pressed = true;
			Invalidate();
			kevent.Handled = true;
		}
		base.OnKeyDown(kevent);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
	/// <param name="kevent">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyUp(KeyEventArgs kevent)
	{
		if (kevent.KeyData == Keys.Space)
		{
			is_pressed = false;
			Invalidate();
			OnClick(EventArgs.Empty);
			kevent.Handled = true;
		}
		base.OnKeyUp(kevent);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnLostFocus(System.EventArgs)" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnLostFocus(EventArgs e)
	{
		Invalidate();
		base.OnLostFocus(e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs mevent)
	{
		if ((mevent.Button & MouseButtons.Left) != 0)
		{
			is_pressed = true;
			Invalidate();
		}
		base.OnMouseDown(mevent);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseEnter(System.EventArgs)" /> event.</summary>
	/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseEnter(EventArgs eventargs)
	{
		is_entered = true;
		Invalidate();
		base.OnMouseEnter(eventargs);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
	/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs eventargs)
	{
		is_entered = false;
		Invalidate();
		base.OnMouseLeave(eventargs);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseMove(MouseEventArgs mevent)
	{
		bool flag = false;
		bool flag2 = false;
		if (base.ClientRectangle.Contains(mevent.Location))
		{
			flag = true;
		}
		if ((mevent.Button & MouseButtons.Left) != 0 && base.Capture && flag != is_pressed)
		{
			is_pressed = flag;
			flag2 = true;
		}
		if (is_entered != flag)
		{
			is_entered = flag;
			flag2 = true;
		}
		if (flag2)
		{
			Invalidate();
		}
		base.OnMouseMove(mevent);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		if (base.Capture && (mevent.Button & MouseButtons.Left) != 0)
		{
			base.Capture = false;
			if (is_pressed)
			{
				is_pressed = false;
				Invalidate();
			}
			else if (flat_style == FlatStyle.Flat || flat_style == FlatStyle.Popup)
			{
				Invalidate();
			}
			if (base.ClientRectangle.Contains(mevent.Location) && !base.ValidationFailed)
			{
				OnClick(EventArgs.Empty);
				OnMouseClick(mevent);
			}
		}
		base.OnMouseUp(mevent);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
	/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs pevent)
	{
		Draw(pevent);
		base.OnPaint(pevent);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnTextChanged(EventArgs e)
	{
		Invalidate();
		base.OnTextChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnVisibleChanged(EventArgs e)
	{
		if (!base.Visible)
		{
			is_pressed = false;
			is_entered = false;
		}
		base.OnVisibleChanged(e);
	}

	/// <summary>Resets the <see cref="T:System.Windows.Forms.Button" /> control to the state before it is pressed and redraws it.</summary>
	protected void ResetFlagsandPaint()
	{
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message m)
	{
		switch ((Msg)m.Msg)
		{
		case Msg.WM_LBUTTONDBLCLK:
			HaveDoubleClick();
			break;
		case Msg.WM_MBUTTONDBLCLK:
			HaveDoubleClick();
			break;
		case Msg.WM_RBUTTONDBLCLK:
			HaveDoubleClick();
			break;
		}
		base.WndProc(ref m);
	}

	internal virtual void Draw(PaintEventArgs pevent)
	{
		ThemeEngine.Current.DrawButtonBase(pevent.Graphics, pevent.ClipRectangle, this);
	}

	internal virtual void HaveDoubleClick()
	{
	}

	internal override void OnPaintBackgroundInternal(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
	}
}
