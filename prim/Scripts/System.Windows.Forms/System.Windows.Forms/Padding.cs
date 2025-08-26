using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents padding or margin information associated with a user interface (UI) element.</summary>
/// <filterpriority>2</filterpriority>
[Serializable]
[TypeConverter(typeof(PaddingConverter))]
public struct Padding
{
	private int _bottom;

	private int _left;

	private int _right;

	private int _top;

	private bool _all;

	/// <summary>Provides a <see cref="T:System.Windows.Forms.Padding" /> object with no padding.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly Padding Empty = new Padding(0);

	/// <summary>Gets or sets the padding value for all the edges.</summary>
	/// <returns>The padding, in pixels, for all edges if the same; otherwise, -1.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public int All
	{
		get
		{
			if (!_all)
			{
				return -1;
			}
			return _top;
		}
		set
		{
			_all = true;
			_left = (_top = (_right = (_bottom = value)));
		}
	}

	/// <summary>Gets or sets the padding value for the bottom edge.</summary>
	/// <returns>The padding, in pixels, for the bottom edge.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public int Bottom
	{
		get
		{
			return _bottom;
		}
		set
		{
			_bottom = value;
			_all = false;
		}
	}

	/// <summary>Gets the combined padding for the right and left edges.</summary>
	/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Left" /> and <see cref="P:System.Windows.Forms.Padding.Right" /> padding values.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int Horizontal => _left + _right;

	/// <summary>Gets or sets the padding value for the left edge.</summary>
	/// <returns>The padding, in pixels, for the left edge.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public int Left
	{
		get
		{
			return _left;
		}
		set
		{
			_left = value;
			_all = false;
		}
	}

	/// <summary>Gets or sets the padding value for the right edge.</summary>
	/// <returns>The padding, in pixels, for the right edge.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public int Right
	{
		get
		{
			return _right;
		}
		set
		{
			_right = value;
			_all = false;
		}
	}

	/// <summary>Gets the padding information in the form of a <see cref="T:System.Drawing.Size" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> containing the padding information.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Size Size => new Size(Horizontal, Vertical);

	/// <summary>Gets or sets the padding value for the top edge.</summary>
	/// <returns>The padding, in pixels, for the top edge.</returns>
	/// <filterpriority>1</filterpriority>
	[RefreshProperties(RefreshProperties.All)]
	public int Top
	{
		get
		{
			return _top;
		}
		set
		{
			_top = value;
			_all = false;
		}
	}

	/// <summary>Gets the combined padding for the top and bottom edges.</summary>
	/// <returns>Gets the sum, in pixels, of the <see cref="P:System.Windows.Forms.Padding.Top" /> and <see cref="P:System.Windows.Forms.Padding.Bottom" /> padding values.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int Vertical => _top + _bottom;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using the supplied padding size for all edges.</summary>
	/// <param name="all">The number of pixels to be used for padding for all edges.</param>
	public Padding(int all)
	{
		_left = all;
		_right = all;
		_top = all;
		_bottom = all;
		_all = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Padding" /> class using a separate padding size for each edge.</summary>
	/// <param name="left">The padding size, in pixels, for the left edge.</param>
	/// <param name="top">The padding size, in pixels, for the top edge.</param>
	/// <param name="right">The padding size, in pixels, for the right edge.</param>
	/// <param name="bottom">The padding size, in pixels, for the bottom edge.</param>
	public Padding(int left, int top, int right, int bottom)
	{
		_left = left;
		_right = right;
		_top = top;
		_bottom = bottom;
		_all = _left == _top && _left == _right && _left == _bottom;
	}

	/// <summary>Computes the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the sum of the two specified <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
	/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
	/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
	public static Padding Add(Padding p1, Padding p2)
	{
		return p1 + p2;
	}

	/// <summary>Determines whether the value of the specified object is equivalent to the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent; otherwise, false.</returns>
	/// <param name="other">The object to compare to the current <see cref="T:System.Windows.Forms.Padding" />.</param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object other)
	{
		if (other is Padding padding)
		{
			return _left == padding.Left && _top == padding.Top && _right == padding.Right && _bottom == padding.Bottom;
		}
		return false;
	}

	/// <summary>Generates a hash code for the current <see cref="T:System.Windows.Forms.Padding" />. </summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return _top ^ _bottom ^ _left ^ _right;
	}

	/// <summary>Subtracts one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that contains the result of the subtraction of one specified <see cref="T:System.Windows.Forms.Padding" /> value from another.</returns>
	/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" />.</param>
	/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" />.</param>
	public static Padding Subtract(Padding p1, Padding p2)
	{
		return p1 - p2;
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.Padding" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Padding" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return "{Left=" + Left + ",Top=" + Top + ",Right=" + Right + ",Bottom=" + Bottom + "}";
	}

	/// <summary>Performs vector addition on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.Padding" /> that results from adding <paramref name="p1" /> and <paramref name="p2" />.</returns>
	/// <param name="p1">The first <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
	/// <param name="p2">The second <see cref="T:System.Windows.Forms.Padding" /> to add.</param>
	/// <filterpriority>3</filterpriority>
	public static Padding operator +(Padding p1, Padding p2)
	{
		return new Padding(p1.Left + p2.Left, p1.Top + p2.Top, p1.Right + p2.Right, p1.Bottom + p2.Bottom);
	}

	/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are equivalent.</summary>
	/// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are equal; otherwise, false.</returns>
	/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
	/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
	/// <filterpriority>3</filterpriority>
	public static bool operator ==(Padding p1, Padding p2)
	{
		return p1.Equals(p2);
	}

	/// <summary>Tests whether two specified <see cref="T:System.Windows.Forms.Padding" /> objects are not equivalent.</summary>
	/// <returns>true if the two <see cref="T:System.Windows.Forms.Padding" /> objects are different; otherwise, false.</returns>
	/// <param name="p1">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
	/// <param name="p2">A <see cref="T:System.Windows.Forms.Padding" /> to test.</param>
	/// <filterpriority>3</filterpriority>
	public static bool operator !=(Padding p1, Padding p2)
	{
		return !p1.Equals(p2);
	}

	/// <summary>Performs vector subtraction on the two specified <see cref="T:System.Windows.Forms.Padding" /> objects, resulting in a new <see cref="T:System.Windows.Forms.Padding" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Padding" /> result of subtracting <paramref name="p2" /> from <paramref name="p1" />.</returns>
	/// <param name="p1">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the minuend).</param>
	/// <param name="p2">The <see cref="T:System.Windows.Forms.Padding" /> to subtract from (the subtrahend).</param>
	/// <filterpriority>3</filterpriority>
	public static Padding operator -(Padding p1, Padding p2)
	{
		return new Padding(p1.Left - p2.Left, p1.Top - p2.Top, p1.Right - p2.Right, p1.Bottom - p2.Bottom);
	}
}
