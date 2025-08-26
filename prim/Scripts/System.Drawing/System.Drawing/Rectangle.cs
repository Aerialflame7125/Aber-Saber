using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing;

/// <summary>Stores a set of four integers that represent the location and size of a rectangle</summary>
[Serializable]
[ComVisible(true)]
[TypeConverter(typeof(RectangleConverter))]
public struct Rectangle
{
	private int x;

	private int y;

	private int width;

	private int height;

	/// <summary>Represents a <see cref="T:System.Drawing.Rectangle" /> structure with its properties left uninitialized.</summary>
	public static readonly Rectangle Empty;

	/// <summary>Gets the y-coordinate that is the sum of the <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The y-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.Y" /> and <see cref="P:System.Drawing.Rectangle.Height" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
	[Browsable(false)]
	public int Bottom => y + height;

	/// <summary>Gets or sets the height of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The height of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
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

	/// <summary>Tests whether all numeric properties of this <see cref="T:System.Drawing.Rectangle" /> have values of zero.</summary>
	/// <returns>This property returns <see langword="true" /> if the <see cref="P:System.Drawing.Rectangle.Width" />, <see cref="P:System.Drawing.Rectangle.Height" />, <see cref="P:System.Drawing.Rectangle.X" />, and <see cref="P:System.Drawing.Rectangle.Y" /> properties of this <see cref="T:System.Drawing.Rectangle" /> all have values of zero; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	public bool IsEmpty
	{
		get
		{
			if (x == 0 && y == 0 && width == 0)
			{
				return height == 0;
			}
			return false;
		}
	}

	/// <summary>Gets the x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The x-coordinate of the left edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
	[Browsable(false)]
	public int Left => X;

	/// <summary>Gets or sets the coordinates of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
	[Browsable(false)]
	public Point Location
	{
		get
		{
			return new Point(x, y);
		}
		set
		{
			x = value.X;
			y = value.Y;
		}
	}

	/// <summary>Gets the x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> property values of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The x-coordinate that is the sum of <see cref="P:System.Drawing.Rectangle.X" /> and <see cref="P:System.Drawing.Rectangle.Width" /> of this <see cref="T:System.Drawing.Rectangle" />.</returns>
	[Browsable(false)]
	public int Right => X + Width;

	/// <summary>Gets or sets the size of this <see cref="T:System.Drawing.Rectangle" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
	[Browsable(false)]
	public Size Size
	{
		get
		{
			return new Size(Width, Height);
		}
		set
		{
			Width = value.Width;
			Height = value.Height;
		}
	}

	/// <summary>Gets the y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The y-coordinate of the top edge of this <see cref="T:System.Drawing.Rectangle" /> structure.</returns>
	[Browsable(false)]
	public int Top => y;

	/// <summary>Gets or sets the width of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The width of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
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

	/// <summary>Gets or sets the x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
	public int X
	{
		get
		{
			return x;
		}
		set
		{
			x = value;
		}
	}

	/// <summary>Gets or sets the y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <returns>The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure. The default is 0.</returns>
	public int Y
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> structure to a <see cref="T:System.Drawing.Rectangle" /> structure by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the next higher integer values.</summary>
	/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> structure to be converted.</param>
	/// <returns>Returns a <see cref="T:System.Drawing.Rectangle" />.</returns>
	public static Rectangle Ceiling(RectangleF value)
	{
		checked
		{
			int num = (int)Math.Ceiling(value.X);
			int num2 = (int)Math.Ceiling(value.Y);
			int num3 = (int)Math.Ceiling(value.Width);
			int num4 = (int)Math.Ceiling(value.Height);
			return new Rectangle(num, num2, num3, num4);
		}
	}

	/// <summary>Creates a <see cref="T:System.Drawing.Rectangle" /> structure with the specified edge locations.</summary>
	/// <param name="left">The x-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
	/// <param name="top">The y-coordinate of the upper-left corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
	/// <param name="right">The x-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
	/// <param name="bottom">The y-coordinate of the lower-right corner of this <see cref="T:System.Drawing.Rectangle" /> structure.</param>
	/// <returns>The new <see cref="T:System.Drawing.Rectangle" /> that this method creates.</returns>
	public static Rectangle FromLTRB(int left, int top, int right, int bottom)
	{
		return new Rectangle(left, top, right - left, bottom - top);
	}

