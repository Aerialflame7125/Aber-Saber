using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Drawing;

/// <summary>Converts colors from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
public class ColorConverter : TypeConverter
{
	private sealed class CompareColors : IComparer
	{
		public int Compare(object x, object y)
		{
			return string.Compare(((Color)x).Name, ((Color)y).Name);
		}
	}

	private static StandardValuesCollection cached;

	private static object creatingCached = new object();

	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.ColorConverter" /> class.</summary>
	public ColorConverter()
	{
	}

	/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked.</param>
	/// <param name="sourceType">The type from which you want to convert.</param>
	/// <returns>
	///   <see langword="true" /> if this object can perform the conversion; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type to which you want to convert.</param>
	/// <returns>
	///   <see langword="true" /> if this converter can perform the operation; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	internal static Color StaticConvertFromString(ITypeDescriptorContext context, string s, CultureInfo culture)
	{
		if (culture == null)
		{
			culture = CultureInfo.InvariantCulture;
		}
		s = s.Trim();
		if (s.Length == 0)
		{
			return Color.Empty;
		}
		if (char.IsLetter(s[0]))
		{
			KnownColor kc;
			try
			{
				kc = (KnownColor)Enum.Parse(typeof(KnownColor), s, ignoreCase: true);
			}
			catch (Exception innerException)
			{
				string text = global::Locale.GetText("Invalid color name '{0}'.", s);
				throw new Exception(text, new FormatException(text, innerException));
			}
			return KnownColors.FromKnownColor(kc);
		}
		string listSeparator = culture.TextInfo.ListSeparator;
		Color color = Color.Empty;
		if (s.IndexOf(listSeparator) == -1)
		{
			bool flag = s[0] == '#';
			int num = (flag ? 1 : 0);
			bool flag2 = false;
			if (s.Length > num + 1 && s[num] == '0')
			{
				flag2 = s[num + 1] == 'x' || s[num + 1] == 'X';
				if (flag2)
				{
					num += 2;
				}
			}
			if (flag || flag2)
			{
				s = s.Substring(num);
				int num2;
				try
				{
					num2 = int.Parse(s, NumberStyles.HexNumber);
				}
				catch (Exception innerException2)
				{
					throw new Exception(global::Locale.GetText("Invalid Int32 value '{0}'.", s), innerException2);
				}
				if (s.Length < 6 || (s.Length == 6 && flag && flag2))
				{
					num2 &= 0xFFFFFF;
				}
				else if (num2 >> 24 == 0)
				{
					num2 |= -16777216;
				}
				color = Color.FromArgb(num2);
			}
		}
		if (color.IsEmpty)
		{
			Int32Converter int32Converter = new Int32Converter();
			string[] array = s.Split(listSeparator.ToCharArray());
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (int)int32Converter.ConvertFrom(context, culture, array[i]);
			}
			color = array.Length switch
			{
				1 => Color.FromArgb(array2[0]), 
				3 => Color.FromArgb(array2[0], array2[1], array2[2]), 
				4 => Color.FromArgb(array2[0], array2[1], array2[2], array2[3]), 
				_ => throw new ArgumentException(s + " is not a valid color value."), 
			};
		}
		if (!color.IsEmpty)
		{
			Color result = KnownColors.FindColorMatch(color);
			if (!result.IsEmpty)
			{
				return result;
			}
		}
		return color;
	}

	/// <summary>Converts the given object to the converter's native type.</summary>
	/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color.</param>
	/// <param name="value">The object to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
	/// <exception cref="T:System.ArgumentException">The conversion cannot be performed.</exception>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (!(value is string s))
		{
			return base.ConvertFrom(context, culture, value);
		}
		return StaticConvertFromString(context, s, culture);
	}

	/// <summary>Converts the specified object to another type.</summary>
	/// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is <see langword="null" />. Also, properties on the context object may return <see langword="null" />.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color.</param>
	/// <param name="value">The object to convert.</param>
	/// <param name="destinationType">The type to convert the object to.</param>
	/// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="destinationtype" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value is Color color)
		{
			if (destinationType == typeof(string))
			{
				if (color == Color.Empty)
				{
					return string.Empty;
				}
				if (color.IsKnownColor || color.IsNamedColor)
				{
					return color.Name;
				}
				string listSeparator = culture.TextInfo.ListSeparator;
				StringBuilder stringBuilder = new StringBuilder();
				if (color.A != byte.MaxValue)
				{
					stringBuilder.Append(color.A);
					stringBuilder.Append(listSeparator);
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(color.R);
				stringBuilder.Append(listSeparator);
				stringBuilder.Append(" ");
				stringBuilder.Append(color.G);
				stringBuilder.Append(listSeparator);
				stringBuilder.Append(" ");
				stringBuilder.Append(color.B);
				return stringBuilder.ToString();
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				if (color.IsEmpty)
				{
					return new InstanceDescriptor(typeof(Color).GetTypeInfo().GetField("Empty"), null);
				}
				if (color.IsSystemColor)
				{
					return new InstanceDescriptor(typeof(SystemColors).GetTypeInfo().GetProperty(color.Name), null);
				}
				if (color.IsKnownColor)
				{
					return new InstanceDescriptor(typeof(Color).GetTypeInfo().GetProperty(color.Name), null);
				}
				return new InstanceDescriptor(typeof(Color).GetTypeInfo().GetMethod("FromArgb", new Type[4]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(int)
				}), new object[4] { color.A, color.R, color.G, color.B });
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Retrieves a collection containing a set of standard values for the data type for which this validator is designed. This will return <see langword="null" /> if the data type does not support a standard set of values.</summary>
	/// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is <see langword="null" />. Also, properties on the context object may return <see langword="null" />.</param>
	/// <returns>A collection containing <see langword="null" /> or a standard set of valid values. The default implementation always returns <see langword="null" />.</returns>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		lock (creatingCached)
		{
			if (cached != null)
			{
				return cached;
			}
			Array array = Array.CreateInstance(typeof(Color), KnownColors.ArgbValues.Length - 1);
			for (int i = 1; i < KnownColors.ArgbValues.Length; i++)
			{
				array.SetValue(KnownColors.FromKnownColor((KnownColor)i), i - 1);
			}
			Array.Sort(array, 0, array.Length, new CompareColors());
			cached = new StandardValuesCollection(array);
		}
		return cached;
	}

	/// <summary>Determines if this object supports a standard set of values that can be chosen from a list.</summary>
	/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
	/// <returns>
	///   <see langword="true" /> if <see cref="Overload:System.Drawing.ColorConverter.GetStandardValues" /> must be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
