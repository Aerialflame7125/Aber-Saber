using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides a user interface for indicating that a control on a form has an error associated with it.</summary>
/// <filterpriority>2</filterpriority>
[ProvideProperty("Error", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[ToolboxItemFilter("System.Windows.Forms")]
[ProvideProperty("IconAlignment", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[ProvideProperty("IconPadding", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[ComplexBindingProperties("DataSource", "DataMember")]
public class ErrorProvider : Component, ISupportInitialize, IExtenderProvider
{
	private class ErrorWindow : UserControl
	{
		public ErrorWindow()
		{
			SetStyle(ControlStyles.Selectable, value: false);
		}
	}

	private class ErrorProperty
	{
		public ErrorIconAlignment alignment;

		public int padding;

		public string text;

		public Control control;

		public ErrorProvider ep;

		private ErrorWindow window;

		private bool visible;

		private int blink_count;

		private EventHandler tick;

		private Timer timer;

		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				bool flag = text != value;
				text = value;
				if (text != string.Empty)
				{
					window.Visible = true;
					if (flag || ep.blinkstyle == ErrorBlinkStyle.AlwaysBlink)
					{
						if (timer == null)
						{
							timer = new Timer();
							timer.Tick += tick;
						}
						timer.Interval = ep.blinkrate;
						blink_count = 0;
						timer.Enabled = true;
					}
				}
				else
				{
					window.Visible = false;
				}
			}
		}

		public ErrorIconAlignment Alignment
		{
			get
			{
				return alignment;
			}
			set
			{
				if (alignment != value)
				{
					alignment = value;
					CalculateAlignment();
				}
			}
		}

		public int Padding
		{
			get
			{
				return padding;
			}
			set
			{
				if (padding != value)
				{
					padding = value;
					CalculateAlignment();
				}
			}
		}

		public ErrorProperty(ErrorProvider ep, Control control)
		{
			ErrorProperty errorProperty = this;
			this.ep = ep;
			this.control = control;
			alignment = ErrorIconAlignment.MiddleRight;
			padding = 0;
			text = string.Empty;
			blink_count = 0;
			tick = window_Tick;
			window = new ErrorWindow();
			window.Visible = false;
			window.Width = ep.icon.Width;
			window.Height = ep.icon.Height;
			OnUIAErrorProviderHookUp(ep, new ControlEventArgs(control));
			window.VisibleChanged += delegate
			{
				if (errorProperty.window.Visible)
				{
					OnUIAControlHookUp(control, new ControlEventArgs(errorProperty.window));
				}
				else
				{
					OnUIAControlUnhookUp(control, new ControlEventArgs(errorProperty.window));
				}
			};
			if (control.Parent != null)
			{
				OnUIAControlHookUp(control, new ControlEventArgs(window));
				control.Parent.Controls.Add(window);
				control.Parent.Controls.SetChildIndex(window, control.Parent.Controls.IndexOf(control) + 1);
			}
			window.Paint += window_Paint;
			window.MouseEnter += window_MouseEnter;
			window.MouseLeave += window_MouseLeave;
			control.SizeChanged += control_SizeLocationChanged;
			control.LocationChanged += control_SizeLocationChanged;
			control.ParentChanged += control_ParentChanged;
			CalculateAlignment();
		}

		private void CalculateAlignment()
		{
			if (visible)
			{
				visible = false;
				ep.tooltip.Visible = false;
			}
			switch (alignment)
			{
			case ErrorIconAlignment.TopLeft:
				window.Left = control.Left - ep.icon.Width - padding;
				window.Top = control.Top;
				break;
			case ErrorIconAlignment.TopRight:
				window.Left = control.Left + control.Width + padding;
				window.Top = control.Top;
				break;
			case ErrorIconAlignment.MiddleLeft:
				window.Left = control.Left - ep.icon.Width - padding;
				window.Top = control.Top + (control.Height - ep.icon.Height) / 2;
				break;
			case ErrorIconAlignment.MiddleRight:
				window.Left = control.Left + control.Width + padding;
				window.Top = control.Top + (control.Height - ep.icon.Height) / 2;
				break;
			case ErrorIconAlignment.BottomLeft:
				window.Left = control.Left - ep.icon.Width - padding;
				window.Top = control.Top + control.Height - ep.icon.Height;
				break;
			case ErrorIconAlignment.BottomRight:
				window.Left = control.Left + control.Width + padding;
				window.Top = control.Top + control.Height - ep.icon.Height;
				break;
			}
		}

		private void window_Paint(object sender, PaintEventArgs e)
		{
			if (text != string.Empty)
			{
				e.Graphics.DrawIcon(ep.icon, 0, 0);
			}
		}

		private void window_MouseEnter(object sender, EventArgs e)
		{
			if (!visible)
			{
				visible = true;
				Point mousePosition = Control.MousePosition;
				Size size = ThemeEngine.Current.ToolTipSize(ep.tooltip, text);
				ep.tooltip.Width = size.Width;
				ep.tooltip.Height = size.Height;
				ep.tooltip.Text = text;
				if (mousePosition.X + size.Width < SystemInformation.WorkingArea.Width)
				{
					ep.tooltip.Left = mousePosition.X;
				}
				else
				{
					ep.tooltip.Left = mousePosition.X - size.Width;
				}
				if (mousePosition.Y + size.Height < SystemInformation.WorkingArea.Height - 16)
				{
					ep.tooltip.Top = mousePosition.Y + 16;
				}
				else
				{
					ep.tooltip.Top = mousePosition.Y - size.Height;
				}
				ep.UIAControl = control;
				ep.tooltip.Visible = true;
			}
		}

		private void window_MouseLeave(object sender, EventArgs e)
		{
			if (visible)
			{
				visible = false;
				ep.tooltip.Visible = false;
			}
		}

		private void control_SizeLocationChanged(object sender, EventArgs e)
		{
			if (visible)
			{
				visible = false;
				ep.tooltip.Visible = false;
			}
			CalculateAlignment();
		}

		private void control_ParentChanged(object sender, EventArgs e)
		{
			if (control.Parent != null)
			{
				OnUIAControlUnhookUp(control, new ControlEventArgs(window));
				control.Parent.Controls.Add(window);
				control.Parent.Controls.SetChildIndex(window, control.Parent.Controls.IndexOf(control) + 1);
				OnUIAControlHookUp(control, new ControlEventArgs(window));
			}
		}

		private void window_Tick(object sender, EventArgs e)
		{
			if (!timer.Enabled || !control.IsHandleCreated || !control.Visible)
			{
				return;
			}
			blink_count++;
			Graphics graphics = window.CreateGraphics();
			if (blink_count % 2 == 0)
			{
				graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(window.Parent.BackColor), window.ClientRectangle);
			}
			else
			{
				graphics.DrawIcon(ep.icon, 0, 0);
			}
			graphics.Dispose();
			switch (ep.blinkstyle)
			{
			case ErrorBlinkStyle.BlinkIfDifferentError:
				if (blink_count > 10)
				{
					timer.Stop();
				}
				break;
			case ErrorBlinkStyle.NeverBlink:
				timer.Stop();
				break;
			}
			if (blink_count == 11)
			{
				blink_count = 1;
			}
		}
	}

	private int blinkrate;

	private ErrorBlinkStyle blinkstyle;

	private string datamember;

	private object datasource;

	private ContainerControl container;

	private Icon icon;

	private Hashtable controls;

	private ToolTip.ToolTipWindow tooltip;

	private bool right_to_left;

	private object tag;

	private static object RightToLeftChangedEvent;

	private Control uia_control;

	/// <summary>Gets or sets the rate at which the error icon flashes.</summary>
	/// <returns>The rate, in milliseconds, at which the error icon should flash. The default is 250 milliseconds.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(250)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int BlinkRate
	{
		get
		{
			return blinkrate;
		}
		set
		{
			blinkrate = value;
		}
	}

	/// <summary>Gets or sets a value indicating when the error icon flashes.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle" /> values. The default is <see cref="F:System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ErrorBlinkStyle.BlinkIfDifferentError)]
	public ErrorBlinkStyle BlinkStyle
	{
		get
		{
			return blinkstyle;
		}
		set
		{
			blinkstyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating the parent control for this <see cref="T:System.Windows.Forms.ErrorProvider" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContainerControl" /> that contains the controls that the <see cref="T:System.Windows.Forms.ErrorProvider" /> is attached to.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	public ContainerControl ContainerControl
	{
		get
		{
			return container;
		}
		set
		{
			container = value;
		}
	}

	/// <summary>Gets or sets the list within a data source to monitor.</summary>
	/// <returns>The string that represents a list within the data source specified by the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" /> to be monitored. Typically, this will be a <see cref="T:System.Data.DataTable" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	[System.MonoTODO("Stub, does nothing")]
	[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DataMember
	{
		get
		{
			return datamember;
		}
		set
		{
			datamember = value;
		}
	}

	/// <summary>Gets or sets the data source that the <see cref="T:System.Windows.Forms.ErrorProvider" /> monitors.</summary>
	/// <returns>A data source based on the <see cref="T:System.Collections.IList" /> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet" /> to be monitored for errors.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	[AttributeProvider(typeof(IListSource))]
	[System.MonoTODO("Stub, does nothing")]
	public object DataSource
	{
		get
		{
			return datasource;
		}
		set
		{
			datasource = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Drawing.Icon" /> that is displayed next to a control when an error description string has been set for the control.</summary>
	/// <returns>An <see cref="T:System.Drawing.Icon" /> that signals an error has occurred. The default icon consists of an exclamation point in a circle with a red background.</returns>
	/// <exception cref="T:System.ArgumentNullException">The assigned value of the <see cref="T:System.Drawing.Icon" /> is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public Icon Icon
	{
		get
		{
			return icon;
		}
		set
		{
			if (value != null && (value.Height != 16 || value.Width != 16))
			{
				icon = new Icon(value, 16, 16);
			}
			else
			{
				icon = value;
			}
		}
	}

	/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override ISite Site
	{
		set
		{
			base.Site = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the component is used in a locale that supports right-to-left fonts.</summary>
	/// <returns>true if the component is used in a right-to-left locale; otherwise, false.</returns>
	[System.MonoTODO("RTL not supported")]
	[Localizable(true)]
	[DefaultValue(false)]
	public virtual bool RightToLeft
	{
		get
		{
			return right_to_left;
		}
		set
		{
			right_to_left = value;
		}
	}

	/// <summary>Gets or sets an object that contains data about the component.</summary>
	/// <returns>An object that contains data about the control. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[Localizable(false)]
	[Bindable(true)]
	[MWFCategory("Data")]
	[DefaultValue(null)]
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

	internal Control UIAControl
	{
		get
		{
			return uia_control;
		}
		set
		{
			uia_control = value;
		}
	}

	internal Rectangle UIAToolTipRectangle => tooltip.Bounds;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ErrorProvider.RightToLeft" /> property changes value. </summary>
	public event EventHandler RightToLeftChanged
	{
		add
		{
			base.Events.AddHandler(RightToLeftChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RightToLeftChangedEvent, value);
		}
	}

	internal static event ControlEventHandler UIAControlHookUp;

	internal static event ControlEventHandler UIAControlUnhookUp;

	internal static event ControlEventHandler UIAErrorProviderHookUp;

	internal static event ControlEventHandler UIAErrorProviderUnhookUp;

	internal static event PopupEventHandler UIAPopup;

	internal static event PopupEventHandler UIAUnPopup;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class and initializes the default settings for <see cref="P:System.Windows.Forms.ErrorProvider.BlinkRate" />, <see cref="P:System.Windows.Forms.ErrorProvider.BlinkStyle" />, and the <see cref="P:System.Windows.Forms.ErrorProvider.Icon" />.</summary>
	public ErrorProvider()
	{
		controls = new Hashtable();
		blinkrate = 250;
		blinkstyle = ErrorBlinkStyle.BlinkIfDifferentError;
		icon = ResourceImageLoader.GetIcon("errorProvider.ico");
		tooltip = new ToolTip.ToolTipWindow();
		tooltip.VisibleChanged += delegate
		{
			if (tooltip.Visible)
			{
				OnUIAPopup(this, new PopupEventArgs(UIAControl, UIAControl, isBalloon: false, Size.Empty));
			}
			else if (!tooltip.Visible)
			{
				OnUIAUnPopup(this, new PopupEventArgs(UIAControl, UIAControl, isBalloon: false, Size.Empty));
			}
		};
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class attached to a container.</summary>
	/// <param name="parentControl">The container of the control to monitor for errors. </param>
	public ErrorProvider(ContainerControl parentControl)
		: this()
	{
		container = parentControl;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class attached to an <see cref="T:System.ComponentModel.IContainer" /> implementation.</summary>
	/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to monitor for errors.</param>
	public ErrorProvider(IContainer container)
		: this()
	{
		container.Add(this);
	}

	static ErrorProvider()
	{
		RightToLeftChanged = new object();
	}

	/// <summary>Signals the object that initialization is starting.</summary>
	void ISupportInitialize.BeginInit()
	{
	}

	/// <summary>Signals the object that initialization is complete.</summary>
	void ISupportInitialize.EndInit()
	{
	}

	/// <summary>Provides a method to set both the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" /> and <see cref="P:System.Windows.Forms.ErrorProvider.DataMember" /> at run time.</summary>
	/// <param name="newDataSource">A data set based on the <see cref="T:System.Collections.IList" /> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet" /> to be monitored for errors. </param>
	/// <param name="newDataMember">A collection within the <paramref name="newDataSource" /> to monitor for errors. Typically, this will be a <see cref="T:System.Data.DataTable" />. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Stub, does nothing")]
	public void BindToDataAndErrors(object newDataSource, string newDataMember)
	{
		datasource = newDataSource;
		datamember = newDataMember;
	}

	/// <summary>Gets a value indicating whether a control can be extended.</summary>
	/// <returns>true if the control can be extended; otherwise, false.This property will be true if the object is a <see cref="T:System.Windows.Forms.Control" /> and is not a <see cref="T:System.Windows.Forms.Form" /> or <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
	/// <param name="extendee">The control to be extended. </param>
	/// <filterpriority>1</filterpriority>
	public bool CanExtend(object extendee)
	{
		if (!(extendee is Control))
		{
			return false;
		}
		if (extendee is Form || extendee is ToolBar)
		{
			return false;
		}
		return true;
	}

	/// <summary>Clears all settings associated with this component.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Clear()
	{
		foreach (ErrorProperty value in controls.Values)
		{
			value.Text = string.Empty;
		}
	}

	/// <summary>Returns the current error description string for the specified control.</summary>
	/// <returns>The error description string for the specified control.</returns>
	/// <param name="control">The item to get the error description string for. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	[Localizable(true)]
	public string GetError(Control control)
	{
		return GetErrorProperty(control).Text;
	}

	/// <summary>Gets a value indicating where the error icon should be placed in relation to the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ErrorIconAlignment" /> values. The default icon alignment is <see cref="F:System.Windows.Forms.ErrorIconAlignment.MiddleRight" />.</returns>
	/// <param name="control">The control to get the icon location for. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ErrorIconAlignment.MiddleRight)]
	[Localizable(true)]
	public ErrorIconAlignment GetIconAlignment(Control control)
	{
		return GetErrorProperty(control).Alignment;
	}

	/// <summary>Returns the amount of extra space to leave next to the error icon.</summary>
	/// <returns>The number of pixels to leave between the icon and the control. </returns>
	/// <param name="control">The control to get the padding for. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(0)]
	[Localizable(true)]
	public int GetIconPadding(Control control)
	{
		return GetErrorProperty(control).padding;
	}

	/// <summary>Sets the error description string for the specified control.</summary>
	/// <param name="control">The control to set the error description string for. </param>
	/// <param name="value">The error description string, or null or <see cref="F:System.String.Empty" /> to remove the error.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetError(Control control, string value)
	{
		GetErrorProperty(control).Text = value;
	}

	/// <summary>Sets the location where the error icon should be placed in relation to the control.</summary>
	/// <param name="control">The control to set the icon location for. </param>
	/// <param name="value">One of the <see cref="T:System.Windows.Forms.ErrorIconAlignment" /> values. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetIconAlignment(Control control, ErrorIconAlignment value)
	{
		GetErrorProperty(control).Alignment = value;
	}

	/// <summary>Sets the amount of extra space to leave between the specified control and the error icon.</summary>
	/// <param name="control">The <paramref name="control" /> to set the padding for. </param>
	/// <param name="padding">The number of pixels to add between the icon and the <paramref name="control" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void SetIconPadding(Control control, int padding)
	{
		GetErrorProperty(control).Padding = padding;
	}

	/// <summary>Provides a method to update the bindings of the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" />, <see cref="P:System.Windows.Forms.ErrorProvider.DataMember" />, and the error text.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Stub, does nothing")]
	public void UpdateBinding()
	{
	}

	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ErrorProvider.RightToLeftChanged" /> event. </summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnRightToLeftChanged(EventArgs e)
	{
		((EventHandler)base.Events[RightToLeftChanged])?.Invoke(this, e);
	}

	private ErrorProperty GetErrorProperty(Control control)
	{
		ErrorProperty errorProperty = (ErrorProperty)controls[control];
		if (errorProperty == null)
		{
			errorProperty = new ErrorProperty(this, control);
			controls[control] = errorProperty;
		}
		return errorProperty;
	}

	internal static void OnUIAPopup(ErrorProvider sender, PopupEventArgs args)
	{
		if (ErrorProvider.UIAPopup != null)
		{
			ErrorProvider.UIAPopup(sender, args);
		}
	}

	internal static void OnUIAUnPopup(ErrorProvider sender, PopupEventArgs args)
	{
		if (ErrorProvider.UIAUnPopup != null)
		{
			ErrorProvider.UIAUnPopup(sender, args);
		}
	}

	internal static void OnUIAControlHookUp(object sender, ControlEventArgs args)
	{
		if (ErrorProvider.UIAControlHookUp != null)
		{
			ErrorProvider.UIAControlHookUp(sender, args);
		}
	}

	internal static void OnUIAControlUnhookUp(object sender, ControlEventArgs args)
	{
		if (ErrorProvider.UIAControlUnhookUp != null)
		{
			ErrorProvider.UIAControlUnhookUp(sender, args);
		}
	}

	internal static void OnUIAErrorProviderHookUp(object sender, ControlEventArgs args)
	{
		if (ErrorProvider.UIAErrorProviderHookUp != null)
		{
			ErrorProvider.UIAErrorProviderHookUp(sender, args);
		}
	}

	internal static void OnUIAErrorProviderUnhookUp(object sender, ControlEventArgs args)
	{
		if (ErrorProvider.UIAErrorProviderUnhookUp != null)
		{
			ErrorProvider.UIAErrorProviderUnhookUp(sender, args);
		}
	}
}
