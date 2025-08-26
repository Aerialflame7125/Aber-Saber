using System;
using UnityEngine;
using VRUI;

public class StandardLevelDifficultyViewController : VRUIViewController
{
	private class HierarchyRebuildData
	{
		public LevelDifficulty difficulty { get; private set; }

		public HierarchyRebuildData(LevelDifficulty difficulty)
		{
			this.difficulty = difficulty;
		}
	}

	public Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap> didSelectDifficultyEvent;

	[SerializeField]
	private DifficultyTableView _difficultyTableView;

	[SerializeField]
	private HowToPlayViewController _howToPlayViewController;

	private IStandardLevelDifficultyBeatmap[] _difficultyLevels;

	private IStandardLevelDifficultyBeatmap _selectedDifficultyLevel;

	private bool _showHowToPlayViewController;

	public IStandardLevelDifficultyBeatmap selectedDifficultyLevel => _selectedDifficultyLevel;

	public void Init(IStandardLevelDifficultyBeatmap[] difficultyLevels, bool showHowToPlayViewController)
	{
		_selectedDifficultyLevel = null;
		_difficultyLevels = difficultyLevels;
		_showHowToPlayViewController = showHowToPlayViewController;
		if (_showHowToPlayViewController)
		{
			_howToPlayViewController.Init(showTutorialButton: true);
		}
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_selectedDifficultyLevel = null;
			_difficultyTableView.Init(_difficultyLevels, HandleDifficultyTableViewDidSelectRow);
			RecenterTableView();
			_difficultyTableView.ClearSelection();
		}
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		if (!(hierarchyRebuildData is HierarchyRebuildData hierarchyRebuildData2))
		{
			return;
		}
		int row = 0;
		for (int i = 0; i < _difficultyLevels.Length; i++)
		{
			if (_difficultyLevels[i].difficulty == hierarchyRebuildData2.difficulty)
			{
				row = i;
			}
		}
		_difficultyTableView.SelectRow(row, callbackTable: true);
	}

	protected override object GetHierarchyRebuildData()
	{
		if (_selectedDifficultyLevel != null)
		{
			return new HierarchyRebuildData(_selectedDifficultyLevel.difficulty);
		}
		return null;
	}

	protected override void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
	{
		leftScreenViewController = ((!_showHowToPlayViewController) ? null : _howToPlayViewController);
		rightScreenViewController = null;
	}

	private int GetClosestDifficultyIndex(LevelDifficulty difficulty)
	{
		int num = -1;
		IStandardLevelDifficultyBeatmap[] difficultyLevels = _difficultyLevels;
		foreach (IStandardLevelDifficultyBeatmap standardLevelDifficultyBeatmap in difficultyLevels)
		{
			if (difficulty < standardLevelDifficultyBeatmap.difficulty)
			{
				break;
			}
			num++;
		}
		if (num == -1)
		{
			num = 0;
		}
		return num;
	}

	private void RecenterTableView()
	{
		RectTransform component = _difficultyTableView.GetComponent<RectTransform>();
		float num = (base.rectTransform.rect.height - (float)_difficultyTableView.NumberOfRows() * _difficultyTableView.RowHeight()) * 0.5f;
		component.offsetMin = new Vector2(component.offsetMin.x, num);
		component.offsetMax = new Vector2(component.offsetMax.x, 0f - num);
	}

	private void HandleDifficultyTableViewDidSelectRow(DifficultyTableView tableView, int row)
	{
		_selectedDifficultyLevel = _difficultyLevels[row];
		if (didSelectDifficultyEvent != null)
		{
			didSelectDifficultyEvent(this, _selectedDifficultyLevel);
		}
	}

	public void SetDifficultyLevels(IStandardLevelDifficultyBeatmap[] difficultyLevels, IStandardLevelDifficultyBeatmap selectedDifficultyLevel)
	{
		_difficultyLevels = difficultyLevels;
		_difficultyTableView.SetDifficultyLevels(_difficultyLevels);
		if (selectedDifficultyLevel != null)
		{
			int closestDifficultyIndex = GetClosestDifficultyIndex(selectedDifficultyLevel.difficulty);
			_difficultyTableView.ClearSelection();
			_difficultyTableView.SelectRow(closestDifficultyIndex, callbackTable: true);
		}
		else
		{
			_difficultyTableView.ClearSelection();
		}
		RecenterTableView();
	}
}
