using System.Collections.Generic;
using HMUI;
using UnityEngine;

public class LeaderboardTableView : MonoBehaviour, TableView.IDataSource
{
	public class ScoreData
	{
		public int score { get; private set; }

		public string playerName { get; private set; }

		public int rank { get; private set; }

		public bool fullCombo { get; private set; }

		public ScoreData(int score, string playerName, int rank, bool fullCombo)
		{
			this.score = score;
			this.playerName = playerName;
			this.rank = rank;
			this.fullCombo = fullCombo;
		}
	}

	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private LeaderboardTableCell _cellPrefab;

	[SerializeField]
	private float _rowHeight = 7f;

	private const string kCellIdentifier = "Cell";

	private List<ScoreData> _scores;

	private int _specialScorePos;

	public float RowHeight()
	{
		return _rowHeight;
	}

	public int NumberOfRows()
	{
		if (_scores == null)
		{
			return 0;
		}
		return _scores.Count;
	}

	public TableCell CellForRow(int row)
	{
		LeaderboardTableCell leaderboardTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as LeaderboardTableCell;
		if (leaderboardTableCell == null)
		{
			leaderboardTableCell = Object.Instantiate(_cellPrefab);
			leaderboardTableCell.reuseIdentifier = "Cell";
		}
		ScoreData scoreData = _scores[row];
		leaderboardTableCell.rank = scoreData.rank;
		leaderboardTableCell.playerName = scoreData.playerName;
		leaderboardTableCell.score = scoreData.score;
		leaderboardTableCell.showFullCombo = scoreData.fullCombo;
		leaderboardTableCell.showSeparator = row != _scores.Count - 1;
		leaderboardTableCell.specialScore = _specialScorePos == row;
		return leaderboardTableCell;
	}

	public void SetScores(List<ScoreData> scores, int specialScorePos)
	{
		_scores = scores;
		_specialScorePos = specialScorePos;
		if (_tableView.dataSource == null)
		{
			_tableView.dataSource = this;
		}
		else
		{
			_tableView.ReloadData();
		}
	}
}
