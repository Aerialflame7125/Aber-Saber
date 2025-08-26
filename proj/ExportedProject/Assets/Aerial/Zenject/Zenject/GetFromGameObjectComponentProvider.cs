using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class GetFromGameObjectComponentProvider : IProvider
{
	private readonly GameObject _gameObject;

	private readonly Type _componentType;

	private readonly bool _matchSingle;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public GetFromGameObjectComponentProvider(Type componentType, GameObject gameObject, bool matchSingle)
	{
		_componentType = componentType;
		_matchSingle = matchSingle;
		_gameObject = gameObject;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return _componentType;
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsNotNull(context);
		injectAction = null;
		if (_matchSingle)
		{
			Component component = _gameObject.GetComponent(_componentType);
			Assert.IsNotNull(component, "Could not find component with type '{0}' on prefab '{1}'", _componentType, _gameObject.name);
			buffer.Add(component);
		}
		else
		{
			Component[] components = _gameObject.GetComponents(_componentType);
			Assert.That(components.Length >= 1, "Expected to find at least one component with type '{0}' on prefab '{1}'", _componentType, _gameObject.name);
			MiscExtensions.AllocFreeAddRange(buffer, components);
		}
	}
}
