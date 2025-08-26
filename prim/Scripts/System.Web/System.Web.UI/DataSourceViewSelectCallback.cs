using System.Collections;

namespace System.Web.UI;

/// <summary>Represents the asynchronous callback method that a data-bound control supplies to a data source view for asynchronous data retrieval. </summary>
/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> collection of data records that the <see cref="M:System.Web.UI.DataSourceView.Select(System.Web.UI.DataSourceSelectArguments,System.Web.UI.DataSourceViewSelectCallback)" /> and <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> methods return.</param>
public delegate void DataSourceViewSelectCallback(IEnumerable data);
