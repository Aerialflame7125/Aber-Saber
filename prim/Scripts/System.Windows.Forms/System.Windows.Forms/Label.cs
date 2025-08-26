using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms;

/// <summary>Represents a standard Windows label. </summary>
/// <filterpriority>1</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultBindingProperty("Text")]
[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.LabelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("Text")]
public class Label : Control
{
	private bool autosize;

	private bool auto_ellipsis;

	private Image image;

	private bool render_transparent;

	private FlatStyle flat_style;

	private bool use_mnemonic;

	private int image_index = -1;

	private string image_key = string.Empty;

	private ImageList image_list;

	internal ContentAlignment image_align;

	internal StringFormat string_format;

	internal ContentAlignment text_align;

	private static SizeF req_witdthsize = new SizeF(0f, 0f);

	private static object AutoSizeChangedEvent;

	private static object TextAlignChangedEvent;

	/// <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the <see cref="T:System.Windows.Forms.Label" />, denoting that the <see cref="T:System.Windows.Forms.Label" /> text extends beyond the specified length of the <see cref="T:System.Windows.Forms.Label" />.</summary>
	/// <returns>true if the additional label text is to be indicated by an ellipsis; otherwise, false. The default is false.</returns>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
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
					string_format.Trimming = StringTrimming.EllipsisCharacter;
				}
				else
				{
					string_format.Trimming = StringTrimming.Character;
				}
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "AutoEllipsis");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
	/// <returns>true if the control adjusts its width to closely fit its contents; otherwise, false.Note:When added to a form using the designer, the default value is true. When instantiated from code, the default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool AutoSize
	{
		get
		{
			return autosize;
		}
		set
		{
			if (autosize != value)
			{
				SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
				base.AutoSize = value;
				autosize = value;
				CalcAutoSize();
				Invalidate();
				OnAutoSizeChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the image rendered on the background of the control.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the background image of the control. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
			Invalidate();
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
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

	/// <summary>Gets or sets the border style for the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(BorderStyle.None)]
	[DispId(-504)]
	public virtual BorderStyle BorderStyle
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

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			if (BorderStyle != BorderStyle.Fixed3D)
			{
				return createParams;
			}
			createParams.ExStyle &= -513;
			createParams.ExStyle |= 131072;
			return createParams;
		}
	}

	/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> supported by this control. The default is <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
	protected override ImeMode DefaultImeMode => ImeMode.Disable;

	/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value that represents the default space between controls.</returns>
	protected override Padding DefaultMargin => new Padding(3, 0, 3, 0);

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeElements.LabelPainter.DefaultSize;

	/// <summary>Gets or sets the flat style appearance of the label control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is Standard.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(FlatStyle.Standard)]
	public FlatStyle FlatStyle
	{
		get
		{
			return flat_style;
		}
		set
		{
			if (!Enum.IsDefined(typeof(FlatStyle), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for FlatStyle");
			}
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

	/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.Label" />.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> displayed on the <see cref="T:System.Windows.Forms.Label" />. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
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

	/// <summary>Gets or sets the alignment of an image that is displayed in the control.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is ContentAlignment.MiddleCenter.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ContentAlignment.MiddleCenter)]
	[Localizable(true)]
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
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the index value of the image displayed on the <see cref="T:System.Windows.Forms.Label" />.</summary>
	/// <returns>A zero-based index that represents the position in the <see cref="T:System.Windows.Forms.ImageList" /> control (assigned to the <see cref="P:System.Windows.Forms.Label.ImageList" /> property) where the image is located. The default is -1.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the lower bounds of the <see cref="P:System.Windows.Forms.Label.ImageIndex" /> property. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[TypeConverter(typeof(ImageIndexConverter))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue(-1)]
	public int ImageIndex
	{
		get
		{
			if (ImageList == null)
			{
				return -1;
			}
			if (image_index >= image_list.Images.Count)
			{
				return image_list.Images.Count - 1;
			}
			return image_index;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentException();
			}
			if (image_index != value)
			{
				image_index = value;
				image = null;
				image_key = string.Empty;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.Label.ImageList" />.</summary>
	/// <returns>A string representing the key of the image.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(ImageKeyConverter))]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue("")]
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
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ImageList" /> that contains the images to display in the <see cref="T:System.Windows.Forms.Label" /> control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that stores the collection of <see cref="T:System.Drawing.Image" /> objects. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	[RefreshProperties(RefreshProperties.Repaint)]
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
				if (image_list != null && image_index != -1)
				{
					Image = null;
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to this property is not within the range of valid values specified in the enumeration. </exception>
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

	/// <summary>Gets the preferred height of the control.</summary>
	/// <returns>The height of the control (in pixels), assuming a single line of text is displayed.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual int PreferredHeight => InternalGetPreferredSize(Size.Empty).Height;

	/// <summary>Gets the preferred width of the control.</summary>
	/// <returns>The width of the control (in pixels), assuming a single line of text is displayed.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual int PreferredWidth => InternalGetPreferredSize(Size.Empty).Width;

	/// <summary>Indicates whether the container control background is rendered on the <see cref="T:System.Windows.Forms.Label" />.</summary>
	/// <returns>true if the background of the <see cref="T:System.Windows.Forms.Label" /> control's container is rendered on the <see cref="T:System.Windows.Forms.Label" />; otherwise, false. The default is false.</returns>
	[Obsolete("This property has been deprecated.  Use BackColor instead.")]
	protected virtual bool RenderTransparent
	{
		get
		{
			return render_transparent;
		}
		set
		{
			render_transparent = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can tab to the <see cref="T:System.Windows.Forms.Label" />. This property is not used by this class.</summary>
	/// <returns>This property is not used by this class. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the alignment of text in the label.</summary>
	/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.TopLeft" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(ContentAlignment.TopLeft)]
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
				switch (value)
				{
				case ContentAlignment.BottomLeft:
					string_format.LineAlignment = StringAlignment.Far;
					string_format.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.BottomCenter:
					string_format.LineAlignment = StringAlignment.Far;
					string_format.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomRight:
					string_format.LineAlignment = StringAlignment.Far;
					string_format.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopLeft:
					string_format.LineAlignment = StringAlignment.Near;
					string_format.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopCenter:
					string_format.LineAlignment = StringAlignment.Near;
					string_format.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopRight:
					string_format.LineAlignment = StringAlignment.Near;
					string_format.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.MiddleLeft:
					string_format.LineAlignment = StringAlignment.Center;
					string_format.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					string_format.LineAlignment = StringAlignment.Center;
					string_format.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.MiddleCenter:
					string_format.LineAlignment = StringAlignment.Center;
					string_format.Alignment = StringAlignment.Center;
					break;
				}
				OnTextAlignChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control interprets an ampersand character (&amp;) in the control's <see cref="P:System.Windows.Forms.Control.Text" /> property to be an access key prefix character.</summary>
	/// <returns>true if the label doesn't display the ampersand character and underlines the character after the ampersand in its displayed text and treats the underlined character as an access key; otherwise, false if the ampersand character is displayed in the text of the control. The default is true.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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
				SetUseMnemonic(use_mnemonic);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
	/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool UseCompatibleTextRendering
	{
		get
		{
			return use_compatible_text_rendering;
		}
		set
		{
			use_compatible_text_rendering = value;
		}
	}

	/// <returns>The text associated with this control.</returns>
	/// <filterpriority>1</filterpriority>
	[SettingsBindable(true)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.AutoSize" /> property changes.</summary>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event EventHandler AutoSizeChanged
	{
		add
		{
			base.Events.AddHandler(AutoSizeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AutoSizeChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.BackgroundImage" /> property changes. </summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.BackgroundImageLayout" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.ImeMode" /> property changes.</summary>
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

	/// <summary>Occurs when the user presses a key while the label has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Occurs when the user presses a key while the label has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the user releases a key while the label has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Label.TabStop" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.TextAlign" /> property has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TextAlignChanged
	{
		add
		{
			base.Events.AddHandler(TextAlignChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextAlignChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Label" /> class.</summary>
	public Label()
	{
		autosize = false;
		TabStop = false;
		string_format = new StringFormat();
		string_format.FormatFlags = StringFormatFlags.LineLimit;
		TextAlign = ContentAlignment.TopLeft;
		image = null;
		UseMnemonic = true;
		image_list = null;
		image_align = ContentAlignment.MiddleCenter;
		SetUseMnemonic(UseMnemonic);
		flat_style = FlatStyle.Standard;
		SetStyle(ControlStyles.Selectable, value: false);
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		base.HandleCreated += OnHandleCreatedLB;
	}

	static Label()
	{
		AutoSizeChanged = new object();
		TextAlignChanged = new object();
	}

	internal virtual Size InternalGetPreferredSize(Size proposed)
	{
		Size result;
		if (Text == string.Empty)
		{
			result = new Size(0, Font.Height);
		}
		else
		{
			result = Size.Ceiling(TextRenderer.MeasureString(Text, Font, req_witdthsize, string_format));
			result.Width += 3;
		}
		result.Width += base.Padding.Horizontal;
		result.Height += base.Padding.Vertical;
		if (!use_compatible_text_rendering)
		{
			return result;
		}
		if (border_style == BorderStyle.None)
		{
			result.Height += 3;
		}
		else
		{
			result.Height += 6;
		}
		return result;
	}

	/// <summary>Retrieves the size of a rectangular area into which a control can be fitted. </summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="proposedSize">The custom-sized area for a control. </param>
	public override Size GetPreferredSize(Size proposedSize)
	{
		return InternalGetPreferredSize(proposedSize);
	}

	/// <summary>Determines the size and location of an image drawn within the <see cref="T:System.Windows.Forms.Label" /> control based on the alignment of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the specified image within the control.</returns>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> used to determine size and location when drawn within the control. </param>
	/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the area to draw the image in. </param>
	/// <param name="align">The alignment of content within the control. </param>
	protected Rectangle CalcImageRenderBounds(Image image, Rectangle r, ContentAlignment align)
	{
		Rectangle result = r;
		result.Inflate(-2, -2);
		int num = r.X;
		int num2 = r.Y;
		switch (align)
		{
		case ContentAlignment.TopCenter:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.BottomCenter:
			num += (r.Width - image.Width) / 2;
			break;
		case ContentAlignment.TopRight:
		case ContentAlignment.MiddleRight:
		case ContentAlignment.BottomRight:
			num += r.Width - image.Width;
			break;
		}
		switch (align)
		{
		case ContentAlignment.BottomLeft:
		case ContentAlignment.BottomCenter:
		case ContentAlignment.BottomRight:
			num2 += r.Height - image.Height;
			break;
		case ContentAlignment.MiddleLeft:
		case ContentAlignment.MiddleCenter:
		case ContentAlignment.MiddleRight:
			num2 += (r.Height - image.Height) / 2;
			break;
		}
		result.X = num;
		result.Y = num2;
		result.Width = image.Width;
		result.Height = image.Height;
		return result;
	}

	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return base.CreateAccessibilityInstance();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Label" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		if (disposing)
		{
			string_format.Dispose();
		}
	}

	/// <summary>Draws an <see cref="T:System.Drawing.Image" /> within the specified bounds.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw. </param>
	/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> bounds to draw within. </param>
	/// <param name="align">The alignment of the image to draw within the <see cref="T:System.Windows.Forms.Label" />. </param>
	protected internal void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
	{
		if (image != null && g != null)
		{
			Rectangle rectangle = CalcImageRenderBounds(image, r, align);
			if (base.Enabled)
			{
				g.DrawImage(image, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			}
			else
			{
				ControlPaint.DrawImageDisabled(g, image, rectangle.X, rectangle.Y, BackColor);
			}
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		if (autosize)
		{
			CalcAutoSize();
		}
		Invalidate();
	}

	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnPaddingChanged(EventArgs e)
	{
		base.OnPaddingChanged(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		ThemeElements.LabelPainter.Draw(e.Graphics, base.ClientRectangle, this);
		base.OnPaint(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.TextAlignChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnTextAlignChanged(EventArgs e)
	{
		((EventHandler)base.Events[TextAlignChanged])?.Invoke(this, e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		if (autosize)
		{
			CalcAutoSize();
		}
		Invalidate();
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnVisibleChanged(EventArgs e)
	{
		base.OnVisibleChanged(e);
	}

	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		if (Control.IsMnemonic(charCode, Text))
		{
			if (base.Parent != null)
			{
				base.Parent.SelectNextControl(this, forward: true, tabStopOnly: false, nested: false, wrap: false);
			}
			return true;
		}
		return base.ProcessMnemonic(charCode);
	}

	/// <summary>Sets the specified bounds of the label.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used. </param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Text: " + Text;
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message m)
	{
		Msg msg = (Msg)m.Msg;
		if (msg == Msg.WM_DRAWITEM)
		{
			m.Result = (IntPtr)1;
		}
		else
		{
			base.WndProc(ref m);
		}
	}

	private void CalcAutoSize()
	{
		if (AutoSize)
		{
			Size size = InternalGetPreferredSize(Size.Empty);
			SetBounds(base.Left, base.Top, size.Width, size.Height, BoundsSpecified.Size);
		}
	}

	private void OnHandleCreatedLB(object o, EventArgs e)
	{
		if (autosize)
		{
			CalcAutoSize();
		}
	}

	private void SetUseMnemonic(bool use)
	{
		if (use)
		{
			string_format.HotkeyPrefix = HotkeyPrefix.Show;
		}
		else
		{
			string_format.HotkeyPrefix = HotkeyPrefix.None;
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}
}
