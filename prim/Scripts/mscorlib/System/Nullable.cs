using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System;

/// <summary>Supports a value type that can be assigned <see langword="null" />. This class cannot be inherited.</summary>
[ComVisible(true)]
public static class Nullable
{
	/// <summary>Compares the relative values of two <see cref="T:System.Nullable`1" /> objects.</summary>
	/// <param name="n1">A <see cref="T:System.Nullable`1" /> object.</param>
	/// <param name="n2">A <see cref="T:System.Nullable`1" /> object.</param>
	/// <typeparam name="T">The underlying value type of the <paramref name="n1" /> and <paramref name="n2" /> parameters.</typeparam>
	/// <returns>An integer that indicates the relative values of the <paramref name="n1" /> and <paramref name="n2" /> parameters.  
	///   Return Value  
	///
	///   Description  
	///
	///   Less than zero  
	///
	///   The <see cref="P:System.Nullable`1.HasValue" /> property for <paramref name="n1" /> is <see langword="false" />, and the <see cref="P:System.Nullable`1.HasValue" /> property for <paramref name="n2" /> is <see langword="true" />.  
	///
	///  -or-  
	///
	///  The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="true" />, and the value of the <see cref="P:System.Nullable`1.Value" /> property for <paramref name="n1" /> is less than the value of the <see cref="P:System.Nullable`1.Value" /> property for <paramref name="n2" />.  
	///
	///   Zero  
	///
	///   The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="false" />.  
	///
	///  -or-  
	///
	///  The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="true" />, and the value of the <see cref="P:System.Nullable`1.Value" /> property for <paramref name="n1" /> is equal to the value of the <see cref="P:System.Nullable`1.Value" /> property for <paramref name="n2" />.  
	///
	///   Greater than zero  
	///
	///   The <see cref="P:System.Nullable`1.HasValue" /> property for <paramref name="n1" /> is <see langword="true" />, and the <see cref="P:System.Nullable`1.HasValue" /> property for <paramref name="n2" /> is <see langword="false" />.  
	///
	///  -or-  
	///
	///  The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="true" />, and the value of the <see cref="P:System.Nullable`1.Value" /> property for <paramref name="n1" /> is greater than the value of the <see cref="P:System.Nullable`1.Value" /> property for <paramref name="n2" />.</returns>
	public static int Compare<T>(T? n1, T? n2) where T : struct
	{
		if (n1.has_value)
		{
			if (!n2.has_value)
			{
				return 1;
			}
			return Comparer<T>.Default.Compare(n1.value, n2.value);
		}
		if (!n2.has_value)
		{
			return 0;
		}
		return -1;
	}

	/// <summary>Indicates whether two specified <see cref="T:System.Nullable`1" /> objects are equal.</summary>
	/// <param name="n1">A <see cref="T:System.Nullable`1" /> object.</param>
	/// <param name="n2">A <see cref="T:System.Nullable`1" /> object.</param>
	/// <typeparam name="T">The underlying value type of the <paramref name="n1" /> and <paramref name="n2" /> parameters.</typeparam>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="n1" /> parameter is equal to the <paramref name="n2" /> parameter; otherwise, <see langword="false" />.  
	/// The return value depends on the <see cref="P:System.Nullable`1.HasValue" /> and <see cref="P:System.Nullable`1.Value" /> properties of the two parameters that are compared.  
	///  Return Value  
	///
	///  Description  
	///
	/// <see langword="true" /> The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="false" />.  
	///
	/// -or-  
	///
	/// The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="true" />, and the <see cref="P:System.Nullable`1.Value" /> properties of the parameters are equal.  
	///
	/// <see langword="false" /> The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" /> for one parameter and <see langword="false" /> for the other parameter.  
	///
	/// -or-  
	///
	/// The <see cref="P:System.Nullable`1.HasValue" /> properties for <paramref name="n1" /> and <paramref name="n2" /> are <see langword="true" />, and the <see cref="P:System.Nullable`1.Value" /> properties of the parameters are unequal.</returns>
	public static bool Equals<T>(T? n1, T? n2) where T : struct
	{
		if (n1.has_value != n2.has_value)
		{
			return false;
		}
		if (!n1.has_value)
		{
			return true;
		}
		return EqualityComparer<T>.Default.Equals(n1.value, n2.value);
	}

	/// <summary>Returns the underlying type argument of the specified nullable type.</summary>
	/// <param name="nullableType">A <see cref="T:System.Type" /> object that describes a closed generic nullable type.</param>
	/// <returns>The type argument of the <paramref name="nullableType" /> parameter, if the <paramref name="nullableType" /> parameter is a closed generic nullable type; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="nullableType" /> is <see langword="null" />.</exception>
	public static Type GetUnderlyingType(Type nullableType)
	{
		if (nullableType == null)
		{
			throw new ArgumentNullException("nullableType");
		}
		if (!nullableType.IsGenericType || nullableType.IsGenericTypeDefinition || !(nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)))
		{
			return null;
		}
		return nullableType.GetGenericArguments()[0];
	}
}
/// <summary>Represents a value type that can be assigned <see langword="null" />.</summary>
/// <typeparam name="T">The underlying value type of the <see cref="T:System.Nullable`1" /> generic type.</typeparam>
[Serializable]
[DebuggerStepThrough]
public struct Nullable<T> where T : struct
{
	internal T value;

