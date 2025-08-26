using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design;

/// <summary>Implements the <see cref="T:System.ComponentModel.Design.IMenuCommandService" /> interface.</summary>
public class MenuCommandService : IMenuCommandService, IDisposable
{
	private IServiceProvider _serviceProvider;

	private DesignerVerbCollection _globalVerbs;

	private DesignerVerbCollection _verbs;

	private Dictionary<CommandID, MenuCommand> _commands;

	/// <summary>Gets a collection of the designer verbs that are currently available.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> of the designer verbs that are currently available.</returns>
	public virtual DesignerVerbCollection Verbs
	{
		get
		{
			EnsureVerbs();
			return _verbs;
		}
	}

	/// <summary>Occurs when the status of a menu command has changed.</summary>
	public event MenuCommandsChangedEventHandler MenuCommandsChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommandService" /> class.</summary>
	/// <param name="serviceProvider">The service provider that this service uses to obtain other services.</param>
	public MenuCommandService(IServiceProvider serviceProvider)
	{
		if (serviceProvider == null)
		{
			throw new ArgumentNullException("serviceProvider");
		}
		_serviceProvider = serviceProvider;
		if (_serviceProvider.GetService(typeof(ISelectionService)) is ISelectionService selectionService)
		{
			selectionService.SelectionChanged += OnSelectionChanged;
		}
	}

