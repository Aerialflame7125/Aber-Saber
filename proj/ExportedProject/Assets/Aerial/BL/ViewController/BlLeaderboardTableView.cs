using System.Collections.Generic;
using HMUI;
using UnityEngine;

public class BlLeaderboardTableView : MonoBehaviour, TableView.IDataSource
{
	public class ScoreData
	{
		public float accuracy { get; private set; }

		public int score { get; private set; }

		public string playerName { get; private set; }

		public int rank { get; private set; }

		public int misses { get; private set; }

		public string userPfp { get; private set; }

		public ScoreData(float accuracy, int score, string playerName, int rank, bool fullCombo, int misses, string userPfp)
		{
			this.accuracy = accuracy;
			this.score = score;
			this.playerName = playerName;
			this.rank = rank;
			this.misses = misses;
			this.userPfp = userPfp;
		}
	}

	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private BlLeaderboardTableCell _cellPrefab;

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
		BlLeaderboardTableCell leaderboardTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as BlLeaderboardTableCell;
		if (leaderboardTableCell == null)
		{
			leaderboardTableCell = Object.Instantiate(_cellPrefab);
			leaderboardTableCell.reuseIdentifier = "Cell";
		}
		ScoreData scoreData = _scores[row];
		leaderboardTableCell.accuracy = scoreData.accuracy;
		leaderboardTableCell.rank = scoreData.rank;
		leaderboardTableCell.playerName = scoreData.playerName;
		leaderboardTableCell.score = scoreData.score;
		leaderboardTableCell.misses = scoreData.misses;
		leaderboardTableCell.pfpPath = scoreData.userPfp;
		leaderboardTableCell.showSeparator = row != _scores.Count - 1;
		leaderboardTableCell.specialScore = _specialScorePos == row;
		return leaderboardTableCell;
	}

	public void SetScores(List<ScoreData> scores, int specialScorePos)
	{
		_scores = scores;
		_specialScorePos = specialScorePos;
		_tableView.dataSource = this;
		_tableView.ReloadData();
		Debug.LogError("Set new data source and reloaded data");

	}
}
