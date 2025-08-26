using System;
using System.Collections;
using UnityEngine;

public class NoteController : MonoBehaviour
{
	[SerializeField]
	private NoteMovement _noteMovement;

	[Space]
	[SerializeField]
	private Transform _noteTransform;

	private NoteData _noteData;

	private bool _dissolving;

	private const float kCutAngleTolerance = 60f;

	public NoteData noteData => _noteData;

	public Transform noteTransform => _noteTransform;

	public NoteMovement.MovementPhase movementPhase => _noteMovement.movementPhase;

	public float jumpStartTime => _noteMovement.jumpStartTime;

	public float jumpDuration => _noteMovement.jumpDuration;

	public event Action<NoteController> didInitEvent;

	public event Action<NoteController> noteDidStartJumpEvent;

	public event Action<NoteController> noteDidFinishJumpEvent;

	public event Action<NoteController> noteDidPassJumpThreeQuartersEvent;

	public event Action<NoteController, NoteCutInfo> noteWasCutEvent;

	public event Action<NoteController> noteWasMissedEvent;

	public event Action<NoteController, float> noteDidStartDissolvingEvent;

	public event Action<NoteController> noteDidDissolveEvent;

	protected virtual void Awake()
	{
		_noteMovement.noteDidFinishJumpEvent += HandleNoteDidFinishJumpEvent;
		_noteMovement.noteDidStartJumpEvent += HandleNoteDidStartJumpEvent;
		_noteMovement.noteDidPassJumpThreeQuartersEvent += HandleNoteDidPassJumpThreeQuartersEvent;
		_noteMovement.noteDidPassMissedMarkerEvent += HandleNoteDidPassMissedMarkerEvent;
	}

	protected virtual void OnDestroy()
	{
		_noteMovement.noteDidFinishJumpEvent -= HandleNoteDidFinishJumpEvent;
		_noteMovement.noteDidStartJumpEvent -= HandleNoteDidStartJumpEvent;
		_noteMovement.noteDidPassJumpThreeQuartersEvent -= HandleNoteDidPassJumpThreeQuartersEvent;
		_noteMovement.noteDidPassMissedMarkerEvent -= HandleNoteDidPassMissedMarkerEvent;
	}

	private void HandleNoteDidStartJumpEvent()
	{
		NoteDidStartJump();
		if (this.noteDidStartJumpEvent != null)
		{
			this.noteDidStartJumpEvent(this);
		}
	}

	private void HandleNoteDidFinishJumpEvent()
	{
		if (!_dissolving)
		{
			NoteDidFinishJump();
			if (this.noteDidFinishJumpEvent != null)
			{
				this.noteDidFinishJumpEvent(this);
			}
		}
	}

	private void HandleNoteDidPassJumpThreeQuartersEvent(NoteMovement noteMovement)
	{
		if (!_dissolving)
		{
			NoteDidPassJumpThreeQuarters(noteMovement);
			if (this.noteDidPassJumpThreeQuartersEvent != null)
			{
				this.noteDidPassJumpThreeQuartersEvent(this);
			}
		}
	}

	private void HandleNoteDidPassMissedMarkerEvent()
	{
		if (!_dissolving)
		{
			NoteDidPassMissedMarker();
			if (this.noteWasMissedEvent != null)
			{
				this.noteWasMissedEvent(this);
			}
		}
	}

	protected virtual void NoteDidStartJump()
	{
	}

	protected virtual void NoteDidFinishJump()
	{
	}

	protected virtual void NoteDidPassJumpThreeQuarters(NoteMovement noteMovement)
	{
	}

	protected virtual void NoteDidPassMissedMarker()
	{
	}

	protected virtual void NoteDidStartDissolving()
	{
	}

	protected void SendNoteWasCutEvent(NoteCutInfo noteCutInfo)
	{
		if (this.noteWasCutEvent != null)
		{
			this.noteWasCutEvent(this, noteCutInfo);
		}
	}

	public virtual void Init(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float startTime, float jumpGravity)
	{
		_noteData = noteData;
		_noteMovement.Init(moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, startTime, jumpGravity, _noteData.flipYSide, _noteData.cutDirection);
		if (this.didInitEvent != null)
		{
			this.didInitEvent(this);
		}
	}

	public void GetBasicCutInfo(Saber saber, Vector3 cutDirVec, float minBladeSpeedForCut, out bool directionOK, out bool speedOK, out bool saberTypeOK, out float cutDirDeviation)
	{
		cutDirVec = noteTransform.InverseTransformVector(cutDirVec);
		bool flag = Mathf.Abs(cutDirVec.z) > Mathf.Abs(cutDirVec.x) * 10f && Mathf.Abs(cutDirVec.z) > Mathf.Abs(cutDirVec.y) * 10f;
		float num = Mathf.Atan2(cutDirVec.y, cutDirVec.x) * 57.29578f;
		directionOK = (!flag && num > -150f && num < -30f) || noteData.cutDirection == NoteCutDirection.Any;
		speedOK = saber.bladeSpeed > minBladeSpeedForCut;
		saberTypeOK = (noteData.noteType == NoteType.NoteA && saber.saberType == Saber.SaberType.SaberA) || (noteData.noteType == NoteType.NoteB && saber.saberType == Saber.SaberType.SaberB);
		cutDirDeviation = ((!flag) ? (num + 90f) : 90f);
		if (cutDirDeviation > 180f)
		{
			cutDirDeviation -= 360f;
		}
	}

	private IEnumerator DissolveCoroutine(float duration)
	{
		if (this.noteDidStartDissolvingEvent != null)
		{
			this.noteDidStartDissolvingEvent(this, duration);
		}
		yield return new WaitForSeconds(duration);
		_dissolving = false;
		if (this.noteDidDissolveEvent != null)
		{
			this.noteDidDissolveEvent(this);
		}
	}

	public void Dissolve(float duration)
	{
		if (!_dissolving)
		{
			_dissolving = true;
			NoteDidStartDissolving();
			StartCoroutine(DissolveCoroutine(duration));
		}
	}
}
