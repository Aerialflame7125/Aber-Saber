using System;

[Serializable]
public class GameplayOptions
{
	public enum ObstaclesOption
	{
		All = 0,
		FullHeightOnly = 1,
		None = 2
	}

	public bool staticLights;

	public bool mirror;

	public bool noEnergy;

	public ObstaclesOption obstaclesOption;

	public bool noObstacles
	{
		get
		{
			return obstaclesOption != ObstaclesOption.All;
		}
	}
    public bool validForScoreUse
	{
		get
		{
			return !noEnergy && obstaclesOption == ObstaclesOption.All;
		}
	}

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
