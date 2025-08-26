using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor;

public class MouseClickHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private MouseClickHandlerEvent _clickEvent;

	[SerializeField]
	private PointerEventData.InputButton _mouseButton;

	private Vector3 _prevMousePos;

	private bool _mouseDown;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == _mouseButton && Vector3.Distance(eventData.pressPosition, eventData.position) < 5f)
		{
			_clickEvent.Invoke(_mouseButton);
		}
	}
}
