using UnityEngine;

public class GameEnvironmentSceneSetup : MonoBehaviour
{
	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	[SerializeField]
	private StandardLevelSO _defaultStandardLevel;

	[SerializeField]
	private GameEnergyUIPanel _energyUIPanel;

	private void Awake()
	{
		if (!_mainGameSceneSetupData.wasUsedInLastTransition)
		{
			_mainGameSceneSetupData.Init(_defaultStandardLevel.difficultyBeatmaps[0], GameplayOptions.defaultOptions, GameplayMode.PartyStandard, 0f);
		}
		_energyUIPanel.EnableEnergyPanel(!_mainGameSceneSetupData.gameplayOptions.noEnergy);
	}
}
