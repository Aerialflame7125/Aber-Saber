using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays a summary of all validation errors inline on a Web page, in a message box, or both. </summary>
[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ValidationSummary : WebControl
{
	private bool pre_render_called;

	private bool has_errors;

	/// <summary>Gets or sets the display mode of the validation summary.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationSummaryDisplayMode" /> values. The default is <see langword="BulletList" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The display mode is not one of the <see cref="T:System.Web.UI.WebControls.ValidationSummaryDisplayMode" /> values. </exception>
	[DefaultValue(ValidationSummaryDisplayMode.BulletList)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public ValidationSummaryDisplayMode DisplayMode
	{
		get
		{
			object obj = ViewState["DisplayMode"];
			if (obj != null)
			{
				return (ValidationSummaryDisplayMode)obj;
			}
			return ValidationSummaryDisplayMode.BulletList;
		}
		set
		{
			ViewState["DisplayMode"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control updates itself using client-side script.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control updates itself using client-side script; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public bool EnableClientScript
	{
		get
		{
			return ViewState.GetBool("EnableClientScript", def: true);
		}
		set
		{
			ViewState["EnableClientScript"] = value;
		}
	}

	/// <summary>Gets or sets the foreground color of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see langword="Red" />.</returns>
	[DefaultValue(typeof(Color), "Red")]
	public override Color ForeColor
	{
		get
		{
			return base.ForeColor;
		}
		set
		{
			base.ForeColor = value;
		}
	}

	/// <summary>Gets or sets the header text displayed at the top of the summary.</summary>
	/// <returns>The header text displayed at the top of the summary. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string HeaderText
	{
		get
		{
			return ViewState.GetString("HeaderText", string.Empty);
		}
		set
		{
			ViewState["HeaderText"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the validation summary is displayed in a message box.</summary>
	/// <returns>
	///     <see langword="true" /> if the validation summary is to be displayed in a message box; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public bool ShowMessageBox
	{
		get
		{
			return ViewState.GetBool("ShowMessageBox", def: false);
		}
		set
		{
			ViewState["ShowMessageBox"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the validation summary is displayed inline.</summary>
	/// <returns>
	///     <see langword="true" /> if the validation summary is displayed inline; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public bool ShowSummary
	{
		get
		{
			return ViewState.GetBool("ShowSummary", def: true);
		}
		set
		{
			ViewState["ShowSummary"] = value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> object displays validation messages.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> object displays validation messages.</returns>
	[DefaultValue("")]
	[Themeable(false)]
	public virtual string ValidationGroup
	{
		get
		{
			return ViewState.GetString("ValidationGroup", string.Empty);
		}
		set
		{
			ViewState["ValidationGroup"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> class.</summary>
	public ValidationSummary()
		: base(HtmlTextWriterTag.Div)
	{
		ForeColor = Color.Red;
	}

	/// <summary>Adds attributes to the HTML tags generated for this control.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	[MonoTODO]
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (EnableClientScript && pre_render_called && Page.AreValidatorsUplevel(ValidationGroup))
		{
			if (ID == null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
			}
			if (ValidationGroup != string.Empty)
			{
				RegisterExpandoAttribute(ClientID, "validationGroup", ValidationGroup);
			}
			if (HeaderText.Length > 0)
			{
				RegisterExpandoAttribute(ClientID, "headertext", HeaderText);
			}
			if (ShowMessageBox)
			{
				RegisterExpandoAttribute(ClientID, "showmessagebox", "True");
			}
			if (!ShowSummary)
			{
				RegisterExpandoAttribute(ClientID, "showsummary", "False");
			}
			if (DisplayMode != ValidationSummaryDisplayMode.BulletList)
			{
				RegisterExpandoAttribute(ClientID, "displaymode", DisplayMode.ToString());
			}
			if (!has_errors)
			{
				writer.AddStyleAttribute("display", "none");
			}
		}
	}

	internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
	{
		RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode: false);
	}

	internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
	{
		if (Page.ScriptManager != null)
		{
			Page.ScriptManager.RegisterExpandoAttributeExternal(this, controlId, attributeName, attributeValue, encode);
		}
		else
		{
			Page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">The event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		if (base.RenderingCompatibilityLessThan40 && ForeColor == Color.Empty)
		{
			ForeColor = Color.Red;
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		pre_render_called = true;
	}

	/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (!base.IsEnabled)
		{
			return;
		}
		ValidatorCollection validators = Page.GetValidators(ValidationGroup);
		ArrayList arrayList = new ArrayList(validators.Count);
		for (int i = 0; i < validators.Count; i++)
		{
			if (!validators[i].IsValid)
			{
				arrayList.Add(validators[i].ErrorMessage);
			}
		}
		has_errors = arrayList.Count > 0;
		if (EnableClientScript && pre_render_called && Page.AreValidatorsUplevel(ValidationGroup))
		{
			if (Page.ScriptManager != null)
			{
				Page.ScriptManager.RegisterArrayDeclarationExternal(this, "Page_ValidationSummaries", "document.getElementById ('" + ClientID + "')");
				Page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), ClientID + "DisposeScript", "\ndocument.getElementById('" + ClientID + "').dispose = function() {\n\tArray.remove(Page_ValidationSummaries, document.getElementById('" + ClientID + "'));\n}\n", addScriptTags: true);
			}
			else
			{
				Page.ClientScript.RegisterArrayDeclaration("Page_ValidationSummaries", "document.getElementById ('" + ClientID + "')");
			}
		}
		if ((ShowSummary && has_errors) || (EnableClientScript && pre_render_called))
		{
			base.RenderBeginTag(writer);
		}
		if (ShowSummary && has_errors)
		{
			switch (DisplayMode)
			{
			case ValidationSummaryDisplayMode.BulletList:
			{
				if (HeaderText.Length > 0)
				{
					writer.Write(HeaderText);
				}
				writer.Write("<ul>");
				for (int k = 0; k < arrayList.Count; k++)
				{
					writer.Write("<li>");
					writer.Write(arrayList[k]);
					writer.Write("</li>");
				}
				writer.Write("</ul>");
				break;
			}
			case ValidationSummaryDisplayMode.List:
			{
				if (HeaderText.Length > 0)
				{
					writer.Write(HeaderText);
					writer.Write("<br />");
				}
				for (int l = 0; l < arrayList.Count; l++)
				{
					writer.Write(arrayList[l]);
					writer.Write("<br />");
				}
				break;
			}
			case ValidationSummaryDisplayMode.SingleParagraph:
			{
				if (HeaderText.Length > 0)
				{
					writer.Write(HeaderText);
					writer.Write(" ");
				}
				for (int j = 0; j < arrayList.Count; j++)
				{
					writer.Write(arrayList[j]);
					writer.Write(" ");
				}
				writer.Write("<br />");
				break;
			}
			}
		}
		if ((ShowSummary && has_errors) || (EnableClientScript && pre_render_called))
		{
			base.RenderEndTag(writer);
		}
	}
}
