namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnClick" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class ColumnClickEventArgs : EventArgs
{
	private int column;

	/// <summary>Gets the zero-based index of the column that is clicked.</summary>
	/// <returns>The zero-based index within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the column that is clicked.</returns>
	/// <filterpriority>1</filterpriority>
	public int Column => column;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnClickEventArgs" /> class.</summary>
	/// <param name="column">The zero-based index of the column that is clicked. </param>
	public ColumnClickEventArgs(int column)
	{
		this.column = column;
	}
}
