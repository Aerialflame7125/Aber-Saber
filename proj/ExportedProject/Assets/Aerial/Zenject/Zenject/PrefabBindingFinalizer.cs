using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class PrefabBindingFinalizer : ProviderBindingFinalizer
{
	private readonly GameObjectCreationParameters _gameObjectBindInfo;

	private readonly UnityEngine.Object _prefab;

	private readonly Func<Type, IPrefabInstantiator, IProvider> _providerFactory;

	public PrefabBindingFinalizer(BindInfo bindInfo, GameObjectCreationParameters gameObjectBindInfo, UnityEngine.Object prefab, Func<Type, IPrefabInstantiator, IProvider> providerFactory)
		: base(bindInfo)
	{
		_gameObjectBindInfo = gameObjectBindInfo;
		_prefab = prefab;
		_providerFactory = providerFactory;
	}

	protected override void OnFinalizeBinding(DiContainer container)
	{
		if (base.BindInfo.ToChoice == ToChoices.Self)
		{
			Assert.IsEmpty(base.BindInfo.ToTypes);
			FinalizeBindingSelf(container);
		}
		else
		{
			FinalizeBindingConcrete(container, base.BindInfo.ToTypes);
		}
	}

	private void FinalizeBindingConcrete(DiContainer container, List<Type> concreteTypes)
	{
		switch (GetScope())
		{
		case ScopeTypes.Transient:
			RegisterProvidersForAllContractsPerConcreteType(container, concreteTypes, (DiContainer _, Type concreteType) => _providerFactory(concreteType, new PrefabInstantiator(container, _gameObjectBindInfo, concreteType, base.BindInfo.Arguments, new PrefabProvider(_prefab), base.BindInfo.InstantiatedCallback)));
			break;
		case ScopeTypes.Singleton:
		{
			Type type = LinqExtensions.OnlyOrDefault(concreteTypes);
			if (type == null)
			{
				Assert.That(LinqExtensions.IsEmpty(base.BindInfo.Arguments), "Cannot provide arguments to prefab instantiator when using more than one concrete type");
			}
			PrefabInstantiatorCached prefabCreator = new PrefabInstantiatorCached(new PrefabInstantiator(container, _gameObjectBindInfo, type, base.BindInfo.Arguments, new PrefabProvider(_prefab), base.BindInfo.InstantiatedCallback));
			RegisterProvidersForAllContractsPerConcreteType(container, concreteTypes, (DiContainer _, Type concreteType) => Zenject.BindingUtil.CreateCachedProvider(_providerFactory(concreteType, prefabCreator)));
			break;
		}
		default:
			throw Assert.CreateException();
		}
	}

	private void FinalizeBindingSelf(DiContainer container)
	{
		switch (GetScope())
		{
		case ScopeTypes.Transient:
			RegisterProviderPerContract(container, (DiContainer _, Type contractType) => _providerFactory(contractType, new PrefabInstantiator(container, _gameObjectBindInfo, contractType, base.BindInfo.Arguments, new PrefabProvider(_prefab), base.BindInfo.InstantiatedCallback)));
			break;
		case ScopeTypes.Singleton:
		{
			Type type = LinqExtensions.OnlyOrDefault(base.BindInfo.ContractTypes);
			if (type == null)
			{
				Assert.That(LinqExtensions.IsEmpty(base.BindInfo.Arguments), "Cannot provide arguments to prefab instantiator when using more than one concrete type");
			}
			PrefabInstantiatorCached prefabCreator = new PrefabInstantiatorCached(new PrefabInstantiator(container, _gameObjectBindInfo, type, base.BindInfo.Arguments, new PrefabProvider(_prefab), base.BindInfo.InstantiatedCallback));
			RegisterProviderPerContract(container, (DiContainer _, Type contractType) => Zenject.BindingUtil.CreateCachedProvider(_providerFactory(contractType, prefabCreator)));
			break;
		}
		default:
			throw Assert.CreateException();
		}
	}
}
