using System.Collections.Generic;

public class LocalLeaderboardTableView : LeaderboardTableView
{
	private const int kFillEmptyCellsNum = 10;

	public void SetContent(string leaderboardID, LocalLeaderboardsModel.LeaderboardType leaderboardType)
	{
		List<ScoreData> list = new List<ScoreData>();
		List<LocalLeaderboardsModel.ScoreData> scores = PersistentSingleton<LocalLeaderboardsModel>.instance.GetScores(leaderboardID, leaderboardType);
		if (scores != null)
		{
			for (int i = 0; i < scores.Count; i++)
			{
				LocalLeaderboardsModel.ScoreData scoreData = scores[i];
				list.Add(new ScoreData(scoreData._score, scoreData._playerName, i + 1, scoreData._fullCombo));
			}
		}
		int num = ((scores != null) ? scores.Count : 0);
		for (int j = num; j < 10; j++)
		{
			list.Add(new ScoreData(-1, string.Empty, j + 1, false));
		}
		SetScores(list, PersistentSingleton<LocalLeaderboardsModel>.instance.GetLastScorePosition(leaderboardID, leaderboardType));
	}
}
