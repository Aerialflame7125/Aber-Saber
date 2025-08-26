using ModestTree;
using UnityEngine;

namespace Zenject;

public static class ScriptableObjectInstallerUtil
{
	public static string GetDefaultResourcePath<TInstaller>() where TInstaller : ScriptableObjectInstallerBase
	{
		return "Installers/" + TypeStringFormatter.PrettyName(typeof(TInstaller));
	}

	public static TInstaller CreateInstaller<TInstaller>(string resourcePath, DiContainer container) where TInstaller : ScriptableObjectInstallerBase
	{
		Object[] array = Resources.LoadAll(resourcePath);
		Assert.That(array.Length == 1, "Could not find unique ScriptableObjectInstaller with type '{0}' at resource path '{1}'", typeof(TInstaller), resourcePath);
		Object obj = array[0];
		Assert.That(obj is TInstaller, "Expected to find installer with type '{0}' at resource path '{1}'", typeof(TInstaller), resourcePath);
		return (TInstaller)obj;
	}
}
