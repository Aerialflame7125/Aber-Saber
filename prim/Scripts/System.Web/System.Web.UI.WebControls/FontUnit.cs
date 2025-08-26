using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Represents the size of a font.</summary>
[Serializable]
[TypeConverter(typeof(FontUnitConverter))]
public struct FontUnit
{
	private FontSize type;

	private Unit unit;

	/// <summary>Represents an empty <see cref="T:System.Web.UI.WebControls.FontUnit" /> object.</summary>
	public static readonly FontUnit Empty;

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.Smaller" />.</summary>
	public static readonly FontUnit Smaller = new FontUnit(FontSize.Smaller);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.Larger" />.</summary>
	public static readonly FontUnit Larger = new FontUnit(FontSize.Larger);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.XXSmall" />.</summary>
	public static readonly FontUnit XXSmall = new FontUnit(FontSize.XXSmall);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.XSmall" />.</summary>
	public static readonly FontUnit XSmall = new FontUnit(FontSize.XSmall);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.Small" />.</summary>
	public static readonly FontUnit Small = new FontUnit(FontSize.Small);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.Medium" />.</summary>
	public static readonly FontUnit Medium = new FontUnit(FontSize.Medium);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.Large" />.</summary>
	public static readonly FontUnit Large = new FontUnit(FontSize.Large);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.XLarge" />.</summary>
	public static readonly FontUnit XLarge = new FontUnit(FontSize.XLarge);

	/// <summary>Represents a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object with the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property set to <see langword="FontSize.XXLarge" />.</summary>
	public static readonly FontUnit XXLarge = new FontUnit(FontSize.XXLarge);

	private static string[] font_size_names = new string[11]
	{
		null, null, "Smaller", "Larger", "XX-Small", "X-Small", "Small", "Medium", "Large", "X-Large",
		"XX-Large"
	};

