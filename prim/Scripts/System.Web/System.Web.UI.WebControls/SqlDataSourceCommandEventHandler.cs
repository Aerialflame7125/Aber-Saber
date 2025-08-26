namespace System.Web.UI.WebControls;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Updating" />, <see cref="E:System.Web.UI.WebControls.SqlDataSource.Inserting" />, and <see cref="E:System.Web.UI.WebControls.SqlDataSource.Deleting" /> events of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control. </param>
/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
public delegate void SqlDataSourceCommandEventHandler(object sender, SqlDataSourceCommandEventArgs e);
