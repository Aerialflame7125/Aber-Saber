using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Contains helper functions to convert property values to and from strings.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public static class PropertyConverter
{
	/// <summary>Converts the string representation to a value of the specified enumeration type. </summary>
	/// <param name="enumType">A <see cref="T:System.Type" /> that represents the enumeration type to create from the <paramref name="value" /> parameter.</param>
	/// <param name="value">The <see cref="T:System.String" /> that represents a value in the enumerator.</param>
	/// <returns>An enumeration of type <paramref name="enumType" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.- or -
	///         <paramref name="value" /> is either an empty string ("") or contains only white spaces.- or - 
	///         <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration.</exception>
	public static object EnumFromString(Type enumType, string value)
	{
		object obj = null;
		try
		{
			return Enum.Parse(enumType, value, ignoreCase: true);
		}
		catch
		{
			return null;
		}
	}

	/// <summary>Converts the value of the specified enumeration type to its equivalent string representation.</summary>
	/// <param name="enumType">A <see cref="T:System.Type" /> that represents the enumeration type of <paramref name="enumValue" />. </param>
	/// <param name="enumValue">The value to convert. </param>
	/// <returns>The string representation of <paramref name="enumValue" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" /> or <paramref name="enumValue" /> parameter is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.- or - The <paramref name="enumValue" /> parameter contains a value from an enumeration that differs in type from <paramref name="enumType" />.- or - The type of <paramref name="enumValue" /> is not an underlying type of <paramref name="enumType" />. </exception>
	public static string EnumToString(Type enumType, object enumValue)
	{
		return Enum.Format(enumType, enumValue, "G");
	}

	/// <summary>Converts the string value to the specified object type.</summary>
	/// <param name="objType">The <see cref="T:System.Type" /> to create from <paramref name="value" />.</param>
	/// <param name="propertyInfo">The properties to use during conversion.</param>
	/// <param name="value">The <see cref="T:System.String" /> to convert into an object.</param>
	/// <returns>An object of type <paramref name="objType" />.</returns>
	/// <exception cref="T:System.Web.HttpException">An object of the type specified by <paramref name="objType" /> cannot be created from the <paramref name="value" /> parameter.</exception>
	public static object ObjectFromString(Type objType, MemberInfo propertyInfo, string value)
	{
		if (objType == typeof(string))
		{
			return value;
		}
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(propertyInfo.ReflectedType).Find(propertyInfo.Name, ignoreCase: false);
		if (propertyDescriptor.Converter == null || !propertyDescriptor.Converter.CanConvertFrom(typeof(string)))
		{
			throw new HttpException(Locale.GetText("Cannot create an object of type '{0}' from its string representation '{1}' for the '{2}' property", objType, value, propertyInfo.Name));
		}
		return propertyDescriptor.Converter.ConvertFromInvariantString(value);
	}
}
