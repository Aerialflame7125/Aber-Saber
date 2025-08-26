using HMUI;
using TMPro;
using UnityEngine;
using VRUI;
using System.Collections.Generic;

public class GameplayOptionsViewController : VRUIViewController
{
    [Header("Base Game Options")]
    [SerializeField]
    private Toggle _noEnergyToggle;
    [SerializeField]
    private Toggle _noObstaclesToggle;
    [SerializeField]
    private Toggle _mirrorToggle;
    [SerializeField]
    private Toggle _staticLightsToggle;
    [SerializeField]
    private TextMeshProUGUI _text;

    [Header("Object Group Display")]
    [SerializeField] private List<GameObject[]> objectGroups = new List<GameObject[]>();
    [SerializeField] private bool hideAllOnStart = true;
    [SerializeField] private bool hideOthersOnShow = true;

    public GameplayOptions gameplayOptions
    {
        get
        {
            return PersistentSingleton<GameDataModel>.instance.gameDynamicData.GetCurrentPlayerDynamicData().gameplayOptions;
        }
    }

    protected override void DidActivate(bool firstActivation, ActivationType activationType)
    {
        if (firstActivation)
        {
            // Set up base game toggle events
            _noEnergyToggle.didSwitchEvent += HandleNoEnergyToggleDidSwitch;
            _mirrorToggle.didSwitchEvent += HandleMirrorToggleDidSwitch;
            _noObstaclesToggle.didSwitchEvent += HandleNoObstaclesToggleDidSwitch;
            _staticLightsToggle.didSwitchEvent += HandleStaticLightsToggleDidSwitch;

            // Hide all object groups initially if configured
            if (hideAllOnStart)
            {
                HideAllObjects();
            }
        }

        if (activationType == ActivationType.AddedToHierarchy)
        {
            RefreshToggles();
        }
    }

    private void HandleNoEnergyToggleDidSwitch(Toggle toggle, bool isOn)
    {
        gameplayOptions.noEnergy = isOn;
    }

    private void HandleNoObstaclesToggleDidSwitch(Toggle toggle, bool isOn)
    {
        gameplayOptions.obstaclesOption = (isOn ? GameplayOptions.ObstaclesOption.None : GameplayOptions.ObstaclesOption.All);
    }

    private void HandleMirrorToggleDidSwitch(Toggle toggle, bool isOn)
    {
        gameplayOptions.mirror = isOn;
    }

    private void HandleStaticLightsToggleDidSwitch(Toggle toggle, bool isOn)
    {
        gameplayOptions.staticLights = isOn;
    }

    private void RefreshToggles()
    {
        _noEnergyToggle.isOn = gameplayOptions.noEnergy;
        _noObstaclesToggle.isOn = gameplayOptions.obstaclesOption != GameplayOptions.ObstaclesOption.All;
        _mirrorToggle.isOn = gameplayOptions.mirror;
        _staticLightsToggle.isOn = gameplayOptions.staticLights;
    }

    public void Init(GameplayMode gameplayMode)
    {
        if (gameplayMode.IsSolo())
        {
            _text.text = "If <color=#00AAFF>NO FAIL</color> or <color=#00AAFF>NO OBSTACLES</color> options are turned ON your score will not be submitted to the leaderboards.";
        }
        else
        {
            _text.text = "If <color=#00AAFF>NO FAIL</color> or <color=#00AAFF>NO OBSTACLES</color> options are turned ON your score will lowered by 30%.";
        }
    }

    public void DefaultsButtonWasPressed()
    {
        gameplayOptions.ResetToDefault();
        RefreshToggles();
    }

    // ---- PUBLIC Button Functions (These will appear in Unity's inspector) ----

    /// <summary>
    /// Show objects from group index 0 (usually Base Game options)
    /// </summary>
    public void ShowGroup0()
    {
        ShowObjectGroup(0);
    }

    /// <summary>
    /// Show objects from group index 1
    /// </summary>
    public void ShowGroup1()
    {
        ShowObjectGroup(1);
    }

    /// <summary>
    /// Show objects from group index 2
    /// </summary>
    public void ShowGroup2()
    {
        ShowObjectGroup(2);
    }

    /// <summary>
    /// Show objects from group index 3
    /// </summary>
    public void ShowGroup3()
    {
        ShowObjectGroup(3);
    }

    /// <summary>
    /// Show objects from group index 4
    /// </summary>
    public void ShowGroup4()
    {
        ShowObjectGroup(4);
    }

    /// <summary>
    /// Hide all object groups
    /// </summary>
    public void HideAllGroups()
    {
        HideAllObjects();
    }

    // ---- Object Group Management Functions ----

    /// <summary>
    /// Show objects from the specified group index
    /// </summary>
    private void ShowObjectGroup(int groupIndex)
    {
        if (groupIndex < 0 || groupIndex >= objectGroups.Count)
        {
            Debug.LogWarning($"Button tried to show group {groupIndex}, but it doesn't exist!");
            return;
        }

        if (hideOthersOnShow)
        {
            HideAllObjects();
        }

        foreach (GameObject obj in objectGroups[groupIndex])
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Hide objects from the specified group index
    /// </summary>
    private void HideObjectGroup(int groupIndex)
    {
        if (groupIndex < 0 || groupIndex >= objectGroups.Count)
        {
            Debug.LogWarning($"Button tried to hide group {groupIndex}, but it doesn't exist!");
            return;
        }

        foreach (GameObject obj in objectGroups[groupIndex])
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Hide all object groups
    /// </summary>
    private void HideAllObjects()
    {
        foreach (GameObject[] group in objectGroups)
        {
            foreach (GameObject obj in group)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}