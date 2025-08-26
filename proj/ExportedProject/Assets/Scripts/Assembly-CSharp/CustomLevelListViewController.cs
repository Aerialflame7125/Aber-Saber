using System;
using UnityEngine;
using VRUI;

public class CustomLevelListViewController : VRUIViewController
{
    private class HierarchyRebuildData
    {
        public string levelID { get; private set; }

        public HierarchyRebuildData(string levelID)
        {
            this.levelID = levelID;
        }
    }

    [SerializeField]
    [Provider(typeof(SongPreviewPlayer))]
    private ObjectProvider _songPreviewPlayerProvider;

    [Space]
    [SerializeField]
    private CustomLevelListTableView _levelListTableView;

    [SerializeField]
    private HowToPlayViewController _howToPlayViewController;

    private StandardLevelListViewController _slevellist;

    private SongPreviewPlayer _songPreviewPlayer;

    private IStandardLevel[] _levels;

    private IStandardLevel _selectedLevel;

    private bool _showHowToPlayViewController;

    public IStandardLevel selectedLevel
    {
        get
        {
            return _selectedLevel;
        }
    }

    public event Action<StandardLevelListViewController, IStandardLevel> didSelectLevelEvent;

    protected override void DidActivate(bool firstActivation, ActivationType activationType)
    {
        if (firstActivation)
        {
            _songPreviewPlayer = _songPreviewPlayerProvider.GetProvidedObject<SongPreviewPlayer>();
        }
        if (activationType == ActivationType.AddedToHierarchy)
        {
            _levelListTableView.Init(_levels, HandleLevelListTableViewDidSelectRow);
            RectTransform component = _levelListTableView.transform.parent.gameObject.GetComponent<RectTransform>();
            float num = (component.rect.height - (float)_levelListTableView.NumberOfRows() * _levelListTableView.RowHeight()) * 0.5f;
            RectTransform component2 = _levelListTableView.GetComponent<RectTransform>();
            component2.offsetMin = new Vector2(component2.offsetMin.x, Mathf.Max(num, 0f));
            component2.offsetMax = new Vector2(component2.offsetMax.x, Mathf.Min(0f - num, 0f));
            _selectedLevel = null;
            _levelListTableView.ClearSelection();
        }
    }

    protected override void RebuildHierarchy(object hierarchyRebuildData)
    {
        HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
        if (hierarchyRebuildData2 != null)
        {
            _levelListTableView.SelectAndScrollToLevel(hierarchyRebuildData2.levelID);
            HandleLevelSelectionDidChange(_levelListTableView.RowNumberForLevelID(hierarchyRebuildData2.levelID), false);
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
        if (_selectedLevel != null)
        {
            return new HierarchyRebuildData(_selectedLevel.levelID);
        }
        return null;
    }

    protected override void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
    {
        leftScreenViewController = ((!_showHowToPlayViewController) ? null : _howToPlayViewController);
        rightScreenViewController = null;
    }

    private void HandleLevelSelectionDidChange(int row, bool crossfadeAudio)
    {
        if (_selectedLevel != _levels[row])
        {
            _selectedLevel = _levels[row];
            if (crossfadeAudio)
            {
                _songPreviewPlayer.CrossfadeTo(_selectedLevel.audioClip, _selectedLevel.previewStartTime, _selectedLevel.previewDuration);
            }
        }
        CustomLevelListViewController customLevelListViewController = this;
        if (customLevelListViewController.didSelectLevelEvent != null)
        {
            customLevelListViewController.didSelectLevelEvent(_slevellist, customLevelListViewController._selectedLevel);
        }
    }

    private void HandleLevelListTableViewDidSelectRow(StandardLevelListTableView tableView, int row)
    {
        HandleLevelSelectionDidChange(row, true);
    }

    public void Init(IStandardLevel[] levels, bool showHowToPlayViewController)
    {
        _selectedLevel = null;
        _levels = levels;
        _showHowToPlayViewController = showHowToPlayViewController;
        if (_showHowToPlayViewController)
        {
            _howToPlayViewController.Init(true);
        }
    }
}
