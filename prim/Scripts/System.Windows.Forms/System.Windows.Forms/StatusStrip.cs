using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows status bar control. </summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class StatusStrip : ToolStrip
{
	private class StatusStripAccessibleObject : AccessibleObject
	{
	}

	private bool sizing_grip;

	/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.StatusStrip" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.StatusStrip" /> is resized with its parent.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Bottom" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DockStyle.Bottom)]
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.StatusStrip" /> supports overflow functionality.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.StatusStrip" /> supports overflow functionality; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	[Browsable(false)]
	public new bool CanOverflow
	{
		get
		{
			return base.CanOverflow;
		}
		set
		{
			base.CanOverflow = value;
		}
	}

	/// <summary>Gets or sets the visibility of the grip used to reposition the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Hidden" />.</returns>
	[DefaultValue(ToolStripGripStyle.Hidden)]
	public new ToolStripGripStyle GripStyle
	{
		get
		{
			return base.GripStyle;
		}
		set
		{
			base.GripStyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating how the <see cref="T:System.Windows.Forms.StatusStrip" /> lays out the items collection.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />.</returns>
	[DefaultValue(ToolStripLayoutStyle.Table)]
	public new ToolStripLayoutStyle LayoutStyle
	{
		get
		{
			return base.LayoutStyle;
		}
		set
		{
			base.LayoutStyle = value;
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
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

	/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	/// <returns>true if ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" />; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public new bool ShowItemToolTips
	{
		get
		{
			return base.ShowItemToolTips;
		}
		set
		{
			base.ShowItemToolTips = value;
		}
	}

	/// <summary>Gets the boundaries of the sizing handle (grip) for a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the grip boundaries.</returns>
	[Browsable(false)]
	public Rectangle SizeGripBounds => new Rectangle(base.Width - 12, 0, 12, base.Height);

	/// <summary>Gets or sets a value indicating whether a sizing handle (grip) is displayed in the lower-right corner of the control.</summary>
	/// <returns>true if a grip is displayed; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public bool SizingGrip
	{
		get
		{
			return sizing_grip;
		}
		set
		{
			sizing_grip = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.StatusStrip" /> stretches from end to end in its container.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.StatusStrip" /> stretches from end to end in its <see cref="T:System.Windows.Forms.ToolStripContainer" />; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public new bool Stretch
	{
		get
		{
			return base.Stretch;
		}
		set
		{
			base.Stretch = value;
		}
	}

	/// <summary>Gets which borders of the <see cref="T:System.Windows.Forms.StatusStrip" /> are docked to the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Bottom" />.</returns>
	protected override DockStyle DefaultDock => DockStyle.Bottom;

	/// <summary>Gets the spacing, in pixels, between the left, right, top, and bottom edges of the <see cref="T:System.Windows.Forms.StatusStrip" /> from the edges of the form.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the spacing. The default is {Left=6, Top=2, Right=0, Bottom=2}.</returns>
	protected override Padding DefaultPadding => new Padding(1, 0, 14, 0);

	/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" /> by default.</summary>
	/// <returns>false in all cases.</returns>
	protected override bool DefaultShowItemToolTips => false;

	/// <summary>Gets the size, in pixels, of the <see cref="T:System.Windows.Forms.StatusStrip" /> when it is first created.</summary>
	/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> constructor representing the size of the <see cref="T:System.Windows.Forms.StatusStrip" />, in pixels.</returns>
	protected override Size DefaultSize => new Size(200, 22);

	/// <summary>This event is not relevant for this class.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusStrip" /> class. </summary>
	public StatusStrip()
	{
		SetStyle(ControlStyles.ResizeRedraw, value: true);
		base.CanOverflow = false;
		GripStyle = ToolStripGripStyle.Hidden;
		base.LayoutStyle = ToolStripLayoutStyle.Table;
		base.RenderMode = ToolStripRenderMode.System;
		sizing_grip = true;
		base.Stretch = true;
	}

	/// <summary>Creates a new accessibility object for the control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new StatusStripAccessibleObject();
	}

	/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.StatusStrip" /> instance.</summary>
	/// <returns>A <see cref="M:System.Windows.Forms.ToolStripStatusLabel.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
	/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> is clicked.</param>
	protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
	{
		if (text == "-")
		{
			return new ToolStripSeparator();
		}
		return new ToolStripLabel(text, image, isLink: false, onClick);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.StatusStrip" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <param name="levent"></param>
	protected override void OnLayout(LayoutEventArgs levent)
	{
		OnSpringTableLayoutCore();
		Invalidate();
	}

	/// <summary>Paints the background of the control.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the <see cref="T:System.Windows.Forms.StatusStrip" /> to paint.</param>
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
		if (sizing_grip)
		{
			base.Renderer.DrawStatusStripSizingGrip(new ToolStripRenderEventArgs(e.Graphics, this, Bounds, SystemColors.Control));
		}
	}

	/// <summary>Provides custom table layout for a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	protected virtual void OnSpringTableLayoutCore()
	{
		if (!base.Created)
		{
			return;
		}
		ToolStripItemOverflow[] array = new ToolStripItemOverflow[Items.Count];
		ToolStripItemPlacement[] array2 = new ToolStripItemPlacement[Items.Count];
		Size constrainingSize = new Size(0, Bounds.Height);
		int[] array3 = new int[Items.Count];
		int num = 0;
		int width = DisplayRectangle.Width;
		int num2 = 0;
		int num3 = 0;
		foreach (ToolStripItem item in Items)
		{
			array[num2] = item.Overflow;
			array3[num2] = item.GetPreferredSize(constrainingSize).Width + item.Margin.Horizontal;
			array2[num2] = ((item.Overflow == ToolStripItemOverflow.Always) ? ToolStripItemPlacement.None : ToolStripItemPlacement.Main);
			array2[num2] = ((!item.Available || !item.InternalVisible) ? ToolStripItemPlacement.None : array2[num2]);
			num += ((array2[num2] == ToolStripItemPlacement.Main) ? array3[num2] : 0);
			if (item is ToolStripStatusLabel && (item as ToolStripStatusLabel).Spring)
			{
				num3++;
			}
			num2++;
		}
		while (num > width)
		{
			bool flag = false;
			for (int num4 = array3.Length - 1; num4 >= 0; num4--)
			{
				if (array[num4] == ToolStripItemOverflow.AsNeeded && array2[num4] == ToolStripItemPlacement.Main)
				{
					array2[num4] = ToolStripItemPlacement.None;
					num -= array3[num4];
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int num5 = array3.Length - 1; num5 >= 0; num5--)
				{
					if (array[num5] == ToolStripItemOverflow.Never && array2[num5] == ToolStripItemPlacement.Main)
					{
						array2[num5] = ToolStripItemPlacement.None;
						num -= array3[num5];
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				break;
			}
		}
		if (num3 > 0)
		{
			int num6 = (width - num) / num3;
			num2 = 0;
			foreach (ToolStripItem item2 in Items)
			{
				if (item2 is ToolStripStatusLabel && (item2 as ToolStripStatusLabel).Spring)
				{
					array3[num2] += num6;
				}
				num2++;
			}
		}
		num2 = 0;
		Point point = new Point(DisplayRectangle.Left, DisplayRectangle.Top);
		int height = DisplayRectangle.Height;
		foreach (ToolStripItem item3 in Items)
		{
			item3.SetPlacement(array2[num2]);
			if (array2[num2] == ToolStripItemPlacement.Main)
			{
				item3.SetBounds(new Rectangle(point.X + item3.Margin.Left, point.Y + item3.Margin.Top, array3[num2] - item3.Margin.Horizontal, height - item3.Margin.Vertical));
				point.X += array3[num2];
			}
			num2++;
		}
		SetDisplayedItems();
	}

	protected override void SetDisplayedItems()
	{
		displayed_items.Clear();
		foreach (ToolStripItem item in Items)
		{
			if (item.Placement == ToolStripItemPlacement.Main && item.Available)
			{
				displayed_items.AddNoOwnerOrLayout(item);
				item.Parent = this;
			}
		}
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		switch ((Msg)m.Msg)
		{
		case Msg.WM_MOUSEMOVE:
			if (Control.FromParamToMouseButtons(m.WParam.ToInt32()) == MouseButtons.None)
			{
				Point pt2 = new Point(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder(m.LParam.ToInt32()));
				if (SizingGrip && SizeGripBounds.Contains(pt2))
				{
					Cursor = Cursors.SizeNWSE;
					return;
				}
				Cursor = Cursors.Default;
			}
			break;
		case Msg.WM_LBUTTONDOWN:
		{
			Point pt = new Point(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder(m.LParam.ToInt32()));
			if (SizingGrip && SizeGripBounds.Contains(pt))
			{
				XplatUI.SendMessage(FindForm().Handle, Msg.WM_NCLBUTTONDOWN, (IntPtr)17, IntPtr.Zero);
				return;
			}
			break;
		}
		}
		base.WndProc(ref m);
	}
}
