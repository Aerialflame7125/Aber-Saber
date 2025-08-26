using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Util;

internal static class Debug
{
	[SuppressUnmanagedCodeSecurity]
	private static class NativeMethods
	{
		[DllImport("kernel32.dll")]
		internal static extern bool IsDebuggerPresent();
	}

	internal const string TAG_INTERNAL = "Internal";

	internal const string TAG_EXTERNAL = "External";

	internal const string TAG_ALL = "*";

	internal const string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss.ffff";

	internal const string TIME_FORMAT = "HH:mm:ss:ffff";

	[Conditional("DBG")]
	internal static void Trace(string tagName, string message)
	{
	}

	[Conditional("DBG")]
	internal static void Trace(string tagName, string message, bool includePrefix)
	{
	}

	[Conditional("DBG")]
	internal static void Trace(string tagName, string message, Exception e)
	{
	}

	[Conditional("DBG")]
	internal static void Trace(string tagName, Exception e)
	{
	}

	[Conditional("DBG")]
	internal static void Trace(string tagName, string message, Exception e, bool includePrefix)
	{
	}

	[Conditional("DBG")]
	public static void TraceException(string tagName, Exception e)
	{
	}

	[Conditional("DBG")]
	internal static void Assert(bool assertion, string message)
	{
	}

	[Conditional("DBG")]
	internal static void Assert(bool assertion)
	{
	}

	[Conditional("DBG")]
	internal static void Fail(string message)
	{
	}

	internal static bool IsTagEnabled(string tagName)
	{
		return false;
	}

	internal static bool IsTagPresent(string tagName)
	{
		return false;
	}

	internal static bool IsDebuggerPresent()
	{
		if (!NativeMethods.IsDebuggerPresent())
		{
			return Debugger.IsAttached;
		}
		return true;
	}

	[Conditional("DBG")]
	internal static void Break()
	{
	}

	[Conditional("DBG")]
	internal static void AlwaysValidate(string tagName)
	{
	}

	[Conditional("DBG")]
	internal static void CheckValid(bool assertion, string message)
	{
	}

	[Conditional("DBG")]
	internal static void Validate(object obj)
	{
	}

	[Conditional("DBG")]
	internal static void ValidateArrayBounds<T>(T[] array, int offset, int count)
	{
	}

	[Conditional("DBG")]
	internal static void Validate(string tagName, object obj)
	{
	}

	[Conditional("DBG")]
	internal static void Dump(string tagName, object obj)
	{
	}

	internal static string FormatUtcDate(DateTime utcTime)
	{
		return string.Empty;
	}

	internal static string FormatLocalDate(DateTime localTime)
	{
		return string.Empty;
	}
}
