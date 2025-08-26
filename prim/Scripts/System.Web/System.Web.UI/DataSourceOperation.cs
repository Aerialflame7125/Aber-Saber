namespace System.Web.UI;

/// <summary>Specifies a data operation performed by a data source control.</summary>
public enum DataSourceOperation
{
	/// <summary>The operation deletes records from a data source.</summary>
	Delete,
	/// <summary>The operation inserts one or more records into a data source.</summary>
	Insert,
	/// <summary>The operation retrieves records from a data source.</summary>
	Select,
	/// <summary>The operation updates records in a data source.</summary>
	Update,
	/// <summary>The operation retrieves the total number of records for a query from the data source.</summary>
	SelectCount
}
