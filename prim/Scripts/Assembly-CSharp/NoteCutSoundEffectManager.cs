using System.Collections.Generic;
using UnityEngine;

public class NoteCutSoundEffectManager : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	[Provider(typeof(BeatmapDataModel))]
	private ObjectProvider _beatmapDataModelProvider;

	[SerializeField]
	private DoubleVariable _songStartDSPTimeOffset;

	[Space]
	[SerializeField]
	private float _aheadTime;

	[Tooltip("If notes are too close, we use different sound effect with shorter tail.")]
	[SerializeField]
	private float _maxTimeForCloseNotes = 0.5f;

	[Space]
	[SerializeField]
	private NoteCutSoundEffect _noteCutSoundEffectPrefab;

	[Space]
	[SerializeField]
	private float _type1CutEffectsVolume = 1f;

	[SerializeField]
	private AudioClip[] _leftType1CutEffectsAudioClips;

	[SerializeField]
	private AudioClip[] _rightType1CutEffectsAudioClips;

	[SerializeField]
	private float _type2CutEffectsVolume = 1f;

	[SerializeField]
	private AudioClip[] _leftType2CutEffectsAudioClips;

	[SerializeField]
	private AudioClip[] _rightType2CutEffectsAudioClips;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private PlayerController _playerController;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private BeatmapDataModel _beatmapDataModel;

	private int _beatmapObjectCallbackId;

	private RandomObjectPicker<AudioClip> _leftType1RandomSoundPicker;

	private RandomObjectPicker<AudioClip> _rightType1RandomSoundPicker;

	private RandomObjectPicker<AudioClip> _leftType2RandomSoundPicker;

	private RandomObjectPicker<AudioClip> _rightType2RandomSoundPicker;

	private float _prevNoteATime = -1f;

	private float _prevNoteBTime = -1f;

	private List<NoteCutSoundEffect> _activeCutSoundEffects;

	public bool handleWrongSaberTypeAsGood { get; set; }

	private void Awake()
	{
		_leftType1RandomSoundPicker = new RandomObjectPicker<AudioClip>(_leftType1CutEffectsAudioClips, 0f);
		_rightType1RandomSoundPicker = new RandomObjectPicker<AudioClip>(_rightType1CutEffectsAudioClips, 0f);
		_leftType2RandomSoundPicker = new RandomObjectPicker<AudioClip>(_leftType2CutEffectsAudioClips, 0f);
		_rightType2RandomSoundPicker = new RandomObjectPicker<AudioClip>(_rightType2CutEffectsAudioClips, 0f);
		_noteCutSoundEffectPrefab.CreatePool(20, SetCutSoundEffectEventCallbacks);
		_activeCutSoundEffects = new List<NoteCutSoundEffect>(20);
	}

	private void Start()
	{
		_beatmapObjectCallbackController = _beatmapObjectCallbackControllerProvider.GetProvidedObject<BeatmapObjectCallbackController>();
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_beatmapObjectCallbackId = _beatmapObjectCallbackController.AddBeatmapObjectCallback(BeatmapObjectCallback, _aheadTime + 0.5f);
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCut;
		_beatmapDataModel = _beatmapDataModelProvider.GetProvidedObject<BeatmapDataModel>();
	}

	private void SetCutSoundEffectEventCallbacks(NoteCutSoundEffect cutSoundEffect)
	{
		cutSoundEffect.didFinishEvent += HandleCutSoundEffectDidFinishEvent;
	}

	private void BeatmapObjectCallback(BeatmapObjectData beatmapObjectData)
	{
		if (beatmapObjectData.beatmapObjectType != 0)
		{
			return;
		}
		NoteData noteData = (NoteData)beatmapObjectData;
		if ((noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB) && (noteData.noteType != 0 || !(noteData.time < _prevNoteATime + 0.001f)) && (noteData.noteType != NoteType.NoteB || !(noteData.time < _prevNoteBTime + 0.001f)))
		{
			bool wasInstantiated = false;
			NoteCutSoundEffect noteCutSoundEffect = _noteCutSoundEffectPrefab.Spawn(base.transform.localPosition, Quaternion.identity, out wasInstantiated);
			if (wasInstantiated)
			{
				SetCutSoundEffectEventCallbacks(noteCutSoundEffect);
			}
			bool flag = _beatmapDataModel.IsNoteInTimeInterval(noteData.time + 0.01f, _maxTimeForCloseNotes);
			float volumeScale = ((!flag) ? _type2CutEffectsVolume : _type1CutEffectsVolume);
			if (noteData.noteType == NoteType.NoteA)
			{
				noteCutSoundEffect.Init((!flag) ? _leftType2RandomSoundPicker.PickRandomObject() : _leftType1RandomSoundPicker.PickRandomObject(), volumeScale, (double)beatmapObjectData.time + _songStartDSPTimeOffset.value, _aheadTime, _beatmapObjectSpawnController.missedTimeOffset, _playerController.leftSaber, noteData, handleWrongSaberTypeAsGood);
				_prevNoteATime = noteData.time;
				_activeCutSoundEffects.Add(noteCutSoundEffect);
			}
			else if (noteData.noteType == NoteType.NoteB)
			{
				noteCutSoundEffect.Init((!flag) ? _rightType2RandomSoundPicker.PickRandomObject() : _rightType1RandomSoundPicker.PickRandomObject(), volumeScale, (double)beatmapObjectData.time + _songStartDSPTimeOffset.value, _aheadTime, _beatmapObjectSpawnController.missedTimeOffset, _playerController.rightSaber, noteData, handleWrongSaberTypeAsGood);
				_prevNoteBTime = noteData.time;
				_activeCutSoundEffects.Add(noteCutSoundEffect);
			}
		}
	}

	private void HandleNoteWasCut(BeatmapObjectSpawnController beatmapObjectSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		for (int i = 0; i < _activeCutSoundEffects.Count; i++)
		{
			_activeCutSoundEffects[i].NoteWasCut(noteController, noteCutInfo);
		}
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectCallbackController)
		{
			_beatmapObjectCallbackController.RemoveBeatmapObjectCallback(_beatmapObjectCallbackId);
		}
		if ((bool)_beatmapObjectSpawnController)
		{
			_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCut;
		}
	}

	private void HandleCutSoundEffectDidFinishEvent(NoteCutSoundEffect cutSoundEffect)
	{
		_activeCutSoundEffects.Remove(cutSoundEffect);
		cutSoundEffect.Recycle();
	}
}
