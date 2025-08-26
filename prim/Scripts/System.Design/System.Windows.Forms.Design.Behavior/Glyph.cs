using System.Drawing;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Represents a single user interface (UI) entity managed by an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</summary>
public abstract class Glyph
{
	private Behavior behavior;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />, or <see langword="null" /> if there is no behavior.</returns>
	[System.MonoTODO]
	public virtual Behavior Behavior => behavior;

	/// <summary>Gets the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</returns>
	[System.MonoTODO]
	public virtual Rectangle Bounds
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> class.</summary>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />. Can be <see langword="null" />.</param>
	[System.MonoTODO]
	protected Glyph(Behavior behavior)
	{
		SetBehavior(behavior);
	}

	/// <summary>Provides hit test logic.</summary>
	/// <param name="p">A point to hit-test.</param>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is associated with <paramref name="p" />; otherwise, <see langword="null" />.</returns>
	public abstract Cursor GetHitTest(Point p);

	/// <summary>Provides paint logic.</summary>
	/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	public abstract void Paint(PaintEventArgs pe);

	/// <summary>Changes the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
	/// <param name="behavior">A <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to associate with the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</param>
	[System.MonoTODO]
	protected void SetBehavior(Behavior behavior)
	{
		throw new NotImplementedException();
	}
}
