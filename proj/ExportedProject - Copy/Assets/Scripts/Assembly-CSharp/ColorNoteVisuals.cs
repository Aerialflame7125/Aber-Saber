using UnityEngine;

public class ColorNoteVisuals : MonoBehaviour
{
	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private NoteController _noteController;

	[SerializeField]
	private SpriteRenderer _arrowGlowSpriteRenderer;

	[SerializeField]
	private SpriteRenderer _circleGlowSpriteRenderer;

	[SerializeField]
	private MaterialPropertyBlockController _materialPropertyBlockController;

	[SerializeField]
	private MeshRenderer _arrowMeshRenderer;

	private int _colorID;

	private bool showArrow
	{
		set
		{
			_arrowMeshRenderer.enabled = value;
			_arrowGlowSpriteRenderer.enabled = value;
		}
	}

	private bool showCircle
	{
		set
		{
			_circleGlowSpriteRenderer.enabled = value;
		}
	}

	private void Awake()
	{
		_colorID = Shader.PropertyToID("_Color");
		_noteController.didInitEvent += HandleNoteControllerDidInitEvent;
		_noteController.noteDidPassJumpThreeQuartersEvent += HandleNoteControllerNoteDidPassJumpThreeQuartersEvent;
		_noteController.noteDidStartDissolvingEvent += HandleNoteDidStartDissolvingEvent;
	}

	private void OnDestroy()
	{
		if ((bool)_noteController)
		{
			_noteController.didInitEvent -= HandleNoteControllerDidInitEvent;
			_noteController.noteDidPassJumpThreeQuartersEvent -= HandleNoteControllerNoteDidPassJumpThreeQuartersEvent;
		}
	}

	public void HandleNoteControllerDidInitEvent(NoteController noteController)
	{
		NoteData noteData = noteController.noteData;
		NoteType noteType = noteData.noteType;
		if (noteData.cutDirection == NoteCutDirection.Any)
		{
			showArrow = false;
			showCircle = true;
		}
		else
		{
			showArrow = true;
			showCircle = false;
		}
		Color color = _colorManager.ColorForNoteType(noteType);
		_arrowGlowSpriteRenderer.color = color.ColorWithAlpha(0.3f);
		_circleGlowSpriteRenderer.color = color;
		MaterialPropertyBlock materialPropertyBlock = _materialPropertyBlockController.materialPropertyBlock;
		materialPropertyBlock.SetColor(_colorID, color.ColorWithAlpha(1f));
		_materialPropertyBlockController.ApplyChanges();
	}

	private bool NoteIsOnDifferentSide(NoteData noteData)
	{
		if ((noteData.noteType == NoteType.NoteA && noteData.lineIndex > 1) || (noteData.noteType == NoteType.NoteB && noteData.lineIndex < 2))
		{
			return true;
		}
		return false;
	}

	private void HandleNoteControllerNoteDidPassJumpThreeQuartersEvent(NoteController noteController)
	{
		showArrow = false;
		showCircle = false;
	}

	private void HandleNoteDidStartDissolvingEvent(NoteController noteController, float duration)
	{
		showArrow = false;
		showCircle = false;
	}
}
