using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Ookii.Dialogs.Properties;

namespace Ookii.Dialogs;

public class TaskDialogButton : TaskDialogItem
{
	private ButtonType _type;

	private bool _elevationRequired;

	private bool _default;

	private string _commandLinkNote;

	[Description("The type of the button.")]
	[DefaultValue(ButtonType.Custom)]
	[Category("Appearance")]
	public ButtonType ButtonType
	{
		get
		{
			return _type;
		}
		set
		{
			if (value != 0)
			{
				CheckDuplicateButton(value, null);
				_type = value;
				base.Id = (int)value;
			}
			else
			{
				_type = value;
				AutoAssignId();
				UpdateOwner();
			}
		}
	}

	[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
	[DefaultValue("")]
	[Localizable(true)]
	[Category("Appearance")]
	[Description("The text of the note associated with a command link button.")]
	public string CommandLinkNote
	{
		get
		{
			return _commandLinkNote ?? string.Empty;
		}
		set
		{
			_commandLinkNote = value;
			UpdateOwner();
		}
	}

	[Category("Behavior")]
	[DefaultValue(false)]
	[Description("Indicates if the button is the default button on the dialog.")]
	public bool Default
	{
		get
		{
			return _default;
		}
		set
		{
			_default = value;
			if (value && base.Owner != null)
			{
				foreach (TaskDialogButton button in base.Owner.Buttons)
				{
					if (button != this)
					{
						button.Default = false;
					}
				}
			}
			UpdateOwner();
		}
	}

	[Category("Behavior")]
	[Description("Indicates whether the Task Dialog button or command link should have a User Account Control (UAC) shield icon (in other words, whether the action invoked by the button requires elevation).")]
	[DefaultValue(false)]
	public bool ElevationRequired
	{
		get
		{
			return _elevationRequired;
		}
		set
		{
			_elevationRequired = value;
			if (base.Owner != null)
			{
				base.Owner.SetButtonElevationRequired(this);
			}
		}
	}

	internal override int Id
	{
		get
		{
			return base.Id;
		}
		set
		{
			if (base.Id != value)
			{
				if (_type != 0)
				{
					throw new InvalidOperationException(Resources.NonCustomTaskDialogButtonIdError);
				}
				base.Id = value;
			}
		}
	}

	internal NativeMethods.TaskDialogCommonButtonFlags ButtonFlag => _type switch
	{
		ButtonType.Ok => NativeMethods.TaskDialogCommonButtonFlags.OkButton, 
		ButtonType.Yes => NativeMethods.TaskDialogCommonButtonFlags.YesButton, 
		ButtonType.No => NativeMethods.TaskDialogCommonButtonFlags.NoButton, 
		ButtonType.Cancel => NativeMethods.TaskDialogCommonButtonFlags.CancelButton, 
		ButtonType.Retry => NativeMethods.TaskDialogCommonButtonFlags.RetryButton, 
		ButtonType.Close => NativeMethods.TaskDialogCommonButtonFlags.CloseButton, 
		_ => (NativeMethods.TaskDialogCommonButtonFlags)0, 
	};

	protected override IEnumerable ItemCollection
	{
		get
		{
			if (base.Owner != null)
			{
				return base.Owner.Buttons;
			}
			return null;
		}
	}

	public TaskDialogButton()
	{
	}

	public TaskDialogButton(ButtonType type)
		: base((int)type)
	{
		_type = type;
	}

	public TaskDialogButton(IContainer container)
		: base(container)
	{
	}

	public TaskDialogButton(string text)
	{
		base.Text = text;
	}

	internal override void AutoAssignId()
	{
		if (_type == ButtonType.Custom)
		{
			base.AutoAssignId();
		}
	}

	internal override void CheckDuplicate(TaskDialogItem itemToExclude)
	{
		CheckDuplicateButton(_type, itemToExclude);
		base.CheckDuplicate(itemToExclude);
	}

	private void CheckDuplicateButton(ButtonType type, TaskDialogItem itemToExclude)
	{
		if (type == ButtonType.Custom || base.Owner == null)
		{
			return;
		}
		foreach (TaskDialogButton button in base.Owner.Buttons)
		{
			if (button != this && button != itemToExclude && button.ButtonType == type)
			{
				throw new InvalidOperationException(Resources.DuplicateButtonTypeError);
			}
		}
	}
}
