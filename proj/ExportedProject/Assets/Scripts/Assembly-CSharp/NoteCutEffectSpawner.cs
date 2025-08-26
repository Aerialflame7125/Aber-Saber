using UnityEngine;
using BL.Replays;
using UnityEngine.SceneManagement;

public class NoteCutEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	[Provider(typeof(BeatmapDataModel))]
	private ObjectProvider _beatmapDataModelProvider;

	[SerializeField]
	private ColorManager _colorManager;

	[Space]
	[SerializeField]
	private NoteCutParticlesEffect _noteCutParticlesEffect;

	[SerializeField]
	private NoteDebrisSpawner _noteDebrisSpawner;

	[SerializeField]
	private NoteCutHapticEffect _noteCutHapticEffect;

	[SerializeField]
	private FlyingSpriteSpawner _failFlyingSpriteSpawner;

	[SerializeField]
	private FlyingScoreSpawner _flyingScoreSpawner;

	[SerializeField]
	private ShockwaveEffect _shockwaveEffect;

	[SerializeField]
	private BombExplosionEffect _bombExplosionEffect;

	[Space]
	[SerializeField]
	private float _maxTimeForCloseNotes = 0.5f;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private ScoreController _scoreController;

	private BeatmapDataModel _beatmapDataModel;

	private void Start()
	{
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCutEvent;
		_beatmapDataModel = _beatmapDataModelProvider.GetProvidedObject<BeatmapDataModel>();
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectSpawnController)
		{
			_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCutEvent;
		}
	}

	private void HandleNoteWasCutEvent(BeatmapObjectSpawnController noteSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		Vector3 position = noteController.noteTransform.position;
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			SpawnBombCutEffect(position, noteController, noteCutInfo);
		}
		else if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			SpawnNoteCutEffect(position, noteController, noteCutInfo);
		}
		_noteCutHapticEffect.RumbleController(noteCutInfo.saberType);
	}

	private void SpawnNoteCutEffect(Vector3 pos, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		// It's a good practice to check if the instance is available before using it
		if (VRControllersRecorder.Instance != null)
		{
			// Simply call the recorder's public method with the data it needs.
			// The VRControllersRecorder class will handle all the logic for creating and storing the NoteEvent.
			VRControllersRecorder.Instance.RecordNoteEvent(noteController.noteData, noteCutInfo, Time.time);
		}
		else
		{
			Debug.LogWarning("VRControllersRecorder instance is not available. Cannot record note event.");
		}

		if (noteCutInfo.allIsOK)
		{
			NoteData noteData = noteController.noteData;
			Color color = _colorManager.ColorForNoteType(noteData.noteType);
			if (_beatmapDataModel.IsNoteInTimeInterval(noteData.time + 0.05f, _maxTimeForCloseNotes))
			{
				_noteCutParticlesEffect.SpawnParticles(pos, noteCutInfo.cutNormal, noteCutInfo.saberDir, color, 100, 50);
			}
			else
			{
				_noteCutParticlesEffect.SpawnParticles(pos, noteCutInfo.cutNormal, noteCutInfo.saberDir, color, 150, 100);
			}
			_flyingScoreSpawner.SpawnFlyingScore(noteCutInfo, noteData.lineIndex, pos, new Color(0f, 0.5f, 1f), noteCutInfo.afterCutSwingRatingCounter);
			_shockwaveEffect.SpawnShockwave(pos);
		}
		else
		{
			_failFlyingSpriteSpawner.SpawnFlyingSprite(pos);
		}
		_noteDebrisSpawner.SpawnDebris(noteCutInfo, noteController);
	}

	private void SpawnBombCutEffect(Vector3 pos, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		_bombExplosionEffect.SpawnExplosion(pos);
		_shockwaveEffect.SpawnShockwave(pos);
		_failFlyingSpriteSpawner.SpawnFlyingSprite(pos);
	}
}
