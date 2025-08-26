using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a dialog box form that contains a <see cref="T:System.Windows.Forms.PrintPreviewControl" /> for printing from a Windows Forms application.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel", ToolboxItemFilterType.Allow)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ToolboxItem(true)]
[DesignTimeVisible(true)]
[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultProperty("Document")]
public class PrintPreviewDialog : Form
{
	private class PrintToolBar : ToolBar
	{
		private bool left_pressed;

		private bool OnDropDownButton => base.CurrentItem != -1 && items[base.CurrentItem].Button.Style == ToolBarButtonStyle.DropDownButton;

		public int GetNext(int pos)
		{
			while (++pos < items.Length && items[pos].Button.Style == ToolBarButtonStyle.Separator)
			{
			}
			return pos;
		}

		public int GetPrev(int pos)
		{
			while (--pos > -1 && items[pos].Button.Style == ToolBarButtonStyle.Separator)
			{
			}
			return pos;
		}

		private void SelectNextOnParent(bool forward)
		{
			if (base.Parent is ContainerControl { ActiveControl: not null } containerControl)
			{
				containerControl.SelectNextControl(containerControl.ActiveControl, forward, tabStopOnly: true, nested: true, wrap: true);
			}
		}

		protected override void OnGotFocus(EventArgs args)
		{
			base.OnGotFocus(args);
			base.CurrentItem = (((Control.ModifierKeys & Keys.Shift) != 0 || left_pressed) ? GetPrev(items.Length) : 0);
			left_pressed = false;
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData & Keys.KeyCode)
			{
			case Keys.Left:
				left_pressed = true;
				SelectNextOnParent(forward: false);
				return true;
			case Keys.Right:
				SelectNextOnParent(forward: true);
				return true;
			default:
				return base.ProcessDialogKey(keyData);
			}
		}

		private void NavigateItems(Keys key)
		{
			bool flag = true;
			switch (key & Keys.KeyCode)
			{
			case Keys.Left:
				flag = false;
				break;
			case Keys.Right:
				flag = true;
				break;
			case Keys.Tab:
				flag = (Control.ModifierKeys & Keys.Shift) == 0;
				break;
			}
			int num = ((!flag) ? GetPrev(base.CurrentItem) : GetNext(base.CurrentItem));
			if (num < 0 || num >= items.Length)
			{
				base.CurrentItem = -1;
				SelectNextOnParent(flag);
			}
			else
			{
				base.CurrentItem = num;
			}
		}

