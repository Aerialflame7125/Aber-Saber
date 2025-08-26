using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Encapsulates properties related to scrolling. </summary>
/// <filterpriority>2</filterpriority>
public abstract class ScrollProperties
{
	private ScrollableControl parentControl;

	internal ScrollBar scroll_bar;

	/// <summary>Gets or sets whether the scroll bar can be used on the container.</summary>
	/// <returns>true if the scroll bar can be used; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool Enabled
	{
		get
		{
			return scroll_bar.Enabled;
		}
		set
		{
			scroll_bar.Enabled = value;
		}
	}

	/// <summary>Gets or sets the distance to move a scroll bar in response to a large scroll command. </summary>
	/// <returns>An <see cref="T:System.Int32" /> describing how far, in pixels, to move the scroll bar in response to a large change.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <see cref="P:System.Windows.Forms.ScrollProperties.LargeChange" /> cannot be less than zero. </exception>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(10)]
	public int LargeChange
	{
		get
		{
			return scroll_bar.LargeChange;
		}
		set
		{
			scroll_bar.LargeChange = value;
		}
	}

	/// <summary>Gets or sets the upper limit of the scrollable range. </summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the maximum range of the scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(100)]
	public int Maximum
	{
		get
		{
			return scroll_bar.Maximum;
		}
		set
		{
			scroll_bar.Maximum = value;
		}
	}

	/// <summary>Gets or sets the lower limit of the scrollable range. </summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the lower range of the scroll bar.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <see cref="P:System.Windows.Forms.ScrollProperties.Minimum" /> cannot be less than zero. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(0)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int Minimum
	{
		get
		{
			return scroll_bar.Minimum;
		}
		set
		{
			scroll_bar.Minimum = value;
		}
	}

	/// <summary>Gets or sets the distance to move a scroll bar in response to a small scroll command. </summary>
	/// <returns>An <see cref="T:System.Int32" /> representing how far, in pixels, to move the scroll bar.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(1)]
	public int SmallChange
	{
		get
		{
			return scroll_bar.SmallChange;
		}
		set
		{
			scroll_bar.SmallChange = value;
		}
	}

	/// <summary>Gets or sets a numeric value that represents the current position of the scroll bar box.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the position of the scroll bar box, in pixels. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true)]
	[DefaultValue(0)]
	public int Value
	{
		get
		{
			return scroll_bar.Value;
		}
		set
		{
			scroll_bar.Value = value;
		}
	}

	/// <summary>Gets or sets whether the scroll bar can be seen by the user.</summary>
	/// <returns>true if it can be seen; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Visible
	{
		get
		{
			return scroll_bar.Visible;
		}
		set
		{
			scroll_bar.Visible = value;
		}
	}

	/// <summary>Gets the control to which this scroll information applies.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl" />.</returns>
	protected ScrollableControl ParentControl => parentControl;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollProperties" /> class. </summary>
	/// <param name="container">The <see cref="T:System.Windows.Forms.ScrollableControl" /> whose scrolling properties this object describes.</param>
	protected ScrollProperties(ScrollableControl container)
	{
		parentControl = container;
	}
}
