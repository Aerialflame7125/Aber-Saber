using System;

namespace LIV.SDK.Unity;

internal static class SteamVRCompatibility
{
	public static bool IsAvailable;

	public static Type SteamVRCamera;

	public static Type SteamVRExternalCamera;

	public static Type SteamVRFade;

	static SteamVRCompatibility()
	{
		IsAvailable = FindSteamVRAsset();
	}

	private static bool FindSteamVRAsset()
	{
		SteamVRCamera = Type.GetType("SteamVR_Camera", throwOnError: false);
		SteamVRExternalCamera = Type.GetType("SteamVR_ExternalCamera", throwOnError: false);
		SteamVRFade = Type.GetType("SteamVR_Fade", throwOnError: false);
		return SteamVRCamera != null && SteamVRExternalCamera != null && SteamVRFade != null;
	}
}
