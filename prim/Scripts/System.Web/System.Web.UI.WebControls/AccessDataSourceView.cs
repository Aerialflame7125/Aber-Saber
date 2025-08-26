using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control and provides an interface for data-bound controls to perform data retrieval using Structured Query Language (SQL) against a Microsoft Access database.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class AccessDataSourceView : SqlDataSourceView
{
	private OleDbConnection oleConnection;

	private OleDbCommand oleCommand;

	private AccessDataSource dataSource;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> class setting the specified <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control as the owner of the current view.</summary>
	/// <param name="owner">The data source control with which the <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> is associated. </param>
	/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it. </param>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" />. </param>
	public AccessDataSourceView(AccessDataSource owner, string name, HttpContext context)
		: base(owner, name, context)
	{
		dataSource = owner;
		oleConnection = new OleDbConnection(owner.ConnectionString);
	}

	/// <summary>Retrieves data from the underlying data storage using the SQL string in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property and any parameters in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> of data rows.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.AccessDataSource.DataFile" /> property is <see langword="null" /> or an empty string ("").</exception>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.AccessDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
	[MonoTODO("Handle arguments")]
	protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
	{
		oleCommand = new OleDbCommand(base.SelectCommand, oleConnection);
		SqlDataSourceSelectingEventArgs e = new SqlDataSourceSelectingEventArgs(oleCommand, arguments);
		OnSelecting(e);
		IEnumerable result = null;
		Exception ex = null;
		OleDbDataReader oleDbDataReader = null;
		try
		{
			File.OpenRead(dataSource.DataFile).Close();
			oleConnection.Open();
			oleDbDataReader = oleCommand.ExecuteReader();
			throw new NotImplementedException("OleDbDataReader doesnt implements GetEnumerator method yet");
		}
		catch (Exception ex2)
		{
			ex = ex2;
		}
		SqlDataSourceStatusEventArgs e2 = new SqlDataSourceStatusEventArgs(oleCommand, oleDbDataReader.RecordsAffected, ex);
		OnSelected(e2);
		if (ex != null)
		{
			throw ex;
		}
		return result;
	}
}
