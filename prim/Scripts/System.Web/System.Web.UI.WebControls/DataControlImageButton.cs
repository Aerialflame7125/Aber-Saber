namespace System.Web.UI.WebControls;

internal class DataControlImageButton : ImageButton, IDataControlButton, IButtonControl
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

	public ButtonType ButtonType => ButtonType.Image;

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
