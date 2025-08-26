using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Provides basic functionality for the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> control. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> and <see cref="T:System.Windows.Forms.ToolStripDropDown" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ToolStripDropDownMenu : ToolStripDropDown
{
	private ToolStripLayoutStyle layout_style;

	private bool show_check_margin;

	private bool show_image_margin;

	/// <summary>Gets the rectangle that represents the display area of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area.</returns>
	public override Rectangle DisplayRectangle => base.DisplayRectangle;

	/// <filterpriority>1</filterpriority>
	public override LayoutEngine LayoutEngine => base.LayoutEngine;

	/// <summary>Gets or sets a value indicating how the items of <see cref="T:System.Windows.Forms.ContextMenuStrip" /> are displayed.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />.</returns>
	[DefaultValue(ToolStripLayoutStyle.Flow)]
	public new ToolStripLayoutStyle LayoutStyle
	{
		get
		{
			return layout_style;
		}
		set
		{
			layout_style = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether space for a check mark is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. </summary>
	/// <returns>true if the check margin is shown; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool ShowCheckMargin
	{
		get
		{
			return show_check_margin;
		}
		set
		{
			if (show_check_margin != value)
			{
				show_check_margin = value;
				PerformLayout(this, "ShowCheckMargin");
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether space for an image is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
	/// <returns>true if the image margin is shown; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool ShowImageMargin
	{
		get
		{
			return show_image_margin;
		}
		set
		{
			if (show_image_margin != value)
			{
				show_image_margin = value;
				PerformLayout(this, "ShowImageMargin");
			}
		}
	}

	/// <summary>Gets the internal spacing, in pixels, of the control.</summary>
	/// <returns>A Padding object representing the spacing.</returns>
	protected override Padding DefaultPadding => base.DefaultPadding;

	/// <summary>Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</summary>
	/// <returns>A Size object representing the height and width of the control, in pixels.</returns>
	protected internal override Size MaxItemSize => base.Size;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> class. </summary>
	public ToolStripDropDownMenu()
	{
		layout_style = ToolStripLayoutStyle.Flow;
		show_image_margin = true;
	}

	/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
	/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</param>
	protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
	{
		return base.CreateDefaultItem(text, image, onClick);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		int num = 0;
		foreach (ToolStripItem item in Items)
		{
			if (item.Available)
			{
				item.SetPlacement(ToolStripItemPlacement.Main);
				num = Math.Max(num, item.GetPreferredSize(Size.Empty).Width);
			}
		}
		int left = base.Padding.Left;
		num = ((!show_check_margin && !show_image_margin) ? (num + (47 - base.Padding.Horizontal)) : (num + (68 - base.Padding.Horizontal)));
		int num2 = base.Padding.Top;
		foreach (ToolStripItem item2 in Items)
		{
			if (item2.Available)
			{
				num2 += item2.Margin.Top;
				int num3 = 0;
				Size preferredSize = item2.GetPreferredSize(Size.Empty);
				num3 = ((preferredSize.Height > 22) ? preferredSize.Height : ((!(item2 is ToolStripSeparator)) ? 22 : 7));
				item2.SetBounds(new Rectangle(left, num2, num, num3));
				num2 += num3 + item2.Margin.Bottom;
			}
		}
		base.Size = new Size(num + base.Padding.Horizontal, num2 + base.Padding.Bottom);
		SetDisplayedItems();
		OnLayoutCompleted(EventArgs.Empty);
		Invalidate();
	}

	/// <summary>Paints the background of the control.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(affectedBounds: new Rectangle(Point.Empty, base.Size), g: e.Graphics, toolStrip: this, backColor: SystemColors.Control);
		toolStripRenderEventArgs.InternalConnectedArea = CalculateConnectedArea();
		base.Renderer.DrawToolStripBackground(toolStripRenderEventArgs);
		if (ShowCheckMargin || ShowImageMargin)
		{
			toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, new Rectangle(toolStripRenderEventArgs.AffectedBounds.Location, new Size(25, toolStripRenderEventArgs.AffectedBounds.Height)), SystemColors.Control);
			base.Renderer.DrawImageMargin(toolStripRenderEventArgs);
		}
	}

	/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
	protected override void SetDisplayedItems()
	{
		base.SetDisplayedItems();
	}

	internal override Rectangle CalculateConnectedArea()
	{
		if (base.OwnerItem != null && !base.OwnerItem.IsOnDropDown && !(base.OwnerItem is MdiControlStrip.SystemMenuItem))
		{
			return new Rectangle(base.OwnerItem.GetCurrentParent().PointToScreen(base.OwnerItem.Location).X - base.Left, 0, base.OwnerItem.Width - 1, 2);
		}
		return base.CalculateConnectedArea();
	}
}