	/// <summary>Creates and returns an enlarged copy of the specified <see cref="T:System.Drawing.Rectangle" /> structure. The copy is enlarged by the specified amount. The original <see cref="T:System.Drawing.Rectangle" /> structure remains unmodified.</summary>
	/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to start. This rectangle is not modified.</param>
	/// <param name="x">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally.</param>
	/// <param name="y">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically.</param>
	/// <returns>The enlarged <see cref="T:System.Drawing.Rectangle" />.</returns>
	public static Rectangle Inflate(Rectangle rect, int x, int y)
	{
		Rectangle result = new Rectangle(rect.Location, rect.Size);
		result.Inflate(x, y);
		return result;
	}

	/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
	/// <param name="width">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> horizontally.</param>
	/// <param name="height">The amount to inflate this <see cref="T:System.Drawing.Rectangle" /> vertically.</param>
	public void Inflate(int width, int height)
	{
		Inflate(new Size(width, height));
	}

	/// <summary>Enlarges this <see cref="T:System.Drawing.Rectangle" /> by the specified amount.</summary>
	/// <param name="size">The amount to inflate this rectangle.</param>
	public void Inflate(Size size)
	{
		x -= size.Width;
		y -= size.Height;
		Width += size.Width * 2;
		Height += size.Height * 2;
	}

	/// <summary>Returns a third <see cref="T:System.Drawing.Rectangle" /> structure that represents the intersection of two other <see cref="T:System.Drawing.Rectangle" /> structures. If there is no intersection, an empty <see cref="T:System.Drawing.Rectangle" /> is returned.</summary>
	/// <param name="a">A rectangle to intersect.</param>
	/// <param name="b">A rectangle to intersect.</param>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the intersection of <paramref name="a" /> and <paramref name="b" />.</returns>
	public static Rectangle Intersect(Rectangle a, Rectangle b)
	{
		if (!a.IntersectsWithInclusive(b))
		{
			return Empty;
		}
		return FromLTRB(Math.Max(a.Left, b.Left), Math.Max(a.Top, b.Top), Math.Min(a.Right, b.Right), Math.Min(a.Bottom, b.Bottom));
	}

