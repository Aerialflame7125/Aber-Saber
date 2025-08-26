using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModestTree;

public static class TypeExtensions
{
	private static readonly Dictionary<Type, bool> _isClosedGenericType = new Dictionary<Type, bool>();

	private static readonly Dictionary<Type, bool> _isOpenGenericType = new Dictionary<Type, bool>();

	private static readonly Dictionary<Type, bool> _isValueType = new Dictionary<Type, bool>();

	private static readonly Dictionary<Type, Type[]> _interfaces = new Dictionary<Type, Type[]>();

	public static bool DerivesFrom<T>(this Type a)
	{
		return DerivesFrom(a, typeof(T));
	}

	public static bool DerivesFrom(this Type a, Type b)
	{
		return b != a && DerivesFromOrEqual(a, b);
	}

	public static bool DerivesFromOrEqual<T>(this Type a)
	{
		return DerivesFromOrEqual(a, typeof(T));
	}

	public static bool DerivesFromOrEqual(this Type a, Type b)
	{
		return b == a || b.IsAssignableFrom(a);
	}

	public static bool IsAssignableToGenericType(Type givenType, Type genericType)
	{
		Type[] array = Interfaces(givenType);
		Type[] array2 = array;
		foreach (Type type in array2)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
			{
				return true;
			}
		}
		if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
		{
			return true;
		}
		Type baseType = givenType.BaseType;
		if (baseType == null)
		{
			return false;
		}
		return IsAssignableToGenericType(baseType, genericType);
	}

	public static bool IsEnum(this Type type)
	{
		return type.IsEnum;
	}

	public static bool IsValueType(this Type type)
	{
		if (!_isValueType.TryGetValue(type, out var value))
		{
			value = type.IsValueType;
			_isValueType[type] = value;
		}
		return value;
	}

	public static MethodInfo[] DeclaredInstanceMethods(this Type type)
	{
		return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	}

	public static PropertyInfo[] DeclaredInstanceProperties(this Type type)
	{
		return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	}

	public static FieldInfo[] DeclaredInstanceFields(this Type type)
	{
		return type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	}

	public static Type BaseType(this Type type)
	{
		return type.BaseType;
	}

	public static bool IsGenericType(this Type type)
	{
		return type.IsGenericType;
	}

	public static bool IsGenericTypeDefinition(this Type type)
	{
		return type.IsGenericTypeDefinition;
	}

	public static bool IsPrimitive(this Type type)
	{
		return type.IsPrimitive;
	}

	public static bool IsInterface(this Type type)
	{
		return type.IsInterface;
	}

	public static bool ContainsGenericParameters(this Type type)
	{
		return type.ContainsGenericParameters;
	}

	public static bool IsAbstract(this Type type)
	{
		return type.IsAbstract;
	}

	public static bool IsSealed(this Type type)
	{
		return type.IsSealed;
	}

	public static MethodInfo Method(this Delegate del)
	{
		return del.Method;
	}

	public static Type[] GenericArguments(this Type type)
	{
		return type.GetGenericArguments();
	}

	public static Type[] Interfaces(this Type type)
	{
		if (!_interfaces.TryGetValue(type, out var value))
		{
			value = type.GetInterfaces();
			_interfaces.Add(type, value);
		}
		return value;
	}

	public static ConstructorInfo[] Constructors(this Type type)
	{
		return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	}

	public static object GetDefaultValue(this Type type)
	{
		if (IsValueType(type))
		{
			return Activator.CreateInstance(type);
		}
		return null;
	}

	public static bool IsClosedGenericType(this Type type)
	{
		if (!_isClosedGenericType.TryGetValue(type, out var value))
		{
			value = IsGenericType(type) && type != type.GetGenericTypeDefinition();
			_isClosedGenericType[type] = value;
		}
		return value;
	}

	public static IEnumerable<Type> GetParentTypes(this Type type)
	{
		if (type == null || BaseType(type) == null || type == typeof(object) || BaseType(type) == typeof(object))
		{
			yield break;
		}
		yield return BaseType(type);
		foreach (Type parentType in GetParentTypes(BaseType(type)))
		{
			yield return parentType;
		}
	}

	public static bool IsOpenGenericType(this Type type)
	{
		if (!_isOpenGenericType.TryGetValue(type, out var value))
		{
			value = IsGenericType(type) && type == type.GetGenericTypeDefinition();
			_isOpenGenericType[type] = value;
		}
		return value;
	}

	public static T GetAttribute<T>(this MemberInfo provider) where T : Attribute
	{
		return AllAttributes<T>(provider).Single();
	}

	public static T TryGetAttribute<T>(this MemberInfo provider) where T : Attribute
	{
		return LinqExtensions.OnlyOrDefault(AllAttributes<T>(provider));
	}

	public static bool HasAttribute(this MemberInfo provider, params Type[] attributeTypes)
	{
		return AllAttributes(provider, attributeTypes).Any();
	}

	public static bool HasAttribute<T>(this MemberInfo provider) where T : Attribute
	{
		return AllAttributes(provider, typeof(T)).Any();
	}

	public static IEnumerable<T> AllAttributes<T>(this MemberInfo provider) where T : Attribute
	{
		return AllAttributes(provider, typeof(T)).Cast<T>();
	}

	public static IEnumerable<Attribute> AllAttributes(this MemberInfo provider, params Type[] attributeTypes)
	{
		Attribute[] customAttributes = Attribute.GetCustomAttributes(provider, typeof(Attribute), inherit: true);
		if (attributeTypes.Length == 0)
		{
			return customAttributes;
		}
		return customAttributes.Where((Attribute a) => attributeTypes.Any((Type x) => DerivesFromOrEqual(a.GetType(), x)));
	}

	public static bool HasAttribute(this ParameterInfo provider, params Type[] attributeTypes)
	{
		return AllAttributes(provider, attributeTypes).Any();
	}

	public static bool HasAttribute<T>(this ParameterInfo provider) where T : Attribute
	{
		return AllAttributes(provider, typeof(T)).Any();
	}

	public static IEnumerable<T> AllAttributes<T>(this ParameterInfo provider) where T : Attribute
	{
		return AllAttributes(provider, typeof(T)).Cast<T>();
	}

	public static IEnumerable<Attribute> AllAttributes(this ParameterInfo provider, params Type[] attributeTypes)
	{
		Attribute[] customAttributes = Attribute.GetCustomAttributes(provider, typeof(Attribute), inherit: true);
		if (attributeTypes.Length == 0)
		{
			return customAttributes;
		}
		return customAttributes.Where((Attribute a) => attributeTypes.Any((Type x) => DerivesFromOrEqual(a.GetType(), x)));
	}
}
