using System;
using System.Collections;
using UnityEngine;

public static class ExtensionMethods
{
	public static bool ContainsLayer(this LayerMask layerMask, int layer)
	{
		return (layerMask.value & (1 << layer)) != 0;
	}

	public static Color SaturatedColor(this Color color, float saturation)
	{
		float h;
		float v;
		float s;
		RGBToHSV(color, out h, out s, out v);
		s = saturation;
		return HSVToRGB(h, s, v);
	}

	public static Color ColorWithAlpha(this Color color, float alpha)
	{
		color.a = alpha;
		return color;
	}

	public static Color ColorWithValue(this Color color, float value)
	{
		float h;
		float s;
		float v;
		RGBToHSV(color, out h, out s, out v);
		v = value;
		return HSVToRGB(h, s, v);
	}

	public static void RGBToHSV(Color c, out float h, out float s, out float v)
	{
		float r = c.r;
		float g = c.g;
		float b = c.b;
		float num = Mathf.Min(Mathf.Min(r, g), b);
		float num2 = (v = Mathf.Max(Mathf.Max(r, g), b));
		float num3 = num2 - num;
		if (num2 != 0f)
		{
			s = num3 / num2;
			if (r == num2)
			{
				h = (g - b) / num3;
			}
			else if (g == num2)
			{
				h = 2f + (b - r) / num3;
			}
			else
			{
				h = 4f + (r - g) / num3;
			}
			h *= 60f;
			if (h < 0f)
			{
				h += 360f;
			}
		}
		else
		{
			v = 0f;
			s = 0f;
			h = 0f;
		}
	}

	public static Color HSVToRGB(float h, float s, float v)
	{
		while (h < 0f)
		{
			h += 360f;
		}
		while (h >= 360f)
		{
			h -= 360f;
		}
		float num = h / 60f;
		int num2 = Mathf.FloorToInt(num);
		float num3 = num - (float)num2;
		float num4 = v * (1f - s);
		float num5 = v * (1f - s * num3);
		float num6 = v * (1f - s * (1f - num3));
		switch (num2)
		{
		case 0:
			return new Color(v, num6, num4);
		case 1:
			return new Color(num5, v, num4);
		case 2:
			return new Color(num4, v, num6);
		case 3:
			return new Color(num4, num5, v);
		case 4:
			return new Color(num6, num4, v);
		case 5:
			return new Color(v, num4, num5);
		case 6:
			return new Color(v, num6, num4);
		case -1:
			return new Color(v, num4, num5);
		default:
			return new Color(v, v, v);
		}
	}

	public static Coroutine StartUniqueCoroutine(this MonoBehaviour m, Func<IEnumerator> func)
	{
		m.StopCoroutine(func.Method.Name);
		return m.StartCoroutine(func.Method.Name);
	}

	public static Coroutine StartUniqueCoroutine<T>(this MonoBehaviour m, Func<T, IEnumerator> func, T value)
	{
		m.StopCoroutine(func.Method.Name);
		return m.StartCoroutine(func.Method.Name, value);
	}

	public static void StopUniqueCoroutine(this MonoBehaviour m, Func<IEnumerator> func)
	{
		m.StopCoroutine(func.Method.Name);
	}

	public static void StopUniqueCoroutine<T>(this MonoBehaviour m, Func<T, IEnumerator> func)
	{
		m.StopCoroutine(func.Method.Name);
	}

	public static bool IsDescendantOf(this Transform transform, Transform parent)
	{
		while (transform != null && transform != parent)
		{
			transform = transform.parent;
		}
		return transform == parent;
	}
}
