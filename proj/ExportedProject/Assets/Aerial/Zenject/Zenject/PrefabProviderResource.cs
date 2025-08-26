using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class PrefabProviderResource : IPrefabProvider
{
	private readonly string _resourcePath;

	public PrefabProviderResource(string resourcePath)
	{
		_resourcePath = resourcePath;
	}

	public Object GetPrefab()
	{
		GameObject gameObject = (GameObject)Resources.Load(_resourcePath);
		Assert.That(gameObject != null, "Expected to find prefab at resource path '{0}'", _resourcePath);
		return gameObject;
	}
}
