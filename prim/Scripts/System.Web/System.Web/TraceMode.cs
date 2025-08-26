namespace System.Web;

/// <summary>Specifies in what order trace messages are emitted into the HTML output of a page.</summary>
public enum TraceMode
{
	/// <summary>Emit trace messages in the order they were processed.</summary>
	SortByTime,
	/// <summary>Emit trace messages alphabetically by category.</summary>
	SortByCategory,
	/// <summary>Specifies the default value of the <see cref="P:System.Web.TraceContext.TraceMode" /> enumeration, which is <see cref="F:System.Web.TraceMode.SortByTime" />.</summary>
	Default
}
