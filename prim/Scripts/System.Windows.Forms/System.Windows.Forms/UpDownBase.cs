using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Implements the basic functionality required by a spin box (also known as an up-down control).</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.UpDownBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public abstract class UpDownBase : ContainerControl
{
	internal sealed class UpDownSpinner : Control
	{
		private const int InitialRepeatDelay = 50;

		private UpDownBase owner;

		private Timer tmrRepeat;

		private Rectangle top_button_rect;

		private Rectangle bottom_button_rect;

		private int mouse_pressed;

		private int mouse_x;

		private int mouse_y;

		private int repeat_delay;

		private int repeat_counter;

		private bool top_button_entered;

		private bool bottom_button_entered;

		public UpDownSpinner(UpDownBase owner)
		{
			this.owner = owner;
			mouse_pressed = 0;
			SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
			SetStyle(ControlStyles.DoubleBuffer, value: true);
			SetStyle(ControlStyles.Opaque, value: true);
			SetStyle(ControlStyles.ResizeRedraw, value: true);
			SetStyle(ControlStyles.UserPaint, value: true);
			SetStyle(ControlStyles.FixedHeight, value: true);
			SetStyle(ControlStyles.Selectable, value: false);
			tmrRepeat = new Timer();
			tmrRepeat.Enabled = false;
			tmrRepeat.Interval = 10;
			tmrRepeat.Tick += tmrRepeat_Tick;
			compute_rects();
		}

		private void compute_rects()
		{
			int num = base.ClientSize.Height / 2;
			int height = base.ClientSize.Height - num;
			top_button_rect = new Rectangle(0, 0, base.ClientSize.Width, num);
			bottom_button_rect = new Rectangle(0, num, base.ClientSize.Width, height);
		}

		private void redraw(Graphics graphics)
		{
			PushButtonState state = PushButtonState.Normal;
			PushButtonState state2 = PushButtonState.Normal;
			if (owner.Enabled)
			{
				if (mouse_pressed != 0)
				{
					if (mouse_pressed == 1 && top_button_rect.Contains(mouse_x, mouse_y))
					{
						state = PushButtonState.Pressed;
					}
					if (mouse_pressed == 2 && bottom_button_rect.Contains(mouse_x, mouse_y))
					{
						state2 = PushButtonState.Pressed;
					}
				}
				else
				{
					if (top_button_entered)
					{
						state = PushButtonState.Hot;
					}
					if (bottom_button_entered)
					{
						state2 = PushButtonState.Hot;
					}
				}
			}
			else
			{
				state = PushButtonState.Disabled;
				state2 = PushButtonState.Disabled;
			}
			ThemeEngine.Current.UpDownBaseDrawButton(graphics, top_button_rect, top: true, state);
			ThemeEngine.Current.UpDownBaseDrawButton(graphics, bottom_button_rect, top: false, state2);
		}

		private void tmrRepeat_Tick(object sender, EventArgs e)
		{
			if (repeat_delay > 1)
			{
				repeat_counter++;
				if (repeat_counter < repeat_delay)
				{
					return;
				}
				repeat_counter = 0;
				repeat_delay = repeat_delay * 3 / 4;
			}
			if (mouse_pressed == 0)
			{
				tmrRepeat.Enabled = false;
			}
			if (mouse_pressed == 1 && top_button_rect.Contains(mouse_x, mouse_y))
			{
				owner.UpButton();
			}
			if (mouse_pressed == 2 && bottom_button_rect.Contains(mouse_x, mouse_y))
			{
				owner.DownButton();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (top_button_rect.Contains(e.X, e.Y))
				{
					mouse_pressed = 1;
					owner.UpButton();
				}
				else if (bottom_button_rect.Contains(e.X, e.Y))
				{
					mouse_pressed = 2;
					owner.DownButton();
				}
				mouse_x = e.X;
				mouse_y = e.Y;
				base.Capture = true;
				tmrRepeat.Enabled = true;
				repeat_counter = 0;
				repeat_delay = 50;
				Refresh();
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			ButtonState buttonState = ButtonState.Normal;
			if (mouse_pressed == 1 && top_button_rect.Contains(mouse_x, mouse_y))
			{
				buttonState = ButtonState.Pushed;
			}
			if (mouse_pressed == 2 && bottom_button_rect.Contains(mouse_x, mouse_y))
			{
				buttonState = ButtonState.Pushed;
			}
			mouse_x = e.X;
			mouse_y = e.Y;
			ButtonState buttonState2 = ButtonState.Normal;
			if (mouse_pressed == 1 && top_button_rect.Contains(mouse_x, mouse_y))
			{
				buttonState2 = ButtonState.Pushed;
			}
			if (mouse_pressed == 2 && bottom_button_rect.Contains(mouse_x, mouse_y))
			{
				buttonState2 = ButtonState.Pushed;
			}
			bool flag = top_button_rect.Contains(e.Location);
			bool flag2 = bottom_button_rect.Contains(e.Location);
			if (buttonState != buttonState2)
			{
				if (buttonState2 == ButtonState.Pushed)
				{
					tmrRepeat.Enabled = true;
					repeat_counter = 0;
					repeat_delay = 50;
					if (mouse_pressed == 1)
					{
						owner.UpButton();
					}
					if (mouse_pressed == 2)
					{
						owner.DownButton();
					}
				}
				else
				{
					tmrRepeat.Enabled = false;
				}
				top_button_entered = flag;
				bottom_button_entered = flag2;
				Refresh();
			}
			else if (ThemeEngine.Current.UpDownBaseHasHotButtonStyle)
			{
				Region region = new Region();
				bool flag3 = false;
				region.MakeEmpty();
				if (top_button_entered != flag)
				{
					top_button_entered = flag;
					region.Union(top_button_rect);
					flag3 = true;
				}
				if (bottom_button_entered != flag2)
				{
					bottom_button_entered = flag2;
					region.Union(bottom_button_rect);
					flag3 = true;
				}
				if (flag3)
				{
					Invalidate(region);
				}
				region.Dispose();
			}
			else
			{
				top_button_entered = flag;
				bottom_button_entered = flag2;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			mouse_pressed = 0;
			base.Capture = false;
			Refresh();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				owner.UpButton();
			}
			else if (e.Delta < 0)
			{
				owner.DownButton();
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (top_button_entered)
			{
				top_button_entered = false;
				if (ThemeEngine.Current.UpDownBaseHasHotButtonStyle)
				{
					Invalidate(top_button_rect);
				}
			}
			if (bottom_button_entered)
			{
				bottom_button_entered = false;
				if (ThemeEngine.Current.UpDownBaseHasHotButtonStyle)
				{
					Invalidate(bottom_button_rect);
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			redraw(e.Graphics);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			compute_rects();
		}
	}

	internal class UpDownTextBox : TextBox
	{
		private UpDownBase owner;

		public UpDownTextBox(UpDownBase owner)
		{
			this.owner = owner;
			SetStyle(ControlStyles.FixedWidth, value: false);
			SetStyle(ControlStyles.Selectable, value: false);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.ShowSelection = true;
			owner.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.ShowSelection = false;
			owner.OnLostFocus(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			owner.OnMouseDown(e);
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			owner.OnMouseUp(e);
			base.OnMouseUp(e);
		}
	}

	internal UpDownTextBox txtView;

	private UpDownSpinner spnSpinner;

	private bool _InterceptArrowKeys = true;

	private LeftRightAlignment _UpDownAlign;

	private bool changing_text;

	private bool user_edit;

	private static object UIAUpButtonClickEvent;

	private static object UIADownButtonClickEvent;

	/// <summary>Gets a value indicating whether the container will allow the user to scroll to any controls placed outside of its visible boundaries.</summary>
	/// <returns>false in all cases.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public override bool AutoScroll
	{
		get
		{
			return base.AutoScroll;
		}
		set
		{
			base.AutoScroll = value;
		}
	}

	/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width, in pixels, of the auto-scroll margin.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Height" /> or <see cref="P:System.Drawing.Size.Width" /> is less than 0.</exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Size AutoScrollMargin
	{
		get
		{
			return base.AutoScrollMargin;
		}
		set
		{
			base.AutoScrollMargin = value;
		}
	}

	/// <summary>Gets or sets the minimum size of the auto-scroll area.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width, in pixels, of the scroll bars.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Size AutoScrollMinSize
	{
		get
		{
			return base.AutoScrollMinSize;
		}
		set
		{
			base.AutoScrollMinSize = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control should automatically resize based on its contents.</summary>
	/// <returns>true to indicate the control should automatically resize based on its contents; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
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

	/// <summary>Gets or sets the background color for the text box portion of the spin box (also known as an up-down control).</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the text box portion of the spin box.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
			txtView.BackColor = value;
		}
	}

	/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.UpDownBase" />.</summary>
	/// <returns>The background image for the <see cref="T:System.Windows.Forms.UpDownBase" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
			txtView.BackgroundImage = value;
		}
	}

	/// <summary>Gets or sets the layout of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImage" /> of the <see cref="T:System.Windows.Forms.UpDownBase" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets or sets the border style for the spin box (also known as an up-down control).</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default value is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(BorderStyle.Fixed3D)]
	[DispId(-504)]
	public BorderStyle BorderStyle
	{
		get
		{
			return base.InternalBorderStyle;
		}
		set
		{
			base.InternalBorderStyle = value;
		}
	}

	/// <summary>Gets or sets the shortcut menu associated with the spin box (also known as an up-down control).</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> associated with the spin box.</returns>
	/// <filterpriority>1</filterpriority>
	public override ContextMenu ContextMenu
	{
		get
		{
			return base.ContextMenu;
		}
		set
		{
			base.ContextMenu = value;
			txtView.ContextMenu = value;
			spnSpinner.ContextMenu = value;
		}
	}

	/// <summary>Gets or sets the shortcut menu for the spin box (also known as an up-down control).</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the control.</returns>
	public override ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return base.ContextMenuStrip;
		}
		set
		{
			base.ContextMenuStrip = value;
			txtView.ContextMenuStrip = value;
			spnSpinner.ContextMenuStrip = value;
		}
	}

	/// <summary>Gets the dock padding settings for all edges of the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new DockPaddingEdges DockPadding => base.DockPadding;

	/// <summary>Returns true if this control has focus.</summary>
	/// <returns>true if the control has focus; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool Focused => txtView.Focused;

	/// <summary>Gets or sets the foreground color of the spin box (also known as an up-down control).</summary>
	/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the spin box.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
			txtView.ForeColor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can use the UP ARROW and DOWN ARROW keys to select values.</summary>
	/// <returns>true if the control allows the use of the UP ARROW and DOWN ARROW keys to select values; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool InterceptArrowKeys
	{
		get
		{
			return _InterceptArrowKeys;
		}
		set
		{
			_InterceptArrowKeys = value;
		}
	}

	/// <summary>Gets or sets the maximum size of the spin box (also known as an up-down control).</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, which is the maximum size of the spin box.</returns>
	/// <filterpriority>1</filterpriority>
	public override Size MaximumSize
	{
		get
		{
			return base.MaximumSize;
		}
		set
		{
			base.MaximumSize = new Size(value.Width, 0);
		}
	}

	/// <summary>Gets or sets the minimum size of the spin box (also known as an up-down control).</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" />, which is the minimum size of the spin box.</returns>
	/// <filterpriority>1</filterpriority>
	public override Size MinimumSize
	{
		get
		{
			return base.MinimumSize;
		}
		set
		{
			base.MinimumSize = new Size(value.Width, 0);
		}
	}

	/// <summary>Gets the height of the spin box (also known as an up-down control).</summary>
	/// <returns>The height, in pixels, of the spin box.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int PreferredHeight
	{
		get
		{
			int height = Font.Height;
			switch (border_style)
			{
			case BorderStyle.FixedSingle:
			case BorderStyle.Fixed3D:
				height += 3;
				return height + 4;
			default:
				return height;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the text can be changed by the use of the up or down buttons only.</summary>
	/// <returns>true if the text can be changed by the use of the up or down buttons only; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool ReadOnly
	{
		get
		{
			return txtView.ReadOnly;
		}
		set
		{
			txtView.ReadOnly = value;
		}
	}

	/// <summary>Gets or sets the text displayed in the spin box (also known as an up-down control).</summary>
	/// <returns>The string value displayed in the spin box.</returns>
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
			if (txtView != null)
			{
				return txtView.Text;
			}
			return string.Empty;
		}
		set
		{
			txtView.Text = value;
			if (UserEdit)
			{
				ValidateEditText();
			}
			txtView.SelectionLength = 0;
		}
	}

	/// <summary>Gets or sets the alignment of the text in the spin box (also known as an up-down control).</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default value is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(HorizontalAlignment.Left)]
	public HorizontalAlignment TextAlign
	{
		get
		{
			return txtView.TextAlign;
		}
		set
		{
			txtView.TextAlign = value;
		}
	}

	/// <summary>Gets or sets the alignment of the up and down buttons on the spin box (also known as an up-down control).</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. The default value is <see cref="F:System.Windows.Forms.LeftRightAlignment.Right" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(LeftRightAlignment.Right)]
	public LeftRightAlignment UpDownAlign
	{
		get
		{
			return _UpDownAlign;
		}
		set
		{
			if (_UpDownAlign != value)
			{
				_UpDownAlign = value;
				if (value == LeftRightAlignment.Left)
				{
					spnSpinner.Dock = DockStyle.Left;
				}
				else
				{
					spnSpinner.Dock = DockStyle.Right;
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the text property is being changed internally by its parent class.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.UpDownBase.Text" /> property is being changed internally by the <see cref="T:System.Windows.Forms.UpDownBase" /> class; otherwise, false.</returns>
	protected bool ChangingText
	{
		get
		{
			return changing_text;
		}
		set
		{
			changing_text = value;
		}
	}

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Gets the default size of the control.</summary>
	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => new Size(120, PreferredHeight);

	/// <summary>Gets or sets a value indicating whether a value has been entered by the user.</summary>
	/// <returns>true if the user has changed the <see cref="P:System.Windows.Forms.UpDownBase.Text" /> property; otherwise, false.</returns>
	protected bool UserEdit
	{
		get
		{
			return user_edit;
		}
		set
		{
			user_edit = value;
		}
	}

	internal event EventHandler UIAUpButtonClick
	{
		add
		{
			base.Events.AddHandler(UIAUpButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAUpButtonClickEvent, value);
		}
	}

	internal event EventHandler UIADownButtonClick
	{
		add
		{
			base.Events.AddHandler(UIADownButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIADownButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.AutoSize" /> property changes.</summary>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImage" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			base.BackgroundImageChanged += value;
		}
		remove
		{
			base.BackgroundImageChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImageLayout" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.BackgroundImageLayoutChanged += value;
		}
		remove
		{
			base.BackgroundImageLayoutChanged -= value;
		}
	}

	/// <summary>Occurs when the mouse pointer enters the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Occurs when the mouse pointer rests on the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MouseHover
	{
		add
		{
			base.MouseHover += value;
		}
		remove
		{
			base.MouseHover -= value;
		}
	}

	/// <summary>Occurs when the mouse pointer leaves the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the user moves the mouse pointer over the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UpDownBase" /> class.</summary>
	public UpDownBase()
	{
		_UpDownAlign = LeftRightAlignment.Right;
		base.InternalBorderStyle = BorderStyle.Fixed3D;
		spnSpinner = new UpDownSpinner(this);
		txtView = new UpDownTextBox(this);
		txtView.ModifiedChanged += OnChanged;
		txtView.AcceptsReturn = true;
		txtView.AutoSize = false;
		txtView.BorderStyle = BorderStyle.None;
		txtView.Location = new Point(17, 17);
		txtView.TabIndex = base.TabIndex;
		spnSpinner.Width = 16;
		spnSpinner.Dock = DockStyle.Right;
		txtView.Dock = DockStyle.Fill;
		SuspendLayout();
		base.Controls.Add(txtView);
		base.Controls.Add(spnSpinner);
		ResumeLayout();
		base.Height = PreferredHeight;
		base.BackColor = txtView.BackColor;
		base.TabIndexChanged += TabIndexChangedHandler;
		txtView.KeyDown += OnTextBoxKeyDown;
		txtView.KeyPress += OnTextBoxKeyPress;
		txtView.Resize += OnTextBoxResize;
		txtView.TextChanged += OnTextBoxTextChanged;
		auto_select_child = false;
		SetStyle(ControlStyles.FixedHeight, value: true);
		SetStyle(ControlStyles.Selectable, value: true);
		SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, value: true);
		SetStyle(ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, value: false);
	}

	static UpDownBase()
	{
		UIAUpButtonClick = new object();
		UIADownButtonClick = new object();
	}

	internal void OnUIAUpButtonClick(EventArgs e)
	{
		((EventHandler)base.Events[UIAUpButtonClick])?.Invoke(this, e);
	}

	internal void OnUIADownButtonClick(EventArgs e)
	{
		((EventHandler)base.Events[UIADownButtonClick])?.Invoke(this, e);
	}

	private void TabIndexChangedHandler(object sender, EventArgs e)
	{
		txtView.TabIndex = base.TabIndex;
	}

	internal override void OnPaintInternal(PaintEventArgs e)
	{
		e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(BackColor), base.ClientRectangle);
	}

	/// <summary>When overridden in a derived class, handles the clicking of the down button on the spin box (also known as an up-down control).</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void DownButton();

	/// <summary>Selects a range of text in the spin box (also known as an up-down control) specifying the starting position and number of characters to select.</summary>
	/// <param name="start">The position of the first character to be selected. </param>
	/// <param name="length">The total number of characters to be selected. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Select(int start, int length)
	{
		txtView.Select(start, length);
	}

	/// <summary>When overridden in a derived class, handles the clicking of the up button on the spin box (also known as an up-down control).</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void UpButton();

	/// <summary>When overridden in a derived class, raises the Changed event.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnChanged(object source, EventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		txtView.Font = Font;
		base.Height = PreferredHeight;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnHandleDestroyed(EventArgs e)
	{
		base.OnHandleDestroyed(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		base.OnLayout(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event. </summary>
	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		base.OnMouseUp(mevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseWheel(MouseEventArgs e)
	{
		if (e.Delta > 0)
		{
			UpButton();
		}
		else if (e.Delta < 0)
		{
			DownButton();
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" />  that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected virtual void OnTextBoxKeyDown(object source, KeyEventArgs e)
	{
		if (_InterceptArrowKeys && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
		{
			e.Handled = true;
			if (e.KeyCode == Keys.Up)
			{
				UpButton();
			}
			if (e.KeyCode == Keys.Down)
			{
				DownButton();
			}
		}
		OnKeyDown(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	protected virtual void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			e.Handled = true;
			ValidateEditText();
		}
		OnKeyPress(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnTextBoxLostFocus(object source, EventArgs e)
	{
		if (UserEdit)
		{
			ValidateEditText();
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnTextBoxResize(object source, EventArgs e)
	{
		base.Height = PreferredHeight;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnTextBoxTextChanged(object source, EventArgs e)
	{
		if (changing_text)
		{
			ChangingText = false;
		}
		else
		{
			UserEdit = true;
		}
		OnTextChanged(e);
	}

	internal override void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCoreInternal(x, y, width, Math.Min(width, PreferredHeight), specified);
	}

	/// <summary>When overridden in a derived class, updates the text displayed in the spin box (also known as an up-down control).</summary>
	protected abstract void UpdateEditText();

	/// <summary>When overridden in a derived class, validates the text displayed in the spin box (also known as an up-down control).</summary>
	protected virtual void ValidateEditText()
	{
	}

	/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void WndProc(ref Message m)
	{
		switch ((Msg)m.Msg)
		{
		case Msg.WM_KEYDOWN:
		case Msg.WM_KEYUP:
		case Msg.WM_CHAR:
			XplatUI.SendMessage(txtView.Handle, (Msg)m.Msg, m.WParam, m.LParam);
			break;
		case Msg.WM_SETFOCUS:
			ActiveControl = txtView;
			break;
		case Msg.WM_KILLFOCUS:
			ActiveControl = null;
			break;
		default:
			base.WndProc(ref m);
			break;
		}
	}
}
