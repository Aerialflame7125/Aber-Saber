using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncScenesLoader : MonoBehaviour
{
	[SerializeField]
	private SceneInfo _mainScene;

	[SerializeField]
	private SceneInfo[] _additiveScenes;

	[Tooltip("Which scene from loaded scenes should be set as active? Indexing starts at 0 with the MainScene.")]
	[SerializeField]
	private int _activeSceneNum = -1;

	public SceneInfo[] additiveScenes
	{
		get
		{
			return _additiveScenes;
		}
		set
		{
			_additiveScenes = value;
		}
	}

	public int activeSceneNum
	{
		get
		{
			return _activeSceneNum;
		}
		set
		{
			_activeSceneNum = value;
		}
	}

	public event Action loadingDidFinishEvent;

	private IEnumerator Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		List<string> loadedScenesNames = new List<string>();
		yield return SceneManager.LoadSceneAsync(_mainScene.sceneName, LoadSceneMode.Single);
		loadedScenesNames.Add(_mainScene.sceneName);
		SceneInfo[] array = _additiveScenes;
		foreach (SceneInfo scene in array)
		{
			string sceneName = scene.sceneName;
			if (!SceneManager.GetSceneByName(sceneName).isLoaded)
			{
				loadedScenesNames.Add(sceneName);
				yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			}
		}
		foreach (string item in loadedScenesNames)
		{
			SetActiveRootObjectsInScene(item, true);
		}
		if (_activeSceneNum > 0)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(_additiveScenes[_activeSceneNum - 1].sceneName));
		}
		else if (_activeSceneNum == 0)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(_mainScene.sceneName));
		}
		if (this.loadingDidFinishEvent != null)
		{
			this.loadingDidFinishEvent();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void SetActiveRootObjectsInScene(string sceneName, bool active)
	{
		Scene sceneByName = SceneManager.GetSceneByName(sceneName);
		if (!sceneByName.IsValid() || !sceneByName.isLoaded)
		{
			Debug.LogError($"Scene '{sceneName}' is not valid or not loaded!");
			return;
		}

		List<GameObject> list = new List<GameObject>(sceneByName.rootCount);
		sceneByName.GetRootGameObjects(list);
		foreach (GameObject item in list)
		{
			item.SetActive(active);
		}
	}
}
