using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls;

/// <summary>Displays a text box control for user input.</summary>
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultEvent("TextChanged")]
[DefaultProperty("Text")]
[ValidationProperty("Text")]
[ControlBuilder(typeof(TextBoxControlBuilder))]
[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(true, "Text")]
[ControlValueProperty("Text", null)]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TextBox : WebControl, IPostBackDataHandler, IEditableTextControl, ITextControl
{
	private static readonly string[] VCardValues = new string[32]
	{
		null, null, "vCard.Cellular", "vCard.Company", "vCard.Department", "vCard.DisplayName", "vCard.Email", "vCard.FirstName", "vCard.Gender", "vCard.Home.City",
		"HomeCountry", "vCard.Home.Fax", "vCard.Home.Phone", "vCard.Home.State", "vCard.Home.StreetAddress", "vCard.Home.ZipCode", "vCard.Home.page", "vCard.JobTitle", "vCard.LastName", "vCard.MiddleName",
		"vCard.Notes", "vCard.Office", "vCard.Pager", "vCard.Business.City", "BusinessCountry", "vCard.Business.Fax", "vCard.Business.Phone", "vCard.Business.State", "vCard.Business.StreetAddress", "vCard.Business.Url",
		"vCard.Business.ZipCode", "search"
	};

	private static readonly object TextChangedEvent;

	/// <summary>Gets or sets a value that indicates the AutoComplete behavior of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.AutoCompleteType" /> enumeration values, indicating the AutoComplete behavior for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control. The default value is <see cref="F:System.Web.UI.WebControls.AutoCompleteType.None" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.AutoCompleteType" /> enumeration values.</exception>
	[DefaultValue(AutoCompleteType.None)]
	[Themeable(false)]
	public virtual AutoCompleteType AutoCompleteType
	{
		get
		{
			object obj = ViewState["AutoCompleteType"];
			if (obj == null)
			{
				return AutoCompleteType.None;
			}
			return (AutoCompleteType)obj;
		}
		set
		{
			ViewState["AutoCompleteType"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether an automatic postback to the server occurs when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control loses focus.</summary>
	/// <returns>
	///     <see langword="true" /> if an automatic postback occurs when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control loses focus; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AutoPostBack
	{
		get
		{
			return ViewState.GetBool("AutoPostBack", def: false);
		}
		set
		{
			ViewState["AutoPostBack"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control is set to validate when a postback occurs.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.WebControls.TextBox" /> control is set to validate when a postback occurs; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	public virtual bool CausesValidation
	{
		get
		{
			return ViewState.GetBool("CausesValidation", def: false);
		}
		set
		{
			ViewState["CausesValidation"] = value;
		}
	}

	/// <summary>Gets or sets the display width of the text box in characters.</summary>
	/// <returns>The display width, in characters, of the text box. The default is 0, which indicates that the property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified width is less than 0. </exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int Columns
	{
		get
		{
			return ViewState.GetInt("Columns", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", "Columns value has to be 0 for 'not set' or bigger than 0.");
			}
			ViewState["Columns"] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of characters allowed in the text box.</summary>
	/// <returns>The maximum number of characters allowed in the text box. The default is 0, which indicates that the property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified width is less than 0. </exception>
	[DefaultValue(0)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual int MaxLength
	{
		get
		{
			return ViewState.GetInt("MaxLength", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", "MaxLength value has to be 0 for 'not set' or bigger than 0.");
			}
			ViewState["MaxLength"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the contents of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control can be changed.</summary>
	/// <returns>
	///     <see langword="true" /> if the contents of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control cannot be changed; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[Bindable(true)]
	[DefaultValue(false)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ReadOnly
	{
		get
		{
			return ViewState.GetBool("ReadOnly", def: false);
		}
		set
		{
			ViewState["ReadOnly"] = value;
		}
	}

	/// <summary>Gets or sets the number of rows displayed in a multiline text box.</summary>
	/// <returns>The number of rows in a multiline text box. The default is 0, which displays a two-line text box.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
	[DefaultValue(0)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual int Rows
	{
		get
		{
			return ViewState.GetInt("Rows", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", "Rows value has to be 0 for 'not set' or bigger than 0.");
			}
			ViewState["Rows"] = value;
		}
	}

	/// <summary>Gets the HTML tag for the text box control. This property is protected.</summary>
	/// <returns>
	///     <see cref="F:System.Web.UI.HtmlTextWriterTag.Textarea" /> if the text box is multiline; otherwise, <see cref="F:System.Web.UI.HtmlTextWriterTag.Input" />.</returns>
	protected override HtmlTextWriterTag TagKey
	{
		get
		{
			if (TextMode != TextBoxMode.MultiLine)
			{
				return HtmlTextWriterTag.Input;
			}
			return HtmlTextWriterTag.Textarea;
		}
	}

	/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
	/// <returns>The text displayed in the <see cref="T:System.Web.UI.WebControls.TextBox" /> control. The default is an empty string ("").</returns>
	[Bindable(true, BindingDirection.TwoWay)]
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
	[Localizable(true)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			return ViewState.GetString("Text", "");
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets the behavior mode (such as, single-line, multiline, or password) of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextBoxMode" /> enumeration values. The default value is <see langword="SingleLine" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified mode is not one of the <see cref="T:System.Web.UI.WebControls.TextBoxMode" /> enumeration values. </exception>
	[DefaultValue(TextBoxMode.SingleLine)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual TextBoxMode TextMode
	{
		get
		{
			return (TextBoxMode)ViewState.GetInt("TextMode", 0);
		}
		set
		{
			ViewState["TextMode"] = (int)value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.TextBox" /> control causes validation when it posts back to the server. </summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.TextBox" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
	[Themeable(false)]
	[DefaultValue("")]
	public virtual string ValidationGroup
	{
		get
		{
			return ViewState.GetString("ValidationGroup", "");
		}
		set
		{
			ViewState["ValidationGroup"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text content wraps within a multiline text box.</summary>
	/// <returns>
	///     <see langword="true" /> if the text content wraps within a multiline text box; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual bool Wrap
	{
		get
		{
			return ViewState.GetBool("Wrap", def: true);
		}
		set
		{
			ViewState["Wrap"] = value;
		}
	}

	/// <summary>Occurs when the content of the text box changes between posts to the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler TextChanged
	{
		add
		{
			base.Events.AddHandler(TextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TextChangedEvent, value);
		}
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> instance.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Page page = Page;
		page?.VerifyRenderingInServerForm(this);
		switch (TextMode)
		{
		case TextBoxMode.MultiLine:
			if (Columns != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cols, Columns.ToString(), fEncode: false);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cols, "20", fEncode: false);
			}
			if (Rows != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Rows, Rows.ToString(), fEncode: false);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Rows, "2", fEncode: false);
			}
			if (!Wrap)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Wrap, "off", fEncode: false);
			}
			break;
		case TextBoxMode.SingleLine:
		case TextBoxMode.Password:
			if (TextMode == TextBoxMode.Password)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "password", fEncode: false);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text", fEncode: false);
				if (Text.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);
				}
			}
			if (Columns != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Size, Columns.ToString(), fEncode: false);
			}
			if (MaxLength != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, MaxLength.ToString(), fEncode: false);
			}
			if (AutoCompleteType != 0 && TextMode == TextBoxMode.SingleLine)
			{
				if (AutoCompleteType != AutoCompleteType.Disabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.VCardName, VCardValues[(int)AutoCompleteType]);
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off", fEncode: false);
				}
			}
			break;
		}
		if (AutoPostBack)
		{
			writer.AddAttribute("onkeypress", "if (WebForm_TextBoxKeyHandler(event) == false) return false;", fEndode: false);
			if (page != null)
			{
				string postBackEventReference = page.ClientScript.GetPostBackEventReference(GetPostBackOptions(), registerForEventValidation: true);
				postBackEventReference = "setTimeout('" + postBackEventReference.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, BuildScriptAttribute("onchange", postBackEventReference));
			}
		}
		else
		{
			page?.ClientScript.RegisterForEventValidation(UniqueID, string.Empty);
		}
		if (ReadOnly)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "ReadOnly", fEncode: false);
		}
		writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
		base.AddAttributesToRender(writer);
	}

	/// <summary>Overridden to allow only literal controls to be added as the <see cref="P:System.Web.UI.WebControls.TextBox.Text" /> property.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
	protected override void AddParsedSubObject(object obj)
	{
		if (obj is LiteralControl literalControl)
		{
			Text = literalControl.Text;
		}
	}

	/// <summary>Registers client script for generating postback events prior to rendering on the client, if <see cref="P:System.Web.UI.WebControls.TextBox.AutoPostBack" /> is <see langword="true" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		if (AutoPostBack)
		{
			RegisterKeyHandlerClientScript();
		}
		Page page = Page;
		if (page != null && base.IsEnabled)
		{
			page.RegisterEnabledControl(this);
		}
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.TextBox" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		RenderBeginTag(writer);
		if (TextMode == TextBoxMode.MultiLine)
		{
			writer.WriteLine();
			HttpUtility.HtmlEncode(Text, writer);
		}
		RenderEndTag(writer);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
	/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
	/// <param name="postCollection">The collection posted to the server. </param>
	/// <returns>
	///     <see langword="true" /> if the posted content is different from the last posting; otherwise, <see langword="false" />. </returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		ValidateEvent(postDataKey, string.Empty);
		if (Text != postCollection[postDataKey])
		{
			Text = postCollection[postDataKey];
			return true;
		}
		return false;
	}

	/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.TextBox.OnTextChanged(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.WebControls.TextBox" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		OnTextChanged(EventArgs.Empty);
	}

	/// <summary>Loads the posted text box content if it is different from the last posting. </summary>
	/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
	/// <param name="postCollection">The collection posted to the server. </param>
	/// <returns>
	///     <see langword="true" /> if the posted content is different from the last posting; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.TextBox.OnTextChanged(System.EventArgs)" /> method whenever posted data for the text box has changed. </summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>Saves the changes to the text box view state since the time the page was posted back to the server.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the changes to the <see cref="T:System.Web.UI.WebControls.TextBox" /> view state. If no view state is associated with the object, this method returns <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		if (TextMode == TextBoxMode.Password)
		{
			ViewState.SetItemDirty("Text", dirty: false);
		}
		return base.SaveViewState();
	}

	private PostBackOptions GetPostBackOptions()
	{
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ActionUrl = null;
		postBackOptions.ValidationGroup = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = false;
		postBackOptions.ClientSubmit = true;
		Page page = Page;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	private void RegisterKeyHandlerClientScript()
	{
		if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(TextBox), "KeyHandler"))
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("function WebForm_TextBoxKeyHandler(event) {");
			stringBuilder.AppendLine("\tvar target = event.target;");
			stringBuilder.AppendLine("\tif ((target == null) || (typeof(target) == \"undefined\")) target = event.srcElement;");
			stringBuilder.AppendLine("\tif (event.keyCode == 13) {");
			stringBuilder.AppendLine("\t\tif ((typeof(target) != \"undefined\") && (target != null)) {");
			stringBuilder.AppendLine("\t\t\tif (typeof(target.onchange) != \"undefined\") {");
			stringBuilder.AppendLine("\t\t\t\ttarget.onchange();");
			stringBuilder.AppendLine("\t\t\t\tevent.cancelBubble = true;");
			stringBuilder.AppendLine("\t\t\t\tif (event.stopPropagation) event.stopPropagation();");
			stringBuilder.AppendLine("\t\t\t\treturn false;");
			stringBuilder.AppendLine("\t\t\t}");
			stringBuilder.AppendLine("\t\t}");
			stringBuilder.AppendLine("\t}");
			stringBuilder.AppendLine("\treturn true;");
			stringBuilder.AppendLine("}");
			Page.ClientScript.RegisterClientScriptBlock(typeof(TextBox), "KeyHandler", stringBuilder.ToString(), addScriptTags: true);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TextBox.TextChanged" /> event. This allows you to handle the event directly.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event information. </param>
	protected virtual void OnTextChanged(EventArgs e)
	{
		((EventHandler)base.Events[TextChanged])?.Invoke(this, e);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TextBox" /> class.</summary>
	public TextBox()
	{
	}

	static TextBox()
	{
		TextChanged = new object();
	}
}
