using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ColumnReordered" /> event. </summary>
public class ColumnReorderedEventArgs : CancelEventArgs
{
	private ColumnHeader header;

	private int new_display_index;

	private int old_display_index;

	/// <summary>Gets the previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
	/// <returns>The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" /></returns>
	public int OldDisplayIndex => old_display_index;

	/// <summary>Gets the new display position of the <see cref="T:System.Windows.Forms.ColumnHeader" /></summary>
	/// <returns>The new display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
	public int NewDisplayIndex => new_display_index;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</returns>
	public ColumnHeader Header => header;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnReorderedEventArgs" /> class. </summary>
	/// <param name="oldDisplayIndex">The previous display position of the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
	/// <param name="newDisplayIndex">The new display position for the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
	/// <param name="header">The <see cref="T:System.Windows.Forms.ColumnHeader" /> that is being reordered.</param>
	public ColumnReorderedEventArgs(int oldDisplayIndex, int newDisplayIndex, ColumnHeader header)
	{
		old_display_index = oldDisplayIndex;
		new_display_index = newDisplayIndex;
		this.header = header;
	}
}
