using HMUI;
using UnityEngine;
using VRUI;

public class LocalLeaderboardViewController : VRUIViewController
{
	[SerializeField]
	private LocalLeaderboardTableView _localLeaderboardTableView;

	[SerializeField]
	private SimpleSegmentedControl _scopeSegmentedControl;

	private static LocalLeaderboardsModel.LeaderboardType _leaderboardType;

	private IStandardLevelDifficultyBeatmap _difficultyLevel;

	private GameplayMode _gameplayMode;

	public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode)
	{
		_difficultyLevel = difficultyLevel;
		_gameplayMode = gameplayMode;
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_scopeSegmentedControl.SetTexts(new string[2] { "All time", "Today" });
			_scopeSegmentedControl.didSelectCellEvent += HandleScopeSegmentedControlDidSelectCell;
			base.rectTransform.anchorMin = Vector2.zero;
			base.rectTransform.anchorMax = Vector2.one;
			base.rectTransform.offsetMin = Vector2.zero;
			base.rectTransform.offsetMax = Vector2.zero;
		}
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_scopeSegmentedControl.SelectColumn((int)_leaderboardType);
			Refresh();
		}
	}

	private void OnDestroy()
	{
		if (_scopeSegmentedControl != null)
		{
			_scopeSegmentedControl.didSelectCellEvent -= HandleScopeSegmentedControlDidSelectCell;
		}
	}

	private void HandleScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int column)
	{
		switch (column)
		{
		case 0:
			_leaderboardType = LocalLeaderboardsModel.LeaderboardType.AllTime;
			Refresh();
			break;
		case 1:
			_leaderboardType = LocalLeaderboardsModel.LeaderboardType.Daily;
			Refresh();
			break;
		}
	}

	public void Refresh()
	{
		string leaderboardID = LeaderboardsModel.GetLeaderboardID(_difficultyLevel, _gameplayMode);
		_localLeaderboardTableView.SetContent(leaderboardID, _leaderboardType);
	}
}
