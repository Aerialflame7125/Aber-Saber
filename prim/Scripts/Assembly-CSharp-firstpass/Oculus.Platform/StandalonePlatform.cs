using System;
using System.Runtime.InteropServices;
using Oculus.Platform.Models;
using UnityEngine;

namespace Oculus.Platform;

public sealed class StandalonePlatform
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void UnityLogDelegate(IntPtr tag, IntPtr msg);

	public Request<PlatformInitialize> InitializeInEditor()
	{
		if (string.IsNullOrEmpty(PlatformSettings.AppID))
		{
			throw new UnityException("Update your App ID by selecting 'Oculus Platform' -> 'Edit Settings'");
		}
		string appID = PlatformSettings.AppID;
		if (string.IsNullOrEmpty(StandalonePlatformSettings.OculusPlatformTestUserAccessToken))
		{
			throw new UnityException("Update your standalone credentials by selecting 'Oculus Platform' -> 'Edit Settings'");
		}
		string oculusPlatformTestUserAccessToken = StandalonePlatformSettings.OculusPlatformTestUserAccessToken;
		CAPI.ovr_UnityResetTestPlatform();
		CAPI.ovr_UnityInitGlobals(IntPtr.Zero);
		return new Request<PlatformInitialize>(CAPI.ovr_PlatformInitializeWithAccessToken(ulong.Parse(appID), oculusPlatformTestUserAccessToken));
	}
}
