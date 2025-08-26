using System.Security.Permissions;

namespace System.Web;

/// <summary>Contains methods that return information about worker processes. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ProcessModelInfo
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.ProcessModelInfo" /> class.</summary>
	public ProcessModelInfo()
	{
	}

	/// <summary>Returns information about the worker process that is executing the current request.</summary>
	/// <returns>A <see cref="T:System.Web.ProcessInfo" /> that contains information about the current process.</returns>
	/// <exception cref="T:System.Web.HttpException">Process information is not available for the current request. </exception>
	[MonoTODO("Retrieve appropriate variables from worker")]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
	public static ProcessInfo GetCurrentProcessInfo()
	{
		DateTime now = DateTime.Now;
		TimeSpan zero = TimeSpan.Zero;
		int processID = 0;
		int requestCount = 0;
		ProcessStatus status = ProcessStatus.Terminated;
		ProcessShutdownReason shutdownReason = ProcessShutdownReason.None;
		int peakMemoryUsed = 0;
		return new ProcessInfo(now, zero, processID, requestCount, status, shutdownReason, peakMemoryUsed);
	}

	/// <summary>Returns information about recent worker processes.</summary>
	/// <param name="numRecords">The number of processes for which information is requested. </param>
	/// <returns>An array of the most recent <see cref="T:System.Web.ProcessInfo" /> objects (up to 100); otherwise, if the number of available objects is less than <paramref name="numRecords" />, all available objects.</returns>
	/// <exception cref="T:System.Web.HttpException">Process information is not available. </exception>
	[MonoTODO("Retrieve process information.")]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
	public static ProcessInfo[] GetHistory(int numRecords)
	{
		throw new NotImplementedException();
	}
}
