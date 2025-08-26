using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class GetFromPrefabComponentProvider : IProvider
{
	private readonly IPrefabInstantiator _prefabInstantiator;

	private readonly Type _componentType;

	private readonly bool _matchSingle;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public GetFromPrefabComponentProvider(Type componentType, IPrefabInstantiator prefabInstantiator, bool matchSingle)
	{
		_prefabInstantiator = prefabInstantiator;
		_componentType = componentType;
		_matchSingle = matchSingle;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return _componentType;
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsNotNull(context);
		GameObject gameObject = _prefabInstantiator.Instantiate(args, out injectAction);
		if (_matchSingle)
		{
			Component componentInChildren = gameObject.GetComponentInChildren(_componentType, includeInactive: true);
			Assert.IsNotNull(componentInChildren, "Could not find component with type '{0}' on prefab '{1}'", _componentType, _prefabInstantiator.GetPrefab().name);
			buffer.Add(componentInChildren);
		}
		else
		{
			Component[] componentsInChildren = gameObject.GetComponentsInChildren(_componentType, includeInactive: true);
			Assert.That(componentsInChildren.Length >= 1, "Expected to find at least one component with type '{0}' on prefab '{1}'", _componentType, _prefabInstantiator.GetPrefab().name);
			MiscExtensions.AllocFreeAddRange(buffer, componentsInChildren);
		}
	}
}
