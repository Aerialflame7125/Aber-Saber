using UnityEngine;

public class BeatEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private BeatEffect _beatEffectPrefab;

	[SerializeField]
	private float _effectDuration = 1f;

	private SongController _songController;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private void Start()
	{
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatEffectPrefab.CreatePool(20);
		_beatmapObjectSpawnController.noteDidStartJumpEvent += HandleNoteDidStartJumpEvent;
	}

	private void HandleNoteDidStartJumpEvent(BeatmapObjectSpawnController beatmapObjectSpawnController, NoteController noteController)
	{
		NoteData noteData = noteController.noteData;
		BeatEffect beatEffect = _beatEffectPrefab.Spawn(noteController.noteTransform.position, Quaternion.identity);
		beatEffect.Init(_colorManager.ColorForNoteType(noteData.noteType), _effectDuration);
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectSpawnController)
		{
			_beatmapObjectSpawnController.noteDidStartJumpEvent -= HandleNoteDidStartJumpEvent;
		}
	}
}
