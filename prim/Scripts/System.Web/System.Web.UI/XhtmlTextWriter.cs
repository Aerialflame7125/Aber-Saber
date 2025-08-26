using System.Collections;
using System.IO;

namespace System.Web.UI;

/// <summary>Writes Extensible Hypertext Markup Language (XHTML)-specific characters, including all variations of XHTML modules that derive from XTHML, to the output stream for an ASP.NET server control for mobile devices. Override the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class to provide custom XHTML rendering for ASP.NET pages and server controls.</summary>
public class XhtmlTextWriter : HtmlTextWriter
{
	private static Hashtable default_common_attrs;

	private static Hashtable default_suppress_common_attrs;

	private static Hashtable default_element_specific_attrs;

	private Hashtable common_attrs;

	private Hashtable suppress_common_attrs;

	private Hashtable element_specific_attrs;

	private static string[] DefaultCommonAttributes;

	private static string[] DefaultSuppressCommonAttributes;

	/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object containing common attributes of the markup tags for the <see cref="T:System.Web.UI.XhtmlTextWriter" /> object.</summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> object containing common attributes.</returns>
	protected Hashtable CommonAttributes
	{
		get
		{
			if (common_attrs == null)
			{
				common_attrs = (Hashtable)default_common_attrs.Clone();
			}
			return common_attrs;
		}
	}

	/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object containing element-specific attributes.</summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> object containing element-specific attributes.</returns>
	protected Hashtable ElementSpecificAttributes
	{
		get
		{
			if (element_specific_attrs == null)
			{
				element_specific_attrs = (Hashtable)default_element_specific_attrs.Clone();
			}
			return element_specific_attrs;
		}
	}

	/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of elements for which <see cref="P:System.Web.UI.XhtmlTextWriter.CommonAttributes" /> attributes are suppressed.</summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> of elements containing a collection of <see cref="P:System.Web.UI.XhtmlTextWriter.CommonAttributes" /> that are not rendered.</returns>
	protected Hashtable SuppressCommonAttributes
	{
		get
		{
			if (suppress_common_attrs == null)
			{
				suppress_common_attrs = (Hashtable)default_suppress_common_attrs.Clone();
			}
			return suppress_common_attrs;
		}
	}

	static XhtmlTextWriter()
	{
		DefaultCommonAttributes = new string[4] { "class", "id", "title", "xml:lang" };
		DefaultSuppressCommonAttributes = new string[7] { "base", "meta", "br", "head", "title", "html", "style" };
		default_common_attrs = new Hashtable(DefaultCommonAttributes.Length);
		SetupHash(default_common_attrs, DefaultCommonAttributes);
		default_suppress_common_attrs = new Hashtable(DefaultSuppressCommonAttributes.Length);
		SetupHash(default_suppress_common_attrs, DefaultSuppressCommonAttributes);
		SetupElementsSpecificAttributes();
	}

	private static void SetupHash(Hashtable hash, string[] values)
	{
		foreach (string key in values)
		{
			hash.Add(key, true);
		}
	}

	private static void SetupElementsSpecificAttributes()
	{
		default_element_specific_attrs = new Hashtable();
		string[] attributesNames = new string[9] { "accesskey", "href", "charset", "hreflang", "rel", "type", "rev", "title", "tabindex" };
		SetupElementSpecificAttributes("a", attributesNames);
		string[] attributesNames2 = new string[1] { "href" };
		SetupElementSpecificAttributes("base", attributesNames2);
		string[] attributesNames3 = new string[1] { "cite" };
		SetupElementSpecificAttributes("blockquote", attributesNames3);
		string[] attributesNames4 = new string[3] { "id", "class", "title" };
		SetupElementSpecificAttributes("br", attributesNames4);
		string[] attributesNames5 = new string[3] { "action", "method", "enctype" };
		SetupElementSpecificAttributes("form", attributesNames5);
		string[] attributesNames6 = new string[1] { "xml:lang" };
		SetupElementSpecificAttributes("head", attributesNames6);
		string[] attributesNames7 = new string[3] { "version", "xml:lang", "xmlns" };
		SetupElementSpecificAttributes("html", attributesNames7);
		string[] attributesNames8 = new string[5] { "src", "alt", "width", "longdesc", "height" };
		SetupElementSpecificAttributes("img", attributesNames8);
		string[] attributesNames9 = new string[11]
		{
			"size", "accesskey", "title", "name", "type", "disabled", "value", "src", "checked", "maxlength",
			"tabindex"
		};
		SetupElementSpecificAttributes("input", attributesNames9);
		string[] attributesNames10 = new string[2] { "accesskey", "for" };
		SetupElementSpecificAttributes("label", attributesNames10);
		string[] attributesNames11 = new string[1] { "value" };
		SetupElementSpecificAttributes("li", attributesNames11);
		string[] attributesNames12 = new string[7] { "hreflang", "rev", "type", "charset", "rel", "href", "media" };
		SetupElementSpecificAttributes("link", attributesNames12);
		string[] attributesNames13 = new string[5] { "content", "name", "xml:lang", "http-equiv", "scheme" };
		SetupElementSpecificAttributes("meta", attributesNames13);
		string[] attributesNames14 = new string[12]
		{
			"codebase", "classid", "data", "standby", "name", "type", "height", "archive", "declare", "width",
			"tabindex", "codetype"
		};
		SetupElementSpecificAttributes("object", attributesNames14);
		string[] attributesNames15 = new string[1] { "start" };
		SetupElementSpecificAttributes("ol", attributesNames15);
		string[] attributesNames16 = new string[2] { "label", "disabled" };
		SetupElementSpecificAttributes("optgroup", attributesNames16);
		string[] attributesNames17 = new string[2] { "selected", "value" };
		SetupElementSpecificAttributes("option", attributesNames17);
		string[] attributesNames18 = new string[5] { "id", "name", "valuetype", "value", "type" };
		SetupElementSpecificAttributes("param", attributesNames18);
		string[] attributesNames19 = new string[1] { "xml:space" };
		SetupElementSpecificAttributes("pre", attributesNames19);
		string[] attributesNames20 = new string[1] { "cite" };
		SetupElementSpecificAttributes("q", attributesNames20);
		string[] attributesNames21 = new string[5] { "name", "tabindex", "disabled", "multiple", "size" };
		SetupElementSpecificAttributes("select", attributesNames21);
		string[] attributesNames22 = new string[5] { "xml:lang", "xml:space", "type", "title", "media" };
		SetupElementSpecificAttributes("style", attributesNames22);
		string[] attributesNames23 = new string[2] { "width", "summary" };
		SetupElementSpecificAttributes("table", attributesNames23);
		string[] attributesNames24 = new string[5] { "name", "cols", "accesskey", "tabindex", "rows" };
		SetupElementSpecificAttributes("textarea", attributesNames24);
		string[] attributesNames25 = new string[8] { "headers", "align", "rowspan", "colspan", "axis", "scope", "abbr", "valign" };
		SetupElementSpecificAttributes("td", attributesNames25);
		SetupElementSpecificAttributes("th", attributesNames25);
		string[] attributesNames26 = new string[1] { "xml:lang" };
		SetupElementSpecificAttributes("title", attributesNames26);
		string[] attributesNames27 = new string[2] { "align", "valign" };
		SetupElementSpecificAttributes("tr", attributesNames27);
	}

