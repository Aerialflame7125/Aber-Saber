using System.Collections.Generic;
using UnityEngine;

public class BombCutSoundEffectManager : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[Space]
	[SerializeField]
	private BombCutSoundEffect _bombCutSoundEffectPrefab;

	[Space]
	[SerializeField]
	private float _volume = 0.3f;

	[SerializeField]
	private AudioClip[] _bombExplosionAudioClips;

	private PlayerController _playerController;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private RandomObjectPicker<AudioClip> _randomSoundPicker;

	private List<BombCutSoundEffect> _activeBombCutSoundEffects;

	private void Awake()
	{
		_randomSoundPicker = new RandomObjectPicker<AudioClip>(_bombExplosionAudioClips, 0f);
		_bombCutSoundEffectPrefab.CreatePool(8, SetBombCutSoundEffectEventCallbacks);
		_activeBombCutSoundEffects = new List<BombCutSoundEffect>(5);
	}

	private void Start()
	{
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCut;
	}

	private void SetBombCutSoundEffectEventCallbacks(BombCutSoundEffect bombCutSoundEffect)
	{
		bombCutSoundEffect.didFinishEvent += HandleBombCutSoundEffectDidFinishEvent;
	}

	private void HandleNoteWasCut(BeatmapObjectSpawnController beatmapObjectSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			bool wasInstantiated = false;
			BombCutSoundEffect bombCutSoundEffect = _bombCutSoundEffectPrefab.Spawn(base.transform.localPosition, Quaternion.identity, out wasInstantiated);
			if (wasInstantiated)
			{
				SetBombCutSoundEffectEventCallbacks(bombCutSoundEffect);
			}
			Saber saber = _playerController.SaberForType(noteCutInfo.saberType);
			bombCutSoundEffect.Init(_randomSoundPicker.PickRandomObject(), saber, _volume);
			_activeBombCutSoundEffects.Add(bombCutSoundEffect);
		}
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectSpawnController)
		{
			_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCut;
		}
	}

	private void HandleBombCutSoundEffectDidFinishEvent(BombCutSoundEffect bombCutSoundEffect)
	{
		_activeBombCutSoundEffects.Remove(bombCutSoundEffect);
		bombCutSoundEffect.Recycle();
	}
}
