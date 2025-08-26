using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("UnityConnectScriptingClasses.h")]
[NativeHeader("Modules/UnityConnect/RemoteSettings.h")]
public static class RemoteSettings
{
	public delegate void UpdatedEventHandler();

	public static event UpdatedEventHandler Updated;

	public static event Action BeforeFetchFromServer;

	public static event Action<bool, bool, int> Completed;

	[RequiredByNativeCode]
	internal static void RemoteSettingsUpdated(bool wasLastUpdatedFromServer)
	{
		RemoteSettings.Updated?.Invoke();
	}

	[RequiredByNativeCode]
	internal static void RemoteSettingsBeforeFetchFromServer()
	{
		RemoteSettings.BeforeFetchFromServer?.Invoke();
	}

	[RequiredByNativeCode]
	internal static void RemoteSettingsUpdateCompleted(bool wasLastUpdatedFromServer, bool settingsChanged, int response)
	{
		RemoteSettings.Completed?.Invoke(wasLastUpdatedFromServer, settingsChanged, response);
	}

	[Obsolete("Calling CallOnUpdate() is not necessary any more and should be removed. Use RemoteSettingsUpdated instead", true)]
	public static void CallOnUpdate()
	{
		throw new NotSupportedException("Calling CallOnUpdate() is not necessary any more and should be removed.");
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void ForceUpdate();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool WasLastUpdatedFromServer();

	[ExcludeFromDocs]
	public static int GetInt(string key)
	{
		return GetInt(key, 0);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern int GetInt(string key, [DefaultValue("0")] int defaultValue);

	[ExcludeFromDocs]
	public static long GetLong(string key)
	{
		return GetLong(key, 0L);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern long GetLong(string key, [DefaultValue("0")] long defaultValue);

	[ExcludeFromDocs]
	public static float GetFloat(string key)
	{
		return GetFloat(key, 0f);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern float GetFloat(string key, [DefaultValue("0.0F")] float defaultValue);

	[ExcludeFromDocs]
	public static string GetString(string key)
	{
		return GetString(key, "");
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern string GetString(string key, [DefaultValue("\"\"")] string defaultValue);

	[ExcludeFromDocs]
	public static bool GetBool(string key)
	{
		return GetBool(key, defaultValue: false);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool GetBool(string key, [DefaultValue("false")] bool defaultValue);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool HasKey(string key);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern int GetCount();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern string[] GetKeys();
}
