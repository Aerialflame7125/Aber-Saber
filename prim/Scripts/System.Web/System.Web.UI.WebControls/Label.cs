using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a label control, which displays text on a Web page.</summary>
[ControlBuilder(typeof(LabelControlBuilder))]
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultProperty("Text")]
[Designer("System.Web.UI.Design.WebControls.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(false)]
[ToolboxData("<{0}:Label runat=\"server\" Text=\"Label\"></{0}:Label>")]
[ControlValueProperty("Text", null)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Label : WebControl, ITextControl
{
	/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
	/// <returns>The text content of the control. The default value is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets the identifier for a server control that the <see cref="T:System.Web.UI.WebControls.Label" /> control is associated with.</summary>
	/// <returns>A string value corresponding to the <see cref="P:System.Web.UI.Control.ID" /> for a server control contained in the Web form. The default is an empty string (""), indicating that the <see cref="T:System.Web.UI.WebControls.Label" /> control is not associated with another server control.</returns>
	[IDReferenceProperty(typeof(Control))]
	[TypeConverter(typeof(AssociatedControlConverter))]
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual string AssociatedControlID
	{
		get
		{
			return ViewState.GetString("AssociatedControlID", string.Empty);
		}
		set
		{
			ViewState["AssociatedControlID"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value used to render the <see cref="T:System.Web.UI.WebControls.Label" />.</returns>
	protected override HtmlTextWriterTag TagKey
	{
		get
		{
			if (!string.IsNullOrEmpty(AssociatedControlID))
			{
				return HtmlTextWriterTag.Label;
			}
			return HtmlTextWriterTag.Span;
		}
	}

	/// <summary>Loads the previously saved state for the control. </summary>
	/// <param name="savedState">An object that contains the saved view state values for the control. </param>
	protected override void LoadViewState(object savedState)
	{
		base.LoadViewState(savedState);
		if (ViewState["Text"] != null)
		{
			Text = (string)ViewState["Text"];
		}
	}

	/// <summary>Notifies the control that an element was parsed and adds the element to the <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
	/// <param name="obj">An object that represents the parsed element.</param>
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

	/// <summary>Renders the contents of the <see cref="T:System.Web.UI.WebControls.Label" /> into the specified writer.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (HasControls() || HasRenderMethodDelegate())
		{
			base.RenderContents(writer);
		}
		else
		{
			writer.Write(Text);
		}
	}

	/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.Label" /> control to render to the specified output stream. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	/// <exception cref="T:System.Web.HttpException">The control specified in the <see cref="P:System.Web.UI.WebControls.Label.AssociatedControlID" /> property cannot be found.</exception>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (!string.IsNullOrEmpty(AssociatedControlID))
		{
			writer.AddAttribute(HtmlTextWriterAttribute.For, NamingContainer.FindControl(AssociatedControlID).ClientID);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Label" /> class.</summary>
	public Label()
	{
	}
}
