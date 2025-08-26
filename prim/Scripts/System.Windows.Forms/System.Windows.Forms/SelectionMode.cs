using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies the selection behavior of a list box.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public enum SelectionMode
{
	/// <summary>No items can be selected.</summary>
	None,
	/// <summary>Only one item can be selected.</summary>
	One,
	/// <summary>Multiple items can be selected.</summary>
	MultiSimple,
	/// <summary>Multiple items can be selected, and the user can use the SHIFT, CTRL, and arrow keys to make selections </summary>
	MultiExtended
}
