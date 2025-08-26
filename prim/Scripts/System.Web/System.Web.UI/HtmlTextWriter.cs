using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI;

/// <summary>Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlTextWriter : TextWriter
{
	private struct AddedTag
	{
		public string name;

		public HtmlTextWriterTag key;

		public bool ignore;
	}

	private struct AddedStyle
	{
		public string name;

		public HtmlTextWriterStyle key;

		public string value;
	}

	private struct AddedAttr
	{
		public string name;

		public HtmlTextWriterAttribute key;

		public string value;
	}

	private enum TagType
	{
		Block,
		Inline,
		SelfClosing
	}

	private sealed class HtmlTag
	{
		public readonly HtmlTextWriterTag key;

		public readonly string name;

		public readonly TagType tag_type;

		public HtmlTag(HtmlTextWriterTag k, string n, TagType tt)
		{
			key = k;
			name = n;
			tag_type = tt;
		}
	}

	private sealed class HtmlStyle
	{
		public readonly HtmlTextWriterStyle key;

		public readonly string name;

		public HtmlStyle(HtmlTextWriterStyle k, string n)
		{
			key = k;
			name = n;
		}
	}

	private sealed class HtmlAttribute
	{
		public readonly HtmlTextWriterAttribute key;

		public readonly string name;

		public HtmlAttribute(HtmlTextWriterAttribute k, string n)
		{
			key = k;
			name = n;
		}
	}

	private static readonly Hashtable _tagTable;

	private static readonly Hashtable _attributeTable;

	private static readonly Hashtable _styleTable;

	private int indent;

	private TextWriter b;

	private string tab_string;

	private bool newline;

	private AddedStyle[] styles;

	private AddedAttr[] attrs;

	private AddedTag[] tagstack;

	private int styles_pos = -1;

	private int attrs_pos = -1;

	private int tagstack_pos = -1;

	/// <summary>Represents a single tab character.</summary>
	public const string DefaultTabString = "\t";

	/// <summary>Represents the quotation mark (") character.</summary>
	public const char DoubleQuoteChar = '"';

	/// <summary>Represents the left angle bracket and slash mark (&lt;/) of the closing tag of a markup element.</summary>
	public const string EndTagLeftChars = "</";

	/// <summary>Represents the equal sign (<see langword="=" />).</summary>
	public const char EqualsChar = '=';

	/// <summary>Represents an equal sign (=) and a double quotation mark (") together in a string (="). </summary>
	public const string EqualsDoubleQuoteString = "=\"";

	/// <summary>Represents a space and the self-closing slash mark (/) of a markup tag.</summary>
	public const string SelfClosingChars = " /";

	/// <summary>Represents the closing slash mark and right angle bracket (/&gt;) of a self-closing markup element.</summary>
	public const string SelfClosingTagEnd = " />";

	/// <summary>Represents the semicolon (;).</summary>
	public const char SemicolonChar = ';';

	/// <summary>Represents an apostrophe (').</summary>
	public const char SingleQuoteChar = '\'';

	/// <summary>Represents the slash mark (/).</summary>
	public const char SlashChar = '/';

	/// <summary>Represents a space ( ) character.</summary>
	public const char SpaceChar = ' ';

	/// <summary>Represents the style equals (<see langword=":" />) character used to set style attributes equal to values.</summary>
	public const char StyleEqualsChar = ':';

	/// <summary>Represents the opening angle bracket (&lt;) of a markup tag.</summary>
	public const char TagLeftChar = '<';

	/// <summary>Represents the closing angle bracket (&gt;) of a markup tag.</summary>
	public const char TagRightChar = '>';

	private static HtmlTag[] tags;

	private static HtmlAttribute[] htmlattrs;

	private static HtmlStyle[] htmlstyles;

	/// <summary>Gets the encoding that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object uses to write content to the page.</summary>
	/// <returns>The <see cref="T:System.Text.Encoding" /> in which the markup is written to the page.</returns>
	public override Encoding Encoding => b.Encoding;

	/// <summary>Gets or sets the number of tab positions to indent the beginning of each line of markup.</summary>
	/// <returns>The number of tab positions to indent each line.</returns>
	public int Indent
	{
		get
		{
			return indent;
		}
		set
		{
			indent = value;
		}
	}

	/// <summary>Gets or sets the text writer that writes the inner content of the markup element.</summary>
	/// <returns>A <see cref="T:System.IO.TextWriter" /> that writes the inner markup content.</returns>
	public TextWriter InnerWriter
	{
		get
		{
			return b;
		}
		set
		{
			b = value;
		}
	}

	/// <summary>Gets or sets the line terminator string used by the <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <returns>The line terminator string used by the current <see cref="T:System.Web.UI.HtmlTextWriter" />.</returns>
	public override string NewLine
	{
		get
		{
			return b.NewLine;
		}
		set
		{
			b.NewLine = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the specified markup element.</summary>
	/// <returns>The markup element that is having its opening tag rendered.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The property value cannot be set. </exception>
	protected HtmlTextWriterTag TagKey
	{
		get
		{
			if (tagstack_pos == -1)
			{
				throw new InvalidOperationException();
			}
			return tagstack[tagstack_pos].key;
		}
		set
		{
			tagstack[tagstack_pos].key = value;
			tagstack[tagstack_pos].name = GetTagName(value);
		}
	}

	/// <summary>Gets or sets the tag name of the markup element being rendered.</summary>
	/// <returns>The tag name of the markup element being rendered.</returns>
	protected string TagName
	{
		get
		{
			if (tagstack_pos == -1)
			{
				throw new InvalidOperationException();
			}
			return tagstack[tagstack_pos].name;
		}
		set
		{
			tagstack[tagstack_pos].name = value;
			tagstack[tagstack_pos].key = GetTagKey(value);
			if (tagstack[tagstack_pos].key != 0)
			{
				tagstack[tagstack_pos].name = GetTagName(tagstack[tagstack_pos].key);
			}
		}
	}

	private bool TagIgnore
	{
		get
		{
			if (tagstack_pos == -1)
			{
				throw new InvalidOperationException();
			}
			return tagstack[tagstack_pos].ignore;
		}
		set
		{
			if (tagstack_pos == -1)
			{
				throw new InvalidOperationException();
			}
			tagstack[tagstack_pos].ignore = value;
		}
	}

	static HtmlTextWriter()
	{
		tags = new HtmlTag[97]
		{
			new HtmlTag(HtmlTextWriterTag.Unknown, string.Empty, TagType.Block),
			new HtmlTag(HtmlTextWriterTag.A, "a", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Acronym, "acronym", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Address, "address", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Area, "area", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.B, "b", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Base, "base", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Basefont, "basefont", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Bdo, "bdo", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Bgsound, "bgsound", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Big, "big", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Blockquote, "blockquote", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Body, "body", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Br, "br", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Button, "button", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Caption, "caption", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Center, "center", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Cite, "cite", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Code, "code", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Col, "col", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Colgroup, "colgroup", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Dd, "dd", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Del, "del", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Dfn, "dfn", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Dir, "dir", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Div, "div", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Dl, "dl", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Dt, "dt", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Em, "em", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Embed, "embed", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Fieldset, "fieldset", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Font, "font", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Form, "form", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Frame, "frame", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Frameset, "frameset", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.H1, "h1", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.H2, "h2", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.H3, "h3", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.H4, "h4", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.H5, "h5", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.H6, "h6", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Head, "head", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Hr, "hr", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Html, "html", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.I, "i", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Iframe, "iframe", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Img, "img", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Input, "input", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Ins, "ins", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Isindex, "isindex", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Kbd, "kbd", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Label, "label", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Legend, "legend", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Li, "li", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Link, "link", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Map, "map", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Marquee, "marquee", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Menu, "menu", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Meta, "meta", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Nobr, "nobr", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Noframes, "noframes", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Noscript, "noscript", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Object, "object", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Ol, "ol", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Option, "option", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.P, "p", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Param, "param", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Pre, "pre", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Q, "q", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Rt, "rt", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Ruby, "ruby", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.S, "s", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Samp, "samp", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Script, "script", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Select, "select", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Small, "small", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Span, "span", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Strike, "strike", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Strong, "strong", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Style, "style", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Sub, "sub", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Sup, "sup", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Table, "table", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Tbody, "tbody", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Td, "td", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Textarea, "textarea", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Tfoot, "tfoot", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Th, "th", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Thead, "thead", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Title, "title", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Tr, "tr", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Tt, "tt", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.U, "u", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Ul, "ul", TagType.Block),
			new HtmlTag(HtmlTextWriterTag.Var, "var", TagType.Inline),
			new HtmlTag(HtmlTextWriterTag.Wbr, "wbr", TagType.SelfClosing),
			new HtmlTag(HtmlTextWriterTag.Xml, "xml", TagType.Block)
		};
		htmlattrs = new HtmlAttribute[54]
		{
			new HtmlAttribute(HtmlTextWriterAttribute.Accesskey, "accesskey"),
			new HtmlAttribute(HtmlTextWriterAttribute.Align, "align"),
			new HtmlAttribute(HtmlTextWriterAttribute.Alt, "alt"),
			new HtmlAttribute(HtmlTextWriterAttribute.Background, "background"),
			new HtmlAttribute(HtmlTextWriterAttribute.Bgcolor, "bgcolor"),
			new HtmlAttribute(HtmlTextWriterAttribute.Border, "border"),
			new HtmlAttribute(HtmlTextWriterAttribute.Bordercolor, "bordercolor"),
			new HtmlAttribute(HtmlTextWriterAttribute.Cellpadding, "cellpadding"),
			new HtmlAttribute(HtmlTextWriterAttribute.Cellspacing, "cellspacing"),
			new HtmlAttribute(HtmlTextWriterAttribute.Checked, "checked"),
			new HtmlAttribute(HtmlTextWriterAttribute.Class, "class"),
			new HtmlAttribute(HtmlTextWriterAttribute.Cols, "cols"),
			new HtmlAttribute(HtmlTextWriterAttribute.Colspan, "colspan"),
			new HtmlAttribute(HtmlTextWriterAttribute.Disabled, "disabled"),
			new HtmlAttribute(HtmlTextWriterAttribute.For, "for"),
			new HtmlAttribute(HtmlTextWriterAttribute.Height, "height"),
			new HtmlAttribute(HtmlTextWriterAttribute.Href, "href"),
			new HtmlAttribute(HtmlTextWriterAttribute.Id, "id"),
			new HtmlAttribute(HtmlTextWriterAttribute.Maxlength, "maxlength"),
			new HtmlAttribute(HtmlTextWriterAttribute.Multiple, "multiple"),
			new HtmlAttribute(HtmlTextWriterAttribute.Name, "name"),
			new HtmlAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap"),
			new HtmlAttribute(HtmlTextWriterAttribute.Onchange, "onchange"),
			new HtmlAttribute(HtmlTextWriterAttribute.Onclick, "onclick"),
			new HtmlAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly"),
			new HtmlAttribute(HtmlTextWriterAttribute.Rows, "rows"),
			new HtmlAttribute(HtmlTextWriterAttribute.Rowspan, "rowspan"),
			new HtmlAttribute(HtmlTextWriterAttribute.Rules, "rules"),
			new HtmlAttribute(HtmlTextWriterAttribute.Selected, "selected"),
			new HtmlAttribute(HtmlTextWriterAttribute.Size, "size"),
			new HtmlAttribute(HtmlTextWriterAttribute.Src, "src"),
			new HtmlAttribute(HtmlTextWriterAttribute.Style, "style"),
			new HtmlAttribute(HtmlTextWriterAttribute.Tabindex, "tabindex"),
			new HtmlAttribute(HtmlTextWriterAttribute.Target, "target"),
			new HtmlAttribute(HtmlTextWriterAttribute.Title, "title"),
			new HtmlAttribute(HtmlTextWriterAttribute.Type, "type"),
			new HtmlAttribute(HtmlTextWriterAttribute.Valign, "valign"),
			new HtmlAttribute(HtmlTextWriterAttribute.Value, "value"),
			new HtmlAttribute(HtmlTextWriterAttribute.Width, "width"),
			new HtmlAttribute(HtmlTextWriterAttribute.Wrap, "wrap"),
			new HtmlAttribute(HtmlTextWriterAttribute.Abbr, "abbr"),
			new HtmlAttribute(HtmlTextWriterAttribute.AutoComplete, "autocomplete"),
			new HtmlAttribute(HtmlTextWriterAttribute.Axis, "axis"),
			new HtmlAttribute(HtmlTextWriterAttribute.Content, "content"),
			new HtmlAttribute(HtmlTextWriterAttribute.Coords, "coords"),
			new HtmlAttribute(HtmlTextWriterAttribute.DesignerRegion, "_designerregion"),
			new HtmlAttribute(HtmlTextWriterAttribute.Dir, "dir"),
			new HtmlAttribute(HtmlTextWriterAttribute.Headers, "headers"),
			new HtmlAttribute(HtmlTextWriterAttribute.Longdesc, "longdesc"),
			new HtmlAttribute(HtmlTextWriterAttribute.Rel, "rel"),
			new HtmlAttribute(HtmlTextWriterAttribute.Scope, "scope"),
			new HtmlAttribute(HtmlTextWriterAttribute.Shape, "shape"),
			new HtmlAttribute(HtmlTextWriterAttribute.Usemap, "usemap"),
			new HtmlAttribute(HtmlTextWriterAttribute.VCardName, "vcard_name")
		};
		htmlstyles = new HtmlStyle[43]
		{
			new HtmlStyle(HtmlTextWriterStyle.BackgroundColor, "background-color"),
			new HtmlStyle(HtmlTextWriterStyle.BackgroundImage, "background-image"),
			new HtmlStyle(HtmlTextWriterStyle.BorderCollapse, "border-collapse"),
			new HtmlStyle(HtmlTextWriterStyle.BorderColor, "border-color"),
			new HtmlStyle(HtmlTextWriterStyle.BorderStyle, "border-style"),
			new HtmlStyle(HtmlTextWriterStyle.BorderWidth, "border-width"),
			new HtmlStyle(HtmlTextWriterStyle.Color, "color"),
			new HtmlStyle(HtmlTextWriterStyle.FontFamily, "font-family"),
			new HtmlStyle(HtmlTextWriterStyle.FontSize, "font-size"),
			new HtmlStyle(HtmlTextWriterStyle.FontStyle, "font-style"),
			new HtmlStyle(HtmlTextWriterStyle.FontWeight, "font-weight"),
			new HtmlStyle(HtmlTextWriterStyle.Height, "height"),
			new HtmlStyle(HtmlTextWriterStyle.TextDecoration, "text-decoration"),
			new HtmlStyle(HtmlTextWriterStyle.Width, "width"),
			new HtmlStyle(HtmlTextWriterStyle.ListStyleImage, "list-style-image"),
			new HtmlStyle(HtmlTextWriterStyle.ListStyleType, "list-style-type"),
			new HtmlStyle(HtmlTextWriterStyle.Cursor, "cursor"),
			new HtmlStyle(HtmlTextWriterStyle.Direction, "direction"),
			new HtmlStyle(HtmlTextWriterStyle.Display, "display"),
			new HtmlStyle(HtmlTextWriterStyle.Filter, "filter"),
			new HtmlStyle(HtmlTextWriterStyle.FontVariant, "font-variant"),
			new HtmlStyle(HtmlTextWriterStyle.Left, "left"),
			new HtmlStyle(HtmlTextWriterStyle.Margin, "margin"),
			new HtmlStyle(HtmlTextWriterStyle.MarginBottom, "margin-bottom"),
			new HtmlStyle(HtmlTextWriterStyle.MarginLeft, "margin-left"),
			new HtmlStyle(HtmlTextWriterStyle.MarginRight, "margin-right"),
			new HtmlStyle(HtmlTextWriterStyle.MarginTop, "margin-top"),
			new HtmlStyle(HtmlTextWriterStyle.Overflow, "overflow"),
			new HtmlStyle(HtmlTextWriterStyle.OverflowX, "overflow-x"),
			new HtmlStyle(HtmlTextWriterStyle.OverflowY, "overflow-y"),
			new HtmlStyle(HtmlTextWriterStyle.Padding, "padding"),
			new HtmlStyle(HtmlTextWriterStyle.PaddingBottom, "padding-bottom"),
			new HtmlStyle(HtmlTextWriterStyle.PaddingLeft, "padding-left"),
			new HtmlStyle(HtmlTextWriterStyle.PaddingRight, "padding-right"),
			new HtmlStyle(HtmlTextWriterStyle.PaddingTop, "padding-top"),
			new HtmlStyle(HtmlTextWriterStyle.Position, "position"),
			new HtmlStyle(HtmlTextWriterStyle.TextAlign, "text-align"),
			new HtmlStyle(HtmlTextWriterStyle.VerticalAlign, "vertical-align"),
			new HtmlStyle(HtmlTextWriterStyle.TextOverflow, "text-overflow"),
			new HtmlStyle(HtmlTextWriterStyle.Top, "top"),
			new HtmlStyle(HtmlTextWriterStyle.Visibility, "visibility"),
			new HtmlStyle(HtmlTextWriterStyle.WhiteSpace, "white-space"),
			new HtmlStyle(HtmlTextWriterStyle.ZIndex, "z-index")
		};
		_tagTable = new Hashtable(tags.Length, StringComparer.OrdinalIgnoreCase);
		_attributeTable = new Hashtable(htmlattrs.Length, StringComparer.OrdinalIgnoreCase);
		_styleTable = new Hashtable(htmlstyles.Length, StringComparer.OrdinalIgnoreCase);
		HtmlTag[] array = tags;
		foreach (HtmlTag htmlTag in array)
		{
			_tagTable.Add(htmlTag.name, htmlTag);
		}
		HtmlAttribute[] array2 = htmlattrs;
		foreach (HtmlAttribute htmlAttribute in array2)
		{
			_attributeTable.Add(htmlAttribute.name, htmlAttribute);
		}
		HtmlStyle[] array3 = htmlstyles;
		foreach (HtmlStyle htmlStyle in array3)
		{
			_styleTable.Add(htmlStyle.name, htmlStyle);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> class that uses a default tab string.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> instance that renders the markup content. </param>
	public HtmlTextWriter(TextWriter writer)
		: this(writer, "\t")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> class with a specified tab string character.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
	/// <param name="tabString">The string to use to render a line indentation. </param>
	public HtmlTextWriter(TextWriter writer, string tabString)
	{
		b = writer;
		tab_string = tabString;
	}

	internal static string StaticGetStyleName(HtmlTextWriterStyle styleKey)
	{
		if ((int)styleKey < htmlstyles.Length)
		{
			return htmlstyles[(int)styleKey].name;
		}
		return null;
	}

	/// <summary>Registers markup attributes, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
	/// <param name="name">A string containing the name of the markup attribute to register. </param>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that corresponds with the attribute name. </param>
	[MonoTODO("Does nothing")]
	protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
	{
	}

	/// <summary>Registers markup style properties, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
	/// <param name="name">The string passed from the source file specifying the style name. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that corresponds with the specified style. </param>
	[MonoTODO("Does nothing")]
	protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
	{
	}

	/// <summary>Registers markup tags, whether literals or dynamically generated, from the source file so that they can be properly rendered to the requesting client.</summary>
	/// <param name="name">A string that contains the HTML tag. </param>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies which element to render. </param>
	[MonoTODO("Does nothing")]
	protected static void RegisterTag(string name, HtmlTextWriterTag key)
	{
	}

	/// <summary>Adds the markup attribute and the attribute value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method, with optional encoding.</summary>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the markup attribute to add to the output stream. </param>
	/// <param name="value">A string containing the value to assign to the attribute. </param>
	/// <param name="fEncode">
	///       <see langword="true" /> to encode the attribute and its value; otherwise, <see langword="false" />. </param>
	public virtual void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode)
	{
		if (fEncode)
		{
			value = HttpUtility.HtmlAttributeEncode(value);
		}
		AddAttribute(GetAttributeName(key), value, key);
	}

	/// <summary>Adds the markup attribute and the attribute value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the markup attribute to add to the output stream. </param>
	/// <param name="value">A string containing the value to assign to the attribute. </param>
	public virtual void AddAttribute(HtmlTextWriterAttribute key, string value)
	{
		if (key != HtmlTextWriterAttribute.Name && key != HtmlTextWriterAttribute.Id)
		{
			value = HttpUtility.HtmlAttributeEncode(value);
		}
		AddAttribute(GetAttributeName(key), value, key);
	}

	/// <summary>Adds the specified markup attribute and value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method, with optional encoding.</summary>
	/// <param name="name">A string containing the name of the attribute to add. </param>
	/// <param name="value">A string containing the value to assign to the attribute. </param>
	/// <param name="fEndode">
	///       <see langword="true" /> to encode the attribute and its value; otherwise, <see langword="false" />. </param>
	public virtual void AddAttribute(string name, string value, bool fEndode)
	{
		if (fEndode)
		{
			value = HttpUtility.HtmlAttributeEncode(value);
		}
		AddAttribute(name, value, GetAttributeKey(name));
	}

	/// <summary>Adds the specified markup attribute and value to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="name">A string containing the name of the attribute to add. </param>
	/// <param name="value">A string containing the value to assign to the attribute. </param>
	public virtual void AddAttribute(string name, string value)
	{
		HtmlTextWriterAttribute attributeKey = GetAttributeKey(name);
		if (attributeKey != HtmlTextWriterAttribute.Name && attributeKey != HtmlTextWriterAttribute.Id)
		{
			value = HttpUtility.HtmlAttributeEncode(value);
		}
		AddAttribute(name, value, attributeKey);
	}

	/// <summary>Adds the specified markup attribute and value, along with an <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value, to the opening tag of the element that the <see cref="T:System.Web.UI.HtmlTextWriter" /> object creates with a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="name">A string containing the name of the attribute to add. </param>
	/// <param name="value">A string containing the value to assign to the attribute. </param>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> that represents the attribute. </param>
	protected virtual void AddAttribute(string name, string value, HtmlTextWriterAttribute key)
	{
		NextAttrStack();
		attrs[attrs_pos].name = name;
		attrs[attrs_pos].value = value;
		attrs[attrs_pos].key = key;
	}

	/// <summary>Adds the specified markup style attribute and the attribute value, along with an <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value, to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="name">A string that contains the style attribute to be added. </param>
	/// <param name="value">A string that contains the value to assign to the attribute. </param>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that represents the style attribute to add. </param>
	protected virtual void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key)
	{
		NextStyleStack();
		styles[styles_pos].name = name;
		value = HttpUtility.HtmlAttributeEncode(value);
		styles[styles_pos].value = value;
		styles[styles_pos].key = key;
	}

	/// <summary>Adds the specified markup style attribute and the attribute value to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="name">A string that contains the style attribute to add. </param>
	/// <param name="value">A string that contains the value to assign to the attribute. </param>
	public virtual void AddStyleAttribute(string name, string value)
	{
		AddStyleAttribute(name, value, GetStyleKey(name));
	}

	/// <summary>Adds the markup style attribute associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> value and the attribute value to the opening markup tag created by a subsequent call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="key">An <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> that represents the style attribute to add to the output stream. </param>
	/// <param name="value">A string that contains the value to assign to the attribute. </param>
	public virtual void AddStyleAttribute(HtmlTextWriterStyle key, string value)
	{
		AddStyleAttribute(GetStyleName(key), value, key);
	}

	/// <summary>Closes the <see cref="T:System.Web.UI.HtmlTextWriter" /> object and releases any system resources associated with it.</summary>
	public override void Close()
	{
		b.Close();
	}

	/// <summary>Encodes the value of the specified markup attribute based on the requirements of the <see cref="T:System.Web.HttpRequest" /> object of the current context.</summary>
	/// <param name="attrKey">An <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> representing the markup attribute. </param>
	/// <param name="value">A string containing the attribute value to encode. </param>
	/// <returns>A string containing the encoded attribute value.</returns>
	protected virtual string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
	{
		return HttpUtility.HtmlAttributeEncode(value);
	}

	/// <summary>Encodes the value of the specified markup attribute based on the requirements of the <see cref="T:System.Web.HttpRequest" /> object of the current context.</summary>
	/// <param name="value">A string containing the attribute value to encode. </param>
	/// <param name="fEncode">
	///       <see langword="true" /> to encode the attribute value; otherwise, <see langword="false" />. </param>
	/// <returns>A string containing the encoded attribute value, <see langword="null" /> if <paramref name="value" /> is empty, or the unencoded attribute value if <paramref name="fEncode" /> is <see langword="false" />.</returns>
	protected string EncodeAttributeValue(string value, bool fEncode)
	{
		if (fEncode)
		{
			return HttpUtility.HtmlAttributeEncode(value);
		}
		return value;
	}

	/// <summary>Performs minimal URL encoding by converting spaces in the specified URL to the string "%20".</summary>
	/// <param name="url">A string containing the URL to encode. </param>
	/// <returns>A string containing the encoded URL.</returns>
	protected string EncodeUrl(string url)
	{
		return HttpUtility.UrlPathEncode(url);
	}

	/// <summary>Removes all the markup and style attributes on all properties of the page or Web server control.</summary>
	protected virtual void FilterAttributes()
	{
		AddedAttr addedAttr = default(AddedAttr);
		for (int i = 0; i <= attrs_pos; i++)
		{
			AddedAttr addedAttr2 = attrs[i];
			if (OnAttributeRender(addedAttr2.name, addedAttr2.value, addedAttr2.key))
			{
				if (addedAttr2.key == HtmlTextWriterAttribute.Style)
				{
					addedAttr = addedAttr2;
				}
				else
				{
					WriteAttribute(addedAttr2.name, addedAttr2.value, fEncode: false);
				}
			}
		}
		if (styles_pos != -1 || addedAttr.value != null)
		{
			Write(' ');
			Write("style");
			Write("=\"");
			for (int j = 0; j <= styles_pos; j++)
			{
				AddedStyle addedStyle = styles[j];
				if (OnStyleAttributeRender(addedStyle.name, addedStyle.value, addedStyle.key))
				{
					if (addedStyle.key == HtmlTextWriterStyle.BackgroundImage)
					{
						addedStyle.value = "url(" + HttpUtility.UrlPathEncode(addedStyle.value) + ")";
					}
					WriteStyleAttribute(addedStyle.name, addedStyle.value, fEncode: false);
				}
			}
			Write(addedAttr.value);
			Write('"');
		}
		styles_pos = (attrs_pos = -1);
	}

	/// <summary>Clears all buffers for the current <see cref="T:System.Web.UI.HtmlTextWriter" /> object and causes any buffered data to be written to the output stream.</summary>
	public override void Flush()
	{
		b.Flush();
	}

	/// <summary>Obtains the corresponding <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value for the specified attribute.</summary>
	/// <param name="attrName">A string that contains the attribute for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterAttribute" />. </param>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value for the specified attribute; otherwise, an invalid <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> value if the attribute is not a member of the enumeration.</returns>
	protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
	{
		object obj = _attributeTable[attrName];
		if (obj == null)
		{
			return (HtmlTextWriterAttribute)(-1);
		}
		return ((HtmlAttribute)obj).key;
	}

	/// <summary>Obtains the name of the markup attribute associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> value.</summary>
	/// <param name="attrKey">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> to obtain the markup attribute name for. </param>
	/// <returns>A string containing the name of the markup attribute.</returns>
	protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
	{
		if ((int)attrKey < htmlattrs.Length)
		{
			return htmlattrs[(int)attrKey].name;
		}
		return null;
	}

	/// <summary>Obtains the <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value for the specified style.</summary>
	/// <param name="styleName">The style attribute for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterStyle" />. </param>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value corresponding to <paramref name="styleName" />.</returns>
	protected HtmlTextWriterStyle GetStyleKey(string styleName)
	{
		object obj = _styleTable[styleName];
		if (obj == null)
		{
			return (HtmlTextWriterStyle)(-1);
		}
		return ((HtmlStyle)obj).key;
	}

	/// <summary>Obtains the markup style attribute name associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value.</summary>
	/// <param name="styleKey">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> to obtain the style attribute name for. </param>
	/// <returns>The style attribute name associated with the <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value specified in <paramref name="styleKey" />.</returns>
	protected string GetStyleName(HtmlTextWriterStyle styleKey)
	{
		return StaticGetStyleName(styleKey);
	}

	/// <summary>Obtains the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value associated with the specified markup element.</summary>
	/// <param name="tagName">The markup element for which to obtain the <see cref="T:System.Web.UI.HtmlTextWriterTag" />. </param>
	/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value; otherwise, if <paramref name="tagName" /> is not associated with a specific <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value, <see cref="F:System.Web.UI.HtmlTextWriterTag.Unknown" />.</returns>
	protected virtual HtmlTextWriterTag GetTagKey(string tagName)
	{
		object obj = _tagTable[tagName];
		if (obj == null)
		{
			return HtmlTextWriterTag.Unknown;
		}
		return ((HtmlTag)obj).key;
	}

	internal static string StaticGetTagName(HtmlTextWriterTag tagKey)
	{
		if ((int)tagKey < tags.Length)
		{
			return tags[(int)tagKey].name;
		}
		return null;
	}

	/// <summary>Obtains the markup element associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</summary>
	/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> to obtain the markup element for. </param>
	/// <returns>A string representing the markup element.</returns>
	protected virtual string GetTagName(HtmlTextWriterTag tagKey)
	{
		if ((int)tagKey < tags.Length)
		{
			return tags[(int)tagKey].name;
		}
		return null;
	}

	/// <summary>Determines whether the specified markup attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, <see langword="false" />.</returns>
	protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
	{
		string value;
		return IsAttributeDefined(key, out value);
	}

	/// <summary>Determines whether the specified markup attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
	/// <param name="value">The value assigned to the attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, <see langword="false" />.</returns>
	protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
	{
		for (int i = 0; i <= attrs_pos; i++)
		{
			if (attrs[i].key == key)
			{
				value = attrs[i].value;
				return true;
			}
		}
		value = null;
		return false;
	}

	/// <summary>Determines whether the specified markup style attribute is rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the attribute will be rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, <see langword="false" />.</returns>
	protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
	{
		string value;
		return IsStyleAttributeDefined(key, out value);
	}

	/// <summary>Determines whether the specified markup style attribute and its value are rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method.</summary>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the attribute. </param>
	/// <param name="value">The value assigned to the style attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the attribute and its value will be rendered during the next call to the <see cref="Overload:System.Web.UI.HtmlTextWriter.RenderBeginTag" /> method; otherwise, <see langword="false" />.</returns>
	protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
	{
		for (int i = 0; i <= styles_pos; i++)
		{
			if (styles[i].key == key)
			{
				value = styles[i].value;
				return true;
			}
		}
		value = null;
		return false;
	}

	/// <summary>Determines whether the specified markup attribute and its value can be rendered to the current markup element.</summary>
	/// <param name="name">A string containing the name of the attribute to render. </param>
	/// <param name="value">A string containing the value that is assigned to the attribute. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with the markup attribute. </param>
	/// <returns>Always <see langword="true" />.</returns>
	protected virtual bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
	{
		return true;
	}

	/// <summary>Determines whether the specified markup style attribute and its value can be rendered to the current markup element.</summary>
	/// <param name="name">A string containing the name of the style attribute to render. </param>
	/// <param name="value">A string containing the value that is assigned to the style attribute. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with the style attribute. </param>
	/// <returns>Always <see langword="true" />.</returns>
	protected virtual bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
	{
		return true;
	}

	/// <summary>Determines whether the specified markup element will be rendered to the requesting page.</summary>
	/// <param name="name">A string containing the name of the element to render. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> associated with the element. </param>
	/// <returns>Always <see langword="true" />.</returns>
	protected virtual bool OnTagRender(string name, HtmlTextWriterTag key)
	{
		return true;
	}

	/// <summary>Writes a series of tab strings that represent the indentation level for a line of markup characters.</summary>
	protected virtual void OutputTabs()
	{
		if (newline)
		{
			newline = false;
			for (int i = 0; i < Indent; i++)
			{
				b.Write(tab_string);
			}
		}
	}

	/// <summary>Removes the most recently saved markup element from the list of rendered elements.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the most recently rendered markup element.</returns>
	/// <exception cref="T:System.InvalidOperationException">The list of rendered elements is empty. </exception>
	protected string PopEndTag()
	{
		if (tagstack_pos == -1)
		{
			throw new InvalidOperationException();
		}
		string tagName = TagName;
		tagstack_pos--;
		return tagName;
	}

	/// <summary>Saves the specified markup element for later use when generating the end tag for a markup element.</summary>
	/// <param name="endTag">The closing tag of the markup element. </param>
	protected void PushEndTag(string endTag)
	{
		NextTagStack();
		TagName = endTag;
	}

	private void PushEndTag(HtmlTextWriterTag t)
	{
		NextTagStack();
		TagKey = t;
	}

	/// <summary>Writes any text or spacing that occurs after the content and before the closing tag of the markup element to the markup output stream.</summary>
	/// <returns>A string that contains the spacing or text to write after the content of the element. </returns>
	protected virtual string RenderAfterContent()
	{
		return null;
	}

	/// <summary>Writes any spacing or text that occurs after the closing tag for a markup element.</summary>
	/// <returns>The spacing or text to write after the closing tag of the element. </returns>
	protected virtual string RenderAfterTag()
	{
		return null;
	}

	/// <summary>Writes any text or spacing before the content and after the opening tag of a markup element.</summary>
	/// <returns>The text or spacing to write prior to the content of the element. If not overridden, <see cref="M:System.Web.UI.HtmlTextWriter.RenderBeforeContent" /> returns <see langword="null" />.</returns>
	protected virtual string RenderBeforeContent()
	{
		return null;
	}

	/// <summary>Writes any text or spacing that occurs before the opening tag of a markup element.</summary>
	/// <returns>The text or spacing to write before the markup element opening tag. If not overridden, <see langword="null" />.</returns>
	protected virtual string RenderBeforeTag()
	{
		return null;
	}

	/// <summary>Writes the opening tag of the specified markup element to the output stream.</summary>
	/// <param name="tagName">A string containing the name of the markup element for which to render the opening tag.</param>
	public virtual void RenderBeginTag(string tagName)
	{
		bool tagIgnore = !OnTagRender(tagName, GetTagKey(tagName));
		PushEndTag(tagName);
		TagIgnore = tagIgnore;
		DoBeginTag();
	}

	/// <summary>Writes the opening tag of the markup element associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value to the output stream.</summary>
	/// <param name="tagKey">One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> values that defines the opening tag of the markup element to render. </param>
	public virtual void RenderBeginTag(HtmlTextWriterTag tagKey)
	{
		bool tagIgnore = !OnTagRender(GetTagName(tagKey), tagKey);
		PushEndTag(tagKey);
		DoBeginTag();
		TagIgnore = tagIgnore;
	}

	private void WriteIfNotNull(string s)
	{
		if (s != null)
		{
			Write(s);
		}
	}

	private void DoBeginTag()
	{
		WriteIfNotNull(RenderBeforeTag());
		if (!TagIgnore)
		{
			WriteBeginTag(TagName);
			FilterAttributes();
			HtmlTextWriterTag htmlTextWriterTag = (((int)TagKey < tags.Length) ? TagKey : HtmlTextWriterTag.Unknown);
			switch (tags[(int)htmlTextWriterTag].tag_type)
			{
			case TagType.Inline:
				Write('>');
				break;
			case TagType.Block:
				Write('>');
				WriteLine();
				Indent++;
				break;
			case TagType.SelfClosing:
				Write(" />");
				break;
			}
		}
		WriteIfNotNull(RenderBeforeContent());
	}

	/// <summary>Writes the end tag of a markup element to the output stream.</summary>
	public virtual void RenderEndTag()
	{
		WriteIfNotNull(RenderAfterContent());
		if (!TagIgnore)
		{
			HtmlTextWriterTag htmlTextWriterTag = (((int)TagKey < tags.Length) ? TagKey : HtmlTextWriterTag.Unknown);
			switch (tags[(int)htmlTextWriterTag].tag_type)
			{
			case TagType.Inline:
				WriteEndTag(TagName);
				break;
			case TagType.Block:
				Indent--;
				WriteLineNoTabs(string.Empty);
				WriteEndTag(TagName);
				break;
			}
		}
		WriteIfNotNull(RenderAfterTag());
		PopEndTag();
	}

	/// <summary>Writes the specified markup attribute and value to the output stream, and, if specified, writes the value encoded.</summary>
	/// <param name="name">The markup attribute to write to the output stream. </param>
	/// <param name="value">The value assigned to the attribute. </param>
	/// <param name="fEncode">
	///       <see langword="true" /> to encode the attribute and its assigned value; otherwise, <see langword="false" />. </param>
	public virtual void WriteAttribute(string name, string value, bool fEncode)
	{
		Write(' ');
		Write(name);
		if (value != null)
		{
			Write("=\"");
			value = EncodeAttributeValue(value, fEncode);
			Write(value);
			Write('"');
		}
	}

	/// <summary>Writes any tab spacing and the opening tag of the specified markup element to the output stream.</summary>
	/// <param name="tagName">The markup element of which to write the opening tag. </param>
	public virtual void WriteBeginTag(string tagName)
	{
		Write('<');
		Write(tagName);
	}

	/// <summary>Writes any tab spacing and the closing tag of the specified markup element.</summary>
	/// <param name="tagName">The element to write the closing tag for. </param>
	public virtual void WriteEndTag(string tagName)
	{
		Write("</");
		Write(tagName);
		Write('>');
	}

	/// <summary>Writes any tab spacing and the opening tag of the specified markup element to the output stream.</summary>
	/// <param name="tagName">The element to write to the output stream. </param>
	public virtual void WriteFullBeginTag(string tagName)
	{
		Write('<');
		Write(tagName);
		Write('>');
	}

	/// <summary>Writes the specified style attribute to the output stream.</summary>
	/// <param name="name">The style attribute to write to the output stream. </param>
	/// <param name="value">The value assigned to the style attribute. </param>
	public virtual void WriteStyleAttribute(string name, string value)
	{
		WriteStyleAttribute(name, value, fEncode: false);
	}

	/// <summary>Writes the specified style attribute and value to the output stream, and encodes the value, if specified.</summary>
	/// <param name="name">The style attribute to write to the output stream. </param>
	/// <param name="value">The value assigned to the style attribute. </param>
	/// <param name="fEncode">
	///       <see langword="true" /> to encode the style attribute and its assigned value; otherwise, <see langword="false" />. </param>
	public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
	{
		Write(name);
		Write(':');
		Write(EncodeAttributeValue(value, fEncode));
		Write(';');
	}

	/// <summary>Writes the text representation of a subarray of Unicode characters to the output stream, along with any pending tab spacing.</summary>
	/// <param name="buffer">The array of characters from which to write text to the output stream. </param>
	/// <param name="index">The index location in the array where writing begins. </param>
	/// <param name="count">The number of characters to write to the output stream. </param>
	public override void Write(char[] buffer, int index, int count)
	{
		OutputTabs();
		b.Write(buffer, index, count);
	}

	/// <summary>Writes the text representation of a double-precision floating-point number to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The double-precision floating-point number to write to the output stream. </param>
	public override void Write(double value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes the text representation of a Unicode character to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The Unicode character to write to the output stream. </param>
	public override void Write(char value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes the text representation of an array of Unicode characters to the output stream, along with any pending tab spacing.</summary>
	/// <param name="buffer">The array of Unicode characters to write to the output stream. </param>
	public override void Write(char[] buffer)
	{
		OutputTabs();
		b.Write(buffer);
	}

	/// <summary>Writes the text representation of a 32-byte signed integer to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The 32-byte signed integer to write to the output stream. </param>
	public override void Write(int value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes a tab string and a formatted string to the output stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method, along with any pending tab spacing.</summary>
	/// <param name="format">A string that contains zero or more format items. </param>
	/// <param name="arg0">An object to format.</param>
	public override void Write(string format, object arg0)
	{
		OutputTabs();
		b.Write(format, arg0);
	}

	/// <summary>Writes a formatted string that contains the text representation of two objects to the output stream, along with any pending tab spacing. This method uses the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
	/// <param name="format">A string that contains zero or more format items. </param>
	/// <param name="arg0">An object to format. </param>
	/// <param name="arg1">An object to format.</param>
	public override void Write(string format, object arg0, object arg1)
	{
		OutputTabs();
		b.Write(format, arg0, arg1);
	}

	/// <summary>Writes a formatted string that contains the text representation of an object array to the output stream, along with any pending tab spacing. This method uses the same semantics as the <see cref="M:System.String.Format(System.String,System.Object[])" /> method.</summary>
	/// <param name="format">A string that contains zero or more format items. </param>
	/// <param name="arg">An object array to format. </param>
	public override void Write(string format, params object[] arg)
	{
		OutputTabs();
		b.Write(format, arg);
	}

	/// <summary>Writes the specified string to the output stream, along with any pending tab spacing.</summary>
	/// <param name="s">The string to write to the output stream. </param>
	public override void Write(string s)
	{
		OutputTabs();
		b.Write(s);
	}

	/// <summary>Writes the text representation of a 64-byte signed integer to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The 64-byte signed integer to write to the output stream. </param>
	public override void Write(long value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes the text representation of an object to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The object to write to the output stream. </param>
	public override void Write(object value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes the text representation of a single-precision floating-point number to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The single-precision floating-point number to write to the output stream. </param>
	public override void Write(float value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes the text representation of a Boolean value to the output stream, along with any pending tab spacing.</summary>
	/// <param name="value">The <see cref="T:System.Boolean" /> to write to the output stream. </param>
	public override void Write(bool value)
	{
		OutputTabs();
		b.Write(value);
	}

	/// <summary>Writes the specified markup attribute and value to the output stream.</summary>
	/// <param name="name">The attribute to write to the output stream. </param>
	/// <param name="value">The value assigned to the attribute. </param>
	public virtual void WriteAttribute(string name, string value)
	{
		WriteAttribute(name, value, fEncode: false);
	}

	/// <summary>Writes any pending tab spacing and a Unicode character, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The character to write to the output stream. </param>
	public override void WriteLine(char value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of a 64-byte signed integer, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The 64-byte signed integer to write to the output stream. </param>
	public override void WriteLine(long value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of an object, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The object to write to the output stream. </param>
	public override void WriteLine(object value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of a double-precision floating-point number, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The double-precision floating-point number to write to the output stream. </param>
	public override void WriteLine(double value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and a subarray of Unicode characters, followed by a line terminator string, to the output stream.</summary>
	/// <param name="buffer">The character array from which to write text to the output stream. </param>
	/// <param name="index">The location in the character array where writing begins. </param>
	/// <param name="count">The number of characters in the array to write to the output stream. </param>
	public override void WriteLine(char[] buffer, int index, int count)
	{
		OutputTabs();
		b.WriteLine(buffer, index, count);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and an array of Unicode characters, followed by a line terminator string, to the output stream.</summary>
	/// <param name="buffer">The character array to write to the output stream. </param>
	public override void WriteLine(char[] buffer)
	{
		OutputTabs();
		b.WriteLine(buffer);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of a Boolean value, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The Boolean to write to the output stream. </param>
	public override void WriteLine(bool value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes a line terminator string to the output stream.</summary>
	public override void WriteLine()
	{
		OutputTabs();
		b.WriteLine();
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of a 32-byte signed integer, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The 32-byte signed integer to write to the output stream. </param>
	public override void WriteLine(int value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and a formatted string that contains the text representation of two objects, followed by a line terminator string, to the output stream.</summary>
	/// <param name="format">A string containing zero or more format items.</param>
	/// <param name="arg0">An object to format.</param>
	/// <param name="arg1">An object to format.</param>
	public override void WriteLine(string format, object arg0, object arg1)
	{
		OutputTabs();
		b.WriteLine(format, arg0, arg1);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and a formatted string containing the text representation of an object, followed by a line terminator string, to the output stream. </summary>
	/// <param name="format">A string containing zero or more format items. </param>
	/// <param name="arg0">An object to format.</param>
	public override void WriteLine(string format, object arg0)
	{
		OutputTabs();
		b.WriteLine(format, arg0);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and a formatted string that contains the text representation of an object array, followed by a line terminator string, to the output stream.</summary>
	/// <param name="format">A string containing zero or more format items.</param>
	/// <param name="arg">An object array to format. </param>
	public override void WriteLine(string format, params object[] arg)
	{
		OutputTabs();
		b.WriteLine(format, arg);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of a 4-byte unsigned integer, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The 4-byte unsigned integer to write to the output stream. </param>
	[CLSCompliant(false)]
	public override void WriteLine(uint value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and a text string, followed by a line terminator string, to the output stream.</summary>
	/// <param name="s">The string to write to the output stream. </param>
	public override void WriteLine(string s)
	{
		OutputTabs();
		b.WriteLine(s);
		newline = true;
	}

	/// <summary>Writes any pending tab spacing and the text representation of a single-precision floating-point number, followed by a line terminator string, to the output stream.</summary>
	/// <param name="value">The single-precision floating point number to write to the output stream. </param>
	public override void WriteLine(float value)
	{
		OutputTabs();
		b.WriteLine(value);
		newline = true;
	}

	/// <summary>Writes a string, followed by a line terminator string, to the output stream. This method ignores any specified tab spacing.</summary>
	/// <param name="s">The string to write to the output stream. </param>
	public void WriteLineNoTabs(string s)
	{
		b.WriteLine(s);
		newline = true;
	}

	internal HttpWriter GetHttpWriter()
	{
		return b as HttpWriter;
	}

	private void NextStyleStack()
	{
		if (styles == null)
		{
			styles = new AddedStyle[16];
		}
		if (++styles_pos >= styles.Length)
		{
			AddedStyle[] destinationArray = new AddedStyle[styles.Length * 2];
			Array.Copy(styles, destinationArray, styles.Length);
			styles = destinationArray;
		}
	}

	private void NextAttrStack()
	{
		if (attrs == null)
		{
			attrs = new AddedAttr[16];
		}
		if (++attrs_pos >= attrs.Length)
		{
			AddedAttr[] destinationArray = new AddedAttr[attrs.Length * 2];
			Array.Copy(attrs, destinationArray, attrs.Length);
			attrs = destinationArray;
		}
	}

	private void NextTagStack()
	{
		if (tagstack == null)
		{
			tagstack = new AddedTag[16];
		}
		if (++tagstack_pos >= tagstack.Length)
		{
			AddedTag[] destinationArray = new AddedTag[tagstack.Length * 2];
			Array.Copy(tagstack, destinationArray, tagstack.Length);
			tagstack = destinationArray;
		}
	}

	/// <summary>Checks an attribute to ensure that it can be rendered in the opening tag of a <see langword="&lt;form&gt;" /> markup element. </summary>
	/// <param name="attribute">A string that contains the name of the attribute to check. </param>
	/// <returns>Always <see langword="true" />.</returns>
	public virtual bool IsValidFormAttribute(string attribute)
	{
		return true;
	}

	/// <summary>Writes a <see langword="&lt;br /&gt;" /> markup element to the output stream. </summary>
	public virtual void WriteBreak()
	{
		string tagName = GetTagName(HtmlTextWriterTag.Br);
		WriteBeginTag(tagName);
		Write(" />");
	}

	/// <summary>Encodes the specified text for the requesting device, and then writes it to the output stream. </summary>
	/// <param name="text">The text string to encode and write to the output stream. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="text" /> is <see langword="null" />.</exception>
	public virtual void WriteEncodedText(string text)
	{
		Write(HttpUtility.HtmlEncode(text));
	}

	/// <summary>Encodes the specified URL, and then writes it to the output stream. The URL might include parameters.</summary>
	/// <param name="url">The URL string to encode and write to the output stream. </param>
	[MonoNotSupported("")]
	public virtual void WriteEncodedUrl(string url)
	{
		throw new NotImplementedException();
	}

	/// <summary>Encodes the specified URL parameter for the requesting device, and then writes it to the output stream.</summary>
	/// <param name="urlText">The URL parameter string to encode and write to the output stream. </param>
	[MonoNotSupported("")]
	public virtual void WriteEncodedUrlParameter(string urlText)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified string, encoding it according to URL requirements.</summary>
	/// <param name="text">The string to encode and write to the output stream. </param>
	/// <param name="argument">
	///       <see langword="true" /> to encode the string as a part of the parameter section of the URL; <see langword="false" /> to encode the string as part of the path section of the URL. </param>
	[MonoNotSupported("")]
	protected void WriteUrlEncodedString(string text, bool argument)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the opening tag of a <see langword="&lt;span&gt;" /> element that contains attributes that implement the layout and character formatting of the specified style. </summary>
	/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to begin applying to the block of markup. </param>
	[MonoNotSupported("")]
	public virtual void EnterStyle(Style style)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the opening tag of a markup element that contains attributes that implement the layout and character formatting of the specified style. </summary>
	/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to begin applying to the block of markup.</param>
	/// <param name="tag">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the opening tag of the markup element that will contain the style object specified in <paramref name="style" />. </param>
	[MonoNotSupported("")]
	public virtual void EnterStyle(Style style, HtmlTextWriterTag tag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the closing tag of a <see langword="&lt;span&gt;" /> element to end the specified layout and character formatting. </summary>
	/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to close. </param>
	[MonoNotSupported("")]
	public virtual void ExitStyle(Style style)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the closing tag of the specified markup element to end the specified layout and character formatting. </summary>
	/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> that specifies the layout and formatting to stop applying to the output text.</param>
	/// <param name="tag">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the closing tag of the markup element that contained the attributes that applied the specified style. This must match the key passed in the corresponding <see cref="Overload:System.Web.UI.HtmlTextWriter.EnterStyle" /> call. </param>
	[MonoNotSupported("")]
	public virtual void ExitStyle(Style style, HtmlTextWriterTag tag)
	{
		throw new NotImplementedException();
	}

	/// <summary>Notifies an <see cref="T:System.Web.UI.HtmlTextWriter" /> object, or an object of a derived class, that a control is about to be rendered. </summary>
	public virtual void BeginRender()
	{
	}

	/// <summary>Notifies an <see cref="T:System.Web.UI.HtmlTextWriter" /> object, or an object of a derived class, that a control has finished rendering. You can use this method to close any markup elements opened in the <see cref="M:System.Web.UI.HtmlTextWriter.BeginRender" /> method.</summary>
	public virtual void EndRender()
	{
	}
}
