using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModestTree;

public static class TypeStringFormatter
{
	private static readonly Dictionary<Type, string> _prettyNameCache = new Dictionary<Type, string>();

	public static string PrettyName(this Type type)
	{
		if (!_prettyNameCache.TryGetValue(type, out var value))
		{
			value = PrettyNameInternal(type);
			_prettyNameCache.Add(type, value);
		}
		return value;
	}

	private static string PrettyNameInternal(Type type)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (type.IsNested)
		{
			stringBuilder.Append(PrettyName(type.DeclaringType));
			stringBuilder.Append(".");
		}
		if (type.IsArray)
		{
			stringBuilder.Append(PrettyName(type.GetElementType()));
			stringBuilder.Append("[]");
		}
		else
		{
			string cSharpTypeName = GetCSharpTypeName(type.Name);
			if (TypeExtensions.IsGenericType(type))
			{
				int num = cSharpTypeName.IndexOf('`');
				if (num != -1)
				{
					stringBuilder.Append(cSharpTypeName.Substring(0, cSharpTypeName.IndexOf('`')));
				}
				else
				{
					stringBuilder.Append(cSharpTypeName);
				}
				stringBuilder.Append("<");
				if (TypeExtensions.IsGenericTypeDefinition(type))
				{
					int num2 = TypeExtensions.GenericArguments(type).Count();
					if (num2 > 0)
					{
						stringBuilder.Append(new string(',', num2 - 1));
					}
				}
				else
				{
					stringBuilder.Append(string.Join(", ", TypeExtensions.GenericArguments(type).Select(PrettyName).ToArray()));
				}
				stringBuilder.Append(">");
			}
			else
			{
				stringBuilder.Append(cSharpTypeName);
			}
		}
		return stringBuilder.ToString();
	}

	private static string GetCSharpTypeName(string typeName)
	{
		switch (typeName)
		{
		case "String":
		case "Object":
		case "Void":
		case "Byte":
		case "Double":
		case "Decimal":
			return typeName.ToLower();
		case "Int16":
			return "short";
		case "Int32":
			return "int";
		case "Int64":
			return "long";
		case "Single":
			return "float";
		case "Boolean":
			return "bool";
		default:
			return typeName;
		}
	}
}