	/// <summary>Gets a value that indicates whether the font size has been set.</summary>
	/// <returns>
	///     <see langword="true" /> if the font size has not been set; otherwise, <see langword="false" />.</returns>
	public bool IsEmpty => type == FontSize.NotSet;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.FontSize" /> enumeration value that represents the font size.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values.</returns>
	public FontSize Type => type;

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the font size.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that specifies the font size.</returns>
	public Unit Unit => unit;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified <see cref="T:System.Web.UI.WebControls.FontSize" />.</summary>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified font size is not one of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </exception>
	public FontUnit(FontSize type)
	{
		if (type < FontSize.NotSet || type > FontSize.XXLarge)
		{
			throw new ArgumentOutOfRangeException("type");
		}
		this.type = type;
		if (type == FontSize.AsUnit)
		{
			unit = new Unit(10.0, UnitType.Point);
		}
		else
		{
			unit = Unit.Empty;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified font size.</summary>
	/// <param name="value">The size of the font. </param>
	public FontUnit(int value)
		: this(new Unit(value, UnitType.Point))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified font size in points.</summary>
	/// <param name="value">A <see cref="T:System.Double" /> that specifies the font size. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="value" /> is outside the valid range.</exception>
	public FontUnit(double value)
		: this(new Unit(value, UnitType.Point))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified font size and <see cref="T:System.Web.UI.WebControls.UnitType" /> value.</summary>
	/// <param name="value">A <see cref="T:System.Double" /> that specifies the font size. </param>
	/// <param name="type">A <see cref="T:System.Web.UI.WebControls.UnitType" /> to specify the units of the size.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="value" /> is outside the valid range.</exception>
	public FontUnit(double value, UnitType type)
		: this(new Unit(value, type))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
	/// <param name="value">A <see cref="T:System.Web.UI.WebControls.Unit" /> that specifies the font size. </param>
	public FontUnit(Unit value)
	{
		type = FontSize.AsUnit;
		unit = value;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified string.</summary>
	/// <param name="value">A string to specify the font size.</param>
	public FontUnit(string value)
		: this(value, Thread.CurrentThread.CurrentCulture)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class with the specified string using the specified <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="value">A string to specify the font size.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to make string comparisons.</param>
	public FontUnit(string value, CultureInfo culture)
	{
		if (string.IsNullOrEmpty(value))
		{
			type = FontSize.NotSet;
			unit = Unit.Empty;
			return;
		}
		switch (value.ToLower(Helpers.InvariantCulture))
		{
		case "smaller":
			type = FontSize.Smaller;
			break;
		case "larger":
			type = FontSize.Larger;
			break;
		case "xxsmall":
			type = FontSize.XXSmall;
			break;
		case "xx-small":
			type = FontSize.XXSmall;
			break;
		case "xsmall":
			type = FontSize.XSmall;
			break;
		case "x-small":
			type = FontSize.XSmall;
			break;
		case "small":
			type = FontSize.Small;
			break;
		case "medium":
			type = FontSize.Medium;
			break;
		case "large":
			type = FontSize.Large;
			break;
		case "xlarge":
			type = FontSize.XLarge;
			break;
		case "x-large":
			type = FontSize.XLarge;
			break;
		case "xxlarge":
			type = FontSize.XXLarge;
			break;
		case "xx-large":
			type = FontSize.XXLarge;
			break;
		default:
			type = FontSize.AsUnit;
			unit = new Unit(value, culture);
			return;
		}
		unit = Unit.Empty;
	}

	/// <summary>Converts the specified string to its <see cref="T:System.Web.UI.WebControls.FontUnit" /> equivalent.</summary>
	/// <param name="s">A string representation of one of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size specified by the <paramref name="s" /> parameter.</returns>
	public static FontUnit Parse(string s)
	{
		return new FontUnit(s);
	}

	/// <summary>Converts the specified string to its <see cref="T:System.Web.UI.WebControls.FontUnit" /> equivalent in the specified culture.</summary>
	/// <param name="s">A string representation of one of the <see cref="T:System.Web.UI.WebControls.FontSize" /> values. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size specified by the <paramref name="s" /> parameter in the culture specified by the <paramref name="culture" /> parameter.</returns>
	public static FontUnit Parse(string s, CultureInfo culture)
	{
		return new FontUnit(s, culture);
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.FontUnit" /> of type <see cref="T:System.Drawing.Point" /> from an integer value.</summary>
	/// <param name="n">An integer representing the <see cref="T:System.Drawing.Point" /> value to convert to a <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that represents the font size specified by the <paramref name="n" /> parameter.</returns>
	public static FontUnit Point(int n)
	{
		return new FontUnit(n);
	}

	/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equivalent to the instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class that this method is called from.</summary>
	/// <param name="obj">A <see cref="T:System.Object" /> that contains the object to compare to this instance. </param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Object" /> is equivalent to the instance of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> class that this method is called from; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is FontUnit fontUnit)
		{
			if (fontUnit.type == type)
			{
				return fontUnit.unit == unit;
			}
			return false;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer that represents the hash code.</returns>
	public override int GetHashCode()
	{
		return type.GetHashCode() ^ unit.GetHashCode();
	}

	/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects for equality.</summary>
	/// <param name="left">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> on the left of the operator that contains font properties. </param>
	/// <param name="right">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> on the right of the operator that contains font properties. </param>
	/// <returns>
	///     <see langword="true" /> if both <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects are equal; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(FontUnit left, FontUnit right)
	{
		if (left.type == right.type)
		{
			return left.unit == right.unit;
		}
		return false;
	}

	/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects for inequality.</summary>
	/// <param name="left">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that contains font properties on the left of the operator. </param>
	/// <param name="right">A <see cref="T:System.Web.UI.WebControls.FontUnit" /> that contains font properties on the right of the operator. </param>
	/// <returns>
	///     <see langword="true" /> if both <see cref="T:System.Web.UI.WebControls.FontUnit" /> objects are not equal; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(FontUnit left, FontUnit right)
	{
		if (left.type == right.type)
		{
			return left.unit != right.unit;
		}
		return true;
	}

	/// <summary>Implicitly creates a <see cref="T:System.Web.UI.WebControls.FontUnit" /> of type <see cref="T:System.Drawing.Point" /> from an integer value.</summary>
	/// <param name="n">An integer representing the <see cref="T:System.Drawing.Point" /> value to convert into a <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontUnit" /> of type <see cref="T:System.Drawing.Point" /> that represents the font size specified the <paramref name="n" /> parameter.</returns>
	public static implicit operator FontUnit(int n)
	{
		return new FontUnit(n);
	}

	/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to a string equivalent using the specified format provider.</summary>
	/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information, which is used if the <see cref="P:System.Web.UI.WebControls.FontUnit.Type" /> property is set to the <see cref="F:System.Web.UI.WebControls.FontSize.AsUnit" /> value; otherwise, it is ignored.</param>
	/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.FontUnit" />, with any numeric unit value in the format specified by <paramref name="formatProvider" />.</returns>
	public string ToString(IFormatProvider formatProvider)
	{
		if (type == FontSize.NotSet)
		{
			return string.Empty;
		}
		if (type == FontSize.AsUnit)
		{
			return unit.ToString(formatProvider);
		}
		return font_size_names[(int)type];
	}

	/// <summary>Converts the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to a string representation, using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that contains the culture to represent the <see cref="T:System.Web.UI.WebControls.FontUnit" />. </param>
	/// <returns>The string representation of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object in the specified culture.</returns>
	public string ToString(CultureInfo culture)
	{
		if (type == FontSize.NotSet)
		{
			return string.Empty;
		}
		if (type == FontSize.AsUnit)
		{
			return unit.ToString(culture);
		}
		return font_size_names[(int)type];
	}

	/// <summary>Converts the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object to the default string representation.</summary>
	/// <returns>The string representation of the <see cref="T:System.Web.UI.WebControls.FontUnit" /> object.</returns>
	public override string ToString()
	{
		return ToString(CultureInfo.CurrentCulture);
	}
}
