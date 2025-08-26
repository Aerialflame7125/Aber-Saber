using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public abstract class BeatmapEditorTable : MonoBehaviour
{
	[SerializeField]
	protected BeatmapEditorScrollView _beatmapEditorScrollView;

	[SerializeField]
	private Transform _cellsContainer;

	private int _prevMinRow = int.MaxValue;

	private int _prevMaxRow = int.MinValue;

	private float _playHeadOffset;

	private Dictionary<int, BeatmapEditorTableCell> _activeCells;

	private Dictionary<string, List<BeatmapEditorTableCell>> _reusableCells;

	private ScrollRect _scrollRect;

	private List<int> _keysToRemove = new List<int>();

	protected virtual void Awake()
	{
		_scrollRect = _beatmapEditorScrollView.scrollRect;
		_playHeadOffset = _beatmapEditorScrollView.playHeadPointsOffset;
		_reusableCells = new Dictionary<string, List<BeatmapEditorTableCell>>();
		_activeCells = new Dictionary<int, BeatmapEditorTableCell>();
	}

	protected virtual void Start()
	{
		UpdateCells(forceUpdate: true);
		_scrollRect.onValueChanged.AddListener(ScrollViewDidScroll);
	}

	protected virtual void OnDestroy()
	{
		_scrollRect.onValueChanged.RemoveListener(ScrollViewDidScroll);
	}

	protected abstract BeatmapEditorTableCell CellForRow(int row);

	public BeatmapEditorTableCell DequeueReusableCell(string identifier)
	{
		if (_reusableCells.TryGetValue(identifier, out var value) && value.Count > 0)
		{
			int index = value.Count - 1;
			BeatmapEditorTableCell result = value[index];
			value.RemoveAt(index);
			return result;
		}
		return null;
	}

	private void ScrollViewDidScroll(Vector2 normalizedPos)
	{
		UpdateCells(forceUpdate: false);
	}

	public void UpdateAllCells()
	{
		UpdateCells(forceUpdate: true);
	}

	private void UpdateCells(bool forceUpdate)
	{
		float rowHeight = _beatmapEditorScrollView.rowHeight;
		float height = _scrollRect.viewport.rect.height;
		int num = Mathf.CeilToInt(height / rowHeight) + 1;
		float num2 = _scrollRect.normalizedPosition.y * (_scrollRect.content.sizeDelta.y - height);
		int num3 = Mathf.Max(Mathf.FloorToInt((num2 - _playHeadOffset) / rowHeight), 0);
		int num4 = num3 + num;
		if (forceUpdate)
		{
			foreach (BeatmapEditorTableCell value3 in _activeCells.Values)
			{
				value3.gameObject.SetActive(value: false);
				if (!_reusableCells.TryGetValue(value3.reuseIdentifier, out var value))
				{
					value = new List<BeatmapEditorTableCell>();
					_reusableCells.Add(value3.reuseIdentifier, value);
				}
				value.Add(value3);
			}
			_activeCells.Clear();
			for (int i = num3; i <= num4; i++)
			{
				BeatmapEditorTableCell beatmapEditorTableCell = CellForRow(i);
				if (beatmapEditorTableCell != null)
				{
					SetupCellForRow(beatmapEditorTableCell, i);
					_activeCells.Add(i, beatmapEditorTableCell);
				}
			}
			_prevMinRow = num3;
			_prevMaxRow = num4;
		}
		else
		{
			if (num3 == _prevMinRow && num4 == _prevMaxRow)
			{
				return;
			}
			_keysToRemove.Clear();
			foreach (int key2 in _activeCells.Keys)
			{
				if (key2 < num3 || key2 > num4)
				{
					_keysToRemove.Add(key2);
				}
			}
			for (int j = 0; j < _keysToRemove.Count; j++)
			{
				int key = _keysToRemove[j];
				BeatmapEditorTableCell beatmapEditorTableCell2 = _activeCells[key];
				_activeCells.Remove(key);
				beatmapEditorTableCell2.gameObject.SetActive(value: false);
				if (!_reusableCells.TryGetValue(beatmapEditorTableCell2.reuseIdentifier, out var value2))
				{
					value2 = new List<BeatmapEditorTableCell>();
					_reusableCells.Add(beatmapEditorTableCell2.reuseIdentifier, value2);
				}
				value2.Add(beatmapEditorTableCell2);
			}
			for (int k = num3; k <= Mathf.Min(_prevMinRow - 1, num4); k++)
			{
				BeatmapEditorTableCell beatmapEditorTableCell3 = CellForRow(k);
				if (beatmapEditorTableCell3 != null)
				{
					SetupCellForRow(beatmapEditorTableCell3, k);
					_activeCells.Add(k, beatmapEditorTableCell3);
				}
			}
			for (int l = Mathf.Max(_prevMaxRow + 1, num3); l <= num4; l++)
			{
				BeatmapEditorTableCell beatmapEditorTableCell4 = CellForRow(l);
				if ((bool)beatmapEditorTableCell4)
				{
					SetupCellForRow(beatmapEditorTableCell4, l);
					_activeCells.Add(l, beatmapEditorTableCell4);
				}
			}
			_prevMinRow = num3;
			_prevMaxRow = num4;
		}
	}

	private void SetupCellForRow(BeatmapEditorTableCell cell, int row)
	{
		float rowHeight = _beatmapEditorScrollView.rowHeight;
		cell.gameObject.transform.SetParent(_cellsContainer, worldPositionStays: false);
		cell.gameObject.SetActive(value: true);
		RectTransform rectTransform = (RectTransform)cell.transform;
		rectTransform.anchoredPosition = new Vector2(base.transform.localPosition.x, (float)row * rowHeight + _playHeadOffset);
	}
}
