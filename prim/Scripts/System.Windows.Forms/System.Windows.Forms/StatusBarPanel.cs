using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a panel in a <see cref="T:System.Windows.Forms.StatusBar" /> control. Although the <see cref="T:System.Windows.Forms.StatusStrip" /> control replaces and adds functionality to the <see cref="T:System.Windows.Forms.StatusBar" /> control of previous versions, <see cref="T:System.Windows.Forms.StatusBar" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxItem(false)]
[DefaultProperty("Text")]
[DesignTimeVisible(false)]
public class StatusBarPanel : Component, ISupportInitialize
{
	private StatusBar parent;

	private bool initializing;

	private string text = string.Empty;

	private string tool_tip_text = string.Empty;

	private Icon icon;

	private HorizontalAlignment alignment;

	private StatusBarPanelAutoSize auto_size = StatusBarPanelAutoSize.None;

	private StatusBarPanelBorderStyle border_style = StatusBarPanelBorderStyle.Sunken;

	private StatusBarPanelStyle style = StatusBarPanelStyle.Text;

	private int width = 100;

	private int min_width = 10;

	internal int X;

	private string name;

	private object tag;

	private static object UIATextChangedEvent;

	/// <summary>Gets or sets the alignment of text and icons within the status bar panel.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(HorizontalAlignment.Left)]
	public HorizontalAlignment Alignment
	{
		get
		{
			return alignment;
		}
		set
		{
			alignment = value;
			InvalidateContents();
		}
	}

	/// <summary>Gets or sets a value indicating whether the status bar panel is automatically resized.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelAutoSize.None" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[DefaultValue(StatusBarPanelAutoSize.None)]
	public StatusBarPanelAutoSize AutoSize
	{
		get
		{
			return auto_size;
		}
		set
		{
			auto_size = value;
			Invalidate();
		}
	}

	/// <summary>Gets or sets the border style of the status bar panel.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelBorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelBorderStyle.Sunken" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelBorderStyle" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(StatusBarPanelBorderStyle.Sunken)]
	[DispId(-504)]
	public StatusBarPanelBorderStyle BorderStyle
	{
		get
		{
			return border_style;
		}
		set
		{
			border_style = value;
			Invalidate();
		}
	}

	/// <summary>Gets or sets the icon to display within the status bar panel.</summary>
	/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon to display in the panel.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(null)]
	public Icon Icon
	{
		get
		{
			return icon;
		}
		set
		{
			icon = value;
			InvalidateContents();
		}
	}

	/// <summary>Gets or sets the minimum allowed width of the status bar panel within the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
	/// <returns>The minimum width, in pixels, of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
	/// <exception cref="T:System.ArgumentException">A value less than 0 is assigned to the property. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	[DefaultValue(10)]
	public int MinWidth
	{
		get
		{
			return min_width;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			min_width = value;
			if (min_width > width)
			{
				width = min_width;
			}
			Invalidate();
		}
	}

	/// <summary>Gets or sets the name to apply to the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </summary>
	/// <returns>The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	public string Name
	{
		get
		{
			if (name == null)
			{
				return string.Empty;
			}
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Gets or sets the width of the status bar panel within the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
	/// <returns>The width, in pixels, of the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
	/// <exception cref="T:System.ArgumentException">The width specified is less than the value of the <see cref="P:System.Windows.Forms.StatusBarPanel.MinWidth" /> property. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(100)]
	[Localizable(true)]
	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException("value");
			}
			if (initializing)
			{
				width = value;
			}
			else
			{
				SetWidth(value);
			}
			Invalidate();
		}
	}

	/// <summary>Gets or sets the style of the status bar panel.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.StatusBarPanelStyle" /> values. The default is <see cref="F:System.Windows.Forms.StatusBarPanelStyle.Text" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.StatusBarPanelStyle" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(StatusBarPanelStyle.Text)]
	public StatusBarPanelStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			style = value;
			Invalidate();
		}
	}

	/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(false)]
	[Bindable(true)]
	[DefaultValue(null)]
	[TypeConverter(typeof(StringConverter))]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	/// <summary>Gets or sets the text of the status bar panel.</summary>
	/// <returns>The text displayed in the panel.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue("")]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
			InvalidateContents();
			OnUIATextChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets ToolTip text associated with the status bar panel.</summary>
	/// <returns>The ToolTip text for the panel.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue("")]
	public string ToolTipText
	{
		get
		{
			return tool_tip_text;
		}
		set
		{
			tool_tip_text = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBar" /> control that hosts the status bar panel.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.StatusBar" /> that contains the panel.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public StatusBar Parent => parent;

	internal event EventHandler UIATextChanged
	{
		add
		{
			base.Events.AddHandler(UIATextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIATextChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> class.</summary>
	public StatusBarPanel()
	{
	}

	static StatusBarPanel()
	{
		UIATextChanged = new object();
	}

	internal void OnUIATextChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIATextChanged])?.Invoke(this, e);
	}

	private void Invalidate()
	{
		if (parent != null)
		{
			parent.UpdatePanel(this);
		}
	}

	private void InvalidateContents()
	{
		if (parent != null)
		{
			parent.UpdatePanelContents(this);
		}
	}

	internal void SetParent(StatusBar parent)
	{
		this.parent = parent;
	}

	internal void SetWidth(int width)
	{
		this.width = width;
		if (min_width > this.width)
		{
			this.width = min_width;
		}
	}

	/// <summary>Retrieves a string that contains information about the panel.</summary>
	/// <returns>Returns a string that contains the class name for the control and the text it contains.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return "StatusBarPanel: {" + Text + "}";
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.StatusBarPanel" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
	}

	/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
	/// <filterpriority>1</filterpriority>
	public void BeginInit()
	{
		initializing = true;
	}

	/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void EndInit()
	{
		if (initializing)
		{
			if (min_width > width)
			{
				width = min_width;
			}
			initializing = false;
		}
	}
}
