using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the base class that defines the methods, properties and events common to all controls in the <see cref="N:System.Web.UI.WebControls" /> namespace.</summary>
[ParseChildren(true)]
[PersistChildren(false, false)]
[Themeable(true)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class WebControl : Control, IAttributeAccessor
{
	private const string DEFAULT_DISABLED_CSS_CLASS = "aspNetDisabled";

	private Style style;

	private HtmlTextWriterTag tag;

	private string tag_name;

	private AttributeCollection attributes;

	private StateBag attribute_state;

	private bool enabled;

	private bool track_enabled_state;

	private static char[] _script_trim_chars;

	/// <summary>Gets or sets the access key that allows you to quickly navigate to the Web server control.</summary>
	/// <returns>The access key for quick navigation to the Web server control. The default value is <see cref="F:System.String.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither <see langword="null" />, <see cref="F:System.String.Empty" /> nor a single character string. </exception>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string AccessKey
	{
		get
		{
			return ViewState.GetString("AccessKey", string.Empty);
		}
		set
		{
			if (value == null || value.Length < 2)
			{
				ViewState["AccessKey"] = value;
				return;
			}
			throw new ArgumentException("AccessKey can only be null, empty or a single character", "value");
		}
	}

	/// <summary>Gets the collection of arbitrary attributes (for rendering only) that do not correspond to properties on the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.AttributeCollection" /> of name and value pairs.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public AttributeCollection Attributes
	{
		get
		{
			if (attributes == null)
			{
				attribute_state = new StateBag(ignoreCase: true);
				if (base.IsTrackingViewState)
				{
					attribute_state.TrackViewState();
				}
				attributes = new AttributeCollection(attribute_state);
			}
			return attributes;
		}
	}

	/// <summary>Gets or sets the background color of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue(typeof(Color), "")]
	[TypeConverter(typeof(WebColorConverter))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual Color BackColor
	{
		get
		{
			if (style == null)
			{
				return Color.Empty;
			}
			return style.BackColor;
		}
		set
		{
			ControlStyle.BackColor = value;
		}
	}

	/// <summary>Gets or sets the border color of the Web control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue(typeof(Color), "")]
	[TypeConverter(typeof(WebColorConverter))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual Color BorderColor
	{
		get
		{
			if (style == null)
			{
				return Color.Empty;
			}
			return style.BorderColor;
		}
		set
		{
			ControlStyle.BorderColor = value;
		}
	}

	/// <summary>Gets or sets the border style of the Web server control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	[DefaultValue(BorderStyle.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual BorderStyle BorderStyle
	{
		get
		{
			if (style == null)
			{
				return BorderStyle.NotSet;
			}
			return style.BorderStyle;
		}
		set
		{
			if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ControlStyle.BorderStyle = value;
		}
	}

	/// <summary>Gets or sets the border width of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentException">The specified border width is a negative value. </exception>
	[DefaultValue(typeof(Unit), "")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual Unit BorderWidth
	{
		get
		{
			if (style == null)
			{
				return Unit.Empty;
			}
			return style.BorderWidth;
		}
		set
		{
			ControlStyle.BorderWidth = value;
		}
	}

	/// <summary>Gets the style of the Web server control. This property is used primarily by control developers.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that encapsulates the appearance properties of the Web server control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Style ControlStyle
	{
		get
		{
			if (style == null)
			{
				style = CreateControlStyle();
				if (base.IsTrackingViewState)
				{
					style.TrackViewState();
				}
			}
			return style;
		}
	}

	/// <summary>Gets a value indicating whether a <see cref="T:System.Web.UI.WebControls.Style" /> object has been created for the <see cref="P:System.Web.UI.WebControls.WebControl.ControlStyle" /> property. This property is primarily used by control developers.</summary>
	/// <returns>
	///     <see langword="true" /> if a <see cref="T:System.Web.UI.WebControls.Style" /> object has been created for the <see cref="P:System.Web.UI.WebControls.WebControl.ControlStyle" /> property; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ControlStyleCreated => style != null;

	/// <summary>Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.</summary>
	/// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
	[CssClassProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string CssClass
	{
		get
		{
			if (style == null)
			{
				return string.Empty;
			}
			return style.CssClass;
		}
		set
		{
			ControlStyle.CssClass = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the Web server control is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if control is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[Bindable(true)]
	[DefaultValue(true)]
	[Themeable(false)]
	public virtual bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			if (enabled != value)
			{
				if (base.IsTrackingViewState)
				{
					track_enabled_state = true;
				}
				enabled = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[Browsable(true)]
	public new virtual bool EnableTheming
	{
		get
		{
			return base.EnableTheming;
		}
		set
		{
			base.EnableTheming = value;
		}
	}

	/// <summary>Gets the font properties associated with the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual FontInfo Font => ControlStyle.Font;

	/// <summary>Gets or sets the foreground color (typically the color of the text) of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	[DefaultValue(typeof(Color), "")]
	[TypeConverter(typeof(WebColorConverter))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual Color ForeColor
	{
		get
		{
			if (style == null)
			{
				return Color.Empty;
			}
			return style.ForeColor;
		}
		set
		{
			ControlStyle.ForeColor = value;
		}
	}

	/// <summary>Gets a value indicating whether the control has attributes set.</summary>
	/// <returns>
	///     <see langword="true" /> if the control has attributes set; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool HasAttributes
	{
		get
		{
			if (attributes != null)
			{
				return attributes.Count > 0;
			}
			return false;
		}
	}

	/// <summary>Gets or sets the height of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
	/// <exception cref="T:System.ArgumentException">The height was set to a negative value.</exception>
	[DefaultValue(typeof(Unit), "")]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual Unit Height
	{
		get
		{
			if (style == null)
			{
				return Unit.Empty;
			}
			return style.Height;
		}
		set
		{
			ControlStyle.Height = value;
		}
	}

	/// <summary>Gets or sets the skin to apply to the control.</summary>
	/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.ArgumentException">The skin specified in the <see cref="P:System.Web.UI.WebControls.WebControl.SkinID" /> property does not exist in the theme.</exception>
	[Browsable(true)]
	public override string SkinID
	{
		get
		{
			return base.SkinID;
		}
		set
		{
			base.SkinID = value;
		}
	}

	/// <summary>Gets a collection of text attributes that will be rendered as a style attribute on the outer tag of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> that contains the HTML style attributes to render on the outer tag of the Web server control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public CssStyleCollection Style => Attributes.CssStyle;

	/// <summary>Gets or sets the tab index of the Web server control.</summary>
	/// <returns>The tab index of the Web server control. The default is <see langword="0" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual short TabIndex
	{
		get
		{
			return ViewState.GetShort("TabIndex", 0);
		}
		set
		{
			ViewState["TabIndex"] = value;
		}
	}

	/// <summary>Gets or sets the text displayed when the mouse pointer hovers over the Web server control.</summary>
	/// <returns>The text displayed when the mouse pointer hovers over the Web server control. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string ToolTip
	{
		get
		{
			return ViewState.GetString("ToolTip", string.Empty);
		}
		set
		{
			ViewState["ToolTip"] = value;
		}
	}

	/// <summary>Gets or sets the width of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />.</returns>
	/// <exception cref="T:System.ArgumentException">The width of the Web server control was set to a negative value. </exception>
	[DefaultValue(typeof(Unit), "")]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual Unit Width
	{
		get
		{
			if (style == null)
			{
				return Unit.Empty;
			}
			return style.Width;
		}
		set
		{
			ControlStyle.Width = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected virtual HtmlTextWriterTag TagKey => tag;

	/// <summary>Gets the name of the control tag. This property is used primarily by control developers.</summary>
	/// <returns>The name of the control tag.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected virtual string TagName
	{
		get
		{
			if (tag_name == null)
			{
				tag_name = HtmlTextWriter.StaticGetTagName(TagKey);
			}
			return tag_name;
		}
	}

	/// <summary>Gets a value indicating whether the control is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.WebControl" /> object is enabled; otherwise, <see langword="false" />.</returns>
	protected internal bool IsEnabled
	{
		get
		{
			for (WebControl webControl = this; webControl != null; webControl = webControl.Parent as WebControl)
			{
				if (!webControl.Enabled)
				{
					return false;
				}
			}
			return true;
		}
	}

	/// <summary>Gets or sets the CSS class to apply to the rendered HTML element when the control is disabled.</summary>
	/// <returns>The CSS class that should be applied to the rendered HTML element when the control is disabled. The default value is "aspNetDisabled".</returns>
	public static string DisabledCssClass { get; set; }

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>Always <see langword="true" />.</returns>
	[Browsable(false)]
	public virtual bool SupportsDisabledAttribute => true;

	static WebControl()
	{
		_script_trim_chars = new char[1] { ';' };
		DisabledCssClass = "aspNetDisabled";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class using the specified HTML tag.</summary>
	/// <param name="tag">One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> values. </param>
	public WebControl(HtmlTextWriterTag tag)
	{
		this.tag = tag;
		enabled = true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class that represents a <see langword="Span" /> HTML tag.</summary>
	protected WebControl()
		: this(HtmlTextWriterTag.Span)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebControl" /> class using the specified HTML tag.</summary>
	/// <param name="tag">An HTML tag. </param>
	protected WebControl(string tag)
	{
		this.tag = HtmlTextWriterTag.Unknown;
		tag_name = tag;
		enabled = true;
	}

	/// <summary>Copies any nonblank elements of the specified style to the Web control, overwriting any existing style elements of the control. This method is primarily used by control developers.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to be copied. </param>
	public void ApplyStyle(Style s)
	{
		if (s != null && !s.IsEmpty)
		{
			ControlStyle.CopyFrom(s);
		}
	}

	/// <summary>Copies the properties not encapsulated by the <see cref="P:System.Web.UI.WebControls.WebControl.Style" /> object from the specified Web server control to the Web server control that this method is called from. This method is used primarily by control developers.</summary>
	/// <param name="controlSrc">A <see cref="T:System.Web.UI.WebControls.WebControl" /> that represents the source control with properties to be copied to the Web server control that this method is called from. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="controlSrc" /> is <see langword="null" />.</exception>
	public void CopyBaseAttributes(WebControl controlSrc)
	{
		if (controlSrc == null)
		{
			return;
		}
		Enabled = controlSrc.Enabled;
		object obj = controlSrc.ViewState["AccessKey"];
		if (obj != null)
		{
			ViewState["AccessKey"] = obj;
		}
		obj = controlSrc.ViewState["TabIndex"];
		if (obj != null)
		{
			ViewState["TabIndex"] = obj;
		}
		obj = controlSrc.ViewState["ToolTip"];
		if (obj != null)
		{
			ViewState["ToolTip"] = obj;
		}
		if (controlSrc.attributes == null)
		{
			return;
		}
		AttributeCollection attributeCollection = Attributes;
		foreach (string key in controlSrc.attributes.Keys)
		{
			attributeCollection[key] = controlSrc.attributes[key];
		}
	}

	/// <summary>Copies any nonblank elements of the specified style to the Web control, but will not overwrite any existing style elements of the control. This method is used primarily by control developers.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to be copied. </param>
	public void MergeStyle(Style s)
	{
		if (s != null && !s.IsEmpty)
		{
			ControlStyle.MergeWith(s);
		}
	}

	/// <summary>Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	public virtual void RenderBeginTag(HtmlTextWriter writer)
	{
		AddAttributesToRender(writer);
		if (TagKey == HtmlTextWriterTag.Unknown)
		{
			writer.RenderBeginTag(TagName);
		}
		else
		{
			writer.RenderBeginTag(TagKey);
		}
	}

	/// <summary>Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	public virtual void RenderEndTag(HtmlTextWriter writer)
	{
		writer.RenderEndTag();
	}

	internal string BuildScriptAttribute(string name, string tail)
	{
		AttributeCollection attributeCollection = Attributes;
		string text = attributeCollection[name];
		if (text == null || text.Length == 0)
		{
			return tail;
		}
		if (text[text.Length - 1] == ';')
		{
			text = text.TrimEnd(_script_trim_chars);
		}
		text = text + ";" + tail;
		attributeCollection.Remove(name);
		return text;
	}

	internal void AddDisplayStyleAttribute(HtmlTextWriter writer)
	{
		if (ControlStyleCreated && (!ControlStyle.BorderWidth.IsEmpty || (ControlStyle.BorderStyle != BorderStyle.None && ControlStyle.BorderStyle != 0) || !ControlStyle.Height.IsEmpty || !ControlStyle.Width.IsEmpty))
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
		}
	}

	private void RenderDisabled(HtmlTextWriter writer)
	{
		if (!IsEnabled)
		{
			if (!SupportsDisabledAttribute)
			{
				ControlStyle.PrependCssClass(DisabledCssClass);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", fEncode: false);
			}
		}
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" />. This method is used primarily by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	protected virtual void AddAttributesToRender(HtmlTextWriter writer)
	{
		RenderDisabled(writer);
		if (ID != null)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
		}
		if (AccessKey != string.Empty)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
		}
		if (ToolTip != string.Empty)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Title, ToolTip);
		}
		if (TabIndex != 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString());
		}
		if (style != null && !style.IsEmpty)
		{
			if (TagKey == HtmlTextWriterTag.Span)
			{
				AddDisplayStyleAttribute(writer);
			}
			style.AddAttributesToRender(writer, this);
		}
		if (attributes == null)
		{
			return;
		}
		foreach (string key in attributes.Keys)
		{
			writer.AddAttribute(key, attributes[key]);
		}
	}

	/// <summary>Creates the style object that is used internally by the <see cref="T:System.Web.UI.WebControls.WebControl" /> class to implement all style related properties. This method is used primarily by control developers.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that is used to implement all style-related properties of the control.</returns>
	protected virtual Style CreateControlStyle()
	{
		return new Style(ViewState);
	}

	/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method. </summary>
	/// <param name="savedState">An object that represents the control state to restore. </param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null || !(savedState is Pair))
		{
			base.LoadViewState(null);
			return;
		}
		Pair pair = (Pair)savedState;
		base.LoadViewState(pair.First);
		if (ViewState["_!SB"] != null)
		{
			ControlStyle.LoadBitState();
		}
		if (pair.Second != null)
		{
			if (attribute_state == null)
			{
				attribute_state = new StateBag();
				if (base.IsTrackingViewState)
				{
					attribute_state.TrackViewState();
				}
			}
			attribute_state.LoadViewState(pair.Second);
			attributes = new AttributeCollection(attribute_state);
		}
		enabled = ViewState.GetBool("Enabled", enabled);
	}

	internal virtual string InlinePropertiesSet()
	{
		List<string> list = new List<string>();
		if (BackColor != Color.Empty)
		{
			list.Add("BackColor");
		}
		if (BorderColor != Color.Empty)
		{
			list.Add("BorderColor");
		}
		if (BorderStyle != 0)
		{
			list.Add("BorderStyle");
		}
		if (BorderWidth != Unit.Empty)
		{
			list.Add("BorderWidth");
		}
		if (CssClass != string.Empty)
		{
			list.Add("CssClass");
		}
		if (ForeColor != Color.Empty)
		{
			list.Add("ForeColor");
		}
		if (Height != Unit.Empty)
		{
			list.Add("Height");
		}
		if (Width != Unit.Empty)
		{
			list.Add("Width");
		}
		if (list.Count == 0)
		{
			return null;
		}
		return string.Join(", ", list);
	}

	internal void VerifyInlinePropertiesNotSet()
	{
		if (this is IRenderOuterTable { RenderOuterTable: false })
		{
			string text = InlinePropertiesSet();
			if (!string.IsNullOrEmpty(text))
			{
				bool flag = text.IndexOf(',') > -1;
				throw new InvalidOperationException(string.Format("The style propert{0} '{1}' cannot be used while RenderOuterTable is disabled on the {2} control with ID '{3}'", flag ? "ies" : "y", text, GetType().Name, ID));
			}
		}
	}

	/// <summary>Renders the control to the specified HTML writer.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (base.Adapter != null)
		{
			base.Adapter.Render(writer);
			return;
		}
		RenderBeginTag(writer);
		RenderContents(writer);
		RenderEndTag(writer);
	}

	/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	protected internal virtual void RenderContents(HtmlTextWriter writer)
	{
		base.Render(writer);
	}

	/// <summary>Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
	/// <returns>An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		if (track_enabled_state)
		{
			ViewState["Enabled"] = enabled;
		}
		object obj = null;
		if (style != null)
		{
			style.SaveBitState();
		}
		object obj2 = base.SaveViewState();
		if (attribute_state != null)
		{
			obj = attribute_state.SaveViewState();
		}
		if (obj2 == null && obj == null)
		{
			return null;
		}
		return new Pair(obj2, obj);
	}

	/// <summary>Causes the control to track changes to its view state so they can be stored in the object's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		if (style != null)
		{
			style.TrackViewState();
		}
		if (attribute_state != null)
		{
			attribute_state.TrackViewState();
			attribute_state.SetDirty(dirty: true);
		}
		base.TrackViewState();
	}

	/// <summary>Gets an attribute of the Web control with the specified name.</summary>
	/// <param name="name">The name of the attribute.</param>
	/// <returns>The value of the attribute.</returns>
	string IAttributeAccessor.GetAttribute(string key)
	{
		if (attributes != null)
		{
			return attributes[key];
		}
		return null;
	}

	/// <summary>Sets an attribute of the Web control to the specified name and value.</summary>
	/// <param name="name">The name component of the attribute's name/value pair.</param>
	/// <param name="value">The value component of the attribute's name/value pair.</param>
	void IAttributeAccessor.SetAttribute(string key, string value)
	{
		Attributes[key] = value;
	}
}
