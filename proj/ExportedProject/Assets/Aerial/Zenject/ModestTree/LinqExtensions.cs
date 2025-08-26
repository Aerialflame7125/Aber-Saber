using System.Collections.Generic;
using System.Linq;

namespace ModestTree;

public static class LinqExtensions
{
	public static IEnumerable<T> Yield<T>(this T item)
	{
		yield return item;
	}

	public static TSource OnlyOrDefault<TSource>(this IEnumerable<TSource> source)
	{
		Assert.IsNotNull(source);
		if (source.Count() > 1)
		{
			return default(TSource);
		}
		return source.FirstOrDefault();
	}

	public static bool HasAtLeast<T>(this IEnumerable<T> enumerable, int amount)
	{
		return enumerable.Take(amount).Count() == amount;
	}

	public static bool HasMoreThan<T>(this IEnumerable<T> enumerable, int amount)
	{
		return HasAtLeast(enumerable, amount + 1);
	}

	public static bool HasLessThan<T>(this IEnumerable<T> enumerable, int amount)
	{
		return HasAtMost(enumerable, amount - 1);
	}

	public static bool HasAtMost<T>(this IEnumerable<T> enumerable, int amount)
	{
		return enumerable.Take(amount + 1).Count() <= amount;
	}

	public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
	{
		return !enumerable.Any();
	}

	public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> list)
	{
		return from x in list
			group x by x into x
			where x.Skip(1).Any()
			select x.Key;
	}

	public static IEnumerable<T> Except<T>(this IEnumerable<T> list, T item)
	{
		return list.Except(Yield(item));
	}

	public static bool ContainsItem<T>(this IEnumerable<T> list, T value)
	{
		return list.Where((T x) => object.Equals(x, value)).Any();
	}
}
