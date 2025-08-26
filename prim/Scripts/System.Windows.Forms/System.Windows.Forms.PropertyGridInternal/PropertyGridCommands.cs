using System.ComponentModel.Design;

namespace System.Windows.Forms.PropertyGridInternal;

/// <summary>Contains a set of menu commands used by the designer in Visual Studio.</summary>
public class PropertyGridCommands
{
	/// <summary>Represents the command identifier for the Commands menu item. </summary>
	public static readonly CommandID Commands;

	/// <summary>Represents the command identifier for the Description menu item.</summary>
	public static readonly CommandID Description;

	/// <summary>Represents the command identifier for the Hide menu item.</summary>
	public static readonly CommandID Hide;

	/// <summary>Represents the command identifier for the Reset menu item.</summary>
	public static readonly CommandID Reset;

	/// <summary>Represents the GUID for the internal property browser’s command set.</summary>
	protected static readonly Guid wfcMenuCommand;

	/// <summary>Represents the GUID the internal property browser uses to create a shortcut menu.</summary>
	protected static readonly Guid wfcMenuGroup;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyGridInternal.PropertyGridCommands" /> class. </summary>
	public PropertyGridCommands()
	{
	}
}
