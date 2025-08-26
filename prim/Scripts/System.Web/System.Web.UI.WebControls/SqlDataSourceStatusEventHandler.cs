namespace System.Web.UI.WebControls;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Selected" />, <see cref="E:System.Web.UI.WebControls.SqlDataSource.Updated" />, <see cref="E:System.Web.UI.WebControls.SqlDataSource.Inserted" />, and <see cref="E:System.Web.UI.WebControls.SqlDataSource.Deleted" /> events of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control. </param>
/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
public delegate void SqlDataSourceStatusEventHandler(object sender, SqlDataSourceStatusEventArgs e);
