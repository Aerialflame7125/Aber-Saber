using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents the menu structure of a form. Although <see cref="T:System.Windows.Forms.MenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MainMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.MainMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxItemFilter("System.Windows.Forms.MainMenu", ToolboxItemFilterType.Allow)]
public class MainMenu : Menu
{
	private RightToLeft right_to_left = RightToLeft.Inherit;

	private Form form;

	private static object CollapseEvent;

	private static object PaintEvent;

	/// <summary>Gets or sets whether the text displayed by the control is displayed from right to left.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[AmbientValue(RightToLeft.Inherit)]
	public virtual RightToLeft RightToLeft
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

	/// <summary>Occurs when the main menu collapses.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Collapse
	{
		add
		{
			base.Events.AddHandler(CollapseEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CollapseEvent, value);
		}
	}

	internal event PaintEventHandler Paint
	{
		add
		{
			base.Events.AddHandler(PaintEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PaintEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class without any specified menu items.</summary>
	public MainMenu()
		: base(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
	/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that will be added to the <see cref="T:System.Windows.Forms.MainMenu" />. </param>
	public MainMenu(MenuItem[] items)
		: base(items)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class with the specified container. </summary>
	/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> representing the container of the <see cref="T:System.Windows.Forms.MainMenu" />.</param>
	public MainMenu(IContainer container)
		: this()
	{
		container.Add(this);
	}

	static MainMenu()
	{
		Collapse = new object();
		Paint = new object();
	}

	/// <summary>Creates a new <see cref="T:System.Windows.Forms.MainMenu" /> that is a duplicate of the current <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the cloned menu.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual MainMenu CloneMenu()
	{
		MainMenu mainMenu = new MainMenu();
		mainMenu.CloneMenu(this);
		return mainMenu;
	}

	/// <returns>A handle to the menu if the method succeeds; otherwise, null.</returns>
	protected override IntPtr CreateMenuHandle()
	{
		return IntPtr.Zero;
	}

	/// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Form" /> that contains this control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that is the container for this control. Returns null if the <see cref="T:System.Windows.Forms.MainMenu" /> is not currently hosted on a form.</returns>
	/// <filterpriority>1</filterpriority>
	public Form GetForm()
	{
		return form;
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MainMenu" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", GetForm: " + form;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MainMenu.Collapse" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal virtual void OnCollapse(EventArgs e)
	{
		((EventHandler)base.Events[Collapse])?.Invoke(this, e);
	}

	internal void Draw()
	{
		Message msg = Message.Create(Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
		PaintEventArgs pe = XplatUI.PaintEventStart(ref msg, Wnd.window.Handle, client: false);
		Draw(pe, base.Rect);
	}

	internal void Draw(Rectangle rect)
	{
		if (Wnd.IsHandleCreated)
		{
			Point menuOrigin = XplatUI.GetMenuOrigin(Wnd.window.Handle);
			Message msg = Message.Create(Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref msg, Wnd.window.Handle, client: false);
			paintEventArgs.Graphics.SetClip(new Rectangle(rect.X + menuOrigin.X, rect.Y + menuOrigin.Y, rect.Width, rect.Height));
			Draw(paintEventArgs, base.Rect);
			XplatUI.PaintEventEnd(ref msg, Wnd.window.Handle, client: false);
		}
	}

	internal void Draw(PaintEventArgs pe)
	{
		Draw(pe, base.Rect);
	}

	internal void Draw(PaintEventArgs pe, Rectangle rect)
	{
		if (Wnd.IsHandleCreated)
		{
			base.X = rect.X;
			base.Y = rect.Y;
			base.Height = base.Rect.Height;
			ThemeEngine.Current.DrawMenuBar(pe.Graphics, this, rect);
			((PaintEventHandler)base.Events[Paint])?.Invoke(this, pe);
		}
	}

	internal override void InvalidateItem(MenuItem item)
	{
		Draw(item.bounds);
	}

	internal void SetForm(Form form)
	{
		this.form = form;
		Wnd = form;
		if (tracker == null)
		{
			tracker = new MenuTracker(this);
			tracker.GrabControl = form;
		}
	}

	internal override void OnMenuChanged(EventArgs e)
	{
		base.OnMenuChanged(EventArgs.Empty);
		if (form != null)
		{
			Rectangle clip = base.Rect;
			base.Height = 0;
			if (Wnd.IsHandleCreated)
			{
				Message msg = Message.Create(Wnd.window.Handle, 15, IntPtr.Zero, IntPtr.Zero);
				PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref msg, Wnd.window.Handle, client: false);
				paintEventArgs.Graphics.SetClip(clip);
				Draw(paintEventArgs, clip);
			}
		}
	}

	internal void OnMouseDown(object window, MouseEventArgs args)
	{
		tracker.OnMouseDown(args);
	}

	internal void OnMouseMove(object window, MouseEventArgs e)
	{
		MouseEventArgs args = new MouseEventArgs(e.Button, e.Clicks, Control.MousePosition.X, Control.MousePosition.Y, e.Delta);
		tracker.OnMotion(args);
	}
}
