using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents the center panel of a <see cref="T:System.Windows.Forms.ToolStripContainer" /> control.</summary>
[ComVisible(true)]
[InitializationEvent("Load")]
[Docking(DockingBehavior.Never)]
[DefaultEvent("Load")]
[ToolboxItem(false)]
[Designer("System.Windows.Forms.Design.ToolStripContentPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class ToolStripContentPanel : Panel
{
	private ToolStripRenderMode render_mode;

	private ToolStripRenderer renderer;

	private static object LoadEvent;

	private static object RendererChangedEvent;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AnchorStyles" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true to enable automatic scrolling; otherwise, false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true to enable automatic sizing; otherwise, false.</returns>
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AutoSizeMode" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override AutoSizeMode AutoSizeMode
	{
		get
		{
			return base.AutoSizeMode;
		}
		set
		{
			base.AutoSizeMode = value;
		}
	}

	/// <summary>Overridden to ensure that the background color of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> reflects the background color of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the background color of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			base.BackColor = value;
			if (base.Parent != null)
			{
				base.Parent.BackColor = value;
			}
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if the control causes validation; otherwise, false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DockStyle" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Size MaximumSize
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Size MinimumSize
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

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.String" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new string Name
	{
		get
		{
			return base.Name;
		}
		set
		{
			base.Name = value;
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the appearance of a <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> that handles painting.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ToolStripRenderer Renderer
	{
		get
		{
			if (render_mode == ToolStripRenderMode.ManagerRenderMode)
			{
				return ToolStripManager.Renderer;
			}
			return renderer;
		}
		set
		{
			if (renderer != value)
			{
				renderer = value;
				render_mode = ToolStripRenderMode.Custom;
				OnRendererChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the painting styles to be applied to the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values. </returns>
	public ToolStripRenderMode RenderMode
	{
		get
		{
			return render_mode;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripRenderMode), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripRenderMode");
			}
			if (value == ToolStripRenderMode.Custom && renderer == null)
			{
				throw new NotSupportedException("Must set Renderer property before setting RenderMode to Custom");
			}
			switch (value)
			{
			case ToolStripRenderMode.Professional:
				renderer = new ToolStripProfessionalRenderer();
				break;
			case ToolStripRenderMode.System:
				renderer = new ToolStripSystemRenderer();
				break;
			}
			render_mode = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Int32" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new int TabIndex
	{
		get
		{
			return base.TabIndex;
		}
		set
		{
			base.TabIndex = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> can be tabbed to; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>This event is not relevant for this class.</summary>
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

	/// <summary>This event is not relevant to this class.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Occurs when the content panel loads.</summary>
	public event EventHandler Load
	{
		add
		{
			base.Events.AddHandler(LoadEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LoadEvent, value);
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContentPanel.Renderer" /> property changes.</summary>
	public event EventHandler RendererChanged
	{
		add
		{
			base.Events.AddHandler(RendererChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RendererChangedEvent, value);
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler TabIndexChanged
	{
		add
		{
			base.TabIndexChanged += value;
		}
		remove
		{
			base.TabIndexChanged -= value;
		}
	}

	/// <summary>This event is not relevant for this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> class. </summary>
	public ToolStripContentPanel()
	{
		RenderMode = ToolStripRenderMode.System;
	}

	static ToolStripContentPanel()
	{
		Load = new object();
		RendererChanged = new object();
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnLoad(EventArgs e)
	{
		((EventHandler)base.Events[Load])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
		Renderer.DrawToolStripContentPanelBackground(new ToolStripContentPanelRenderEventArgs(e.Graphics, this));
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripContentPanel.RendererChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnRendererChanged(EventArgs e)
	{
		((EventHandler)base.Events[RendererChanged])?.Invoke(this, e);
	}
}
