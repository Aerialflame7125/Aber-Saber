using UnityEngine;

namespace HMUI
{
	public class SimpleSegmentedControl : SegmentedControl, SegmentedControl.IDataSource
	{
		[SerializeField]
		private TextSegmentedControlCell _firstCellPrefab;

		[SerializeField]
		private TextSegmentedControlCell _lastCellPrefab;

		[NullAllowed]
		[SerializeField]
		private TextSegmentedControlCell _middleCellPrefab;

		private string[] _texts;

		protected override void Awake()
		{
			base.Awake();
			base.dataSource = this;
		}

		public void SetTexts(string[] texts)
		{
			_texts = texts;
			ReloadData();
		}

		public int NumberOfColumns()
		{
			if (_texts == null)
			{
				return 0;
			}
			return _texts.Length;
		}

		public SegmentedControlCell CellForColumn(int column)
		{
			TextSegmentedControlCell textSegmentedControlCell = null;
			textSegmentedControlCell = ((column == 0) ? Object.Instantiate(_firstCellPrefab, Vector3.zero, Quaternion.identity, base.transform) : ((column != _texts.Length - 1) ? Object.Instantiate(_middleCellPrefab, Vector3.zero, Quaternion.identity, base.transform) : Object.Instantiate(_lastCellPrefab, Vector3.zero, Quaternion.identity, base.transform)));
			textSegmentedControlCell.text = _texts[column];
			return textSegmentedControlCell;
		}
	}
}
