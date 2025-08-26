using System;
using System.Collections.Generic;

namespace Zenject.Internal;

public static class ZenPools
{
	private static readonly StaticMemoryPool<InjectContext> _contextPool = new StaticMemoryPool<InjectContext>();

	private static readonly StaticMemoryPool<LookupId> _lookupIdPool = new StaticMemoryPool<LookupId>();

	private static readonly StaticMemoryPool<BindInfo> _bindInfoPool = new StaticMemoryPool<BindInfo>();

	private static readonly StaticMemoryPool<BindStatement> _bindStatementPool = new StaticMemoryPool<BindStatement>();

	public static Dictionary<TKey, TValue> SpawnDictionary<TKey, TValue>()
	{
		return DictionaryPool<TKey, TValue>.Instance.Spawn();
	}

	public static BindStatement SpawnStatement()
	{
		return _bindStatementPool.Spawn();
	}

	public static void DespawnStatement(BindStatement statement)
	{
		statement.Reset();
		_bindStatementPool.Despawn(statement);
	}

	public static BindInfo SpawnBindInfo()
	{
		return _bindInfoPool.Spawn();
	}

	public static void DespawnBindInfo(BindInfo bindInfo)
	{
		bindInfo.Reset();
		_bindInfoPool.Despawn(bindInfo);
	}

	public static void DespawnDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
	{
		DictionaryPool<TKey, TValue>.Instance.Despawn(dictionary);
	}

	public static LookupId SpawnLookupId(IProvider provider, BindingId bindingId)
	{
		LookupId lookupId = _lookupIdPool.Spawn();
		lookupId.Provider = provider;
		lookupId.BindingId = bindingId;
		return lookupId;
	}

	public static void DespawnLookupId(LookupId lookupId)
	{
		_lookupIdPool.Despawn(lookupId);
	}

	public static List<T> SpawnList<T>()
	{
		return ListPool<T>.Instance.Spawn();
	}

	public static void DespawnList<T>(List<T> list)
	{
		ListPool<T>.Instance.Despawn(list);
	}

	public static void DespawnArray<T>(T[] arr)
	{
		ArrayPool<T>.GetPool(arr.Length).Despawn(arr);
	}

	public static T[] SpawnArray<T>(int length)
	{
		return ArrayPool<T>.GetPool(length).Spawn();
	}

	public static InjectContext SpawnInjectContext(DiContainer container, Type memberType)
	{
		InjectContext injectContext = _contextPool.Spawn();
		injectContext.Container = container;
		injectContext.MemberType = memberType;
		return injectContext;
	}

	public static void DespawnInjectContext(InjectContext context)
	{
		context.Reset();
		_contextPool.Despawn(context);
	}

	public static InjectContext SpawnInjectContext(DiContainer container, InjectableInfo injectableInfo, InjectContext currentContext, object targetInstance, Type targetType, object concreteIdentifier)
	{
		InjectContext injectContext = SpawnInjectContext(container, injectableInfo.MemberType);
		injectContext.ObjectType = targetType;
		injectContext.ParentContext = currentContext;
		injectContext.ObjectInstance = targetInstance;
		injectContext.Identifier = injectableInfo.Identifier;
		injectContext.MemberName = injectableInfo.MemberName;
		injectContext.Optional = injectableInfo.Optional;
		injectContext.SourceType = injectableInfo.SourceType;
		injectContext.FallBackValue = injectableInfo.DefaultValue;
		injectContext.ConcreteIdentifier = concreteIdentifier;
		return injectContext;
	}
}
