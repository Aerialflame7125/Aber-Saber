using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using Aerial.Note;

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
			_flyingTextSpawner.SpawnText(position, "X");
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene scene = SceneManager.GetSceneAt(i);
				// Get all root GameObjects in the scene
				GameObject[] rootObjects = scene.GetRootGameObjects();

				foreach (GameObject rootObject in rootObjects)
				{
					// Search for MissedCounter in the root object and its children (including inactive)
					Aerial.Note.MissedCounter counter = rootObject.GetComponentInChildren<Aerial.Note.MissedCounter>(true);
					counter.newMiss();
				}
			}
		}
	}

	private void OnDestroy()
	{
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissed;
	}
}
