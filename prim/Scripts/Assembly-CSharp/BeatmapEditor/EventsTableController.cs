using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor;

public class EventsTableController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private EventSetDrawStyleSO _eventSetDrawStyle;

	[Space]
	[SerializeField]
	private IBeatmapEditorEventTypeContainer _eventType;

	[Space]
	[SerializeField]
	private EventsTable _eventsTable;

	private void Awake()
	{
		_eventsTable.Init(_editorBeatmap, _eventSetDrawStyle);
	}

	public void OnPointerClick(PointerEventData pointerEventData)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, pointerEventData.position, pointerEventData.enterEventCamera, out var localPoint);
		Vector2Int vector2Int = _eventsTable.GridPosForLocalPos(localPoint);
		if (pointerEventData.button == PointerEventData.InputButton.Left)
		{
			if (_eventSetDrawStyle.events[vector2Int.x] != null)
			{
				EventDrawStyleSO.SubEventDrawStyle selectedEventType = _eventType.Result.GetSelectedEventType(_eventSetDrawStyle.events[vector2Int.x].eventId);
				EditorEventData eventData = new EditorEventData(selectedEventType.eventValue, isPreviousValidValue: false);
				_editorBeatmap.AddEvent(vector2Int.y, vector2Int.x, eventData);
				_eventsTable.UpdateAllCells();
			}
		}
		else if (pointerEventData.button == PointerEventData.InputButton.Right)
		{
			_editorBeatmap.EraseEvent(vector2Int.y, vector2Int.x);
			_eventsTable.UpdateAllCells();
		}
	}
}
