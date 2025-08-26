namespace System.Web.UI.WebControls;

[SupportsEventValidation]
internal class DataControlButton : Button, IDataControlButton, IButtonControl
{
	private Control _container;

	public Control Container
	{
		get
		{
			return _container;
		}
		set
		{
			_container = value;
		}
	}

	public string ImageUrl
	{
		get
		{
			return string.Empty;
		}
		set
		{
		}
	}

	public bool AllowCallback
	{
		get
		{
			return ViewState.GetBool("AllowCallback", def: true);
		}
		set
		{
			ViewState["AllowCallback"] = value;
		}
	}

	public ButtonType ButtonType => ButtonType.Button;

	public override bool UseSubmitBehavior
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	public static IDataControlButton CreateButton(ButtonType type, Control container, string text, string image, string command, string commandArg, bool allowCallback)
	{
		IDataControlButton dataControlButton;
		switch (type)
		{
		case ButtonType.Link:
			dataControlButton = new DataControlLinkButton();
			break;
		case ButtonType.Image:
			dataControlButton = new DataControlImageButton();
			dataControlButton.ImageUrl = image;
			break;
		default:
			dataControlButton = new DataControlButton();
			break;
		}
		dataControlButton.Container = container;
		dataControlButton.CommandName = command;
		dataControlButton.CommandArgument = commandArg;
		dataControlButton.Text = text;
		dataControlButton.CausesValidation = false;
		dataControlButton.AllowCallback = allowCallback;
		return dataControlButton;
	}

	internal override string GetClientScriptEventReference()
	{
		if (AllowCallback && Container is ICallbackContainer callbackContainer)
		{
			return callbackContainer.GetCallbackScript(this, base.CommandName + "$" + base.CommandArgument);
		}
		return base.GetClientScriptEventReference();
	}

	protected override PostBackOptions GetPostBackOptions()
	{
		if (Container is IPostBackContainer postBackContainer)
		{
			return postBackContainer.GetPostBackOptions(this);
		}
		return base.GetPostBackOptions();
	}
}
