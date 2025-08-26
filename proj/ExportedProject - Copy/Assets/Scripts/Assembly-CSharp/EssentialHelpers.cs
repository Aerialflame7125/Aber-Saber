using System;
using UnityEngine;

public static class EssentialHelpers
{
	public static double CurrentTimeStamp
	{
		get
		{
			return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		}
	}

	public static void SafeDestroy(UnityEngine.Object obj)
	{
		if (!(obj == null))
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(obj);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}
	}
}
