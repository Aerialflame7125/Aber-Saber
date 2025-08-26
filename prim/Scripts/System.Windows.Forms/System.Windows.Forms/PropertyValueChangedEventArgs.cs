using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyValueChanged" /> event of a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class PropertyValueChangedEventArgs : EventArgs
{
	private GridItem changed_item;

	private object old_value;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.GridItem" /> that was changed.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.GridItem" /> in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
	/// <filterpriority>1</filterpriority>
	public GridItem ChangedItem => changed_item;

	/// <summary>The value of the grid item before it was changed.</summary>
	/// <returns>A object representing the old value of the property.</returns>
	/// <filterpriority>1</filterpriority>
	public object OldValue => old_value;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyValueChangedEventArgs" /> class.</summary>
	/// <param name="changedItem">The item in the grid that changed. </param>
	/// <param name="oldValue">The old property value. </param>
	public PropertyValueChangedEventArgs(GridItem changedItem, object oldValue)
	{
		changed_item = changedItem;
		old_value = oldValue;
	}
}
