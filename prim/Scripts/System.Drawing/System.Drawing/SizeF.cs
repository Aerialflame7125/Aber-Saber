using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Drawing;

/// <summary>Stores an ordered pair of floating-point numbers, typically the width and height of a rectangle.</summary>
[Serializable]
[ComVisible(true)]
[TypeConverter(typeof(SizeFConverter))]
public struct SizeF
{
	private float width;

	private float height;

	/// <summary>Gets a <see cref="T:System.Drawing.SizeF" /> structure that has a <see cref="P:System.Drawing.SizeF.Height" /> and <see cref="P:System.Drawing.SizeF.Width" /> value of 0.</summary>
	public static readonly SizeF Empty;

	/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.SizeF" /> structure has zero width and height.</summary>
	/// <returns>
	///   <see langword="true" /> when this <see cref="T:System.Drawing.SizeF" /> structure has both a width and height of zero; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	public bool IsEmpty
	{
		get
		{
			if ((double)width == 0.0)
			{
				return (double)height == 0.0;
			}
			return false;
		}
	}

	/// <summary>Gets or sets the horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <returns>The horizontal component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
	public float Width
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

	/// <summary>Gets or sets the vertical component of this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <returns>The vertical component of this <see cref="T:System.Drawing.SizeF" /> structure, typically measured in pixels.</returns>
	public float Height
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

	/// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
	/// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
	public static SizeF operator +(SizeF sz1, SizeF sz2)
	{
		return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
	}

	/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are equal.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the equality operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the equality operator.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(SizeF sz1, SizeF sz2)
	{
		if (sz1.Width == sz2.Width)
		{
			return sz1.Height == sz2.Height;
		}
		return false;
	}

	/// <summary>Tests whether two <see cref="T:System.Drawing.SizeF" /> structures are different.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left of the inequality operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right of the inequality operator.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; <see langword="false" /> if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
	public static bool operator !=(SizeF sz1, SizeF sz2)
	{
		if (sz1.Width == sz2.Width)
		{
			return sz1.Height != sz2.Height;
		}
		return true;
	}

	/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator.</param>
	/// <returns>A <see cref="T:System.Drawing.SizeF" /> that is the result of the subtraction operation.</returns>
	public static SizeF operator -(SizeF sz1, SizeF sz2)
	{
		return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
	/// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure to be converted</param>
	/// <returns>The <see cref="T:System.Drawing.PointF" /> structure to which this operator converts.</returns>
	public static explicit operator PointF(SizeF size)
	{
		return new PointF(size.Width, size.Height);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified <see cref="T:System.Drawing.PointF" /> structure.</summary>
	/// <param name="pt">The <see cref="T:System.Drawing.PointF" /> structure from which to initialize this <see cref="T:System.Drawing.SizeF" /> structure.</param>
	public SizeF(PointF pt)
	{
		width = pt.X;
		height = pt.Y;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified existing <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="size">The <see cref="T:System.Drawing.SizeF" /> structure from which to create the new <see cref="T:System.Drawing.SizeF" /> structure.</param>
	public SizeF(SizeF size)
	{
		width = size.Width;
		height = size.Height;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeF" /> structure from the specified dimensions.</summary>
	/// <param name="width">The width component of the new <see cref="T:System.Drawing.SizeF" /> structure.</param>
	/// <param name="height">The height component of the new <see cref="T:System.Drawing.SizeF" /> structure.</param>
	public SizeF(float width, float height)
	{
		this.width = width;
		this.height = height;
	}

	/// <summary>Tests to see whether the specified object is a <see cref="T:System.Drawing.SizeF" /> structure with the same dimensions as this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.SizeF" /> and has the same width and height as this <see cref="T:System.Drawing.SizeF" />; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is SizeF))
		{
			return false;
		}
		return this == (SizeF)obj;
	}

	/// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
	public override int GetHashCode()
	{
		return (int)width ^ (int)height;
	}

	/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.PointF" /> structure.</summary>
	/// <returns>A <see cref="T:System.Drawing.PointF" /> structure.</returns>
	public PointF ToPointF()
	{
		return new PointF(width, height);
	}

	/// <summary>Converts a <see cref="T:System.Drawing.SizeF" /> structure to a <see cref="T:System.Drawing.Size" /> structure.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure.</returns>
	public Size ToSize()
	{
		checked
		{
			int num = (int)width;
			int num2 = (int)height;
			return new Size(num, num2);
		}
	}

	/// <summary>Creates a human-readable string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <returns>A string that represents this <see cref="T:System.Drawing.SizeF" /> structure.</returns>
	public override string ToString()
	{
		return $"{{Width={width.ToString(CultureInfo.CurrentCulture)}, Height={height.ToString(CultureInfo.CurrentCulture)}}}";
	}

	/// <summary>Adds the width and height of one <see cref="T:System.Drawing.SizeF" /> structure to the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="sz1">The first <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
	/// <param name="sz2">The second <see cref="T:System.Drawing.SizeF" /> structure to add.</param>
	/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is the result of the addition operation.</returns>
	public static SizeF Add(SizeF sz1, SizeF sz2)
	{
		return new SizeF(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
	}

	/// <summary>Subtracts the width and height of one <see cref="T:System.Drawing.SizeF" /> structure from the width and height of another <see cref="T:System.Drawing.SizeF" /> structure.</summary>
	/// <param name="sz1">The <see cref="T:System.Drawing.SizeF" /> structure on the left side of the subtraction operator.</param>
	/// <param name="sz2">The <see cref="T:System.Drawing.SizeF" /> structure on the right side of the subtraction operator.</param>
	/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that is a result of the subtraction operation.</returns>
	public static SizeF Subtract(SizeF sz1, SizeF sz2)
	{
		return new SizeF(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
	}
}
