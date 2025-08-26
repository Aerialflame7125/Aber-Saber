using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.ModelBinding;

/// <summary>Represents the result of retrieving a value from a value provider. </summary>
[Serializable]
public class ValueProviderResult
{
	private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

	private CultureInfo _instanceCulture;

	/// <summary>Gets or sets the raw value that is converted to a string for display.</summary>
	/// <returns>A string representation of the raw value.</returns>
	public string AttemptedValue { get; protected set; }

	/// <summary>Gets or sets the culture.</summary>
	/// <returns>The culture.</returns>
	public CultureInfo Culture
	{
		get
		{
			if (_instanceCulture == null)
			{
				_instanceCulture = _staticCulture;
			}
			return _instanceCulture;
		}
		protected set
		{
			_instanceCulture = value;
		}
	}

	/// <summary>Gets or sets the raw value that is supplied by the value provider.</summary>
	/// <returns>The raw value.</returns>
	public object RawValue { get; protected set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class.</summary>
	protected ValueProviderResult()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ValueProviderResult" /> class by using the specified raw value, attempted value, and culture information.</summary>
	/// <param name="rawValue">The raw value.</param>
	/// <param name="attemptedValue">The attempted value.</param>
	/// <param name="culture">The culture information.</param>
	public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
	{
		RawValue = rawValue;
		AttemptedValue = attemptedValue;
		Culture = culture;
	}

	private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
	{
		if (value == null || destinationType.IsInstanceOfType(value))
		{
			return value;
		}
		if (value is string text && text.Trim().Length == 0)
		{
			return null;
		}
		TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
		bool flag = converter.CanConvertFrom(value.GetType());
		if (!flag)
		{
			converter = TypeDescriptor.GetConverter(value.GetType());
		}
		if (!flag && !converter.CanConvertTo(destinationType))
		{
			if (destinationType.IsEnum && value is int)
			{
				return Enum.ToObject(destinationType, (int)value);
			}
			Type underlyingType = Nullable.GetUnderlyingType(destinationType);
			if (underlyingType != null)
			{
				return ConvertSimpleType(culture, value, underlyingType);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The parameter conversion from type '{0}' to type '{1}' failed because no type converter can convert between these types."), value.GetType().FullName, destinationType.FullName));
		}
		try
		{
			return flag ? converter.ConvertFrom(null, culture, value) : converter.ConvertTo(null, culture, value, destinationType);
		}
		catch (Exception innerException)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The parameter conversion from type '{0}' to type '{1}' failed. See the inner exception for more information."), value.GetType().FullName, destinationType.FullName), innerException);
		}
	}

	/// <summary>Converts a value that is encapsulated by this result to the specified type.</summary>
	/// <param name="type">The type.</param>
	/// <returns>The converted value.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">Conversion was unsuccessful.</exception>
	public object ConvertTo(Type type)
	{
		return ConvertTo(type, null);
	}

	/// <summary>Converts the value that is encapsulated by this result to the specified type by using the specified culture information.</summary>
	/// <param name="type">The type.</param>
	/// <param name="culture">The culture information.</param>
	/// <returns>The converted value.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">Conversion was unsuccessful.</exception>
	public virtual object ConvertTo(Type type, CultureInfo culture)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		return UnwrapPossibleArrayType(culture ?? Culture, RawValue, type);
	}

	private static object UnwrapPossibleArrayType(CultureInfo culture, object value, Type destinationType)
	{
		if (value == null || destinationType.IsInstanceOfType(value))
		{
			return value;
		}
		Array array = value as Array;
		if (destinationType.IsArray)
		{
			Type elementType = destinationType.GetElementType();
			if (array != null)
			{
				IList list = Array.CreateInstance(elementType, array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					list[i] = ConvertSimpleType(culture, array.GetValue(i), elementType);
				}
				return list;
			}
			object value2 = ConvertSimpleType(culture, value, elementType);
			Array array2 = Array.CreateInstance(elementType, 1);
			((IList)array2)[0] = value2;
			return array2;
		}
		if (array != null)
		{
			if (array.Length > 0)
			{
				value = array.GetValue(0);
				return ConvertSimpleType(culture, value, destinationType);
			}
			return null;
		}
		return ConvertSimpleType(culture, value, destinationType);
	}
}
