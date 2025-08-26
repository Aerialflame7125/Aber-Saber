namespace System.Windows.Forms;

/// <filterpriority>2</filterpriority>
public sealed class GridTablesFactory
{
	internal GridTablesFactory()
	{
	}

	/// <summary>Returns the specified <see cref="P:System.Windows.Forms.DataGridColumnStyle.DataGridTableStyle" /> in a one-element array.</summary>
	/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects.</returns>
	/// <param name="gridTable">A <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</param>
	/// <param name="dataSource">An <see cref="T:System.Object" />.</param>
	/// <param name="dataMember">A <see cref="T:System.String" />.</param>
	/// <param name="bindingManager">A <see cref="T:System.Windows.Forms.BindingContext" />.</param>
	/// <filterpriority>1</filterpriority>
	public static DataGridTableStyle[] CreateGridTables(DataGridTableStyle gridTable, object dataSource, string dataMember, BindingContext bindingManager)
	{
		throw new NotImplementedException();
	}
}
