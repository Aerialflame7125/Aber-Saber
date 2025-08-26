using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyTabChanged" /> event of a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class PropertyTabChangedEventArgs : EventArgs
{
	private PropertyTab old_tab;

	private PropertyTab new_tab;

	/// <summary>Gets the new <see cref="T:System.Windows.Forms.Design.PropertyTab" /> selected.</summary>
	/// <returns>The newly selected <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</returns>
	/// <filterpriority>1</filterpriority>
	public PropertyTab NewTab => new_tab;

	/// <summary>Gets the old <see cref="T:System.Windows.Forms.Design.PropertyTab" /> selected.</summary>
	/// <returns>The old <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that was selected.</returns>
	/// <filterpriority>1</filterpriority>
	public PropertyTab OldTab => old_tab;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyTabChangedEventArgs" /> class.</summary>
	/// <param name="oldTab">The Previously selected property tab. </param>
	/// <param name="newTab">The newly selected property tab. </param>
	public PropertyTabChangedEventArgs(PropertyTab oldTab, PropertyTab newTab)
	{
		old_tab = oldTab;
		new_tab = newTab;
	}
}
