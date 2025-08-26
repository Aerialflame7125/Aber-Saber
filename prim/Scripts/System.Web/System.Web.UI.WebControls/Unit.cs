using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Represents a length measurement.</summary>
[Serializable]
[TypeConverter(typeof(UnitConverter))]
public struct Unit
{
	private enum ParsingStage
	{
		Trim,
		SignOrSep,
		DigitOrSep,
		DigitOrUnit,
		Unit
	}

	private UnitType type;

	private double value;

	private bool valueSet;

	/// <summary>Represents an empty <see cref="T:System.Web.UI.WebControls.Unit" />. This field is read-only.</summary>
	public static readonly Unit Empty;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Unit" /> is empty.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.Unit" /> is empty; otherwise, <see langword="false" />.</returns>
	public bool IsEmpty => type == (UnitType)0;

	/// <summary>Gets the unit type of the <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.UnitType" /> enumeration values. The default is <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" />.</returns>
	public UnitType Type
	{
		get
		{
			if (type == (UnitType)0)
			{
				return UnitType.Pixel;
			}
			return type;
		}
	}

	/// <summary>Gets the length of the <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
	/// <returns>A double-precision floating point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />.</returns>
	public double Value => value;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified double precision floating point number and <see cref="T:System.Web.UI.WebControls.UnitType" />.</summary>
	/// <param name="value">A double precision floating point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
	/// <param name="type">One of the <see cref="T:System.Web.UI.WebControls.UnitType" /> enumeration values. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="value" /> is not between -32768 and 32767. </exception>
	public Unit(double value, UnitType type)
	{
		if (value < -32768.0 || value > 32767.0)
		{
			throw new ArgumentOutOfRangeException("value");
		}
		this.type = type;
		if (type == UnitType.Pixel)
		{
			this.value = (int)value;
		}
		else
		{
			this.value = value;
		}
		valueSet = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified double precision floating point number.</summary>
	/// <param name="value">A double precision floating point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" /> in pixels. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="value" /> is not between -32768 and 32767. </exception>
	public Unit(double value)
		: this(value, UnitType.Pixel)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified 32-bit signed integer.</summary>
	/// <param name="value">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" /> in pixels. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="value" /> is not between -32768 and 32767. </exception>
	public Unit(int value)
		: this(value, UnitType.Pixel)
	{
	}

	internal Unit(string input, char sep)
	{
		if (input == null || input == string.Empty)
		{
			type = (UnitType)0;
			value = 0.0;
			valueSet = false;
			return;
		}
		value = 0.0;
		double num = 0.0;
		double num2 = 0.1;
		int num3 = 0;
		int length = input.Length;
		int num4 = 1;
		int num5 = -1;
		int num6 = 0;
		int num7 = 0;
		ParsingStage parsingStage = ParsingStage.Trim;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		while (!flag && num3 < length)
		{
			char c = input[num3];
			switch (parsingStage)
			{
			case ParsingStage.Trim:
				if (char.IsWhiteSpace(c))
				{
					num3++;
				}
				else
				{
					parsingStage = ParsingStage.SignOrSep;
				}
				break;
			case ParsingStage.SignOrSep:
				num7 = 0;
				if (c == '-')
				{
					num4 = -1;
					num3++;
					parsingStage = ParsingStage.DigitOrSep;
				}
				else if (c == sep)
				{
					num3++;
					flag2 = true;
					parsingStage = ParsingStage.DigitOrUnit;
					num = 0.0;
				}
				else
				{
					if (!char.IsDigit(c))
					{
						throw new FormatException();
					}
					parsingStage = ParsingStage.DigitOrSep;
				}
				break;
			case ParsingStage.DigitOrSep:
			{
				if (char.IsDigit(c))
				{
					num = num * 10.0 + (double)(int)c - 48.0;
					num3++;
					flag3 = true;
					break;
				}
				if (c == sep)
				{
					if (num7 > 0)
					{
						throw new ArgumentOutOfRangeException("input");
					}
					num3++;
					flag2 = true;
					value = num * (double)num4;
					num = 0.0;
					parsingStage = ParsingStage.DigitOrUnit;
					break;
				}
				bool flag4 = char.IsWhiteSpace(c);
				if (flag4 || c == '%' || char.IsLetter(c))
				{
					if (flag4)
					{
						if (!flag3)
						{
							throw new ArgumentOutOfRangeException("input");
						}
						num7++;
						num3++;
						break;
					}
					value = num * (double)num4;
					num = 0.0;
					num5 = num3;
					if (flag2)
					{
						flag3 = false;
						parsingStage = ParsingStage.DigitOrUnit;
					}
					else
					{
						parsingStage = ParsingStage.Unit;
					}
					num7 = 0;
					break;
				}
				throw new FormatException();
			}
			case ParsingStage.DigitOrUnit:
			{
				if (c == '%')
				{
					num5 = num3;
					num6 = 1;
					flag = true;
					break;
				}
				bool flag4 = char.IsWhiteSpace(c);
				if (flag4 || char.IsLetter(c))
				{
					if (flag4)
					{
						num7++;
						num3++;
					}
					else
					{
						parsingStage = ParsingStage.Unit;
						num5 = num3;
					}
					break;
				}
				if (char.IsDigit(c))
				{
					if (num7 > 0)
					{
						throw new ArgumentOutOfRangeException();
					}
					num += (double)(c - 48) * num2;
					num2 *= 0.1;
					num3++;
					break;
				}
				throw new FormatException();
			}
			case ParsingStage.Unit:
				if (c == '%' || char.IsLetter(c))
				{
					num3++;
					num6++;
				}
				else if (num6 == 0 && char.IsWhiteSpace(c))
				{
					num3++;
					num5++;
				}
				else
				{
					flag = true;
				}
				break;
			}
		}
		value += num * (double)num4;
		if (num5 >= 0)
		{
			int num8 = num5 + num6;
			if (num8 < length)
			{
				for (int i = num8; i < length; i++)
				{
					if (!char.IsWhiteSpace(input[i]))
					{
						throw new ArgumentOutOfRangeException("input");
					}
				}
			}
			if (num6 == 1 && input[num5] == '%')
			{
				type = UnitType.Percentage;
			}
			else
			{
				switch (input.Substring(num5, num6).ToLower(Helpers.InvariantCulture))
				{
				case "in":
					type = UnitType.Inch;
					break;
				case "cm":
					type = UnitType.Cm;
					break;
				case "mm":
					type = UnitType.Mm;
					break;
				case "pt":
					type = UnitType.Point;
					break;
				case "pc":
					type = UnitType.Pica;
					break;
				case "em":
					type = UnitType.Em;
					break;
				case "ex":
					type = UnitType.Ex;
					break;
				case "px":
					type = UnitType.Pixel;
					break;
				default:
					throw new ArgumentOutOfRangeException("value");
				}
			}
		}
		else
		{
			type = UnitType.Pixel;
		}
		if (flag2 && type == UnitType.Pixel)
		{
			throw new FormatException("Pixel units do not allow floating point values");
		}
		valueSet = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified length.</summary>
	/// <param name="value">A string that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified length is not between -32768 and 32767. </exception>
	/// <exception cref="T:System.FormatException">
	///         <paramref name="value" /> is not a valid CSS-compliant unit expression. </exception>
	public Unit(string value)
		: this(value, '.')
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Unit" /> structure with the specified length and <see cref="T:System.Globalization.CultureInfo" />.</summary>
	/// <param name="value">A string that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified length is not between -32768 and 32767. </exception>
	/// <exception cref="T:System.FormatException">
	///         <paramref name="value" /> is not a valid CSS-compliant unit expression. </exception>
	public Unit(string value, CultureInfo culture)
		: this(value, culture.NumberFormat.NumberDecimalSeparator[0])
	{
	}

	internal Unit(string value, CultureInfo culture, UnitType t)
		: this(value, '.')
	{
	}

	/// <summary>Converts the specified string to a <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
	/// <param name="s">The string to convert. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the specified string.</returns>
	public static Unit Parse(string s)
	{
		return new Unit(s);
	}

	/// <summary>Converts the specified string and <see cref="T:System.Globalization.CultureInfo" /> to a <see cref="T:System.Web.UI.WebControls.Unit" />.</summary>
	/// <param name="s">The string to convert. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the specified string.</returns>
	public static Unit Parse(string s, CultureInfo culture)
	{
		return new Unit(s, culture);
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> from the specified double-precision floating-point number.</summary>
	/// <param name="n">A double-precision floating-point number that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Percentage" /> that represents the length specified by the double-precision floating-point number.</returns>
	public static Unit Percentage(double n)
	{
		return new Unit(n, UnitType.Percentage);
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> from the specified 32-bit signed integer.</summary>
	/// <param name="n">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> that represents the length specified by the <paramref name="n" /> parameter.</returns>
	public static Unit Pixel(int n)
	{
		return new Unit(n);
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Point" /> from the specified 32-bit signed integer.</summary>
	/// <param name="n">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Point" /> that represents the length specified by the 32-bit signed integer.</returns>
	public static Unit Point(int n)
	{
		return new Unit(n, UnitType.Point);
	}

	/// <summary>Compares this <see cref="T:System.Web.UI.WebControls.Unit" /> with the specified object.</summary>
	/// <param name="obj">The object for comparison. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.Unit" /> that this method is called from is equal to the specified object; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (obj is Unit unit)
		{
			if (unit.type == type && unit.value == value)
			{
				return valueSet == unit.valueSet;
			}
			return false;
		}
		return false;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
	public override int GetHashCode()
	{
		return Type.GetHashCode() ^ Value.GetHashCode();
	}

	/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.Unit" /> objects to determine whether they are equal.</summary>
	/// <param name="left">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the left side of the operator. </param>
	/// <param name="right">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the right side of the operator. </param>
	/// <returns>
	///     <see langword="true" /> if both <see cref="T:System.Web.UI.WebControls.Unit" /> objects are equal; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(Unit left, Unit right)
	{
		if (left.Type == right.Type && left.Value == right.Value)
		{
			return left.valueSet == right.valueSet;
		}
		return false;
	}

	/// <summary>Compares two <see cref="T:System.Web.UI.WebControls.Unit" /> objects to determine whether they are not equal.</summary>
	/// <param name="left">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the left side of the operator. </param>
	/// <param name="right">The <see cref="T:System.Web.UI.WebControls.Unit" /> on the right side of the operator. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.Unit" /> objects are not equal; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(Unit left, Unit right)
	{
		if (left.Type == right.Type && left.Value == right.Value)
		{
			return left.valueSet != right.valueSet;
		}
		return true;
	}

	/// <summary>Implicitly creates a <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> from the specified 32-bit unsigned integer.</summary>
	/// <param name="n">A 32-bit signed integer that represents the length of the <see cref="T:System.Web.UI.WebControls.Unit" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> of type <see cref="F:System.Web.UI.WebControls.UnitType.Pixel" /> that represents the 32-bit unsigned integer specified by the <paramref name="n" /> parameter.</returns>
	public static implicit operator Unit(int n)
	{
		return new Unit(n);
	}

	internal static string GetExtension(UnitType type)
	{
		return type switch
		{
			UnitType.Pixel => "px", 
			UnitType.Point => "pt", 
			UnitType.Pica => "pc", 
			UnitType.Inch => "in", 
			UnitType.Mm => "mm", 
			UnitType.Cm => "cm", 
			UnitType.Percentage => "%", 
			UnitType.Em => "em", 
			UnitType.Ex => "ex", 
			_ => string.Empty, 
		};
	}

	/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.Unit" /> to a string equivalent in the specified culture.</summary>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents the culture. </param>
	/// <returns>A <see cref="T:System.String" /> represents this <see cref="T:System.Web.UI.WebControls.Unit" /> in the culture specified by <paramref name="culture" />.</returns>
	public string ToString(CultureInfo culture)
	{
		if (type == (UnitType)0)
		{
			return string.Empty;
		}
		string extension = GetExtension(type);
		return value.ToString(culture) + extension;
	}

	/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.Unit" /> to a <see cref="T:System.String" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents this <see cref="T:System.Web.UI.WebControls.Unit" />.</returns>
	public override string ToString()
	{
		return ToString(Helpers.InvariantCulture);
	}

	/// <summary>Converts a <see cref="T:System.Web.UI.WebControls.Unit" /> to a string equivalent using the specified format provider.</summary>
	/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
	/// <returns>A <see cref="T:System.String" /> representing this <see cref="T:System.Web.UI.WebControls.Unit" /> in the format specified by <paramref name="formatProvider" />.</returns>
	public string ToString(IFormatProvider formatProvider)
	{
		if (type == (UnitType)0)
		{
			return string.Empty;
		}
		string extension = GetExtension(type);
		return value.ToString(formatProvider) + extension;
	}
}
