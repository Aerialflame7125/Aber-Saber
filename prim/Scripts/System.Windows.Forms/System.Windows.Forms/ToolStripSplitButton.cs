using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a combination of a standard button on the left and a drop-down button on the right, or the other way around if the value of <see cref="T:System.Windows.Forms.RightToLeft" /> is Yes.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
[DefaultEvent("ButtonClick")]
public class ToolStripSplitButton : ToolStripDropDownItem
{
	/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> for users with impairments.</summary>
	public class ToolStripSplitButtonAccessibleObject : ToolStripItemAccessibleObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that owns this <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" />.</param>
		public ToolStripSplitButtonAccessibleObject(ToolStripSplitButton item)
			: base(item)
		{
		}

		/// <summary>Performs the default action associated with this <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" />.</summary>
		public override void DoDefaultAction()
		{
			(owner_item as ToolStripSplitButton).PerformButtonClick();
		}
	}

	private bool button_pressed;

	private ToolStripItem default_item;

	private bool drop_down_button_selected;

	private int drop_down_button_width;

	private static object ButtonClickEvent;

	private static object ButtonDoubleClickEvent;

	private static object DefaultItemChangedEvent;

	/// <summary>Gets or sets a value indicating whether default or custom <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
	/// <returns>true if default <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public new bool AutoToolTip
	{
		get
		{
			return base.AutoToolTip;
		}
		set
		{
			base.AutoToolTip = value;
		}
	}

	/// <summary>Gets the size and location of the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Rectangle ButtonBounds => new Rectangle(Bounds.Left, Bounds.Top, Bounds.Width - drop_down_button_width - 1, base.Height);

	/// <summary>Gets a value indicating whether the button portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state. </summary>
	/// <returns>true if the button portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public bool ButtonPressed => button_pressed;

	/// <summary>Gets a value indicating whether the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected or the <see cref="P:System.Windows.Forms.ToolStripSplitButton.DropDownButtonPressed" /> property is true.</summary>
	/// <returns>true if the button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected or whether <see cref="P:System.Windows.Forms.ToolStripSplitButton.DropDownButtonPressed" /> is true; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public bool ButtonSelected => base.Selected;

	/// <summary>Gets or sets the portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that is activated when the control is first selected.</summary>
	/// <returns>A Forms.ToolStripItem representing the portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that is activated when first selected. The default value is null.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(null)]
	[Browsable(false)]
	public ToolStripItem DefaultItem
	{
		get
		{
			return default_item;
		}
		set
		{
			if (default_item != value)
			{
				default_item = value;
				OnDefaultItemChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the size and location, in screen coordinates, of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />, in screen coordinates.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Rectangle DropDownButtonBounds => new Rectangle(Bounds.Right - drop_down_button_width, 0, drop_down_button_width, Bounds.Height);

	/// <summary>Gets a value indicating whether the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state. </summary>
	/// <returns>true if the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public bool DropDownButtonPressed => drop_down_button_selected || (HasDropDownItems && base.DropDown.Visible);

	/// <summary>Gets a value indicating whether the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected.</summary>
	/// <returns>true if the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool DropDownButtonSelected => base.Selected;

	/// <summary>The width, in pixels, of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than zero (0). </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public int DropDownButtonWidth
	{
		get
		{
			return drop_down_button_width;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (drop_down_button_width != value)
			{
				drop_down_button_width = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets the boundaries of the separator between the standard and drop-down button portions of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the separator.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Rectangle SplitterBounds => new Rectangle(Bounds.Width - drop_down_button_width - 1, 0, 1, base.Height);

	/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default. </summary>
	/// <returns>true in all cases.</returns>
	protected override bool DefaultAutoToolTip => true;

	/// <summary>Gets a value indicating whether items on a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> are hidden after they are clicked.</summary>
	/// <returns>true if the items are hidden after they are clicked; otherwise, false.</returns>
	protected internal override bool DismissWhenClicked => true;

	/// <summary>Occurs when the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ButtonClick
	{
		add
		{
			base.Events.AddHandler(ButtonClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ButtonClickEvent, value);
		}
	}

	/// <summary>Occurs when the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is double-clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ButtonDoubleClick
	{
		add
		{
			base.Events.AddHandler(ButtonDoubleClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ButtonDoubleClickEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripSplitButton.DefaultItem" /> has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DefaultItemChanged
	{
		add
		{
			base.Events.AddHandler(DefaultItemChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DefaultItemChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class.</summary>
	public ToolStripSplitButton()
		: this(string.Empty, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified image. </summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	public ToolStripSplitButton(Image image)
		: this(string.Empty, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text. </summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	public ToolStripSplitButton(string text)
		: this(text, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text and image.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	public ToolStripSplitButton(string text, Image image)
		: this(text, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified display text, image, and <see cref="E:System.Windows.Forms.Control.Click" /> event handler.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	public ToolStripSplitButton(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text, image, and <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="dropDownItems">A <see cref="T:System.Windows.Forms.ToolStripItem" /> array of controls.</param>
	public ToolStripSplitButton(string text, Image image, params ToolStripItem[] dropDownItems)
		: base(text, image, dropDownItems)
	{
		ResetDropDownButtonWidth();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified display text, image, <see cref="E:System.Windows.Forms.Control.Click" /> event handler, and name.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
	public ToolStripSplitButton(string text, Image image, EventHandler onClick, string name)
		: base(text, image, onClick, name)
	{
		ResetDropDownButtonWidth();
	}

	static ToolStripSplitButton()
	{
		ButtonClick = new object();
		ButtonDoubleClick = new object();
		DefaultItemChanged = new object();
	}

	/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		Size preferredSize = base.GetPreferredSize(constrainingSize);
		if (preferredSize.Width < 23)
		{
			preferredSize.Width = 23;
		}
		if (base.AutoSize)
		{
			preferredSize.Width += drop_down_button_width - 2;
		}
		return preferredSize;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.ButtonDoubleClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void OnButtonDoubleClick(EventArgs e)
	{
		((EventHandler)base.Events[ButtonDoubleClick])?.Invoke(this, e);
	}

	/// <summary>If the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property is true, calls the <see cref="M:System.Windows.Forms.ToolStripSplitButton.OnButtonClick(System.EventArgs)" /> method.</summary>
	public void PerformButtonClick()
	{
		if (Enabled)
		{
			OnButtonClick(EventArgs.Empty);
		}
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ResetDropDownButtonWidth()
	{
		DropDownButtonWidth = 11;
	}

	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripSplitButtonAccessibleObject(this);
	}

	protected override ToolStripDropDown CreateDefaultDropDown()
	{
		ToolStripDropDownMenu toolStripDropDownMenu = new ToolStripDropDownMenu();
		toolStripDropDownMenu.OwnerItem = this;
		return toolStripDropDownMenu;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.ButtonClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnButtonClick(EventArgs e)
	{
		((EventHandler)base.Events[ButtonClick])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.DefaultItemChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnDefaultItemChanged(EventArgs e)
	{
		((EventHandler)base.Events[DefaultItemChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (ButtonBounds.Contains(e.Location))
		{
			button_pressed = true;
			Invalidate();
			base.OnMouseDown(e);
		}
		else if (DropDownButtonBounds.Contains(e.Location))
		{
			if (base.DropDown.Visible)
			{
				HideDropDown(ToolStripDropDownCloseReason.ItemClicked);
			}
			else
			{
				ShowDropDown();
			}
			Invalidate();
			base.OnMouseDown(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		drop_down_button_selected = false;
		button_pressed = false;
		Invalidate();
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs e)
	{
		button_pressed = false;
		Invalidate();
		base.OnMouseUp(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (base.Owner != null)
		{
			Color textColor = ((!Enabled) ? SystemColors.GrayText : ForeColor);
			Image image = ((!Enabled) ? ToolStripRenderer.CreateDisabledImage(Image) : Image);
			base.Owner.Renderer.DrawSplitButton(new ToolStripItemRenderEventArgs(e.Graphics, this));
			Rectangle contentRectangle = base.ContentRectangle;
			contentRectangle.Width -= drop_down_button_width + 1;
			CalculateTextAndImageRectangles(contentRectangle, out var text_rect, out var image_rect);
			if (text_rect != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, textColor, Font, TextAlign));
			}
			if (image_rect != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, image_rect));
			}
			base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(base.Width - 9, 1, 6, base.Height), Color.Black, ArrowDirection.Down));
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	protected internal override bool ProcessDialogKey(Keys keyData)
	{
		if (Selected && keyData == Keys.Return && DefaultItem != null)
		{
			DefaultItem.FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
			return true;
		}
		return base.ProcessDialogKey(keyData);
	}

	/// <returns>true in all cases.</returns>
	/// <param name="charCode">The character to process. </param>
	protected internal override bool ProcessMnemonic(char charCode)
	{
		if (!Selected)
		{
			base.Parent.ChangeSelection(this);
		}
		if (HasDropDownItems)
		{
			ShowDropDown();
		}
		else
		{
			PerformClick();
		}
		return true;
	}

	internal override void HandleClick(EventArgs e)
	{
		base.HandleClick(e);
		if (e is MouseEventArgs mouseEventArgs && ButtonBounds.Contains(mouseEventArgs.Location))
		{
			OnButtonClick(EventArgs.Empty);
		}
	}
}
