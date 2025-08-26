using System.ComponentModel;
using System.Threading;

namespace System.Windows.Forms;

/// <summary>Implements a timer that raises an event at user-defined intervals. This timer is optimized for use in Windows Forms applications and must be used in a window.</summary>
/// <filterpriority>1</filterpriority>
[DefaultProperty("Interval")]
[DefaultEvent("Tick")]
[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow)]
public class Timer : Component
{
	private bool enabled;

	private int interval = 100;

	private DateTime expires;

	internal Thread thread;

	internal bool Busy;

	internal IntPtr window;

	private object control_tag;

	internal static readonly int Minimum = 15;

	/// <summary>Gets or sets whether the timer is running.</summary>
	/// <returns>true if the timer is currently enabled; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public virtual bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			if (value != enabled)
			{
				enabled = value;
				if (value)
				{
					expires = DateTime.UtcNow.AddMilliseconds((interval <= Minimum) ? Minimum : interval);
					thread = Thread.CurrentThread;
					XplatUI.SetTimer(this);
				}
				else
				{
					XplatUI.KillTimer(this);
					thread = null;
				}
			}
		}
	}

	/// <summary>Gets or sets the time, in milliseconds, before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
	/// <returns>An <see cref="T:System.Int32" /> specifying the number of milliseconds before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event. The value cannot be less than one.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(100)]
	public int Interval
	{
		get
		{
			return interval;
		}
		set
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException("Interval", $"'{value}' is not a valid value for Interval. Interval must be greater than 0.");
			}
			if (interval != value)
			{
				interval = value;
				expires = DateTime.UtcNow.AddMilliseconds((interval <= Minimum) ? Minimum : interval);
				if (enabled)
				{
					XplatUI.KillTimer(this);
					XplatUI.SetTimer(this);
				}
			}
		}
	}

	/// <summary>Gets or sets an arbitrary string representing some type of user state.</summary>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[MWFCategory("Data")]
	[Localizable(false)]
	[Bindable(true)]
	[DefaultValue(null)]
	public object Tag
	{
		get
		{
			return control_tag;
		}
		set
		{
			control_tag = value;
		}
	}

	internal DateTime Expires => expires;

	/// <summary>Occurs when the specified timer interval has elapsed and the timer is enabled.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Tick;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class.</summary>
	public Timer()
	{
		enabled = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class together with the specified container.</summary>
	/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the timer. </param>
	public Timer(IContainer container)
		: this()
	{
		container.Add(this);
	}

	/// <summary>Starts the timer.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Start()
	{
		Enabled = true;
	}

	/// <summary>Stops the timer.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Stop()
	{
		Enabled = false;
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.Timer" />.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Timer" />. </returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Interval: " + Interval;
	}

	internal void Update(DateTime update)
	{
		expires = update.AddMilliseconds((interval <= Minimum) ? Minimum : interval);
	}

	internal void FireTick()
	{
		OnTick(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. This is always <see cref="F:System.EventArgs.Empty" />. </param>
	protected virtual void OnTick(EventArgs e)
	{
		if (this.Tick != null)
		{
			this.Tick(this, e);
		}
	}

	/// <summary>Disposes of the resources, other than memory, used by the timer.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources. false to release only the unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		Enabled = false;
	}

	internal void TickHandler(object sender, EventArgs e)
	{
		OnTick(e);
	}
}
