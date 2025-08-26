using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public abstract class SubContainerCreatorByNewPrefabDynamicContext : ISubContainerCreator
{
	private readonly GameObjectCreationParameters _gameObjectBindInfo;

	private readonly IPrefabProvider _prefabProvider;

	private readonly DiContainer _container;

	public SubContainerCreatorByNewPrefabDynamicContext(DiContainer container, IPrefabProvider prefabProvider, GameObjectCreationParameters gameObjectBindInfo)
	{
		_gameObjectBindInfo = gameObjectBindInfo;
		_prefabProvider = prefabProvider;
		_container = container;
	}

	public DiContainer CreateSubContainer(List<TypeValuePair> args, InjectContext parentContext)
	{
		Object prefab = _prefabProvider.GetPrefab();
		bool shouldMakeActive;
		GameObject gameObject = _container.CreateAndParentPrefab(prefab, _gameObjectBindInfo, null, out shouldMakeActive);
		if (gameObject.GetComponent<GameObjectContext>() != null)
		{
			throw Assert.CreateException("Found GameObjectContext already attached to prefab with name '{0}'!  When using ByNewPrefabMethod, the GameObjectContext is added to the prefab dynamically", prefab.name);
		}
		GameObjectContext gameObjectContext = gameObject.AddComponent<GameObjectContext>();
		AddInstallers(args, gameObjectContext);
		_container.Inject(gameObjectContext);
		if (shouldMakeActive && !_container.IsValidating)
		{
			gameObject.SetActive(value: true);
		}
		return gameObjectContext.Container;
	}

	protected abstract void AddInstallers(List<TypeValuePair> args, GameObjectContext context);
}
