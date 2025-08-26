using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a selectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that can contain text and images. </summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
public class ToolStripButton : ToolStripItem
{
	private CheckState checked_state;

	private bool check_on_click;

	private static object CheckedChangedEvent;

	private static object CheckStateChangedEvent;

	private static object UIACheckOnClickChangedEvent;

	/// <summary>Gets or sets a value indicating whether default or custom <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed on the <see cref="T:System.Windows.Forms.ToolStripButton" />. </summary>
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

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> can be selected; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public override bool CanSelect => true;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed or not pressed.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> is pressed in or not pressed in; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
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
			if (checked_state != (CheckState)(value ? 1 : 0))
			{
				checked_state = (value ? CheckState.Checked : CheckState.Unchecked);
				OnCheckedChanged(EventArgs.Empty);
				OnCheckStateChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripButton" /> should automatically appear pressed in and not pressed in when clicked; otherwise, false. The default is false.</returns>
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripButton" /> is in the pressed or not pressed state by default, or is in an indeterminate state.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is Unchecked.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(CheckState.Unchecked)]
	public CheckState CheckState
	{
		get
		{
			return checked_state;
		}
		set
		{
			if (checked_state != value)
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for CheckState");
				}
				checked_state = value;
				OnCheckedChanged(EventArgs.Empty);
				OnCheckStateChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets a value indicating whether to display the ToolTip that is defined as the default. </summary>
	/// <returns>true in all cases.</returns>
	protected override bool DefaultAutoToolTip => true;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripButton.Checked" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripButton.CheckState" /> property changes.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class.</summary>
	public ToolStripButton()
		: this(null, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified image.</summary>
	/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	public ToolStripButton(Image image)
		: this(null, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	public ToolStripButton(string text)
		: this(text, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	public ToolStripButton(string text, Image image)
		: this(text, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
	public ToolStripButton(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripButton" /> class with the specified name that displays the specified text and image and that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	/// <param name="image">The image to display on the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	public ToolStripButton(string text, Image image, EventHandler onClick, string name)
		: base(text, image, onClick, name)
	{
		checked_state = CheckState.Unchecked;
		base.ToolTipText = string.Empty;
	}

	static ToolStripButton()
	{
		CheckedChanged = new object();
		CheckStateChanged = new object();
		UIACheckOnClickChanged = new object();
	}

	/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripButton" /> can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The specified area for a <see cref="T:System.Windows.Forms.ToolStripButton" />.</param>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		Size preferredSize = base.GetPreferredSize(constrainingSize);
		if (preferredSize.Width < 23)
		{
			preferredSize.Width = 23;
		}
		return preferredSize;
	}

	/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripButton" />.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripButton" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		ToolStripItemAccessibleObject toolStripItemAccessibleObject = new ToolStripItemAccessibleObject(this);
		toolStripItemAccessibleObject.default_action = "Press";
		toolStripItemAccessibleObject.role = AccessibleRole.PushButton;
		toolStripItemAccessibleObject.state = AccessibleStates.Focusable;
		return toolStripItemAccessibleObject;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckedChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckedChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckedChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripButton.CheckStateChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckStateChanged(EventArgs e)
	{
		((EventHandler)base.Events[CheckStateChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnClick(EventArgs e)
	{
		if (check_on_click)
		{
			Checked = !Checked;
		}
		base.OnClick(e);
		GetTopLevelToolStrip()?.Dismiss(ToolStripDropDownCloseReason.ItemClicked);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (base.Owner != null)
		{
			Color textColor = ((!Enabled) ? SystemColors.GrayText : ForeColor);
			Image image = ((!Enabled) ? ToolStripRenderer.CreateDisabledImage(Image) : Image);
			base.Owner.Renderer.DrawButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			CalculateTextAndImageRectangles(out var text_rect, out var image_rect);
			if (text_rect != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, textColor, Font, TextAlign));
			}
			if (image_rect != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, image_rect));
			}
		}
	}

	internal void OnUIACheckOnClickChangedEvent(EventArgs args)
	{
		((EventHandler)base.Events[UIACheckOnClickChanged])?.Invoke(this, args);
	}
}
