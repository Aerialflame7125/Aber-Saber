using UnityEngine;
using UnityEngine.EventSystems;

namespace BeatmapEditor;

public class ObstaclesTableController : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[Space]
	[SerializeField]
	private IBeatmapEditorObstacleLengthContainer _obstacleLength;

	[SerializeField]
	private IBeatmapEditorObstacleTypeContainer _obstacleType;

	[Space]
	[SerializeField]
	private ObstaclesTable _obstaclesTable;

	private void Awake()
	{
		_obstaclesTable.Init(_editorBeatmap);
	}

	public void OnPointerClick(PointerEventData pointerEventData)
	{
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, pointerEventData.position, pointerEventData.enterEventCamera, out var localPoint);
		Vector2Int vector2Int = _obstaclesTable.GridPosForLocalPos(localPoint);
		if (pointerEventData.button == PointerEventData.InputButton.Left)
		{
			_editorBeatmap.AddObstacle(vector2Int.y, vector2Int.x, _obstacleLength.Result.obstacleLength, _obstacleType.Result.obstacleType);
			_obstaclesTable.UpdateAllCells();
		}
		else if (pointerEventData.button == PointerEventData.InputButton.Right)
		{
			_editorBeatmap.EraseObstacle(vector2Int.y, vector2Int.x);
			_obstaclesTable.UpdateAllCells();
		}
	}
}
