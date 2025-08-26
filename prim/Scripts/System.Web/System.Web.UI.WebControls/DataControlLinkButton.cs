using System.Drawing;

namespace System.Web.UI.WebControls;

internal class DataControlLinkButton : LinkButton, IDataControlButton, IButtonControl
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

	public ButtonType ButtonType => ButtonType.Link;

	protected internal override void Render(HtmlTextWriter writer)
	{
		EnsureForeColor();
		if (AllowCallback && Container is ICallbackContainer callbackContainer)
		{
			OnClientClick = ClientScriptManager.EnsureEndsWithSemicolon(OnClientClick) + callbackContainer.GetCallbackScript(this, base.CommandName + "$" + base.CommandArgument);
		}
		base.Render(writer);
	}

	private void EnsureForeColor()
	{
		if (ForeColor != Color.Empty)
		{
			return;
		}
		Control parent = Parent;
		while (parent != null)
		{
			if (parent is WebControl webControl && webControl.ForeColor != Color.Empty)
			{
				ForeColor = webControl.ForeColor;
				break;
			}
			if (parent != Container)
			{
				parent = parent.Parent;
				continue;
			}
			break;
		}
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
