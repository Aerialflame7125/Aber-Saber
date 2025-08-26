namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> and <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> events of a <see cref="T:System.Web.UI.WebControls.Menu" /> control. This class cannot be inherited. </summary>
public sealed class MenuEventArgs : CommandEventArgs
{
	private MenuItem _item;

	private object _commandSource;

	/// <summary>Gets the <see cref="T:System.Object" /> that raised the event.</summary>
	/// <returns>The <see cref="T:System.Object" /> that raised the event.</returns>
	public object CommandSource => _commandSource;

	/// <summary>Gets the menu item associated with the event raised.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.MenuItem" /> that represents the menu item associated with the event raised.</returns>
	public MenuItem Item => _item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> class using the specified menu item, command source, and event arguments.</summary>
	/// <param name="item">For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> event, this parameter represents the menu item clicked by the user. For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> event, this parameter represents the menu item being bound to data.</param>
	/// <param name="commandSource">The <see cref="T:System.Object" /> that raised the event.</param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the command name and command argument values for the menu item.</param>
	public MenuEventArgs(MenuItem item, object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		_item = item;
		_commandSource = commandSource;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MenuEventArgs" /> class using the specified menu item.</summary>
	/// <param name="item">For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemClick" /> event, this parameter represents the menu item clicked by the user. For the <see cref="E:System.Web.UI.WebControls.Menu.MenuItemDataBound" /> event, this parameter represents the menu item being bound to data.</param>
	public MenuEventArgs(MenuItem item)
		: this(item, null, new CommandEventArgs(string.Empty, null))
	{
	}
}
