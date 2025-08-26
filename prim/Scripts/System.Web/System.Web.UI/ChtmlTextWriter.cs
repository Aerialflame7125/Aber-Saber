using System.Collections;
using System.IO;

namespace System.Web.UI;

/// <summary>Writes a series of cHTML-specific characters and text to the output stream of an ASP.NET server control. The <see cref="T:System.Web.UI.ChtmlTextWriter" /> class provides formatting capabilities that ASP.NET server controls use when rendering cHTML content to clients.</summary>
public class ChtmlTextWriter : Html32TextWriter
{
	private static Hashtable global_suppressed_attrs;

	private static string[] global_suppressed_attributes;

	private static string[] recognized_attributes;

	private Hashtable recognized_attrs = new Hashtable(recognized_attributes.Length);

	private Hashtable suppressed_attrs = new Hashtable(recognized_attributes.Length);

	private Hashtable attr_render = new Hashtable();

	/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of globally suppressed attributes that cannot be rendered on cHTML elements. </summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> of globally suppressed cHTML attributes.</returns>
	protected Hashtable GlobalSuppressedAttributes => global_suppressed_attrs;

	/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of recognized attributes that could be rendered on cHTML elements.</summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> of recognized cHTML attributes.</returns>
	protected Hashtable RecognizedAttributes => recognized_attrs;

	/// <summary>Gets a <see cref="T:System.Collections.Hashtable" /> object of user-specified suppressed attributes that are not rendered on cHTML elements.</summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> of suppressed cHTML attributes.</returns>
	protected Hashtable SuppressedAttributes => suppressed_attrs;

	static ChtmlTextWriter()
	{
		global_suppressed_attributes = new string[10] { "onclick", "ondblclick", "onmousedown", "onmouseup", "onmouseover", "onmousemove", "onmouseout", "onkeypress", "onkeydown", "onkeyup" };
		recognized_attributes = new string[2] { "div", "span" };
		SetupGlobalSuppressedAttrs(global_suppressed_attributes);
	}

	private static void SetupGlobalSuppressedAttrs(string[] attrs)
	{
		global_suppressed_attrs = new Hashtable();
		PopulateHash(global_suppressed_attrs, global_suppressed_attributes);
	}

	private static void PopulateHash(Hashtable hash, string[] keys)
	{
		foreach (string key in keys)
		{
			hash.Add(key, true);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class that uses the <see cref="F:System.Web.UI.HtmlTextWriter.DefaultTabString" /> constant to indent lines.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
	public ChtmlTextWriter(TextWriter writer)
		: this(writer, "\t")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class with the specified line indentation.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> that renders the markup content. </param>
	/// <param name="tabString">The number of spaces defined in the <see cref="P:System.Web.UI.HtmlTextWriter.Indent" />. </param>
	public ChtmlTextWriter(TextWriter writer, string tabString)
		: base(writer, tabString)
	{
		string[] array = recognized_attributes;
		foreach (string key in array)
		{
			recognized_attrs.Add(key, new Hashtable());
		}
		SetupSuppressedAttrs();
	}

	private void SetupSuppressedAttrs()
	{
		string[] array = new string[5] { "accesskey", "cellspacing", "cellpadding", "gridlines", "rules" };
		string[] array2 = new string[4] { "cellspacing", "cellpadding", "gridlines", "rules" };
		Init("div", array, suppressed_attrs);
		Init("span", array2, suppressed_attrs);
	}

	private static void Init(string key, string[] attrs, Hashtable container)
	{
		Hashtable hashtable = new Hashtable(attrs.Length);
		PopulateHash(hashtable, attrs);
		container.Add(key, hashtable);
	}

	/// <summary>Adds an attribute to a cHTML element of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> object.</summary>
	/// <param name="elementName">The cHTML element to add the attribute to.</param>
	/// <param name="attributeName">The attribute to add to <paramref name="elementName" />.</param>
	public virtual void AddRecognizedAttribute(string elementName, string attributeName)
	{
		Hashtable hashtable = (Hashtable)recognized_attrs[elementName];
		if (hashtable == null)
		{
			hashtable = new Hashtable();
			hashtable.Add(attributeName, true);
			recognized_attrs.Add(elementName, hashtable);
		}
		else
		{
			hashtable.Add(attributeName, true);
		}
	}

	/// <summary>Removes an attribute of a cHTML element of the <see cref="T:System.Web.UI.ChtmlTextWriter" /> object.</summary>
	/// <param name="elementName">The cHTML element to remove an attribute from.</param>
	/// <param name="attributeName">The attribute to remove from <paramref name="elementName" />.</param>
	public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
	{
		((Hashtable)recognized_attrs[elementName])?.Remove(attributeName);
	}

	/// <summary>Writes a <see langword="br" /> element to the cHTML output stream.</summary>
	public override void WriteBreak()
	{
		string tagName = GetTagName(HtmlTextWriterTag.Br);
		WriteBeginTag(tagName);
		Write('>');
	}

	/// <summary>Encodes the specified text for the requesting device, and then writes it to the output stream. </summary>
	/// <param name="text">The text string to encode and write to the output stream. </param>
	public override void WriteEncodedText(string text)
	{
		base.WriteEncodedText(text);
	}

	/// <summary>Determines whether the specified cHTML attribute and its value are rendered to the requesting page. You can override the <see cref="M:System.Web.UI.ChtmlTextWriter.OnAttributeRender(System.String,System.String,System.Web.UI.HtmlTextWriterAttribute)" /> method in classes that derive from the <see cref="T:System.Web.UI.ChtmlTextWriter" /> class to filter out attributes that you do not want to render on devices that support cHTML.</summary>
	/// <param name="name">The cHTML attribute to render. </param>
	/// <param name="value">The value assigned to <paramref name="name" />. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterAttribute" /> associated with <paramref name="name" />. </param>
	/// <returns>
	///     <see langword="true" /> to write the attribute and its value to the <see cref="T:System.Web.UI.ChtmlTextWriter" /> output stream; otherwise, <see langword="false" />.</returns>
	protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
	{
		return (bool)attr_render[null];
	}

	/// <summary>Determines whether the specified cHTML markup style attribute and its value can be rendered to the current markup element.</summary>
	/// <param name="name">A string containing the name of the style attribute to render. </param>
	/// <param name="value">A string containing the value that is assigned to <paramref name="name" />. </param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterStyle" /> associated with <paramref name="name" />.</param>
	/// <returns>
	///     <see langword="true" /> if the style can be rendered; otherwise, <see langword="false" />.</returns>
	protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
	{
		return key == HtmlTextWriterStyle.Display;
	}

	/// <summary>Determines whether the specified cHTML markup element is rendered to the requesting page. </summary>
	/// <param name="name">A string containing the name of the cHTML element to render.</param>
	/// <param name="key">The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> associated with <paramref name="name" />.</param>
	/// <returns>
	///     <see langword="true" /> if the specified cHTML markup element can be rendered; otherwise, <see langword="false" />.</returns>
	protected override bool OnTagRender(string name, HtmlTextWriterTag key)
	{
		return key != HtmlTextWriterTag.Span;
	}
}
