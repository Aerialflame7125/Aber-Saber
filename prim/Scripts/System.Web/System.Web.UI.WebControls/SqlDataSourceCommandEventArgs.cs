using System.ComponentModel;
using System.Data.Common;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Updating" />, <see cref="E:System.Web.UI.WebControls.SqlDataSource.Deleting" /> and <see cref="E:System.Web.UI.WebControls.SqlDataSource.Inserting" /> events of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
public class SqlDataSourceCommandEventArgs : CancelEventArgs
{
	private DbCommand _command;

	/// <summary>Gets the pending database command.</summary>
	/// <returns>An <see cref="T:System.Data.Common.DbCommand" /> object that represents the pending database command.</returns>
	public DbCommand Command => _command;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> class, using the specified database command object.</summary>
	/// <param name="command">An <see cref="T:System.Data.Common.DbCommand" /> object that represents the cancelable <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" />, <see cref="M:System.Web.UI.WebControls.SqlDataSource.Insert" />, or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> command. </param>
	public SqlDataSourceCommandEventArgs(DbCommand command)
	{
		_command = command;
	}
}
