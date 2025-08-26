using UnityEngine;

public class GameOptionsTextsHelper : MonoBehaviour
{
	public static string PlayedWithOptionsText(bool noEnergy, bool noObstacles)
	{
		if (noEnergy && !noObstacles)
		{
			return "Played with <color=#FF175A>NO FAIL</color> option.";
		}
		if (noEnergy && noObstacles)
		{
			return "Played with <color=#FF175A>NO FAIL</color> and <color=#FF175A>NO OBSTACLES</color> options.";
		}
		if (!noEnergy && noObstacles)
		{
			return "Played with <color=#FF175A>NO OBSTACLES</color> option.";
		}
		return string.Empty;
	}

	public static string OptionsAreTurnOnText(bool noEnergy, bool noObstacles)
	{
		if (noEnergy && !noObstacles)
		{
			return "<color=#00AAFF>NO FAIL</color> option is turned ON.";
		}
		if (noEnergy && noObstacles)
		{
			return "<color=#00AAFF>NO FAIL</color> and <color=#00AAFF>NO OBSTACLES</color> options are turned ON.";
		}
		if (!noEnergy && noObstacles)
		{
			return "<color=#00AAFF>NO OBSTACLES</color> option is turned ON.";
		}
		return string.Empty;
	}

	public static string OptionsToText(bool noEnergy, bool noObstacles)
	{
		if (noEnergy && !noObstacles)
		{
			return "NO FAIL";
		}
		if (noEnergy && noObstacles)
		{
			return "NO FAIL and NO OBSTACLES";
		}
		if (!noEnergy && noObstacles)
		{
			return "NO OBSTACLES";
		}
		return string.Empty;
	}
}
