using UnityEngine;

namespace BeatmapEditor;

public class NoteTypeAndCutDirectionKeyboardChanger : MonoBehaviour
{
	[SerializeField]
	private EditorSelectedNoteTypeSO _selectedNoteType;

	[SerializeField]
	private EditorSelectedNoteCutDirectionSO _selectedNoteCutDirection;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad0) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteType.value = NoteType.NoteA;
		}
		else if (Input.GetKeyDown(KeyCode.KeypadPeriod) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteType.value = NoteType.NoteB;
		}
		if (Input.GetKeyDown(KeyCode.Keypad1) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.DownLeft;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.Down;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad3) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.DownRight;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad4) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.Left;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad6) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.Right;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad7) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.UpLeft;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad8) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.Up;
		}
		else if (Input.GetKeyDown(KeyCode.Keypad9) && !EventSystemHelper.IsInputFieldSelected())
		{
			_selectedNoteCutDirection.value = NoteCutDirection.UpRight;
		}
	}
}
