using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Diagnostics;

internal static class Tracing
{
	private static bool tracingEnabled = true;

	private static bool tracingInitialized;

	private static bool appDomainShutdown;

	private const string TraceSourceAsmx = "System.Web.Services.Asmx";

	private static TraceSource asmxTraceSource;

	private static object internalSyncObject;

	private static object InternalSyncObject
	{
		get
		{
			if (internalSyncObject == null)
			{
				object value = new object();
				Interlocked.CompareExchange(ref internalSyncObject, value, null);
			}
			return internalSyncObject;
		}
	}

	internal static bool On
	{
		get
		{
			if (!tracingInitialized)
			{
				InitializeLogging();
			}
			return tracingEnabled;
		}
	}

	internal static bool IsVerbose => ValidateSettings(Asmx, TraceEventType.Verbose);

	internal static TraceSource Asmx
	{
		get
		{
			if (!tracingInitialized)
			{
				InitializeLogging();
			}
			if (!tracingEnabled)
			{
				return null;
			}
			return asmxTraceSource;
		}
	}

	private static void InitializeLogging()
	{
		lock (InternalSyncObject)
		{
			if (!tracingInitialized)
			{
				bool flag = false;
				asmxTraceSource = new TraceSource("System.Web.Services.Asmx");
				if (asmxTraceSource.Switch.ShouldTrace(TraceEventType.Critical))
				{
					flag = true;
					AppDomain currentDomain = AppDomain.CurrentDomain;
					currentDomain.UnhandledException += UnhandledExceptionHandler;
					currentDomain.DomainUnload += AppDomainUnloadEvent;
					currentDomain.ProcessExit += ProcessExitEvent;
				}
				tracingEnabled = flag;
				tracingInitialized = true;
			}
		}
	}

	private static void Close()
	{
		if (asmxTraceSource != null)
		{
			asmxTraceSource.Close();
		}
	}

	private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
	{
		Exception e = (Exception)args.ExceptionObject;
		ExceptionCatch(TraceEventType.Error, sender, "UnhandledExceptionHandler", e);
	}

	private static void ProcessExitEvent(object sender, EventArgs e)
	{
		Close();
		appDomainShutdown = true;
	}

	private static void AppDomainUnloadEvent(object sender, EventArgs e)
	{
		Close();
		appDomainShutdown = true;
	}

	private static bool ValidateSettings(TraceSource traceSource, TraceEventType traceLevel)
	{
		if (!tracingEnabled)
		{
			return false;
		}
		if (!tracingInitialized)
		{
			InitializeLogging();
		}
		if (traceSource == null || !traceSource.Switch.ShouldTrace(traceLevel))
		{
			return false;
		}
		if (appDomainShutdown)
		{
			return false;
		}
		return true;
	}

	internal static void Information(string format, params object[] args)
	{
		if (ValidateSettings(Asmx, TraceEventType.Information))
		{
			TraceEvent(TraceEventType.Information, Res.GetString(format, args));
		}
	}

	private static void TraceEvent(TraceEventType eventType, string format)
	{
	}

	internal static Exception ExceptionThrow(TraceMethod method, Exception e)
	{
		return ExceptionThrow(TraceEventType.Error, method, e);
	}

	internal static Exception ExceptionThrow(TraceEventType eventType, TraceMethod method, Exception e)
	{
		if (!ValidateSettings(Asmx, eventType))
		{
			return e;
		}
		TraceEvent(eventType, Res.GetString("TraceExceptionThrown", method.ToString(), e.GetType(), e.Message));
		StackTrace(eventType, e);
		return e;
	}

	internal static Exception ExceptionCatch(TraceMethod method, Exception e)
	{
		return ExceptionCatch(TraceEventType.Error, method, e);
	}

	internal static Exception ExceptionCatch(TraceEventType eventType, TraceMethod method, Exception e)
	{
		if (!ValidateSettings(Asmx, eventType))
		{
			return e;
		}
		TraceEvent(eventType, Res.GetString("TraceExceptionCought", method, e.GetType(), e.Message));
		StackTrace(eventType, e);
		return e;
	}

	internal static Exception ExceptionCatch(TraceEventType eventType, object target, string method, Exception e)
	{
		if (!ValidateSettings(Asmx, eventType))
		{
			return e;
		}
		TraceEvent(eventType, Res.GetString("TraceExceptionCought", TraceMethod.MethodId(target, method), e.GetType(), e.Message));
		StackTrace(eventType, e);
		return e;
	}

