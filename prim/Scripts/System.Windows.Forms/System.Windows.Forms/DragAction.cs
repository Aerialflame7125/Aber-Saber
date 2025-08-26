using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies how and if a drag-and-drop operation should continue.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public enum DragAction
{
	/// <summary>The operation will continue.</summary>
	Continue,
	/// <summary>The operation will stop with a drop.</summary>
	Drop,
	/// <summary>The operation is canceled with no drop message.</summary>
	Cancel
}
