using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ModestTree;

public static class ReflectionUtil
{
	public static Array CreateArray(Type elementType, List<object> instances)
	{
		Array array = Array.CreateInstance(elementType, instances.Count);
		for (int i = 0; i < instances.Count; i++)
		{
			object obj = instances[i];
			if (obj != null)
			{
				Assert.That(TypeExtensions.DerivesFromOrEqual(obj.GetType(), elementType), string.Concat("Wrong type when creating array, expected something assignable from '", elementType, "', but found '", obj.GetType(), "'"));
			}
			array.SetValue(obj, i);
		}
		return array;
	}

	public static IList CreateGenericList(Type elementType, List<object> instances)
	{
		Type type = typeof(List<>).MakeGenericType(elementType);
		IList list = (IList)Activator.CreateInstance(type);
		for (int i = 0; i < instances.Count; i++)
		{
			object obj = instances[i];
			if (obj != null)
			{
				Assert.That(TypeExtensions.DerivesFromOrEqual(obj.GetType(), elementType), string.Concat("Wrong type when creating generic list, expected something assignable from '", elementType, "', but found '", obj.GetType(), "'"));
			}
			list.Add(obj);
		}
		return list;
	}

	public static string ToDebugString(this MethodInfo method)
	{
		return MiscExtensions.Fmt("{0}.{1}", TypeStringFormatter.PrettyName(method.DeclaringType), method.Name);
	}

	public static string ToDebugString(this Action action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1>(this Action<TParam1> action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1, TParam2>(this Action<TParam1, TParam2> action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1, TParam2, TParam3>(this Action<TParam1, TParam2, TParam3> action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1, TParam2, TParam3, TParam4>(this Action<TParam1, TParam2, TParam3, TParam4> action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1, TParam2, TParam3, TParam4, TParam5>(this Action<TParam1, TParam2, TParam3, TParam4, TParam5> action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(this Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> action)
	{
		return ToDebugString(action.Method);
	}

	public static string ToDebugString<TParam1>(this Func<TParam1> func)
	{
		return ToDebugString(func.Method);
	}

	public static string ToDebugString<TParam1, TParam2>(this Func<TParam1, TParam2> func)
	{
		return ToDebugString(func.Method);
	}

	public static string ToDebugString<TParam1, TParam2, TParam3>(this Func<TParam1, TParam2, TParam3> func)
	{
		return ToDebugString(func.Method);
	}

	public static string ToDebugString<TParam1, TParam2, TParam3, TParam4>(this Func<TParam1, TParam2, TParam3, TParam4> func)
	{
		return ToDebugString(func.Method);
	}
}