	internal static Exception ExceptionIgnore(TraceEventType eventType, TraceMethod method, Exception e)
	{
		if (!ValidateSettings(Asmx, eventType))
		{
			return e;
		}
		TraceEvent(eventType, Res.GetString("TraceExceptionIgnored", method, e.GetType(), e.Message));
		StackTrace(eventType, e);
		return e;
	}

	private static void StackTrace(TraceEventType eventType, Exception e)
	{
		if (IsVerbose && !string.IsNullOrEmpty(e.StackTrace))
		{
			TraceEvent(eventType, Res.GetString("TraceExceptionDetails", e.ToString()));
		}
	}

	internal static string TraceId(string id)
	{
		return Res.GetString(id);
	}

	private static string GetHostByAddress(string ipAddress)
	{
		try
		{
			return Dns.GetHostByAddress(ipAddress).HostName;
		}
		catch
		{
			return null;
		}
	}

	internal static List<string> Details(HttpRequest request)
	{
		if (request == null)
		{
			return null;
		}
		List<string> list = null;
		list = new List<string>();
		list.Add(Res.GetString("TraceUserHostAddress", request.UserHostAddress));
		string text = ((request.UserHostAddress == request.UserHostName) ? GetHostByAddress(request.UserHostAddress) : request.UserHostName);
		if (!string.IsNullOrEmpty(text))
		{
			list.Add(Res.GetString("TraceUserHostName", text));
		}
		list.Add(Res.GetString("TraceUrl", request.HttpMethod, request.Url));
		if (request.UrlReferrer != null)
		{
			list.Add(Res.GetString("TraceUrlReferrer", request.UrlReferrer));
		}
		return list;
	}

	internal static void Enter(string callId, TraceMethod caller)
	{
		Enter(callId, caller, null, null);
	}

	internal static void Enter(string callId, TraceMethod caller, List<string> details)
	{
		Enter(callId, caller, null, details);
	}

	internal static void Enter(string callId, TraceMethod caller, TraceMethod callDetails)
	{
		Enter(callId, caller, callDetails, null);
	}

	internal static void Enter(string callId, TraceMethod caller, TraceMethod callDetails, List<string> details)
	{
		if (!ValidateSettings(Asmx, TraceEventType.Information))
		{
			return;
		}
		string text = ((callDetails == null) ? Res.GetString("TraceCallEnter", callId, caller) : Res.GetString("TraceCallEnterDetails", callId, caller, callDetails));
		if (details != null && details.Count > 0)
		{
			StringBuilder stringBuilder = new StringBuilder(text);
			foreach (string detail in details)
			{
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("    ");
				stringBuilder.Append(detail);
			}
			text = stringBuilder.ToString();
		}
		TraceEvent(TraceEventType.Information, text);
	}

	internal static XmlDeserializationEvents GetDeserializationEvents()
	{
		XmlDeserializationEvents result = default(XmlDeserializationEvents);
		result.OnUnknownElement = OnUnknownElement;
		result.OnUnknownAttribute = OnUnknownAttribute;
		return result;
	}

	internal static void Exit(string callId, TraceMethod caller)
	{
		if (ValidateSettings(Asmx, TraceEventType.Information))
		{
			TraceEvent(TraceEventType.Information, Res.GetString("TraceCallExit", callId, caller));
		}
	}

	internal static void OnUnknownElement(object sender, XmlElementEventArgs e)
	{
		if (ValidateSettings(Asmx, TraceEventType.Warning) && e.Element != null)
		{
			string text = RuntimeUtils.ElementString(e.Element);
			string name = ((e.ExpectedElements == null) ? "WebUnknownElement" : ((e.ExpectedElements.Length == 0) ? "WebUnknownElement1" : "WebUnknownElement2"));
			TraceEvent(TraceEventType.Warning, Res.GetString(name, text, e.ExpectedElements));
		}
	}

	internal static void OnUnknownAttribute(object sender, XmlAttributeEventArgs e)
	{
		if (ValidateSettings(Asmx, TraceEventType.Warning) && e.Attr != null && !RuntimeUtils.IsKnownNamespace(e.Attr.NamespaceURI))
		{
			string name = ((e.ExpectedAttributes == null) ? "WebUnknownAttribute" : ((e.ExpectedAttributes.Length == 0) ? "WebUnknownAttribute2" : "WebUnknownAttribute3"));
			TraceEvent(TraceEventType.Warning, Res.GetString(name, e.Attr.Name, e.Attr.Value, e.ExpectedAttributes));
		}
	}
}
