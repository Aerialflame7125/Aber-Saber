using System;
using TMPro;
using UnityEngine;

public class DemoMenuLevelPanelView : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	[SerializeField]
	private TextMeshProUGUI _difficultyText;

	[SerializeField]
	private LocalLeaderboardTableView _localLeaderboardTableView;

	public event Action<DemoMenuLevelPanelView> playButtonWasPressedEvent;

	public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode)
	{
		_songNameText.text = difficultyLevel.level.songName;
		_difficultyText.text = difficultyLevel.difficulty.Name();
		_localLeaderboardTableView.SetContent(LeaderboardsModel.GetLeaderboardID(difficultyLevel, gameplayMode), LocalLeaderboardsModel.LeaderboardType.Daily);
	}

	public void PlayButtonWasPressed()
	{
		if (this.playButtonWasPressedEvent != null)
		{
			this.playButtonWasPressedEvent(this);
		}
	}
}
