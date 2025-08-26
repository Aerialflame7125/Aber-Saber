using UnityEngine;

public class TutorialNoteController : NoteController
{
	[SerializeField]
	private CuttableBySaber _cuttableBySaberCore;

	[SerializeField]
	private CuttableBySaber _cuttableBySaberBeforeNote;

	[Space]
	[SerializeField]
	private float _minBladeSpeedForCut = 7f;

	private bool _beforeNoteCutWasOK;

	protected override void Awake()
	{
		base.Awake();
		_cuttableBySaberCore.wasCutBySaberEvent += HandleCoreWasCutBySaberEvent;
		_cuttableBySaberBeforeNote.wasCutBySaberEvent += HandleBeforeNoteWasCutBySaberEvent;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_cuttableBySaberCore.wasCutBySaberEvent -= HandleCoreWasCutBySaberEvent;
		_cuttableBySaberBeforeNote.wasCutBySaberEvent -= HandleBeforeNoteWasCutBySaberEvent;
	}

	protected override void NoteDidPassMissedMarker()
	{
		_cuttableBySaberCore.canBeCut = false;
		_cuttableBySaberBeforeNote.canBeCut = false;
	}

	private void HandleBeforeNoteWasCutBySaberEvent(Saber saber, Vector3 cutCenter, Quaternion orientation, Vector3 cutDirVec)
	{
		if (!_beforeNoteCutWasOK)
		{
			bool directionOK;
			bool speedOK;
			bool saberTypeOK;
			float cutDirDeviation;
			GetBasicCutInfo(saber, cutDirVec, _minBladeSpeedForCut, out directionOK, out speedOK, out saberTypeOK, out cutDirDeviation);
			_beforeNoteCutWasOK = directionOK && speedOK && saberTypeOK;
		}
	}

	private void HandleCoreWasCutBySaberEvent(Saber saber, Vector3 cutCenter, Quaternion orientation, Vector3 cutDirVec)
	{
		float timeDeviation = 0f;
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
		bool wasCutTooSoon = _beforeNoteCutWasOK && saberTypeOK && (!directionOK || !speedOK);
		Vector3 cutNormal = orientation * Vector3.up;
		NoteCutInfo noteCutInfo = new NoteCutInfo(speedOK, directionOK, saberTypeOK, wasCutTooSoon, saber.bladeSpeed, cutDirVec, saber.saberType, swingRating, timeDeviation, cutDirDeviation, cutCenter, cutNormal, afterCutSwingRatingCounter, 0f);
		SendNoteWasCutEvent(noteCutInfo);
	}

	public override void Init(NoteData noteData, Vector3 moveStartPos, Vector3 moveEndPos, Vector3 jumpEndPos, float moveDuration, float jumpDuration, float startTime, float jumpGravity)
	{
		base.Init(noteData, moveStartPos, moveEndPos, jumpEndPos, moveDuration, jumpDuration, startTime, jumpGravity);
		_beforeNoteCutWasOK = false;
		_cuttableBySaberCore.canBeCut = true;
		_cuttableBySaberBeforeNote.canBeCut = true;
	}
}
