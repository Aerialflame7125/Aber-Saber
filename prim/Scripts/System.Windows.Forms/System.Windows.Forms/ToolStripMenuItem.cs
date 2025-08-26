using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a selectable option displayed on a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
[DesignerSerializer("System.Windows.Forms.Design.ToolStripMenuItemCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class ToolStripMenuItem : ToolStripDropDownItem
{
	private class ToolStripMenuItemAccessibleObject : AccessibleObject
	{
	}

	private CheckState checked_state;

	private bool check_on_click;

	private bool close_on_mouse_release;

	private string shortcut_display_string;

	private Keys shortcut_keys;

	private bool show_shortcut_keys = true;

	private Form mdi_client_form;

	private static object CheckedChangedEvent;

	private static object CheckStateChangedEvent;

	private static object UIACheckOnClickChangedEvent;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked or is in an indeterminate state; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Bindable(true)]
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.All)]
	public bool Checked
	{
		get
		{
			switch (checked_state)
			{
			default:
				return false;
			case CheckState.Checked:
			case CheckState.Indeterminate:
				return true;
			}
		}
		set
		{
			CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> should automatically appear checked and unchecked when clicked.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> should automatically appear checked when clicked; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool CheckOnClick
	{
		get
		{
			return check_on_click;
		}
		set
		{
			if (check_on_click != value)
			{
				check_on_click = value;
				OnUIACheckOnClickChangedEvent(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is in the checked, unchecked, or indeterminate state.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is Unchecked.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property is not set to one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	[Bindable(true)]
	[DefaultValue(CheckState.Unchecked)]
	public CheckState CheckState
	{
		get
		{
			return checked_state;
		}
		set
		{
			if (!Enum.IsDefined(typeof(CheckState), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for CheckState");
			}
			if (value != checked_state)
			{
				checked_state = value;
				Invalidate();
				OnCheckedChanged(EventArgs.Empty);
				OnCheckStateChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is enabled. </summary>
	/// <returns>true if the control is enabled; otherwise, false. The default is true.</returns>
	public override bool Enabled
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

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a multiple document interface (MDI) window list.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a MDI window list; otherwise, false.</returns>
	[Browsable(false)]
	public bool IsMdiWindowListEntry => mdi_client_form != null;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or whether it can float between the two.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is Never.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(ToolStripItemOverflow.Never)]
	public new ToolStripItemOverflow Overflow
	{
		get
		{
			return base.Overflow;
		}
		set
		{
			base.Overflow = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the shortcut keys that are associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> are displayed next to the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. </summary>
	/// <returns>true if the shortcut keys are shown; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	[Localizable(true)]
	public bool ShowShortcutKeys
	{
		get
		{
			return show_shortcut_keys;
		}
		set
		{
			show_shortcut_keys = value;
		}
	}

	/// <summary>Gets or sets the shortcut key text.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the shortcut key.</returns>
	[Localizable(true)]
	[DefaultValue(null)]
	public string ShortcutKeyDisplayString
	{
		get
		{
			return shortcut_display_string;
		}
		set
		{
			shortcut_display_string = value;
		}
	}

	/// <summary>Gets or sets the shortcut keys associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property was not set to one of the <see cref="T:System.Windows.Forms.Keys" /> values.</exception>
	[DefaultValue(Keys.None)]
	[Localizable(true)]
	public Keys ShortcutKeys
	{
		get
		{
			return shortcut_keys;
		}
		set
		{
			if (shortcut_keys != value)
			{
				shortcut_keys = value;
				if (base.Parent != null)
				{
					ToolStripManager.AddToolStripMenuItem(this);
				}
			}
		}
	}

	/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and an adjacent item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
	protected internal override Padding DefaultMargin => new Padding(0);

	/// <summary>Gets the internal spacing within the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
	protected override Padding DefaultPadding => new Padding(4, 0, 4, 0);

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, measured in pixels. The default is 100 pixels horizontally.</returns>
	protected override Size DefaultSize => new Size(32, 19);

	internal Form MdiClientForm
	{
		get
		{
			return mdi_client_form;
		}
		set
		{
			mdi_client_form = value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripMenuItem.Checked" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CheckedChanged
	{
		add
		{
			base.Events.AddHandler(CheckedChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CheckedChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler CheckStateChanged
	{
		add
		{
			base.Events.AddHandler(CheckStateChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CheckStateChangedEvent, value);
		}
	}

	internal event EventHandler UIACheckOnClickChanged
	{
		add
		{
			base.Events.AddHandler(UIACheckOnClickChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIACheckOnClickChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class.</summary>
	public ToolStripMenuItem()
		: this(null, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified <see cref="T:System.Drawing.Image" />.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
	public ToolStripMenuItem(Image image)
		: this(null, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text.</summary>
	/// <param name="text">The text to display on the menu item.</param>
	public ToolStripMenuItem(string text)
		: this(text, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image.</summary>
	/// <param name="text">The text to display on the menu item.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
	public ToolStripMenuItem(string text, Image image)
		: this(text, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
	/// <param name="text">The text to display on the menu item.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
	public ToolStripMenuItem(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that contains the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> collection.</summary>
	/// <param name="text">The text to display on the menu item.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
	/// <param name="dropDownItems">The menu items to display when the control is clicked.</param>
	public ToolStripMenuItem(string text, Image image, params ToolStripItem[] dropDownItems)
		: this(text, image, null, string.Empty)
	{
		if (dropDownItems != null)
		{
			foreach (ToolStripItem value in dropDownItems)
			{
				base.DropDownItems.Add(value);
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image, does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked, and displays the specified shortcut keys.</summary>
	/// <param name="text">The text to display on the menu item.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
	/// <param name="shortcutKeys">One of the values of <see cref="T:System.Windows.Forms.Keys" /> that represents the shortcut key for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
	public ToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class with the specified name that displays the specified text and image that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
	/// <param name="text">The text to display on the menu item.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
	/// <param name="name">The name of the menu item.</param>
	public ToolStripMenuItem(string text, Image image, EventHandler onClick, string name)
		: base(text, image, onClick, name)
	{
		base.Overflow = ToolStripItemOverflow.Never;
	}

	static ToolStripMenuItem()
	{
		CheckedChanged = new object();
		CheckStateChanged = new object();
		UIACheckOnClickChanged = new object();
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripMenuItemAccessibleObject();
	}

	/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
	protected override ToolStripDropDown CreateDefaultDropDown()
	{
		ToolStripDropDownMenu toolStripDropDownMenu = new ToolStripDropDownMenu();
		toolStripDropDownMenu.OwnerItem = this;
		return toolStripDropDownMenu;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckedChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckedChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckedChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckStateChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckStateChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckStateChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnClick(EventArgs e)
	{
		if (!Enabled)
		{
			return;
		}
		if (HasDropDownItems)
		{
			base.OnClick(e);
			return;
		}
		if (base.OwnerItem is ToolStripDropDownItem)
		{
			(base.OwnerItem as ToolStripDropDownItem).OnDropDownItemClicked(new ToolStripItemClickedEventArgs(this));
		}
		if (base.IsOnDropDown)
		{
			GetTopLevelToolStrip()?.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
		}
		if (IsMdiWindowListEntry)
		{
			mdi_client_form.MdiParent.MdiContainer.ActivateChild(mdi_client_form);
			return;
		}
		if (check_on_click)
		{
			Checked = !Checked;
		}
		base.OnClick(e);
		if (!base.IsOnDropDown && !HasDropDownItems)
		{
			GetTopLevelToolStrip()?.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
		}
	}

	/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnDropDownHide(EventArgs e)
	{
		base.OnDropDownHide(e);
	}

	/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnDropDownShow(EventArgs e)
	{
		base.OnDropDownShow(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (!base.IsOnDropDown && HasDropDownItems && base.DropDown.Visible)
		{
			close_on_mouse_release = true;
		}
		if (Enabled && !base.DropDown.Visible)
		{
			ShowDropDown();
		}
		base.OnMouseDown(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseEnter(EventArgs e)
	{
		if (base.IsOnDropDown && HasDropDownItems && Enabled)
		{
			ShowDropDown();
		}
		base.OnMouseEnter(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseUp(MouseEventArgs e)
	{
		if (close_on_mouse_release)
		{
			base.DropDown.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
			Invalidate();
			close_on_mouse_release = false;
			if (!base.IsOnDropDown && base.Parent is MenuStrip)
			{
				(base.Parent as MenuStrip).MenuDroppedDown = false;
			}
		}
		if (!HasDropDownItems && Enabled)
		{
			base.OnMouseUp(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnOwnerChanged(EventArgs e)
	{
		base.OnOwnerChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (base.Owner == null)
		{
			return;
		}
		Image image = ((!base.UseImageMargin) ? null : Image);
		Color color = ForeColor;
		if ((Selected || Pressed) && base.IsOnDropDown && color == SystemColors.MenuText)
		{
			color = SystemColors.HighlightText;
		}
		if (!Enabled && ForeColor == SystemColors.ControlText)
		{
			color = SystemColors.GrayText;
		}
		image = ((!Enabled) ? ToolStripRenderer.CreateDisabledImage(image) : image);
		base.Owner.Renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
		CalculateTextAndImageRectangles(out var text_rect, out var image_rect);
		if (base.IsOnDropDown)
		{
			if (!base.UseImageMargin)
			{
				image_rect = Rectangle.Empty;
				text_rect = new Rectangle(8, text_rect.Top, text_rect.Width, text_rect.Height);
			}
			else
			{
				text_rect = new Rectangle(35, text_rect.Top, text_rect.Width, text_rect.Height);
				if (image_rect != Rectangle.Empty)
				{
					image_rect = new Rectangle(new Point(4, 3), GetImageSize());
				}
			}
			if (Checked && base.ShowMargin)
			{
				base.Owner.Renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(e.Graphics, this, new Rectangle(2, 1, 19, 19)));
			}
		}
		if (text_rect != Rectangle.Empty)
		{
			base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, color, Font, TextAlign));
		}
		string shortcutDisplayString = GetShortcutDisplayString();
		if (!string.IsNullOrEmpty(shortcutDisplayString) && !HasDropDownItems)
		{
			int num = 15;
			Size size = TextRenderer.MeasureText(shortcutDisplayString, Font);
			Rectangle textRectangle = new Rectangle(base.ContentRectangle.Right - size.Width - num, text_rect.Top, size.Width, text_rect.Height);
			base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, shortcutDisplayString, textRectangle, color, Font, TextAlign));
		}
		if (image_rect != Rectangle.Empty)
		{
			base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, image_rect));
		}
		if (base.IsOnDropDown && HasDropDownItems && base.Parent is ToolStripDropDownMenu)
		{
			base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(Bounds.Width - 17, 2, 10, 20), Color.Black, ArrowDirection.Right));
		}
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, which represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		Control control = Control.FromHandle(m.HWnd);
		Form form = ((control != null) ? ((Form)control.TopLevelControl) : null);
		if (Enabled && keyData == shortcut_keys && GetTopLevelControl() == form)
		{
			FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
			return true;
		}
		return base.ProcessCmdKey(ref m, keyData);
	}

	private Control GetTopLevelControl()
	{
		ToolStripItem toolStripItem = this;
		while (toolStripItem.OwnerItem != null)
		{
			toolStripItem = toolStripItem.OwnerItem;
		}
		if (toolStripItem.Owner == null)
		{
			return null;
		}
		if (toolStripItem.Owner is ContextMenuStrip)
		{
			return ((ContextMenuStrip)toolStripItem.Owner).container?.TopLevelControl;
		}
		return toolStripItem.Owner.TopLevelControl;
	}

	/// <summary>Processes a mnemonic character.</summary>
	/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
	/// <param name="charCode">The character to process. </param>
	protected internal override bool ProcessMnemonic(char charCode)
	{
		if (!Selected)
		{
			base.Parent.ChangeSelection(this);
		}
		if (HasDropDownItems)
		{
			ToolStripManager.SetActiveToolStrip(base.Parent, keyboard: true);
			ShowDropDown();
			base.DropDown.SelectNextToolStripItem(null, forward: true);
		}
		else
		{
			PerformClick();
		}
		return true;
	}

	/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
	protected internal override void SetBounds(Rectangle rect)
	{
		base.SetBounds(rect);
	}

	internal void OnUIACheckOnClickChangedEvent(EventArgs args)
	{
		((EventHandler)base.Events[UIACheckOnClickChanged])?.Invoke(this, args);
	}

	internal override Size CalculatePreferredSize(Size constrainingSize)
	{
		Size result = base.CalculatePreferredSize(constrainingSize);
		string shortcutDisplayString = GetShortcutDisplayString();
		if (string.IsNullOrEmpty(shortcutDisplayString))
		{
			return result;
		}
		Size size = TextRenderer.MeasureText(shortcutDisplayString, Font);
		return new Size(result.Width + size.Width - 25, result.Height);
	}

	internal string GetShortcutDisplayString()
	{
		if (!show_shortcut_keys)
		{
			return string.Empty;
		}
		if (base.Parent == null || !(base.Parent is ToolStripDropDownMenu))
		{
			return string.Empty;
		}
		string result = string.Empty;
		if (!string.IsNullOrEmpty(shortcut_display_string))
		{
			result = shortcut_display_string;
		}
		else if (shortcut_keys != 0)
		{
			KeysConverter keysConverter = new KeysConverter();
			result = keysConverter.ConvertToString(shortcut_keys);
		}
		return result;
	}

	internal void HandleAutoExpansion()
	{
		if (HasDropDownItems)
		{
			ShowDropDown();
			base.DropDown.SelectNextToolStripItem(null, forward: true);
		}
	}

	internal override void HandleClick(EventArgs e)
	{
		OnClick(e);
		if (base.Parent != null)
		{
			base.Parent.Invalidate();
		}
	}
}
