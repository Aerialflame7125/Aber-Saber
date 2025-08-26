using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides panels on each side of the form and a central panel that can hold one or more controls.</summary>
[Designer("System.Windows.Forms.Design.ToolStripContainerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ToolStripContainer : ContainerControl
{
	private class ToolStripContainerTypedControlCollection : ControlCollection
	{
		public ToolStripContainerTypedControlCollection(Control owner)
			: base(owner)
		{
		}
	}

	private ToolStripPanel bottom_panel;

	private ToolStripContentPanel content_panel;

	private ToolStripPanel left_panel;

	private ToolStripPanel right_panel;

	private ToolStripPanel top_panel;

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>true to enable automatic scrolling; otherwise, false. </returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> value.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new Color BackColor
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Image BackgroundImage
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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

	/// <summary>Gets the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(false)]
	public ToolStripPanel BottomToolStripPanel => bottom_panel;

	/// <summary>Gets or sets a value indicating whether the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible. </summary>
	/// <returns>true if the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public bool BottomToolStripPanelVisible
	{
		get
		{
			return bottom_panel.Visible;
		}
		set
		{
			bottom_panel.Visible = value;
		}
	}

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>true if the control causes validation; otherwise, false. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets the center panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> representing the center panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(false)]
	public ToolStripContentPanel ContentPanel => content_panel;

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ContextMenuStrip ContextMenuStrip
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ControlCollection Controls => base.Controls;

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>This property is not relevant for this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Color ForeColor
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

	/// <summary>Gets the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Localizable(false)]
	public ToolStripPanel LeftToolStripPanel => left_panel;

	/// <summary>Gets or sets a value indicating whether the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
	/// <returns>true if the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public bool LeftToolStripPanelVisible
	{
		get
		{
			return left_panel.Visible;
		}
		set
		{
			left_panel.Visible = value;
		}
	}

	/// <summary>Gets the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ToolStripPanel RightToolStripPanel => right_panel;

	/// <summary>Gets or sets a value indicating whether the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
	/// <returns>true if the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public bool RightToolStripPanelVisible
	{
		get
		{
			return right_panel.Visible;
		}
		set
		{
			right_panel.Visible = value;
		}
	}

	/// <summary>Gets the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ToolStripPanel TopToolStripPanel => top_panel;

	/// <summary>Gets or sets a value indicating whether the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
	/// <returns>true if the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public bool TopToolStripPanelVisible
	{
		get
		{
			return top_panel.Visible;
		}
		set
		{
			top_panel.Visible = value;
		}
	}

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripContainer" />, in pixels.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the horizontal and vertical dimensions of the <see cref="T:System.Windows.Forms.ToolStripContainer" />, in pixels.</returns>
	protected override Size DefaultSize => new Size(150, 175);

	/// <summary>This event is not relevant for this class.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContainer.CausesValidation" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContainer.ContextMenuStrip" /> property changes.</summary>
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

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This event is not relevant for this class.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> class. </summary>
	public ToolStripContainer()
	{
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		SetStyle(ControlStyles.ResizeRedraw, value: true);
		content_panel = new ToolStripContentPanel();
		content_panel.Dock = DockStyle.Fill;
		Controls.Add(content_panel);
		top_panel = new ToolStripPanel();
		top_panel.Dock = DockStyle.Top;
		top_panel.Height = 0;
		Controls.Add(top_panel);
		bottom_panel = new ToolStripPanel();
		bottom_panel.Dock = DockStyle.Bottom;
		bottom_panel.Height = 0;
		Controls.Add(bottom_panel);
		left_panel = new ToolStripPanel();
		left_panel.Dock = DockStyle.Left;
		left_panel.Width = 0;
		Controls.Add(left_panel);
		right_panel = new ToolStripPanel();
		right_panel.Dock = DockStyle.Right;
		right_panel.Width = 0;
		Controls.Add(right_panel);
	}

	/// <summary>Creates and returns a <see cref="T:System.Windows.Forms.ToolStripContainer" /> collection.</summary>
	/// <returns>A read-only <see cref="T:System.Windows.Forms.ToolStripContainer" /> collection.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override ControlCollection CreateControlsInstance()
	{
		return new ToolStripContainerTypedControlCollection(this);
	}

	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
	}
}
