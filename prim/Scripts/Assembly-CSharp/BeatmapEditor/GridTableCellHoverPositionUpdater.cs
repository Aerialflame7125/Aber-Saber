using UnityEngine;

namespace BeatmapEditor;

public class GridTableCellHoverPositionUpdater : MonoBehaviour
{
	[SerializeField]
	private GridTable _gridTable;

	private Camera _canvasCamera;

	private void Awake()
	{
		_canvasCamera = base.transform.root.GetComponentInChildren<Canvas>().worldCamera;
	}

	private void LateUpdate()
	{
		RectTransformUtility.ScreenPointToWorldPointInRectangle(base.transform as RectTransform, Input.mousePosition, _canvasCamera, out var worldPoint);
		((RectTransform)base.transform).anchoredPosition = _gridTable.GridCellBottomLeftPosforWorldPos(worldPoint);
	}
}
