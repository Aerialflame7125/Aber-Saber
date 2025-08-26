using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides support for rapid application development (RAD) designers to generate and parse data-binding expression syntax. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DataBinder
{
	[ThreadStatic]
	private static Dictionary<Type, PropertyInfo> dataItemCache;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBinder" /> class.</summary>
	public DataBinder()
	{
	}

	internal static string FormatResult(object result, string format)
	{
		if (result == null)
		{
			return string.Empty;
		}
		if (format == null || format.Length == 0)
		{
			return result.ToString();
		}
		return string.Format(format, result);
	}

	/// <summary>Evaluates data-binding expressions at run time.</summary>
	/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language. </param>
	/// <param name="expression">The navigation path from the <paramref name="container" /> object to the public property value to be placed in the bound control property. This must be a string of property or field names separated by periods, such as Tables[0].DefaultView.[0].Price in C# or Tables(0).DefaultView.(0).Price in Visual Basic. </param>
	/// <returns>An <see cref="T:System.Object" /> instance that results from the evaluation of the data-binding expression.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="expression" /> is <see langword="null" /> or is an empty string after trimming.</exception>
	public static object Eval(object container, string expression)
	{
		expression = expression?.Trim();
		if (expression == null || expression.Length == 0)
		{
			throw new ArgumentNullException("expression");
		}
		object obj = container;
		while (obj != null)
		{
			int num = expression.IndexOf('.');
			int length = ((num == -1) ? expression.Length : num);
			string text = expression.Substring(0, length);
			obj = ((text.IndexOf('[') == -1) ? GetPropertyValue(obj, text) : GetIndexedPropertyValue(obj, text));
			if (num == -1)
			{
				break;
			}
			expression = expression.Substring(text.Length + 1);
		}
		return obj;
	}

	/// <summary>Evaluates data-binding expressions at run time and formats the result as a string.</summary>
	/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language. </param>
	/// <param name="expression">The navigation path from the <paramref name="container" /> object to the public property value to be placed in the bound control property. This must be a string of property or field names separated by periods, such as Tables[0].DefaultView.[0].Price in C# or Tables(0).DefaultView.(0).Price in Visual Basic. </param>
	/// <param name="format">A .NET Framework format string (like those used by <see cref="M:System.String.Format(System.String,System.Object)" />) that converts the <see cref="T:System.Object" /> instance returned by the data-binding expression to a <see cref="T:System.String" /> object. </param>
	/// <returns>A <see cref="T:System.String" /> object that results from evaluating the data-binding expression and converting it to a string type.</returns>
	public static string Eval(object container, string expression, string format)
	{
		return FormatResult(Eval(container, expression), format);
	}

	/// <summary>Retrieves the value of a property of the specified container and navigation path.</summary>
	/// <param name="container">The object reference against which <paramref name="expr" /> is evaluated. This must be a valid object identifier in the specified language for the page.</param>
	/// <param name="expr">The navigation path from the <paramref name="container" /> object to the public property value to place in the bound control property. This must be a string of property or field names separated by periods, such as Tables[0].DefaultView.[0].Price in C# or Tables(0).DefaultView.(0).Price in Visual Basic.</param>
	/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="container" /> is <see langword="null" />.- or -
	///         <paramref name="expr" /> is <see langword="null" /> or an empty string ("").</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="expr" /> is not a valid indexed expression.- or -
	///         <paramref name="expr" /> does not allow indexed access.</exception>
	public static object GetIndexedPropertyValue(object container, string expr)
	{
		if (container == null)
		{
			throw new ArgumentNullException("container");
		}
		if (expr == null || expr.Length == 0)
		{
			throw new ArgumentNullException("expr");
		}
		int num = expr.IndexOf('[');
		int num2 = expr.IndexOf(']');
		if (num < 0 || num2 < 0 || num2 - num <= 1)
		{
			throw new ArgumentException(expr + " is not a valid indexed expression.");
		}
		string text = expr.Substring(num + 1, num2 - num - 1);
		text = text.Trim();
		if (text.Length == 0)
		{
			throw new ArgumentException(expr + " is not a valid indexed expression.");
		}
		bool flag = false;
		if ((text[0] == '\'' && text[text.Length - 1] == '\'') || (text[0] == '"' && text[text.Length - 1] == '"'))
		{
			flag = true;
			text = text.Substring(1, text.Length - 2);
		}
		else
		{
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsDigit(text[i]))
				{
					flag = true;
					break;
				}
			}
		}
		int num3 = 0;
		if (!flag)
		{
			try
			{
				num3 = int.Parse(text);
			}
			catch
			{
				throw new ArgumentException(expr + " is not a valid indexed expression.");
			}
		}
		string text2 = null;
		if (num > 0)
		{
			text2 = expr.Substring(0, num);
			if (text2 != null && text2.Length > 0)
			{
				container = GetPropertyValue(container, text2);
			}
		}
		if (container == null)
		{
			return null;
		}
		if (container is IList)
		{
			if (flag)
			{
				throw new ArgumentException(expr + " cannot be indexed with a string.");
			}
			return ((IList)container)[num3];
		}
		Type type = container.GetType();
		object[] customAttributes = type.GetCustomAttributes(typeof(DefaultMemberAttribute), inherit: false);
		text2 = ((customAttributes.Length == 1) ? ((DefaultMemberAttribute)customAttributes[0]).MemberName : "Item");
		Type[] types = new Type[1] { flag ? typeof(string) : typeof(int) };
		PropertyInfo property = type.GetProperty(text2, types);
		if (property == null)
		{
			throw new ArgumentException(expr + " indexer not found.");
		}
		object[] array = new object[1];
		if (flag)
		{
			array[0] = text;
		}
		else
		{
			array[0] = num3;
		}
		return property.GetValue(container, array);
	}

	/// <summary>Retrieves the value of the specified property for the specified container, and then formats the results.</summary>
	/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the specified language for the page.</param>
	/// <param name="propName">The name of the property that contains the value to retrieve.</param>
	/// <param name="format">A string that specifies the format in which to display the results.</param>
	/// <returns>The value of the specified property in the format specified by <paramref name="format" />.</returns>
	public static string GetIndexedPropertyValue(object container, string propName, string format)
	{
		return FormatResult(GetIndexedPropertyValue(container, propName), format);
	}

	/// <summary>Retrieves the value of the specified property of the specified object.</summary>
	/// <param name="container">The object that contains the property. </param>
	/// <param name="propName">The name of the property that contains the value to retrieve. </param>
	/// <returns>The value of the specified property.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="container" /> is <see langword="null" />.-or- 
	///         <paramref name="propName" /> is <see langword="null" /> or an empty string (""). </exception>
	/// <exception cref="T:System.Web.HttpException">The object in <paramref name="container" /> does not have the property specified by <paramref name="propName" />. </exception>
	public static object GetPropertyValue(object container, string propName)
	{
		if (container == null)
		{
			throw new ArgumentNullException("container");
		}
		if (propName == null || propName.Length == 0)
		{
			throw new ArgumentNullException("propName");
		}
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(container).Find(propName, ignoreCase: true);
		if (propertyDescriptor == null)
		{
			throw new HttpException("Property " + propName + " not found in " + container.GetType());
		}
		return propertyDescriptor.GetValue(container);
	}

	/// <summary>Retrieves the value of the specified property of the specified object, and then formats the results.</summary>
	/// <param name="container">The object that contains the property. </param>
	/// <param name="propName">The name of the property that contains the value to retrieve. </param>
	/// <param name="format">A string that specifies the format in which to display the results. </param>
	/// <returns>The value of the specified property in the format specified by <paramref name="format" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="container" /> is <see langword="null" />.- or - 
	///         <paramref name="propName" /> is <see langword="null" /> or an empty string (""). </exception>
	/// <exception cref="T:System.Web.HttpException">The object in <paramref name="container" /> does not have the property specified by <paramref name="propName" />. </exception>
	public static string GetPropertyValue(object container, string propName, string format)
	{
		return FormatResult(GetPropertyValue(container, propName), format);
	}

	/// <summary>Retrieves an object's declared data item, indicating success or failure.</summary>
	/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language.</param>
	/// <param name="foundDataItem">A Boolean value that indicates whether the data item was successfully resolved and returned. This parameter is passed uninitialized.</param>
	/// <returns>An object that represents the container's declared data item. Returns <see langword="null" /> if no data item is found or if the container evaluates to <see langword="null" />.</returns>
	public static object GetDataItem(object container, out bool foundDataItem)
	{
		foundDataItem = false;
		if (container == null)
		{
			return null;
		}
		if (container is IDataItemContainer)
		{
			foundDataItem = true;
			return ((IDataItemContainer)container).DataItem;
		}
		PropertyInfo value = null;
		if (dataItemCache == null)
		{
			dataItemCache = new Dictionary<Type, PropertyInfo>();
		}
		Type type = container.GetType();
		if (!dataItemCache.TryGetValue(type, out value))
		{
			value = type.GetProperty("DataItem", BindingFlags.Instance | BindingFlags.Public);
			dataItemCache[type] = value;
		}
		if (value == null)
		{
			return null;
		}
		foundDataItem = true;
		return value.GetValue(container, null);
	}

	/// <summary>Retrieves an object's declared data item.</summary>
	/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language.</param>
	/// <returns>An object that represents the container's declared data item. Returns <see langword="null" /> if no data item is found or if the container evaluates to <see langword="null" />.</returns>
	public static object GetDataItem(object container)
	{
		bool foundDataItem;
		return GetDataItem(container, out foundDataItem);
	}
}
