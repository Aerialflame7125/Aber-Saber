using System;
using UnityEngine;
using UnityEngine.SceneManagement; // For SceneManager
using System.Collections; // Required for IEnumerator
using VRUI;
using TMPro;
using SongLoader;

public class HowToPlayButtonController : VRUIViewController
{

    [SerializeField]
    private MenuSceneSetupData menuSceneSetupData;

    public enum SubMenuType
    {
        Init = 0,
        ReloadSongs = 1,
        Back = 2
    }

    private class HierarchyRebuildData
    {
        public SubMenuType subMenuType;

        public HierarchyRebuildData(SubMenuType subMenuType)
        {
            this.subMenuType = subMenuType;
        }
    }

    public Action<HowToPlayButtonController, SubMenuType> didFinishEvent;

    [SerializeField]
    private SimpleDialogPromptViewController _simpleDialogPromptViewController;

    [SerializeField]
    private MainMenuViewController mainMenuViewController;

    [SerializeField]
    private MainSongLoader mainSongLoader;

    [SerializeField]
    private FadeOutOnGameEvent _fadeOutOnGameEvent;

    private HierarchyRebuildData _hierarchyRebuildData;

    protected override void RebuildHierarchy(object hierarchyRebuildData)
    {
        HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
        if (hierarchyRebuildData2 != null)
        {
            HandleSubMenuButton(hierarchyRebuildData2.subMenuType);
        }
    }

    protected override object GetHierarchyRebuildData()
    {
        return _hierarchyRebuildData;
    }

    private void HandleSubMenuButton(SubMenuType subMenuType)
    {
        _hierarchyRebuildData = new HierarchyRebuildData(subMenuType);
        if (didFinishEvent != null)
        {
            didFinishEvent(this, subMenuType);
        }
    }

    public void InitButtonWasPressed()
    {
        StartCoroutine(FadeOutAndLoadScene()); // Start the coroutine
    }

    IEnumerator FadeOutAndLoadScene()
    {
        yield return StartCoroutine(_fadeOutOnGameEvent.HandleGameEventCoroutine(0.5f));
        SceneManager.LoadScene("Init", LoadSceneMode.Single);
    }

    public void ReloadSongsButtonWasPressed()
    {
        mainSongLoader.StartLoadingAndMerge(true);
    }

    public void AnalyzeReplayButtonWasPressed()
    {
        ReplayDecoder.Program.Main();
    }
}
