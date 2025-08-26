using System.Globalization;
using System.Threading;

namespace System.Web.Services.Protocols;

internal class ScalarFormatter
{
	private ScalarFormatter()
	{
	}

	internal static string ToString(object value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		if (value is string)
		{
			return (string)value;
		}
		if (value.GetType().IsEnum)
		{
			return EnumToString(value);
		}
		return Convert.ToString(value, CultureInfo.InvariantCulture);
	}

	internal static object FromString(string value, Type type)
	{
		try
		{
			if (type == typeof(string))
			{
				return value;
			}
			if (type.IsEnum)
			{
				return EnumFromString(value, type);
			}
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			throw new ArgumentException(Res.GetString("WebChangeTypeFailed", value, type.FullName), "type", ex);
		}
	}

	private static object EnumFromString(string value, Type type)
	{
		return Enum.Parse(type, value);
	}

	private static string EnumToString(object value)
	{
		return Enum.Format(value.GetType(), value, "G");
	}

	internal static bool IsTypeSupported(Type type)
	{
		if (type.IsEnum)
		{
			return true;
		}
		if (!(type == typeof(int)) && !(type == typeof(string)) && !(type == typeof(long)) && !(type == typeof(byte)) && !(type == typeof(sbyte)) && !(type == typeof(short)) && !(type == typeof(bool)) && !(type == typeof(char)) && !(type == typeof(float)) && !(type == typeof(decimal)) && !(type == typeof(DateTime)) && !(type == typeof(ushort)) && !(type == typeof(uint)) && !(type == typeof(ulong)))
		{
			return type == typeof(double);
		}
		return true;
	}
}
