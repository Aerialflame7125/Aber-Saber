using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Represents the design-time version of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> control. This class cannot be inherited.</summary>
[ToolboxItem(false)]
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DesignerDataBoundLiteralControl : Control
{
	private string text = string.Empty;

	/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> control.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the text in the &lt;%# … %&gt; data-binding expression.</returns>
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (value == null)
			{
				text = string.Empty;
			}
			else
			{
				text = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignerDataBoundLiteralControl" /> class.</summary>
	public DesignerDataBoundLiteralControl()
	{
		base.AutoID = false;
	}

	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	protected override void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			text = (string)savedState;
		}
	}

	protected internal override void Render(HtmlTextWriter output)
	{
		output.Write(text);
	}

	protected override object SaveViewState()
	{
		return text;
	}
}
