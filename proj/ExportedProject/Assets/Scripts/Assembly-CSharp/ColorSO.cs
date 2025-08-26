using UnityEngine;

public abstract class ColorSO : ScriptableObject
{
	public abstract Color color { get; }

	public static implicit operator Color(ColorSO c)
	{
		if (c == null)
		{
			return Color.clear;
		}
		return c.color;
	}
}
