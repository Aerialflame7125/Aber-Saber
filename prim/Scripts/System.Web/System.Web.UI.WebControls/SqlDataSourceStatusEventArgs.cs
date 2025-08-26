using System.Data.Common;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for an event that is raised by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control after a data operation has completed.</summary>
public class SqlDataSourceStatusEventArgs : EventArgs
{
	private DbCommand _command;

	private Exception _exception;

	private bool _exceptionHandled;

	private int _affectedRows;

	/// <summary>Gets the number of rows affected by a database operation.</summary>
	/// <returns>The number of rows affected by a database operation. The default value is -1.</returns>
	public int AffectedRows => _affectedRows;

	/// <summary>Gets the database command submitted to the database.</summary>
	/// <returns>The <see cref="T:System.Data.Common.DbCommand" /> object that represents the database command submitted to the database.</returns>
	public DbCommand Command => _command;

	/// <summary>Gets a wrapper for any exceptions thrown by the database during a data operation.</summary>
	/// <returns>An <see cref="T:System.Exception" /> that wraps any exceptions thrown by the database in its <see cref="P:System.Exception.InnerException" /> property.</returns>
	public Exception Exception => _exception;

	/// <summary>Gets or sets a value indicating whether an exception thrown by the database has been handled.</summary>
	/// <returns>
	///     <see langword="true" /> if an exception thrown by the database has been handled and should not be thrown by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control; otherwise, <see langword="false" />.</returns>
	public bool ExceptionHandled
	{
		get
		{
			return _exceptionHandled;
		}
		set
		{
			_exceptionHandled = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> class, using the specified output parameters, return value, and number of rows affected by the database operation.</summary>
	/// <param name="command">The <see cref="T:System.Data.Common.DbCommand" /> that represents the database query, command, or stored procedure that is submitted to the database by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control. </param>
	/// <param name="affectedRows">The number of rows affected by the database operation, if applicable. </param>
	/// <param name="exception">An <see cref="T:System.Exception" /> thrown by the database operation, if applicable.</param>
	public SqlDataSourceStatusEventArgs(DbCommand command, int affectedRows, Exception exception)
	{
		_command = command;
		_affectedRows = affectedRows;
		_exception = exception;
	}
}
