using System;
using System.Collections.Generic;
using UnityEngine;
using VRUI;

public class DemoMenuViewController : VRUIViewController
{
	public enum MenuButton
	{
		LeftLevel,
		RightLevel,
		Tutorial
	}

	private class HierarchyRebuildData
	{
		public MenuButton menuButton;

		public HierarchyRebuildData(MenuButton menuButton)
		{
			this.menuButton = menuButton;
		}
	}

	[SerializeField]
	private DemoMenuLevelPanelView _leftLevelPanelView;

	[SerializeField]
	private DemoMenuLevelPanelView _rightLevelPanelView;

	[SerializeField]
	private HowToPlayViewController _howToPlayViewController;

	[SerializeField]
	private VRUIViewController _demoInfoViewController;

	[Space]
	[SerializeField]
	private StandardLevelSO _leftStandardLevel;

	[SerializeField]
	private LevelDifficulty _leftLevelDifficulty;

	[SerializeField]
	private StandardLevelSO _rightStandardLevel;

	[SerializeField]
	private LevelDifficulty _rightLevelDifficulty;

	private HierarchyRebuildData _hierarchyRebuildData;

	private Dictionary<MenuButton, IStandardLevelDifficultyBeatmap> _buttonLevelDict;

	private Dictionary<DemoMenuLevelPanelView, MenuButton> _demoMenuLevelPanelViewButtonDict;

	private GameplayOptions _gameplayOptions;

	private GameplayMode _gameplayMode;

	public GameplayMode gameplayMode => _gameplayMode;

	public GameplayOptions gameplayOptions => _gameplayOptions;

	public event Action<DemoMenuViewController, IStandardLevelDifficultyBeatmap> didSelectLevelEvent;

	public event Action<DemoMenuViewController> didSelectTutorialEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_buttonLevelDict = new Dictionary<MenuButton, IStandardLevelDifficultyBeatmap>
			{
				{
					MenuButton.LeftLevel,
					_leftStandardLevel.GetDifficultyLevel(_leftLevelDifficulty)
				},
				{
					MenuButton.RightLevel,
					_rightStandardLevel.GetDifficultyLevel(_rightLevelDifficulty)
				}
			};
			_demoMenuLevelPanelViewButtonDict = new Dictionary<DemoMenuLevelPanelView, MenuButton>
			{
				{
					_leftLevelPanelView,
					MenuButton.LeftLevel
				},
				{
					_rightLevelPanelView,
					MenuButton.RightLevel
				}
			};
			_leftLevelPanelView.playButtonWasPressedEvent += HandleDemoMenuLevelPanelViewPlayButtonWasPressed;
			_rightLevelPanelView.playButtonWasPressedEvent += HandleDemoMenuLevelPanelViewPlayButtonWasPressed;
			_gameplayOptions = GameplayOptions.defaultOptions;
			_gameplayOptions.noEnergy = true;
			_gameplayMode = GameplayMode.PartyStandard;
		}
		_leftLevelPanelView.Init(_leftStandardLevel.GetDifficultyLevel(_leftLevelDifficulty), _gameplayMode);
		_rightLevelPanelView.Init(_rightStandardLevel.GetDifficultyLevel(_rightLevelDifficulty), _gameplayMode);
		PersistentSingleton<LocalLeaderboardsModel>.instance.ClearLastScorePosition();
	}

	protected override void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
	{
		leftScreenViewController = _howToPlayViewController;
		rightScreenViewController = _demoInfoViewController;
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		if (hierarchyRebuildData is HierarchyRebuildData hierarchyRebuildData2)
		{
			HandleMenuButton(hierarchyRebuildData2.menuButton);
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	private void HandleMenuButton(MenuButton menuButton)
	{
		_hierarchyRebuildData = new HierarchyRebuildData(menuButton);
		if (menuButton == MenuButton.Tutorial)
		{
			if (this.didSelectTutorialEvent != null)
			{
				this.didSelectTutorialEvent(this);
			}
		}
		else if (this.didSelectLevelEvent != null)
		{
			this.didSelectLevelEvent(this, _buttonLevelDict[menuButton]);
		}
	}

	private void HandleDemoMenuLevelPanelViewPlayButtonWasPressed(DemoMenuLevelPanelView demoMenuLevelPanelView)
	{
		HandleMenuButton(_demoMenuLevelPanelViewButtonDict[demoMenuLevelPanelView]);
	}

	public void TutorialButtonPressed()
	{
		HandleMenuButton(MenuButton.Tutorial);
	}
}
