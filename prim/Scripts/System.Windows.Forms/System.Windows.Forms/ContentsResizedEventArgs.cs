using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.ContentsResized" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class ContentsResizedEventArgs : EventArgs
{
	private Rectangle rect;

	/// <summary>Represents the requested size of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the requested size of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle NewRectangle => rect;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContentsResizedEventArgs" /> class.</summary>
	/// <param name="newRectangle">A <see cref="T:System.Drawing.Rectangle" /> that specifies the requested dimensions of the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
	public ContentsResizedEventArgs(Rectangle newRectangle)
	{
		rect = newRectangle;
	}
}
