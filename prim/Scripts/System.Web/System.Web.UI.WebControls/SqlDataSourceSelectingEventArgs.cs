using System.Data.Common;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Selecting" /> event of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
public class SqlDataSourceSelectingEventArgs : SqlDataSourceCommandEventArgs
{
	private DataSourceSelectArguments _arguments;

	/// <summary>Gets the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object, or <see langword="null" /> if no <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object is specified during <see cref="T:System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs" /> creation.</returns>
	public DataSourceSelectArguments Arguments => _arguments;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs" /> class, using the specified <see cref="T:System.Data.Common.DbCommand" /> object and any <see cref="T:System.Web.UI.DataSourceSelectArguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
	/// <param name="command">An <see cref="T:System.Data.Common.DbCommand" /> object that represents the cancelable <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> query.</param>
	/// <param name="arguments">The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</param>
	public SqlDataSourceSelectingEventArgs(DbCommand command, DataSourceSelectArguments arguments)
		: base(command)
	{
		_arguments = arguments;
	}
}
