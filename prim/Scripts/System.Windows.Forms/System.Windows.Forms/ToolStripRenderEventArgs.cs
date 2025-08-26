using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs)" />, <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs)" />, and <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs)" /> methods. </summary>
/// <filterpriority>2</filterpriority>
public class ToolStripRenderEventArgs : EventArgs
{
	private Rectangle affected_bounds;

	private Color back_color;

	private Rectangle connected_area;

	private Graphics graphics;

	private ToolStrip tool_strip;

	/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted. </summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle AffectedBounds => affected_bounds;

	/// <summary>Gets the <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</returns>
	/// <filterpriority>1</filterpriority>
	public Color BackColor => back_color;

	/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> representing the overlap area between a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and its <see cref="P:System.Windows.Forms.ToolStripDropDown.OwnerItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Rectangle ConnectedArea => connected_area;

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> to be painted.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStrip ToolStrip => tool_strip;

	internal Rectangle InternalConnectedArea
	{
		set
		{
			connected_area = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" /> and using the specified <see cref="T:System.Drawing.Graphics" />. </summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
	/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
	public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip)
		: this(g, toolStrip, new Rectangle(0, 0, 100, 25), SystemColors.Control)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStrip" />, using the specified <see cref="T:System.Drawing.Graphics" /> to paint the specified bounds with the specified <see cref="T:System.Drawing.Color" />.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use for painting.</param>
	/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to paint.</param>
	/// <param name="affectedBounds">The <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the area to be painted.</param>
	/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> that the background of the <see cref="T:System.Windows.Forms.ToolStrip" /> is painted with.</param>
	public ToolStripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle affectedBounds, Color backColor)
	{
		graphics = g;
		tool_strip = toolStrip;
		affected_bounds = affectedBounds;
		back_color = backColor;
	}
}
