using UnityEngine;

public class StandardLevelLoaderSceneSetup : MonoBehaviour
{
	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	[SerializeField]
	private StandardLevelSO _defaultStandardLevel;

	[SerializeField]
	private AsyncScenesLoader _asyncScenesLoader;

	private void Awake()
	{
		if (!_mainGameSceneSetupData.wasUsedInLastTransition)
		{
			_mainGameSceneSetupData.Init(_defaultStandardLevel.difficultyBeatmaps[0], GameplayOptions.defaultOptions, GameplayMode.PartyStandard, 0f);
		}
		SceneInfo[] array = new SceneInfo[_asyncScenesLoader.additiveScenes.Length + 1];
		_asyncScenesLoader.additiveScenes.CopyTo(array, 0);
		SceneInfo environmentSceneInfo = _mainGameSceneSetupData.difficultyLevel.level.environmentSceneInfo;
		if (environmentSceneInfo == null)
		{
			environmentSceneInfo = _defaultStandardLevel.environmentSceneInfo;
		}
		array[_asyncScenesLoader.additiveScenes.Length] = environmentSceneInfo;
		_asyncScenesLoader.additiveScenes = array;
	}
}
