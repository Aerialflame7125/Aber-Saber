using System.Collections.Specialized;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Selecting" /> event of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
public class ObjectDataSourceSelectingEventArgs : ObjectDataSourceMethodEventArgs
{
	private DataSourceSelectArguments _arguments;

	private bool _executingSelectCount;

	/// <summary>Provides a mechanism that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object can use to request data-related operations when data is retrieved.</summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that specifies which additional data-related operations the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> should perform on a results set.</returns>
	public DataSourceSelectArguments Arguments => _arguments;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> is retrieving a row count during a data retrieval operation.</summary>
	/// <returns>
	///     <see langword="true" />, if data source paging is enabled and the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> is retrieving a row count; otherwise, <see langword="false" />.</returns>
	public bool ExecutingSelectCount => _executingSelectCount;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs" /> class.</summary>
	/// <param name="inputParameters">An <see cref="T:System.Collections.IDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that specifies which additional data-related operations the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> should perform on a results set, such as sorting the data or returning a specific subset of data. </param>
	/// <param name="executingSelectCount">
	///       <see langword="true" /> to indicate the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> is retrieving the number of rows of data, in addition to the data itself; otherwise, <see langword="false" />.</param>
	public ObjectDataSourceSelectingEventArgs(IOrderedDictionary inputParameters, DataSourceSelectArguments arguments, bool executingSelectCount)
		: base(inputParameters)
	{
		_arguments = arguments;
		_executingSelectCount = executingSelectCount;
	}
}
