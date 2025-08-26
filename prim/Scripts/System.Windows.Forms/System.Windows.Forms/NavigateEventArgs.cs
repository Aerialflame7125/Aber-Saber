using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGrid.Navigate" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class NavigateEventArgs : EventArgs
{
	private bool forward;

	/// <summary>Gets a value indicating whether to navigate in a forward direction.</summary>
	/// <returns>true if the navigation is in a forward direction; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool Forward => forward;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NavigateEventArgs" /> class.</summary>
	/// <param name="isForward">true to navigate in a forward direction; otherwise, false. </param>
	public NavigateEventArgs(bool isForward)
	{
		forward = isForward;
	}
}
