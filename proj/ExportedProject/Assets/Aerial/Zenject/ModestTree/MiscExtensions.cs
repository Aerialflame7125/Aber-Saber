using System;
using System.Collections.Generic;
using System.Linq;

namespace ModestTree;

public static class MiscExtensions
{
	public static string Fmt(this string s, params object[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			object obj = args[i];
			if (obj == null)
			{
				args[i] = "NULL";
			}
			else if (obj is Type)
			{
				args[i] = TypeStringFormatter.PrettyName((Type)obj);
			}
		}
		return string.Format(s, args);
	}

	public static int IndexOf<T>(this IList<T> list, T item)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (object.Equals(list[i], item))
			{
				return i;
			}
		}
		return -1;
	}

	public static string Join(this IEnumerable<string> values, string separator)
	{
		return string.Join(separator, values.ToArray());
	}

	public static void AllocFreeAddRange<T>(this IList<T> list, IList<T> items)
	{
		for (int i = 0; i < items.Count; i++)
		{
			list.Add(items[i]);
		}
	}

	public static void RemoveWithConfirm<T>(this IList<T> list, T item)
	{
		bool condition = list.Remove(item);
		Assert.That(condition);
	}

	public static void RemoveWithConfirm<T>(this LinkedList<T> list, T item)
	{
		bool condition = list.Remove(item);
		Assert.That(condition);
	}

	public static void RemoveWithConfirm<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key)
	{
		bool condition = dictionary.Remove(key);
		Assert.That(condition);
	}

	public static void RemoveWithConfirm<T>(this HashSet<T> set, T item)
	{
		bool condition = set.Remove(item);
		Assert.That(condition);
	}

	public static TVal GetValueAndRemove<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key)
	{
		TVal result = dictionary[key];
		RemoveWithConfirm(dictionary, key);
		return result;
	}
}
