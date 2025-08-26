using System.Collections;
using System.Diagnostics;

namespace System.Web.Util;

internal class WebTrace
{
	private static Stack ctxStack;

	private static bool trace;

	public static string Context
	{
		get
		{
			if (ctxStack.Count == 0)
			{
				return "No context";
			}
			return (string)ctxStack.Peek();
		}
	}

	public static bool StackTrace
	{
		get
		{
			return trace;
		}
		set
		{
			trace = value;
		}
	}

	static WebTrace()
	{
		ctxStack = new Stack();
	}

	[Conditional("WEBTRACE")]
	public static void PushContext(string context)
	{
		ctxStack.Push(context);
	}

	[Conditional("WEBTRACE")]
	public static void PopContext()
	{
		if (ctxStack.Count != 0)
		{
			ctxStack.Pop();
		}
	}

	[Conditional("WEBTRACE")]
	public static void WriteLine(string msg)
	{
	}

	[Conditional("WEBTRACE")]
	public static void WriteLine(string msg, object arg)
	{
	}

	[Conditional("WEBTRACE")]
	public static void WriteLine(string msg, object arg1, object arg2)
	{
	}

	[Conditional("WEBTRACE")]
	public static void WriteLine(string msg, object arg1, object arg2, object arg3)
	{
	}

	[Conditional("WEBTRACE")]
	public static void WriteLine(string msg, params object[] args)
	{
	}

	private static string Format(string msg)
	{
		if (trace)
		{
			return $"{Context}: {msg}\n{Environment.StackTrace}";
		}
		return $"{Context}: {msg}";
	}
}
