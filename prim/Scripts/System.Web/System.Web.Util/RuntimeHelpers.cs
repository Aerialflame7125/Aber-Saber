using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;

namespace System.Web.Util;

internal static class RuntimeHelpers
{
	public static bool CaseInsensitive { get; private set; }

	public static bool DebuggingEnabled
	{
		get
		{
			if (WebConfigurationManager.GetSection("system.web/compilation") is CompilationSection compilationSection)
			{
				return compilationSection.Debug;
			}
			return false;
		}
	}

	public static IEqualityComparer<string> StringEqualityComparer { get; private set; }

	public static IEqualityComparer<string> StringEqualityComparerCulture { get; private set; }

	public static bool IsUncShare { get; private set; }

	public static string MonoVersion { get; private set; }

	public static bool RunningOnWindows { get; private set; }

	public static StringComparison StringComparison { get; private set; }

	public static StringComparison StringComparisonCulture { get; private set; }

	static RuntimeHelpers()
	{
		PlatformID platform = Environment.OSVersion.Platform;
		RunningOnWindows = platform != (PlatformID)128 && platform != PlatformID.Unix && platform != PlatformID.MacOSX;
		if (RunningOnWindows)
		{
			CaseInsensitive = true;
			string text = AppDomain.CurrentDomain.GetData(".appPath") as string;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					IsUncShare = new Uri(text).IsUnc;
				}
				catch
				{
				}
			}
		}
		else
		{
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_IOMAP");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				if (environmentVariable == "all")
				{
					CaseInsensitive = true;
				}
				else
				{
					string[] array = environmentVariable.Split(':');
					foreach (string text2 in array)
					{
						if (text2 == "all" || text2 == "case")
						{
							CaseInsensitive = true;
							break;
						}
					}
				}
			}
		}
		if (CaseInsensitive)
		{
			StringEqualityComparer = StringComparer.OrdinalIgnoreCase;
			StringEqualityComparerCulture = StringComparer.CurrentCultureIgnoreCase;
			StringComparison = StringComparison.OrdinalIgnoreCase;
			StringComparisonCulture = StringComparison.CurrentCultureIgnoreCase;
		}
		else
		{
			StringEqualityComparer = StringComparer.Ordinal;
			StringEqualityComparerCulture = StringComparer.CurrentCulture;
			StringComparison = StringComparison.Ordinal;
			StringComparisonCulture = StringComparison.CurrentCulture;
		}
		string text3 = null;
		try
		{
			Type type = Type.GetType("Mono.Runtime", throwOnError: false);
			if (type != null)
			{
				MethodInfo method = type.GetMethod("GetDisplayName", BindingFlags.Static | BindingFlags.NonPublic);
				if (method != null)
				{
					text3 = method.Invoke(null, new object[0]) as string;
				}
			}
		}
		catch
		{
		}
		if (text3 == null)
		{
			text3 = Environment.Version.ToString();
		}
		MonoVersion = text3;
	}
}
