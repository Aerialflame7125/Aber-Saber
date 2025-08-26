using UnityEngine;

public class GameNoteController : NoteController
{
	[SerializeField]
	private FloatVariable _songTime;

	[Space]
	[SerializeField]
	private CuttableBySaber _bigCuttableBySaber;

	[SerializeField]
	private CuttableBySaber _smallCuttableBySaber;

	[Space]
	[SerializeField]
	private float _minBladeSpeedForCut = 7f;

	public float minBladeSpeedForCut
	{
		get
		{
			return _minBladeSpeedForCut;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		_bigCuttableBySaber.wasCutBySaberEvent += HandleBigWasCutBySaberEvent;
		_smallCuttableBySaber.wasCutBySaberEvent += HandleSmallWasCutBySaberEvent;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_bigCuttableBySaber.wasCutBySaberEvent -= HandleBigWasCutBySaberEvent;
		_smallCuttableBySaber.wasCutBySaberEvent -= HandleSmallWasCutBySaberEvent;
	}

	protected override void NoteDidPassMissedMarker()
	{
		_bigCuttableBySaber.canBeCut = false;
		_smallCuttableBySaber.canBeCut = false;
	}

	private void HandleBigWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		HandleCut(saber, cutPoint, orientation, cutDirVec, false);
	}

	private void HandleSmallWasCutBySaberEvent(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec)
	{
		HandleCut(saber, cutPoint, orientation, cutDirVec, true);
	}

	private void HandleCut(Saber saber, Vector3 cutPoint, Quaternion orientation, Vector3 cutDirVec, bool allowBadCut)
	{
		float timeDeviation = base.noteData.time - _songTime.value;
		bool directionOK;
		bool speedOK;
		bool saberTypeOK;
		float cutDirDeviation;
		GetBasicCutInfo(saber, cutDirVec, _minBladeSpeedForCut, out directionOK, out speedOK, out saberTypeOK, out cutDirDeviation);
		float swingRating = 0f;
		SaberAfterCutSwingRatingCounter afterCutSwingRatingCounter = null;
		if (directionOK && speedOK && saberTypeOK)
		{
			swingRating = saber.ComputeSwingRating();
			afterCutSwingRatingCounter = saber.CreateAfterCutSwingRatingCounter();
		}
		else if (!allowBadCut)
		{
			return;
		}
		Vector3 vector = orientation * Vector3.up;
		float cutDistanceToCenter = Mathf.Abs(new Plane(vector, cutPoint).GetDistanceToPoint(base.noteTransform.position));
		NoteCutInfo noteCutInfo = new NoteCutInfo(speedOK, directionOK, saberTypeOK, false, saber.bladeSpeed, cutDirVec, saber.saberType, swingRating, timeDeviation, cutDirDeviation, cutPoint, vector, afterCutSwingRatingCounter, cutDistanceToCenter);
		SendNoteWasCutEvent(noteCutInfo);
		_bigCuttableBySaber.canBeCut = false;
		_smallCuttableBySaber.canBeCut = false;
	}

	public override void Init(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float startTime, float jumpGravity)
	{
		base.Init(noteData, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, startTime, jumpGravity);
		_bigCuttableBySaber.canBeCut = false;
		_smallCuttableBySaber.canBeCut = false;
	}

	protected override void NoteDidStartJump()
	{
		_bigCuttableBySaber.canBeCut = true;
		_smallCuttableBySaber.canBeCut = true;
	}
}
