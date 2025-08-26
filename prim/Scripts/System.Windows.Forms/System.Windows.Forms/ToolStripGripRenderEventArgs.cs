using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class ToolStripGripRenderEventArgs : ToolStripRenderEventArgs
{
	private Rectangle grip_bounds;

	private ToolStripGripDisplayStyle grip_display_style;

	private ToolStripGripStyle grip_style;

	/// <summary>Gets the rectangle representing the area in which to paint the move handle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area in which to paint the move handle.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Rectangle GripBounds => grip_bounds;

	/// <summary>Gets the style that indicates whether the move handle is displayed vertically or horizontally.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStripGripDisplayStyle GripDisplayStyle => grip_display_style;

	/// <summary>Gets the style that indicates whether or not the move handle is visible.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStripGripStyle GripStyle => grip_style;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> class.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to paint the move handle.</param>
	/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> the move handle is to be drawn on.</param>
	public ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip)
		: base(g, toolStrip)
	{
		grip_bounds = new Rectangle(2, 0, 3, 25);
		grip_display_style = ToolStripGripDisplayStyle.Vertical;
		grip_style = ToolStripGripStyle.Visible;
	}

	internal ToolStripGripRenderEventArgs(Graphics g, ToolStrip toolStrip, Rectangle gripBounds, ToolStripGripDisplayStyle displayStyle, ToolStripGripStyle gripStyle)
		: base(g, toolStrip)
	{
		grip_bounds = gripBounds;
		grip_display_style = displayStyle;
		grip_style = gripStyle;
	}
}
