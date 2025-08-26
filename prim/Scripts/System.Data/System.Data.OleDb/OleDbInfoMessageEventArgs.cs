using System.Data.Common;

namespace System.Data.OleDb;

/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event. This class cannot be inherited.</summary>
[System.MonoTODO("OleDb is not implemented.")]
public sealed class OleDbInfoMessageEventArgs : EventArgs
{
	/// <summary>Gets the HRESULT following the ANSI SQL standard for the database.</summary>
	/// <returns>The HRESULT, which identifies the source of the error, if the error can be issued from more than one place.</returns>
	public int ErrorCode
	{
		get
		{
			throw ADP.OleDb();
		}
	}

	/// <summary>Gets the collection of warnings sent from the data source.</summary>
	/// <returns>The collection of warnings sent from the data source.</returns>
	public OleDbErrorCollection Errors
	{
		get
		{
			throw ADP.OleDb();
		}
	}

	/// <summary>Gets the full text of the error sent from the data source.</summary>
	/// <returns>The full text of the error.</returns>
	public string Message
	{
		get
		{
			throw ADP.OleDb();
		}
	}

	/// <summary>Gets the name of the object that generated the error.</summary>
	/// <returns>The name of the object that generated the error.</returns>
	public string Source
	{
		get
		{
			throw ADP.OleDb();
		}
	}

	internal OleDbInfoMessageEventArgs()
	{
		throw ADP.OleDb();
	}

	/// <summary>Retrieves a string representation of the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</summary>
	/// <returns>A string representing the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</returns>
	public override string ToString()
	{
		throw ADP.OleDb();
	}
}