	private static void SetupElementSpecificAttributes(string elementName, string[] attributesNames)
	{
		Hashtable hashtable = new Hashtable(attributesNames.Length);
		SetupHash(hashtable, attributesNames);
		default_element_specific_attrs.Add(elementName, hashtable);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class that uses the line indentation that is specified in the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> field. Use the <see cref="M:System.Web.UI.XhtmlTextWriter.#ctor(System.IO.TextWriter)" /> constructor if you do not want to change the default line indentation.</summary>
	/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> instance that renders the XHTML content. </param>
	public XhtmlTextWriter(TextWriter writer)
		: this(writer, "\t")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class with the specified line indentation.</summary>
	/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> instance that renders the XHTML content. </param>
	/// <param name="tabString">The string used to render a line indentation.</param>
	public XhtmlTextWriter(TextWriter writer, string tabString)
		: base(writer, tabString)
	{
	}

	/// <summary>Adds an attribute to an XHTML element. The collection of element-specific attributes for the <see cref="T:System.Web.UI.XhtmlTextWriter" /> object is referenced by the <see cref="P:System.Web.UI.XhtmlTextWriter.ElementSpecificAttributes" /> property.</summary>
	/// <param name="elementName">The XHTML element to add the attribute to.</param>
	/// <param name="attributeName">The attribute to add.</param>
	public virtual void AddRecognizedAttribute(string elementName, string attributeName)
	{
		Hashtable hashtable = (Hashtable)ElementSpecificAttributes[elementName];
		if (hashtable == null)
		{
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add(attributeName, true);
			ElementSpecificAttributes.Add(elementName, hashtable2);
		}
		else
		{
			hashtable.Add(attributeName, true);
		}
	}

	/// <summary>Checks an XHTML attribute to ensure that it can be rendered in the opening tag of a <see langword="&lt;form&gt;" /> element.</summary>
	/// <param name="attributeName">The attribute name to check. </param>
	/// <returns>
	///     <see langword="true" /> if the attribute can be applied to a <see langword="&lt;form&gt;" /> element; otherwise, <see langword="false" />.</returns>
	public override bool IsValidFormAttribute(string attributeName)
	{
		if (!(attributeName == "action") && !(attributeName == "method"))
		{
			return attributeName == "enctype";
		}
		return true;
	}

	/// <summary>Removes an attribute from the <see cref="P:System.Web.UI.XhtmlTextWriter.ElementSpecificAttributes" /> collection of an element.</summary>
	/// <param name="elementName">The XHTML element to remove an attribute from.</param>
	/// <param name="attributeName">The attribute to remove from the specified XHTML element.</param>
	public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
	{
		((Hashtable)ElementSpecificAttributes[elementName])?.Remove(attributeName);
	}

	/// <summary>Specifies the XHTML document type for the text writer to render to the page or control.</summary>
	/// <param name="docType">One of the <see cref="T:System.Web.UI.XhtmlMobileDocType" /> enumeration values. </param>
	public virtual void SetDocType(XhtmlMobileDocType docType)
	{
	}

	/// <summary>Writes a <see langword="&lt;br/&gt;" /> element to the XHTML output stream.</summary>
	public override void WriteBreak()
	{
		string tagName = GetTagName(HtmlTextWriterTag.Br);
		WriteBeginTag(tagName);
		Write('/');
		Write('>');
	}

	/// <summary>Determines whether the specified XHTML attribute and its value can be rendered to the current markup element.</summary>
	/// <param name="name">The XHTML attribute to render. </param>
	/// <param name="value">The value assigned to the XHTML attribute. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> enumeration value associated with the XHTML attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the attribute is rendered to the page; otherwise, <see langword="false" />.</returns>
	protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
	{
		throw new ArgumentNullException();
	}

	/// <summary>Determines whether the specified XHTML style attribute and its value can be rendered to the current markup element.</summary>
	/// <param name="name">The XHTML style attribute to render. </param>
	/// <param name="value">The value assigned to the XHTML style attribute. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value associated with the XHTML style attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the style attribute is rendered; otherwise, <see langword="false" />.</returns>
	protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
	{
		return false;
	}
}
