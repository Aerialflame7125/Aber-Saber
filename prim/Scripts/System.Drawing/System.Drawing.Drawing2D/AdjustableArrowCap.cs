using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D;

/// <summary>Represents an adjustable arrow-shaped line cap. This class cannot be inherited.</summary>
public sealed class AdjustableArrowCap : CustomLineCap
{
	/// <summary>Gets or sets the height of the arrow cap.</summary>
	/// <returns>The height of the arrow cap.</returns>
	public float Height
	{
		get
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapHeight(new HandleRef(this, nativeCap), out var height));
			return height;
		}
		set
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapHeight(new HandleRef(this, nativeCap), value));
		}
	}

	/// <summary>Gets or sets the width of the arrow cap.</summary>
	/// <returns>The width, in units, of the arrow cap.</returns>
	public float Width
	{
		get
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapWidth(new HandleRef(this, nativeCap), out var width));
			return width;
		}
		set
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapWidth(new HandleRef(this, nativeCap), value));
		}
	}

	/// <summary>Gets or sets the number of units between the outline of the arrow cap and the fill.</summary>
	/// <returns>The number of units between the outline of the arrow cap and the fill of the arrow cap.</returns>
	public float MiddleInset
	{
		get
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapMiddleInset(new HandleRef(this, nativeCap), out var middleInset));
			return middleInset;
		}
		set
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapMiddleInset(new HandleRef(this, nativeCap), value));
		}
	}

	/// <summary>Gets or sets whether the arrow cap is filled.</summary>
	/// <returns>This property is <see langword="true" /> if the arrow cap is filled; otherwise, <see langword="false" />.</returns>
	public bool Filled
	{
		get
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetAdjustableArrowCapFillState(new HandleRef(this, nativeCap), out var isFilled));
			return isFilled;
		}
		set
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipSetAdjustableArrowCapFillState(new HandleRef(this, nativeCap), value));
		}
	}

	internal AdjustableArrowCap(IntPtr nativeCap)
		: base(nativeCap)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width and height. The arrow end caps created with this constructor are always filled.</summary>
	/// <param name="width">The width of the arrow.</param>
	/// <param name="height">The height of the arrow.</param>
	public AdjustableArrowCap(float width, float height)
		: this(width, height, isFilled: true)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.AdjustableArrowCap" /> class with the specified width, height, and fill property. Whether an arrow end cap is filled depends on the argument passed to the <paramref name="isFilled" /> parameter.</summary>
	/// <param name="width">The width of the arrow.</param>
	/// <param name="height">The height of the arrow.</param>
	/// <param name="isFilled">
	///   <see langword="true" /> to fill the arrow cap; otherwise, <see langword="false" />.</param>
	public AdjustableArrowCap(float width, float height, bool isFilled)
	{
		SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateAdjustableArrowCap(height, width, isFilled, out var arrowCap));
		SetNativeLineCap(arrowCap);
	}
}
