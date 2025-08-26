using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a control that when clicked displays an associated <see cref="T:System.Windows.Forms.ToolStripDropDown" /> from which the user can select a single item.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
public class ToolStripDropDownButton : ToolStripDropDownItem
{
	private bool show_drop_down_arrow = true;

	/// <summary>Gets or sets a value indicating whether to use the Text property or the <see cref="P:System.Windows.Forms.ToolStripItem.ToolTipText" /> property for the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> ToolTip.</summary>
	/// <returns>true to use the <see cref="P:System.Windows.Forms.Control.Text" /> property for the ToolTip; otherwise, false. The default is true.</returns>
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

	/// <summary>Gets or sets a value indicating whether an arrow is displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, which indicates that further options are available in a drop-down list.</summary>
	/// <returns>true to show an arrow on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public bool ShowDropDownArrow
	{
		get
		{
			return show_drop_down_arrow;
		}
		set
		{
			if (show_drop_down_arrow != value)
			{
				show_drop_down_arrow = value;
				CalculateAutoSize();
			}
		}
	}

	/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
	/// <returns>true in all cases.</returns>
	protected override bool DefaultAutoToolTip => true;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class. </summary>
	public ToolStripDropDownButton()
		: this(string.Empty, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified image.</summary>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	public ToolStripDropDownButton(Image image)
		: this(string.Empty, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	public ToolStripDropDownButton(string text)
		: this(text, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text and image.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	public ToolStripDropDownButton(string text, Image image)
		: this(text, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text and image and raises the Click event.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
	public ToolStripDropDownButton(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="dropDownItems">An array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> containing the items of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	public ToolStripDropDownButton(string text, Image image, params ToolStripItem[] dropDownItems)
		: base(text, image, dropDownItems)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that has the specified name, displays the specified text and image, and raises the Click event.</summary>
	/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
	public ToolStripDropDownButton(string text, Image image, EventHandler onClick, string name)
		: base(text, image, onClick, name)
	{
	}

	/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
	protected override ToolStripDropDown CreateDefaultDropDown()
	{
		ToolStripDropDownMenu toolStripDropDownMenu = new ToolStripDropDownMenu();
		toolStripDropDownMenu.OwnerItem = this;
		return toolStripDropDownMenu;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			if (base.DropDown.Visible)
			{
				HideDropDown(ToolStripDropDownCloseReason.ItemClicked);
			}
			else
			{
				ShowDropDown();
			}
		}
		base.OnMouseDown(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" />  that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (base.Owner != null)
		{
			Color textColor = ((!Enabled) ? SystemColors.GrayText : ForeColor);
			Image image = ((!Enabled) ? ToolStripRenderer.CreateDisabledImage(Image) : Image);
			base.Owner.Renderer.DrawDropDownButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			CalculateTextAndImageRectangles(out var text_rect, out var image_rect);
			if (text_rect != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, textColor, Font, TextAlign));
			}
			if (image_rect != Rectangle.Empty)
			{
				base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, image_rect));
			}
			if (ShowDropDownArrow)
			{
				base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(base.Width - 10, 0, 6, base.Height), Color.Black, ArrowDirection.Down));
			}
		}
	}

	/// <summary>Retrieves a value indicating whether the drop-down list of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> has items.</summary>
	/// <returns>true if the drop-down list has items; otherwise, false.</returns>
	/// <param name="charCode">The character to process.</param>
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

	internal override Size CalculatePreferredSize(Size constrainingSize)
	{
		Size result = base.CalculatePreferredSize(constrainingSize);
		if (ShowDropDownArrow)
		{
			result.Width += 9;
		}
		return result;
	}
}
