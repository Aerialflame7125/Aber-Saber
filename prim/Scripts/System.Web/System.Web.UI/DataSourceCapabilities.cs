namespace System.Web.UI;

/// <summary>Provides a way to request processing beyond record retrieval for a data retrieval operation of a data source control.</summary>
[Flags]
public enum DataSourceCapabilities
{
	/// <summary>Represents no paging, sorting, or total row count retrieval capabilities.</summary>
	None = 0,
	/// <summary>Represents the capability to sort through the rows returned by an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation.</summary>
	Sort = 1,
	/// <summary>Represents the capability to page through the rows returned by an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation.</summary>
	Page = 2,
	/// <summary>Represents the capability to retrieve a total row count of data, which corresponds to using the <see cref="F:System.Web.UI.DataSourceOperation.SelectCount" /> value. </summary>
	RetrieveTotalRowCount = 4
}
