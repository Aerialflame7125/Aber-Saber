using System;
using System.Collections.Generic;
using UnityEngine;

namespace HMUI
{
	public class SegmentedControl : MonoBehaviour
	{
		public interface IDataSource
		{
			int NumberOfColumns();

			SegmentedControlCell CellForColumn(int column);
		}

		[SerializeField]
		private float _fontSize = 4f;

		private int _numberOfColumns;

		private List<SegmentedControlCell> _cells;

		private IDataSource _dataSource;

		private int _selectedColumn;

		public IDataSource dataSource
		{
			get
			{
				return _dataSource;
			}
			set
			{
				_dataSource = value;
				ReloadData();
			}
		}

		public event Action<SegmentedControl, int> didSelectCellEvent;

		protected virtual void Awake()
		{
			_cells = new List<SegmentedControlCell>();
			_selectedColumn = -1;
		}

		private void CreateCells()
		{
			Transform transform = base.transform;
			for (int i = 0; i < _numberOfColumns; i++)
			{
				SegmentedControlCell segmentedControlCell = _dataSource.CellForColumn(i);
				_cells.Add(segmentedControlCell);
				segmentedControlCell.gameObject.SetActive(true);
				segmentedControlCell.SegmentedControlSetup(this, i);
				segmentedControlCell.ChangeSelection(_selectedColumn == i, SegmentedControlCell.TransitionType.Instant, false, true);
				segmentedControlCell.ChangeHighlight(false, SegmentedControlCell.TransitionType.Instant, true);
				TextSegmentedControlCell textSegmentedControlCell = segmentedControlCell as TextSegmentedControlCell;
				if (textSegmentedControlCell != null)
				{
					textSegmentedControlCell.fontSize = _fontSize;
				}
				if (segmentedControlCell.transform.parent != transform)
				{
					segmentedControlCell.transform.SetParent(base.transform, false);
				}
				segmentedControlCell.transform.localPosition = Vector3.zero;
				segmentedControlCell.transform.localRotation = Quaternion.identity;
			}
		}

		public void CellSelectionStateDidChange(SegmentedControlCell changedCell)
		{
			_cells[_selectedColumn].ChangeSelection(false, SegmentedControlCell.TransitionType.Instant, false, false);
			_selectedColumn = changedCell.column;
			if (this.didSelectCellEvent != null)
			{
				this.didSelectCellEvent(this, changedCell.column);
			}
		}

		public void ReloadData()
		{
			foreach (SegmentedControlCell cell in _cells)
			{
				UnityEngine.Object.Destroy(cell.gameObject);
			}
			_cells.Clear();
			_numberOfColumns = _dataSource.NumberOfColumns();
			_selectedColumn = 0;
			CreateCells();
		}

		public void SelectColumn(int selectedColumn)
		{
			_selectedColumn = selectedColumn;
			for (int i = 0; i < _numberOfColumns; i++)
			{
				_cells[i].ChangeSelection(i == selectedColumn, SegmentedControlCell.TransitionType.Instant, false, false);
			}
		}
	}
}
