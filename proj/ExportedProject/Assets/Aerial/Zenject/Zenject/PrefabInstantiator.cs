using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class PrefabInstantiator : IPrefabInstantiator
{
	private readonly IPrefabProvider _prefabProvider;

	private readonly DiContainer _container;

	private readonly List<TypeValuePair> _extraArguments;

	private readonly GameObjectCreationParameters _gameObjectBindInfo;

	private readonly Type _argumentTarget;

	private readonly Action<InjectContext, object> _instantiateCallback;

	public GameObjectCreationParameters GameObjectCreationParameters => _gameObjectBindInfo;

	public Type ArgumentTarget => _argumentTarget;

	public List<TypeValuePair> ExtraArguments => _extraArguments;

	public PrefabInstantiator(DiContainer container, GameObjectCreationParameters gameObjectBindInfo, Type argumentTarget, IEnumerable<TypeValuePair> extraArguments, IPrefabProvider prefabProvider, Action<InjectContext, object> instantiateCallback)
	{
		_prefabProvider = prefabProvider;
		_extraArguments = extraArguments.ToList();
		_container = container;
		_gameObjectBindInfo = gameObjectBindInfo;
		_argumentTarget = argumentTarget;
		_instantiateCallback = instantiateCallback;
	}

	public UnityEngine.Object GetPrefab()
	{
		return _prefabProvider.GetPrefab();
	}

	public GameObject Instantiate(List<TypeValuePair> args, out Action injectAction)
	{
		InjectContext context = new InjectContext(_container, _argumentTarget, null);
		bool shouldMakeActive;
		GameObject gameObject = _container.CreateAndParentPrefab(GetPrefab(), _gameObjectBindInfo, context, out shouldMakeActive);
		Assert.IsNotNull(gameObject);
		injectAction = delegate
		{
			List<TypeValuePair> list = ZenPools.SpawnList<TypeValuePair>();
			MiscExtensions.AllocFreeAddRange(list, _extraArguments);
			MiscExtensions.AllocFreeAddRange(list, args);
			if (_argumentTarget == null)
			{
				Assert.That(LinqExtensions.IsEmpty(list), "Unexpected arguments provided to prefab instantiator.  Arguments are not allowed if binding multiple components in the same binding");
			}
			Component component = null;
			if (_argumentTarget == null || LinqExtensions.IsEmpty(list))
			{
				_container.InjectGameObject(gameObject);
			}
			else
			{
				component = _container.InjectGameObjectForComponentExplicit(gameObject, _argumentTarget, list, context, null);
				Assert.That(list.Count == 0);
			}
			ZenPools.DespawnList(list);
			if (shouldMakeActive && !_container.IsValidating)
			{
				gameObject.SetActive(value: true);
			}
			if (_instantiateCallback != null && _argumentTarget != null)
			{
				if (component == null)
				{
					component = gameObject.GetComponentInChildren(_argumentTarget);
				}
				if (component != null)
				{
					_instantiateCallback(context, component);
				}
			}
		};
		return gameObject;
	}
}
