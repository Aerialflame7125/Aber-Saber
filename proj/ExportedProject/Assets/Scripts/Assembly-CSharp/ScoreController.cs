using System;
using System.Collections.Generic;
using UnityEngine;
using BL.Replays;

public class ScoreController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	[SerializeField]
	private FloatVariable _songTime;

	[Space]
	public const int kMaxMultiplier = 8;

	private const int kMaxBeforeCutSwingScore = 70;

	private const int kMaxCutDistanceScore = 10;

	private const int kMaxAfterCutSwingScore = 30;

	private const int kMaxCutScore = 115;

	private const float kSwingScorePart = 0.7f;

	private const float kDistanceToCenterScorePart = 0.3f;

	private const float kMaxDistanceForDistanceToCenterScore = 0.2f;

	private int _baseScore;

	private int _prevFrameScore;

	private int _multiplier;

	private int _multiplierIncreaseProgress;

	private int _multiplierIncreaseMaxProgress;

	private int _combo;

	private int _maxCombo;

	private bool _playerHeadWasInObstacle;

	private List<AfterCutScoreBuffer> _afterCutScoreBuffers = new List<AfterCutScoreBuffer>();

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	public int prevFrameScore
	{
		get
		{
			return _prevFrameScore;
		}
	}

	public int maxCombo
	{
		get
		{
			return _maxCombo;
		}
	}

	public event Action<NoteData, NoteCutInfo, int> noteWasCutEvent;

	public event Action<NoteData, int> noteWasMissedEvent;

	public event Action<int> scoreDidChangeEvent;

	public event Action<int, float> multiplierDidChangeEvent;

	public event Action<int> comboDidChangeEvent;

	public static int MaxScoreForNumberOfNotes(int noteCount)
	{
		int num = 0;
		int num2 = 1;
		while (num2 < 8)
		{
			if (noteCount >= num2 * 2)
			{
				num += num2 * num2 * 2 + num2;
				noteCount -= num2 * 2;
				num2 *= 2;
				continue;
			}
			num += num2 * noteCount;
			noteCount = 0;
			break;
		}
		num += noteCount * num2;
		return num * 115;
	}

	public static void ScoreWithoutMultiplier(NoteCutInfo noteCutInfo, SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter, out int beforeCutScore, out int afterCutScore)
	{
		beforeCutScore = Mathf.RoundToInt(70f * noteCutInfo.swingRating);
		float num = 1f - Mathf.Clamp01(noteCutInfo.cutDistanceToCenter / 0.2f);
		beforeCutScore += Mathf.RoundToInt(10f * num);
		afterCutScore = 0;
		if (saberAfterCutSwingRatingCounter != null)
		{
			afterCutScore = Mathf.RoundToInt(30f * saberAfterCutSwingRatingCounter.rating);
		}
	}

	private void Start()
	{
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCutEvent;
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissedEvent;
		_multiplier = 1;
		_multiplierIncreaseProgress = 0;
		_multiplierIncreaseMaxProgress = 2;
	}

	private void OnDestroy()
	{
		if (_beatmapObjectSpawnController != null)
		{
			_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCutEvent;
			_beatmapObjectSpawnController.noteWasMissedEvent -= HandleNoteWasMissedEvent;
		}
	}

	private void Update()
	{
		bool comboChanged = false;
		bool multiplierChanged = false;
		if (_playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0)
		{
			if (!_playerHeadWasInObstacle)
			{
				LoseMultiplier(out comboChanged, out multiplierChanged);
			}
			_playerHeadWasInObstacle = true;
		}
		else
		{
			_playerHeadWasInObstacle = false;
		}
		NotifyForChange(comboChanged, multiplierChanged);
	}

	private void LateUpdate()
	{
		int num = _baseScore;
		int count = _afterCutScoreBuffers.Count;
		for (int i = 0; i < count; i++)
		{
			num += _afterCutScoreBuffers[i].scoreWithMultiplier;
		}
		if (num != _prevFrameScore && this.scoreDidChangeEvent != null)
		{
			this.scoreDidChangeEvent(num);
		}
		_prevFrameScore = num;
	}

	private void LoseMultiplier(out bool comboChanged, out bool multiplierChanged)
	{
		comboChanged = false;
		multiplierChanged = false;
		if (_combo > 0)
		{
			_combo = 0;
			comboChanged = true;
		}
		if (_multiplierIncreaseProgress > 0)
		{
			_multiplierIncreaseProgress = 0;
			multiplierChanged = true;
		}
		if (_multiplier > 1)
		{
			_multiplier /= 2;
			_multiplierIncreaseMaxProgress = _multiplier * 2;
			multiplierChanged = true;
		}
	}

	private void NotifyForChange(bool comboChanged, bool multiplierChanged)
	{
		if (multiplierChanged && this.multiplierDidChangeEvent != null)
		{
			this.multiplierDidChangeEvent(_multiplier, (float)_multiplierIncreaseProgress / (float)_multiplierIncreaseMaxProgress);
		}
		if (comboChanged && this.comboDidChangeEvent != null)
		{
			this.comboDidChangeEvent(_combo);
		}
	}

	private void HandleNoteWasCutEvent(BeatmapObjectSpawnController noteSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		bool multiplierChanged = false;
		bool comboChanged = false;
		if (noteCutInfo.allIsOK)
		{
			_combo++;
			if (_maxCombo < _combo)
			{
				_maxCombo = _combo;
			}
			comboChanged = true;
			if (_multiplier < 8)
			{
				if (_multiplierIncreaseProgress < _multiplierIncreaseMaxProgress)
				{
					_multiplierIncreaseProgress++;
					multiplierChanged = true;
				}
				if (_multiplierIncreaseProgress >= _multiplierIncreaseMaxProgress)
				{
					_multiplier *= 2;
					_multiplierIncreaseProgress = 0;
					_multiplierIncreaseMaxProgress = _multiplier * 2;
					multiplierChanged = true;
				}
			}
			int beforeCutScore;
			int afterCutScore;
			ScoreWithoutMultiplier(noteCutInfo, null, out beforeCutScore, out afterCutScore);
			_baseScore += beforeCutScore;
			AfterCutScoreBuffer afterCutScoreBuffer = new AfterCutScoreBuffer(noteCutInfo, noteCutInfo.afterCutSwingRatingCounter);
			afterCutScoreBuffer.didFinishEvent = (Action<AfterCutScoreBuffer>)Delegate.Combine(afterCutScoreBuffer.didFinishEvent, new Action<AfterCutScoreBuffer>(HandleAfterCutScoreBufferDidFinishEvent));
			_afterCutScoreBuffers.Add(afterCutScoreBuffer);

			if (VRControllersRecorder.Instance != null)
			{
				VRControllersRecorder.Instance.RecordNoteEvent(noteController.noteData, noteCutInfo, Time.time);
			}
		}
		else
		{
			LoseMultiplier(out comboChanged, out multiplierChanged);
			if (VRControllersRecorder.Instance != null)
            {
				VRControllersRecorder.Instance.RecordNoteEvent(noteController.noteData, noteCutInfo, Time.time);
			}
		}
		NotifyForChange(comboChanged, multiplierChanged);
		if (this.noteWasCutEvent != null)
		{
			this.noteWasCutEvent(noteController.noteData, noteCutInfo, _multiplier);
		}
	}

	private void HandleNoteWasMissedEvent(BeatmapObjectSpawnController noteSpawnController, NoteController noteController)
	{
		if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			bool comboChanged = false;
			bool multiplierChanged = false;
			LoseMultiplier(out comboChanged, out multiplierChanged);
			NotifyForChange(comboChanged, multiplierChanged);

			if (VRControllersRecorder.Instance != null)
            {
				VRControllersRecorder.Instance.RecordNoteEvent(noteController.noteData, null, Time.time);
			}
		}
		if (this.noteWasMissedEvent != null)
		{
			this.noteWasMissedEvent(noteController.noteData, _multiplier);
		}
	}

	private void HandleAfterCutScoreBufferDidFinishEvent(AfterCutScoreBuffer afterCutScoreBuffer)
	{
		_baseScore += afterCutScoreBuffer.scoreWithMultiplier;
		_afterCutScoreBuffers.Remove(afterCutScoreBuffer);
	}
}
