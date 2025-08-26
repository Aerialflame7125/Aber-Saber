using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows progress bar control.</summary>
/// <filterpriority>1</filterpriority>
[DefaultBindingProperty("Value")]
[ComVisible(true)]
[DefaultProperty("Value")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ProgressBar : Control
{
	private int maximum;

	private int minimum;

	internal int step;

	internal int val;

	internal DateTime start = DateTime.Now;

	internal Rectangle client_area = default(Rectangle);

	internal ProgressBarStyle style;

	private Timer marquee_timer;

	private bool right_to_left_layout;

	private static readonly Color defaultForeColor = SystemColors.Highlight;

	private static object RightToLeftLayoutChangedEvent;

	private int marquee_animation_speed = 100;

	/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.AllowDrop" />.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the layout of the background image of the progress bar.</summary>
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

	/// <summary>Gets or sets a value indicating whether the control, when it receives focus, causes validation to be performed on any controls that require validation.</summary>
	/// <returns>true if the control, when it receives focus, causes validation to be performed on any controls that require validation; otherwise, false. The default is true.</returns>
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

	/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
	protected override ImeMode DefaultImeMode => base.DefaultImeMode;

	/// <summary>Gets the default size of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the default size of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.ProgressBarDefaultSize;

	/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer.</summary>
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

	/// <summary>Gets or sets the font of text in the <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> of the text. The default is the font set by the container.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets the input method editor (IME) for the <see cref="T:System.Windows.Forms.ProgressBar" /></summary>
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

	/// <summary>Gets or sets the maximum value of the range of the control.</summary>
	/// <returns>The maximum value of the range. The default is 100.</returns>
	/// <exception cref="T:System.ArgumentException">The value specified is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(100)]
	public int Maximum
	{
		get
		{
			return maximum;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Maximum", $"Value '{value}' must be greater than or equal to 0.");
			}
			maximum = value;
			minimum = Math.Min(minimum, maximum);
			val = Math.Min(val, maximum);
			Refresh();
		}
	}

	/// <summary>Gets or sets the minimum value of the range of the control.</summary>
	/// <returns>The minimum value of the range. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentException">The value specified for the property is less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int Minimum
	{
		get
		{
			return minimum;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Minimum", $"Value '{value}' must be greater than or equal to 0.");
			}
			minimum = value;
			maximum = Math.Max(maximum, minimum);
			val = Math.Max(val, minimum);
			Refresh();
		}
	}

	/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.ProgressBar" /> control and its contents.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ProgressBar" /> and any text it contains is displayed from right to left. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ProgressBar" /> is displayed from right to left; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	[System.MonoTODO("RTL is not supported")]
	[Localizable(true)]
	public virtual bool RightToLeftLayout
	{
		get
		{
			return right_to_left_layout;
		}
		set
		{
			if (right_to_left_layout != value)
			{
				right_to_left_layout = value;
				OnRightToLeftLayoutChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the amount by which a call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method increases the current position of the progress bar.</summary>
	/// <returns>The amount by which to increment the progress bar with each call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method. The default is 10.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(10)]
	public int Step
	{
		get
		{
			return step;
		}
		set
		{
			step = value;
			Refresh();
		}
	}

	/// <summary>Gets or sets the manner in which progress should be indicated on the progress bar.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" /></returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a member of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(true)]
	[DefaultValue(ProgressBarStyle.Blocks)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public ProgressBarStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			if (value != 0 && value != ProgressBarStyle.Continuous && value != ProgressBarStyle.Marquee)
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(ProgressBarStyle));
			}
			if (style == value)
			{
				return;
			}
			style = value;
			if (style == ProgressBarStyle.Marquee)
			{
				if (marquee_timer == null)
				{
					marquee_timer = new Timer();
					marquee_timer.Interval = 10;
					marquee_timer.Tick += marquee_timer_Tick;
				}
				marquee_timer.Start();
			}
			else
			{
				if (marquee_timer != null)
				{
					marquee_timer.Stop();
				}
				Refresh();
			}
		}
	}

	/// <summary>Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</summary>
	/// <returns>The time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The indicated time period is less than 0.</exception>
	[DefaultValue(100)]
	public int MarqueeAnimationSpeed
	{
		get
		{
			return marquee_animation_speed;
		}
		set
		{
			marquee_animation_speed = value;
		}
	}

	/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.TabStop" />.</summary>
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

	/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.Text" />.</summary>
	/// <filterpriority>1</filterpriority>
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
			base.Text = value;
		}
	}

	/// <summary>Gets or sets the current position of the progress bar.</summary>
	/// <returns>The position within the range of the progress bar. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentException">The value specified is greater than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Maximum" /> property.-or- The value specified is less than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Minimum" /> property. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	[Bindable(true)]
	public int Value
	{
		get
		{
			return val;
		}
		set
		{
			if (value < Minimum || value > Maximum)
			{
				throw new ArgumentOutOfRangeException("Value", $"'{value}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'");
			}
			val = value;
			Refresh();
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImage" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImageLayout" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.CausesValidation" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the user double-clicks the control.</summary>
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

	/// <summary>Occurs when focus enters the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Font" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.ImeMode" /> property changes.</summary>
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

	/// <summary>Occurs when the user presses a key while the control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the user presses a key while the control has focus.</summary>
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

	/// <summary>Occurs when the user releases a key while the control has focus.</summary>
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

	/// <summary>Occurs when focus leaves the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the user double-clicks the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Padding" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ProgressBar" /> is drawn.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.RightToLeftLayout" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.TabStop" /> property changes.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.Text" /> property changes.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ProgressBar" /> class.</summary>
	public ProgressBar()
	{
		maximum = 100;
		minimum = 0;
		step = 10;
		val = 0;
		base.Resize += OnResizeTB;
		SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UseTextForAccessibility, value: false);
		force_double_buffer = true;
		ForeColor = defaultForeColor;
	}

	static ProgressBar()
	{
		RightToLeftLayoutChanged = new object();
	}

	private void marquee_timer_Tick(object sender, EventArgs e)
	{
		Invalidate();
	}

	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <summary>Advances the current position of the progress bar by the specified amount.</summary>
	/// <param name="value">The amount by which to increment the progress bar's current position. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ProgressBar.Style" /> property is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /></exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Increment(int value)
	{
		if (Style == ProgressBarStyle.Marquee)
		{
			throw new InvalidOperationException("Increment should not be called if the style is Marquee.");
		}
		int num = Value + value;
		if (num < Minimum)
		{
			num = Minimum;
		}
		if (num > Maximum)
		{
			num = Maximum;
		}
		Value = num;
		Refresh();
	}

	/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /></summary>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
		UpdateAreas();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnBackColorChanged(EventArgs e)
	{
		base.OnBackColorChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnForeColorChanged(EventArgs e)
	{
		base.OnForeColorChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <summary>Raises the <see cref="P:System.Windows.Forms.ProgressBar.RightToLeftLayout" /> event. </summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
	{
		((EventHandler)base.Events[RightToLeftLayoutChanged])?.Invoke(this, e);
	}

	/// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ProgressBar.Step" /> property.</summary>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.ProgressBar.Style" /> is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void PerformStep()
	{
		if (Style == ProgressBarStyle.Marquee)
		{
			throw new InvalidOperationException("PerformStep should not be called if the style is Marquee.");
		}
		Increment(Step);
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> to its default value.</summary>
	/// <filterpriority>2</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void ResetForeColor()
	{
		ForeColor = defaultForeColor;
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ProgressBar" />. </returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return $"{GetType().FullName}, Minimum: {Minimum.ToString()}, Maximum: {Maximum.ToString()}, Value: {Value.ToString()}";
	}

	private void UpdateAreas()
	{
		ref Rectangle reference = ref client_area;
		int num = 2;
		client_area.Y = num;
		reference.X = num;
		client_area.Width = base.Width - 4;
		client_area.Height = base.Height - 4;
	}

	private void OnResizeTB(object o, EventArgs e)
	{
		if (base.Width > 0 && base.Height > 0)
		{
			UpdateAreas();
			Invalidate();
		}
	}

	internal override void OnPaintInternal(PaintEventArgs pevent)
	{
		ThemeEngine.Current.DrawProgressBar(pevent.Graphics, pevent.ClipRectangle, this);
	}
}
