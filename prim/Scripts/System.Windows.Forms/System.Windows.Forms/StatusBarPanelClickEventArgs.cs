namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.StatusBar.PanelClick" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class StatusBarPanelClickEventArgs : MouseEventArgs
{
	private StatusBarPanel panel;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public StatusBarPanel StatusBarPanel => panel;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBarPanelClickEventArgs" /> class.</summary>
	/// <param name="statusBarPanel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel that was clicked. </param>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that represents the mouse buttons that were clicked while over the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
	/// <param name="clicks">The number of times that the mouse button was clicked. </param>
	/// <param name="x">The x-coordinate of the mouse click. </param>
	/// <param name="y">The y-coordinate of the mouse click. </param>
	public StatusBarPanelClickEventArgs(StatusBarPanel statusBarPanel, MouseButtons button, int clicks, int x, int y)
		: base(button, clicks, x, y, 0)
	{
		panel = statusBarPanel;
	}
}
