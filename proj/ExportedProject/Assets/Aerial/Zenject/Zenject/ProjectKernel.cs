using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject.Internal;

namespace Zenject;

public class ProjectKernel : MonoKernel
{
	[Inject]
	private ZenjectSettings _settings;

	[Inject]
	private SceneContextRegistry _contextRegistry;

	public void OnApplicationQuit()
	{
		if (_settings.EnsureDeterministicDestructionOrderOnApplicationQuit)
		{
			DestroyEverythingInOrder();
		}
	}

	public void DestroyEverythingInOrder()
	{
		ForceUnloadAllScenes(immediate: true);
		Assert.That(!base.IsDestroyed);
		Object.DestroyImmediate(base.gameObject);
		Assert.That(base.IsDestroyed);
	}

	public void ForceUnloadAllScenes(bool immediate = false)
	{
		Assert.That(!base.IsDestroyed);
		List<Scene> sceneOrder = new List<Scene>();
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			sceneOrder.Add(SceneManager.GetSceneAt(i));
		}
		foreach (SceneContext item in _contextRegistry.SceneContexts.OrderByDescending((SceneContext x) => sceneOrder.IndexOf(x.gameObject.scene)).ToList())
		{
			if (immediate)
			{
				Object.DestroyImmediate(item.gameObject);
			}
			else
			{
				Object.Destroy(item.gameObject);
			}
		}
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((ProjectKernel)P_0)._settings = (ZenjectSettings)P_1;
	}

	private static void __zenFieldSetter1(object P_0, object P_1)
	{
		((ProjectKernel)P_0)._contextRegistry = (SceneContextRegistry)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ProjectKernel), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[2]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_settings", typeof(ZenjectSettings), null, InjectSources.Any)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter1, new InjectableInfo(optional: false, null, "_contextRegistry", typeof(SceneContextRegistry), null, InjectSources.Any))
		});
	}
}
