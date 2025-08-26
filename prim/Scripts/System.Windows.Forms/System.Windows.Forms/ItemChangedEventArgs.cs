namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class ItemChangedEventArgs : EventArgs
{
	private int index;

	/// <summary>Indicates the position of the item being changed within the list.</summary>
	/// <returns>The zero-based index to the item being changed.</returns>
	/// <filterpriority>1</filterpriority>
	public int Index => index;

	internal ItemChangedEventArgs(int index)
	{
		this.index = index;
	}
}
