namespace System.Web.Configuration;

/// <summary>Specifies the order in which trace messages are displayed.</summary>
public enum TraceDisplayMode
{
	/// <summary>Emit trace messages in the order they were processed.</summary>
	SortByTime = 1,
	/// <summary>Emit trace messages alphabetically by category.</summary>
	SortByCategory
}
