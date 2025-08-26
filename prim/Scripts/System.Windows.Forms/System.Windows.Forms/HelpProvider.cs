using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides pop-up or online Help for controls.</summary>
/// <filterpriority>2</filterpriority>
[ProvideProperty("ShowHelp", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[ToolboxItemFilter("System.Windows.Forms")]
[ProvideProperty("HelpString", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[ProvideProperty("HelpKeyword", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[ProvideProperty("HelpNavigator", "System.Windows.Forms.Control, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
public class HelpProvider : Component, IExtenderProvider
{
	private class HelpProperty
	{
		internal string keyword;

		internal HelpNavigator navigator;

		internal string text;

		internal bool show;

		internal Control control;

		internal HelpProvider hp;

		public string Keyword
		{
			get
			{
				return keyword;
			}
			set
			{
				keyword = value;
			}
		}

		public HelpNavigator Navigator
		{
			get
			{
				return navigator;
			}
			set
			{
				navigator = value;
			}
		}

		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}

		public bool Show
		{
			get
			{
				return show;
			}
			set
			{
				show = value;
			}
		}

		public HelpProperty(HelpProvider hp, Control control)
		{
			this.control = control;
			this.hp = hp;
			keyword = null;
			navigator = HelpNavigator.AssociateIndex;
			text = null;
			show = false;
			control.HelpRequested += hp.HelpRequestHandler;
		}
	}

	private string helpnamespace;

	private Hashtable controls;

	private ToolTip.ToolTipWindow tooltip;

	private EventHandler HideToolTipHandler;

	private KeyPressEventHandler HideToolTipKeyHandler;

	private MouseEventHandler HideToolTipMouseHandler;

	private HelpEventHandler HelpRequestHandler;

	private object tag;

	private Control uia_control;

	/// <summary>Gets or sets a value specifying the name of the Help file associated with this <see cref="T:System.Windows.Forms.HelpProvider" /> object.</summary>
	/// <returns>The name of the Help file. This can be of the form C:\path\sample.chm or /folder/file.htm.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.HelpNamespaceEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	public virtual string HelpNamespace
	{
		get
		{
			return helpnamespace;
		}
		set
		{
			helpnamespace = value;
		}
	}

	/// <summary>Gets or sets the object that contains supplemental data about the <see cref="T:System.Windows.Forms.HelpProvider" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.HelpProvider" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Bindable(true)]
	[MWFCategory("Data")]
	[DefaultValue(null)]
	[Localizable(false)]
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

	private Control UIAControl
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

	internal static event ControlEventHandler UIAHelpRequested;

	internal static event ControlEventHandler UIAHelpUnRequested;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HelpProvider" /> class.</summary>
	public HelpProvider()
	{
		controls = new Hashtable();
		tooltip = new ToolTip.ToolTipWindow();
		tooltip.VisibleChanged += delegate
		{
			if (tooltip.Visible)
			{
				OnUIAHelpRequested(this, new ControlEventArgs(UIAControl));
			}
			else
			{
				OnUIAHelpUnRequested(this, new ControlEventArgs(UIAControl));
			}
		};
		HideToolTipHandler = HideToolTip;
		HideToolTipKeyHandler = HideToolTipKey;
		HideToolTipMouseHandler = HideToolTipMouse;
		HelpRequestHandler = HelpRequested;
	}

	/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
	/// <param name="target">The object </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool CanExtend(object target)
	{
		if (!(target is Control))
		{
			return false;
		}
		if (target is Form || target is ToolBar)
		{
			return false;
		}
		return true;
	}

	/// <summary>Returns the Help keyword for the specified control.</summary>
	/// <returns>The Help keyword associated with this control, or null if the <see cref="T:System.Windows.Forms.HelpProvider" /> is currently configured to display the entire Help file or is configured to provide a Help string.</returns>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help topic. </param>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(null)]
	public virtual string GetHelpKeyword(Control ctl)
	{
		return GetHelpProperty(ctl).Keyword;
	}

	/// <summary>Returns the current <see cref="T:System.Windows.Forms.HelpNavigator" /> setting for the specified control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HelpNavigator" /> setting for the specified control. The default is <see cref="F:System.Windows.Forms.HelpNavigator.AssociateIndex" />.</returns>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help navigator. </param>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(HelpNavigator.AssociateIndex)]
	[Localizable(true)]
	public virtual HelpNavigator GetHelpNavigator(Control ctl)
	{
		return GetHelpProperty(ctl).Navigator;
	}

	/// <summary>Returns the contents of the pop-up Help window for the specified control.</summary>
	/// <returns>The Help string associated with this control. The default is null.</returns>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> from which to retrieve the Help string. </param>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Localizable(true)]
	public virtual string GetHelpString(Control ctl)
	{
		return GetHelpProperty(ctl).Text;
	}

	/// <summary>Returns a value indicating whether the specified control's Help should be displayed.</summary>
	/// <returns>true if Help will be displayed for the control; otherwise, false.</returns>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which Help will be displayed. </param>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	public virtual bool GetShowHelp(Control ctl)
	{
		return GetHelpProperty(ctl).Show;
	}

	/// <summary>Removes the Help associated with the specified control.</summary>
	/// <param name="ctl">The control to remove Help from.</param>
	/// <filterpriority>1</filterpriority>
	public virtual void ResetShowHelp(Control ctl)
	{
		HelpProperty helpProperty = GetHelpProperty(ctl);
		if (helpProperty.Keyword != null || helpProperty.Text != null)
		{
			helpProperty.Show = true;
		}
		else
		{
			helpProperty.Show = false;
		}
	}

	/// <summary>Specifies the keyword used to retrieve Help when the user invokes Help for the specified control.</summary>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control for which to set the Help topic. </param>
	/// <param name="keyword">The Help keyword to associate with the control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void SetHelpKeyword(Control ctl, string keyword)
	{
		GetHelpProperty(ctl).Keyword = keyword;
	}

	/// <summary>Specifies the Help command to use when retrieving Help from the Help file for the specified control.</summary>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which to set the Help keyword. </param>
	/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <paramref name="navigator" /> is not one of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void SetHelpNavigator(Control ctl, HelpNavigator navigator)
	{
		GetHelpProperty(ctl).Navigator = navigator;
	}

	/// <summary>Specifies the Help string associated with the specified control.</summary>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> with which to associate the Help string. </param>
	/// <param name="helpString">The Help string associated with the control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void SetHelpString(Control ctl, string helpString)
	{
		GetHelpProperty(ctl).Text = helpString;
	}

	/// <summary>Specifies whether Help is displayed for the specified control.</summary>
	/// <param name="ctl">A <see cref="T:System.Windows.Forms.Control" /> for which Help is turned on or off. </param>
	/// <param name="value">true if Help displays for the control; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void SetShowHelp(Control ctl, bool value)
	{
		GetHelpProperty(ctl).Show = value;
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.HelpProvider" />.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.HelpProvider" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", HelpNameSpace: " + helpnamespace;
	}

	private HelpProperty GetHelpProperty(Control control)
	{
		HelpProperty helpProperty = (HelpProperty)controls[control];
		if (helpProperty == null)
		{
			helpProperty = new HelpProperty(this, control);
			controls[control] = helpProperty;
		}
		return helpProperty;
	}

	private void HideToolTip(object Sender, EventArgs e)
	{
		Control control = (Control)Sender;
		control.LostFocus -= HideToolTipHandler;
		tooltip.Visible = false;
	}

	private void HideToolTipKey(object Sender, KeyPressEventArgs e)
	{
		Control control = (Control)Sender;
		control.KeyPress -= HideToolTipKeyHandler;
		tooltip.Visible = false;
	}

	private void HideToolTipMouse(object Sender, MouseEventArgs e)
	{
		Control control = (Control)Sender;
		control.MouseDown -= HideToolTipMouseHandler;
		tooltip.Visible = false;
	}

	private void HelpRequested(object sender, HelpEventArgs e)
	{
		Control control2 = (UIAControl = (Control)sender);
		if (GetHelpProperty(control2).Text != null)
		{
			Point mousePos = e.MousePos;
			tooltip.Text = GetHelpProperty(control2).Text;
			Size size = ThemeEngine.Current.ToolTipSize(tooltip, tooltip.Text);
			tooltip.Width = size.Width;
			tooltip.Height = size.Height;
			mousePos.X -= size.Width / 2;
			if (mousePos.X < 0)
			{
				mousePos.X += size.Width / 2;
			}
			if (mousePos.X + size.Width < SystemInformation.WorkingArea.Width)
			{
				tooltip.Left = mousePos.X;
			}
			else
			{
				tooltip.Left = mousePos.X - size.Width;
			}
			if (mousePos.Y + size.Height < SystemInformation.WorkingArea.Height - 16)
			{
				tooltip.Top = mousePos.Y;
			}
			else
			{
				tooltip.Top = mousePos.Y - size.Height;
			}
			tooltip.Visible = true;
			control2.KeyPress += HideToolTipKeyHandler;
			control2.MouseDown += HideToolTipMouseHandler;
			control2.LostFocus += HideToolTipHandler;
			e.Handled = true;
		}
	}

	internal static void OnUIAHelpRequested(HelpProvider provider, ControlEventArgs args)
	{
		if (HelpProvider.UIAHelpRequested != null)
		{
			HelpProvider.UIAHelpRequested(provider, args);
		}
	}

	internal static void OnUIAHelpUnRequested(HelpProvider provider, ControlEventArgs args)
	{
		if (HelpProvider.UIAHelpUnRequested != null)
		{
			HelpProvider.UIAHelpUnRequested(provider, args);
		}
	}
}
