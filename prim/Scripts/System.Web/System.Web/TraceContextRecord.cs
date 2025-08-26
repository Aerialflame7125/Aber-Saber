using System.Security.Permissions;

namespace System.Web;

/// <summary>Represents an ASP.NET trace message and any associated data.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class TraceContextRecord
{
	private string category;

	private Exception errorInfo;

	private bool isWarning;

	private string message;

	/// <summary>Gets the user-defined category for the trace record.</summary>
	/// <returns>A string that represents a category for the trace record.</returns>
	public string Category => category;

	/// <summary>Gets the <see cref="T:System.Exception" /> associated with the trace record, if one is available.</summary>
	/// <returns>A <see cref="T:System.Exception" /> associated with the trace record, if one exists, or <see langword="null" />.</returns>
	public Exception ErrorInfo => errorInfo;

	/// <summary>Gets a value indicating whether the trace record is associated with a <see cref="Overload:System.Web.TraceContext.Warn" /> method call.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.TraceContextRecord" /> is associated with the <see cref="Overload:System.Web.TraceContext.Warn" /> method call; otherwise, <see langword="false" />.</returns>
	public bool IsWarning => isWarning;

	/// <summary>Gets the user-defined trace message.</summary>
	/// <returns>A string that represents a message for the trace record.</returns>
	public string Message => message;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.TraceContextRecord" /> class. </summary>
	/// <param name="category">The trace category that receives the message.</param>
	/// <param name="msg">The trace message.</param>
	/// <param name="isWarning">
	///       <see langword="true" /> if the method associated with the <see cref="T:System.Web.TraceContextRecord" /> is the <see cref="Overload:System.Web.TraceContext.Warn" /> method;<see langword=" false" /> if the tracing method is the <see cref="Overload:System.Web.TraceContext.Write" /> method.</param>
	/// <param name="errorInfo">A <see cref="T:System.Exception" /> object that contains additional error information. </param>
	public TraceContextRecord(string category, string msg, bool isWarning, Exception errorInfo)
	{
		this.category = category;
		message = msg;
		this.isWarning = isWarning;
		this.errorInfo = errorInfo;
	}
}
