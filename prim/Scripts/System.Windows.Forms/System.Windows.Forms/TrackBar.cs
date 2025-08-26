using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;

namespace System.Windows.Forms;

/// <summary>Represents a standard Windows track bar.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Designer("System.Windows.Forms.Design.TrackBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultEvent("Scroll")]
[DefaultProperty("Value")]
[DefaultBindingProperty("Value")]
[ComVisible(true)]
public class TrackBar : Control, ISupportInitialize
{
	private const int size_of_autosize = 45;

	private int minimum;

	private int maximum;

	internal int tickFrequency;

	private bool autosize;

	private int position;

	private int smallChange;

	private int largeChange;

	private Orientation orientation;

	private TickStyle tickStyle;

	private Rectangle thumb_pos = default(Rectangle);

	private Rectangle thumb_area = default(Rectangle);

	internal bool thumb_pressed;

	private System.Timers.Timer holdclick_timer = new System.Timers.Timer();

	internal int thumb_mouseclick;

	private bool mouse_clickmove;

	private bool is_moving_right;

	internal int mouse_down_x_offset;

	internal bool mouse_moved;

	private bool right_to_left_layout;

	private bool thumb_entered;

	private static object RightToLeftLayoutChangedEvent;

	private static object ScrollEvent;

	private static object ValueChangedEvent;

	private static object UIAValueParamChangedEvent;

	internal Rectangle ThumbPos
	{
		get
		{
			return thumb_pos;
		}
		set
		{
			thumb_pos = value;
		}
	}

	internal Rectangle ThumbArea
	{
		get
		{
			return thumb_area;
		}
		set
		{
			thumb_area = value;
		}
	}

