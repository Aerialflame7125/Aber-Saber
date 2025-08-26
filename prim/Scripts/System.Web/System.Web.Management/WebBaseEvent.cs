namespace System.Web.Management;

/// <summary>Defines the base class for the ASP.NET health-monitoring events.</summary>
public class WebBaseEvent
{
	private string message;

	private object event_source;

	private int event_code;

	private int event_detail_code;

	/// <summary>Gets a <see cref="T:System.Web.Management.WebApplicationInformation" /> object that contains information about the current application being monitored.</summary>
	/// <returns>A <see cref="T:System.Web.Management.WebApplicationInformation" /> object that contains information about the application being monitored.</returns>
	public static WebApplicationInformation ApplicationInformation
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the code value associated with the event.</summary>
	/// <returns>One of the <see cref="T:System.Web.Management.WebEventCodes" /> values.</returns>
	public int EventCode => event_code;

	/// <summary>Gets the event detail code.</summary>
	/// <returns>The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</returns>
	public int EventDetailCode => event_detail_code;

	/// <summary>Gets the identifier associated with the event.</summary>
	/// <returns>The identifier associated with the event.</returns>
	public Guid EventID
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of times the event has been raised by the application.</summary>
	/// <returns>The number of times the event has been raised.</returns>
	public long EventSequence
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the object that raises the event.</summary>
	/// <returns>The object that raised the event.</returns>
	public object EventSource => event_source;

	/// <summary>Gets the time when the event was raised.</summary>
	/// <returns>The time that the event was raised.</returns>
	public DateTime EventTime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time when the event was raised.</summary>
	/// <returns>The time of the event in Coordinated Universal Time (UTC) format.</returns>
	public DateTime EventTimeUtc
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the message that describes the event.</summary>
	/// <returns>The description of the event.</returns>
	public string Message => message;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEvent" /> class using the supplied parameters.</summary>
	/// <param name="message">The description of the event. </param>
	/// <param name="eventSource">The object that raised the event. </param>
	/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
	protected WebBaseEvent(string message, object eventSource, int eventCode)
	{
		this.message = message;
		event_source = eventSource;
		event_code = eventCode;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEvent" /> class using the supplied parameters.</summary>
	/// <param name="message">The description of the raised event. </param>
	/// <param name="eventSource">The object that raised the event. </param>
	/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
	/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
	protected WebBaseEvent(string message, object eventSource, int eventCode, int eventDetailCode)
	{
		this.message = message;
		event_source = eventSource;
		event_code = eventCode;
		event_detail_code = eventDetailCode;
	}

	/// <summary>Provides standard formatting of the event information.</summary>
	/// <param name="formatter">A <see cref="T:System.Web.Management.WebEventFormatter" /> object that contains the formatted event information.</param>
	public virtual void FormatCustomEventDetails(WebEventFormatter formatter)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises an event by notifying any configured provider that the event has occurred.</summary>
	public virtual void Raise()
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the specified event by notifying any configured provider that the event has occurred.</summary>
	/// <param name="eventRaised">A <see cref="T:System.Web.Management.WebBaseEvent" /> object. </param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Management.WebBaseEvent.EventCode" /> property of <paramref name="eventRaised" /> has a value that is reserved for ASP.NET.</exception>
	public static void Raise(WebBaseEvent eventRaised)
	{
		throw new NotImplementedException();
	}

	/// <summary>Formats event information for display purposes.</summary>
	/// <returns>The event information.</returns>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Formats event information for display purposes.</summary>
	/// <param name="includeAppInfo">
	///       <see langword="true" /> if standard application information must be displayed as part of the event information; otherwise, <see langword="false" />. </param>
	/// <param name="includeCustomEventDetails">
	///       <see langword="true" /> if custom information must be displayed as part of the event information; otherwise, <see langword="false" />.</param>
	/// <returns>The event information.</returns>
	public virtual string ToString(bool includeAppInfo, bool includeCustomEventDetails)
	{
		throw new NotImplementedException();
	}
}
