using System;
using System.Collections.Generic;

namespace UnityEngine.Timeline;

internal static class TimelineCreateUtilities
{
	public static string GenerateUniqueActorName(List<ScriptableObject> tracks, string name)
	{
		if (!tracks.Exists((ScriptableObject x) => (object)x != null && x.name == name))
		{
			return name;
		}
		int result = 0;
		string text = name;
		if (!string.IsNullOrEmpty(name) && name[name.Length - 1] == ')')
		{
			int num = name.LastIndexOf('(');
			if (num > 0)
			{
				string s = name.Substring(num + 1, name.Length - num - 2);
				if (int.TryParse(s, out result))
				{
					result++;
					text = name.Substring(0, num);
				}
			}
		}
		text = text.TrimEnd();
		for (int i = result; i < result + 5000; i++)
		{
			if (i > 0)
			{
				string result2 = $"{text} ({i})";
				if (!tracks.Exists((ScriptableObject x) => (object)x != null && x.name == result2))
				{
					return result2;
				}
			}
		}
		return name;
	}

	public static void SaveAssetIntoObject(Object childAsset, Object masterAsset)
	{
		if ((masterAsset.hideFlags & HideFlags.DontSave) != 0)
		{
			childAsset.hideFlags |= HideFlags.DontSave;
		}
		else
		{
			childAsset.hideFlags |= HideFlags.HideInHierarchy;
		}
	}

	internal static bool ValidateParentTrack(TrackAsset parent, Type childType)
	{
		if ((object)childType == null || !typeof(TrackAsset).IsAssignableFrom(childType))
		{
			return false;
		}
		if (parent == null)
		{
			return true;
		}
		if (!(Attribute.GetCustomAttribute(parent.GetType(), typeof(SupportsChildTracksAttribute)) is SupportsChildTracksAttribute supportsChildTracksAttribute))
		{
			return false;
		}
		if ((object)supportsChildTracksAttribute.childType == null)
		{
			return true;
		}
		if ((object)childType == supportsChildTracksAttribute.childType)
		{
			int num = 0;
			TrackAsset trackAsset = parent;
			while (trackAsset != null && trackAsset.isSubTrack)
			{
				num++;
				trackAsset = trackAsset.parent as TrackAsset;
			}
			return num < supportsChildTracksAttribute.levels;
		}
		return false;
	}
}
