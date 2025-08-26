namespace System.Data.SqlClient;

/// <summary>Represents the method that handles the <see cref="E:System.Data.SqlClient.SqlBulkCopy.SqlRowsCopied" /> event of a <see cref="T:System.Data.SqlClient.SqlBulkCopy" />.</summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventArgs" /> object that contains the event data.</param>
public delegate void SqlRowsCopiedEventHandler(object sender, SqlRowsCopiedEventArgs e);
