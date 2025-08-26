using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Displays a check box that allows the user to select a <see langword="true" /> or <see langword="false" /> condition.</summary>
[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultEvent("CheckedChanged")]
[DefaultProperty("Text")]
[ControlValueProperty("Checked", null)]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CheckBox : WebControl, IPostBackDataHandler, ICheckBoxControl
{
	private string render_type;

	private AttributeCollection common_attrs;

	private AttributeCollection inputAttributes;

	private StateBag inputAttributesState;

	private AttributeCollection labelAttributes;

	private StateBag labelAttributesState;

	private static readonly object EventCheckedChanged = new object();

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBox" /> state automatically posts back to the server when clicked.</summary>
	/// <returns>
	///     <see langword="true" /> to automatically post the state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control to the server when it is clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
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

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.WebControls.CheckBox" /> is clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control is checked.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate a checked state; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[Bindable(true, BindingDirection.TwoWay)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool Checked
	{
		get
		{
			return ViewState.GetBool("Checked", def: false);
		}
		set
		{
			ViewState["Checked"] = value;
		}
	}

	/// <summary>Gets a reference to the collection of attributes for the rendered input element of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
	/// <returns>The collection of attribute names and values that are added to the rendered INPUT element for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control. The default is an empty <see cref="T:System.Web.UI.AttributeCollection" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public AttributeCollection InputAttributes
	{
		get
		{
			if (inputAttributes == null)
			{
				if (inputAttributesState == null)
				{
					inputAttributesState = new StateBag(ignoreCase: true);
					if (base.IsTrackingViewState)
					{
						inputAttributesState.TrackViewState();
					}
				}
				inputAttributes = new AttributeCollection(inputAttributesState);
			}
			return inputAttributes;
		}
	}

	/// <summary>Gets a reference to the collection of attributes for the rendered LABEL element of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
	/// <returns>The collection of attribute names and values that are added to the rendered LABEL element for the <see cref="T:System.Web.UI.WebControls.CheckBox" />. The default is an empty <see cref="T:System.Web.UI.AttributeCollection" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public AttributeCollection LabelAttributes
	{
		get
		{
			if (labelAttributes == null)
			{
				if (labelAttributesState == null)
				{
					labelAttributesState = new StateBag(ignoreCase: true);
					if (base.IsTrackingViewState)
					{
						labelAttributesState.TrackViewState();
					}
				}
				labelAttributes = new AttributeCollection(labelAttributesState);
			}
			return labelAttributes;
		}
	}

	/// <summary>Gets or sets the text label associated with the <see cref="T:System.Web.UI.WebControls.CheckBox" />.</summary>
	/// <returns>The text label associated with the <see langword="CheckBox" />. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Bindable(true)]
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
		}
	}

	/// <summary>Gets or sets the alignment of the text label associated with the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default value is <see langword="Right" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
	[DefaultValue(TextAlign.Right)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual TextAlign TextAlign
	{
		get
		{
			return (TextAlign)ViewState.GetInt("TextAlign", 2);
		}
		set
		{
			if (value != TextAlign.Left && value != TextAlign.Right)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["TextAlign"] = value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control causes validation when it posts back to the server. </summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.CheckBox" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
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

	internal virtual string NameAttribute => UniqueID;

	/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.CheckBox.Checked" /> property changes between posts to the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler CheckedChanged
	{
		add
		{
			base.Events.AddHandler(EventCheckedChanged, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventCheckedChanged, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> class.</summary>
	public CheckBox()
		: base(HtmlTextWriterTag.Input)
	{
		render_type = "checkbox";
	}

	internal CheckBox(string render_type)
		: base(HtmlTextWriterTag.Input)
	{
		this.render_type = render_type;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.CheckBox.CheckedChanged" /> event of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control. This allows you to handle the event directly.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnCheckedChanged(EventArgs e)
	{
		((EventHandler)base.Events[EventCheckedChanged])?.Invoke(this, e);
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
	/// <param name="savedState">An object that contains the saved view state values for the control. </param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState((object)null);
			return;
		}
		Triplet triplet = (Triplet)savedState;
		base.LoadViewState(triplet.First);
		if (triplet.Second != null)
		{
			if (inputAttributesState == null)
			{
				inputAttributesState = new StateBag(ignoreCase: true);
				inputAttributesState.TrackViewState();
			}
			inputAttributesState.LoadViewState(triplet.Second);
		}
		if (triplet.Third != null)
		{
			if (labelAttributesState == null)
			{
				labelAttributesState = new StateBag(ignoreCase: true);
				labelAttributesState.TrackViewState();
			}
			labelAttributesState.LoadViewState(triplet.Third);
		}
	}

	/// <summary>Saves the changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> view state since the time the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> view state; otherwise, if no view state is associated with the object, <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		object obj = base.SaveViewState();
		object obj2 = null;
		object obj3 = null;
		if (inputAttributesState != null)
		{
			obj2 = inputAttributesState.SaveViewState();
		}
		if (labelAttributesState != null)
		{
			obj3 = labelAttributesState.SaveViewState();
		}
		if (obj == null && obj2 == null && obj3 == null)
		{
			return null;
		}
		return new Triplet(obj, obj2, obj3);
	}

	/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control so that they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property. </summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (inputAttributesState != null)
		{
			inputAttributesState.TrackViewState();
		}
		if (labelAttributesState != null)
		{
			labelAttributesState.TrackViewState();
		}
	}

	/// <summary>Registers client script for generating postback prior to rendering on the client if <see cref="P:System.Web.UI.WebControls.CheckBox.AutoPostBack" /> is <see langword="true" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null && base.IsEnabled)
		{
			page.RegisterRequiresPostBack(this);
			page.RegisterEnabledControl(this);
		}
	}

	private static bool IsInputOrCommonAttr(string attname)
	{
		attname = attname.ToUpper(Helpers.InvariantCulture);
		switch (attname)
		{
		case "VALUE":
		case "CHECKED":
		case "SIZE":
		case "MAXLENGTH":
		case "SRC":
		case "ALT":
		case "USEMAP":
		case "DISABLED":
		case "READONLY":
		case "ACCEPT":
		case "ACCESSKEY":
		case "TABINDEX":
		case "ONFOCUS":
		case "ONBLUR":
		case "ONSELECT":
		case "ONCHANGE":
		case "ONCLICK":
		case "ONDBLCLICK":
		case "ONMOUSEDOWN":
		case "ONMOUSEUP":
		case "ONMOUSEOVER":
		case "ONMOUSEMOVE":
		case "ONMOUSEOUT":
		case "ONKEYPRESS":
		case "ONKEYDOWN":
		case "ONKEYUP":
			return true;
		default:
			return false;
		}
	}

	private bool AddAttributesForSpan(HtmlTextWriter writer)
	{
		if (base.HasAttributes)
		{
			AttributeCollection attributeCollection = base.Attributes;
			ICollection keys = attributeCollection.Keys;
			string[] array = new string[keys.Count];
			keys.CopyTo(array, 0);
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (IsInputOrCommonAttr(text))
				{
					if (common_attrs == null)
					{
						common_attrs = new AttributeCollection(new StateBag());
					}
					common_attrs[text] = base.Attributes[text];
					attributeCollection.Remove(text);
				}
			}
			if (attributeCollection.Count > 0)
			{
				attributeCollection.AddAttributes(writer);
				return true;
			}
		}
		return false;
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.CheckBox" /> on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		Page page = Page;
		if (page != null)
		{
			page.VerifyRenderingInServerForm(this);
			page.ClientScript.RegisterForEventValidation(UniqueID);
		}
		bool flag = base.ControlStyleCreated && !base.ControlStyle.IsEmpty;
		bool isEnabled = base.IsEnabled;
		if (!isEnabled)
		{
			if (!base.RenderingCompatibilityLessThan40)
			{
				base.ControlStyle.PrependCssClass(WebControl.DisabledCssClass);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", fEncode: false);
			}
			flag = true;
		}
		if (flag)
		{
			AddDisplayStyleAttribute(writer);
			base.ControlStyle.AddAttributesToRender(writer, this);
		}
		string toolTip = ToolTip;
		if (toolTip != null && toolTip.Length > 0)
		{
			writer.AddAttribute("title", toolTip);
			flag = true;
		}
		if (base.HasAttributes && AddAttributesForSpan(writer))
		{
			flag = true;
		}
		if (flag)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
		}
		if (TextAlign == TextAlign.Right)
		{
			RenderInput(writer, isEnabled);
			RenderLabel(writer);
		}
		else
		{
			RenderLabel(writer);
			RenderInput(writer, isEnabled);
		}
		if (flag)
		{
			writer.RenderEndTag();
		}
	}

	private void RenderInput(HtmlTextWriter w, bool enabled)
	{
		if (ClientID != null && ClientID.Length > 0)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
		}
		w.AddAttribute(HtmlTextWriterAttribute.Type, render_type);
		string nameAttribute = NameAttribute;
		if (nameAttribute != null && nameAttribute.Length > 0)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Name, nameAttribute);
		}
		InternalAddAttributesToRender(w, enabled);
		AddAttributesToRender(w);
		if (Checked)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Checked, "checked", fEncode: false);
		}
		if (AutoPostBack)
		{
			Page page = Page;
			string text = ((page != null) ? page.ClientScript.GetPostBackEventReference(GetPostBackOptions(), registerForEventValidation: true) : string.Empty);
			text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
			if (common_attrs != null && common_attrs["onclick"] != null)
			{
				text = ClientScriptManager.EnsureEndsWithSemicolon(common_attrs["onclick"]) + text;
				common_attrs.Remove("onclick");
			}
			w.AddAttribute(HtmlTextWriterAttribute.Onclick, text);
		}
		if (AccessKey.Length > 0)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
		}
		if (TabIndex != 0)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString(NumberFormatInfo.InvariantInfo));
		}
		if (common_attrs != null)
		{
			common_attrs.AddAttributes(w);
		}
		if (inputAttributes != null)
		{
			inputAttributes.AddAttributes(w);
		}
		w.RenderBeginTag(HtmlTextWriterTag.Input);
		w.RenderEndTag();
	}

	private void RenderLabel(HtmlTextWriter w)
	{
		string text = Text;
		if (text.Length > 0)
		{
			if (labelAttributes != null)
			{
				labelAttributes.AddAttributes(w);
			}
			w.AddAttribute(HtmlTextWriterAttribute.For, ClientID);
			w.RenderBeginTag(HtmlTextWriterTag.Label);
			w.Write(text);
			w.RenderEndTag();
		}
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
	/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
	/// <param name="postCollection">The collection posted to the server.</param>
	/// <returns>
	///     <see langword="true" /> if the posted content is different from the last posting; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		if (!base.IsEnabled)
		{
			return false;
		}
		string text = postCollection[postDataKey];
		bool flag = text != null && text.Length > 0;
		if (Checked != flag)
		{
			Checked = flag;
			return true;
		}
		return false;
	}

	/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.CheckBox.OnCheckedChanged(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		if (CausesValidation)
		{
			Page?.Validate(ValidationGroup);
		}
		OnCheckedChanged(EventArgs.Empty);
	}

	/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.</summary>
	/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information. </param>
	/// <returns>
	///     <see langword="true" /> if the state of the <see cref="T:System.Web.UI.WebControls.CheckBox" /> has changed; otherwise <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Raises when posted data for a control has changed.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
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

	/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.CheckBox" /> control to be rendered to the specified output stream.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
	}

	internal virtual void InternalAddAttributesToRender(HtmlTextWriter w, bool enabled)
	{
		if (!enabled)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", fEncode: false);
		}
	}
}
