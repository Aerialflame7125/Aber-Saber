namespace System.Drawing;

/// <summary>Provides a graphics buffer for double buffering.</summary>
public sealed class BufferedGraphics : IDisposable
{
	private Rectangle size;

	private Bitmap membmp;

	private Graphics target;

	private Graphics source;

	/// <summary>Gets a <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</summary>
	/// <returns>A <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</returns>
	public Graphics Graphics
	{
		get
		{
			if (source == null)
			{
				source = Graphics.FromImage(membmp);
			}
			return source;
		}
	}

	private BufferedGraphics()
	{
	}

	internal BufferedGraphics(Graphics targetGraphics, Rectangle targetRectangle)
	{
		size = targetRectangle;
		target = targetGraphics;
		membmp = new Bitmap(size.Width, size.Height);
	}

	/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
	~BufferedGraphics()
	{
		Dispose(disposing: false);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphics" /> object.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (membmp != null)
			{
				membmp.Dispose();
				membmp = null;
			}
			if (source != null)
			{
				source.Dispose();
				source = null;
			}
			target = null;
		}
	}

	/// <summary>Writes the contents of the graphics buffer to the default device.</summary>
	public void Render()
	{
		Render(target);
	}

	/// <summary>Writes the contents of the graphics buffer to the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
	/// <param name="target">A <see cref="T:System.Drawing.Graphics" /> object to which to write the contents of the graphics buffer.</param>
	public void Render(Graphics target)
	{
		target?.DrawImage(membmp, size);
	}

	/// <summary>Writes the contents of the graphics buffer to the device context associated with the specified <see cref="T:System.IntPtr" /> handle.</summary>
	/// <param name="targetDC">An <see cref="T:System.IntPtr" /> that points to the device context to which to write the contents of the graphics buffer.</param>
	[System.MonoTODO("The targetDC parameter has no equivalent in libgdiplus.")]
	public void Render(IntPtr targetDC)
	{
		throw new NotImplementedException();
	}
}
