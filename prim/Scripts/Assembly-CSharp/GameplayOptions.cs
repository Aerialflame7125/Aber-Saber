using System;

[Serializable]
public class GameplayOptions
{
	public enum ObstaclesOption
	{
		All,
		FullHeightOnly,
		None
	}

	public bool staticLights;

	public bool mirror;

	public bool noEnergy;

	public ObstaclesOption obstaclesOption;

	public bool noObstacles => obstaclesOption != ObstaclesOption.All;

	public bool validForScoreUse => !noEnergy && obstaclesOption == ObstaclesOption.All;

	public static GameplayOptions defaultOptions
	{
		get
		{
			GameplayOptions gameplayOptions = new GameplayOptions();
			gameplayOptions.ResetToDefault();
			return gameplayOptions;
		}
	}

	public void ResetToDefault()
	{
		staticLights = false;
		mirror = false;
		noEnergy = false;
		obstaclesOption = ObstaclesOption.All;
	}
}
