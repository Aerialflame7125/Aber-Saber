using System;
using System.Collections.Generic;

namespace UnityEngine.Experimental.UIElements;

public class ContextualMenu
{
	public abstract class MenuItem
	{
	}

	public class Separator : MenuItem
	{
	}

	public class MenuAction : MenuItem
	{
		[Flags]
		public enum StatusFlags
		{
			Normal = 0,
			Disabled = 1,
			Checked = 2,
			Hidden = 4
		}

		public string name;

		private Action<EventBase> actionCallback;

		private Func<EventBase, StatusFlags> actionStatusCallback;

		public StatusFlags status { get; private set; }

		public MenuAction(string actionName, Action<EventBase> actionCallback, Func<EventBase, StatusFlags> actionStatusCallback)
		{
			name = actionName;
			this.actionCallback = actionCallback;
			this.actionStatusCallback = actionStatusCallback;
		}

		public static StatusFlags AlwaysEnabled(EventBase e)
		{
			return StatusFlags.Normal;
		}

		public static StatusFlags AlwaysDisabled(EventBase e)
		{
			return StatusFlags.Disabled;
		}

		public void UpdateActionStatus(EventBase e)
		{
			status = ((actionStatusCallback == null) ? StatusFlags.Hidden : actionStatusCallback(e));
		}

		public void Execute(EventBase e)
		{
			if (actionCallback != null)
			{
				actionCallback(e);
			}
		}
	}

	private List<MenuItem> menuItems = new List<MenuItem>();

	public List<MenuItem> MenuItems()
	{
		return menuItems;
	}

	public void AppendAction(string actionName, Action<EventBase> action, Func<EventBase, MenuAction.StatusFlags> actionStatusCallback)
	{
		MenuAction item = new MenuAction(actionName, action, actionStatusCallback);
		menuItems.Add(item);
	}

	public void InsertAction(string actionName, Action<EventBase> action, Func<EventBase, MenuAction.StatusFlags> actionStatusCallback, int atIndex)
	{
		MenuAction item = new MenuAction(actionName, action, actionStatusCallback);
		menuItems.Insert(atIndex, item);
	}

	public void AppendSeparator()
	{
		if (menuItems.Count > 0 && !(menuItems[menuItems.Count - 1] is Separator))
		{
			Separator item = new Separator();
			menuItems.Add(item);
		}
	}

	public void InsertSeparator(int atIndex)
	{
		if (atIndex > 0 && atIndex <= menuItems.Count && !(menuItems[atIndex - 1] is Separator))
		{
			Separator item = new Separator();
			menuItems.Insert(atIndex, item);
		}
	}

	public void PrepareForDisplay(EventBase e)
	{
		foreach (MenuItem menuItem in menuItems)
		{
			if (menuItem is MenuAction menuAction)
			{
				menuAction.UpdateActionStatus(e);
			}
		}
		if (menuItems[menuItems.Count - 1] is Separator)
		{
			menuItems.RemoveAt(menuItems.Count - 1);
		}
	}
}
