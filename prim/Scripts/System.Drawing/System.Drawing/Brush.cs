using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing;

/// <summary>Defines objects used to fill the interiors of graphical shapes such as rectangles, ellipses, pies, polygons, and paths.</summary>
public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
{
	private IntPtr _nativeBrush;

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal IntPtr NativeBrush => _nativeBrush;

	/// <summary>When overridden in a derived class, creates an exact copy of this <see cref="T:System.Drawing.Brush" />.</summary>
	/// <returns>The new <see cref="T:System.Drawing.Brush" /> that this method creates.</returns>
	public abstract object Clone();

	/// <summary>In a derived class, sets a reference to a GDI+ brush object.</summary>
	/// <param name="brush">A pointer to the GDI+ brush object.</param>
	protected internal void SetNativeBrush(IntPtr brush)
	{
		SetNativeBrushInternal(brush);
	}

	internal void SetNativeBrushInternal(IntPtr brush)
	{
		_nativeBrush = brush;
	}

	/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Brush" /> object.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Brush" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!(_nativeBrush != IntPtr.Zero))
		{
			return;
		}
		try
		{
			GDIPlus.GdipDeleteBrush(new HandleRef(this, _nativeBrush));
		}
		catch (Exception ex) when (!ClientUtils.IsSecurityOrCriticalException(ex))
		{
		}
		finally
		{
			_nativeBrush = IntPtr.Zero;
		}
	}

	/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
	~Brush()
	{
		Dispose(disposing: false);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Brush" /> class.</summary>
	protected Brush()
	{
	}
}
