using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor;

public class NotesTableController : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IEventSystemHandler
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[Space]
	[SerializeField]
	private EditorSelectedNoteCutDirectionSO _selectedNoteCutDirection;

	[SerializeField]
	private EditorSelectedNoteTypeSO _selectedNoteType;

	[Space]
	[SerializeField]
	private NoteLineLayer _noteLineLayer;

	[SerializeField]
	private NotesTable _notesTable;

	private Vector2Int _prevGridPos;

	private Camera _canvasCamera;

	private void Awake()
	{
		_notesTable.Init(_editorBeatmap, _noteLineLayer);
		_canvasCamera = base.transform.root.GetComponentInChildren<Canvas>().worldCamera;
	}

	public void OnPointerClick(PointerEventData pointerEventData)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, pointerEventData.position, pointerEventData.enterEventCamera, out var localPoint);
		Vector2Int vector2Int = _notesTable.GridPosForLocalPos(localPoint);
		if (pointerEventData.button == PointerEventData.InputButton.Left)
		{
			NoteCutDirection cutDirection = ((_noteLineLayer != NoteLineLayer.Top) ? _selectedNoteCutDirection.value : NoteCutDirection.Any);
			_editorBeatmap.AddNote(vector2Int.y, _noteLineLayer, vector2Int.x, new EditorNoteData(_selectedNoteType.value, cutDirection));
			_notesTable.UpdateAllCells();
		}
	}

	public void OnPointerDown(PointerEventData pointerEventData)
	{
		if (pointerEventData.button == PointerEventData.InputButton.Right)
		{
			_prevGridPos = new Vector2Int(-1, -1);
			StartCoroutine(ErasingCoroutine());
		}
	}

	private IEnumerator ErasingCoroutine()
	{
		while (Input.GetMouseButton(1))
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, Input.mousePosition, _canvasCamera, out var localPos);
			Vector2Int gridPos = _notesTable.GridPosForLocalPos(localPos);
			if (_prevGridPos != gridPos)
			{
				_editorBeatmap.EraseNote(gridPos.y, _noteLineLayer, gridPos.x);
				_prevGridPos = gridPos;
				_notesTable.UpdateAllCells();
			}
			yield return null;
		}
	}
}
