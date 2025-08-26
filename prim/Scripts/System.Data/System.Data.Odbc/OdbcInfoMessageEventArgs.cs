using System.Text;

namespace System.Data.Odbc;

/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
public sealed class OdbcInfoMessageEventArgs : EventArgs
{
	private OdbcErrorCollection _errors;

	/// <summary>Gets the collection of warnings sent from the data source.</summary>
	/// <returns>The collection of warnings sent from the data source.</returns>
	public OdbcErrorCollection Errors => _errors;

	/// <summary>Gets the full text of the error sent from the database.</summary>
	/// <returns>The full text of the error.</returns>
	public string Message
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (OdbcError error in Errors)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(error.Message);
			}
			return stringBuilder.ToString();
		}
	}

	internal OdbcInfoMessageEventArgs(OdbcErrorCollection errors)
	{
		_errors = errors;
	}

	/// <summary>Retrieves a string representation of the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
	/// <returns>A string representing the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</returns>
	public override string ToString()
	{
		return Message;
	}
}
