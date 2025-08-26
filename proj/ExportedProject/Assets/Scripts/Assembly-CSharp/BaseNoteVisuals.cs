using UnityEngine;

public class BaseNoteVisuals : MonoBehaviour
{
	[SerializeField]
	private NoteController _noteController;

	[Space]
	[SerializeField]
	private CutoutAnimateEffect _cutoutAnimateEffect;

	[SerializeField]
	private ParticleSystem _trailPS;

	private const float kStartEmitingTrailParticlesZPos = 30f;

	private bool _trailPSEmitting;

	private bool trailPSEnabled
	{
		get
		{
			return _trailPSEmitting;
		}
		set
		{
			ParticleSystem.EmissionModule emission = _trailPS.emission;
			emission.enabled = value;
			_trailPSEmitting = value;
		}
	}

	private void Awake()
	{
		_noteController.didInitEvent += HandleNoteControllerDidInitEvent;
		_noteController.noteDidStartJumpEvent += HandleNoteControllerNoteDidStartJumpEvent;
		_noteController.noteDidPassJumpThreeQuartersEvent += HandleNoteControllerNoteDidPassJumpThreeQuartersEvent;
		_noteController.noteDidStartDissolvingEvent += HandleNoteDidStartDissolvingEvent;
		trailPSEnabled = false;
	}

	private void OnDestroy()
	{
		if ((bool)_noteController)
		{
			_noteController.didInitEvent -= HandleNoteControllerDidInitEvent;
			_noteController.noteDidStartJumpEvent -= HandleNoteControllerNoteDidStartJumpEvent;
			_noteController.noteDidPassJumpThreeQuartersEvent -= HandleNoteControllerNoteDidPassJumpThreeQuartersEvent;
		}
	}

	private void Update()
	{
		float z = base.transform.localPosition.z;
		if (_noteController.movementPhase == NoteMovement.MovementPhase.movingOnTheFloor && !trailPSEnabled && z < 30f)
		{
			trailPSEnabled = true;
		}
	}

	public void HandleNoteControllerDidInitEvent(NoteController noteController)
	{
		NoteType noteType = noteController.noteData.noteType;
		_cutoutAnimateEffect.ResetEffect();
		ParticleSystem.MainModule main = _trailPS.main;
		main.startColor = Color.white;
	}

	private void HandleNoteControllerNoteDidStartJumpEvent(NoteController noteController)
	{
		trailPSEnabled = false;
	}

	private void HandleNoteControllerNoteDidPassJumpThreeQuartersEvent(NoteController noteController)
	{
		AnimateCutout(0f, 1f, noteController.jumpDuration * 0.25f);
	}

	private void HandleNoteDidStartDissolvingEvent(NoteController noteController, float duration)
	{
		trailPSEnabled = false;
		AnimateCutout(0f, 1f, duration);
	}

	private void AnimateCutout(float cutoutStart, float cutoutEnd, float duration)
	{
		if (!_cutoutAnimateEffect.animating)
		{
			_cutoutAnimateEffect.AnimateCutout(cutoutStart, cutoutEnd, duration);
		}
	}
}
