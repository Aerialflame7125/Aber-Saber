using System.Diagnostics;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web;

/// <summary>Provides a listener that directs <see cref="T:System.Diagnostics.Trace" /> messages to ASP.NET Web page outputs. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class WebPageTraceListener : TraceListener
{
	/// <summary>Writes an event message to a Web page or to the ASP.NET trace viewer using the specified system and event data.</summary>
	/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process and  thread IDs and stack trace information.</param>
	/// <param name="source">A category name used to organize the output. </param>
	/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
	/// <param name="id">A numeric identifier for the event.</param>
	/// <param name="message">A message to write.</param>
	public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
	{
		if (HttpContext.Current != null && HttpContext.Current.Trace != null)
		{
			HttpContext.Current.Trace.Write(source, message);
		}
	}

	/// <summary>Writes a localized event message to a Web page or to the ASP.NET trace viewer using the specified system and event data.</summary>
	/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process and thread IDs and stack trace information.</param>
	/// <param name="source">A category name used to organize the output. </param>
	/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
	/// <param name="id">A numeric identifier for the event.</param>
	/// <param name="format">A format string that contains zero or more format items, which correspond to objects in <paramref name="args" />.</param>
	/// <param name="args">An array of zero or more objects to format.</param>
	public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
	{
		TraceEvent(eventCache, source, severity, id, string.Format(Helpers.InvariantCulture, format, args));
	}

	/// <summary>Writes a message to a Web page or to the ASP.NET trace viewer.</summary>
	/// <param name="message">The message to write.</param>
	public override void Write(string message)
	{
		if (HttpContext.Current != null && HttpContext.Current.Trace != null)
		{
			HttpContext.Current.Trace.Write(message);
		}
	}

	/// <summary>Writes a category name and a message to a Web page or to the ASP.NET trace viewer.</summary>
	/// <param name="message">The message to write.</param>
	/// <param name="category">A category name used to organize the output.</param>
	public override void Write(string message, string category)
	{
		if (HttpContext.Current != null && HttpContext.Current.Trace != null)
		{
			HttpContext.Current.Trace.Write(category, message);
		}
	}

	/// <summary>Writes a message to a Web page or to the ASP.NET trace viewer.</summary>
	/// <param name="message">The message to write.</param>
	public override void WriteLine(string message)
	{
		if (HttpContext.Current != null && HttpContext.Current.Trace != null)
		{
			HttpContext.Current.Trace.Write(message);
		}
	}

	/// <summary>Writes a category name and a message to a Web page or to the ASP.NET trace viewer.</summary>
	/// <param name="message">The message to write.</param>
	/// <param name="category">A category name used to organize the output.</param>
	public override void WriteLine(string message, string category)
	{
		if (HttpContext.Current != null && HttpContext.Current.Trace != null)
		{
			HttpContext.Current.Trace.Write(category, message);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.WebPageTraceListener" /> class.</summary>
	public WebPageTraceListener()
	{
	}
}
