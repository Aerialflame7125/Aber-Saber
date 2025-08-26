namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TabControl.Selected" /> and <see cref="E:System.Windows.Forms.TabControl.Deselected" /> events of a <see cref="T:System.Windows.Forms.TabControl" /> control. </summary>
/// <filterpriority>2</filterpriority>
public class TabControlEventArgs : EventArgs
{
	private TabControlAction action;

	private TabPage tab_page;

	private int tab_page_index;

	/// <summary>Gets a value indicating which event is occurring. </summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public TabControlAction Action => action;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</returns>
	/// <filterpriority>1</filterpriority>
	public TabPage TabPage => tab_page;

	/// <summary>Gets the zero-based index of the <see cref="P:System.Windows.Forms.TabControlEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
	/// <returns>The zero-based index of the <see cref="P:System.Windows.Forms.TabControlEventArgs.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</returns>
	/// <filterpriority>1</filterpriority>
	public int TabPageIndex => tab_page_index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControlEventArgs" /> class. </summary>
	/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> the event is occurring for.</param>
	/// <param name="tabPageIndex">The zero-based index of <paramref name="tabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</param>
	/// <param name="action">One of the <see cref="T:System.Windows.Forms.TabControlAction" /> values.</param>
	public TabControlEventArgs(TabPage tabPage, int tabPageIndex, TabControlAction action)
	{
		tab_page = tabPage;
		tab_page_index = tabPageIndex;
		this.action = action;
	}
}
