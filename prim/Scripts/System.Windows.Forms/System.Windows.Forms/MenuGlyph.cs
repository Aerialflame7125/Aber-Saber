namespace System.Windows.Forms;

/// <summary>Specifies the image to draw when drawing a menu with the <see cref="M:System.Windows.Forms.ControlPaint.DrawMenuGlyph(System.Drawing.Graphics,System.Drawing.Rectangle,System.Windows.Forms.MenuGlyph)" /> method.</summary>
/// <filterpriority>2</filterpriority>
public enum MenuGlyph
{
	/// <summary>Draws a submenu arrow.</summary>
	Arrow = 0,
	/// <summary>The minimum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Arrow" /> value).</summary>
	Min = 0,
	/// <summary>Draws a menu check mark.</summary>
	Checkmark = 1,
	/// <summary>Draws a menu bullet.</summary>
	Bullet = 2,
	/// <summary>The maximum value available by this enumeration (equal to the <see cref="F:System.Windows.Forms.MenuGlyph.Bullet" /> value).</summary>
	Max = 2
}
