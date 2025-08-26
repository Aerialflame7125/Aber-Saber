using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zenject.Internal;

public static class ZenUtilInternal
{
	public static bool IsNull(object obj)
	{
		return obj?.Equals(null) ?? true;
	}

	public static bool AreFunctionsEqual(Delegate left, Delegate right)
	{
		return left.Target == right.Target && TypeExtensions.Method(left) == TypeExtensions.Method(right);
	}

	public static int GetInheritanceDelta(Type derived, Type parent)
	{
		Assert.That(TypeExtensions.DerivesFromOrEqual(derived, parent));
		if (TypeExtensions.IsInterface(parent))
		{
			return 1;
		}
		if (derived == parent)
		{
			return 0;
		}
		int num = 1;
		Type type = derived;
		while ((type = TypeExtensions.BaseType(type)) != parent)
		{
			num++;
		}
		return num;
	}

	public static IEnumerable<SceneContext> GetAllSceneContexts()
	{
		foreach (Scene scene in UnityUtil.AllLoadedScenes)
		{
			List<SceneContext> contexts = scene.GetRootGameObjects().SelectMany((GameObject root) => root.GetComponentsInChildren<SceneContext>()).ToList();
			if (!LinqExtensions.IsEmpty(contexts))
			{
				Assert.That(contexts.Count == 1, "Found multiple scene contexts in scene '{0}'", scene.name);
				yield return contexts[0];
			}
		}
	}

	public static void AddStateMachineBehaviourAutoInjectersInScene(Scene scene)
	{
		foreach (GameObject rootGameObject in GetRootGameObjects(scene))
		{
			if (rootGameObject != null)
			{
				AddStateMachineBehaviourAutoInjectersUnderGameObject(rootGameObject);
			}
		}
	}

	public static void AddStateMachineBehaviourAutoInjectersUnderGameObject(GameObject root)
	{
		Animator[] componentsInChildren = root.GetComponentsInChildren<Animator>(includeInactive: true);
		Animator[] array = componentsInChildren;
		foreach (Animator animator in array)
		{
			if (animator.gameObject.GetComponent<ZenjectStateMachineBehaviourAutoInjecter>() == null)
			{
				animator.gameObject.AddComponent<ZenjectStateMachineBehaviourAutoInjecter>();
			}
		}
	}

	public static void GetInjectableMonoBehavioursInScene(Scene scene, List<MonoBehaviour> monoBehaviours)
	{
		foreach (GameObject rootGameObject in GetRootGameObjects(scene))
		{
			if (rootGameObject != null)
			{
				GetInjectableMonoBehavioursUnderGameObjectInternal(rootGameObject, monoBehaviours);
			}
		}
	}

	public static void GetInjectableMonoBehavioursUnderGameObject(GameObject gameObject, List<MonoBehaviour> injectableComponents)
	{
		GetInjectableMonoBehavioursUnderGameObjectInternal(gameObject, injectableComponents);
	}

	private static void GetInjectableMonoBehavioursUnderGameObjectInternal(GameObject gameObject, List<MonoBehaviour> injectableComponents)
	{
		if (gameObject == null)
		{
			return;
		}
		MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour monoBehaviour in components)
		{
			if (monoBehaviour != null && TypeExtensions.DerivesFromOrEqual<GameObjectContext>(monoBehaviour.GetType()))
			{
				injectableComponents.Add(monoBehaviour);
				return;
			}
		}
		for (int j = 0; j < gameObject.transform.childCount; j++)
		{
			Transform child = gameObject.transform.GetChild(j);
			if (child != null)
			{
				GetInjectableMonoBehavioursUnderGameObjectInternal(child.gameObject, injectableComponents);
			}
		}
		foreach (MonoBehaviour monoBehaviour2 in components)
		{
			if (monoBehaviour2 != null && IsInjectableMonoBehaviourType(monoBehaviour2.GetType()))
			{
				injectableComponents.Add(monoBehaviour2);
			}
		}
	}

	public static bool IsInjectableMonoBehaviourType(Type type)
	{
		return type != null && !TypeExtensions.DerivesFrom<MonoInstaller>(type) && TypeAnalyzer.HasInfo(type);
	}

	public static IEnumerable<GameObject> GetRootGameObjects(Scene scene)
	{
		if (scene.isLoaded)
		{
			return from x in scene.GetRootGameObjects()
				where x.GetComponent<ProjectContext>() == null
				select x;
		}
		return from x in Resources.FindObjectsOfTypeAll<GameObject>()
			where x.transform.parent == null && x.GetComponent<ProjectContext>() == null && x.scene == scene
			select x;
	}
}
