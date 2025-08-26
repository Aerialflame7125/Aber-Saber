using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Threading;

namespace System.Timers;

/// <summary>Generates an event after a set interval, with an option to generate recurring events.</summary>
[DefaultProperty("Interval")]
[DefaultEvent("Elapsed")]
[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
public class Timer : Component, ISupportInitialize
{
	private double interval;

	private bool enabled;

	private bool initializing;

	private bool delayedEnable;

	private ElapsedEventHandler onIntervalElapsed;

	private bool autoReset;

	private ISynchronizeInvoke synchronizingObject;

	private bool disposed;

	private System.Threading.Timer timer;

	private TimerCallback callback;

	private object cookie;

	/// <summary>Gets or sets a Boolean indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once (<see langword="false" />) or repeatedly (<see langword="true" />).</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the interval elapses; <see langword="false" /> if it should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once, after the first time the interval elapses. The default is <see langword="true" />.</returns>
	[Category("Behavior")]
	[TimersDescription("Indicates whether the timer will be restarted when it is enabled.")]
	[DefaultValue(true)]
	public bool AutoReset
	{
		get
		{
			return autoReset;
		}
		set
		{
			if (base.DesignMode)
			{
				autoReset = value;
			}
			else if (autoReset != value)
			{
				autoReset = value;
				if (timer != null)
				{
					UpdateTimer();
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.ObjectDisposedException">This property cannot be set because the timer has been disposed.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Timers.Timer.Interval" /> property was set to a value greater than <see cref="F:System.Int32.MaxValue" /> before the timer was enabled.</exception>
	[Category("Behavior")]
	[TimersDescription("Indicates whether the timer is enabled to fire events at a defined interval.")]
	[DefaultValue(false)]
	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			if (base.DesignMode)
			{
				delayedEnable = value;
				enabled = value;
			}
			else if (initializing)
			{
				delayedEnable = value;
			}
			else
			{
				if (enabled == value)
				{
					return;
				}
				if (!value)
				{
					if (timer != null)
					{
						cookie = null;
						timer.Dispose();
						timer = null;
					}
					enabled = value;
					return;
				}
				enabled = value;
				if (timer == null)
				{
					if (disposed)
					{
						throw new ObjectDisposedException(GetType().Name);
					}
					int num = CalculateRoundedInterval(interval);
					cookie = new object();
					timer = new System.Threading.Timer(callback, cookie, num, autoReset ? num : (-1));
				}
				else
				{
					UpdateTimer();
				}
			}
		}
	}

	/// <summary>Gets or sets the interval, expressed in milliseconds, at which to raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	/// <returns>The time, in milliseconds, between <see cref="E:System.Timers.Timer.Elapsed" /> events. The value must be greater than zero, and less than or equal to <see cref="F:System.Int32.MaxValue" />. The default is 100 milliseconds.</returns>
	/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero.  
	///  -or-  
	///  The interval is greater than <see cref="F:System.Int32.MaxValue" />, and the timer is currently enabled. (If the timer is not currently enabled, no exception is thrown until it becomes enabled.)</exception>
	[Category("Behavior")]
	[TimersDescription("The number of milliseconds between timer events.")]
	[DefaultValue(100.0)]
	[SettingsBindable(true)]
	public double Interval
	{
		get
		{
			return interval;
		}
		set
		{
			if (value <= 0.0)
			{
				throw new ArgumentException(global::SR.GetString("'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}.", value, 0));
			}
			interval = value;
			if (timer != null)
			{
				UpdateTimer();
			}
		}
	}

	/// <summary>Gets or sets the site that binds the <see cref="T:System.Timers.Timer" /> to its container in design mode.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.ISite" /> interface representing the site that binds the <see cref="T:System.Timers.Timer" /> object to its container.</returns>
	public override ISite Site
	{
		get
		{
			return base.Site;
		}
		set
		{
			base.Site = value;
			if (base.DesignMode)
			{
				enabled = true;
			}
		}
	}

	/// <summary>Gets or sets the object used to marshal event-handler calls that are issued when an interval has elapsed.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> representing the object used to marshal the event-handler calls that are issued when an interval has elapsed. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TimersDescription("The object used to marshal the event handler calls issued when an interval has elapsed.")]
	public ISynchronizeInvoke SynchronizingObject
	{
		get
		{
			if (synchronizingObject == null && base.DesignMode)
			{
				IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					object rootComponent = designerHost.RootComponent;
					if (rootComponent != null && rootComponent is ISynchronizeInvoke)
					{
						synchronizingObject = (ISynchronizeInvoke)rootComponent;
					}
				}
			}
			return synchronizingObject;
		}
		set
		{
			synchronizingObject = value;
		}
	}

	/// <summary>Occurs when the interval elapses.</summary>
	[Category("Behavior")]
	[TimersDescription("Occurs when the Interval has elapsed.")]
	public event ElapsedEventHandler Elapsed
	{
		add
		{
			onIntervalElapsed = (ElapsedEventHandler)Delegate.Combine(onIntervalElapsed, value);
		}
		remove
		{
			onIntervalElapsed = (ElapsedEventHandler)Delegate.Remove(onIntervalElapsed, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets all the properties to their initial values.</summary>
	public Timer()
	{
		interval = 100.0;
		enabled = false;
		autoReset = true;
		initializing = false;
		delayedEnable = false;
		callback = MyTimerCallback;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets the <see cref="P:System.Timers.Timer.Interval" /> property to the specified number of milliseconds.</summary>
	/// <param name="interval">The time, in milliseconds, between events. The value must be greater than zero and less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
	/// <exception cref="T:System.ArgumentException">The value of the <paramref name="interval" /> parameter is less than or equal to zero, or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
	public Timer(double interval)
		: this()
	{
		if (interval <= 0.0)
		{
			throw new ArgumentException(global::SR.GetString("Invalid value '{1}' for parameter '{0}'.", "interval", interval));
		}
		this.interval = CalculateRoundedInterval(interval, argumentCheck: true);
	}

	private static int CalculateRoundedInterval(double interval, bool argumentCheck = false)
	{
		double num = Math.Ceiling(interval);
		if (num > 2147483647.0 || num <= 0.0)
		{
			if (argumentCheck)
			{
				throw new ArgumentException(global::SR.GetString("Invalid value '{1}' for parameter '{0}'.", "interval", interval));
			}
			throw new ArgumentOutOfRangeException(global::SR.GetString("Invalid value '{1}' for parameter '{0}'.", "interval", interval));
		}
		return (int)num;
	}

	private void UpdateTimer()
	{
		int num = CalculateRoundedInterval(interval);
		timer.Change(num, autoReset ? num : (-1));
	}

	/// <summary>Begins the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
	public void BeginInit()
	{
		Close();
		initializing = true;
	}

	/// <summary>Releases the resources used by the <see cref="T:System.Timers.Timer" />.</summary>
	public void Close()
	{
		initializing = false;
		delayedEnable = false;
		enabled = false;
		if (timer != null)
		{
			timer.Dispose();
			timer = null;
		}
	}

	/// <summary>Releases all resources used by the current <see cref="T:System.Timers.Timer" />.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		Close();
		disposed = true;
		base.Dispose(disposing);
	}

	/// <summary>Ends the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
	public void EndInit()
	{
		initializing = false;
		Enabled = delayedEnable;
	}

	/// <summary>Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to <see langword="true" />.</summary>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Timers.Timer" /> is created with an interval equal to or greater than <see cref="F:System.Int32.MaxValue" /> + 1, or set to an interval less than zero.</exception>
	public void Start()
	{
		Enabled = true;
	}

	/// <summary>Stops raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to <see langword="false" />.</summary>
	public void Stop()
	{
		Enabled = false;
	}

	private void MyTimerCallback(object state)
	{
		if (state != cookie)
		{
			return;
		}
		if (!autoReset)
		{
			enabled = false;
		}
		ElapsedEventArgs elapsedEventArgs = new ElapsedEventArgs(DateTime.Now);
		try
		{
			ElapsedEventHandler elapsedEventHandler = onIntervalElapsed;
			if (elapsedEventHandler != null)
			{
				if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
				{
					SynchronizingObject.BeginInvoke(elapsedEventHandler, new object[2] { this, elapsedEventArgs });
				}
				else
				{
					elapsedEventHandler(this, elapsedEventArgs);
				}
			}
		}
		catch
		{
		}
	}
}
