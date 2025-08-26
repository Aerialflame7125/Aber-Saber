using System.Runtime.InteropServices;

namespace System.ComponentModel.Design;

/// <summary>Specifies the type of action that occurred to the related object's <see cref="T:System.Windows.Forms.Design.MenuCommands" /> collection.</summary>
[ComVisible(true)]
public enum MenuCommandsChangedType
{
	/// <summary>Specifies that one or more command objects were added.</summary>
	CommandAdded,
	/// <summary>Specifies that one or more commands were removed.</summary>
	CommandRemoved,
	/// <summary>Specifies that one or more commands have changed their status.</summary>
	CommandChanged
}
