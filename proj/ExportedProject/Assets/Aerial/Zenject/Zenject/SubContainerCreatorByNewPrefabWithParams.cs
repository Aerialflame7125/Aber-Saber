using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class SubContainerCreatorByNewPrefabWithParams : ISubContainerCreator
{
	private readonly DiContainer _container;

	private readonly IPrefabProvider _prefabProvider;

	private readonly Type _installerType;

	private readonly GameObjectCreationParameters _gameObjectBindInfo;

	protected DiContainer Container => _container;

	public SubContainerCreatorByNewPrefabWithParams(Type installerType, DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo)
	{
		_gameObjectBindInfo = gameObjectBindInfo;
		_prefabProvider = prefabProvider;
		_container = container;
		_installerType = installerType;
	}

	private DiContainer CreateTempContainer(List<TypeValuePair> args)
	{
		DiContainer diContainer = Container.CreateSubContainer();
		InjectTypeInfo info = TypeAnalyzer.GetInfo(_installerType);
		foreach (TypeValuePair argPair in args)
		{
			InjectableInfo injectableInfo = (from x in info.AllInjectables
				where TypeExtensions.DerivesFromOrEqual(argPair.Type, x.MemberType)
				orderby ZenUtilInternal.GetInheritanceDelta(argPair.Type, x.MemberType)
				select x).FirstOrDefault();
			Assert.That(injectableInfo != null, "Could not find match for argument type '{0}' when injecting into sub container installer '{1}'", argPair.Type, _installerType);
			diContainer.Bind(injectableInfo.MemberType).FromInstance(argPair.Value).WhenInjectedInto(_installerType);
		}
		return diContainer;
	}

	public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext)
	{
		Assert.That(!LinqExtensions.IsEmpty(args));
		UnityEngine.Object prefab = _prefabProvider.GetPrefab();
		GameObject gameObject = CreateTempContainer(args).InstantiatePrefab(prefab, _gameObjectBindInfo);
		GameObjectContext component = gameObject.GetComponent<GameObjectContext>();
		Assert.That(component != null, "Expected prefab with name '{0}' to container a component of type 'GameObjectContext'", prefab.name);
		return component.Container;
	}
}
