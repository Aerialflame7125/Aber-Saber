using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics;

[RequiredByNativeCode]
[NativeHeader("UnityConnectScriptingClasses.h")]
[NativeHeader("Modules/UnityConnect/UnityConnectClient.h")]
public static class AnalyticsSessionInfo
{
	public delegate void SessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged);

	public static extern AnalyticsSessionState sessionState
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetPlayerSessionState")]
		get;
	}

	public static extern long sessionId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetPlayerSessionId")]
		get;
	}

	public static extern long sessionElapsedTime
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetPlayerSessionElapsedTime")]
		get;
	}

	public static extern string userId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetUserId")]
		get;
	}

	public static event SessionStateChanged sessionStateChanged;

	[RequiredByNativeCode]
	internal static void CallSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
	{
		AnalyticsSessionInfo.sessionStateChanged?.Invoke(sessionState, sessionId, sessionElapsedTime, sessionChanged);
	}
}
