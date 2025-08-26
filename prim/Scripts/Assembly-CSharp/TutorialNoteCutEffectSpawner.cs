using UnityEngine;

public class TutorialNoteCutEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private NoteCutParticlesEffect _noteCutParticlesEffect;

	[SerializeField]
	private ShockwaveEffect _shockwaveEffect;

	[SerializeField]
	private NoteDebrisSpawner _noteDebrisSpawner;

	[SerializeField]
	private NoteCutHapticEffect _noteCutHapticEffect;

	[SerializeField]
	private FlyingTextSpawner _failFlyingTextSpawner;

	[SerializeField]
	private BombExplosionEffect _bombExplosionEffect;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	public bool handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts { get; set; }

	private void Start()
	{
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCut;
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissed;
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectSpawnController)
		{
			_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCut;
			_beatmapObjectSpawnController.noteWasMissedEvent -= HandleNoteWasMissed;
		}
	}

	private void HandleNoteWasCut(BeatmapObjectSpawnController noteSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
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
		if ((!handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts && noteCutInfo.allIsOK) || (handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts && noteCutInfo.allExceptSaberTypeIsOK && !noteCutInfo.saberTypeOK))
		{
			Color color = _colorManager.ColorForNoteType(noteController.noteData.noteType);
			_noteCutParticlesEffect.SpawnParticles(pos, noteCutInfo.cutNormal, noteCutInfo.saberDir, color, 150, 100);
			_shockwaveEffect.SpawnShockwave(pos);
		}
		else if (!handleWrongSaberTypeAsGoodAndDontWarnOnBadCuts)
		{
			string failText = noteCutInfo.FailText;
			if (failText != string.Empty)
			{
				_failFlyingTextSpawner.SpawnText(pos, failText);
			}
		}
		_noteDebrisSpawner.SpawnDebris(noteCutInfo, noteController);
	}

	private void SpawnBombCutEffect(Vector3 pos, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		_bombExplosionEffect.SpawnExplosion(pos);
		_failFlyingTextSpawner.SpawnText(pos, "Do not cut!");
		_shockwaveEffect.SpawnShockwave(pos);
	}

	private void HandleNoteWasMissed(BeatmapObjectSpawnController noteSpawnController, NoteController noteController)
	{
		Vector3 position = noteController.noteTransform.position;
		if (noteController.noteData.noteType != NoteType.Bomb)
		{
		}
	}
}
