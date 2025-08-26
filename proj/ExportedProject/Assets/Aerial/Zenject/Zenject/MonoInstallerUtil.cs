using ModestTree;
using UnityEngine;

namespace Zenject;

public static class MonoInstallerUtil
{
	public static string GetDefaultResourcePath<TInstaller>() where TInstaller : MonoInstallerBase
	{
		return "Installers/" + TypeStringFormatter.PrettyName(typeof(TInstaller));
	}

	public static TInstaller CreateInstaller<TInstaller>(string resourcePath, DiContainer container) where TInstaller : MonoInstallerBase
	{
		bool shouldMakeActive;
		GameObject gameObject = container.CreateAndParentPrefabResource(resourcePath, GameObjectCreationParameters.Default, null, out shouldMakeActive);
		if (shouldMakeActive && !container.IsValidating)
		{
			gameObject.SetActive(value: true);
		}
		TInstaller[] componentsInChildren = gameObject.GetComponentsInChildren<TInstaller>();
		Assert.That(componentsInChildren.Length == 1, "Could not find unique MonoInstaller with type '{0}' on prefab '{1}'", typeof(TInstaller), gameObject.name);
		return componentsInChildren[0];
	}
}