	internal bool has_value;

	/// <summary>Gets a value indicating whether the current <see cref="T:System.Nullable`1" /> object has a valid value of its underlying type.</summary>
	/// <returns>
	///   <see langword="true" /> if the current <see cref="T:System.Nullable`1" /> object has a value; <see langword="false" /> if the current <see cref="T:System.Nullable`1" /> object has no value.</returns>
	public bool HasValue => has_value;

	/// <summary>Gets the value of the current <see cref="T:System.Nullable`1" /> object if it has been assigned a valid underlying value.</summary>
	/// <returns>The value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />. An exception is thrown if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</exception>
	public T Value
	{
		get
		{
			if (!has_value)
			{
				throw new InvalidOperationException("Nullable object must have a value.");
			}
			return value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Nullable`1" /> structure to the specified value.</summary>
	/// <param name="value">A value type.</param>
	public Nullable(T value)
	{
		has_value = true;
		this.value = value;
	}

	/// <summary>Indicates whether the current <see cref="T:System.Nullable`1" /> object is equal to a specified object.</summary>
	/// <param name="other">An object.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="other" /> parameter is equal to the current <see cref="T:System.Nullable`1" /> object; otherwise, <see langword="false" />.  
	/// This table describes how equality is defined for the compared values:  
	///  Return Value  
	///
	///  Description  
	///
	/// <see langword="true" /> The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />, and the <paramref name="other" /> parameter is <see langword="null" />. That is, two null values are equal by definition.  
	///
	/// -or-  
	///
	/// The <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is equal to the <paramref name="other" /> parameter.  
	///
	/// <see langword="false" /> The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is <see langword="true" />, and the <paramref name="other" /> parameter is <see langword="null" />.  
	///
	/// -or-  
	///
	/// The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is <see langword="false" />, and the <paramref name="other" /> parameter is not <see langword="null" />.  
	///
	/// -or-  
	///
	/// The <see cref="P:System.Nullable`1.HasValue" /> property for the current <see cref="T:System.Nullable`1" /> structure is <see langword="true" />, and the value returned by the <see cref="P:System.Nullable`1.Value" /> property is not equal to the <paramref name="other" /> parameter.</returns>
	public override bool Equals(object other)
	{
		if (other == null)
		{
			return !has_value;
		}
		if (!(other is T?))
		{
			return false;
		}
		return Equals((T?)other);
	}

	private bool Equals(T? other)
	{
		if (other.has_value != has_value)
		{
			return false;
		}
		if (!has_value)
		{
			return true;
		}
		return other.value.Equals(value);
	}

	/// <summary>Retrieves the hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property.</summary>
	/// <returns>The hash code of the object returned by the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />, or zero if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</returns>
	public override int GetHashCode()
	{
		if (!has_value)
		{
			return 0;
		}
		return value.GetHashCode();
	}

	/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the default value of the underlying type.</summary>
	/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the  <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />; otherwise, the default value of the underlying type.</returns>
	public T GetValueOrDefault()
	{
		return value;
	}

	/// <summary>Retrieves the value of the current <see cref="T:System.Nullable`1" /> object, or the specified default value.</summary>
	/// <param name="defaultValue">A value to return if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</param>
	/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />; otherwise, the <paramref name="defaultValue" /> parameter.</returns>
	public T GetValueOrDefault(T defaultValue)
	{
		if (!has_value)
		{
			return defaultValue;
		}
		return value;
	}

	/// <summary>Returns the text representation of the value of the current <see cref="T:System.Nullable`1" /> object.</summary>
	/// <returns>The text representation of the value of the current <see cref="T:System.Nullable`1" /> object if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="true" />, or an empty string ("") if the <see cref="P:System.Nullable`1.HasValue" /> property is <see langword="false" />.</returns>
	public override string ToString()
	{
		if (has_value)
		{
			return value.ToString();
		}
		return string.Empty;
	}

	/// <summary>Creates a new <see cref="T:System.Nullable`1" /> object initialized to a specified value.</summary>
	/// <param name="value">A value type.</param>
	/// <returns>A <see cref="T:System.Nullable`1" /> object whose <see cref="P:System.Nullable`1.Value" /> property is initialized with the <paramref name="value" /> parameter.</returns>
	public static implicit operator T?(T value)
	{
		return value;
	}

	/// <summary>Defines an explicit conversion of a <see cref="T:System.Nullable`1" /> instance to its underlying value.</summary>
	/// <param name="value">A nullable value.</param>
	/// <returns>The value of the <see cref="P:System.Nullable`1.Value" /> property for the <paramref name="value" /> parameter.</returns>
	public static explicit operator T(T? value)
	{
		return value.Value;
	}

	private static object Box(T? o)
	{
		if (!o.has_value)
		{
			return null;
		}
		return o.value;
	}

	private static T? Unbox(object o)
	{
		if (o == null)
		{
			return null;
		}
		return (T)o;
	}
}
