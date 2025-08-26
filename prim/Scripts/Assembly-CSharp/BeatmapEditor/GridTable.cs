using UnityEngine;

namespace BeatmapEditor;

public abstract class GridTable : BeatmapEditorTable
{
	protected abstract int numberOfColumns { get; }

	protected abstract float columnWidth { get; }

	public Vector2Int GridPosForWorldPos(Vector3 worldPos)
	{
		float x = base.transform.InverseTransformPoint(worldPos).x;
		Vector2Int result = default(Vector2Int);
		result.x = Mathf.FloorToInt(x / columnWidth);
		result.y = _beatmapEditorScrollView.GetRowForWorldPos(worldPos);
		return result;
	}

	public Vector2Int GridPosForLocalPos(Vector3 localPos)
	{
		Vector2Int result = default(Vector2Int);
		result.x = Mathf.FloorToInt(localPos.x / columnWidth);
		result.y = _beatmapEditorScrollView.GetRowForWorldPos(base.transform.TransformPoint(localPos));
		return result;
	}

	public Vector2 GridCellBottomLeftPosforWorldPos(Vector3 worldPos)
	{
		Vector2Int vector2Int = GridPosForWorldPos(worldPos);
		float x = (float)vector2Int.x * columnWidth;
		float y = base.transform.InverseTransformPoint(new Vector2(0f, _beatmapEditorScrollView.GetRowWorldPos(vector2Int.y))).y;
		return new Vector2(x, y);
	}
}
