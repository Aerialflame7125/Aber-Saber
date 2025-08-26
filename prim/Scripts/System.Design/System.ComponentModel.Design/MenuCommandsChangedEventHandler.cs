using System.Runtime.InteropServices;

namespace System.ComponentModel.Design;

/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> event of a <see cref="T:System.ComponentModel.Design.MenuCommandService" />. This class cannot be inherited.</summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="T:System.ComponentModel.Design.MenuCommandsChangedEventArgs" /> that contains the event data.</param>
[ComVisible(true)]
public delegate void MenuCommandsChangedEventHandler(object sender, MenuCommandsChangedEventArgs e);
