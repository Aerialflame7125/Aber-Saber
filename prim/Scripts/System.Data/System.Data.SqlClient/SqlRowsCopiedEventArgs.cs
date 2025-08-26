namespace System.Data.SqlClient;

/// <summary>Represents the set of arguments passed to the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventHandler" />.</summary>
public class SqlRowsCopiedEventArgs : EventArgs
{
	private bool _abort;

	private long _rowsCopied;

	/// <summary>Gets or sets a value that indicates whether the bulk copy operation should be aborted.</summary>
	/// <returns>
	///   <see langword="true" /> if the bulk copy operation should be aborted; otherwise <see langword="false" />.</returns>
	public bool Abort
	{
		get
		{
			return _abort;
		}
		set
		{
			_abort = value;
		}
	}

	/// <summary>Gets a value that returns the number of rows copied during the current bulk copy operation.</summary>
	/// <returns>
	///   <see langword="int" /> that returns the number of rows copied.</returns>
	public long RowsCopied => _rowsCopied;

	/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventArgs" /> object.</summary>
	/// <param name="rowsCopied">An <see cref="T:System.Int64" /> that indicates the number of rows copied during the current bulk copy operation.</param>
	public SqlRowsCopiedEventArgs(long rowsCopied)
	{
		_rowsCopied = rowsCopied;
	}
}
