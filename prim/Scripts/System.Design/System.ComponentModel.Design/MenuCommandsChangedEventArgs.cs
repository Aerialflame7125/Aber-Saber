using System.Runtime.InteropServices;

namespace System.ComponentModel.Design;

/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> event.</summary>
[ComVisible(true)]
public class MenuCommandsChangedEventArgs : EventArgs
{
	private MenuCommandsChangedType change_type;

	private MenuCommand command;

	/// <summary>Gets the type of change that caused <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> to be raised.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.MenuCommandsChangedType" /> that caused <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> to be raised.</returns>
	public MenuCommandsChangedType ChangeType => change_type;

	/// <summary>Gets the command that was added, removed, or changed.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.MenuCommand" /> that was added, removed, or changed.</returns>
	public MenuCommand Command => command;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommandsChangedEventArgs" /> class.</summary>
	/// <param name="changeType">The type of change.</param>
	/// <param name="command">The menu command.</param>
	public MenuCommandsChangedEventArgs(MenuCommandsChangedType changeType, MenuCommand command)
	{
		change_type = changeType;
		this.command = command;
	}
}
