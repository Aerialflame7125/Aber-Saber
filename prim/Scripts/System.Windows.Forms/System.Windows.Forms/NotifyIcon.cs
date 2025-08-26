using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;

namespace System.Windows.Forms;

/// <summary>Specifies a component that creates an icon in the notification area. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow)]
[DefaultEvent("MouseDoubleClick")]
[Designer("System.Windows.Forms.Design.NotifyIconDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("Text")]
public sealed class NotifyIcon : Component
{
	internal class NotifyIconWindow : Form
	{
		private NotifyIcon owner;

		private Rectangle rect;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Parent = IntPtr.Zero;
				createParams.Style = int.MinValue;
				createParams.Style |= 67108864;
				createParams.ExStyle = 128;
				return createParams;
			}
		}

		public NotifyIconWindow(NotifyIcon owner)
		{
			this.owner = owner;
			is_visible = false;
			rect = new Rectangle(0, 0, 1, 1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.SizeChanged += HandleSizeChanged;
			base.DoubleClick += HandleDoubleClick;
			base.MouseDown += HandleMouseDown;
			base.MouseUp += HandleMouseUp;
			base.MouseMove += HandleMouseMove;
			ContextMenu = owner.context_menu;
			ContextMenuStrip = owner.context_menu_strip;
		}

		protected override void WndProc(ref Message m)
		{
			switch ((Msg)m.Msg)
			{
			case Msg.WM_CONTEXTMENU:
				break;
			case Msg.WM_USER:
				switch ((Msg)m.LParam.ToInt32())
				{
				case Msg.WM_LBUTTONDOWN:
					owner.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.WM_LBUTTONUP:
					owner.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.WM_LBUTTONDBLCLK:
					owner.OnDoubleClick(EventArgs.Empty);
					owner.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.WM_MOUSEMOVE:
					owner.OnMouseMove(new MouseEventArgs(MouseButtons.None, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.WM_RBUTTONDOWN:
					owner.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.WM_RBUTTONUP:
					owner.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 1, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.WM_RBUTTONDBLCLK:
					owner.OnDoubleClick(EventArgs.Empty);
					owner.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, Control.MousePosition.X, Control.MousePosition.Y, 0));
					break;
				case Msg.NIN_BALLOONUSERCLICK:
					owner.OnBalloonTipClicked(EventArgs.Empty);
					break;
				case Msg.NIN_BALLOONSHOW:
					owner.OnBalloonTipShown(EventArgs.Empty);
					break;
				case Msg.WM_ASYNC_MESSAGE:
				case Msg.NIN_BALLOONTIMEOUT:
					owner.OnBalloonTipClosed(EventArgs.Empty);
					break;
				}
				break;
			default:
				base.WndProc(ref m);
				break;
			}
		}

		internal void CalculateIconRect()
		{
			int num = ((base.ClientRectangle.Width >= base.ClientRectangle.Height) ? base.ClientRectangle.Height : base.ClientRectangle.Width);
			int x = base.ClientRectangle.Width / 2 - num / 2;
			int y = base.ClientRectangle.Height / 2 - num / 2;
			rect = new Rectangle(x, y, num, num);
			base.Bounds = new Rectangle(0, 0, num, num);
		}

		internal override void OnPaintInternal(PaintEventArgs e)
		{
			if (owner.icon != null)
			{
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(SystemColors.Window), rect);
				e.Graphics.DrawImage(owner.icon_bitmap, rect, new Rectangle(0, 0, owner.icon_bitmap.Width, owner.icon_bitmap.Height), GraphicsUnit.Pixel);
			}
		}

		internal void InternalRecreateHandle()
		{
			RecreateHandle();
		}

		private void HandleSizeChanged(object sender, EventArgs e)
		{
			owner.Recalculate();
		}

		private void HandleDoubleClick(object sender, EventArgs e)
		{
			owner.OnDoubleClick(e);
			owner.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, Control.MousePosition.X, Control.MousePosition.Y, 0));
		}

		private void HandleMouseDown(object sender, MouseEventArgs e)
		{
			owner.OnMouseDown(e);
		}

		private void HandleMouseUp(object sender, MouseEventArgs e)
		{
			owner.OnMouseUp(e);
		}

		private void HandleMouseMove(object sender, MouseEventArgs e)
		{
			owner.OnMouseMove(e);
		}
	}

	internal class BalloonWindow : Form
	{
		private IntPtr owner;

		private Timer timer;

		private string title;

		private string text;

		private ToolTipIcon icon;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style = int.MinValue;
				createParams.Style |= 67108864;
				createParams.ExStyle = 136;
				return createParams;
			}
		}

		internal StringFormat Format
		{
			get
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
				return stringFormat;
			}
		}

		public new ToolTipIcon Icon
		{
			get
			{
				return icon;
			}
			set
			{
				if (value != icon)
				{
					icon = value;
					Recalculate();
				}
			}
		}

		public string Title
		{
			get
			{
				return title;
			}
			set
			{
				if (!(value == title))
				{
					title = value;
					Recalculate();
				}
			}
		}

		public override string Text
		{
			get
			{
				return text;
			}
			set
			{
				if (!(value == text))
				{
					text = value;
					Recalculate();
				}
			}
		}

		public int Timeout
		{
			get
			{
				return timer.Interval;
			}
			set
			{
				if (value < 10000)
				{
					timer.Interval = 10000;
				}
				else if (value > 30000)
				{
					timer.Interval = 30000;
				}
				else
				{
					timer.Interval = value;
				}
			}
		}

		public BalloonWindow(IntPtr owner)
		{
			this.owner = owner;
			base.StartPosition = FormStartPosition.Manual;
			base.FormBorderStyle = FormBorderStyle.None;
			base.MouseDown += HandleMouseDown;
			timer = new Timer();
			timer.Enabled = false;
			timer.Tick += HandleTimer;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				timer.Stop();
				timer.Dispose();
			}
			base.Dispose(disposing);
		}

		public new void Close()
		{
			base.Close();
			XplatUI.SendMessage(owner, Msg.WM_USER, IntPtr.Zero, (IntPtr)1027);
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			timer.Start();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			ThemeEngine.Current.DrawBalloonWindow(e.Graphics, base.ClientRectangle, this);
			base.OnPaint(e);
		}

		private void Recalculate()
		{
			Rectangle rectangle = ThemeEngine.Current.BalloonWindowRect(this);
			base.Left = rectangle.Left;
			base.Top = rectangle.Top;
			base.Width = rectangle.Width;
			base.Height = rectangle.Height;
		}

		private void HandleMouseDown(object sender, MouseEventArgs e)
		{
			XplatUI.SendMessage(owner, Msg.WM_USER, IntPtr.Zero, (IntPtr)1029);
			base.Close();
		}

		private void HandleTimer(object sender, EventArgs e)
		{
			timer.Stop();
			XplatUI.SendMessage(owner, Msg.WM_USER, IntPtr.Zero, (IntPtr)1028);
			base.Close();
		}
	}

	private ContextMenu context_menu;

	private Icon icon;

	private Bitmap icon_bitmap;

	private string text;

	private bool visible;

	private NotifyIconWindow window;

	private bool systray_active;

	private ToolTip tooltip;

	private bool double_click;

	private string balloon_text;

	private string balloon_title;

	private ToolTipIcon balloon_icon;

	private ContextMenuStrip context_menu_strip;

	private object tag;

	private static object ClickEvent;

	private static object DoubleClickEvent;

	private static object MouseDownEvent;

	private static object MouseMoveEvent;

	private static object MouseUpEvent;

	private static object BalloonTipClickedEvent;

	private static object BalloonTipClosedEvent;

	private static object BalloonTipShownEvent;

	private static object MouseClickEvent;

	private static object MouseDoubleClickEvent;

	/// <summary>Gets or sets the icon to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolTipIcon" /> to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not a <see cref="T:System.Windows.Forms.ToolTipIcon" />.</exception>
	[DefaultValue("None")]
	public ToolTipIcon BalloonTipIcon
	{
		get
		{
			return balloon_icon;
		}
		set
		{
			if (value != balloon_icon)
			{
				balloon_icon = value;
			}
		}
	}

	/// <summary>Gets or sets the text to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
	/// <returns>The text to display on the balloon tip associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string BalloonTipText
	{
		get
		{
			return balloon_text;
		}
		set
		{
			if (!(value == balloon_text))
			{
				balloon_text = value;
			}
		}
	}

	/// <summary>Gets or sets the title of the balloon tip displayed on the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
	/// <returns>The text to display as the title of the balloon tip.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	public string BalloonTipTitle
	{
		get
		{
			return balloon_title;
		}
		set
		{
			if (!(value == balloon_title))
			{
				balloon_title = value;
			}
		}
	}

	/// <summary>Gets or sets the shortcut menu for the icon.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> for the icon. The default value is null.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(null)]
	[Browsable(false)]
	public ContextMenu ContextMenu
	{
		get
		{
			return context_menu;
		}
		set
		{
			if (context_menu != value)
			{
				context_menu = value;
				window.ContextMenu = value;
			}
		}
	}

	/// <summary>Gets or sets the shortcut menu associated with the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the <see cref="T:System.Windows.Forms.NotifyIcon" /></returns>
	[DefaultValue(null)]
	public ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return context_menu_strip;
		}
		set
		{
			if (context_menu_strip != value)
			{
				context_menu_strip = value;
				window.ContextMenuStrip = value;
			}
		}
	}

	/// <summary>Gets or sets the current icon.</summary>
	/// <returns>The <see cref="T:System.Drawing.Icon" /> displayed by the <see cref="T:System.Windows.Forms.NotifyIcon" /> component. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
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
			if (icon != value)
			{
				icon = value;
				Recalculate();
			}
		}
	}

	/// <summary>Gets or sets an object that contains data about the <see cref="T:System.Windows.Forms.NotifyIcon" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.NotifyIcon" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Bindable(true)]
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

	/// <summary>Gets or sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.</summary>
	/// <returns>The ToolTip text displayed when the mouse pointer rests on a notification area icon.</returns>
	/// <exception cref="T:System.ArgumentException">ToolTip text is more than 63 characters long.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue("")]
	[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (text != value)
			{
				if (value.Length >= 64)
				{
					throw new ArgumentException("ToolTip length must be less than 64 characters long", "Text");
				}
				text = value;
				Recalculate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the icon is visible in the notification area of the taskbar.</summary>
	/// <returns>true if the icon is visible in the notification area; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(false)]
	public bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			if (visible != value)
			{
				visible = value;
				window.is_visible = value;
				if (visible)
				{
					ShowSystray();
				}
				else
				{
					HideSystray();
				}
			}
		}
	}

	/// <summary>Occurs when the balloon tip is clicked.</summary>
	[MWFCategory("Action")]
	public event EventHandler BalloonTipClicked
	{
		add
		{
			base.Events.AddHandler(BalloonTipClickedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BalloonTipClickedEvent, value);
		}
	}

	/// <summary>Occurs when the balloon tip is closed by the user.</summary>
	[MWFCategory("Action")]
	public event EventHandler BalloonTipClosed
	{
		add
		{
			base.Events.AddHandler(BalloonTipClosedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BalloonTipClosedEvent, value);
		}
	}

	/// <summary>Occurs when the balloon tip is displayed on the screen.</summary>
	[MWFCategory("Action")]
	public event EventHandler BalloonTipShown
	{
		add
		{
			base.Events.AddHandler(BalloonTipShownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BalloonTipShownEvent, value);
		}
	}

	/// <summary>Occurs when the user clicks a <see cref="T:System.Windows.Forms.NotifyIcon" /> with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Action")]
	public event MouseEventHandler MouseClick
	{
		add
		{
			base.Events.AddHandler(MouseClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseClickEvent, value);
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.NotifyIcon" /> with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Action")]
	public event MouseEventHandler MouseDoubleClick
	{
		add
		{
			base.Events.AddHandler(MouseDoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseDoubleClickEvent, value);
		}
	}

	/// <summary>Occurs when the user clicks the icon in the notification area.</summary>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Action")]
	public event EventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Occurs when the user double-clicks the icon in the notification area of the taskbar.</summary>
	/// <filterpriority>1</filterpriority>
	[MWFCategory("Action")]
	public event EventHandler DoubleClick
	{
		add
		{
			base.Events.AddHandler(DoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DoubleClickEvent, value);
		}
	}

	/// <summary>Occurs when the user presses the mouse button while the pointer is over the icon in the notification area of the taskbar.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseDown
	{
		add
		{
			base.Events.AddHandler(MouseDownEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseDownEvent, value);
		}
	}

	/// <summary>Occurs when the user moves the mouse while the pointer is over the icon in the notification area of the taskbar.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseMove
	{
		add
		{
			base.Events.AddHandler(MouseMoveEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseMoveEvent, value);
		}
	}

	/// <summary>Occurs when the user releases the mouse button while the pointer is over the icon in the notification area of the taskbar.</summary>
	/// <filterpriority>1</filterpriority>
	public event MouseEventHandler MouseUp
	{
		add
		{
			base.Events.AddHandler(MouseUpEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MouseUpEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NotifyIcon" /> class.</summary>
	public NotifyIcon()
	{
		window = new NotifyIconWindow(this);
		systray_active = false;
		balloon_title = string.Empty;
		balloon_text = string.Empty;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NotifyIcon" /> class with the specified container.</summary>
	/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the <see cref="T:System.Windows.Forms.NotifyIcon" /> control. </param>
	public NotifyIcon(IContainer container)
		: this()
	{
	}

	static NotifyIcon()
	{
		Click = new object();
		DoubleClick = new object();
		MouseDown = new object();
		MouseMove = new object();
		MouseUp = new object();
		BalloonTipClicked = new object();
		BalloonTipClosed = new object();
		BalloonTipShown = new object();
		MouseClick = new object();
		MouseDoubleClick = new object();
	}

	/// <summary>Displays a balloon tip in the taskbar for the specified time period.</summary>
	/// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
	public void ShowBalloonTip(int timeout)
	{
		ShowBalloonTip(timeout, balloon_title, balloon_text, balloon_icon);
	}

	/// <summary>Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.</summary>
	/// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
	/// <param name="tipTitle">The title to display on the balloon tip.</param>
	/// <param name="tipText">The text to display on the balloon tip.</param>
	/// <param name="tipIcon">One of the <see cref="T:System.Windows.Forms.ToolTipIcon" /> values.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="timeout" /> is less than 0.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="tipText" /> is null or an empty string.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="tipIcon" /> is not a member of <see cref="T:System.Windows.Forms.ToolTipIcon" />.</exception>
	public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
	{
		XplatUI.SystrayBalloon(window.Handle, timeout, tipTitle, tipText, tipIcon);
	}

	private void OnBalloonTipClicked(EventArgs e)
	{
		((EventHandler)base.Events[BalloonTipClicked])?.Invoke(this, e);
	}

	private void OnBalloonTipClosed(EventArgs e)
	{
		((EventHandler)base.Events[BalloonTipClosed])?.Invoke(this, e);
	}

	private void OnBalloonTipShown(EventArgs e)
	{
		((EventHandler)base.Events[BalloonTipShown])?.Invoke(this, e);
	}

	private void OnClick(EventArgs e)
	{
		((EventHandler)base.Events[Click])?.Invoke(this, e);
	}

	private void OnDoubleClick(EventArgs e)
	{
		double_click = true;
		((EventHandler)base.Events[DoubleClick])?.Invoke(this, e);
	}

	private void OnMouseClick(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseClick])?.Invoke(this, e);
	}

	private void OnMouseDoubleClick(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseDoubleClick])?.Invoke(this, e);
	}

	private void OnMouseDown(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseDown])?.Invoke(this, e);
	}

	private void OnMouseUp(MouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
		{
			if (context_menu != null)
			{
				XplatUI.SetForegroundWindow(window.Handle);
				context_menu.Show(window, new Point(e.X, e.Y));
			}
			else if (context_menu_strip != null)
			{
				XplatUI.SetForegroundWindow(window.Handle);
				context_menu_strip.Show(window, new Point(e.X, e.Y), ToolStripDropDownDirection.AboveLeft);
			}
		}
		((MouseEventHandler)base.Events[MouseUp])?.Invoke(this, e);
		if (!double_click)
		{
			OnClick(EventArgs.Empty);
			OnMouseClick(e);
			double_click = false;
		}
	}

	private void OnMouseMove(MouseEventArgs e)
	{
		((MouseEventHandler)base.Events[MouseMove])?.Invoke(this, e);
	}

	private void Recalculate()
	{
		window.CalculateIconRect();
		if (!Visible || (text == string.Empty && icon == null))
		{
			HideSystray();
		}
		else if (systray_active)
		{
			UpdateSystray();
		}
		else
		{
			ShowSystray();
		}
	}

	private void ShowSystray()
	{
		if (icon != null)
		{
			icon_bitmap = icon.ToBitmap();
			systray_active = true;
			XplatUI.SystrayAdd(window.Handle, text, icon, out tooltip);
		}
	}

	private void HideSystray()
	{
		if (systray_active)
		{
			systray_active = false;
			XplatUI.SystrayRemove(window.Handle, ref tooltip);
		}
	}

	private void UpdateSystray()
	{
		if (icon_bitmap != null)
		{
			icon_bitmap.Dispose();
		}
		if (icon != null)
		{
			icon_bitmap = icon.ToBitmap();
		}
		window.Invalidate();
		XplatUI.SystrayChange(window.Handle, text, icon, ref tooltip);
	}

	protected override void Dispose(bool disposing)
	{
		if (visible)
		{
			HideSystray();
		}
		if (icon_bitmap != null)
		{
			icon_bitmap.Dispose();
		}
		if (disposing)
		{
			icon = null;
		}
		base.Dispose(disposing);
	}
}
