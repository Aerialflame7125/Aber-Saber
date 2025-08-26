using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style of a Web server control.</summary>
[ToolboxItem("")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Style : Component, IStateManager
{
	[Flags]
	internal enum Styles
	{
		BackColor = 8,
		BorderColor = 0x10,
		BorderStyle = 0x40,
		BorderWidth = 0x20,
		CssClass = 2,
		Font = 1,
		ForeColor = 4,
		Height = 0x80,
		Width = 0x100,
		FontAll = 0xFE00,
		FontBold = 0x800,
		FontItalic = 0x1000,
		FontNames = 0x200,
		FontOverline = 0x4000,
		FontSize = 0x400,
		FontStrikeout = 0x8000,
		FontUnderline = 0x2000
	}

	internal const string BitStateKey = "_!SB";

	private int styles;

	private int stylesTraked;

	internal StateBag viewstate;

	private FontInfo fontinfo;

	private bool tracking;

	private bool _isSharedViewState;

	private string registered_class;

	/// <summary>Gets or sets the background color of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue(typeof(Color), "")]
	[NotifyParentProperty(true)]
	[TypeConverter(typeof(WebColorConverter))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Color BackColor
	{
		get
		{
			if (!CheckBit(8))
			{
				return Color.Empty;
			}
			return (Color)viewstate["BackColor"];
		}
		set
		{
			viewstate["BackColor"] = value;
			SetBit(8);
		}
	}

	/// <summary>Gets or sets the border color of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />, which indicates that this property is not set.</returns>
	[DefaultValue(typeof(Color), "")]
	[NotifyParentProperty(true)]
	[TypeConverter(typeof(WebColorConverter))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Color BorderColor
	{
		get
		{
			if (!CheckBit(16))
			{
				return Color.Empty;
			}
			return (Color)viewstate["BorderColor"];
		}
		set
		{
			viewstate["BorderColor"] = value;
			SetBit(16);
		}
	}

	/// <summary>Gets or sets the border style of the Web server control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> enumeration values.</exception>
	[DefaultValue(BorderStyle.NotSet)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public BorderStyle BorderStyle
	{
		get
		{
			if (!CheckBit(64))
			{
				return BorderStyle.NotSet;
			}
			return (BorderStyle)viewstate["BorderStyle"];
		}
		set
		{
			if (value < BorderStyle.NotSet || value > BorderStyle.Outset)
			{
				throw new ArgumentOutOfRangeException("value", "The selected value is not one of the BorderStyle enumeration values.");
			}
			viewstate["BorderStyle"] = value;
			SetBit(64);
		}
	}

	/// <summary>Gets or sets the border width of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width of a Web server control. The default value is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified border width is a negative value. </exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Unit BorderWidth
	{
		get
		{
			if (!CheckBit(32))
			{
				return Unit.Empty;
			}
			return (Unit)viewstate["BorderWidth"];
		}
		set
		{
			if (value.Value < 0.0)
			{
				throw new ArgumentOutOfRangeException("Value", value.Value, "BorderWidth must not be negative");
			}
			viewstate["BorderWidth"] = value;
			SetBit(32);
		}
	}

	/// <summary>Gets or sets the cascading style sheet (CSS) class rendered by the Web server control on the client.</summary>
	/// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
	[CssClassProperty]
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string CssClass
	{
		get
		{
			if (!CheckBit(2))
			{
				return string.Empty;
			}
			if (!(viewstate["CssClass"] is string result))
			{
				return string.Empty;
			}
			return result;
		}
		set
		{
			viewstate["CssClass"] = value;
			SetBit(2);
		}
	}

	/// <summary>Gets the font properties associated with the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that represents the font properties of the Web server control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public FontInfo Font
	{
		get
		{
			if (fontinfo == null)
			{
				fontinfo = new FontInfo(this);
			}
			return fontinfo;
		}
	}

	/// <summary>Gets or sets the foreground color (typically the color of the text) of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	[DefaultValue(typeof(Color), "")]
	[NotifyParentProperty(true)]
	[TypeConverter(typeof(WebColorConverter))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Color ForeColor
	{
		get
		{
			if (!CheckBit(4))
			{
				return Color.Empty;
			}
			return (Color)viewstate["ForeColor"];
		}
		set
		{
			viewstate["ForeColor"] = value;
			SetBit(4);
		}
	}

	/// <summary>Gets or sets the height of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the height of the Web server control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Unit.Value" /> property of the <see cref="T:System.Web.UI.WebControls.Unit" /> is negative. </exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Unit Height
	{
		get
		{
			if (!CheckBit(128))
			{
				return Unit.Empty;
			}
			return (Unit)viewstate["Height"];
		}
		set
		{
			if (value.Value < 0.0)
			{
				throw new ArgumentOutOfRangeException("Value", value.Value, "Height must not be negative");
			}
			viewstate["Height"] = value;
			SetBit(128);
		}
	}

	/// <summary>Gets or sets the width of the Web server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the width of the Web server control. The default is <see cref="F:System.Web.UI.WebControls.Unit.Empty" />, which indicates that this property is not set.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.Unit.Value" /> property of the <see cref="T:System.Web.UI.WebControls.Unit" /> is negative. </exception>
	[DefaultValue(typeof(Unit), "")]
	[NotifyParentProperty(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public Unit Width
	{
		get
		{
			if (!CheckBit(256))
			{
				return Unit.Empty;
			}
			return (Unit)viewstate["Width"];
		}
		set
		{
			if (value.Value < 0.0)
			{
				throw new ArgumentOutOfRangeException("Value", value.Value, "Width must not be negative");
			}
			viewstate["Width"] = value;
			SetBit(256);
		}
	}

	/// <summary>A protected property. Gets a value indicating whether any style elements have been defined in the state bag.</summary>
	/// <returns>
	///     <see langword="true" /> if the state bag has no style elements defined; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool IsEmpty
	{
		get
		{
			if (styles == 0)
			{
				return RegisteredCssClass.Length == 0;
			}
			return false;
		}
	}

	/// <summary>Returns a value indicating whether any style elements have been defined in the state bag.</summary>
	/// <returns>
	///     <see langword="true" /> if there are style elements defined in the state bag; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => tracking;

	/// <summary>Gets the state bag that holds the style elements.</summary>
	/// <returns>A state bag that holds the style elements defined in it.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected internal StateBag ViewState => viewstate;

	internal bool AlwaysRenderTextDecoration
	{
		get
		{
			if (viewstate["AlwaysRenderTextDecoration"] == null)
			{
				return false;
			}
			return (bool)viewstate["AlwaysRenderTextDecoration"];
		}
		set
		{
			viewstate["AlwaysRenderTextDecoration"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether a server control is tracking its view state changes.</summary>
	/// <returns>
	///     <see langword="true" /> if there are style elements defined in the view state bag; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Gets the cascading style sheet (CSS) class that is registered with the control.</summary>
	/// <returns>The CSS class name with which the current instance was registered on the page.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string RegisteredCssClass
	{
		get
		{
			if (registered_class == null)
			{
				registered_class = string.Empty;
			}
			return registered_class;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class using default values.</summary>
	public Style()
	{
		viewstate = new StateBag();
		GC.SuppressFinalize(this);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class with the specified state bag information.</summary>
	/// <param name="bag">A <see cref="T:System.Web.UI.StateBag" /> that represents the state bag in which to store style information. </param>
	public Style(StateBag bag)
	{
		viewstate = bag;
		if (viewstate == null)
		{
			viewstate = new StateBag();
		}
		_isSharedViewState = true;
		GC.SuppressFinalize(this);
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />. This method is primarily used by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	public void AddAttributesToRender(HtmlTextWriter writer)
	{
		AddAttributesToRender(writer, null);
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> and Web server control. This method is primarily used by control developers.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.WebControl" /> or <see cref="T:System.Web.UI.WebControls.WebControl" /> derived object that represents the Web server control associated with the <see cref="T:System.Web.UI.WebControls.Style" />. </param>
	public virtual void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
	{
		if (RegisteredCssClass.Length > 0)
		{
			string cssClass = CssClass;
			if (!string.IsNullOrEmpty(cssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass + " " + RegisteredCssClass);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, RegisteredCssClass);
			}
			return;
		}
		string cssClass2 = CssClass;
		if (cssClass2 != null && cssClass2.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass2);
		}
		CssStyleCollection cssStyleCollection = new CssStyleCollection();
		FillStyleAttributes(cssStyleCollection, owner);
		foreach (string key in cssStyleCollection.Keys)
		{
			writer.AddStyleAttribute(key, cssStyleCollection[key]);
		}
	}

	/// <summary>Adds the specified object's style properties to a <see cref="T:System.Web.UI.CssStyleCollection" /> object.</summary>
	/// <param name="attributes">The <see cref="T:System.Web.UI.CssStyleCollection" /> object to which to add the style properties. </param>
	/// <param name="urlResolver">A <see cref="T:System.Web.UI.IUrlResolutionService" /> -implemented object that contains the context information for the current location (URL). </param>
	protected virtual void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
	{
		if (CheckBit(8))
		{
			Color c = (Color)viewstate["BackColor"];
			if (!c.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(c));
			}
		}
		if (CheckBit(16))
		{
			Color c = (Color)viewstate["BorderColor"];
			if (!c.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(c));
			}
		}
		bool flag = false;
		if (CheckBit(32))
		{
			Unit unit = (Unit)viewstate["BorderWidth"];
			if (!unit.IsEmpty)
			{
				if (unit.Value > 0.0)
				{
					flag = true;
				}
				attributes.Add(HtmlTextWriterStyle.BorderWidth, unit.ToString());
			}
		}
		if (CheckBit(64))
		{
			BorderStyle borderStyle = (BorderStyle)viewstate["BorderStyle"];
			if (borderStyle != 0)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, borderStyle.ToString());
			}
			else if (flag)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
			}
		}
		else if (flag)
		{
			attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
		}
		if (CheckBit(4))
		{
			Color c = (Color)viewstate["ForeColor"];
			if (!c.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(c));
			}
		}
		if (CheckBit(128))
		{
			Unit unit = (Unit)viewstate["Height"];
			if (!unit.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.Height, unit.ToString());
			}
		}
		if (CheckBit(256))
		{
			Unit unit = (Unit)viewstate["Width"];
			if (!unit.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.Width, unit.ToString());
			}
		}
		Font.FillStyleAttributes(attributes, AlwaysRenderTextDecoration);
	}

	/// <summary>Duplicates the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> into the instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class that this method is called from.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to copy. </param>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.UI.WebControls.Style.RegisteredCssClass" /> has been set.</exception>
	public virtual void CopyFrom(Style s)
	{
		if (s != null && !s.IsEmpty)
		{
			if (s.fontinfo != null)
			{
				Font.CopyFrom(s.fontinfo);
			}
			if (s.CheckBit(8) && s.BackColor != Color.Empty)
			{
				BackColor = s.BackColor;
			}
			if (s.CheckBit(16) && s.BorderColor != Color.Empty)
			{
				BorderColor = s.BorderColor;
			}
			if (s.CheckBit(64) && s.BorderStyle != 0)
			{
				BorderStyle = s.BorderStyle;
			}
			if (s.CheckBit(32) && !s.BorderWidth.IsEmpty)
			{
				BorderWidth = s.BorderWidth;
			}
			if (s.CheckBit(2) && s.CssClass != string.Empty)
			{
				CssClass = s.CssClass;
			}
			if (s.CheckBit(4) && s.ForeColor != Color.Empty)
			{
				ForeColor = s.ForeColor;
			}
			if (s.CheckBit(128) && !s.Height.IsEmpty)
			{
				Height = s.Height;
			}
			if (s.CheckBit(256) && !s.Width.IsEmpty)
			{
				Width = s.Width;
			}
		}
	}

	/// <summary>Combines the style properties of the specified <see cref="T:System.Web.UI.WebControls.Style" /> with the instance of the <see cref="T:System.Web.UI.WebControls.Style" /> class that this method is called from.</summary>
	/// <param name="s">A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style to combine. </param>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.UI.WebControls.Style.RegisteredCssClass" /> has been set.</exception>
	public virtual void MergeWith(Style s)
	{
		if (s != null && !s.IsEmpty)
		{
			if (s.fontinfo != null)
			{
				Font.MergeWith(s.fontinfo);
			}
			if (!CheckBit(8) && s.CheckBit(8) && s.BackColor != Color.Empty)
			{
				BackColor = s.BackColor;
			}
			if (!CheckBit(16) && s.CheckBit(16) && s.BorderColor != Color.Empty)
			{
				BorderColor = s.BorderColor;
			}
			if (!CheckBit(64) && s.CheckBit(64) && s.BorderStyle != 0)
			{
				BorderStyle = s.BorderStyle;
			}
			if (!CheckBit(32) && s.CheckBit(32) && !s.BorderWidth.IsEmpty)
			{
				BorderWidth = s.BorderWidth;
			}
			if (!CheckBit(2) && s.CheckBit(2) && s.CssClass != string.Empty)
			{
				CssClass = s.CssClass;
			}
			if (!CheckBit(4) && s.CheckBit(4) && s.ForeColor != Color.Empty)
			{
				ForeColor = s.ForeColor;
			}
			if (!CheckBit(128) && s.CheckBit(128) && !s.Height.IsEmpty)
			{
				Height = s.Height;
			}
			if (!CheckBit(256) && s.CheckBit(256) && !s.Width.IsEmpty)
			{
				Width = s.Width;
			}
		}
	}

	/// <summary>Removes any defined style elements from the state bag.</summary>
	public virtual void Reset()
	{
		viewstate.Remove("BackColor");
		viewstate.Remove("BorderColor");
		viewstate.Remove("BorderStyle");
		viewstate.Remove("BorderWidth");
		viewstate.Remove("CssClass");
		viewstate.Remove("ForeColor");
		viewstate.Remove("Height");
		viewstate.Remove("Width");
		if (fontinfo != null)
		{
			fontinfo.Reset();
		}
		styles = 0;
		viewstate.Remove("_!SB");
		stylesTraked = 0;
	}

	/// <summary>Loads the previously saved state.</summary>
	/// <param name="state">The previously saved state. </param>
	protected internal void LoadViewState(object state)
	{
		viewstate.LoadViewState(state);
		LoadBitState();
	}

	/// <summary>A protected method. Saves any state that has been modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.</summary>
	/// <returns>An object that represents the saved state. The default is <see langword="null" />.</returns>
	protected internal virtual object SaveViewState()
	{
		SaveBitState();
		if (_isSharedViewState)
		{
			return null;
		}
		return viewstate.SaveViewState();
	}

	internal void SaveBitState()
	{
		if (stylesTraked != 0)
		{
			viewstate["_!SB"] = stylesTraked;
		}
	}

	internal void LoadBitState()
	{
		if (viewstate["_!SB"] != null)
		{
			int num = (int)viewstate["_!SB"];
			styles |= num;
			stylesTraked |= num;
		}
	}

	/// <summary>A protected internal method. Sets an internal bitmask field that indicates the style properties that are stored in the state bag.</summary>
	/// <param name="bit">A bitmask value.</param>
	protected internal virtual void SetBit(int bit)
	{
		styles |= bit;
		if (tracking)
		{
			stylesTraked |= bit;
		}
	}

	internal void RemoveBit(int bit)
	{
		styles &= ~bit;
		if (tracking)
		{
			stylesTraked &= ~bit;
			if (stylesTraked == 0)
			{
				viewstate.Remove("_!SB");
			}
		}
	}

	internal bool CheckBit(int bit)
	{
		return (styles & bit) != 0;
	}

	/// <summary>A protected method. Marks the beginning for tracking state changes on the control. Any changes made after tracking has begun will be tracked and saved as part of the control view state.</summary>
	protected internal virtual void TrackViewState()
	{
		tracking = true;
		viewstate.TrackViewState();
	}

	/// <summary>Loads the previously saved state.</summary>
	/// <param name="state">The previously saved state.</param>
	void IStateManager.LoadViewState(object state)
	{
		LoadViewState(state);
	}

	/// <summary>Returns the object containing state changes.</summary>
	/// <returns>An object that represents the saved state. The default is <see langword="null" />.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>Starts tracking state changes.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	internal void SetRegisteredCssClass(string name)
	{
		registered_class = name;
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.CssStyleCollection" /> object for the specified <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object.</summary>
	/// <param name="urlResolver">A <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL). </param>
	/// <returns>A <see cref="T:System.Web.UI.CssStyleCollection" /> object.</returns>
	public CssStyleCollection GetStyleAttributes(IUrlResolutionService urlResolver)
	{
		CssStyleCollection cssStyleCollection = new CssStyleCollection();
		FillStyleAttributes(cssStyleCollection, urlResolver);
		return cssStyleCollection;
	}

	internal void CopyTextStylesFrom(Style source)
	{
		if (source.CheckBit(4))
		{
			ForeColor = source.ForeColor;
		}
		if (source.CheckBit(65024))
		{
			Font.CopyFrom(source.Font);
		}
	}

	internal void RemoveTextStyles()
	{
		ForeColor = Color.Empty;
		fontinfo = null;
	}

	internal void AddCssClass(string cssClass)
	{
		if (!string.IsNullOrEmpty(cssClass))
		{
			string text = CssClass;
			if (text.Length > 0)
			{
				text += " ";
			}
			text += cssClass;
			CssClass = text;
		}
	}

	internal void PrependCssClass(string cssClass)
	{
		if (!string.IsNullOrEmpty(cssClass))
		{
			string cssClass2 = CssClass;
			if (cssClass2.Length > 0)
			{
				cssClass += " ";
			}
			CssClass = cssClass + cssClass2;
		}
	}

	/// <summary>Marks the <see cref="T:System.Web.UI.WebControls.Style" /> so that its state will be recorded in view state.</summary>
	public void SetDirty()
	{
		if (viewstate != null)
		{
			viewstate.SetDirty(dirty: true);
		}
		stylesTraked = styles;
	}
}
