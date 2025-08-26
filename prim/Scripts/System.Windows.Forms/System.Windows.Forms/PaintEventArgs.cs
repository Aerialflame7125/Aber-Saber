using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class PaintEventArgs : EventArgs, IDisposable
{
	private Graphics graphics;

	private Rectangle clip_rectangle;

	internal bool Handled;

	private bool disposed;

	/// <summary>Gets the rectangle in which to paint.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> in which to paint.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle ClipRectangle => clip_rectangle;

	/// <summary>Gets the graphics used to paint.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> object used to paint. The <see cref="T:System.Drawing.Graphics" /> object provides methods for drawing objects on the display device.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PaintEventArgs" /> class with the specified graphics and clipping rectangle.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item. </param>
	/// <param name="clipRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle in which to paint. </param>
	public PaintEventArgs(Graphics graphics, Rectangle clipRect)
	{
		if (graphics == null)
		{
			throw new ArgumentNullException("graphics");
		}
		this.graphics = graphics;
		clip_rectangle = clipRect;
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" />.</summary>
	/// <filterpriority>1</filterpriority>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	internal Graphics SetGraphics(Graphics g)
	{
		Graphics result = graphics;
		graphics = g;
		return result;
	}

	internal void SetClip(Rectangle clip)
	{
		clip_rectangle = clip;
	}

	~PaintEventArgs()
	{
		Dispose(disposing: false);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.PaintEventArgs" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposed)
		{
			disposed = true;
		}
	}
}
