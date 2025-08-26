using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UnityEngine.Analytics;

public static class Analytics
{
	private static UnityAnalyticsHandler s_UnityAnalyticsHandler;

	public static bool limitUserTracking
	{
		get
		{
			return UnityAnalyticsHandler.limitUserTracking;
		}
		set
		{
			UnityAnalyticsHandler.limitUserTracking = value;
		}
	}

	public static bool deviceStatsEnabled
	{
		get
		{
			return UnityAnalyticsHandler.deviceStatsEnabled;
		}
		set
		{
			UnityAnalyticsHandler.deviceStatsEnabled = value;
		}
	}

	public static bool enabled
	{
		get
		{
			return GetUnityAnalyticsHandler()?.enabled ?? false;
		}
		set
		{
			UnityAnalyticsHandler unityAnalyticsHandler = GetUnityAnalyticsHandler();
			if (unityAnalyticsHandler != null)
			{
				unityAnalyticsHandler.enabled = value;
			}
		}
	}

	internal static UnityAnalyticsHandler GetUnityAnalyticsHandler()
	{
		if (s_UnityAnalyticsHandler == null)
		{
			s_UnityAnalyticsHandler = new UnityAnalyticsHandler();
		}
		if (s_UnityAnalyticsHandler.IsInitialized())
		{
			return s_UnityAnalyticsHandler;
		}
		return null;
	}

	public static AnalyticsResult FlushEvents()
	{
		UnityAnalyticsHandler unityAnalyticsHandler = GetUnityAnalyticsHandler();
		if (unityAnalyticsHandler == null)
		{
			return AnalyticsResult.NotInitialized;
		}
		return (!unityAnalyticsHandler.FlushEvents()) ? AnalyticsResult.NotInitialized : AnalyticsResult.Ok;
	}

	public static AnalyticsResult SetUserId(string userId)
	{
		if (string.IsNullOrEmpty(userId))
		{
			throw new ArgumentException("Cannot set userId to an empty or null string");
		}
		return GetUnityAnalyticsHandler()?.SetUserId(userId) ?? AnalyticsResult.NotInitialized;
	}

	public static AnalyticsResult SetUserGender(Gender gender)
	{
		return GetUnityAnalyticsHandler()?.SetUserGender(gender) ?? AnalyticsResult.NotInitialized;
	}

	public static AnalyticsResult SetUserBirthYear(int birthYear)
	{
		UnityAnalyticsHandler unityAnalyticsHandler = GetUnityAnalyticsHandler();
		if (s_UnityAnalyticsHandler == null)
		{
			return AnalyticsResult.NotInitialized;
		}
		return unityAnalyticsHandler.SetUserBirthYear(birthYear);
	}

	public static AnalyticsResult Transaction(string productId, decimal amount, string currency)
	{
		return Transaction(productId, amount, currency, null, null, usingIAPService: false);
	}

	public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature)
	{
		return Transaction(productId, amount, currency, receiptPurchaseData, signature, usingIAPService: false);
	}

	public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService)
	{
		if (string.IsNullOrEmpty(productId))
		{
			throw new ArgumentException("Cannot set productId to an empty or null string");
		}
		if (string.IsNullOrEmpty(currency))
		{
			throw new ArgumentException("Cannot set currency to an empty or null string");
		}
		UnityAnalyticsHandler unityAnalyticsHandler = GetUnityAnalyticsHandler();
		if (unityAnalyticsHandler == null)
		{
			return AnalyticsResult.NotInitialized;
		}
		if (receiptPurchaseData == null)
		{
			receiptPurchaseData = string.Empty;
		}
		if (signature == null)
		{
			signature = string.Empty;
		}
		return unityAnalyticsHandler.Transaction(productId, Convert.ToDouble(amount), currency, receiptPurchaseData, signature, usingIAPService);
	}

	public static AnalyticsResult CustomEvent(string customEventName)
	{
		if (string.IsNullOrEmpty(customEventName))
		{
			throw new ArgumentException("Cannot set custom event name to an empty or null string");
		}
		return GetUnityAnalyticsHandler()?.SendCustomEventName(customEventName) ?? AnalyticsResult.NotInitialized;
	}

	public static AnalyticsResult CustomEvent(string customEventName, Vector3 position)
	{
		if (string.IsNullOrEmpty(customEventName))
		{
			throw new ArgumentException("Cannot set custom event name to an empty or null string");
		}
		UnityAnalyticsHandler unityAnalyticsHandler = GetUnityAnalyticsHandler();
		if (unityAnalyticsHandler == null)
		{
			return AnalyticsResult.NotInitialized;
		}
		CustomEventData customEventData = new CustomEventData(customEventName);
		customEventData.AddDouble("x", (double)Convert.ToDecimal(position.x));
		customEventData.AddDouble("y", (double)Convert.ToDecimal(position.y));
		customEventData.AddDouble("z", (double)Convert.ToDecimal(position.z));
		return unityAnalyticsHandler.SendCustomEvent(customEventData);
	}

	public static AnalyticsResult CustomEvent(string customEventName, IDictionary<string, object> eventData)
	{
		if (string.IsNullOrEmpty(customEventName))
		{
			throw new ArgumentException("Cannot set custom event name to an empty or null string");
		}
		UnityAnalyticsHandler unityAnalyticsHandler = GetUnityAnalyticsHandler();
		if (unityAnalyticsHandler == null)
		{
			return AnalyticsResult.NotInitialized;
		}
		if (eventData == null)
		{
			return unityAnalyticsHandler.SendCustomEventName(customEventName);
		}
		CustomEventData customEventData = new CustomEventData(customEventName);
		customEventData.AddDictionary(eventData);
		return unityAnalyticsHandler.SendCustomEvent(customEventData);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey = "", string prefix = "")
	{
		string empty = string.Empty;
		empty = Assembly.GetCallingAssembly().FullName;
		return RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, 1, prefix, empty);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix = "")
	{
		string empty = string.Empty;
		empty = Assembly.GetCallingAssembly().FullName;
		return RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, empty);
	}

	private static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo)
	{
		if (string.IsNullOrEmpty(eventName))
		{
			throw new ArgumentException("Cannot set event name to an empty or null string");
		}
		return GetUnityAnalyticsHandler()?.RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, assemblyInfo) ?? AnalyticsResult.NotInitialized;
	}

	public static AnalyticsResult SendEvent(string eventName, object parameters, int ver = 1, string prefix = "")
	{
		if (string.IsNullOrEmpty(eventName))
		{
			throw new ArgumentException("Cannot set event name to an empty or null string");
		}
		if (parameters == null)
		{
			throw new ArgumentException("Cannot set parameters to null");
		}
		return GetUnityAnalyticsHandler()?.SendEvent(eventName, parameters, ver, prefix) ?? AnalyticsResult.NotInitialized;
	}
}
