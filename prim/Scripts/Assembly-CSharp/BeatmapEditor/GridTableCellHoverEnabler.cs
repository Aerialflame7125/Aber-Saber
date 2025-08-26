using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor;

public class GridTableCellHoverEnabler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	[SerializeField]
	private GridTableCellHoverPositionUpdater _hoverPositionUpdater;

	private void Awake()
	{
		_hoverPositionUpdater.gameObject.SetActive(value: false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_hoverPositionUpdater.gameObject.SetActive(value: true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_hoverPositionUpdater.gameObject.SetActive(value: false);
	}
}
