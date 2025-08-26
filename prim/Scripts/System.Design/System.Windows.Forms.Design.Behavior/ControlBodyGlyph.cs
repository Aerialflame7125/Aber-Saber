using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Associates a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> with its control.</summary>
public class ControlBodyGlyph : ComponentGlyph
{
	private Rectangle bounds;

	/// <summary>Gets the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</returns>
	[System.MonoTODO]
	public override Rectangle Bounds => bounds;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> class.</summary>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
	/// <param name="cursor">A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
	/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	[System.MonoTODO]
	public ControlBodyGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, Behavior behavior)
		: base(relatedComponent, behavior)
	{
		this.bounds = bounds;
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" /> class.</summary>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
	/// <param name="cursor">A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
	/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	/// <param name="designer">A <see cref="T:System.Windows.Forms.Design.ControlDesigner" /> with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	[System.MonoTODO]
	public ControlBodyGlyph(Rectangle bounds, Cursor cursor, IComponent relatedComponent, ControlDesigner designer)
		: this(bounds, cursor, relatedComponent, designer.BehaviorService.CurrentBehavior)
	{
	}

	/// <summary>Indicates whether a mouse click at the specified point should be handled by the <see cref="T:System.Windows.Forms.Design.Behavior.ControlBodyGlyph" />.</summary>
	/// <param name="p">A point to hit test.</param>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, <see langword="null" />.</returns>
	[System.MonoTODO]
	public override Cursor GetHitTest(Point p)
	{
		throw new NotImplementedException();
	}
}
