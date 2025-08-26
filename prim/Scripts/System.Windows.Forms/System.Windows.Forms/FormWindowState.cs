using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies how a form window is displayed.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public enum FormWindowState
{
	/// <summary>A default sized window.</summary>
	Normal,
	/// <summary>A minimized window.</summary>
	Minimized,
	/// <summary>A maximized window.</summary>
	Maximized
}