	internal bool ThumbEntered
	{
		get
		{
			return thumb_entered;
		}
		set
		{
			if (thumb_entered != value)
			{
				thumb_entered = value;
				if (ThemeEngine.Current.TrackBarHasHotThumbStyle)
				{
					Invalidate(GetRealThumbRectangle());
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the height or width of the track bar is being automatically sized.</summary>
	/// <returns>true if the track bar is being automatically sized; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[DefaultValue(true)]
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
			autosize = value;
		}
	}

	/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.TrackBar" />.</returns>
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

	/// <summary>Gets or sets an <see cref="T:System.Windows.Forms.ImageLayout" /> value; however, setting this property has no effect on the <see cref="T:System.Windows.Forms.TrackBar" /> control. </summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
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

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets a value indicating the mode for the Input Method Editor (IME) for the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
	/// <returns>Always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
	protected override ImeMode DefaultImeMode => ImeMode.Disable;

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the default size of the control. </returns>
	protected override Size DefaultSize => ThemeEngine.Current.TrackBarDefaultSize;

	/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker; however, this property has no effect on the <see cref="T:System.Windows.Forms.TrackBar" /> control </summary>
	/// <returns>true if the control has a secondary buffer; otherwise, false.</returns>
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

	/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.Font" /></summary>
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

	/// <summary>Gets the foreground color of the track bar.</summary>
	/// <returns>Always <see cref="P:System.Drawing.SystemColors.WindowText" />.</returns>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Gets or sets a value to be added to or subtracted from the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property when the scroll box is moved a large distance.</summary>
	/// <returns>A numeric value. The default is 5.</returns>
	/// <exception cref="T:System.ArgumentException">The assigned value is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(5)]
	public int LargeChange
	{
		get
		{
			return largeChange;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException($"Value '{value}' must be greater than or equal to 0.");
			}
			largeChange = value;
			OnUIAValueParamChanged();
		}
	}

	/// <summary>Gets or sets the upper limit of the range this <see cref="T:System.Windows.Forms.TrackBar" /> is working with.</summary>
	/// <returns>The maximum value for the <see cref="T:System.Windows.Forms.TrackBar" />. The default is 10.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(10)]
	[RefreshProperties(RefreshProperties.All)]
	public int Maximum
	{
		get
		{
			return maximum;
		}
		set
		{
			if (maximum != value)
			{
				maximum = value;
				if (maximum < minimum)
				{
					minimum = maximum;
				}
				Refresh();
				OnUIAValueParamChanged();
			}
		}
	}

	/// <summary>Gets or sets the lower limit of the range this <see cref="T:System.Windows.Forms.TrackBar" /> is working with.</summary>
	/// <returns>The minimum value for the <see cref="T:System.Windows.Forms.TrackBar" />. The default is 0.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	[RefreshProperties(RefreshProperties.All)]
	public int Minimum
	{
		get
		{
			return minimum;
		}
		set
		{
			if (Minimum != value)
			{
				minimum = value;
				if (minimum > maximum)
				{
					maximum = minimum;
				}
				Refresh();
				OnUIAValueParamChanged();
			}
		}
	}

	/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the track bar.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Orientation" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(Orientation.Horizontal)]
	public Orientation Orientation
	{
		get
		{
			return orientation;
		}
		set
		{
			if (!Enum.IsDefined(typeof(Orientation), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for Orientation");
			}
			if (orientation != value)
			{
				orientation = value;
				if (base.IsHandleCreated)
				{
					base.Size = new Size(base.Height, base.Width);
					Refresh();
				}
			}
		}
	}

	/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.TrackBar" /> control and its contents.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the contents of the <see cref="T:System.Windows.Forms.TrackBar" /> will be laid out from right to left.</summary>
	/// <returns>true if the contents of the <see cref="T:System.Windows.Forms.TrackBar" /> are laid out from right to left; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	[Localizable(true)]
	public virtual bool RightToLeftLayout
	{
		get
		{
			return right_to_left_layout;
		}
		set
		{
			if (value != right_to_left_layout)
			{
				right_to_left_layout = value;
				OnRightToLeftLayoutChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the value added to or subtracted from the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property when the scroll box is moved a small distance.</summary>
	/// <returns>A numeric value. The default value is 1.</returns>
	/// <exception cref="T:System.ArgumentException">The assigned value is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(1)]
	public int SmallChange
	{
		get
		{
			return smallChange;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException($"Value '{value}' must be greater than or equal to 0.");
			}
			if (smallChange != value)
			{
				smallChange = value;
				OnUIAValueParamChanged();
			}
		}
	}

	/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
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

	/// <summary>Gets or sets a value that specifies the delta between ticks drawn on the control.</summary>
	/// <returns>The numeric value representing the delta between ticks. The default is 1.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(1)]
	public int TickFrequency
	{
		get
		{
			return tickFrequency;
		}
		set
		{
			if (value > 0)
			{
				tickFrequency = value;
				Refresh();
			}
		}
	}

	/// <summary>Gets or sets a value indicating how to display the tick marks on the track bar.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TickStyle" /> values. The default is <see cref="F:System.Windows.Forms.TickStyle.BottomRight" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not a valid <see cref="T:System.Windows.Forms.TickStyle" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(TickStyle.BottomRight)]
	public TickStyle TickStyle
	{
		get
		{
			return tickStyle;
		}
		set
		{
			if (!Enum.IsDefined(typeof(TickStyle), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for TickStyle");
			}
			if (tickStyle != value)
			{
				tickStyle = value;
				Refresh();
			}
		}
	}

	/// <summary>Gets or sets a numeric value that represents the current position of the scroll box on the track bar.</summary>
	/// <returns>A numeric value that is within the <see cref="P:System.Windows.Forms.TrackBar.Minimum" /> and <see cref="P:System.Windows.Forms.TrackBar.Maximum" /> range. The default value is 0.</returns>
	/// <exception cref="T:System.ArgumentException">The assigned value is less than the value of <see cref="P:System.Windows.Forms.TrackBar.Minimum" />.-or- The assigned value is greater than the value of <see cref="P:System.Windows.Forms.TrackBar.Maximum" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true)]
	[DefaultValue(0)]
	public int Value
	{
		get
		{
			return position;
		}
		set
		{
			SetValue(value, fire_onscroll: false);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.AutoSize" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.BackgroundImage" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.BackgroundImageLayout" /> property changes.</summary>
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

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler Click
	{
		add
		{
			base.Click += value;
		}
		remove
		{
			base.Click -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DoubleClick
	{
		add
		{
			base.DoubleClick += value;
		}
		remove
		{
			base.DoubleClick -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Font" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.ForeColor" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.ImeMode" /> property changes.</summary>
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

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseClick
	{
		add
		{
			base.MouseClick += value;
		}
		remove
		{
			base.MouseClick -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event MouseEventHandler MouseDoubleClick
	{
		add
		{
			base.MouseDoubleClick += value;
		}
		remove
		{
			base.MouseDoubleClick -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.Padding" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler PaddingChanged
	{
		add
		{
			base.PaddingChanged += value;
		}
		remove
		{
			base.PaddingChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TrackBar" /> control is drawn.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.RightToLeftLayout" /> property changes.</summary>
	public event EventHandler RightToLeftLayoutChanged
	{
		add
		{
			base.Events.AddHandler(RightToLeftLayoutChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RightToLeftLayoutChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Text" /> property changes.</summary>
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

	/// <summary>Occurs when either a mouse or keyboard action moves the scroll box.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Scroll
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property of a track bar changes, either by movement of the scroll box or by manipulation in code.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ValueChanged
	{
		add
		{
			base.Events.AddHandler(ValueChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ValueChangedEvent, value);
		}
	}

	internal event EventHandler UIAValueParamChanged
	{
		add
		{
			base.Events.AddHandler(UIAValueParamChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAValueParamChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TrackBar" /> class.</summary>
	public TrackBar()
	{
		orientation = Orientation.Horizontal;
		minimum = 0;
		maximum = 10;
		tickFrequency = 1;
		autosize = true;
		position = 0;
		tickStyle = TickStyle.BottomRight;
		smallChange = 1;
		largeChange = 5;
		mouse_clickmove = false;
		base.MouseDown += OnMouseDownTB;
		base.MouseUp += OnMouseUpTB;
		base.MouseMove += OnMouseMoveTB;
		base.MouseLeave += OnMouseLeave;
		base.KeyDown += OnKeyDownTB;
		base.LostFocus += OnLostFocusTB;
		base.GotFocus += OnGotFocusTB;
		holdclick_timer.Elapsed += OnFirstClickTimer;
		SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.UseTextForAccessibility, value: false);
	}

	static TrackBar()
	{
		RightToLeftLayoutChanged = new object();
		Scroll = new object();
		ValueChanged = new object();
		UIAValueParamChanged = new object();
	}

	internal void OnUIAValueParamChanged()
	{
		((EventHandler)base.Events[UIAValueParamChanged])?.Invoke(this, EventArgs.Empty);
	}

	private void SetValue(int value, bool fire_onscroll)
	{
		if (value < Minimum || value > Maximum)
		{
			throw new ArgumentException($"'{value}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'");
		}
		if (position != value)
		{
			position = value;
			if (fire_onscroll)
			{
				OnScroll(EventArgs.Empty);
			}
			((EventHandler)base.Events[ValueChanged])?.Invoke(this, EventArgs.Empty);
			Invalidate(thumb_area);
		}
	}

	/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.TrackBar" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
	/// <filterpriority>1</filterpriority>
	public void BeginInit()
	{
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> method.</summary>
	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <summary>Overrides Control.setBoundsCore to enforce autoSize.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		if (AutoSize)
		{
			if (orientation == Orientation.Vertical)
			{
				width = 45;
			}
			else
			{
				height = 45;
			}
		}
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.TrackBar" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void EndInit()
	{
	}

	/// <summary>Handles special input keys, such as PAGE UP, PAGE DOWN, HOME, and END.</summary>
	/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
	protected override bool IsInputKey(Keys keyData)
	{
		if ((keyData & Keys.Alt) == 0)
		{
			switch (keyData & Keys.KeyCode)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			}
		}
		return base.IsInputKey(keyData);
	}

	/// <summary>This method is called by the control when any property changes. Inheriting controls can override this method to get property change notification on basic properties. Inheriting controls must call base.propertyChanged.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
	}

	/// <summary>Use the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
		if (AutoSize)
		{
			if (Orientation == Orientation.Horizontal)
			{
				base.Size = new Size(base.Width, 40);
			}
			else
			{
				base.Size = new Size(50, base.Height);
			}
		}
		UpdatePos(Value, update_trumbpos: true);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnMouseWheel(MouseEventArgs e)
	{
		base.OnMouseWheel(e);
		if (base.Enabled)
		{
			if (e.Delta > 0)
			{
				SmallDecrement();
			}
			else
			{
				SmallIncrement();
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.RightToLeftLayoutChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" />  that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
	{
		((EventHandler)base.Events[RightToLeftLayoutChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.Scroll" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnScroll(EventArgs e)
	{
		((EventHandler)base.Events[Scroll])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnSystemColorsChanged(EventArgs e)
	{
		base.OnSystemColorsChanged(e);
		Invalidate();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.ValueChanged" /> event.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnValueChanged(EventArgs e)
	{
		((EventHandler)base.Events[ValueChanged])?.Invoke(this, e);
	}

	/// <summary>Sets the minimum and maximum values for a <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
	/// <param name="minValue">The lower limit of the range of the track bar. </param>
	/// <param name="maxValue">The upper limit of the range of the track bar. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetRange(int minValue, int maxValue)
	{
		Minimum = minValue;
		Maximum = maxValue;
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TrackBar" />. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override string ToString()
	{
		return $"System.Windows.Forms.TrackBar, Minimum: {Minimum}, Maximum: {Maximum}, Value: {Value}";
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
	/// <param name="m">A Windows Message object. </param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
		if (m.Msg == 71 && base.Visible)
		{
			Invalidate();
		}
	}

	private void UpdatePos(int newPos, bool update_trumbpos)
	{
		if (newPos < minimum)
		{
			SetValue(minimum, fire_onscroll: true);
		}
		else if (newPos > maximum)
		{
			SetValue(maximum, fire_onscroll: true);
		}
		else
		{
			SetValue(newPos, fire_onscroll: true);
		}
	}

	internal void LargeIncrement()
	{
		UpdatePos(position + LargeChange, update_trumbpos: true);
		Invalidate(thumb_area);
	}

	internal void LargeDecrement()
	{
		UpdatePos(position - LargeChange, update_trumbpos: true);
		Invalidate(thumb_area);
	}

	private void SmallIncrement()
	{
		UpdatePos(position + SmallChange, update_trumbpos: true);
		Invalidate(thumb_area);
	}

	private void SmallDecrement()
	{
		UpdatePos(position - SmallChange, update_trumbpos: true);
		Invalidate(thumb_area);
	}

	private void OnMouseUpTB(object sender, MouseEventArgs e)
	{
		if (base.Enabled && (thumb_pressed || mouse_clickmove))
		{
			thumb_pressed = false;
			holdclick_timer.Enabled = false;
			base.Capture = false;
			Invalidate(thumb_area);
		}
	}

	private void OnMouseDownTB(object sender, MouseEventArgs e)
	{
		if (!base.Enabled)
		{
			return;
		}
		mouse_moved = false;
		bool flag = false;
		Point pt = new Point(e.X, e.Y);
		if (orientation == Orientation.Horizontal)
		{
			if (thumb_pos.Contains(pt))
			{
				base.Capture = true;
				thumb_pressed = true;
				thumb_mouseclick = e.X;
				mouse_down_x_offset = e.X - thumb_pos.X;
				Invalidate(thumb_area);
			}
			else if (thumb_area.Contains(pt))
			{
				is_moving_right = e.X > thumb_pos.X + thumb_pos.Width;
				if (is_moving_right)
				{
					LargeIncrement();
				}
				else
				{
					LargeDecrement();
				}
				Invalidate(thumb_area);
				flag = true;
				mouse_clickmove = true;
			}
		}
		else
		{
			Rectangle rectangle = thumb_pos;
			rectangle.Width = thumb_pos.Height;
			rectangle.Height = thumb_pos.Width;
			if (rectangle.Contains(pt))
			{
				base.Capture = true;
				thumb_pressed = true;
				thumb_mouseclick = e.Y;
				mouse_down_x_offset = e.Y - thumb_pos.Y;
				Invalidate(thumb_area);
			}
			else if (thumb_area.Contains(pt))
			{
				is_moving_right = e.Y > thumb_pos.Y + thumb_pos.Width;
				if (is_moving_right)
				{
					LargeDecrement();
				}
				else
				{
					LargeIncrement();
				}
				Invalidate(thumb_area);
				flag = true;
				mouse_clickmove = true;
			}
		}
		if (flag)
		{
			holdclick_timer.Interval = 300.0;
			holdclick_timer.Enabled = true;
		}
	}

	private void OnMouseMoveTB(object sender, MouseEventArgs e)
	{
		if (base.Enabled)
		{
			mouse_moved = true;
			if (thumb_pressed)
			{
				SetValue(ThemeEngine.Current.TrackBarValueFromMousePosition(e.X, e.Y, this), fire_onscroll: true);
			}
			ThumbEntered = GetRealThumbRectangle().Contains(e.Location);
		}
	}

	private Rectangle GetRealThumbRectangle()
	{
		Rectangle result = thumb_pos;
		if (Orientation == Orientation.Vertical)
		{
			result.Width = thumb_pos.Height;
			result.Height = thumb_pos.Width;
		}
		return result;
	}

	internal override void OnPaintInternal(PaintEventArgs pevent)
	{
		ThemeEngine.Current.DrawTrackBar(pevent.Graphics, pevent.ClipRectangle, this);
	}

	private void OnLostFocusTB(object sender, EventArgs e)
	{
		Invalidate();
	}

	private void OnGotFocusTB(object sender, EventArgs e)
	{
		Invalidate();
	}

	private void OnKeyDownTB(object sender, KeyEventArgs e)
	{
		bool flag = Orientation == Orientation.Horizontal;
		switch (e.KeyCode)
		{
		case Keys.Right:
		case Keys.Down:
			if (flag)
			{
				SmallIncrement();
			}
			else
			{
				SmallDecrement();
			}
			break;
		case Keys.Left:
		case Keys.Up:
			if (flag)
			{
				SmallDecrement();
			}
			else
			{
				SmallIncrement();
			}
			break;
		case Keys.PageUp:
			if (flag)
			{
				LargeDecrement();
			}
			else
			{
				LargeIncrement();
			}
			break;
		case Keys.PageDown:
			if (flag)
			{
				LargeIncrement();
			}
			else
			{
				LargeDecrement();
			}
			break;
		case Keys.Home:
			if (flag)
			{
				SetValue(Minimum, fire_onscroll: true);
			}
			else
			{
				SetValue(Maximum, fire_onscroll: true);
			}
			break;
		case Keys.End:
			if (flag)
			{
				SetValue(Maximum, fire_onscroll: true);
			}
			else
			{
				SetValue(Minimum, fire_onscroll: true);
			}
			break;
		}
	}

	private void OnFirstClickTimer(object source, ElapsedEventArgs e)
	{
		Point pt = PointToClient(Control.MousePosition);
		if (!thumb_area.Contains(pt))
		{
			return;
		}
		bool flag = false;
		if (orientation == Orientation.Horizontal)
		{
			if (pt.X > thumb_pos.X + thumb_pos.Width && is_moving_right)
			{
				LargeIncrement();
				flag = true;
			}
			else if (pt.X < thumb_pos.X && !is_moving_right)
			{
				LargeDecrement();
				flag = true;
			}
		}
		else if (pt.Y > thumb_pos.Y + thumb_pos.Width && is_moving_right)
		{
			LargeDecrement();
			flag = true;
		}
		else if (pt.Y < thumb_pos.Y && !is_moving_right)
		{
			LargeIncrement();
			flag = true;
		}
		if (flag)
		{
			Refresh();
		}
	}

	private void OnMouseLeave(object sender, EventArgs e)
	{
		ThumbEntered = false;
	}
}
