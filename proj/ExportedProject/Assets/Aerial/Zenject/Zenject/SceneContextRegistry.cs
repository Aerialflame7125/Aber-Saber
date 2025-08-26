using System.Collections.Generic;
using ModestTree;
using UnityEngine.SceneManagement;
using Zenject.Internal;

namespace Zenject;

public class SceneContextRegistry
{
	private readonly Dictionary<Scene, SceneContext> _map = new Dictionary<Scene, SceneContext>();

	public IEnumerable<SceneContext> SceneContexts => _map.Values;

	public void Add(SceneContext context)
	{
		Assert.That(!_map.ContainsKey(context.gameObject.scene));
		_map.Add(context.gameObject.scene, context);
	}

	public SceneContext GetSceneContextForScene(string name)
	{
		Scene sceneByName = SceneManager.GetSceneByName(name);
		Assert.That(sceneByName.IsValid(), "Could not find scene with name '{0}'", name);
		return GetSceneContextForScene(sceneByName);
	}

	public SceneContext GetSceneContextForScene(Scene scene)
	{
		return _map[scene];
	}

	public SceneContext TryGetSceneContextForScene(string name)
	{
		Scene sceneByName = SceneManager.GetSceneByName(name);
		Assert.That(sceneByName.IsValid(), "Could not find scene with name '{0}'", name);
		return TryGetSceneContextForScene(sceneByName);
	}

	public SceneContext TryGetSceneContextForScene(Scene scene)
	{
		if (_map.TryGetValue(scene, out var value))
		{
			return value;
		}
		return null;
	}

	public DiContainer GetContainerForScene(Scene scene)
	{
		DiContainer diContainer = TryGetContainerForScene(scene);
		if (diContainer != null)
		{
			return diContainer;
		}
		throw Assert.CreateException("Unable to find DiContainer for scene '{0}'", scene.name);
	}

	public DiContainer TryGetContainerForScene(Scene scene)
	{
		if (scene == ProjectContext.Instance.gameObject.scene)
		{
			return ProjectContext.Instance.Container;
		}
		SceneContext sceneContext = TryGetSceneContextForScene(scene);
		if (sceneContext != null)
		{
			return sceneContext.Container;
		}
		return null;
	}

	public void Remove(SceneContext context)
	{
		if (!_map.Remove(context.gameObject.scene))
		{
			Log.Warn("Failed to remove SceneContext from SceneContextRegistry");
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new SceneContextRegistry();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(SceneContextRegistry), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
