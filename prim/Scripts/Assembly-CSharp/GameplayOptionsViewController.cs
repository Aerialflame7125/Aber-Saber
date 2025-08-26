using HMUI;
using TMPro;
using UnityEngine;
using VRUI;

public class GameplayOptionsViewController : VRUIViewController
{
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

	public GameplayOptions gameplayOptions => PersistentSingleton<GameDataModel>.instance.gameDynamicData.GetCurrentPlayerDynamicData().gameplayOptions;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_noEnergyToggle.didSwitchEvent += HandleNoEnergyToggleDidSwitch;
			_mirrorToggle.didSwitchEvent += HandleMirrorToggleDidSwitch;
			_noObstaclesToggle.didSwitchEvent += HandleNoObstaclesToggleDidSwitch;
			_staticLightsToggle.didSwitchEvent += HandleStaticLightsToggleDidSwitch;
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
}
