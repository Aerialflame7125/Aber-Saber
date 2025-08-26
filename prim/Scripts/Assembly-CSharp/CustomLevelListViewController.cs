using System;
using UnityEngine;
using VRUI;

public class CustomLevelListViewController : VRUIViewController
{
	private class HierarchyRebuildData
	{
		public enum FinishAction
		{
			SelectImport,
			SelectSong
		}

		public FinishAction finishAction;

		public CustomLevelInfoWrapper[] levels { get; private set; }

		public StandardLevelSO selectedStandardLevel { get; private set; }

		public HierarchyRebuildData(CustomLevelInfoWrapper[] levels, StandardLevelSO selectedStandardLevel, FinishAction finishAction)
		{
			this.finishAction = finishAction;
			this.levels = levels;
			this.selectedStandardLevel = selectedStandardLevel;
		}
	}

	[SerializeField]
	[Provider(typeof(SongPreviewPlayer))]
	private ObjectProvider _songPreviewPlayerProvider;

	[Space]
	[SerializeField]
	private CustomLevelListTableView _songListTableView;

	[SerializeField]
	private LoadingIndicator _loadingIndicatorController;

	[SerializeField]
	private CustomLevelStandardLevelLoader customLevelStandardLevelLoader;

	[Space]
	[SerializeField]
	private CustomLevelsModelSO _customLevelsModel;

	private SongPreviewPlayer _songPreviewPlayer;

	private CustomLevelInfoWrapper[] _levels;

	private CustomLevelInfoWrapper _selectedLevel;

	private StandardLevelSO _selectedStandardLevel;

	private HierarchyRebuildData _hierarchyRebuildData;

	public event Action<CustomLevelListViewController, IStandardLevel> didSelectSongEvent;

	public event Action importButtonWasPressedEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_songPreviewPlayer = _songPreviewPlayerProvider.GetProvidedObject<SongPreviewPlayer>();
		}
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_levels = null;
			_songListTableView.Init(_levels, HandleSongListTableViewDidSelectRow);
			if (!base.isRebuildingHierarchy)
			{
				Reload();
			}
		}
	}

	public void Reload(Action callback = null)
	{
		_loadingIndicatorController.ShowLoading();
		_customLevelsModel.LoadCustomLevelsAsync(delegate(CustomLevelInfoWrapper[] levels)
		{
			_levels = levels;
			if (base.isActiveAndEnabled)
			{
				ReloadTable();
				_selectedLevel = null;
				_songListTableView.ClearSelection();
				_loadingIndicatorController.HideLoading();
				if (callback != null)
				{
					callback();
				}
			}
		});
	}

	public void SelectSongWithLevelID(string levelID)
	{
		_songListTableView.SelectAndScrollToLevel(levelID);
		HandleSongDidChange(_songListTableView.RowNumberForLevelID(levelID), crossfadeAudio: false);
	}

	private void ReloadTable()
	{
		_songListTableView.SetItems(_levels);
		RecenterTable();
	}

	private void RecenterTable()
	{
		RectTransform component = _songListTableView.transform.parent.gameObject.GetComponent<RectTransform>();
		float num = (component.rect.height - (float)_songListTableView.NumberOfRows() * _songListTableView.RowHeight()) * 0.5f;
		RectTransform component2 = _songListTableView.GetComponent<RectTransform>();
		component2.offsetMin = new Vector2(component2.offsetMin.x, Mathf.Max(num, 0f));
		component2.offsetMax = new Vector2(component2.offsetMax.x, Mathf.Min(0f - num, 0f));
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		if (hierarchyRebuildData is HierarchyRebuildData hierarchyRebuildData2)
		{
			_levels = hierarchyRebuildData2.levels;
			ReloadTable();
			if (hierarchyRebuildData2.finishAction == HierarchyRebuildData.FinishAction.SelectImport)
			{
				ImportButtonPressed();
			}
			else if (hierarchyRebuildData2.finishAction == HierarchyRebuildData.FinishAction.SelectSong)
			{
				SelectSongWithLevelID(hierarchyRebuildData2.selectedStandardLevel.levelID);
				HandleSongDidChange(_songListTableView.RowNumberForLevelID(hierarchyRebuildData2.selectedStandardLevel.levelID), crossfadeAudio: false, hierarchyRebuildData2.selectedStandardLevel);
			}
		}
	}

	protected override void DidDeactivate(DeactivationType deactivationType)
	{
		if (deactivationType == DeactivationType.RemovedFromHierarchy)
		{
			_songPreviewPlayer.CrossfadeToDefault();
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	private void HandleSongDidChange(int row, bool crossfadeAudio)
	{
		if (_selectedLevel != _levels[row])
		{
			_selectedLevel = _levels[row];
			customLevelStandardLevelLoader.LoadStandardLevel(_selectedLevel, delegate(StandardLevelSO standardLevel)
			{
				_selectedStandardLevel = standardLevel;
				_hierarchyRebuildData = new HierarchyRebuildData(_levels, _selectedStandardLevel, HierarchyRebuildData.FinishAction.SelectSong);
				HandleSongDidChange(row, crossfadeAudio, standardLevel);
			});
		}
	}

	private void HandleSongDidChange(int row, bool crossfadeAudio, StandardLevelSO standardLevel)
	{
		if (_selectedLevel == _levels[row])
		{
			if (crossfadeAudio)
			{
				_songPreviewPlayer.CrossfadeTo(standardLevel.audioClip, _selectedLevel.customLevelInfo.previewStartTime, _selectedLevel.customLevelInfo.previewDuration);
			}
			if (this.didSelectSongEvent != null)
			{
				this.didSelectSongEvent(this, standardLevel);
			}
		}
	}

	private void HandleSongListTableViewDidSelectRow(CustomLevelListTableView tableView, int row)
	{
		HandleSongDidChange(row, crossfadeAudio: true);
	}

	public void Init()
	{
		_selectedLevel = null;
	}

	public void ImportButtonPressed()
	{
		_hierarchyRebuildData = new HierarchyRebuildData(_levels, _selectedStandardLevel, HierarchyRebuildData.FinishAction.SelectImport);
		_songPreviewPlayer.CrossfadeToDefault();
		if (this.importButtonWasPressedEvent != null)
		{
			this.importButtonWasPressedEvent();
		}
	}
}
