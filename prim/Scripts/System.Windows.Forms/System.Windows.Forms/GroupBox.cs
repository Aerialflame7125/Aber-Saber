using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows control that displays a frame around a group of controls with an optional caption.</summary>
/// <filterpriority>1</filterpriority>
[DefaultProperty("Text")]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.GroupBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultEvent("Enter")]
public class GroupBox : Control
{
	private class GroupBoxAccessibleObject : ControlAccessibleObject
	{
		public GroupBoxAccessibleObject(Control owner)
			: base(owner)
		{
		}
	}

	private FlatStyle flat_style;

	private Rectangle display_rectangle = default(Rectangle);

	/// <summary>Gets or sets a value that indicates whether the control will allow drag-and-drop operations and events to be used.</summary>
	/// <returns>true to allow drag-and-drop operations and events to be used; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
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

	/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Windows.Forms.GroupBox" /> resizes based on its contents.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.GroupBox" /> automatically resizes based on its contents; otherwise, false. The default is true.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool AutoSize
	{
		get
		{
			return base.AutoSize;
		}
		set
		{
			base.AutoSize = value;
		}
	}

	/// <summary>Gets or sets how the <see cref="T:System.Windows.Forms.GroupBox" /> behaves when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled. </summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
	[DefaultValue(AutoSizeMode.GrowOnly)]
	[Browsable(true)]
	[Localizable(true)]
	public AutoSizeMode AutoSizeMode
	{
		get
		{
			return GetAutoSizeMode();
		}
		set
		{
			SetAutoSizeMode(value);
		}
	}

	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.GroupBoxDefaultSize;

	/// <summary>Gets a rectangle that represents the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> with the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override Rectangle DisplayRectangle
	{
		get
		{
			display_rectangle.X = base.Padding.Left;
			display_rectangle.Y = Font.Height + base.Padding.Top;
			display_rectangle.Width = base.Width - base.Padding.Horizontal;
			display_rectangle.Height = base.Height - Font.Height - base.Padding.Vertical;
			return display_rectangle;
		}
	}

	/// <summary>Gets or sets the flat style appearance of the group box control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(FlatStyle.Standard)]
	public FlatStyle FlatStyle
	{
		get
		{
			return flat_style;
		}
		set
		{
			if (!Enum.IsDefined(typeof(FlatStyle), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for FlatStyle");
			}
			if (flat_style != value)
			{
				flat_style = value;
				Refresh();
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the user can press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
	/// <returns>true to allow the user to press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
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

	/// <returns>The text associated with this control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			if (!(base.Text == value))
			{
				base.Text = value;
				Refresh();
			}
		}
	}

	/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text</summary>
	/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool UseCompatibleTextRendering
	{
		get
		{
			return use_compatible_text_rendering;
		}
		set
		{
			if (use_compatible_text_rendering != value)
			{
				use_compatible_text_rendering = value;
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "UseCompatibleTextRendering");
				}
				Invalidate();
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Padding" /> structure that contains the default padding settings for a <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> with all its edges set to three pixels. </returns>
	protected override Padding DefaultPadding => new Padding(3);

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.GroupBox.AutoSize" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public new event EventHandler AutoSizeChanged
	{
		add
		{
			base.AutoSizeChanged += value;
		}
		remove
		{
			base.AutoSizeChanged -= value;
		}
	}

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new event EventHandler Click
	{
		add
		{
			base.Click += value;
		}
		remove
		{
			base.Click -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
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

	/// <summary>Occurs when the user presses a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
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

	/// <summary>Occurs when the user presses a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus. </summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
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

	/// <summary>Occurs when the user releases a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
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

	/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public new event MouseEventHandler MouseClick
	{
		add
		{
			base.MouseClick += value;
		}
		remove
		{
			base.MouseClick -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
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

	/// <summary>Occurs when the user presses a mouse button while the mouse pointer is over the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new event MouseEventHandler MouseDown
	{
		add
		{
			base.MouseDown += value;
		}
		remove
		{
			base.MouseDown -= value;
		}
	}

	/// <summary>Occurs when the mouse pointer enters the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new event EventHandler MouseEnter
	{
		add
		{
			base.MouseEnter += value;
		}
		remove
		{
			base.MouseEnter -= value;
		}
	}

	/// <summary>Occurs when the mouse pointer leaves the control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public new event EventHandler MouseLeave
	{
		add
		{
			base.MouseLeave += value;
		}
		remove
		{
			base.MouseLeave -= value;
		}
	}

	/// <summary>Occurs when the user moves the mouse pointer over the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new event MouseEventHandler MouseMove
	{
		add
		{
			base.MouseMove += value;
		}
		remove
		{
			base.MouseMove -= value;
		}
	}

	/// <summary>Occurs when the user releases a mouse button while the mouse pointer is over the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new event MouseEventHandler MouseUp
	{
		add
		{
			base.MouseUp += value;
		}
		remove
		{
			base.MouseUp -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.GroupBox.TabStop" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GroupBox" /> class.</summary>
	public GroupBox()
	{
		TabStop = false;
		flat_style = FlatStyle.Standard;
		SetStyle(ControlStyles.ContainerControl | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, value: true);
		SetStyle(ControlStyles.Selectable, value: false);
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new GroupBoxAccessibleObject(this);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		Refresh();
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		ThemeEngine.Current.DrawGroupBox(e.Graphics, base.ClientRectangle, this);
		base.OnPaint(e);
	}

	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		if (Control.IsMnemonic(charCode, Text))
		{
			if (base.Parent != null)
			{
				base.Parent.SelectNextControl(this, forward: true, tabStopOnly: false, nested: true, wrap: false);
			}
			return true;
		}
		return base.ProcessMnemonic(charCode);
	}

	/// <summary>Scales the <see cref="T:System.Windows.Forms.GroupBox" /> by the specified factor and scaling instruction.</summary>
	/// <param name="factor">The <see cref="T:System.Drawing.SizeF" /> that indicates the height and width of the scaled control.</param>
	/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicates how the control should be scaled.</param>
	protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		base.ScaleControl(factor, specified);
	}

	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().FullName + ", Text: " + Text;
	}

	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		Size result = new Size(base.Padding.Left, base.Padding.Top);
		foreach (Control control in base.Controls)
		{
			if (control.Dock == DockStyle.Fill)
			{
				if (control.Bounds.Right > result.Width)
				{
					result.Width = control.Bounds.Right;
				}
			}
			else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && control.Bounds.Right + control.Margin.Right > result.Width)
			{
				result.Width = control.Bounds.Right + control.Margin.Right;
			}
			if (control.Dock == DockStyle.Fill)
			{
				if (control.Bounds.Bottom > result.Height)
				{
					result.Height = control.Bounds.Bottom;
				}
			}
			else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && control.Bounds.Bottom + control.Margin.Bottom > result.Height)
			{
				result.Height = control.Bounds.Bottom + control.Margin.Bottom;
			}
		}
		result.Width += base.Padding.Right;
		result.Height += base.Padding.Bottom;
		result.Height += Font.Height;
		return result;
	}
}
