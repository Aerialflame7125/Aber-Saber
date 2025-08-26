using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A control that displays a link to another Web page.</summary>
[ControlBuilder(typeof(HyperLinkControlBuilder))]
[DataBindingHandler("System.Web.UI.Design.HyperLinkDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ParseChildren(false)]
[ToolboxData("<{0}:HyperLink runat=\"server\">HyperLink</{0}:HyperLink>")]
[DefaultProperty("Text")]
[Designer("System.Web.UI.Design.WebControls.HyperLinkDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HyperLink : WebControl
{
	/// <summary>Gets or sets the path to an image to display for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
	/// <returns>The path to the image to display for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[UrlProperty]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string ImageUrl
	{
		get
		{
			return ViewState.GetString("ImageUrl", string.Empty);
		}
		set
		{
			ViewState["ImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the URL to link to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked.</summary>
	/// <returns>The URL to link to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked. The default value is an empty string ('').</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[UrlProperty]
	[WebSysDescription("")]
	[WebCategory("Navigation")]
	public string NavigateUrl
	{
		get
		{
			return ViewState.GetString("NavigateUrl", string.Empty);
		}
		set
		{
			ViewState["NavigateUrl"] = value;
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked.</summary>
	/// <returns>The target window or frame to load the Web page linked to when the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control is clicked. Values must begin with a letter in the range of a through z (case-insensitive), except for the special values listed in the following table, which begin with an underscore.
	///             <see langword="_blank " />
	///           Renders the content in a new window without frames. 
	///             <see langword="_parent " />
	///           Renders the content in the immediate frameset parent. 
	///             <see langword="_search" />
	///           Renders the content in the search pane.
	///             <see langword="_self " />
	///           Renders the content in the frame with focus. 
	///             <see langword="_top " />
	///           Renders the content in the full window without frames. Check your browser documentation to determine if the <see langword="_search" /> value is supported.  For example, Microsoft Internet Explorer 5.0 and later support the <see langword="_search" /> target value.The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[TypeConverter(typeof(TargetConverter))]
	[WebSysDescription("")]
	[WebCategory("Navigation")]
	public string Target
	{
		get
		{
			return ViewState.GetString("Target", string.Empty);
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
	/// <returns>The text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. The default value is an empty string ("").</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			return ViewState.GetString("Text", string.Empty);
		}
		set
		{
			ViewState["Text"] = value;
			if (HasControls())
			{
				Controls.Clear();
			}
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLink" /> class.</summary>
	public HyperLink()
		: base(HtmlTextWriterTag.A)
	{
	}

	/// <summary>Adds the attributes of a <see cref="T:System.Web.UI.WebControls.HyperLink" /> control to the output stream for rendering.</summary>
	/// <param name="writer">The output stream to render on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		AddDisplayStyleAttribute(writer);
		if (base.IsEnabled)
		{
			string target = Target;
			string navigateUrl = NavigateUrl;
			if (navigateUrl.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, ResolveClientUrl(navigateUrl));
			}
			if (target.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Target, target);
			}
		}
	}

	/// <summary>Notifies the control that an element was parsed, and adds the element to the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control. </summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
	protected override void AddParsedSubObject(object obj)
	{
		if (HasControls())
		{
			base.AddParsedSubObject(obj);
		}
		else if (!(obj is LiteralControl literalControl))
		{
			string text = Text;
			if (text.Length != 0)
			{
				Text = null;
				Controls.Add(new LiteralControl(text));
			}
			base.AddParsedSubObject(obj);
		}
		else
		{
			Text = literalControl.Text;
		}
	}

	/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method.</summary>
	/// <param name="savedState">An object that contains the previously saved state.</param>
	[MonoTODO("Why override?")]
	protected override void LoadViewState(object savedState)
	{
		base.LoadViewState(savedState);
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control on a page.</summary>
	/// <param name="writer">The output stream to render on the client. </param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (HasControls() || HasRenderMethodDelegate())
		{
			base.RenderContents(writer);
			return;
		}
		string imageUrl = ImageUrl;
		if (!string.IsNullOrEmpty(imageUrl))
		{
			string toolTip = ToolTip;
			if (!string.IsNullOrEmpty(toolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(imageUrl));
			toolTip = Text;
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, toolTip);
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}
		else
		{
			writer.Write(Text);
		}
	}
}
