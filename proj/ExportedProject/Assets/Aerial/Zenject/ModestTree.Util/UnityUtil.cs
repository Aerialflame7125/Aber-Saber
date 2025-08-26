using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModestTree.Util;

public static class UnityUtil
{
	public static IEnumerable<Scene> AllScenes
	{
		get
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				yield return SceneManager.GetSceneAt(i);
			}
		}
	}

	public static IEnumerable<Scene> AllLoadedScenes => AllScenes.Where((Scene scene) => scene.isLoaded);

	public static bool IsAltKeyDown => Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);

	public static bool IsControlKeyDown => Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

	public static bool IsShiftKeyDown => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

	public static bool WasShiftKeyJustPressed => Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);

	public static bool WasAltKeyJustPressed => Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt);

	public static int GetDepthLevel(Transform transform)
	{
		if (transform == null)
		{
			return 0;
		}
		return 1 + GetDepthLevel(transform.parent);
	}

	public static GameObject GetRootParentOrSelf(GameObject gameObject)
	{
		return (from x in GetParentsAndSelf(gameObject.transform)
			select x.gameObject).LastOrDefault();
	}

	public static IEnumerable<Transform> GetParents(Transform transform)
	{
		if (transform == null)
		{
			yield break;
		}
		foreach (Transform item in GetParentsAndSelf(transform.parent))
		{
			yield return item;
		}
	}

	public static IEnumerable<Transform> GetParentsAndSelf(Transform transform)
	{
		if (transform == null)
		{
			yield break;
		}
		yield return transform;
		foreach (Transform item in GetParentsAndSelf(transform.parent))
		{
			yield return item;
		}
	}

	public static IEnumerable<Component> GetComponentsInChildrenTopDown(GameObject gameObject, bool includeInactive)
	{
		return from x in gameObject.GetComponentsInChildren<Component>(includeInactive)
			orderby (!(x == null)) ? GetDepthLevel(x.transform) : int.MinValue
			select x;
	}

	public static IEnumerable<Component> GetComponentsInChildrenBottomUp(GameObject gameObject, bool includeInactive)
	{
		return from x in gameObject.GetComponentsInChildren<Component>(includeInactive)
			orderby (!(x == null)) ? GetDepthLevel(x.transform) : int.MinValue descending
			select x;
	}

	public static IEnumerable<GameObject> GetDirectChildrenAndSelf(GameObject obj)
	{
		yield return obj;
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform child = (Transform)enumerator.Current;
				yield return child.gameObject;
			}
		}
		finally
		{
			IDisposable disposable2;
			IDisposable disposable = (disposable2 = enumerator as IDisposable);
			if (disposable2 != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static IEnumerable<GameObject> GetDirectChildren(GameObject obj)
	{
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform child = (Transform)enumerator.Current;
				yield return child.gameObject;
			}
		}
		finally
		{
			IDisposable disposable2;
			IDisposable disposable = (disposable2 = enumerator as IDisposable);
			if (disposable2 != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static IEnumerable<GameObject> GetAllGameObjects()
	{
		return from x in UnityEngine.Object.FindObjectsOfType<Transform>()
			select x.gameObject;
	}

	public static List<GameObject> GetAllRootGameObjects()
	{
		return (from x in GetAllGameObjects()
			where x.transform.parent == null
			select x).ToList();
	}
}
