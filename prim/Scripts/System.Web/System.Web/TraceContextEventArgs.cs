using System.Collections;

namespace System.Web;

/// <summary>Provides a collection of trace records to any method that handles the <see cref="E:System.Web.TraceContext.TraceFinished" /> event. This class cannot be inherited.</summary>
public sealed class TraceContextEventArgs : EventArgs
{
	private ICollection _records;

	/// <summary>Gets a collection of <see cref="T:System.Web.TraceContextRecord" /> messages that are associated with the current request.</summary>
	/// <returns>A collection of trace records that are associated with the current request.</returns>
	public ICollection TraceRecords => _records;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.TraceContextEventArgs" /> class, using the provided collection of trace records.</summary>
	/// <param name="records">A collection of <see cref="T:System.Web.TraceContextRecord" /> objects that represent all the trace records logged for the current request.</param>
	public TraceContextEventArgs(ICollection records)
	{
		_records = records;
	}
}
