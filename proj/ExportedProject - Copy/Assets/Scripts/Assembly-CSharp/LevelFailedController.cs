using System.Collections;
using UnityEngine;

public class LevelFailedController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	[Provider(typeof(SongController))]
	private ObjectProvider _songControllerProvider;

	[SerializeField]
	private PrepareLevelEndMenuSceneSetupData _prepareLevelFinishedMenuSceneSetupData;

	[SerializeField]
	private LevelFailedTextEffect _levelFailedTextEffect;

	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	private GameSongController _gameSongController;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private void Start()
	{
		_gameSongController = _songControllerProvider.GetProvidedObject<SongController>() as GameSongController;
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
	}

	public void StartLevelFailed()
	{
		StartCoroutine(LevelFailedCoroutine());
	}

	private IEnumerator LevelFailedCoroutine()
	{
		LevelCompletionResults levelCompletionResults = _prepareLevelFinishedMenuSceneSetupData.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.Failed);
		_gameSongController.FailStopSong();
		_beatmapObjectSpawnController.StopSpawningAndDissolveAllObjects();
		_levelFailedTextEffect.ShowEffect();
		yield return new WaitForSeconds(2f);
		_mainGameSceneSetupData.Finish(levelCompletionResults);
	}
}
