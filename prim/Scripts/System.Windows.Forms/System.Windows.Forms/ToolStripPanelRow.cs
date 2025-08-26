using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Represents a row of a <see cref="T:System.Windows.Forms.ToolStripPanel" /> that can contain controls.</summary>
[ToolboxItem(false)]
public class ToolStripPanelRow : Component, IDisposable, IComponent, IBounds
{
	private Rectangle bounds;

	internal List<Control> controls;

	private LayoutEngine layout_engine;

	private Padding margin;

	private Padding padding;

	private ToolStripPanel parent;

	/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />, including its nonclient elements, in pixels, relative to the parent control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location.</returns>
	public Rectangle Bounds => bounds;

	/// <summary>Gets the controls in the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
	/// <returns>An array of controls.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control[] Controls => controls.ToArray();

	/// <summary>Gets the display area of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location.</returns>
	public Rectangle DisplayRectangle => Bounds;

	/// <summary>Gets an instance of the control's layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
	public LayoutEngine LayoutEngine
	{
		get
		{
			if (layout_engine == null)
			{
				layout_engine = new DefaultLayout();
			}
			return layout_engine;
		}
	}

	/// <summary>Gets or sets the space between controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
	public Padding Margin
	{
		get
		{
			return margin;
		}
		set
		{
			margin = value;
		}
	}

	/// <summary>Gets the layout direction of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> relative to its containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
	public Orientation Orientation => parent.Orientation;

	/// <summary>Gets or sets padding within the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
	public virtual Padding Padding
	{
		get
		{
			return padding;
		}
		set
		{
			padding = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanel" /> that contains the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that contains the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</returns>
	public ToolStripPanel ToolStripPanel => parent;

	/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
	protected virtual Padding DefaultMargin => Padding.Empty;

	/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
	protected virtual Padding DefaultPadding => Padding.Empty;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> class, specifying the containing <see cref="T:System.Windows.Forms.ToolStripPanel" />. </summary>
	/// <param name="parent">The containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
	public ToolStripPanelRow(ToolStripPanel parent)
	{
		bounds = Rectangle.Empty;
		controls = new List<Control>();
		layout_engine = new DefaultLayout();
		this.parent = parent;
	}

	/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> can be dragged and dropped into a <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
	/// <returns>true if there is enough space in the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to receive the <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, false. </returns>
	/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be dragged and dropped into the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
	public bool CanMove(ToolStrip toolStripToDrag)
	{
		if (controls.Count > 0 && (toolStripToDrag.Stretch || (controls[0] as ToolStrip).Stretch))
		{
			return false;
		}
		int num = 0;
		foreach (ToolStrip control in controls)
		{
			num += control.Width + control.Margin.Horizontal;
		}
		if (num + toolStripToDrag.Width + toolStripToDrag.Margin.Horizontal <= bounds.Width)
		{
			return true;
		}
		return false;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property changes.</summary>
	/// <param name="oldBounds">The original value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property.</param>
	/// <param name="newBounds">The new value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property.</param>
	protected void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
	/// <param name="control">The control that was added to the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
	/// <param name="index">The zero-based index representing the position of the added control.</param>
	protected internal virtual void OnControlAdded(Control control, int index)
	{
		control.SizeChanged += control_SizeChanged;
		controls.Add(control);
		OnLayout(new LayoutEventArgs(control, string.Empty));
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
	/// <param name="control">The control that was removed from the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
	/// <param name="index">The zero-based index representing the position of the removed control.</param>
	protected internal virtual void OnControlRemoved(Control control, int index)
	{
		control.SizeChanged -= control_SizeChanged;
		controls.Remove(control);
		OnLayout(new LayoutEventArgs(control, string.Empty));
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	protected virtual void OnLayout(LayoutEventArgs e)
	{
		int num = 0;
		if (Orientation == Orientation.Horizontal)
		{
			foreach (ToolStrip control in controls)
			{
				if (control.Height > num)
				{
					num = control.Height;
				}
			}
			if (num != bounds.Height)
			{
				bounds.Height = num;
			}
		}
		else
		{
			foreach (ToolStrip control2 in controls)
			{
				if (control2.GetPreferredSize(Size.Empty).Width > num)
				{
					num = control2.GetPreferredSize(Size.Empty).Width;
				}
			}
			if (num != bounds.Width)
			{
				bounds.Width = num;
			}
		}
		Layout(this, e);
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Orientation" /> property changes.</summary>
	protected internal virtual void OnOrientationChanged()
	{
	}

	internal void SetBounds(Rectangle bounds)
	{
		if (this.bounds != bounds)
		{
			Rectangle oldBounds = this.bounds;
			this.bounds = bounds;
			OnBoundsChanged(oldBounds, bounds);
			OnLayout(new LayoutEventArgs(null, "Bounds"));
		}
	}

	private bool Layout(object container, LayoutEventArgs args)
	{
		ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)container;
		Point location = toolStripPanelRow.DisplayRectangle.Location;
		Control[] array = toolStripPanelRow.Controls;
		for (int i = 0; i < array.Length; i++)
		{
			ToolStrip toolStrip = (ToolStrip)array[i];
			if (Orientation == Orientation.Horizontal)
			{
				if (toolStrip.Stretch)
				{
					toolStrip.Width = bounds.Width - toolStrip.Margin.Horizontal - Padding.Horizontal;
				}
				else
				{
					toolStrip.Width = toolStrip.GetToolStripPreferredSize(Size.Empty).Width;
				}
				location.X += toolStrip.Margin.Left;
				toolStrip.Location = location;
				location.X += toolStrip.Width + toolStrip.Margin.Left;
			}
			else
			{
				if (toolStrip.Stretch)
				{
					toolStrip.Size = new Size(toolStrip.GetToolStripPreferredSize(Size.Empty).Width, bounds.Height - toolStrip.Margin.Vertical - Padding.Vertical);
				}
				else
				{
					toolStrip.Size = toolStrip.GetToolStripPreferredSize(Size.Empty);
				}
				location.Y += toolStrip.Margin.Top;
				toolStrip.Location = location;
				location.Y += toolStrip.Height + toolStrip.Margin.Top;
			}
		}
		return false;
	}

	private void control_SizeChanged(object sender, EventArgs e)
	{
		OnLayout(new LayoutEventArgs((Control)sender, string.Empty));
	}
}
