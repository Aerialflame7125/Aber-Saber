using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Associates a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> with its component.</summary>
public class ComponentGlyph : Glyph
{
	private IComponent component;

	/// <summary>Gets the component that is associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> .</summary>
	/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</returns>
	[System.MonoTODO]
	public IComponent RelatedComponent => component;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ComponentGlyph" /> class.</summary>
	/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	public ComponentGlyph(IComponent relatedComponent)
		: this(relatedComponent, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.ComponentGlyph" /> class.</summary>
	/// <param name="relatedComponent">The component with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> with which the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated.</param>
	public ComponentGlyph(IComponent relatedComponent, Behavior behavior)
		: base(behavior)
	{
		component = relatedComponent;
	}

	/// <summary>Indicates whether a mouse click at the specified point should be handled by the <see cref="T:System.Windows.Forms.Design.Behavior.ComponentGlyph" />.</summary>
	/// <param name="p">A point to hit-test.</param>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, <see langword="null" />.</returns>
	[System.MonoTODO]
	public override Cursor GetHitTest(Point p)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides paint logic.</summary>
	/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> containing the <see cref="P:System.Windows.Forms.Design.Behavior.BehaviorService.AdornerWindowGraphics" /> of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</param>
	[System.MonoTODO]
	public override void Paint(PaintEventArgs pe)
	{
		throw new NotImplementedException();
	}
}
