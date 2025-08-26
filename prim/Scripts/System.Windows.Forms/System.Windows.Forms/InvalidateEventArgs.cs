using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class InvalidateEventArgs : EventArgs
{
	private Rectangle invalidated_rectangle;

	/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area.</summary>
	/// <returns>The invalidated window area.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle InvalidRect => invalidated_rectangle;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> class.</summary>
	/// <param name="invalidRect">The <see cref="T:System.Drawing.Rectangle" /> that contains the invalidated window area. </param>
	public InvalidateEventArgs(Rectangle invalidRect)
	{
		invalidated_rectangle = invalidRect;
	}
}
