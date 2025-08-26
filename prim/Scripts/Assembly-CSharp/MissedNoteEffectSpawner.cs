using UnityEngine;

public class MissedNoteEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	private FlyingTextSpawner _flyingTextSpawner;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private void Start()
	{
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissed;
	}

	private void HandleNoteWasMissed(BeatmapObjectSpawnController noteSpawnController, NoteController noteController)
	{
		NoteData noteData = noteController.noteData;
		if (noteData.noteType != NoteType.Bomb && (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB))
		{
			Vector3 position = noteController.transform.position;
			position.z = base.transform.position.z;
			_flyingTextSpawner.SpawnText(position, "MISS");
		}
	}

	private void OnDestroy()
	{
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissed;
	}
}
