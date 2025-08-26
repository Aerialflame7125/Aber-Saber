namespace System.Web.UI.WebControls;

/// <summary>Describes the type of SQL command used by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> and <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> controls when performing a database operation.</summary>
public enum SqlDataSourceCommandType
{
	/// <summary>The text contained in a corresponding text property is a SQL query or command.</summary>
	Text,
	/// <summary>The text contained in a corresponding text property is the name of a stored procedure.</summary>
	StoredProcedure
}
