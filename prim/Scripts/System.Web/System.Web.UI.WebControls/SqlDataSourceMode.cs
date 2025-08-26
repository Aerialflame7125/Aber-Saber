namespace System.Web.UI.WebControls;

/// <summary>Specifies whether a <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> or <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control retrieves data as a <see cref="T:System.Data.IDataReader" /> or <see cref="T:System.Data.DataSet" />.</summary>
public enum SqlDataSourceMode
{
	/// <summary>Retrieves data from the underlying data storage as an <see cref="T:System.Data.IDataReader" /></summary>
	DataReader,
	/// <summary>Retrieves data from the underlying data storage into a <see cref="T:System.Data.DataSet" /> structure.</summary>
	DataSet
}