	/// <summary>Replaces this <see cref="T:System.Drawing.Rectangle" /> with the intersection of itself and the specified <see cref="T:System.Drawing.Rectangle" />.</summary>
	/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> with which to intersect.</param>
	public void Intersect(Rectangle rect)
	{
		this = Intersect(this, rect);
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by rounding the <see cref="T:System.Drawing.RectangleF" /> values to the nearest integer values.</summary>
	/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted.</param>
	/// <returns>The rounded interger value of the <see cref="T:System.Drawing.Rectangle" />.</returns>
	public static Rectangle Round(RectangleF value)
	{
		checked
		{
			int num = (int)Math.Round(value.X);
			int num2 = (int)Math.Round(value.Y);
			int num3 = (int)Math.Round(value.Width);
			int num4 = (int)Math.Round(value.Height);
			return new Rectangle(num, num2, num3, num4);
		}
	}

	/// <summary>Converts the specified <see cref="T:System.Drawing.RectangleF" /> to a <see cref="T:System.Drawing.Rectangle" /> by truncating the <see cref="T:System.Drawing.RectangleF" /> values.</summary>
	/// <param name="value">The <see cref="T:System.Drawing.RectangleF" /> to be converted.</param>
	/// <returns>The truncated value of the  <see cref="T:System.Drawing.Rectangle" />.</returns>
	public static Rectangle Truncate(RectangleF value)
	{
		checked
		{
			int num = (int)value.X;
			int num2 = (int)value.Y;
			int num3 = (int)value.Width;
			int num4 = (int)value.Height;
			return new Rectangle(num, num2, num3, num4);
		}
	}

	/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> structure that contains the union of two <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
	/// <param name="a">A rectangle to union.</param>
	/// <param name="b">A rectangle to union.</param>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> structure that bounds the union of the two <see cref="T:System.Drawing.Rectangle" /> structures.</returns>
	public static Rectangle Union(Rectangle a, Rectangle b)
	{
		return FromLTRB(Math.Min(a.Left, b.Left), Math.Min(a.Top, b.Top), Math.Max(a.Right, b.Right), Math.Max(a.Bottom, b.Bottom));
	}

	/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures have equal location and size.</summary>
	/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the equality operator.</param>
	/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the equality operator.</param>
	/// <returns>This operator returns <see langword="true" /> if the two <see cref="T:System.Drawing.Rectangle" /> structures have equal <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties.</returns>
	public static bool operator ==(Rectangle left, Rectangle right)
	{
		if (left.Location == right.Location)
		{
			return left.Size == right.Size;
		}
		return false;
	}

	/// <summary>Tests whether two <see cref="T:System.Drawing.Rectangle" /> structures differ in location or size.</summary>
	/// <param name="left">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the left of the inequality operator.</param>
	/// <param name="right">The <see cref="T:System.Drawing.Rectangle" /> structure that is to the right of the inequality operator.</param>
	/// <returns>This operator returns <see langword="true" /> if any of the <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" /> or <see cref="P:System.Drawing.Rectangle.Height" /> properties of the two <see cref="T:System.Drawing.Rectangle" /> structures are unequal; otherwise <see langword="false" />.</returns>
	public static bool operator !=(Rectangle left, Rectangle right)
	{
		if (!(left.Location != right.Location))
		{
			return left.Size != right.Size;
		}
		return true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
	/// <param name="location">A <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the rectangular region.</param>
	/// <param name="size">A <see cref="T:System.Drawing.Size" /> that represents the width and height of the rectangular region.</param>
	public Rectangle(Point location, Size size)
	{
		x = location.X;
		y = location.Y;
		width = size.Width;
		height = size.Height;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Rectangle" /> class with the specified location and size.</summary>
	/// <param name="x">The x-coordinate of the upper-left corner of the rectangle.</param>
	/// <param name="y">The y-coordinate of the upper-left corner of the rectangle.</param>
	/// <param name="width">The width of the rectangle.</param>
	/// <param name="height">The height of the rectangle.</param>
	public Rectangle(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <param name="x">The x-coordinate of the point to test.</param>
	/// <param name="y">The y-coordinate of the point to test.</param>
	/// <returns>This method returns <see langword="true" /> if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
	public bool Contains(int x, int y)
	{
		if (x >= Left && x < Right && y >= Top)
		{
			return y < Bottom;
		}
		return false;
	}

	/// <summary>Determines if the specified point is contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <param name="pt">The <see cref="T:System.Drawing.Point" /> to test.</param>
	/// <returns>This method returns <see langword="true" /> if the point represented by <paramref name="pt" /> is contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
	public bool Contains(Point pt)
	{
		return Contains(pt.X, pt.Y);
	}

	/// <summary>Determines if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> to test.</param>
	/// <returns>This method returns <see langword="true" /> if the rectangular region represented by <paramref name="rect" /> is entirely contained within this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise <see langword="false" />.</returns>
	public bool Contains(Rectangle rect)
	{
		return rect == Intersect(this, rect);
	}

	/// <summary>Tests whether <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure with the same location and size of this <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
	/// <returns>This method returns <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.Rectangle" /> structure and its <see cref="P:System.Drawing.Rectangle.X" />, <see cref="P:System.Drawing.Rectangle.Y" />, <see cref="P:System.Drawing.Rectangle.Width" />, and <see cref="P:System.Drawing.Rectangle.Height" /> properties are equal to the corresponding properties of this <see cref="T:System.Drawing.Rectangle" /> structure; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is Rectangle))
		{
			return false;
		}
		return this == (Rectangle)obj;
	}

	/// <summary>Returns the hash code for this <see cref="T:System.Drawing.Rectangle" /> structure. For information about the use of hash codes, see <see cref="M:System.Object.GetHashCode" /> .</summary>
	/// <returns>An integer that represents the hash code for this rectangle.</returns>
	public override int GetHashCode()
	{
		return (height + width) ^ (x + y);
	}

	/// <summary>Determines if this rectangle intersects with <paramref name="rect" />.</summary>
	/// <param name="rect">The rectangle to test.</param>
	/// <returns>This method returns <see langword="true" /> if there is any intersection, otherwise <see langword="false" />.</returns>
	public bool IntersectsWith(Rectangle rect)
	{
		if (Left < rect.Right && Right > rect.Left && Top < rect.Bottom)
		{
			return Bottom > rect.Top;
		}
		return false;
	}

	private bool IntersectsWithInclusive(Rectangle r)
	{
		if (Left <= r.Right && Right >= r.Left && Top <= r.Bottom)
		{
			return Bottom >= r.Top;
		}
		return false;
	}

	/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
	/// <param name="x">The horizontal offset.</param>
	/// <param name="y">The vertical offset.</param>
	public void Offset(int x, int y)
	{
		this.x += x;
		this.y += y;
	}

	/// <summary>Adjusts the location of this rectangle by the specified amount.</summary>
	/// <param name="pos">Amount to offset the location.</param>
	public void Offset(Point pos)
	{
		x += pos.X;
		y += pos.Y;
	}

	/// <summary>Converts the attributes of this <see cref="T:System.Drawing.Rectangle" /> to a human-readable string.</summary>
	/// <returns>A string that contains the position, width, and height of this <see cref="T:System.Drawing.Rectangle" /> structure ¾ for example, {X=20, Y=20, Width=100, Height=50}</returns>
	public override string ToString()
	{
		return $"{{X={x},Y={y},Width={width},Height={height}}}";
	}
}
