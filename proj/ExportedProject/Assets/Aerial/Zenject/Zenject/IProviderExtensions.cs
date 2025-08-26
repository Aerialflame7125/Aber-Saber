using System;
using System.Collections.Generic;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

public static class IProviderExtensions
{
	private static readonly List<TypeValuePair> EmptyArgList = new List<TypeValuePair>();

	public static void GetAllInstancesWithInjectSplit(this IProvider creator, InjectContext context, out Action injectAction, List<object> buffer)
	{
		creator.GetAllInstancesWithInjectSplit(context, EmptyArgList, out injectAction, buffer);
	}

	public static void GetAllInstances(this IProvider creator, InjectContext context, List<object> buffer)
	{
		GetAllInstances(creator, context, EmptyArgList, buffer);
	}

	public static void GetAllInstances(this IProvider creator, InjectContext context, List<TypeValuePair> args, List<object> buffer)
	{
		Assert.IsNotNull(context);
		creator.GetAllInstancesWithInjectSplit(context, args, out var injectAction, buffer);
		injectAction?.Invoke();
	}

	public static object TryGetInstance(this IProvider creator, InjectContext context)
	{
		return TryGetInstance(creator, context, EmptyArgList);
	}

	public static object TryGetInstance(this IProvider creator, InjectContext context, List<TypeValuePair> args)
	{
		List<object> list = ZenPools.SpawnList<object>();
		try
		{
			GetAllInstances(creator, context, args, list);
			if (list.Count == 0)
			{
				return null;
			}
			Assert.That(list.Count == 1, "Provider returned multiple instances when one or zero was expected");
			return list[0];
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}

	public static object GetInstance(this IProvider creator, InjectContext context)
	{
		return GetInstance(creator, context, EmptyArgList);
	}

	public static object GetInstance(this IProvider creator, InjectContext context, List<TypeValuePair> args)
	{
		List<object> list = ZenPools.SpawnList<object>();
		try
		{
			GetAllInstances(creator, context, args, list);
			Assert.That(list.Count > 0, "Provider returned zero instances when one was expected when looking up type '{0}'", context.MemberType);
			Assert.That(list.Count == 1, "Provider returned multiple instances when only one was expected when looking up type '{0}'", context.MemberType);
			return list[0];
		}
		finally
		{
			ZenPools.DespawnList(list);
		}
	}
}
