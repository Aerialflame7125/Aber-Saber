using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CheckedListBox.ItemCheck" /> event of the <see cref="T:System.Windows.Forms.CheckedListBox" /> and <see cref="T:System.Windows.Forms.ListView" /> controls. </summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class ItemCheckEventArgs : EventArgs
{
	private CheckState currentValue;

	private int index;

	private CheckState newValue;

	/// <summary>Gets a value indicating the current state of the item's check box.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public CheckState CurrentValue => currentValue;

	/// <summary>Gets the zero-based index of the item to change.</summary>
	/// <returns>The zero-based index of the item to change.</returns>
	/// <filterpriority>1</filterpriority>
	public int Index => index;

	/// <summary>Gets or sets a value indicating whether to set the check box for the item to be checked, unchecked, or indeterminate.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public CheckState NewValue
	{
		get
		{
			return newValue;
		}
		set
		{
			newValue = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckEventArgs" /> class.</summary>
	/// <param name="index">The zero-based index of the item to change. </param>
	/// <param name="newCheckValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether to change the check box for the item to be checked, unchecked, or indeterminate. </param>
	/// <param name="currentValue">One of the <see cref="T:System.Windows.Forms.CheckState" /> values that indicates whether the check box for the item is currently checked, unchecked, or indeterminate. </param>
	public ItemCheckEventArgs(int index, CheckState newCheckValue, CheckState currentValue)
	{
		this.index = index;
		newValue = newCheckValue;
		this.currentValue = currentValue;
	}
}
