using System;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
	public enum MovementPhase
	{
		undefined,
		movingOnTheFloor,
		jumping
	}

	[SerializeField]
	private NoteFloorMovement _floorMovement;

	[SerializeField]
	private NoteJump _jump;

	[Space]
	[SerializeField]
	private float _zOffset;

	public MovementPhase movementPhase { get; private set; }

	public float jumpDuration => _jump.jumpDuration;

	public float jumpStartTime => _jump.startTime;

	public event Action noteDidStartJumpEvent;

	public event Action noteDidFinishJumpEvent;

	public event Action noteDidPassMissedMarkerEvent;

	public event Action<NoteMovement> noteDidPassJumpThreeQuartersEvent;

	private void Awake()
	{
		movementPhase = MovementPhase.undefined;
		_floorMovement.floorMovementDidFinishEvent += HandleFloorMovementDidFinishEvent;
		_jump.noteJumpDidFinishEvent += HandleNoteJumpDidFinishEvent;
		_jump.noteJumpDidPassMissedMarkerEvent += HandleNoteJumpDidPassMissedMarkEvent;
		_jump.noteJumpDidPassThreeQuartersEvent += HandleNoteJumpDidPassThreeQuartersEvent;
	}

	private void OnDestroy()
	{
		_floorMovement.floorMovementDidFinishEvent -= HandleFloorMovementDidFinishEvent;
		_jump.noteJumpDidFinishEvent -= HandleNoteJumpDidFinishEvent;
		_jump.noteJumpDidPassMissedMarkerEvent -= HandleNoteJumpDidPassMissedMarkEvent;
		_jump.noteJumpDidPassThreeQuartersEvent -= HandleNoteJumpDidPassThreeQuartersEvent;
	}

	private void HandleFloorMovementDidFinishEvent()
	{
		movementPhase = MovementPhase.jumping;
		_jump.ManualUpdate();
		if (this.noteDidStartJumpEvent != null)
		{
			this.noteDidStartJumpEvent();
		}
	}

	private void HandleNoteJumpDidFinishEvent()
	{
		movementPhase = MovementPhase.undefined;
		if (this.noteDidFinishJumpEvent != null)
		{
			this.noteDidFinishJumpEvent();
		}
	}

	private void HandleNoteJumpDidPassMissedMarkEvent()
	{
		if (this.noteDidPassMissedMarkerEvent != null)
		{
			this.noteDidPassMissedMarkerEvent();
		}
	}

	private void HandleNoteJumpDidPassThreeQuartersEvent(NoteJump noteJump)
	{
		if (this.noteDidPassJumpThreeQuartersEvent != null)
		{
			this.noteDidPassJumpThreeQuartersEvent(this);
		}
	}

	private void Update()
	{
		if (movementPhase == MovementPhase.movingOnTheFloor)
		{
			_floorMovement.ManualUpdate();
		}
		else
		{
			_jump.ManualUpdate();
		}
	}

	public void Init(Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float startTime, float jumpGravity, float flipYSide, NoteCutDirection cutDirection)
	{
		moveStartPos.z += _zOffset;
		moveEndPos.z += _zOffset;
		jumpEndPos.z += _zOffset;
		_floorMovement.Init(moveStartPos, moveEndPos, moveDuration, startTime);
		_floorMovement.SetToStart();
		_jump.Init(moveEndPos, jumpEndPos, jumpDuration, startTime + moveDuration, jumpGravity, flipYSide, cutDirection);
		movementPhase = MovementPhase.movingOnTheFloor;
	}
}