	private void OnSelectionChanged(object sender, EventArgs arg)
	{
		OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandChanged, null));
	}

	/// <summary>Adds a command handler to the menu command service.</summary>
	/// <param name="command">The <see cref="T:System.ComponentModel.Design.MenuCommand" /> to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="command" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A command handler <paramref name="command" /> already exists.</exception>
	public virtual void AddCommand(MenuCommand command)
	{
		if (command == null)
		{
			throw new ArgumentNullException("command");
		}
		if (_commands == null)
		{
			_commands = new Dictionary<CommandID, MenuCommand>();
		}
		_commands.Add(command.CommandID, command);
		OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandAdded, command));
	}

	/// <summary>Adds a verb to the verb table of the <see cref="T:System.ComponentModel.Design.MenuCommandService" />.</summary>
	/// <param name="verb">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="verb" /> is <see langword="null" />.</exception>
	public virtual void AddVerb(DesignerVerb verb)
	{
		if (verb == null)
		{
			throw new ArgumentNullException("verb");
		}
		EnsureVerbs();
		if (!_verbs.Contains(verb))
		{
			if (_globalVerbs == null)
			{
				_globalVerbs = new DesignerVerbCollection();
			}
			_globalVerbs.Add(verb);
		}
		OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandAdded, verb));
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.MenuCommandService" />.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.MenuCommandService" />.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}
		if (_globalVerbs != null)
		{
			_globalVerbs.Clear();
			_globalVerbs = null;
		}
		if (_verbs != null)
		{
			_verbs.Clear();
			_verbs = null;
		}
		if (_commands != null)
		{
			_commands.Clear();
			_commands = null;
		}
		if (_serviceProvider != null)
		{
			if (_serviceProvider.GetService(typeof(ISelectionService)) is ISelectionService selectionService)
			{
				selectionService.SelectionChanged -= OnSelectionChanged;
			}
			_serviceProvider = null;
		}
	}

	/// <summary>Ensures that the verb list has been created.</summary>
	protected void EnsureVerbs()
	{
		DesignerVerbCollection designerVerbCollection = null;
		ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
		IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
		if (selectionService != null && designerHost != null && selectionService.SelectionCount == 1 && selectionService.PrimarySelection is IComponent component)
		{
			IDesigner designer = designerHost.GetDesigner(component);
			if (designer != null)
			{
				designerVerbCollection = designer.Verbs;
			}
		}
		Dictionary<string, DesignerVerb> dictionary = new Dictionary<string, DesignerVerb>();
		if (_globalVerbs != null)
		{
			foreach (DesignerVerb globalVerb in _globalVerbs)
			{
				dictionary[globalVerb.Text] = globalVerb;
			}
		}
		if (designerVerbCollection != null)
		{
			foreach (DesignerVerb item in designerVerbCollection)
			{
				dictionary[item.Text] = item;
			}
		}
		if (_verbs == null)
		{
			_verbs = new DesignerVerbCollection();
		}
		else
		{
			_verbs.Clear();
		}
		foreach (DesignerVerb value in dictionary.Values)
		{
			_verbs.Add(value);
		}
	}

	/// <summary>Searches for the <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command.</summary>
	/// <param name="guid">The GUID of the command.</param>
	/// <param name="id">The ID of the command.</param>
	/// <returns>The <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command; otherwise, <see langword="null" /> if the command is not found.</returns>
	protected MenuCommand FindCommand(Guid guid, int id)
	{
		return FindCommand(new CommandID(guid, id));
	}

	/// <summary>Searches for the <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command ID.</summary>
	/// <param name="commandID">The <see cref="T:System.ComponentModel.Design.CommandID" /> to find.</param>
	/// <returns>The <see cref="T:System.ComponentModel.Design.MenuCommand" /> associated with the given command; otherwise, <see langword="null" /> if the command is not found.</returns>
	public MenuCommand FindCommand(CommandID commandID)
	{
		if (commandID == null)
		{
			throw new ArgumentNullException("commandID");
		}
		MenuCommand value = null;
		if (_commands != null)
		{
			_commands.TryGetValue(commandID, out value);
		}
		if (value == null)
		{
			EnsureVerbs();
			foreach (DesignerVerb verb in _verbs)
			{
				if (verb.CommandID.Equals(commandID))
				{
					value = verb;
					break;
				}
			}
		}
		return value;
	}

	/// <summary>Gets the command list for a given GUID.</summary>
	/// <param name="guid">The GUID of the command list.</param>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> of commands.</returns>
	protected ICollection GetCommandList(Guid guid)
	{
		List<MenuCommand> list = new List<MenuCommand>();
		if (_commands != null)
		{
			foreach (MenuCommand value in _commands.Values)
			{
				if (value.CommandID.Guid == guid)
				{
					list.Add(value);
				}
			}
		}
		return list;
	}

	/// <summary>Invokes the given command on the local form or in the global environment.</summary>
	/// <param name="commandID">The command to invoke.</param>
	/// <returns>
	///   <see langword="true" />, if the command was found; otherwise, <see langword="false" />.</returns>
	public virtual bool GlobalInvoke(CommandID commandID)
	{
		if (commandID == null)
		{
			throw new ArgumentNullException("commandID");
		}
		MenuCommand menuCommand = FindCommand(commandID);
		if (menuCommand != null)
		{
			menuCommand.Invoke();
			return true;
		}
		return false;
	}

	/// <summary>Invokes the given command with the given parameter on the local form or in the global environment.</summary>
	/// <param name="commandId">The command to invoke.</param>
	/// <param name="arg">A parameter for the invocation.</param>
	/// <returns>
	///   <see langword="true" />, if the command was found; otherwise, <see langword="false" />.</returns>
	public virtual bool GlobalInvoke(CommandID commandId, object arg)
	{
		if (commandId == null)
		{
			throw new ArgumentNullException("commandId");
		}
		MenuCommand menuCommand = FindCommand(commandId);
		if (menuCommand != null)
		{
			menuCommand.Invoke(arg);
			return true;
		}
		return false;
	}

	/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommandService.MenuCommandsChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.MenuCommandsChangedEventArgs" /> that contains the event data.</param>
	protected virtual void OnCommandsChanged(MenuCommandsChangedEventArgs e)
	{
		if (this.MenuCommandsChanged != null)
		{
			this.MenuCommandsChanged(this, e);
		}
	}

	/// <summary>Removes the given menu command from the document.</summary>
	/// <param name="command">The command to remove.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="command" /> is <see langword="null" />.</exception>
	public virtual void RemoveCommand(MenuCommand command)
	{
		if (command == null)
		{
			throw new ArgumentNullException("command");
		}
		if (_commands != null)
		{
			_commands.Remove(command.CommandID);
		}
		OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandRemoved, null));
	}

	/// <summary>Removes the given verb from the document.</summary>
	/// <param name="verb">The verb to remove.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="verb" /> is <see langword="null" />.</exception>
	public virtual void RemoveVerb(DesignerVerb verb)
	{
		if (verb == null)
		{
			throw new ArgumentNullException("verb");
		}
		if (_globalVerbs.Contains(verb))
		{
			_globalVerbs.Remove(verb);
		}
		OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandRemoved, verb));
	}

	/// <summary>Shows the shortcut menu with the given command ID at the given location.</summary>
	/// <param name="menuID">The shortcut menu to display.</param>
	/// <param name="x">The x-coordinate of the shortcut menu's location.</param>
	/// <param name="y">The y-coordinate of the shortcut menu's location.</param>
	public virtual void ShowContextMenu(CommandID menuID, int x, int y)
	{
	}

	/// <summary>Gets a reference to the requested service.</summary>
	/// <param name="serviceType">The <see cref="T:System.Type" /> of the service to retrieve.</param>
	/// <returns>A reference to <paramref name="serviceType" />; otherwise, <see langword="null" /> if the service is not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serviceType" /> is <see langword="null" />.</exception>
	protected object GetService(Type serviceType)
	{
		if (_serviceProvider != null)
		{
			return _serviceProvider.GetService(serviceType);
		}
		return null;
	}
}
