using UnityEngine;

public class BombNoteController : NoteController
{
	[SerializeField]
	private CuttableBySaber _cuttableBySaber;

	protected override void Awake()
	{
		base.Awake();
		_cuttableBySaber.wasCutBySaberEvent += HandleWasCutBySaberEvent;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_cuttableBySaber.wasCutBySaberEvent -= HandleWasCutBySaberEvent;
	}

	protected override void NoteDidPassMissedMarker()
	{
		_cuttableBySaber.canBeCut = false;
	}

	private void HandleWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		bool directionOK = true;
		bool speedOK = true;
		bool saberTypeOK = false;
		float timeDeviation = 0f;
		float cutDirDeviation = 0f;
		float swingRating = 0f;
		Vector3 cutNormal = orientation * Vector3.up;
		NoteCutInfo noteCutInfo = new NoteCutInfo(speedOK, directionOK, saberTypeOK, false, saber.bladeSpeed, cutDirVec, saber.saberType, swingRating, timeDeviation, cutDirDeviation, cutPoint, cutNormal, null, 0f);
		SendNoteWasCutEvent(noteCutInfo);
	}

	public override void Init(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float startTime, float jumpGravity)
	{
		base.Init(noteData, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, startTime, jumpGravity);
		_cuttableBySaber.canBeCut = true;
	}

	protected override void NoteDidStartDissolving()
	{
		_cuttableBySaber.canBeCut = false;
	}
}
