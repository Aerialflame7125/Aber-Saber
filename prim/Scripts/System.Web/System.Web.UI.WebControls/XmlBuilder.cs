using System.Collections;
using System.Web.Compilation;
using System.Xml;

namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
public class XmlBuilder : ControlBuilder
{
	/// <summary>Adds literal content to the control.</summary>
	/// <param name="s">The literal content to add to the control.</param>
	public override void AppendLiteralString(string s)
	{
	}

	/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.WebControls.Xml" /> control's specified child control.</summary>
	/// <param name="tagName">The tag name of the child control.</param>
	/// <param name="attribs">An array of attributes contained in the child control.</param>
	/// <returns>The <see cref="M:System.Web.UI.ControlBuilder.GetChildControlType(System.String,System.Collections.IDictionary)" /> method is overridden to always return <see langword="null" />.</returns>
	public override Type GetChildControlType(string tagName, IDictionary attribs)
	{
		return null;
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.Xml" /> control needs to get its inner text.</summary>
	/// <returns>The <see cref="M:System.Web.UI.ControlBuilder.NeedsTagInnerText" /> method is overridden to always return <see langword="true" />.</returns>
	public override bool NeedsTagInnerText()
	{
		return true;
	}

	/// <summary>Sets the <see cref="T:System.Web.UI.WebControls.Xml" /> control's inner text. </summary>
	/// <param name="text">The value to insert as the inner text. </param>
	/// <exception cref="T:System.Xml.XmlException">The <see cref="T:System.String" /> object passed in is not well-formed XML.</exception>
	public override void SetTagInnerText(string text)
	{
		string text2 = text.Trim();
		if (text2 == "")
		{
			return;
		}
		XmlDocument xmlDocument = new XmlDocument();
		try
		{
			xmlDocument.LoadXml(text);
		}
		catch (XmlException ex)
		{
			Location location = new Location(base.Location);
			if (ex.LineNumber >= 0)
			{
				location.BeginLine += ex.LineNumber - 1;
			}
			base.Location = location;
			throw;
		}
		base.AppendLiteralString(text2);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.XmlBuilder" /> class.</summary>
	public XmlBuilder()
	{
	}
}
