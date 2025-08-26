using System.Runtime.InteropServices;

namespace System.Drawing;

/// <summary>Defines a brush of a single color. Brushes are used to fill graphics shapes, such as rectangles, ellipses, pies, polygons, and paths. This class cannot be inherited.</summary>
public sealed class SolidBrush : Brush
{
	private Color _color = Color.Empty;

	private bool _immutable;

	/// <summary>Gets or sets the color of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.SolidBrush.Color" /> property is set on an immutable <see cref="T:System.Drawing.SolidBrush" />.</exception>
	public Color Color
	{
		get
		{
			if (_color == Color.Empty)
			{
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetSolidFillColor(new HandleRef(this, base.NativeBrush), out var color));
				_color = Color.FromArgb(color);
			}
			return _color;
		}
		set
		{
			if (_immutable)
			{
				throw new ArgumentException(global::SR.Format("Changes cannot be made to {0} because permissions are not valid.", "Brush"));
			}
			if (_color != value)
			{
				_ = _color;
				InternalSetColor(value);
			}
		}
	}

	/// <summary>Initializes a new <see cref="T:System.Drawing.SolidBrush" /> object of the specified color.</summary>
	/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush.</param>
	public SolidBrush(Color color)
	{
		_color = color;
		IntPtr brush = IntPtr.Zero;
		SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateSolidFill(_color.ToArgb(), out brush));
		SetNativeBrushInternal(brush);
	}

	internal SolidBrush(Color color, bool immutable)
		: this(color)
	{
		_immutable = immutable;
	}

	internal SolidBrush(IntPtr nativeBrush)
	{
		SetNativeBrushInternal(nativeBrush);
	}

	/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
	/// <returns>The <see cref="T:System.Drawing.SolidBrush" /> object that this method creates.</returns>
	public override object Clone()
	{
		IntPtr clonedBrush = IntPtr.Zero;
		SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out clonedBrush));
		return new SolidBrush(clonedBrush);
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposing)
		{
			_immutable = false;
		}
		else if (_immutable)
		{
			throw new ArgumentException(global::SR.Format("Changes cannot be made to {0} because permissions are not valid.", "Brush"));
		}
		base.Dispose(disposing);
	}

	private void InternalSetColor(Color value)
	{
		SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetSolidFillColor(new HandleRef(this, base.NativeBrush), value.ToArgb()));
		_color = value;
	}
}
