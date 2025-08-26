using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Writes a series of HTML 3.2–specific characters and text to the output stream for an ASP.NET server control. The <see cref="T:System.Web.UI.Html32TextWriter" /> class provides formatting capabilities that ASP.NET server controls use when rendering HTML 3.2 content to clients.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Html32TextWriter : HtmlTextWriter
{
	private bool div_table_substitution;

	private bool bold;

	private bool italic;

	/// <summary>Gets or sets a Boolean value indicating whether to replace a <see langword="Table" /> element with a <see langword="Div" /> element to reduce the time that it takes to render a block of HTML.</summary>
	/// <returns>
	///     <see langword="true" /> to replace <see langword="Table" /> with <see langword="Div" />; otherwise, <see langword="false" />.</returns>
	[MonoTODO("no effect on html generation")]
	public bool ShouldPerformDivTableSubstitution
	{
		get
		{
			return div_table_substitution;
		}
		set
		{
			div_table_substitution = value;
		}
	}

	/// <summary>Gets or sets a Boolean value indicating whether the requesting device supports bold HTML text. Use the <see cref="P:System.Web.UI.Html32TextWriter.SupportsBold" /> property to conditionally render bold text to the <see cref="T:System.Web.UI.Html32TextWriter" /> output stream.</summary>
	/// <returns>
	///     <see langword="true" /> if the requesting device supports bold text; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[MonoTODO("no effect on html generation")]
	public bool SupportsBold
	{
		get
		{
			return bold;
		}
		set
		{
			bold = value;
		}
	}

	/// <summary>Gets or sets a Boolean value indicating whether the requesting device supports italic HTML text. Use the <see cref="P:System.Web.UI.Html32TextWriter.SupportsItalic" /> property to conditionally render italicized text to the <see cref="T:System.Web.UI.Html32TextWriter" /> output stream.</summary>
	/// <returns>
	///     <see langword="true" /> if the requesting device supports italic text; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[MonoTODO("no effect on html generation")]
	public bool SupportsItalic
	{
		get
		{
			return italic;
		}
		set
		{
			italic = value;
		}
	}

	/// <summary>Gets a collection of font information for the HTML to render.</summary>
	/// <returns>The collection of font information.</returns>
	[MonoTODO("Not implemented, always returns null")]
	protected Stack FontStack => null;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Html32TextWriter" /> class that uses the line indentation that is specified in the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> field when the requesting browser requires line indentation.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the HMTL content. </param>
	public Html32TextWriter(TextWriter writer)
		: base(writer)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Html32TextWriter" /> class that uses the specified line indentation.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the HMTL 3.2 content. </param>
	/// <param name="tabString">A string that represents the number of spaces defined by the <see cref="P:System.Web.UI.HtmlTextWriter.Indent" />. </param>
	public Html32TextWriter(TextWriter writer, string tabString)
		: base(writer, tabString)
	{
	}

	/// <summary>Writes the opening tag of the specified element to the HTML 3.2 output stream.</summary>
	/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value that indicates which HTML element to write. </param>
	public override void RenderBeginTag(HtmlTextWriterTag tagKey)
	{
		base.RenderBeginTag(tagKey);
	}

	/// <summary>Writes the end tag of an HTML element to the <see cref="T:System.Web.UI.Html32TextWriter" /> output stream, along with any font information that is associated with the element.</summary>
	public override void RenderEndTag()
	{
		base.RenderEndTag();
	}

	/// <summary>Returns the HTML element that is associated with the specified <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</summary>
	/// <param name="tagKey">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value to obtain the HTML element for. </param>
	/// <returns>The HTML element.</returns>
	protected override string GetTagName(HtmlTextWriterTag tagKey)
	{
		if (tagKey == HtmlTextWriterTag.Unknown || !Enum.IsDefined(typeof(HtmlTextWriterTag), tagKey))
		{
			return string.Empty;
		}
		return tagKey.ToString().ToLower(Helpers.InvariantCulture);
	}

	/// <summary>Determines whether to write the specified HTML style attribute and its value to the output stream.</summary>
	/// <param name="name">The HTML style attribute to write to the output stream. </param>
	/// <param name="value">The value associated with the HTML style attribute. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> enumeration value associated with the HTML style attribute. </param>
	/// <returns>
	///     <see langword="true" /> if the HTML style attribute and its value will be rendered to the output stream; otherwise, <see langword="false" />.</returns>
	protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
	{
		return base.OnStyleAttributeRender(name, value, key);
	}

	/// <summary>Determines whether to write the specified HTML element to the output stream.</summary>
	/// <param name="name">The HTML element to write to the output stream. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value associated with the HTML element. </param>
	/// <returns>
	///     <see langword="true" /> if the HTML element is written to the output stream; otherwise, <see langword="false" />.</returns>
	protected override bool OnTagRender(string name, HtmlTextWriterTag key)
	{
		return base.OnTagRender(name, key);
	}

	/// <summary>Writes any text or spacing that appears after the content of the HTML element.</summary>
	/// <returns>The spacing or text to write after the content of the HTML element; otherwise, if there is no such information to render, <see langword="null" />.</returns>
	protected override string RenderAfterContent()
	{
		return base.RenderAfterContent();
	}

	/// <summary>Writes any spacing or text that occurs after an HTML element's closing tag.</summary>
	/// <returns>The spacing or text to write after the closing tag of the HTML element; otherwise, if there is no such information to render, <see langword="null" />.</returns>
	protected override string RenderAfterTag()
	{
		return base.RenderAfterTag();
	}

	/// <summary>Writes any tab spacing or font information that appears before the content that is contained in an HTML element.</summary>
	/// <returns>The font information or spacing to write before rendering the content of the HTML element; otherwise, if there is no such information to render, <see langword="null" />.</returns>
	protected override string RenderBeforeContent()
	{
		return base.RenderBeforeContent();
	}

	/// <summary>Writes any text or tab spacing that occurs before the opening tag of an HTML element to the HTML 3.2 output stream.</summary>
	/// <returns>The HTML font and spacing information to render before the tag; otherwise, if there is no such information to render, <see langword="null" />.</returns>
	protected override string RenderBeforeTag()
	{
		return base.RenderBeforeTag();
	}
}
