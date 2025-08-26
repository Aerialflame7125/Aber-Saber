using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class PrefabGameObjectProvider : IProvider
{
	private readonly IPrefabInstantiator _prefabCreator;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public PrefabGameObjectProvider(IPrefabInstantiator prefabCreator)
	{
		_prefabCreator = prefabCreator;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(GameObject);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		GameObject item = _prefabCreator.Instantiate(args, out injectAction);
		buffer.Add(item);
	}
}