		internal override bool InternalPreProcessMessage(ref Message msg)
		{
			Keys keys = (Keys)msg.WParam.ToInt32();
			switch (keys)
			{
			case Keys.Up:
			case Keys.Down:
				if (OnDropDownButton)
				{
					break;
				}
				return true;
			case Keys.Tab:
			case Keys.Left:
			case Keys.Right:
				if (OnDropDownButton)
				{
					((ContextMenu)items[base.CurrentItem].Button.DropDownMenu).Hide();
				}
				NavigateItems(keys);
				return true;
			}
			return base.InternalPreProcessMessage(ref msg);
		}
	}

	private PrintPreviewControl print_preview;

	private MenuItem previous_checked_menu_item;

	private Menu mag_menu;

	private MenuItem auto_zoom_item;

	private NumericUpDown pageUpDown;

	/// <summary>Gets or sets the button on the form that is clicked when the user presses the ENTER key.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the button to use as the accept button for the form.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new IButtonControl AcceptButton
	{
		get
		{
			return base.AcceptButton;
		}
		set
		{
			base.AcceptButton = value;
		}
	}

	/// <summary>Gets or sets the accessible description of the control.</summary>
	/// <returns>The accessible description of the control. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new string AccessibleDescription
	{
		get
		{
			return base.AccessibleDescription;
		}
		set
		{
			base.AccessibleDescription = value;
		}
	}

	/// <summary>Gets or sets the accessible name of the control.</summary>
	/// <returns>The accessible name of the control. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new string AccessibleName
	{
		get
		{
			return base.AccessibleName;
		}
		set
		{
			base.AccessibleName = value;
		}
	}

	/// <summary>The accessible role of the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleRole.Default" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new AccessibleRole AccessibleRole
	{
		get
		{
			return base.AccessibleRole;
		}
		set
		{
			base.AccessibleRole = value;
		}
	}

	/// <summary>Gets or sets whether the control can accept data that the user drags onto it.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the anchor style for the control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override AnchorStyles Anchor
	{
		get
		{
			return base.Anchor;
		}
		set
		{
			base.Anchor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.</summary>
	/// <returns>true if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool AutoScale
	{
		get
		{
			return base.AutoScale;
		}
		set
		{
			base.AutoScale = value;
		}
	}

	/// <summary>The <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> class does not support the <see cref="P:System.Windows.Forms.PrintPreviewDialog.AutoScaleBaseSize" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[Obsolete("This property has been deprecated.  Use AutoScaleDimensions instead.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Size AutoScaleBaseSize
	{
		get
		{
			return base.AutoScaleBaseSize;
		}
		set
		{
			base.AutoScaleBaseSize = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the form enables autoscrolling.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
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
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the minimum size of the automatic scroll bars.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width, in pixels, of the scroll bars.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should automatically resize to fit its contents.</summary>
	/// <returns>true if <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should resize to fit its contents; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets or sets how the control performs validation when the user changes focus to another control.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override AutoValidate AutoValidate
	{
		get
		{
			return base.AutoValidate;
		}
		set
		{
			base.AutoValidate = value;
		}
	}

	/// <summary>Gets or sets the background color of the form.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
		}
	}

	/// <summary>Gets or sets the background image for the control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
		}
	}

	/// <summary>Gets or sets the layout of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImage" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the cancel button for the <see cref="T:System.Windows.Forms.PrintPreviewDialog" />.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new IButtonControl CancelButton
	{
		get
		{
			return base.CancelButton;
		}
		set
		{
			base.CancelButton = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether entering the control causes validation for all controls that require validation.</summary>
	/// <returns>true if entering the control causes validation to be performed on controls requiring validation; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool CausesValidation
	{
		get
		{
			return base.CausesValidation;
		}
		set
		{
			base.CausesValidation = value;
		}
	}

	/// <summary>Gets or sets the shortcut menu for the control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override ContextMenu ContextMenu
	{
		get
		{
			return base.ContextMenu;
		}
		set
		{
			base.ContextMenu = value;
		}
	}

	/// <summary>Gets or sets how the short cut menu for the control.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return base.ContextMenuStrip;
		}
		set
		{
			base.ContextMenuStrip = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.</summary>
	/// <returns>true if the form displays a control box in the upper-left corner of the form; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool ControlBox
	{
		get
		{
			return base.ControlBox;
		}
		set
		{
			base.ControlBox = value;
		}
	}

	/// <summary>Gets or sets the cursor for the control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Cursor Cursor
	{
		get
		{
			return base.Cursor;
		}
		set
		{
			base.Cursor = value;
		}
	}

	/// <summary>Gets the data bindings for the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects for the control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ControlBindingsCollection DataBindings => base.DataBindings;

	/// <summary>Gets the default minimum size, in pixels, of the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> control.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" /> structure representing the default minimum size.</returns>
	protected override Size DefaultMinimumSize => new Size(370, 300);

	/// <summary>Gets or sets how the control should be docked in its parent control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			base.Dock = value;
		}
	}

	/// <summary>Overrides the <see cref="P:System.Windows.Forms.ScrollableControl.DockPadding" /> property.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new DockPaddingEdges DockPadding => base.DockPadding;

	/// <summary>Gets or sets the document to preview.</summary>
	/// <returns>The <see cref="T:System.Drawing.Printing.PrintDocument" /> representing the document to preview.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	public PrintDocument Document
	{
		get
		{
			return print_preview.Document;
		}
		set
		{
			print_preview.Document = value;
		}
	}

	/// <summary>Get or sets a value indicating whether the control is enabled.</summary>
	/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
		}
	}

	/// <summary>Gets or sets the font used for the control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			base.Font = value;
		}
	}

	/// <summary>Gets or sets the foreground color of the control.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	/// <summary>Gets or sets the border style of the form.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FormBorderStyle" /> that represents the style of border to display for the form. The default is <see cref="F:System.Windows.Forms.FormBorderStyle.Sizable" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new FormBorderStyle FormBorderStyle
	{
		get
		{
			return base.FormBorderStyle;
		}
		set
		{
			base.FormBorderStyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.</summary>
	/// <returns>true to display a Help button in the form's caption bar; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool HelpButton
	{
		get
		{
			return base.HelpButton;
		}
		set
		{
			base.HelpButton = value;
		}
	}

	/// <summary>Gets or sets the icon for the form.</summary>
	/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon for the form.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Icon Icon
	{
		get
		{
			return base.Icon;
		}
		set
		{
			base.Icon = value;
		}
	}

	/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ImeMode ImeMode
	{
		get
		{
			return base.ImeMode;
		}
		set
		{
			base.ImeMode = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the form is a container for multiple document interface (MDI) child forms.</summary>
	/// <returns>true if the form is a container for MDI child forms; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool IsMdiContainer
	{
		get
		{
			return base.IsMdiContainer;
		}
		set
		{
			base.IsMdiContainer = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.</summary>
	/// <returns>true if the form will receive all key events; false if the currently selected control on the form receives key events. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool KeyPreview
	{
		get
		{
			return base.KeyPreview;
		}
		set
		{
			base.KeyPreview = value;
		}
	}

	/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
	/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Point Location
	{
		get
		{
			return base.Location;
		}
		set
		{
			base.Location = value;
		}
	}

	/// <summary>Gets or sets the margins for the control.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Padding Margin
	{
		get
		{
			return base.Margin;
		}
		set
		{
			base.Margin = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the maximize button is displayed in the caption bar of the form.</summary>
	/// <returns>true to display a maximize button for the form; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool MaximizeBox
	{
		get
		{
			return base.MaximizeBox;
		}
		set
		{
			base.MaximizeBox = value;
		}
	}

	/// <summary>Gets or sets the maximum size the form can be resized to.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the maximum size for the form.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> are less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Size MaximumSize
	{
		get
		{
			return base.MaximumSize;
		}
		set
		{
			base.MaximumSize = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.MainMenu" /> that is displayed in the form.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the menu to display in the form.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new MainMenu Menu
	{
		get
		{
			return base.Menu;
		}
		set
		{
			base.Menu = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the minimize button is displayed in the caption bar of the form.</summary>
	/// <returns>true to display a minimize button for the form; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool MinimizeBox
	{
		get
		{
			return base.MinimizeBox;
		}
		set
		{
			base.MinimizeBox = value;
		}
	}

	/// <summary>Gets the minimum size the form can be resized to.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum size for the form.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> are less than 0. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Size MinimumSize
	{
		get
		{
			return base.MinimumSize;
		}
		set
		{
			base.MinimumSize = value;
		}
	}

	/// <summary>Gets or sets the opacity level of the form.</summary>
	/// <returns>The level of opacity for the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new double Opacity
	{
		get
		{
			return base.Opacity;
		}
		set
		{
			base.Opacity = value;
		}
	}

	/// <summary>Gets or sets the padding for the control.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	/// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.PrintPreviewControl" /> contained in this form.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.PrintPreviewControl" /> contained in this form.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public PrintPreviewControl PrintPreviewControl => print_preview;

	/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts. </summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override RightToLeft RightToLeft
	{
		get
		{
			return base.RightToLeft;
		}
		set
		{
			base.RightToLeft = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> should be laid out from right to left.</summary>
	/// <returns>true to indicate the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> contents should be laid out from right to left; otherwise, false. The default is false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool RightToLeftLayout
	{
		get
		{
			return base.RightToLeftLayout;
		}
		set
		{
			base.RightToLeftLayout = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the form is displayed in the Windows taskbar.</summary>
	/// <returns>true to display the form in the Windows taskbar at run time; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DefaultValue(false)]
	public new bool ShowInTaskbar
	{
		get
		{
			return base.ShowInTaskbar;
		}
		set
		{
			base.ShowInTaskbar = value;
		}
	}

	/// <summary>Gets or sets the size of the form.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Size Size
	{
		get
		{
			return base.Size;
		}
		set
		{
			base.Size = value;
		}
	}

	/// <summary>Gets or sets the style of the size grip to display in the lower-right corner of the form.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(SizeGripStyle.Hide)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new SizeGripStyle SizeGripStyle
	{
		get
		{
			return base.SizeGripStyle;
		}
		set
		{
			base.SizeGripStyle = value;
		}
	}

	/// <summary>Gets or sets the starting position of the dialog box at run time.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FormStartPosition" /> that represents the starting position of the dialog box.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new FormStartPosition StartPosition
	{
		get
		{
			return base.StartPosition;
		}
		set
		{
			base.StartPosition = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
	/// <returns>true if the user can give the focus to this control using the TAB key; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets or sets the object that contains data about the control.</summary>
	/// <returns>An object that contains data about the control. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new object Tag
	{
		get
		{
			return base.Tag;
		}
		set
		{
			base.Tag = value;
		}
	}

	/// <summary>Gets or sets the text displayed on the control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the form should be displayed as the topmost form of your application.</summary>
	/// <returns>true to display the form as a topmost form; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool TopMost
	{
		get
		{
			return base.TopMost;
		}
		set
		{
			base.TopMost = value;
		}
	}

	/// <summary>Gets or sets the color that will represent transparent areas of the form.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display transparently on the form.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Color TransparencyKey
	{
		get
		{
			return base.TransparencyKey;
		}
		set
		{
			base.TransparencyKey = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether printing uses the anti-aliasing features of the operating system.</summary>
	/// <returns>true if anti-aliasing is used; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool UseAntiAlias
	{
		get
		{
			return print_preview.UseAntiAlias;
		}
		set
		{
			print_preview.UseAntiAlias = value;
		}
	}

	/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool UseWaitCursor
	{
		get
		{
			return base.UseWaitCursor;
		}
		set
		{
			base.UseWaitCursor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is visible.</summary>
	/// <returns>This property is not relevant for this class.true if the control is visible; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	/// <summary>Gets or sets the form's window state.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FormWindowState" /> that represents the window state of the form. The default is <see cref="F:System.Windows.Forms.FormWindowState.Normal" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new FormWindowState WindowState
	{
		get
		{
			return base.WindowState;
		}
		set
		{
			base.WindowState = value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.AutoSize" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.AutoValidate" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler AutoValidateChanged
	{
		add
		{
			base.AutoValidateChanged += value;
		}
		remove
		{
			base.AutoValidateChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackColorChanged
	{
		add
		{
			base.BackColorChanged += value;
		}
		remove
		{
			base.BackColorChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImage" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.BackgroundImageLayout" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.CausesValidation" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler CausesValidationChanged
	{
		add
		{
			base.CausesValidationChanged += value;
		}
		remove
		{
			base.CausesValidationChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ContextMenu" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ContextMenuChanged
	{
		add
		{
			base.ContextMenuChanged += value;
		}
		remove
		{
			base.ContextMenuChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ContextMenuStrip" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ContextMenuStripChanged
	{
		add
		{
			base.ContextMenuStripChanged += value;
		}
		remove
		{
			base.ContextMenuStripChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Cursor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler CursorChanged
	{
		add
		{
			base.CursorChanged += value;
		}
		remove
		{
			base.CursorChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Dock" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler DockChanged
	{
		add
		{
			base.DockChanged += value;
		}
		remove
		{
			base.DockChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Enabled" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler EnabledChanged
	{
		add
		{
			base.EnabledChanged += value;
		}
		remove
		{
			base.EnabledChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Font" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler FontChanged
	{
		add
		{
			base.FontChanged += value;
		}
		remove
		{
			base.FontChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ForeColor" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler ForeColorChanged
	{
		add
		{
			base.ForeColorChanged += value;
		}
		remove
		{
			base.ForeColorChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.ImeMode" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ImeModeChanged
	{
		add
		{
			base.ImeModeChanged += value;
		}
		remove
		{
			base.ImeModeChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Location" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler LocationChanged
	{
		add
		{
			base.LocationChanged += value;
		}
		remove
		{
			base.LocationChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Margin" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler MarginChanged
	{
		add
		{
			base.MarginChanged += value;
		}
		remove
		{
			base.MarginChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.MaximumSize" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MaximumSizeChanged
	{
		add
		{
			base.MaximumSizeChanged += value;
		}
		remove
		{
			base.MaximumSizeChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.MinimumSize" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler MinimumSizeChanged
	{
		add
		{
			base.MinimumSizeChanged += value;
		}
		remove
		{
			base.MinimumSizeChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Padding" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler PaddingChanged
	{
		add
		{
			base.PaddingChanged += value;
		}
		remove
		{
			base.PaddingChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.RightToLeft" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler RightToLeftChanged
	{
		add
		{
			base.RightToLeftChanged += value;
		}
		remove
		{
			base.RightToLeftChanged -= value;
		}
	}

	/// <summary>Occurs when value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.RightToLeftLayout" /> property changes.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler RightToLeftLayoutChanged
	{
		add
		{
			base.RightToLeftLayoutChanged += value;
		}
		remove
		{
			base.RightToLeftLayoutChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Size" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler SizeChanged
	{
		add
		{
			base.SizeChanged += value;
		}
		remove
		{
			base.SizeChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.TabStop" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Text" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PrintPreviewDialog.Visible" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler VisibleChanged
	{
		add
		{
			base.VisibleChanged += value;
		}
		remove
		{
			base.VisibleChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintPreviewDialog" /> class.</summary>
	public PrintPreviewDialog()
	{
		base.ClientSize = new Size(400, 300);
		ToolBar toolBar = CreateToolBar();
		toolBar.Location = new Point(0, 0);
		toolBar.Dock = DockStyle.Top;
		base.Controls.Add(toolBar);
		print_preview = new PrintPreviewControl();
		print_preview.Location = new Point(0, toolBar.Location.Y + toolBar.Size.Height);
		print_preview.Size = new Size(base.ClientSize.Width, base.ClientSize.Height - toolBar.Bottom);
		print_preview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		print_preview.TabStop = false;
		base.Controls.Add(print_preview);
		print_preview.Show();
	}

	private ToolBar CreateToolBar()
	{
		ImageList imageList = new ImageList();
		imageList.Images.Add(ResourceImageLoader.Get("32_printer.png"));
		imageList.Images.Add(ResourceImageLoader.Get("22_page-magnifier.png"));
		imageList.Images.Add(ResourceImageLoader.Get("1-up.png"));
		imageList.Images.Add(ResourceImageLoader.Get("2-up.png"));
		imageList.Images.Add(ResourceImageLoader.Get("3-up.png"));
		imageList.Images.Add(ResourceImageLoader.Get("4-up.png"));
		imageList.Images.Add(ResourceImageLoader.Get("6-up.png"));
		mag_menu = new ContextMenu();
		ToolBar toolBar = new PrintToolBar();
		ToolBarButton toolBarButton = new ToolBarButton();
		ToolBarButton toolBarButton2 = new ToolBarButton();
		ToolBarButton toolBarButton3 = new ToolBarButton();
		ToolBarButton toolBarButton4 = new ToolBarButton();
		ToolBarButton toolBarButton5 = new ToolBarButton();
		ToolBarButton toolBarButton6 = new ToolBarButton();
		ToolBarButton toolBarButton7 = new ToolBarButton();
		ToolBarButton toolBarButton8 = new ToolBarButton();
		ToolBarButton toolBarButton9 = new ToolBarButton();
		Button button = new Button();
		Label label = new Label();
		pageUpDown = new NumericUpDown();
		toolBar.ImageList = imageList;
		toolBar.Size = new Size(792, 26);
		toolBar.Dock = DockStyle.Top;
		toolBar.Appearance = ToolBarAppearance.Flat;
		toolBar.ShowToolTips = true;
		toolBar.DropDownArrows = true;
		toolBar.TabStop = true;
		toolBar.Buttons.AddRange(new ToolBarButton[9] { toolBarButton, toolBarButton2, toolBarButton3, toolBarButton4, toolBarButton5, toolBarButton6, toolBarButton7, toolBarButton8, toolBarButton9 });
		toolBar.ButtonClick += OnClickToolBarButton;
		toolBarButton.ImageIndex = 0;
		toolBarButton.Tag = 0;
		toolBarButton.ToolTipText = "Print";
		toolBarButton2.ImageIndex = 1;
		toolBarButton2.Tag = 1;
		toolBarButton2.ToolTipText = "Zoom";
		toolBarButton2.Style = ToolBarButtonStyle.DropDownButton;
		toolBarButton2.DropDownMenu = mag_menu;
		MenuItem menuItem = mag_menu.MenuItems.Add("Auto", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem.Checked = true;
		previous_checked_menu_item = menuItem;
		auto_zoom_item = menuItem;
		menuItem = mag_menu.MenuItems.Add("500%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("200%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("150%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("100%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("75%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("50%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("25%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		menuItem = mag_menu.MenuItems.Add("10%", OnClickPageMagnifierItem);
		menuItem.RadioCheck = true;
		toolBarButton3.Style = ToolBarButtonStyle.Separator;
		toolBarButton4.ImageIndex = 2;
		toolBarButton4.Tag = 2;
		toolBarButton4.ToolTipText = "One page";
		toolBarButton5.ImageIndex = 3;
		toolBarButton5.Tag = 3;
		toolBarButton5.ToolTipText = "Two pages";
		toolBarButton6.ImageIndex = 4;
		toolBarButton6.Tag = 4;
		toolBarButton6.ToolTipText = "Three pages";
		toolBarButton7.ImageIndex = 5;
		toolBarButton7.Tag = 5;
		toolBarButton7.ToolTipText = "Four pages";
		toolBarButton8.ImageIndex = 6;
		toolBarButton8.Tag = 6;
		toolBarButton8.ToolTipText = "Six pages";
		toolBarButton9.Style = ToolBarButtonStyle.Separator;
		label.Text = "Page";
		label.TabStop = false;
		label.Size = new Size(50, 18);
		label.TextAlign = ContentAlignment.MiddleLeft;
		label.Dock = DockStyle.Right;
		pageUpDown.Dock = DockStyle.Right;
		pageUpDown.TextAlign = HorizontalAlignment.Right;
		pageUpDown.DecimalPlaces = 0;
		pageUpDown.TabIndex = 1;
		pageUpDown.Text = "1";
		pageUpDown.Minimum = 0m;
		pageUpDown.Maximum = 1000m;
		pageUpDown.Size = new Size(64, 14);
		pageUpDown.Dock = DockStyle.Right;
		pageUpDown.ValueChanged += OnPageUpDownValueChanged;
		button.Location = new Point(196, 2);
		button.Size = new Size(50, 20);
		button.TabIndex = 0;
		button.FlatStyle = FlatStyle.Popup;
		button.Text = "Close";
		button.Click += CloseButtonClicked;
		toolBar.Controls.Add(label);
		toolBar.Controls.Add(pageUpDown);
		toolBar.Controls.Add(button);
		return toolBar;
	}

	private void CloseButtonClicked(object sender, EventArgs e)
	{
		Close();
	}

	private void OnPageUpDownValueChanged(object sender, EventArgs e)
	{
		print_preview.StartPage = (int)pageUpDown.Value;
	}

	private void OnClickToolBarButton(object sender, ToolBarButtonClickEventArgs e)
	{
		if (e.Button.Tag != null && e.Button.Tag is int)
		{
			switch ((int)e.Button.Tag)
			{
			case 0:
				Console.WriteLine("do print here");
				break;
			case 1:
				OnClickPageMagnifierItem(auto_zoom_item, EventArgs.Empty);
				break;
			case 2:
				print_preview.Rows = 0;
				print_preview.Columns = 1;
				break;
			case 3:
				print_preview.Rows = 0;
				print_preview.Columns = 2;
				break;
			case 4:
				print_preview.Rows = 0;
				print_preview.Columns = 3;
				break;
			case 5:
				print_preview.Rows = 1;
				print_preview.Columns = 2;
				break;
			case 6:
				print_preview.Rows = 1;
				print_preview.Columns = 3;
				break;
			}
		}
	}

	private void OnClickPageMagnifierItem(object sender, EventArgs e)
	{
		MenuItem menuItem = (MenuItem)sender;
		previous_checked_menu_item.Checked = false;
		switch (menuItem.Index)
		{
		case 0:
			print_preview.AutoZoom = true;
			break;
		case 1:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 5.0;
			break;
		case 2:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 2.0;
			break;
		case 3:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 1.5;
			break;
		case 4:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 1.0;
			break;
		case 5:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 0.75;
			break;
		case 6:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 0.5;
			break;
		case 7:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 0.25;
			break;
		case 8:
			print_preview.AutoZoom = false;
			print_preview.Zoom = 0.1;
			break;
		}
		menuItem.Checked = true;
		previous_checked_menu_item = menuItem;
	}

	/// <summary>Creates the handle for the form that encapsulates the <see cref="T:System.Windows.Forms.PrintPreviewDialog" />.</summary>
	/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer settings in <see cref="P:System.Windows.Forms.PrintPreviewDialog.Document" /> are not valid. </exception>
	[System.MonoInternalNote("Throw InvalidPrinterException")]
	protected override void CreateHandle()
	{
		base.CreateHandle();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.</summary>
	protected override void OnClosing(CancelEventArgs e)
	{
		print_preview.InvalidatePreview();
		base.OnClosing(e);
	}

	/// <summary>Determines whether a key should be processed further.</summary>
	/// <returns>true to indicate the key should be processed; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		switch (keyData)
		{
		case Keys.Left:
		case Keys.Up:
		case Keys.Right:
		case Keys.Down:
			return false;
		default:
			return base.ProcessDialogKey(keyData);
		}
	}

	/// <summary>Processes the TAB key.</summary>
	/// <returns>true to indicate the TAB key was successfully processed; otherwise, false.</returns>
	/// <param name="forward">true to cycle forward through the controls in the form; otherwise, false.</param>
	protected override bool ProcessTabKey(bool forward)
	{
		return base.ProcessTabKey(forward);
	}
}
