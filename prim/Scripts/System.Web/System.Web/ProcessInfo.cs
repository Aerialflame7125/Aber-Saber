using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides information on processes currently executing.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ProcessInfo
{
	private TimeSpan age;

	private int peakMemoryUsed;

	private int processID;

	private int requestCount;

	private ProcessShutdownReason shutdownReason;

	private DateTime startTime;

	private ProcessStatus status;

	/// <summary>Gets the length of time the process has been running.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started.</returns>
	public TimeSpan Age => age;

	/// <summary>Gets the maximum amount of memory the process has used.</summary>
	/// <returns>The maximum memory used, in kilobytes (KB).</returns>
	public int PeakMemoryUsed => peakMemoryUsed;

	/// <summary>Gets the ID number assigned to the process.</summary>
	/// <returns>The process ID number assigned by Windows.</returns>
	public int ProcessID => processID;

	/// <summary>Gets the number of start requests for the process.</summary>
	/// <returns>The number of requests executed by the process.</returns>
	public int RequestCount => requestCount;

	/// <summary>Gets a value that indicates why the process shut down.</summary>
	/// <returns>On of the <see cref="T:System.Web.ProcessShutdownReason" /> values.</returns>
	public ProcessShutdownReason ShutdownReason => shutdownReason;

	/// <summary>Gets the time at which the process started.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that indicates the time at which the process started.</returns>
	public DateTime StartTime => startTime;

	/// <summary>Gets the current status of the process.</summary>
	/// <returns>One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the current status of the process.</returns>
	public ProcessStatus Status => status;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessInfo" /> class.</summary>
	public ProcessInfo()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessInfo" /> class and sets internal information indicating the status of the process.</summary>
	/// <param name="startTime">A <see cref="T:System.DateTime" /> that indicates the time at which the process started. </param>
	/// <param name="age">The <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started. </param>
	/// <param name="processID">The ID number assigned to the process. </param>
	/// <param name="requestCount">The number of start requests for the process. </param>
	/// <param name="status">One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the current status of the process. </param>
	/// <param name="shutdownReason">One of the <see cref="T:System.Web.ProcessShutdownReason" /> values. </param>
	/// <param name="peakMemoryUsed">The maximum memory used, in kilobytes (KB). </param>
	public ProcessInfo(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
	{
		this.age = age;
		this.peakMemoryUsed = peakMemoryUsed;
		this.processID = processID;
		this.requestCount = requestCount;
		this.shutdownReason = shutdownReason;
		this.startTime = startTime;
		this.status = status;
	}

	/// <summary>Sets internal information indicating the status of the process.</summary>
	/// <param name="startTime">A <see cref="T:System.DateTime" /> that indicates the time at which the process started. </param>
	/// <param name="age">A <see cref="T:System.TimeSpan" /> that indicates the time elapsed since the process started. </param>
	/// <param name="processID">The ID number assigned to the process. </param>
	/// <param name="requestCount">The number of start requests for the process. </param>
	/// <param name="status">One of the <see cref="T:System.Web.ProcessStatus" /> values that indicates the time elapsed since the process started. </param>
	/// <param name="shutdownReason">One of the <see cref="T:System.Web.ProcessShutdownReason" /> values. </param>
	/// <param name="peakMemoryUsed">The maximum memory used, in kilobytes (KB). </param>
	public void SetAll(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
	{
		this.age = age;
		this.peakMemoryUsed = peakMemoryUsed;
		this.processID = processID;
		this.requestCount = requestCount;
		this.shutdownReason = shutdownReason;
		this.startTime = startTime;
		this.status = status;
	}
}
