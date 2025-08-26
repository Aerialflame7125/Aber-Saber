using System.Diagnostics;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides a listener that routes all tracing and debugging output to the IIS 7.0 infrastructure.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
public sealed class IisTraceListener : TraceListener
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	public IisTraceListener()
	{
	}

	/// <summary>Writes trace information, a data object, and event information to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
	/// <param name="source">A name that identifies the output. This is typically the name of the application that generated the trace event.</param>
	/// <param name="eventType">A <see cref="T:System.Diagnostics.TraceEventType" /> value that specifies the type of event that caused the trace.</param>
	/// <param name="id">A numeric identifier for the event.</param>
	/// <param name="data">The trace data to write.</param>
	public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
	{
		base.TraceData(eventCache, source, eventType, id, data);
	}

	/// <summary>Writes trace information, an array of data objects, and event information to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
	/// <param name="source">A name that identifies the output. This is typically the name of the application that generated the trace event.</param>
	/// <param name="eventType">A <see cref="T:System.Diagnostics.TraceEventType" /> value that specifies the type of event that caused the trace.</param>
	/// <param name="id">A numeric identifier for the event.</param>
	/// <param name="data">An array of objects to write as data.</param>
	public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
	{
		base.TraceData(eventCache, source, eventType, id, data);
	}

	/// <summary>Writes trace and event information to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
	/// <param name="source">A name that identifies the output. This is typically the name of the application that generated the trace event.</param>
	/// <param name="severity">A <see cref="T:System.Diagnostics.TraceEventType" /> value that specifies the type of event that caused the trace.</param>
	/// <param name="id">A numeric identifier for the event.</param>
	/// <param name="message">A message to write.</param>
	public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
	{
		base.TraceEvent(eventCache, source, severity, id, message);
	}

	/// <summary>Writes trace information, a formatted array of objects, and event information to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
	/// <param name="source">A name that identifies the output. This is typically the name of the application that generated the trace event.</param>
	/// <param name="severity">A <see cref="T:System.Diagnostics.TraceEventType" /> value that specifies the type of event that caused the trace.</param>
	/// <param name="id">A numeric identifier for the event.</param>
	/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
	/// <param name="args">An object array that contains zero or more objects to format.</param>
	public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
	{
		base.TraceEvent(eventCache, source, severity, id, format, args);
	}

	/// <summary>Writes the specified message to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="message">The message to write.</param>
	public override void Write(string message)
	{
		HttpContext.Current.Trace.Write(message);
	}

	/// <summary>Writes the specified message and the specified category name to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="message">The message to write.</param>
	/// <param name="category">Ignored. The <see cref="T:System.Web.IisTraceListener" /> implementation of the <see cref="T:System.Diagnostics.TraceListener" /> class disregards the <paramref name="category" /> parameter.</param>
	public override void Write(string message, string category)
	{
		Write(message);
	}

	/// <summary>Writes the specified message to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="message">The message to write.</param>
	public override void WriteLine(string message)
	{
		HttpContext.Current.Trace.Write(message + Environment.NewLine);
	}

	/// <summary>Writes the specified message and category name to the output of the <see cref="T:System.Web.IisTraceListener" /> class.</summary>
	/// <param name="message">The message to write.</param>
	/// <param name="category">Ignored. The <see cref="T:System.Web.Iis7TraceListener" /> implementation of <see cref="T:System.Diagnostics.TraceListener" /> class disregards the <paramref name="category" /> parameter.</param>
	public override void WriteLine(string message, string category)
	{
		WriteLine(message);
	}
}
