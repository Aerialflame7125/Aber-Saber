public static class GameplayModeMethods
{
	public static bool IsSolo(this GameplayMode gameplayMode)
	{
		return gameplayMode != GameplayMode.PartyStandard;
	}
}
