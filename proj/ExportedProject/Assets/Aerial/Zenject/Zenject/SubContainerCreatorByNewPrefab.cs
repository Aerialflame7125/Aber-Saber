using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class SubContainerCreatorByNewPrefab : ISubContainerCreator
{
	private readonly GameObjectCreationParameters _gameObjectBindInfo;

	private readonly IPrefabProvider _prefabProvider;

	private readonly DiContainer _container;

	public SubContainerCreatorByNewPrefab(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo)
	{
		_gameObjectBindInfo = gameObjectBindInfo;
		_prefabProvider = prefabProvider;
		_container = container;
	}

	public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext)
	{
		Assert.That(LinqExtensions.IsEmpty(args));
		Object prefab = _prefabProvider.GetPrefab();
		GameObject gameObject = _container.InstantiatePrefab(prefab, _gameObjectBindInfo);
		GameObjectContext component = gameObject.GetComponent<GameObjectContext>();
		Assert.That(component != null, "Expected prefab with name '{0}' to container a component of type 'GameObjectContext'", prefab.name);
		return component.Container;
	}
}
