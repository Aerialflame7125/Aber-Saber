namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedGridItemChanged" /> event of the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class SelectedGridItemChangedEventArgs : EventArgs
{
	private GridItem new_selection;

	private GridItem old_selection;

	/// <summary>Gets the newly selected <see cref="T:System.Windows.Forms.GridItem" />.</summary>
	/// <returns>The new <see cref="T:System.Windows.Forms.GridItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public GridItem NewSelection => new_selection;

	/// <summary>Gets the previously selected <see cref="T:System.Windows.Forms.GridItem" />.</summary>
	/// <returns>The old <see cref="T:System.Windows.Forms.GridItem" />. This can be null.</returns>
	/// <filterpriority>1</filterpriority>
	public GridItem OldSelection => old_selection;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SelectedGridItemChangedEventArgs" /> class.</summary>
	/// <param name="oldSel">The previously selected grid item. </param>
	/// <param name="newSel">The newly selected grid item. </param>
	public SelectedGridItemChangedEventArgs(GridItem oldSel, GridItem newSel)
	{
		old_selection = oldSel;
		new_selection = newSel;
	}
}
