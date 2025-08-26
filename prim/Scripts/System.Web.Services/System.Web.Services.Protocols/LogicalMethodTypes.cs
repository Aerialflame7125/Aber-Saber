namespace System.Web.Services.Protocols;

/// <summary>Specifies how the XML Web service method was invoked.</summary>
public enum LogicalMethodTypes
{
	/// <summary>The XML Web service method is invoked synchronously.</summary>
	Sync = 1,
	/// <summary>The XML Web service method is invoked asynchronously.</summary>
	Async
}
