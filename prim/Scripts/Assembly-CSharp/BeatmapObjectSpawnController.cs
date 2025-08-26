using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapObjectSpawnController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[Header("Prefabs")]
	[SerializeField]
	private NoteController _gameNotePrefab;

	[SerializeField]
	private GhostNoteController _ghostNotePrefab;

	[SerializeField]
	private BombNoteController _bombNotePrefab;

	[SerializeField]
	private ObstacleController _obstacleTopPrefab;

	[SerializeField]
	private ObstacleController _obstacleFullHeightPrefab;

	[Header("Global")]
	[SerializeField]
	private float _noteLinesDistance;

	[Header("Jump")]
	public float _globalYJumpOffset;

	[Tooltip("If half jump distance computed using halfJumpDurationInBeats is longer than this value, it is devided by two until it's smaller.")]
	[SerializeField]
	private float _maxHalfJumpDistance = 18f;

	[SerializeField]
	private float _halfJumpDurationInBeats = 1f;

	[SerializeField]
	private float _baseLinesHighestJumpPosY;

	[SerializeField]
	private float _upperLinesHighestJumpPosY;

	[SerializeField]
	private float _topLinesHighestJumpPosY;

	[SerializeField]
	private float _topLinesZPosOffset = -0.2f;

	[Header("Arrival")]
	[SerializeField]
	private float _moveSpeed = 1f;

	[SerializeField]
	private float _moveDurationInBeats = 1f;

	[SerializeField]
	private float _baseLinesYPos;

	[SerializeField]
	private float _upperLinesYPos;

	[SerializeField]
	private float _topLinesYPos;

	[Header("Obstacles")]
	[SerializeField]
	private float _verticalObstaclePosY;

	[SerializeField]
	private float _topObstaclePosY;

	private float _spawnAheadTime;

	private float _jumpDistance;

	private float _moveDistance;

	private bool _disableSpawning;

	private int _noteSpawnCallbackId;

	private float _beatsPerMinute = 120f;

	private float _noteLinesCount = 4f;

	private float _noteJumpMovementSpeed;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private const float kReferencePlayerHeight = 1.8f;

	public float missedTimeOffset => 0.15f;

	public event Action<BeatmapObjectSpawnController, NoteController> noteWasMissedEvent;

	public event Action<BeatmapObjectSpawnController, NoteController, NoteCutInfo> noteWasCutEvent;

	public event Action<BeatmapObjectSpawnController, NoteController> noteDidStartJumpEvent;

	public event Action<BeatmapObjectSpawnController, ObstacleController> obstacleDiStartMovementEvent;

	public event Action<BeatmapObjectSpawnController, ObstacleController> obstacleDidFinishMovementEvent;

	public event Action<BeatmapObjectSpawnController, ObstacleController> obstacleDidPassThreeQuartersOfMove2Event;

	public event Action<BeatmapObjectSpawnController, ObstacleController> obstacleDidPassAvoidedMarkEvent;

	public void Init(float beatsPerMinute, int noteLinesCount, float noteJumpMovementSpeed)
	{
		_beatsPerMinute = beatsPerMinute;
		_noteLinesCount = noteLinesCount;
		_noteJumpMovementSpeed = noteJumpMovementSpeed;
		AdjustForBeatsPerMinute();
	}

	private void Start()
	{
		_gameNotePrefab.CreatePool(30, SetNoteControllerEventCallbacks);
		_ghostNotePrefab.CreatePool(20, SetNoteControllerEventCallbacks);
		_bombNotePrefab.CreatePool(20, SetNoteControllerEventCallbacks);
		_obstacleTopPrefab.CreatePool(4, SetObstacleEventCallbacks);
		_obstacleFullHeightPrefab.CreatePool(6, SetObstacleEventCallbacks);
		_beatmapObjectCallbackController = _beatmapObjectCallbackControllerProvider.GetProvidedObject<BeatmapObjectCallbackController>();
		AdjustForBeatsPerMinute();
	}

	private void AdjustForBeatsPerMinute()
	{
		float num = 60f / _beatsPerMinute;
		_moveDistance = _moveSpeed * num * _moveDurationInBeats;
		while (_noteJumpMovementSpeed * num * _halfJumpDurationInBeats > _maxHalfJumpDistance)
		{
			_halfJumpDurationInBeats /= 2f;
		}
		_jumpDistance = _noteJumpMovementSpeed * num * _halfJumpDurationInBeats * 2f;
		_spawnAheadTime = _moveDistance / _moveSpeed + _jumpDistance * 0.5f / _noteJumpMovementSpeed;
		if (_beatmapObjectCallbackController != null)
		{
			if (_noteSpawnCallbackId != 0)
			{
				_beatmapObjectCallbackController.RemoveBeatmapObjectCallback(_noteSpawnCallbackId);
			}
			_noteSpawnCallbackId = _beatmapObjectCallbackController.AddBeatmapObjectCallback(NoteSpawnCallback, _spawnAheadTime);
		}
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectCallbackController)
		{
			_beatmapObjectCallbackController.RemoveBeatmapObjectCallback(_noteSpawnCallbackId);
		}
	}

	private void SetNoteControllerEventCallbacks(NoteController noteController)
	{
		noteController.noteDidStartJumpEvent += HandleNoteDidStartJumpEvent;
		noteController.noteDidFinishJumpEvent += HandleNoteDidFinishJumpEvent;
		noteController.noteWasCutEvent += HandleNoteWasCutEvent;
		noteController.noteWasMissedEvent += HandleNoteWasMissedEvent;
	}

	private void SetObstacleEventCallbacks(ObstacleController obstacleController)
	{
		obstacleController.finishedMovementEvent += HandleObstacleFinishedMovementEvent;
		obstacleController.passedThreeQuartersOfMove2Event += HandleObstaclePassedThreeQuartersOfMove2;
		obstacleController.passedAvoidedMarkEvent += HandleObstaclePassedAvoidedMark;
	}

	private float HighestJumpPosYForLineLayer(NoteLineLayer lineLayer)
	{
		return lineLayer switch
		{
			NoteLineLayer.Base => _baseLinesHighestJumpPosY + _globalYJumpOffset, 
			NoteLineLayer.Upper => _upperLinesHighestJumpPosY + _globalYJumpOffset, 
			_ => _topLinesHighestJumpPosY + _globalYJumpOffset, 
		};
	}

	private float LineYPosForLineLayer(NoteLineLayer lineLayer)
	{
		return lineLayer switch
		{
			NoteLineLayer.Base => _baseLinesYPos, 
			NoteLineLayer.Upper => _upperLinesYPos, 
			_ => _topLinesYPos, 
		};
	}

	private float JumpGravityForLineLayer(NoteLineLayer lineLayer, NoteLineLayer startLineLayer)
	{
		return 2f * (HighestJumpPosYForLineLayer(lineLayer) - LineYPosForLineLayer(startLineLayer)) / Mathf.Pow(_jumpDistance / _noteJumpMovementSpeed * 0.5f, 2f);
	}

	private void NoteSpawnCallback(BeatmapObjectData beatmapObjectData)
	{
		if (_disableSpawning)
		{
			return;
		}
		float num = _moveDistance / _moveSpeed;
		float num2 = _jumpDistance / _noteJumpMovementSpeed;
		if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
		{
			ObstacleData obstacleData = (ObstacleData)beatmapObjectData;
			Vector3 forward = base.transform.forward;
			Vector3 position = base.transform.position;
			position += forward * (_moveDistance + _jumpDistance * 0.5f);
			Vector3 vector = position - forward * _moveDistance;
			Vector3 vector2 = position - forward * (_moveDistance + _jumpDistance);
			Vector3 noteOffset = GetNoteOffset(beatmapObjectData.lineIndex, NoteLineLayer.Base);
			noteOffset.y = ((obstacleData.obstacleType != ObstacleType.Top) ? _verticalObstaclePosY : (_topObstaclePosY + _globalYJumpOffset));
			bool wasInstantiated = false;
			ObstacleController prefab = ((obstacleData.obstacleType != ObstacleType.Top) ? _obstacleFullHeightPrefab : _obstacleTopPrefab);
			ObstacleController obstacleController = prefab.Spawn(position + noteOffset, Quaternion.identity, out wasInstantiated);
			if (wasInstantiated)
			{
				SetObstacleEventCallbacks(obstacleController);
			}
			obstacleController.Init(obstacleData, position + noteOffset, vector + noteOffset, vector2 + noteOffset, num, num2, beatmapObjectData.time - _spawnAheadTime, _noteLinesDistance);
			if (this.obstacleDiStartMovementEvent != null)
			{
				this.obstacleDiStartMovementEvent(this, obstacleController);
			}
			return;
		}
		NoteData noteData = (NoteData)beatmapObjectData;
		Vector3 forward2 = base.transform.forward;
		Vector3 position2 = base.transform.position;
		position2 += forward2 * (_moveDistance + _jumpDistance * 0.5f);
		Vector3 vector3 = position2 - forward2 * _moveDistance;
		Vector3 vector4 = position2 - forward2 * (_moveDistance + _jumpDistance);
		if (noteData.noteLineLayer == NoteLineLayer.Top)
		{
			vector4 += forward2 * _topLinesZPosOffset * 2f;
		}
		Vector3 noteOffset2 = GetNoteOffset(noteData.lineIndex, noteData.startNoteLineLayer);
		float jumpGravity = JumpGravityForLineLayer(noteData.noteLineLayer, noteData.startNoteLineLayer);
		if (noteData.noteType == NoteType.Bomb)
		{
			bool wasInstantiated2 = false;
			BombNoteController bombNoteController = _bombNotePrefab.Spawn(position2 + noteOffset2, Quaternion.identity, out wasInstantiated2);
			if (wasInstantiated2)
			{
				SetNoteControllerEventCallbacks(bombNoteController);
			}
			bombNoteController.Init(noteData, position2 + noteOffset2, vector3 + noteOffset2, vector4 + noteOffset2, num, num2, noteData.time - _spawnAheadTime, jumpGravity);
		}
		else if (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB)
		{
			Vector3 noteOffset3 = GetNoteOffset(noteData.flipLineIndex, noteData.startNoteLineLayer);
			bool wasInstantiated3 = false;
			NoteController noteController = _gameNotePrefab.Spawn(position2 + noteOffset3, Quaternion.identity, out wasInstantiated3);
			if (wasInstantiated3)
			{
				SetNoteControllerEventCallbacks(noteController);
			}
			noteController.Init(noteData, position2 + noteOffset3, vector3 + noteOffset3, vector4 + noteOffset2, num, num2, noteData.time - _spawnAheadTime, jumpGravity);
		}
	}

	private void HandleGhostNoteDidFinishJumpEvent(GhostNoteController noteController)
	{
		noteController.gameObject.Recycle();
	}

	private void HandleNoteDidStartJumpEvent(NoteController noteController)
	{
		if (this.noteDidStartJumpEvent != null)
		{
			this.noteDidStartJumpEvent(this, noteController);
		}
	}

	private void HandleNoteWasMissedEvent(NoteController noteController)
	{
		if (this.noteWasMissedEvent != null)
		{
			this.noteWasMissedEvent(this, noteController);
		}
	}

	private void HandleNoteDidFinishJumpEvent(NoteController noteController)
	{
		noteController.gameObject.Recycle();
	}

	private void HandleNoteWasCutEvent(NoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (this.noteWasCutEvent != null)
		{
			this.noteWasCutEvent(this, noteController, noteCutInfo);
		}
		noteController.gameObject.Recycle();
	}

	private void HandleObstaclePassedThreeQuartersOfMove2(ObstacleController obstacleController)
	{
		if (this.obstacleDidPassThreeQuartersOfMove2Event != null)
		{
			this.obstacleDidPassThreeQuartersOfMove2Event(this, obstacleController);
		}
	}

	private void HandleObstaclePassedAvoidedMark(ObstacleController obstacleController)
	{
		if (this.obstacleDidPassAvoidedMarkEvent != null)
		{
			this.obstacleDidPassAvoidedMarkEvent(this, obstacleController);
		}
	}

	private void HandleObstacleFinishedMovementEvent(ObstacleController obstacleController)
	{
		if (this.obstacleDidFinishMovementEvent != null)
		{
			this.obstacleDidFinishMovementEvent(this, obstacleController);
		}
		obstacleController.gameObject.Recycle();
	}

	public Vector3 GetNoteOffset(int noteLineIndex, NoteLineLayer noteLineLayer)
	{
		float num = (0f - (_noteLinesCount - 1f)) * 0.5f;
		num = (num + (float)noteLineIndex) * _noteLinesDistance;
		return base.transform.right * num + new Vector3(0f, LineYPosForLineLayer(noteLineLayer), 0f);
	}

	public Vector3 GetNotePosInAheadTime(int noteLineIndex, float aheadTime)
	{
		float num = _moveDistance / _moveSpeed;
		float num2 = _jumpDistance / _noteJumpMovementSpeed / 2f;
		Vector3 forward = base.transform.forward;
		Vector3 vector = base.transform.position + forward * (_moveDistance + _jumpDistance * 0.5f);
		Vector3 vector2 = vector - forward * _moveDistance;
		Vector3 a = vector - forward * (_moveDistance + _jumpDistance * 0.5f);
		Vector3 noteOffset = GetNoteOffset(noteLineIndex, NoteLineLayer.Base);
		if (aheadTime < num2)
		{
			return Vector3.LerpUnclamped(a, vector2, aheadTime / num2) + noteOffset;
		}
		return Vector3.LerpUnclamped(vector2, vector, (aheadTime - num2) / num) + noteOffset;
	}

	public Vector3 NoteHighestJumpPoint(NoteData noteData)
	{
		Vector3 noteOffset = GetNoteOffset(noteData.lineIndex, noteData.noteLineLayer);
		return base.transform.position + new Vector3(noteOffset.x, HighestJumpPosYForLineLayer(noteData.noteLineLayer), noteOffset.z);
	}

	public void StopSpawningAndDissolveAllObjects()
	{
		_disableSpawning = true;
		StartCoroutine(DissolveAllObjectsCoroutine());
	}

	private IEnumerator DissolveAllObjectsCoroutine()
	{
		float duration = 1.4f;
		List<NoteController> cubeNoteControllers = _gameNotePrefab.GetSpawned();
		foreach (NoteController item in cubeNoteControllers)
		{
			item.Dissolve(duration);
		}
		List<BombNoteController> bombNoteControllers = _bombNotePrefab.GetSpawned();
		foreach (BombNoteController item2 in bombNoteControllers)
		{
			item2.Dissolve(duration);
		}
		List<ObstacleController> obstacleFullHeightControllers = _obstacleFullHeightPrefab.GetSpawned();
		foreach (ObstacleController item3 in obstacleFullHeightControllers)
		{
			item3.Dissolve(duration);
		}
		List<ObstacleController> obstacleTopControllers = _obstacleTopPrefab.GetSpawned();
		foreach (ObstacleController item4 in obstacleTopControllers)
		{
			item4.Dissolve(duration);
		}
		yield return new WaitForSeconds(duration + 0.1f);
		foreach (NoteController item5 in cubeNoteControllers)
		{
			item5.gameObject.Recycle();
		}
		foreach (BombNoteController item6 in bombNoteControllers)
		{
			item6.gameObject.Recycle();
		}
		foreach (ObstacleController item7 in obstacleFullHeightControllers)
		{
			item7.gameObject.Recycle();
		}
		foreach (ObstacleController item8 in obstacleTopControllers)
		{
			item8.gameObject.Recycle();
		}
	}
}
