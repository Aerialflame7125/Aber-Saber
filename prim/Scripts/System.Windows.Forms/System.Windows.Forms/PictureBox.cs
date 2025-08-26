using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows picture box control for displaying an image.</summary>
/// <filterpriority>1</filterpriority>
[ComVisible(true)]
[DefaultProperty("Image")]
[Designer("System.Windows.Forms.Design.PictureBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Docking(DockingBehavior.Ask)]
[DefaultBindingProperty("Image")]
public class PictureBox : Control, ISupportInitialize
{
	private Image image;

	private PictureBoxSizeMode size_mode;

	private Image error_image;

	private string image_location;

	private Image initial_image;

	private bool wait_on_load;

	private WebClient image_download;

	private bool image_from_url;

	private int no_update;

	private EventHandler frame_handler;

	private static object LoadCompletedEvent;

	private static object LoadProgressChangedEvent;

	private static object SizeModeChangedEvent;

	/// <summary>Indicates how the image is displayed.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.PictureBoxSizeMode.Normal" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.PictureBoxSizeMode" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(PictureBoxSizeMode.Normal)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Localizable(true)]
	public PictureBoxSizeMode SizeMode
	{
		get
		{
			return size_mode;
		}
		set
		{
			if (size_mode != value)
			{
				size_mode = value;
				if (size_mode == PictureBoxSizeMode.AutoSize)
				{
					AutoSize = true;
					SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
				}
				else
				{
					AutoSize = false;
					SetAutoSizeMode(AutoSizeMode.GrowOnly);
				}
				UpdateSize();
				if (no_update == 0)
				{
					Invalidate();
				}
				OnSizeModeChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the image that is displayed by <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> to display.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[Bindable(true)]
	public Image Image
	{
		get
		{
			return image;
		}
		set
		{
			ChangeImage(value, from_url: false);
		}
	}

	/// <summary>Indicates the border style for the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> enumeration values. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
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

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property.</summary>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets the image to display when an error occurs during the image-loading process or if the image load is canceled.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> to display if an error occurs during the image-loading process or if the image load is canceled.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	public Image ErrorImage
	{
		get
		{
			return error_image;
		}
		set
		{
			error_image = value;
		}
	}

	/// <summary>Gets or sets the image displayed in the <see cref="T:System.Windows.Forms.PictureBox" /> control when the main image is loading.</summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> displayed in the picture box control when the main image is loading.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	public Image InitialImage
	{
		get
		{
			return initial_image;
		}
		set
		{
			initial_image = value;
		}
	}

	/// <summary>Gets or sets the path or URL for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
	/// <returns>The path or URL for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[DefaultValue(null)]
	[Localizable(true)]
	public string ImageLocation
	{
		get
		{
			return image_location;
		}
		set
		{
			image_location = value;
			if (!string.IsNullOrEmpty(value))
			{
				if (WaitOnLoad)
				{
					Load(value);
				}
				else
				{
					LoadAsync(value);
				}
			}
			else if (image_from_url)
			{
				ChangeImage(null, from_url: true);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether an image is loaded synchronously.</summary>
	/// <returns>true if an image-loading operation is completed synchronously, otherwise, false. The default is false.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(false)]
	[Localizable(true)]
	public bool WaitOnLoad
	{
		get
		{
			return wait_on_load;
		}
		set
		{
			wait_on_load = value;
		}
	}

	/// <summary>Gets or sets the Input Method Editor(IME) mode supported by this control.</summary>
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

	/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left languages.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
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
			base.RightToLeft = value;
		}
	}

	/// <summary>Gets or sets the tab index value.</summary>
	/// <returns>The tab index value.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
	/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
	/// <returns>The text of the <see cref="T:System.Windows.Forms.PictureBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[Bindable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets a value indicating the mode for Input Method Editor (IME) for the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
	/// <returns>Always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
	protected override ImeMode DefaultImeMode => base.DefaultImeMode;

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.Font" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.AllowDrop" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.PictureBoxDefaultSize;

	private WebClient ImageDownload
	{
		get
		{
			if (image_download == null)
			{
				image_download = new WebClient();
			}
			return image_download;
		}
	}

	/// <summary>Overrides the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Overrides the <see cref="E:System.Windows.Forms.Control.Enter" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.Font" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.ForeColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.ImeMode" /> property changes.</summary>
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

	/// <summary>Occurs when a key is pressed when the control has focus.</summary>
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

	/// <summary>Occurs when a key is pressed when the control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when a key is released when the control has focus. </summary>
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

	/// <summary>Occurs when input focus leaves the <see cref="T:System.Windows.Forms.PictureBox" />. </summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the asynchronous image-load operation is completed, been canceled, or raised an exception.</summary>
	/// <filterpriority>1</filterpriority>
	public event AsyncCompletedEventHandler LoadCompleted
	{
		add
		{
			base.Events.AddHandler(LoadCompletedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LoadCompletedEvent, value);
		}
	}

	/// <summary>Occurs when the progress of an asynchronous image-loading operation has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event ProgressChangedEventHandler LoadProgressChanged
	{
		add
		{
			base.Events.AddHandler(LoadProgressChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LoadProgressChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.RightToLeft" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.TabIndex" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.TabStop" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PictureBox.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Occurs when <see cref="P:System.Windows.Forms.PictureBox.SizeMode" /> changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SizeModeChanged
	{
		add
		{
			base.Events.AddHandler(SizeModeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SizeModeChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PictureBox" /> class.</summary>
	public PictureBox()
	{
		no_update = 0;
		SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		SetStyle(ControlStyles.Opaque, value: false);
		SetStyle(ControlStyles.Selectable, value: false);
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		base.HandleCreated += PictureBox_HandleCreated;
		initial_image = ResourceImageLoader.Get("image-x-generic.png");
		error_image = ResourceImageLoader.Get("image-missing.png");
	}

	static PictureBox()
	{
		LoadCompleted = new object();
		LoadProgressChanged = new object();
		SizeModeChanged = new object();
	}

	/// <summary>Signals the object that initialization is starting.</summary>
	void ISupportInitialize.BeginInit()
	{
		no_update++;
	}

	/// <summary>Signals to the object that initialization is complete.</summary>
	void ISupportInitialize.EndInit()
	{
		if (no_update > 0)
		{
			no_update--;
		}
		if (no_update == 0)
		{
			Invalidate();
		}
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PictureBox" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release managed and unmanaged resources; false to release unmanaged resources only.</param>
	protected override void Dispose(bool disposing)
	{
		if (image != null)
		{
			StopAnimation();
			image = null;
		}
		initial_image = null;
		base.Dispose(disposing);
	}

	/// <param name="pe"></param>
	protected override void OnPaint(PaintEventArgs pe)
	{
		ThemeEngine.Current.DrawPictureBox(pe.Graphics, pe.ClipRectangle, this);
		base.OnPaint(pe);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnVisibleChanged(EventArgs e)
	{
		base.OnVisibleChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.SizeModeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSizeModeChanged(EventArgs e)
	{
		((EventHandler)base.Events[SizeModeChanged])?.Invoke(this, e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.LoadCompleted" /> event.</summary>
	/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data. </param>
	protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
	{
		((AsyncCompletedEventHandler)base.Events[LoadCompleted])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.PictureBox.LoadProgressChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> that contains the event data.</param>
	protected virtual void OnLoadProgressChanged(ProgressChangedEventArgs e)
	{
		((ProgressChangedEventHandler)base.Events[LoadProgressChanged])?.Invoke(this, e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		Invalidate();
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		if (image == null)
		{
			return base.GetPreferredSizeCore(proposedSize);
		}
		return image.Size;
	}

	private void ChangeImage(Image value, bool from_url)
	{
		StopAnimation();
		image_from_url = from_url;
		image = value;
		if (base.IsHandleCreated)
		{
			UpdateSize();
			if (image != null && ImageAnimator.CanAnimate(image))
			{
				frame_handler = OnAnimateImage;
				ImageAnimator.Animate(image, frame_handler);
			}
			if (no_update == 0)
			{
				Invalidate();
			}
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

	private void UpdateSize()
	{
		if (image != null && base.Parent != null)
		{
			base.Parent.PerformLayout(this, "AutoSize");
		}
	}

	private void OnAnimateImage(object sender, EventArgs e)
	{
		if (base.IsHandleCreated)
		{
			BeginInvoke(new EventHandler(UpdateAnimatedImage), this, e);
		}
	}

	private void UpdateAnimatedImage(object sender, EventArgs e)
	{
		if (base.IsHandleCreated)
		{
			ImageAnimator.UpdateFrames(image);
			Refresh();
		}
	}

	private void PictureBox_HandleCreated(object sender, EventArgs e)
	{
		UpdateSize();
		if (image != null && ImageAnimator.CanAnimate(image))
		{
			frame_handler = OnAnimateImage;
			ImageAnimator.Animate(image, frame_handler);
		}
		if (no_update == 0)
		{
			Invalidate();
		}
	}

	private void ImageDownload_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
	{
		if (e.Error != null && !e.Cancelled)
		{
			Image = error_image;
		}
		else if (e.Error == null && !e.Cancelled)
		{
			using MemoryStream stream = new MemoryStream(e.Result);
			Image = Image.FromStream(stream);
		}
		ImageDownload.DownloadProgressChanged -= ImageDownload_DownloadProgressChanged;
		ImageDownload.DownloadDataCompleted -= ImageDownload_DownloadDataCompleted;
		image_download = null;
		OnLoadCompleted(e);
	}

	private void ImageDownload_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		OnLoadProgressChanged(new ProgressChangedEventArgs(e.ProgressPercentage, e.UserState));
	}

	/// <summary>Cancels an asynchronous image load.</summary>
	/// <filterpriority>2</filterpriority>
	public void CancelAsync()
	{
		if (image_download != null)
		{
			image_download.CancelAsync();
		}
	}

	/// <summary>Displays the image specified by the <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> property of the <see cref="T:System.Windows.Forms.PictureBox" />.</summary>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> is null or an empty string.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Load()
	{
		Load(image_location);
	}

	/// <summary>Sets the <see cref="P:System.Windows.Forms.PictureBox.ImageLocation" /> to the specified URL and displays the image indicated.</summary>
	/// <param name="url">The path for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="url" /> is null or an empty string.</exception>
	/// <exception cref="T:System.Net.WebException">
	///   <paramref name="url" /> refers to an image on the Web that cannot be accessed.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="url" /> refers to a file that is not an image.</exception>
	/// <exception cref="T:System.IO.FileNotFoundException">
	///   <paramref name="url" /> refers to a file that does not exist.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Load(string url)
	{
		if (string.IsNullOrEmpty(url))
		{
			throw new InvalidOperationException("ImageLocation not specified.");
		}
		image_location = url;
		if (url.Contains("://"))
		{
			using (Stream stream = ImageDownload.OpenRead(url))
			{
				ChangeImage(Image.FromStream(stream), from_url: true);
				return;
			}
		}
		ChangeImage(Image.FromFile(url), from_url: true);
	}

	/// <summary>Loads the image asynchronously.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	/// </PermissionSet>
	public void LoadAsync()
	{
		LoadAsync(image_location);
	}

	/// <summary>Loads the image at the specified location, asynchronously.</summary>
	/// <param name="url">The path for the image to display in the <see cref="T:System.Windows.Forms.PictureBox" />.</param>
	/// <filterpriority>2</filterpriority>
	public void LoadAsync(string url)
	{
		if (wait_on_load)
		{
			Load(url);
			return;
		}
		if (string.IsNullOrEmpty(url))
		{
			throw new InvalidOperationException("ImageLocation not specified.");
		}
		image_location = url;
		ChangeImage(InitialImage, from_url: true);
		if (ImageDownload.IsBusy)
		{
			ImageDownload.CancelAsync();
		}
		Uri uri = null;
		try
		{
			uri = new Uri(url);
		}
		catch (UriFormatException)
		{
			uri = new Uri(Path.GetFullPath(url));
		}
		ImageDownload.DownloadProgressChanged += ImageDownload_DownloadProgressChanged;
		ImageDownload.DownloadDataCompleted += ImageDownload_DownloadDataCompleted;
		ImageDownload.DownloadDataAsync(uri);
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.PictureBox" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.PictureBox" />. </returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return $"{base.ToString()}, SizeMode: {SizeMode}";
	}
}
