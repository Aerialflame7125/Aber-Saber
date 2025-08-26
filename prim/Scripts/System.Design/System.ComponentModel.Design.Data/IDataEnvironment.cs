using System.CodeDom;
using System.Collections;
using System.Data.Common;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Data;

/// <summary>Defines an interface to data services that enables control designers to integrate data store or database-related functionality into their design environment.</summary>
public interface IDataEnvironment
{
	/// <summary>Gets a collection of data connections defined in the current design session.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> objects representing the data connections available in the current design session.</returns>
	ICollection Connections { get; }

	/// <summary>Creates a new data connection or edits an existing connection using the design tool's new connection user interface.</summary>
	/// <param name="owner">The parent window for the connection dialog.</param>
	/// <param name="initialConnection">The connection, if any, to edit. To create a new connection, <paramref name="initialConnection" /> should be <see langword="null" />.</param>
	/// <returns>A new or edited <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> object, or <see langword="null" /> if the user canceled.</returns>
	DesignerDataConnection BuildConnection(IWin32Window owner, DesignerDataConnection initialConnection);

	/// <summary>Launches a dialog to build a SQL query string.</summary>
	/// <param name="owner">The parent window for the dialog.</param>
	/// <param name="connection">The data connection to use for the query.</param>
	/// <param name="mode">One of the <see cref="T:System.ComponentModel.Design.Data.QueryBuilderMode" /> values.</param>
	/// <param name="initialQueryText">The initial value of the query or <see cref="F:System.String.Empty" /> to create a new query.</param>
	/// <returns>A string containing the SQL query, or <see langword="null" /> if the user canceled.</returns>
	string BuildQuery(IWin32Window owner, DesignerDataConnection connection, QueryBuilderMode mode, string initialQueryText);

	/// <summary>Writes a connection string to the application's configuration file.</summary>
	/// <param name="owner">The parent window for the dialog, if any.</param>
	/// <param name="connection">A <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> object containing the connection data to save.</param>
	/// <param name="name">The name of the new connection configuration entry.</param>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> object containing the saved connection data with the <see cref="P:System.ComponentModel.Design.Data.DesignerDataConnection.Name" /> property set to <paramref name="name" />, and the <see cref="P:System.ComponentModel.Design.Data.DesignerDataConnection.IsConfigured" /> property set to <see langword="true" />.</returns>
	DesignerDataConnection ConfigureConnection(IWin32Window owner, DesignerDataConnection connection, string name);

	/// <summary>Returns a code expression that contains the source code required to retrieve a connection string from the application's configuration file.</summary>
	/// <param name="connection">The connection to retrieve from the application's configuration file.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> object containing the source code required to retrieve a connection string from the application's configuration file.</returns>
	CodeExpression GetCodeExpression(DesignerDataConnection connection);

	/// <summary>Gets the schema for the specified data connection.</summary>
	/// <param name="connection">The data connection for which to return schema information.</param>
	/// <returns>An <see cref="T:System.ComponentModel.Design.Data.IDesignerDataSchema" /> object containing the schema information for the specified data connection, or <see langword="null" /> if no schema information is available.</returns>
	IDesignerDataSchema GetConnectionSchema(DesignerDataConnection connection);

	/// <summary>Gets a database connection that can be used at design time.</summary>
	/// <param name="connection">The desired data connection.</param>
	/// <returns>A <see cref="T:System.Data.Common.DbConnection" /> object that can be used at design time.</returns>
	DbConnection GetDesignTimeConnection(DesignerDataConnection connection);
}
