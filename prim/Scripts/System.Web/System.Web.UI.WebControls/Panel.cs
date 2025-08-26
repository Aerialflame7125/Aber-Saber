using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a control that acts as a container for other controls.</summary>
[Designer("System.Web.UI.Design.WebControls.PanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(false)]
[PersistChildren(true)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Panel : WebControl
{
	private PanelStyle PanelStyle => base.ControlStyle as PanelStyle;

	/// <summary>Gets or sets the URL of the background image for the panel control.</summary>
	/// <returns>The URL of the background image for the panel control. The default is <see cref="F:System.String.Empty" />.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string BackImageUrl
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				if (PanelStyle != null)
				{
					return PanelStyle.BackImageUrl;
				}
				return ViewState.GetString("BackImageUrl", string.Empty);
			}
			return string.Empty;
		}
		set
		{
			if (PanelStyle != null)
			{
				PanelStyle.BackImageUrl = value;
			}
			else
			{
				ViewState["BackImageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the contents within the panel.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The horizontal alignment is not one of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. </exception>
	[DefaultValue(HorizontalAlign.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				if (PanelStyle != null)
				{
					return PanelStyle.HorizontalAlign;
				}
				if (ViewState["HorizontalAlign"] == null)
				{
					return HorizontalAlign.NotSet;
				}
				return (HorizontalAlign)ViewState["HorizontalAlign"];
			}
			return HorizontalAlign.NotSet;
		}
		set
		{
			if (PanelStyle != null)
			{
				PanelStyle.HorizontalAlign = value;
			}
			else
			{
				ViewState["HorizontalAlign"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the content wraps within the panel.</summary>
	/// <returns>
	///     <see langword="true" /> if the content wraps within the panel; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual bool Wrap
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				if (PanelStyle != null)
				{
					return PanelStyle.Wrap;
				}
				return ViewState.GetBool("Wrap", def: true);
			}
			return true;
		}
		set
		{
			if (PanelStyle != null)
			{
				PanelStyle.Wrap = value;
			}
			else
			{
				ViewState["Wrap"] = value;
			}
		}
	}

	/// <summary>Gets or sets the identifier for the default button that is contained in the <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	/// <returns>A string value corresponding to the <see cref="P:System.Web.UI.Control.ID" /> for a button control contained in the <see cref="T:System.Web.UI.WebControls.Panel" />. The default is an empty string, indicating that the <see cref="T:System.Web.UI.WebControls.Panel" /> does not have a default button.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	public virtual string DefaultButton
	{
		get
		{
			return ViewState.GetString("DefaultButton", string.Empty);
		}
		set
		{
			ViewState["DefaultButton"] = value;
		}
	}

	/// <summary>Gets or sets the direction in which to display controls that include text in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ContentDirection" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	[DefaultValue(ContentDirection.NotSet)]
	public virtual ContentDirection Direction
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				if (PanelStyle != null)
				{
					return PanelStyle.Direction;
				}
				if (ViewState["Direction"] == null)
				{
					return ContentDirection.NotSet;
				}
				return (ContentDirection)ViewState["Direction"];
			}
			return ContentDirection.NotSet;
		}
		set
		{
			if (PanelStyle != null)
			{
				PanelStyle.Direction = value;
			}
			else
			{
				ViewState["Direction"] = value;
			}
		}
	}

	/// <summary>Gets or sets the caption for the group of controls that is contained in the panel control.</summary>
	/// <returns>The caption text for the child controls contained in the panel control. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	public virtual string GroupingText
	{
		get
		{
			return ViewState.GetString("GroupingText", string.Empty);
		}
		set
		{
			ViewState["GroupingText"] = value;
		}
	}

	/// <summary>Gets or sets the visibility and position of scroll bars in a <see cref="T:System.Web.UI.WebControls.Panel" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ScrollBars" /> enumeration values. The default is <see langword="None" />.</returns>
	[DefaultValue(ScrollBars.None)]
	public virtual ScrollBars ScrollBars
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				if (PanelStyle != null)
				{
					return PanelStyle.ScrollBars;
				}
				if (ViewState["ScrollBars"] == null)
				{
					return ScrollBars.None;
				}
				return (ScrollBars)ViewState["Direction"];
			}
			return ScrollBars.None;
		}
		set
		{
			if (PanelStyle != null)
			{
				PanelStyle.ScrollBars = value;
			}
			else
			{
				ViewState["ScrollBars"] = value;
			}
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Panel" /> class.</summary>
	public Panel()
		: base(HtmlTextWriterTag.Div)
	{
	}

	/// <summary>Adds information about the background image, alignment, wrap, and direction to the list of attributes to render.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.Panel.DefaultButton" /> property of the <see cref="T:System.Web.UI.WebControls.Panel" /> control must be the ID of a control of type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		string backImageUrl = BackImageUrl;
		if (backImageUrl != "")
		{
			backImageUrl = ResolveClientUrl(backImageUrl);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, backImageUrl);
		}
		if (!string.IsNullOrEmpty(DefaultButton) && Page != null)
		{
			Control control = FindControl(DefaultButton);
			if (control == null || !(control is IButtonControl))
			{
				throw new InvalidOperationException($"The DefaultButton of '{ID}' must be the ID of a control of type IButtonControl.");
			}
			Page.ClientScript.RegisterWebFormClientScript();
			writer.AddAttribute("onkeypress", "javascript:return " + Page.WebFormScriptReference + ".WebForm_FireDefaultButton(event, '" + control.ClientID + "')");
		}
		if (Direction != 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Dir, (Direction == ContentDirection.RightToLeft) ? "rtl" : "ltr", fEncode: false);
		}
		switch (ScrollBars)
		{
		case ScrollBars.Auto:
			writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
			break;
		case ScrollBars.Both:
			writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
			break;
		case ScrollBars.Horizontal:
			writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowX, "scroll");
			break;
		case ScrollBars.Vertical:
			writer.AddStyleAttribute(HtmlTextWriterStyle.OverflowY, "scroll");
			break;
		}
		if (!Wrap)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
		}
		string text = "";
		switch (HorizontalAlign)
		{
		case HorizontalAlign.Center:
			text = "center";
			break;
		case HorizontalAlign.Justify:
			text = "justify";
			break;
		case HorizontalAlign.Left:
			text = "left";
			break;
		case HorizontalAlign.Right:
			text = "right";
			break;
		}
		if (text != "")
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, text);
		}
	}

	/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.Panel" /> control to implement all style related properties.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.PanelStyle" /> that contains the style properties of the control.</returns>
	protected override Style CreateControlStyle()
	{
		return new PanelStyle(ViewState);
	}

	/// <summary>Renders the HTML opening tag of the <see cref="T:System.Web.UI.WebControls.Panel" /> control to the specified writer.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	public override void RenderBeginTag(HtmlTextWriter writer)
	{
		base.RenderBeginTag(writer);
		if (!string.IsNullOrEmpty(GroupingText))
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
			writer.RenderBeginTag(HtmlTextWriterTag.Legend);
			writer.Write(GroupingText);
			writer.RenderEndTag();
		}
	}

	/// <summary>Renders the HTML closing tag of the <see cref="T:System.Web.UI.WebControls.Panel" /> control into the specified writer.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	public override void RenderEndTag(HtmlTextWriter writer)
	{
		if (!string.IsNullOrEmpty(GroupingText))
		{
			writer.RenderEndTag();
		}
		base.RenderEndTag(writer);
	}
}
