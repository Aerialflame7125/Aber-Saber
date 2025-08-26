using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents a Windows progress bar control contained in a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
/// <filterpriority>2</filterpriority>
[DefaultProperty("Value")]
public class ToolStripProgressBar : ToolStripControlHost
{
	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets or sets a value representing the delay between each <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /> display update, in milliseconds.</summary>
	/// <returns>An integer representing the delay, in milliseconds.</returns>
	[DefaultValue(100)]
	public int MarqueeAnimationSpeed
	{
		get
		{
			return ProgressBar.MarqueeAnimationSpeed;
		}
		set
		{
			ProgressBar.MarqueeAnimationSpeed = value;
		}
	}

	/// <summary>Gets or sets the upper bound of the range that is defined for this <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
	/// <returns>An integer representing the upper bound of the range. The default is 100.</returns>
	[DefaultValue(100)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int Maximum
	{
		get
		{
			return ProgressBar.Maximum;
		}
		set
		{
			ProgressBar.Maximum = value;
		}
	}

	/// <summary>Gets or sets the lower bound of the range that is defined for this <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
	/// <returns>An integer representing the lower bound of the range. The default is 0.</returns>
	[DefaultValue(0)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int Minimum
	{
		get
		{
			return ProgressBar.Minimum;
		}
		set
		{
			ProgressBar.Minimum = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ProgressBar" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ProgressBar ProgressBar => (ProgressBar)base.Control;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> layout is right-to-left or left-to-right when the <see cref="T:System.Windows.Forms.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />. </summary>
	/// <returns>true to turn on mirroring and lay out control from right to left when the <see cref="T:System.Windows.Forms.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	[Localizable(true)]
	public virtual bool RightToLeftLayout
	{
		get
		{
			return ProgressBar.RightToLeftLayout;
		}
		set
		{
			ProgressBar.RightToLeftLayout = value;
		}
	}

	/// <summary>Gets or sets the amount by which to increment the current value of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> when the <see cref="M:System.Windows.Forms.ToolStripProgressBar.PerformStep" /> method is called.</summary>
	/// <returns>An integer representing the incremental amount. The default value is 10.</returns>
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
			return ProgressBar.Step;
		}
		set
		{
			ProgressBar.Step = value;
		}
	}

	/// <summary>Gets or sets the style of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default value is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ProgressBarStyle.Blocks)]
	public ProgressBarStyle Style
	{
		get
		{
			return ProgressBar.Style;
		}
		set
		{
			ProgressBar.Style = value;
		}
	}

	/// <summary>Gets or sets the text displayed on the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the display text.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets or sets the current value of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
	/// <returns>An integer representing the current value.</returns>
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
			return ProgressBar.Value;
		}
		set
		{
			ProgressBar.Value = value;
		}
	}

	/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> and adjacent items.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
	protected internal override Padding DefaultMargin => new Padding(1, 2, 1, 1);

	/// <summary>Gets the height and width of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> in pixels.</summary>
	/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> value representing the height and width.</returns>
	protected override Size DefaultSize => new Size(100, 15);

	/// <summary>This event is not relevant for this class.</summary>
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

	/// <summary>This event is not relevant for this class.</summary>
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

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler LocationChanged
	{
		add
		{
			base.LocationChanged += value;
		}
		remove
		{
			base.LocationChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler OwnerChanged
	{
		add
		{
			base.OwnerChanged += value;
		}
		remove
		{
			base.OwnerChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripProgressBar.RightToLeftLayout" /> property changes.</summary>
	public event EventHandler RightToLeftLayoutChanged
	{
		add
		{
			ProgressBar.RightToLeftLayoutChanged += value;
		}
		remove
		{
			ProgressBar.RightToLeftLayoutChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
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

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> class. </summary>
	public ToolStripProgressBar()
		: base(new ProgressBar())
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> class with specified name. </summary>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</param>
	public ToolStripProgressBar(string name)
		: this()
	{
		base.Name = name;
	}

	/// <summary>Advances the current position of the progress bar by the specified amount.</summary>
	/// <param name="value">The amount by which to increment the progress bar's current position.</param>
	public void Increment(int value)
	{
		ProgressBar.Increment(value);
	}

	/// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ToolStripProgressBar.Step" /> property.</summary>
	public void PerformStep()
	{
		ProgressBar.PerformStep();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ProgressBar.RightToLeftLayoutChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
	{
	}

	/// <param name="control">The control from which to subscribe events.</param>
	protected override void OnSubscribeControlEvents(Control control)
	{
		base.OnSubscribeControlEvents(control);
	}

	/// <param name="control">The control from which to unsubscribe events.</param>
	protected override void OnUnsubscribeControlEvents(Control control)
	{
		base.OnUnsubscribeControlEvents(control);
	}
}
