using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using Zenject.Internal;

namespace Zenject;

public abstract class KeyedFactoryBase<TBase, TKey> : IValidatable
{
	[Inject]
	private readonly DiContainer _container;

	[InjectOptional]
	private readonly List<ValuePair<TKey, Type>> _typePairs;

	private Dictionary<TKey, Type> _typeMap;

	[InjectOptional]
	private readonly Type _fallbackType;

	protected DiContainer Container => _container;

	protected abstract IEnumerable<Type> ProvidedTypes { get; }

	public ICollection<TKey> Keys => _typeMap.Keys;

	protected Dictionary<TKey, Type> TypeMap => _typeMap;

	[Inject]
	public void Initialize()
	{
		Assert.That(_fallbackType == null || TypeExtensions.DerivesFromOrEqual<TBase>(_fallbackType), "Expected fallback type '{0}' to derive from '{1}'", _fallbackType, typeof(TBase));
		_typeMap = _typePairs.ToDictionary((ValuePair<TKey, Type> x) => x.First, (ValuePair<TKey, Type> x) => x.Second);
		_typePairs.Clear();
	}

	public bool HasKey(TKey key)
	{
		return _typeMap.ContainsKey(key);
	}

	protected Type GetTypeForKey(TKey key)
	{
		if (!_typeMap.TryGetValue(key, out var value))
		{
			Assert.IsNotNull(_fallbackType, "Could not find instance for key '{0}'", key);
			return _fallbackType;
		}
		return value;
	}

	public virtual void Validate()
	{
		foreach (Type value in _typeMap.Values)
		{
			Container.InstantiateExplicit(value, ValidationUtil.CreateDefaultArgs(ProvidedTypes.ToArray()));
		}
	}

	protected static ConditionCopyNonLazyBinder AddBindingInternal<TDerived>(DiContainer container, TKey key) where TDerived : TBase
	{
		return container.Bind<ValuePair<TKey, Type>>().FromInstance(ValuePair.New(key, typeof(TDerived)));
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((KeyedFactoryBase<TBase, TKey>)P_0)._container = (DiContainer)P_1;
	}

	private static void __zenFieldSetter1(object P_0, object P_1)
	{
		((KeyedFactoryBase<TBase, TKey>)P_0)._typePairs = (List<ValuePair<TKey, Type>>)P_1;
	}

	private static void __zenFieldSetter2(object P_0, object P_1)
	{
		((KeyedFactoryBase<TBase, TKey>)P_0)._fallbackType = (Type)P_1;
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((KeyedFactoryBase<TBase, TKey>)P_0).Initialize();
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(KeyedFactoryBase<TBase, TKey>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[0], "Initialize")
		}, new InjectTypeInfo.InjectMemberInfo[3]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter1, new InjectableInfo(optional: true, null, "_typePairs", typeof(List<ValuePair<TKey, Type>>), null, InjectSources.Any)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter2, new InjectableInfo(optional: true, null, "_fallbackType", typeof(Type), null, InjectSources.Any))
		});
	}
}
