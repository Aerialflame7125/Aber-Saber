using System.Security.Permissions;

namespace System.Web.Util;

/// <summary>Provides the ability to move work items to another thread for execution.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class WorkItem
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Util.WorkItem" /> class.</summary>
	public WorkItem()
	{
	}

	/// <summary>Moves a work item to a separate thread for execution.</summary>
	/// <param name="callback">A <see cref="T:System.Web.Util.WorkItemCallback" /> that represents the method that is to be called on a separate thread.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
	[MonoTODO("Not implemented, not currently supported by Mono")]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static void Post(WorkItemCallback callback)
	{
		throw new PlatformNotSupportedException("Not supported on mono");
	}
}
