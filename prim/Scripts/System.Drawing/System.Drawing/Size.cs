using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing;

/// <summary>Stores an ordered pair of integers, which specify a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" />.</summary>
[Serializable]
[ComVisible(true)]
[TypeConverter(typeof(SizeConverter))]
public struct Size
{
	private int width;

	private int height;

	/// <summary>Gets a <see cref="T:System.Drawing.Size" /> structure that has a <see cref="P:System.Drawing.Size.Height" /> and <see cref="P:System.Drawing.Size.Width" /> value of 0.</summary>
	public static readonly Size Empty;

	/// <summary>Tests whether this <see cref="T:System.Drawing.Size" /> structure has width and height of 0.</summary>
	/// <returns>This property returns <see langword="true" /> when this <see cref="T:System.Drawing.Size" /> structure has both a width and height of 0; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	public bool IsEmpty
	{
		get
		{
			if (width == 0)
			{
				return height == 0;
			}
			return false;
		}
	}

	/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <returns>The horizontal component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			width = value;
		}
	}

	/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <returns>The vertical component of this <see cref="T:System.Drawing.Size" /> structure, typically measured in pixels.</returns>
	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.Size" /> structure to the next higher integer values.</summary>
	/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert.</param>
	/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
	public static Size Ceiling(SizeF value)
	{
		checked
		{
			int num = (int)Math.Ceiling(value.Width);
			int num2 = (int)Math.Ceiling(value.Height);
			return new Size(num, num2);
		}
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by rounding the values of the <see cref="T:System.Drawing.SizeF" /> structure to the nearest integer values.</summary>
	/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert.</param>
	/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
	public static Size Round(SizeF value)
	{
		checked
		{
			int num = (int)Math.Round(value.Width);
			int num2 = (int)Math.Round(value.Height);
			return new Size(num, num2);
		}
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure by truncating the values of the <see cref="T:System.Drawing.SizeF" /> structure to the next lower integer values.</summary>
	/// <param name="value">The <see cref="T:System.Drawing.SizeF" /> structure to convert.</param>
	/// <returns>The <see cref="T:System.Drawing.Size" /> structure this method converts to.</returns>
	public static Size Truncate(SizeF value)
	{
		checked
		{
			int num = (int)value.Width;
			int num2 = (int)value.Height;
			return new Size(num, num2);
		}
	}

	/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> to add.</param>
	/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> to add.</param>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
	public static Size operator +(Size sz1, Size sz2)
	{
		return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
	}

	/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are equal.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the equality operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the equality operator.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(Size sz1, Size sz2)
	{
		if (sz1.Width == sz2.Width)
		{
			return sz1.Height == sz2.Height;
		}
		return false;
	}

	/// <summary>Tests whether two <see cref="T:System.Drawing.Size" /> structures are different.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left of the inequality operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right of the inequality operator.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; <see langword="false" /> if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
	public static bool operator !=(Size sz1, Size sz2)
	{
		if (sz1.Width == sz2.Width)
		{
			return sz1.Height != sz2.Height;
		}
		return true;
	}

	/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator.</param>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the subtraction operation.</returns>
	public static Size operator -(Size sz1, Size sz2)
	{
		return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.Point" /> structure.</summary>
	/// <param name="size">The <see cref="T:System.Drawing.Size" /> structure to convert.</param>
	/// <returns>The <see cref="T:System.Drawing.Point" /> structure to which this operator converts.</returns>
	public static explicit operator Point(Size size)
	{
		return new Point(size.Width, size.Height);
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.Size" /> structure to a <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="p">The <see cref="T:System.Drawing.Size" /> structure to convert.</param>
	/// <returns>The <see cref="T:System.Drawing.SizeF" /> structure to which this operator converts.</returns>
	public static implicit operator SizeF(Size p)
	{
		return new SizeF(p.Width, p.Height);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified <see cref="T:System.Drawing.Point" /> structure.</summary>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> structure from which to initialize this <see cref="T:System.Drawing.Size" /> structure.</param>
	public Size(Point pt)
	{
		width = pt.X;
		height = pt.Y;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Size" /> structure from the specified dimensions.</summary>
	/// <param name="width">The width component of the new <see cref="T:System.Drawing.Size" />.</param>
	/// <param name="height">The height component of the new <see cref="T:System.Drawing.Size" />.</param>
	public Size(int width, int height)
	{
		this.width = width;
		this.height = height;
	}

	/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.Size" /> structure with the same dimensions as this <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Size" /> and has the same width and height as this <see cref="T:System.Drawing.Size" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is Size))
		{
			return false;
		}
		return this == (Size)obj;
	}

	/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
	public override int GetHashCode()
	{
		return width ^ height;
	}

	/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <returns>A string that represents this <see cref="T:System.Drawing.Size" />.</returns>
	public override string ToString()
	{
		return $"{{Width={width}, Height={height}}}";
	}

	/// <summary>Adds the width and height of one <see cref="T:System.Drawing.Size" /> structure to the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <param name="sz1">The first <see cref="T:System.Drawing.Size" /> structure to add.</param>
	/// <param name="sz2">The second <see cref="T:System.Drawing.Size" /> structure to add.</param>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
	public static Size Add(Size sz1, Size sz2)
	{
		return new Size(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
	}

	/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.Size" /> structure from the width and height of another <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.Size" /> structure on the left side of the subtraction operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.Size" /> structure on the right side of the subtraction operator.</param>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is a result of the subtraction operation.</returns>
	public static Size Subtract(Size sz1, Size sz2)
	{
		return new Size(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
	}
}
