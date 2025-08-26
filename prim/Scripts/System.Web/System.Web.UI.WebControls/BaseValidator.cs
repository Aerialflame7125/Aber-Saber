using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the abstract base class for validation controls.</summary>
[DefaultProperty("ErrorMessage")]
[Designer("System.Web.UI.Design.WebControls.BaseValidatorDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class BaseValidator : Label, IValidator
{
	private bool render_uplevel;

	private bool valid;

	private Color forecolor;

	private bool pre_render_called;

	/// <summary>This property is not supported.</summary>
	/// <returns>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set this property.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string AssociatedControlID
	{
		get
		{
			return base.AssociatedControlID;
		}
		set
		{
			base.AssociatedControlID = value;
		}
	}

	/// <summary>Gets or sets the name of the validation group to which this validation control belongs.</summary>
	/// <returns>The name of the validation group to which this validation control belongs. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Themeable(false)]
	[DefaultValue("")]
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

	/// <summary>Gets or sets a value that indicates whether focus is set to the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property when validation fails.</summary>
	/// <returns>
	///     <see langword="true" /> to set focus on the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> when validation fails; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Themeable(false)]
	[DefaultValue(false)]
	public bool SetFocusOnError
	{
		get
		{
			return ViewState.GetBool("SetFocusOnError", def: false);
		}
		set
		{
			ViewState["SetFocusOnError"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed in the validation control when validation fails.</summary>
	/// <returns>The text displayed in the validation control when validation fails. The default is an empty string (""), which indicates that this property is not set.</returns>
	[MonoTODO("Why override?")]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[DefaultValue("")]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <summary>Gets or sets the input control to validate.</summary>
	/// <returns>The input control to validate. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	[IDReferenceProperty(typeof(Control))]
	[Themeable(false)]
	[TypeConverter(typeof(ValidatedControlConverter))]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string ControlToValidate
	{
		get
		{
			return ViewState.GetString("ControlToValidate", string.Empty);
		}
		set
		{
			ViewState["ControlToValidate"] = value;
		}
	}

	/// <summary>Gets or sets the display behavior of the error message in a validation control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidatorDisplay" /> values. The default value is <see langword="Static" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.ValidatorDisplay" /> values. </exception>
	[Themeable(false)]
	[DefaultValue(ValidatorDisplay.Static)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public ValidatorDisplay Display
	{
		get
		{
			return (ValidatorDisplay)ViewState.GetInt("Display", 1);
		}
		set
		{
			ViewState["Display"] = (int)value;
		}
	}

	/// <summary>Gets or sets a value indicating whether client-side validation is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if client-side validation is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[Themeable(false)]
	[DefaultValue(true)]
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

	/// <summary>Gets or sets a value that indicates whether the validation control is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the validation control is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool Enabled
	{
		get
		{
			return ViewState.GetBool("BaseValidatorEnabled", def: true);
		}
		set
		{
			ViewState["BaseValidatorEnabled"] = value;
		}
	}

	/// <summary>Gets or sets the text for the error message displayed in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when validation fails.</summary>
	/// <returns>The error message displayed in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when validation fails. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string ErrorMessage
	{
		get
		{
			return ViewState.GetString("ErrorMessage", string.Empty);
		}
		set
		{
			ViewState["ErrorMessage"] = value;
		}
	}

	/// <summary>Gets or sets the color of the message displayed when validation fails.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the message displayed when validation fails. The default is <see cref="P:System.Drawing.Color.Red" />.</returns>
	[DefaultValue(typeof(Color), "Red")]
	public override Color ForeColor
	{
		get
		{
			return forecolor;
		}
		set
		{
			forecolor = value;
			base.ForeColor = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the associated input control passes validation.</summary>
	/// <returns>
	///     <see langword="true" /> if the associated input control passes validation; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[Browsable(false)]
	[DefaultValue(true)]
	[Themeable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public bool IsValid
	{
		get
		{
			return valid;
		}
		set
		{
			valid = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is a valid control.</summary>
	/// <returns>
	///     <see langword="true" /> if the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> is a valid control; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">No value is specified in the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is not found on the page. </exception>
	protected bool PropertiesValid
	{
		get
		{
			if (NamingContainer.FindControl(ControlToValidate) == null)
			{
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets a value that indicates whether the client's browser supports "uplevel" rendering.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports "uplevel" rendering; otherwise, <see langword="false" />.</returns>
	protected bool RenderUplevel => render_uplevel;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> class.</summary>
	protected BaseValidator()
	{
		valid = true;
		ForeColor = Color.Red;
	}

	internal bool GetRenderUplevel()
	{
		return render_uplevel;
	}

	/// <summary>Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		if (render_uplevel)
		{
			if (ID == null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
			}
			if (ControlToValidate != string.Empty)
			{
				RegisterExpandoAttribute(ClientID, "controltovalidate", GetControlRenderID(ControlToValidate));
			}
			if (ErrorMessage != string.Empty)
			{
				RegisterExpandoAttribute(ClientID, "errormessage", ErrorMessage, encode: true);
			}
			if (ValidationGroup != string.Empty)
			{
				RegisterExpandoAttribute(ClientID, "validationGroup", ValidationGroup, encode: true);
			}
			if (SetFocusOnError)
			{
				RegisterExpandoAttribute(ClientID, "focusOnError", "t");
			}
			bool isEnabled = base.IsEnabled;
			if (!isEnabled)
			{
				RegisterExpandoAttribute(ClientID, "enabled", "False");
			}
			if (isEnabled && !IsValid)
			{
				RegisterExpandoAttribute(ClientID, "isvalid", "False");
			}
			else if (Display == ValidatorDisplay.Static)
			{
				writer.AddStyleAttribute("visibility", "hidden");
			}
			else
			{
				writer.AddStyleAttribute("display", "none");
			}
			if (Display != ValidatorDisplay.Static)
			{
				RegisterExpandoAttribute(ClientID, "display", Display.ToString());
			}
		}
		base.AddAttributesToRender(writer);
	}

	internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
	{
		RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode: false);
	}

	internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
	{
		Page page = Page;
		if (page.ScriptManager != null)
		{
			page.ScriptManager.RegisterExpandoAttributeExternal(this, controlId, attributeName, attributeValue, encode);
		}
		else
		{
			page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}
	}

	/// <summary>Verifies whether the specified control is on the page and contains validation properties.</summary>
	/// <param name="name">The control to verify. </param>
	/// <param name="propertyName">Additional text to describe the source of the exception, if an exception is thrown from using this method. </param>
	/// <exception cref="T:System.Web.HttpException">The specified control is not found.- or -The specified control does not have a <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> attribute associated with it; therefore, it cannot be validated with a validation control. </exception>
	protected void CheckControlValidationProperty(string name, string propertyName)
	{
		if (GetValidationProperty(NamingContainer.FindControl(name) ?? throw new HttpException($"Unable to find control id '{name}'.")) == null)
		{
			throw new HttpException($"Unable to find ValidationProperty attribute '{propertyName}' on control '{name}'");
		}
	}

	/// <summary>Determines whether the control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is a valid control.</summary>
	/// <returns>
	///     <see langword="true" /> if the control specified by <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> is a valid control; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">No value is specified for the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property is not found on the page.- or -The input control specified by the <see cref="P:System.Web.UI.WebControls.BaseValidator.ControlToValidate" /> property does not have a <see cref="T:System.Web.UI.ValidationPropertyAttribute" /> attribute associated with it; therefore, it cannot be validated with a validation control.</exception>
	protected virtual bool ControlPropertiesValid()
	{
		if (ControlToValidate.Length == 0)
		{
			throw new HttpException($"ControlToValidate property of '{ID}' cannot be blank.");
		}
		CheckControlValidationProperty(ControlToValidate, string.Empty);
		return true;
	}

	/// <summary>Determines whether the validation control can perform client-side validation.</summary>
	/// <returns>
	///     <see langword="true" /> if the validation control can perform client-side validation; otherwise, <see langword="false" />.</returns>
	protected virtual bool DetermineRenderUplevel()
	{
		if (!EnableClientScript)
		{
			return false;
		}
		return UplevelHelper.IsUplevel(HttpCapabilitiesBase.GetUserAgentForDetection(HttpContext.Current.Request));
	}

	/// <summary>When overridden in a derived class, this method contains the code to determine whether the value in the input control is valid.</summary>
	/// <returns>
	///     <see langword="true" /> if the value in the input control is valid; otherwise, <see langword="false" />.</returns>
	protected abstract bool EvaluateIsValid();

	/// <summary>Gets the client ID of the specified control.</summary>
	/// <param name="name">The name of the control to get the client ID from. </param>
	/// <returns>The client ID of the specified control.</returns>
	protected string GetControlRenderID(string name)
	{
		return NamingContainer.FindControl(name)?.ClientID;
	}

	/// <summary>Gets the value associated with the specified input control.</summary>
	/// <param name="name">The name of the input control to get the value from. </param>
	/// <returns>The value associated with the specified input control.</returns>
	protected string GetControlValidationValue(string name)
	{
		Control control = NamingContainer.FindControl(name);
		if (control == null)
		{
			return null;
		}
		PropertyDescriptor validationProperty = GetValidationProperty(control);
		if (validationProperty == null)
		{
			return null;
		}
		object value = validationProperty.GetValue(control);
		if (value == null)
		{
			return string.Empty;
		}
		if (value is ListItem)
		{
			return ((ListItem)value).Value;
		}
		return value.ToString();
	}

	/// <summary>Determines the validation property of a control (if it exists).</summary>
	/// <param name="component">A <see cref="T:System.Object" /> that represents the control to get the validation property of. </param>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains the validation property of the control.</returns>
	public static PropertyDescriptor GetValidationProperty(object component)
	{
		PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
		foreach (Attribute attribute in TypeDescriptor.GetAttributes(component))
		{
			if (attribute is ValidationPropertyAttribute { Name: not null } validationPropertyAttribute)
			{
				return properties[validationPropertyAttribute.Name];
			}
		}
		return null;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		Page page = Page;
		if (page != null)
		{
			page.Validators.Add(this);
			page.GetValidators(ValidationGroup).Add(this);
		}
		base.OnInit(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		pre_render_called = true;
		ControlPropertiesValid();
		render_uplevel = DetermineRenderUplevel();
		if (render_uplevel)
		{
			RegisterValidatorCommonScript();
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnUnload(EventArgs e)
	{
		Page page = Page;
		if (page != null)
		{
			page.Validators.Remove(this);
			if (!string.IsNullOrEmpty(ValidationGroup))
			{
				page.GetValidators(ValidationGroup).Remove(this);
			}
		}
		base.OnUnload(e);
	}

	/// <summary>Registers code on the page for client-side validation.</summary>
	protected void RegisterValidatorCommonScript()
	{
		Page page = Page;
		if (page != null)
		{
			if (page.ScriptManager != null)
			{
				page.ScriptManager.RegisterClientScriptResourceExternal(this, typeof(BaseValidator), "WebUIValidation_2.0.js");
				page.ScriptManager.RegisterClientScriptBlockExternal(this, typeof(BaseValidator), "ValidationInitializeScript", page.ValidationInitializeScript, addScriptTags: true);
				page.ScriptManager.RegisterOnSubmitStatementExternal(this, typeof(BaseValidator), "ValidationOnSubmitStatement", page.ValidationOnSubmitStatement);
				page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), "ValidationStartupScript", page.ValidationStartupScript, addScriptTags: true);
			}
			else if (!page.ClientScript.IsClientScriptIncludeRegistered(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock"))
			{
				page.ClientScript.RegisterClientScriptInclude(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock", page.ClientScript.GetWebResourceUrl(typeof(BaseValidator), "WebUIValidation_2.0.js"));
				page.ClientScript.RegisterClientScriptBlock(typeof(BaseValidator), "Mono-System.Web-ValidationClientScriptBlock.Initialize", page.ValidationInitializeScript, addScriptTags: true);
				page.ClientScript.RegisterOnSubmitStatement(typeof(BaseValidator), "Mono-System.Web-ValidationOnSubmitStatement", page.ValidationOnSubmitStatement);
				page.ClientScript.RegisterStartupScript(typeof(BaseValidator), "Mono-System.Web-ValidationStartupScript", page.ValidationStartupScript, addScriptTags: true);
			}
		}
	}

	/// <summary>Registers an ECMAScript array declaration using the array name <see langword="Page_Validators" />.</summary>
	protected virtual void RegisterValidatorDeclaration()
	{
		Page page = Page;
		if (page != null)
		{
			if (page.ScriptManager != null)
			{
				page.ScriptManager.RegisterArrayDeclarationExternal(this, "Page_Validators", "document.getElementById ('" + ClientID + "')");
				page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), ClientID + "DisposeScript", "\ndocument.getElementById('" + ClientID + "').dispose = function() {\n    Array.remove(Page_Validators, document.getElementById('" + ClientID + "'));\n}\n", addScriptTags: true);
			}
			else
			{
				page.ClientScript.RegisterArrayDeclaration("Page_Validators", "document.getElementById ('" + ClientID + "')");
			}
		}
	}

	/// <summary>Displays the control on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (!base.IsEnabled && !EnableClientScript)
		{
			return;
		}
		if (render_uplevel)
		{
			RegisterValidatorDeclaration();
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool isValid = IsValid;
		if (!pre_render_called)
		{
			flag = true;
			flag2 = true;
		}
		else if (render_uplevel)
		{
			flag = true;
			flag2 = Display != ValidatorDisplay.None;
		}
		else if (Display != 0)
		{
			flag = !isValid;
			flag2 = !isValid;
			flag3 = isValid && Display == ValidatorDisplay.Static;
		}
		if (flag)
		{
			AddAttributesToRender(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}
		if (flag2 || flag3)
		{
			string value;
			if (flag2)
			{
				value = Text;
				if (string.IsNullOrEmpty(value))
				{
					value = ErrorMessage;
				}
			}
			else
			{
				value = "&nbsp;";
			}
			writer.Write(value);
		}
		if (flag)
		{
			writer.RenderEndTag();
		}
	}

	/// <summary>Performs validation on the associated input control and updates the <see cref="P:System.Web.UI.WebControls.BaseValidator.IsValid" /> property.</summary>
	public void Validate()
	{
		if (base.IsEnabled && Visible)
		{
			IsValid = ControlPropertiesValid() && EvaluateIsValid();
		}
		else
		{
			IsValid = true;
		}
	}
}
