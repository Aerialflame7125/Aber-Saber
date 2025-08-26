using System.Globalization;
using UnityEngine;

public class ScoreFormatter : MonoBehaviour
{
	private static NumberFormatInfo _numberFormatInfo;

	public static string Format(int score)
	{
		if (_numberFormatInfo == null)
		{
			_numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
			_numberFormatInfo.NumberGroupSeparator = " ";
		}
		return score.ToString("#,0", _numberFormatInfo);
	}
}
